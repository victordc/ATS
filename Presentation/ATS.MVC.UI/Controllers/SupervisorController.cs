using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.DAL;
using ATS.Data.Model;

namespace ATS.MVC.UI.Controllers
{
    public class SupervisorController : Controller
    {
        private IPersonRepository personRepository;

        public SupervisorController()
        {
            this.personRepository = new PersonRepository(new ATSCEEntities());
        }

        public SupervisorController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        //
        // GET: /Supervisor/

        public ActionResult Index()
        {
            return View(personRepository.GetSupervisors());
        }

        //
        // GET: /Supervisor/Details/5

        public ActionResult Details(int id)
        {
            var supervisor = personRepository.GetSupervisorByID(id);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Create

        public ActionResult Create()
        {
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription");
            ViewBag.AgentId = new SelectList(personRepository.GetAgents(), "PersonId", "PersonName");
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
                    personRepository.InsertSupervisor(supervisor);
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
            ViewBag.AgentId = new SelectList(personRepository.GetAgents(), "PersonId", "PersonName");

            return View(supervisor);
        }

        //
        // GET: /Supervisor/Edit/5

        public ActionResult Edit(int id)
        {
            Supervisor supervisor = personRepository.GetSupervisorByID(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            //TODO, Add CompanyID selection
            ViewBag.CompanyId = new SelectList(Company.GetAll(), "CompanyId", "CompanyDescription", supervisor.CompanyId);
            ViewBag.AgentId = new SelectList(personRepository.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
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
                    personRepository.UpdateSupervisor(supervisor);
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
            ViewBag.AgentId = new SelectList(personRepository.GetAgents(), "PersonId", "PersonName", supervisor.AgentId);
            return View(supervisor);
        }

        //
        // GET: /Supervisor/Delete/5

        public ActionResult Delete(int id)
        {
            Supervisor supervisor = personRepository.GetSupervisorByID(id);
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
                personRepository.DeleteSupervisor(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
