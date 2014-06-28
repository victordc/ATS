using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.DAL;
using ATS.Data.Model;
using ATS.BLL;
using ATS.MVC.UI.Filters;
using WebMatrix.WebData;
using System.Web.Security;

namespace ATS.MVC.UI.Controllers
{
    public class SupervisorController : BaseController
    {
        private MaintainPersonFacade personFacade;

        public SupervisorController()
        {
            this.personFacade = new MaintainPersonFacade();
        }

        
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View(personFacade.GetSupervisors());
        }

        //
        // GET: /Supervisor/Details/5

        public ActionResult Details(int id)
        {
            var supervisor = personFacade.GetSupervisorById(id);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Create

        public ActionResult Create()
        {
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription");
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName");
            return View();
        }

        //
        // POST: /Supervisor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(Supervisor supervisor)
        {
            try
            {
                if (!personFacade.IsUniqueEmail(supervisor))
                {
                    ModelState.AddModelError("Email", "Email is taken!");
                }

                if (!personFacade.IsUniqueUsername(supervisor))
                {
                    ModelState.AddModelError("UserName", "User name is taken!");
                }
                if (ModelState.IsValid)
                {
                    personFacade.InsertSupervisor(supervisor);
                    WebSecurity.CreateUserAndAccount(supervisor.UserName, "p@ssword1");
                    Roles.AddUserToRole(supervisor.UserName, "Supervisor");
                    return RedirectToAction("Details", new { id = supervisor.PersonId });
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription");
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName");

            return View(supervisor);
        }

        //
        // GET: /Supervisor/Edit/5

        public ActionResult Edit(int id)
        {
            Supervisor supervisor = personFacade.GetSupervisorById(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription", supervisor.CompanyId);
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
            return View(supervisor);
        }

        //
        // POST: /Supervisor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supervisor supervisor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personFacade.UpdateSupervisor(supervisor);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription", supervisor.CompanyId);
            ViewBag.AgentId = new SelectList(personFacade.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Delete/5

        public ActionResult Delete(int id)
        {
            Supervisor supervisor = personFacade.GetSupervisorById(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        //
        // POST: /Supervisor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                personFacade.DeleteSupervisor(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GET : /Supervisor/AssignStaff/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AssignStaff(int id)
        {
            Supervisor supervisor = personFacade.GetSupervisorById(id);
            //find supervided Staffs
            IEnumerable<Staff> supervisedStaffs = personFacade.GetSupervisedStaffs(supervisor);
            IEnumerable<Staff> avaiableStaffs = personFacade.GetUnsupervisedStaffs();
            IEnumerable<SelectListItem> assignedStaffs = Enumerable.Empty<SelectListItem>();
            if (supervisedStaffs != null && supervisedStaffs.Count<Staff>() > 0)
            {
                assignedStaffs = from value in supervisedStaffs
                    select new SelectListItem
                    {
                        Text = value.PersonName,
                        Value = value.PersonId.ToString(),
                        Selected = false,
                    };
            }
            IEnumerable<SelectListItem> freeStaffs = Enumerable.Empty<SelectListItem>();
            if(avaiableStaffs != null && avaiableStaffs.Count<Staff>() > 0)
            {
                freeStaffs = from value in avaiableStaffs
                select new SelectListItem
                {
                    Text = value.PersonName,
                    Value = value.PersonId.ToString(),
                    Selected = false,
                };
            }
            ViewBag.AssignedStaffs = assignedStaffs;
            ViewBag.AvailableStaffs = freeStaffs;
            return View(supervisor);
        }

        [HttpPost, ActionName("AssignStaff")]
        public ActionResult AssignStaffConfirm(int id)
        {
            Supervisor supervisor = personFacade.GetSupervisorById(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }

            string assignedStaffIdStr = Request.Params.Get("assignedStaffIds");
            string availableStaffIdStr = Request.Params.Get("availableStaffIds");
            if (assignedStaffIdStr.Length > 0)
            {
                int[] assignedStaffIds = assignedStaffIdStr.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                personFacade.AssignStaffsUnderSupervisor(supervisor, assignedStaffIds);
            }

            if (availableStaffIdStr.Length > 0)
            {
                int[] availableStaffIds = availableStaffIdStr.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                personFacade.RemoveStaffsFromSupervisor(supervisor, availableStaffIds);
            }

            //Ajax method to assign the staffs to Supervisor
            if (Request.IsAjaxRequest())
            {
                return Json(new { code = 1 });
            }
            else
            {
                return View(supervisor);
            }
        }

        
    }
}
