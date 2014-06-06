﻿using System;
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
            return View(personFacade.GetStaffs());
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id)
        {
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
        public ActionResult Create(Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    personFacade.InsertStaff(staff);
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
