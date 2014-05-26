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
    public class AgentController : BaseController
    {
        private MaintainPersonBLL personBLL;

        public AgentController()
        {
            this.personBLL = new MaintainPersonBLL();
        }

        
        //
        // GET: /Agent/

        public ActionResult Index()
        {
            return View(personBLL.GetAgents());
        }

        //
        // GET: /Agent/Details/5

        public ActionResult Details(int id)
        {
            var agent = personBLL.GetAgentById(id);
            return View(agent);
        }

        //
        // GET: /Agent/Create

        public ActionResult Create()
        {
            ViewBag.SupervisorId = new SelectList(personBLL.GetSupervisors(), "PersonId", "PersonName");
            
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
                    personBLL.InsertAgent(agent);
                    return RedirectToAction("Details", new { id = agent.PersonId });
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personBLL.GetSupervisors(), "PersonId", "PersonName");
            

            return View(agent);
        }

        //
        // GET: /Agent/Edit/5

        public ActionResult Edit(int id)
        {
            Agent agent = personBLL.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupervisorId = new SelectList(personBLL.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
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
                    personBLL.UpdateAgent(agent);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personBLL.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
            return View(agent);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            Agent agent = personBLL.GetAgentById(id);
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
                personBLL.DeleteAgent(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// GET : /Agent/AssignStaff/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AssignStaff(int id)
        {
            Agent agent = personBLL.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            //find supervided Staffs
            IEnumerable<Staff> agentedStaffs = personBLL.GetRepresentedStaffs(agent);
            IEnumerable<Staff> avaiableStaffs = personBLL.GetStaffsWithoutAgency();
            IEnumerable<SelectListItem> assignedStaffs = Enumerable.Empty<SelectListItem>();
            if(agentedStaffs != null && agentedStaffs.Count<Staff>() > 0)
            {
                assignedStaffs = from value in agentedStaffs
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
            return View(agent);
        }

        [HttpPost]
        public ActionResult AssignStaffConfirm(int id)
        {
            //Ajax method to assign the staffs to Supervisor
            return null;
        }
    }
}
