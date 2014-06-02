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
using Setting = ATS.MVC.UI.Common.UserSetting;


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

        private IEnumerable<webpages_Roles> RoleList
        {
            get
            {
                if (Session["RoleList"] == null)
                {
                    Session["RoleList"] = TimesheetRepository.Instance.GetAllRoles();
                }
                return (IEnumerable<webpages_Roles>)Session["RoleList"];
            }
        }

        public ActionResult AddCompany()
        {
            if (Setting.Current.SetupCompany.Company != null)
                return View(Setting.Current.SetupCompany.Company);
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(Company data, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    Setting.Current.SetupCompany.Company = data;
                    BindSupervisorRole();
                    ViewBag.Users = Setting.Current.SetupCompany.Supervisors;
                    return View("AddSupervisor");
                }
            }
            return View();
        }

        public ActionResult AddSupervisor()
        {
            BindSupervisorRole();
            ViewBag.Users = Setting.Current.SetupCompany.Supervisors;
            return View();
        }

        [HttpPost]
        public ActionResult AddSupervisor(RegisterModel data, string btnAdd, string prevBtn, string nextBtn)
        {
            if (btnAdd != null)
            {
                //if (IsDuplicatedName(data.UserName))
                //{
                //    ModelState.AddModelError(string.Empty, "Duplicated Name");
                //}
                BindSupervisorRole();
                
                if (ModelState.IsValid)
                {
                    if (Setting.Current.SetupCompany.Supervisors == null)
                    {
                        Setting.Current.SetupCompany.Supervisors = new List<RegisterModel>();
                    }
                    Setting.Current.SetupCompany.Supervisors.Add(data);
                    ViewBag.Users = Setting.Current.SetupCompany.Supervisors;
                }
                return View();
            }
            
            return View();
        }

        public ActionResult AddAgent()
        {
            BindAgentRole();
            ViewBag.Users = Setting.Current.SetupCompany.Agents;
            return View();
        }

        [HttpPost]
        public ActionResult AddAgent(RegisterModel data, string btnAdd, string prevBtn, string nextBtn)
        {
            if (btnAdd != null)
            {
                if (Setting.Current.SetupCompany.Agents == null)
                {
                    Setting.Current.SetupCompany.Agents = new List<RegisterModel>();
                }
                Setting.Current.SetupCompany.Agents.Add(data);
                BindAgentRole();
                ViewBag.Users = Setting.Current.SetupCompany.Agents;
                return View();
            }
            
            return View();
        }

        public ActionResult AddStaff()
        {
            ViewBag.Users = Setting.Current.SetupCompany.Staffs;
            BindStaffRole();
            BindSupervisorList();
            BindAgentList(); 
            return View();
        }

        [HttpPost]
        public ActionResult AddStaff(RegisterModel data, string btnAdd, string prevBtn, string finishBtn)
        {
            if (btnAdd != null)
            {
                if (Setting.Current.SetupCompany.Staffs == null)
                {
                    Setting.Current.SetupCompany.Staffs = new List<RegisterModel>();
                }
                Setting.Current.SetupCompany.Staffs.Add(data);
                ViewBag.Users = Setting.Current.SetupCompany.Staffs;
                BindStaffRole();
                BindSupervisorList();
                BindAgentList();
                return View();
            }
            else
            {
                //1. Create user account for supervisor/agent/staff
                for (int i = 0; i < Setting.Current.SetupCompany.Supervisors.Count; i++)
                {
                    CreateUserAccount(Setting.Current.SetupCompany.Supervisors[i]);
                }
                for (int i = 0; i < Setting.Current.SetupCompany.Agents.Count; i++)
                {
                    CreateUserAccount(Setting.Current.SetupCompany.Agents[i]);
                }
                for (int i = 0; i < Setting.Current.SetupCompany.Staffs.Count; i++)
                {
                    CreateUserAccount(Setting.Current.SetupCompany.Staffs[i]);
                }

                //2. Setup company
                TimesheetRepository.Instance.SetupCompany(Setting.Current.SetupCompany);

                //3. Clear session
                Setting.Current.SetupCompany = null;

                //4. Redirect to successfull page
                return RedirectToAction("SuccessSetupCompary");
            }
        }

        public ActionResult SuccessSetupCompary()
        {
            return View();
        }

        /// <summary>
        /// Check whether username is existed.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [InitializeSimpleMembership]
        public JsonResult DoesUserNameExist(string UserName)
        {
            var user = Membership.GetUser(UserName);
            if (user != null)
                return Json(false);

            if (Setting.Current.SetupCompany.Supervisors != null)
            {
                var query = Setting.Current.SetupCompany.Supervisors.Where(r => r.UserName == UserName).FirstOrDefault();
                if (query != null)
                    return Json(false);
            }

            return Json(true);
        }

        #endregion

        #region Helper

        private void BindSupervisorRole()
        {
            var role = RoleList.Where(r => r.RoleName == "Supervisor");
            SelectList list = new SelectList(role.ToList(), "RoleName", "RoleName");
            ViewBag.myList = list;
        }

        private void BindAgentRole()
        {
            var role = RoleList.Where(r => r.RoleName == "Agent");
            SelectList list = new SelectList(role.ToList(), "RoleName", "RoleName");
            ViewBag.myList = list;
        }

        private void BindStaffRole()
        {
            var role = RoleList.Where(r => r.RoleName == "Staff");
            SelectList list = new SelectList(role.ToList(), "RoleName", "RoleName");
            ViewBag.myList = list;
        }

        private void BindSupervisorList()
        {
            SelectList list = new SelectList(Setting.Current.SetupCompany.Supervisors, "FullName", "FullName");
            ViewBag.Supervisors = list;
        }

        private void BindAgentList()
        {
            SelectList list = new SelectList(Setting.Current.SetupCompany.Agents, "FullName", "FullName");
            ViewBag.Agents = list;
        }

        private bool IsDuplicatedName(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user != null)
                return true;

            if (Setting.Current.SetupCompany.Supervisors != null)
            {
                var query = Setting.Current.SetupCompany.Supervisors.Where(r => r.UserName == userName).FirstOrDefault();
                if (query != null)
                    return true;
            }

            return false;
        }

        [InitializeSimpleMembership]
        private int CreateUserAccount(RegisterModel model)
        {   
            WebSecurity.CreateUserAndAccount(model.UserName, "password");
            Roles.AddUserToRole(model.UserName, model.RoleName);
            return 1;
        }
        #endregion
    }
}
