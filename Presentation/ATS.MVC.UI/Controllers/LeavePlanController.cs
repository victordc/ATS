using System;
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
            int currentUserId = UserSetting.Current.PersonId;
            ViewBag.UserRole = UserSetting.Current.RoleName;
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
            int currentUserId = UserSetting.Current.PersonId;
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName", currentUserId);
            return View();
        }

        //
        // POST: /LeavePlan/Create

        [HttpPost]
        public string Create(LeavePlan leaveplan)
        {

            int currentUserId = UserSetting.Current.PersonId;
            leaveplan.PersonId = currentUserId;

            if (leaveplan.StartDate < DateTime.Now || leaveplan.EndDate < DateTime.Now)
            {
                if ((TimesheetRepository.getLeaveCategoryById(leaveplan.LeaveCategoryId).LeaveCategoryDesc != "MC"))
                {
                    return "Start and End Dates can not be in the past, unless you apply for MC. Please correct.";
                }

            }

            if (TimesheetRepository.getTotalRemainingLeaveDays(leaveplan) - leaveplan.Duration < 0)
            {
                return "Your total remaining leaves for theis leave category is less than the applied duration.";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!TimesheetRepository.CheckLeavesOverlaps(leaveplan))
                    {
                        return "The selected dates have overlap with previously applied leaves, please correct.";
                    }
                    TimesheetRepository.AddUpdateLeavePlan(leaveplan);
                    return "True";
                }
                catch (Exception ex)
                {
                    return "False";
                }
            }
            return "False";
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
        public string Edit(LeavePlan leaveplan)
        {

            int currentUserId = UserSetting.Current.PersonId;
            leaveplan.PersonId = currentUserId;

            if (leaveplan.StartDate < DateTime.Now || leaveplan.EndDate < DateTime.Now)
            {
                if ((TimesheetRepository.getLeaveCategoryById(leaveplan.LeaveCategoryId).LeaveCategoryDesc != "MC"))
                {
                    return "Start and End Dates can not be in the past, unless you apply for MC. Please correct.";
                }
            }

            if (TimesheetRepository.getTotalRemainingLeaveDays(leaveplan) - leaveplan.Duration < 0)
            {
                return "Your total remaining leaves for theis leave category is less than the applied duration.";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!TimesheetRepository.CheckLeavesOverlaps(leaveplan))
                    {
                        return "The selected dates have overlap with previously applied leaves, please correct.";
                    }
                    TimesheetRepository.AddUpdateLeavePlan(leaveplan);
                    return "True";
                }
                catch (Exception)
                {
                    return "False";
                }
            }
            return "False";
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
            int thisUserId = UserSetting.Current.PersonId;
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

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TimesheetRepository.RemoveLeavePlan(id);
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        public bool DeleteConfirmed(int id)
        {
            return TimesheetRepository.RemoveLeavePlan(id);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}