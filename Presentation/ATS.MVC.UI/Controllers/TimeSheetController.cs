using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;

namespace ATS.MVC.UI.Controllers
{
    public class TimeSheetController : BaseController
    {
        //
        // GET: /TimeSheet/

        public ActionResult Index()
        {
            var timeSheetDetails = TimesheetRepository.GetAllTimeSheetDetails();
            return View(timeSheetDetails.ToList());
        }

        //
        // GET: /TimeSheet/Details/5

        public ActionResult Details(int id)
        {
            TimeSheetDetail detail = TimesheetRepository.GetTimeSheetDetailById(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        //
        // GET: /TimeSheet/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeSheet/Create

        [HttpPost]
        public ActionResult Create(TimeSheetDetail detail)
        {
            if (ModelState.IsValid)
            {
                TimeSheetDetail newDetail = detail;
                newDetail.Save();
                return RedirectToAction("Index");
            }

            return View(detail);
        }

        //
        // GET: /TimeSheet/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeSheetDetail detail = TimesheetRepository.GetTimeSheetDetailById(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        //
        // POST: /TimeSheet/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheetDetail detail)
        {
            TimeSheetDetail newDetail = detail;
            if (ModelState.IsValid)
            {
                newDetail.Save();
                return RedirectToAction("Index");
            }
            return View(detail);
        }

        //
        // GET: /TimeSheet/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeSheetDetail detail = TimesheetRepository.GetTimeSheetDetailById(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        //
        // POST: /TimeSheet/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheetDetail.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
