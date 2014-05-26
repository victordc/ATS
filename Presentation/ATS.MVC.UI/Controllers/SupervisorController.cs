using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.DAL;
using ATS.Data.Model;
using ATS.BLL;

namespace ATS.MVC.UI.Controllers
{
    public class SupervisorController : BaseController
    {
        private MaintainPersonBLL personBLL;

        public SupervisorController()
        {
            this.personBLL = new MaintainPersonBLL();
        }

        
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View(personBLL.GetSupervisors());
        }

        //
        // GET: /Supervisor/Details/5

        public ActionResult Details(int id)
        {
            var supervisor = personBLL.GetSupervisorById(id);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Create

        public ActionResult Create()
        {
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription");
            ViewBag.AgentId = new SelectList(personBLL.GetAgents(), "PersonId", "PersonName");
            return View();
        }

        //
        // POST: /Supervisor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supervisor supervisor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personBLL.InsertSupervisor(supervisor);
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
            ViewBag.AgentId = new SelectList(personBLL.GetAgents(), "PersonId", "PersonName");

            return View(supervisor);
        }

        //
        // GET: /Supervisor/Edit/5

        public ActionResult Edit(int id)
        {
            Supervisor supervisor = personBLL.GetSupervisorById(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription", supervisor.CompanyId);
            ViewBag.AgentId = new SelectList(personBLL.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
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
                    personBLL.UpdateSupervisor(supervisor);
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
            ViewBag.AgentId = new SelectList(personBLL.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Delete/5

        public ActionResult Delete(int id)
        {
            Supervisor supervisor = personBLL.GetSupervisorById(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        //
        // POST: /Supervisor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                personBLL.DeleteSupervisor(id);
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
            Supervisor supervisor = personBLL.GetSupervisorById(id);
            //find supervided Staffs
            IEnumerable<Staff> supervisedStaffs = personBLL.GetSupervisedStaffs(supervisor);
            IEnumerable<Staff> avaiableStaffs = personBLL.GetUnsupervisedStaffs();
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

        [HttpPost]
        public ActionResult AssignStaffConfirm(int id)
        {
            //Ajax method to assign the staffs to Supervisor
            return null;
        }

        
    }
}
