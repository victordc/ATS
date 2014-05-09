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
    public class AgentController : Controller
    {
        private IPersonRepository personRepository;

        public AgentController()
        {
            this.personRepository = new PersonRepository(new ATSCEEntities());
        }

        public AgentController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        //
        // GET: /Agent/

        public ActionResult Index()
        {
            return View(personRepository.GetAgents());
        }

        //
        // GET: /Agent/Details/5

        public ActionResult Details(int id)
        {
            var agent = personRepository.GetAgentByID(id);
            return View(agent);
        }

        //
        // GET: /Agent/Create

        public ActionResult Create()
        {
            ViewBag.SupervisorId = new SelectList(personRepository.GetSupervisors(), "PersonId", "PersonName");
            
            return View();
        }

        //
        // POST: /Agent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Agent agent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personRepository.InsertAgent(agent);
                    return RedirectToAction("Details", new { id = agent.PersonId });
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personRepository.GetSupervisors(), "PersonId", "PersonName");
            

            return View(agent);
        }

        //
        // GET: /Agent/Edit/5

        public ActionResult Edit(int id)
        {
            Agent agent = personRepository.GetAgentByID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupervisorId = new SelectList(personRepository.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
            return View(agent);
        }

        //
        // POST: /Agent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent agent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personRepository.UpdateAgent(agent);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personRepository.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
            return View(agent);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            Agent agent = personRepository.GetAgentByID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        //
        // POST: /Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                personRepository.DeleteAgent(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
