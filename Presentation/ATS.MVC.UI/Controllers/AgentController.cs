using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.DAL;
using ATS.Data.Model;
using ATS.BLL;
using WebMatrix.WebData;
using System.Web.Security;
using ATS.MVC.UI.Filters;

namespace ATS.MVC.UI.Controllers
{
    public class AgentController : BaseController
    {
        private MaintainPersonFacade personFacade;

        public AgentController()
        {
            this.personFacade = new MaintainPersonFacade();
        }

        
        //
        // GET: /Agent/

        public ActionResult Index()
        {
            return View(personFacade.GetAgents());
        }

        //
        // GET: /Agent/Details/5

        public ActionResult Details(int id)
        {
            var agent = personFacade.GetAgentById(id);
            return View(agent);
        }

        //
        // GET: /Agent/Create

        public ActionResult Create()
        {
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName");
            
            return View();
        }

        //
        // POST: /Agent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Create(Agent agent)
        {
            try
            {
                if(!personFacade.IsUniqueEmail(agent))
                {
                    ModelState.AddModelError("Email", "Email is taken!");
                }

                if (!personFacade.IsUniqueUsername(agent))
                {
                    ModelState.AddModelError("UserName", "User name is taken!");
                }

                if (ModelState.IsValid)
                {
                    personFacade.InsertAgent(agent);
                    WebSecurity.CreateUserAndAccount(agent.UserName, "p@ssword1");
                    Roles.AddUserToRole(agent.UserName, "Agent");
                    return RedirectToAction("Details", new { id = agent.PersonId });
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName");
            

            return View(agent);
        }

        //
        // GET: /Agent/Edit/5

        public ActionResult Edit(int id)
        {
            Agent agent = personFacade.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
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
                    personFacade.UpdateAgent(agent);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log error?
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.SupervisorId = new SelectList(personFacade.GetSupervisors(), "PersonId", "PersonName", agent.SupervisorId);
            return View(agent);
        }

        //
        // GET: /Staff/Delete/5

        public ActionResult Delete(int id)
        {
            Agent agent = personFacade.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        //
        // POST: /Staff/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                personFacade.DeleteAgent(id);
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
            Agent agent = personFacade.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            //find supervided Staffs
            IEnumerable<Staff> agentedStaffs = personFacade.GetRepresentedStaffs(agent);
            IEnumerable<Staff> avaiableStaffs = personFacade.GetStaffsWithoutAgency();
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

        [HttpPost, ActionName("AssignStaff")]
        public ActionResult AssignStaffConfirm(int id)
        {
            Agent agent = personFacade.GetAgentById(id);
            if (agent == null)
            {
                return HttpNotFound();
            }

            string assignedStaffIdStr = Request.Params.Get("assignedStaffIds");
            string availableStaffIdStr = Request.Params.Get("availableStaffIds");
            if (assignedStaffIdStr.Length > 0)
            {
                int[] assignedStaffIds = assignedStaffIdStr.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                personFacade.AssignStaffsUnderAgent(agent, assignedStaffIds);
            }

            if (availableStaffIdStr.Length > 0)
            {
                int[] availableStaffIds = availableStaffIdStr.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                personFacade.RemoveStaffsFromAgent(agent, availableStaffIds);
            }

            //Ajax method to assign the staffs to Supervisor
            if (Request.IsAjaxRequest())
            {
                return Json(new { code = 1 });
            }
            else
            {
                return View(agent);
            }
        }
    }
}
