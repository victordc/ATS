using ATS.Data;
using ATS.Data.Model;
using ATS.MVC.UI.Common;
using ATS.MVC.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebMatrix.WebData;

namespace ATS.MVC.UI.Controllers
{
    //[ATSAuthorizeAttribute]
    public class AdminController : BaseController
    {
        #region Membership
        
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            // Get list of role
            ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();
            SelectList list = new SelectList(ViewBag.Roles, "RoleId", "RoleName");
            ViewBag.myList = list;

            // Get all users
            var users = TimesheetRepository.Instance.GetAllUsers();
            foreach (var item in users)
            {
                item.RoleName = Roles.GetRolesForUser(item.UserName).FirstOrDefault();
            }
            var query = from ur in users
                        select new ATS.Data.Model.UserProfile
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName,
                            RoleName = Roles.GetRolesForUser(ur.UserName).FirstOrDefault()
                        };
            ViewBag.Users = query.ToList().OrderBy(r=>r.UserId);

            // Get all ObjectAccesses
            var obs = TimesheetRepository.Instance.GetAllObjectAccesses();
            ViewBag.ObjectAccesses = obs;

            // Object Access
            return View();
        }

        public ActionResult UpdateUser(int userId = 0)
        {
            //1. Get all role to bind to ddl
            ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();

            //2. Add mode
            if (userId <= 0)
            {
                SelectList list = new SelectList(ViewBag.Roles, "RoleName", "RoleName");
                ViewBag.myList = list;
                return View();
            }
            //3. Edit mode
            else
            {
                var user = TimesheetRepository.Instance.GetUserById(userId);
                ATS.MVC.UI.Models.RegisterModel model = new ATS.MVC.UI.Models.RegisterModel();
                model.UserName = user.UserName;
                SelectList list = new SelectList(ViewBag.Roles, "RoleName", "RoleName", Roles.GetRolesForUser(user.UserName).FirstOrDefault());
                ViewBag.myList = list;
                return View(model);
            }
        }

        [HttpPost]
        [InitializeSimpleMembership]
        public ActionResult UpdateUser(ATS.MVC.UI.Models.RegisterModel model)
        {
            try
            {
                // Edit mode
                if (WebSecurity.UserExists(model.UserName))
                {
                    // Get previous role
                    string role = Roles.GetRolesForUser(model.UserName).FirstOrDefault();
                    if (string.IsNullOrEmpty(role) || !role.Equals(model.RoleName))
                    {
                        //1. Remove previous mode if any
                        if (!string.IsNullOrEmpty(role))
                        {
                            Roles.RemoveUserFromRole(model.UserName, role);
                        }
                        //2. Add new role
                        Roles.AddUserToRole(model.UserName, model.RoleName);
                    }
                }
                // Add mode
                else
                {
                    if (ModelState.IsValid)
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                        Roles.AddUserToRole(model.UserName, model.RoleName);
                        WebSecurity.Login(model.UserName, model.Password);
                    }
                }

                return RedirectToAction("Index", "Admin");
            }
            catch (MembershipCreateUserException e)
            {
                //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }

            // If we got this far, something failed, redisplay form
            return View(model); ;
        }

        [HttpPost]
        [InitializeSimpleMembership]
        public ActionResult DeleteUser(string userName)
        {
            // Attempt to register the user
            try
            {
                if (Roles.GetRolesForUser(userName).Count() > 0)
                {
                    Roles.RemoveUserFromRoles(userName, Roles.GetRolesForUser(userName));
                }
                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(userName); // deletes record from webpages_Membership table
                ((SimpleMembershipProvider)Membership.Provider).DeleteUser(userName, true); // deletes record from UserProfile table
                return Json("Deleted Completed");
            }
            catch (MembershipCreateUserException e)
            {
                //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        #endregion

        #region ObjectAccess

        public ActionResult UpdateObjectAccess(int objectAccessId = 0)
        {
            //1. Get all role to bind to ddl
            ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();

            //2. Add mode
            if (objectAccessId <= 0)
            {
                SelectList list = new SelectList(ViewBag.Roles, "RoleName", "RoleName");
                ViewBag.myList = list;
                return View();
            }
            //3. Edit mode
            else
            {
                var objectAccess = TimesheetRepository.Instance.GetObjectAccessById(objectAccessId);
                SelectList list = new SelectList(ViewBag.Roles, "RoleName", "RoleName", objectAccess.Role);
                ViewBag.myList = list;
                return View(objectAccess);
            }
        }

        [HttpPost]
        public ActionResult UpdateObjectAccess(ObjectAccess model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Edit mode
                    if (model.ObjectAccessId > 0)
                    {
                        model.UpdatedOn = DateTime.Now;
                        model.UpdatedBy = Helper.CurrentUserId;
                        TimesheetRepository.Instance.SaveObjectAccess(model);
                    }
                    // Add mode
                    else
                    {
                        model.AccessType = "W";
                        model.Status = "A";
                        model.CreatedBy = Helper.CurrentUserId;
                        model.CreatedOn = DateTime.Now;
                        TimesheetRepository.Instance.SaveObjectAccess(model);
                    }

                    return RedirectToAction("Index", "Admin");
                }
                catch (MembershipCreateUserException e)
                {
                    //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            // If we got this far, something failed, redisplay form
            return View();

        }

        [HttpPost]
        public ActionResult DeleteObjectAccess(int objectAccessId)
        {
            // Attempt to register the user
            try
            {
                ObjectAccess oa = new ObjectAccess { ObjectAccessId = objectAccessId };
                TimesheetRepository.Instance.DeleteObjectAccess(oa);
                return Json("Deleted Completed");

            }
            catch (MembershipCreateUserException e)
            {
                //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [HttpPost]
        public ActionResult SearchUsers(string userName)
        {
            IEnumerable<UserProfile> users = TimesheetRepository.Instance.GetUsersByName(userName);
            UserProfile user = users.FirstOrDefault();

            return Json(user);
        }

        #endregion

        #region Setup

        private SetupCompany CurrentSetupCompany
        {
            get
            {
                if (Session["SetupCompany"] == null)
                {
                    Session["SetupCompany"] = new SetupCompany();
                }
                return (SetupCompany)Session["SetupCompany"];
            }
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(Company data, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    SetupCompany obj = CurrentSetupCompany;
                    obj.Company = data;
                    ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();
                    SelectList list = new SelectList(ViewBag.Roles, "RoleId", "RoleName");
                    ViewBag.myList = list;
                    return View("AddSupervisor");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddSupervisor(RegisterModel data, string prevBtn, string nextBtn)
        {
            
            if (prevBtn != null)
            {
                return View("AddCompany", CurrentSetupCompany.Company);
            }

            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    CurrentSetupCompany.RegisterModels = new List<RegisterModel>();
                    CurrentSetupCompany.RegisterModels.Add(data);
                    ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();
                    SelectList list = new SelectList(ViewBag.Roles, "RoleId", "RoleName");
                    ViewBag.myList = list;
                    return View("AddAgent");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddAgent(ATS.MVC.UI.Models.RegisterModel data, string prevBtn, string nextBtn)
        {
            if (prevBtn != null)
            {
                ViewBag.Roles = TimesheetRepository.Instance.GetAllRoles();
                SelectList list = new SelectList(ViewBag.Roles, "RoleId", "RoleName");
                ViewBag.myList = list;
                return View("AddSupervisor", CurrentSetupCompany.RegisterModels[0]);
            }

            if (nextBtn != null)
            {
                //if (ModelState.IsValid)
                {
                    //CurrentSetupCompany.UserProfiles = new List<UserProfile>();

                    return RedirectToAction("SuccessSetupCompary");
                }
            }
            return View();
        }

        public ActionResult SuccessSetupCompary()
        {
            return View();
        }

        #endregion
    }
}
