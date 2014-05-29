﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;
using ATS.MVC.UI.Common;

namespace ATS.MVC.UI.Controllers
{
    public class LeavePlanController : Controller
    {
        private ATSCEEntities db = new ATSCEEntities();

        //
        // GET: /LeavePlan/
        public ActionResult Index()
        {
            int currentUserId = UserSetting.Current.UserId;
            //int currentUserId = 1;
            var leavePlans = TimesheetRepository.GetLeavePlans(currentUserId);
            return View(leavePlans.ToList());
        }

        //
        // GET: /LeavePlan/Details/5

        public ActionResult Details(int id = 0)
        {
            LeavePlan leaveplan = TimesheetRepository.GetLeavePlanById(id);
            if (leaveplan == null)
            {
                return HttpNotFound();
            }
            return View(leaveplan);
        }

        //
        // GET: /LeavePlan/Create

        public ActionResult Create()
        {
            ViewBag.LeaveCategoryId = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc");
            int currentUserId = UserSetting.Current.UserId;
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName", currentUserId);
            return View();
        }

        //
        // POST: /LeavePlan/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeavePlan leaveplan)
        {
            if (ModelState.IsValid)
            {
                TimesheetRepository.AddUpdateLeavePlan(leaveplan);
                return RedirectToAction("Index");
            }

            ViewBag.LeaveCategoryId = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", leaveplan.LeaveCategoryId);
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName", leaveplan.PersonId);
            return View(leaveplan);
        }

        //
        // GET: /LeavePlan/Edit/5

        public ActionResult Edit(int id = 0)
        {
            LeavePlan leaveplan = TimesheetRepository.GetLeavePlanById(id);
            if (leaveplan == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaveCategoryId = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", leaveplan.LeaveCategoryId);
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName", leaveplan.PersonId);
            return View(leaveplan);
        }

        //
        // POST: /LeavePlan/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeavePlan leaveplan)
        {

            if (ModelState.IsValid)
            {
                TimesheetRepository.AddUpdateLeavePlan(leaveplan);
                return RedirectToAction("Index");
            }
            ViewBag.LeaveCategoryId = new SelectList(TimesheetRepository.GetLeaveCategories(), "LeaveCategoryId", "LeaveCategoryDesc", leaveplan.LeaveCategoryId);
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName", leaveplan.PersonId);
            return View(leaveplan);
        }

        //
        // GET: /LeavePlan/Delete/5

        public ActionResult Delete(int id = 0)
        {
            LeavePlan leaveplan = TimesheetRepository.GetLeavePlanById(id);
            if (leaveplan == null)
            {
                return HttpNotFound();
            }
            return View(leaveplan);
        }

        //
        // POST: /LeavePlan/Delete/5

        public ActionResult Supervise()
        {
            int thisUserId = UserSetting.Current.UserId;
            var leavePlans = TimesheetRepository.GetAllPersonsBySupervisorId(thisUserId);
            return View(leavePlans.ToList());
        }

        public bool AdmitOrReject(int LeavePlanId, bool AdmitReject)
        {
            try
            {
                var leavePlans = TimesheetRepository.AdmitReject(LeavePlanId, AdmitReject);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimesheetRepository.RemoveLeavePlan(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}