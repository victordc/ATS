using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.DAL;
using ATS.Data.Model;
using ATS.BusinessFacade;
using ATS.MVC.UI.Filters;
using WebMatrix.WebData;
using System.Web.Security;
using ATS.MVC.UI.Common;

namespace ATS.MVC.UI.Controllers
{
    public class StaffController : BaseController
    {
        private MaintainPersonFacade personFacade;

        public StaffController()
        {
            this.personFacade = new MaintainPersonFacade();
        }

        //
        // GET: /Staff/

        public ActionResult Index()
        {
            IEnumerable<Staff> staffs = Enumerable.Empty<Staff>();
            var userName = UserSetting.Current.UserName;
            var userRole = UserSetting.Current.RoleName;
            if (userRole == "Agent")
            {
                Agent agent = personFacade.GetAgentByUsername(userName);
                if (agent != null)
                {
                    staffs = personFacade.GetRepresentedStaffs(agent);
                }
                ViewBag.ViewOnly = false;
            }
            else if (userRole == "Supervisor")
            {
                Supervisor supervisor = personFacade.GetSupervisorByUsername(userName);
                if (supervisor != null)
                {
                    staffs = personFacade.GetSupervisedStaffs(supervisor);
                }
                ViewBag.ViewOnly = true;
            }
            else if (userRole == "Administrator")
            {
                staffs = personFacade.GetStaffs();
                ViewBag.ViewOnly = false;
            }
            return View(staffs);
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id)
        {
             var userRole = UserSetting.Current.RoleName;
             if (userRole == "Supervisor") ViewBag.ViewOnly = true;
             else ViewBag.ViewOnly = false;
            var staff = personFacade.GetStaffById(id);
            return View(staff);
        }

        //
        // GET: /Staff/Create

        public ActionResult Create()
        {
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName");
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName");
            return View();
        }

        //
        // POST: /Staff/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(Staff staff)
        {
            try
            {
                if (!personFacade.IsUniqueEmail(staff))
                {
                    ModelState.AddModelError("Email", "Email is taken!");
                }

                if (!personFacade.IsUniqueUsername(staff))
                {
                    ModelState.AddModelError("UserName", "User name is taken!");
                }

                if (ModelState.IsValid)
                {
                    personFacade.InsertStaff(staff);
                    WebSecurity.CreateUserAndAccount(staff.UserName, "p@ssword1");
                    Roles.AddUserToRole(staff.UserName, "Staff");
                    return RedirectToAction("Details", new { id = staff.PersonId});
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName");
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName");

            return View(staff);
        }

        //
        // GET: /Staff/Edit/5

        public ActionResult Edit(int id)
        {
            Staff staff = personFacade.GetStaffById(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName", staff.SupervisorId);
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName", staff.AgentId);
            return View(staff);
        }

        //
        // POST: /Staff/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            try
            {
                if (!personFacade.IsUniqueEmail(staff))
                {
                    ModelState.AddModelError("Email", "Email is taken!");
                }

                if (ModelState.IsValid)
                {
                    personFacade.UpdateStaff(staff);
                    return RedirectToAction("Index");
                }
                
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName", staff.SupervisorId);
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName", staff.AgentId);
            return View(staff);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            Staff staff = personFacade.GetStaffById(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                personFacade.DeleteStaff(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
