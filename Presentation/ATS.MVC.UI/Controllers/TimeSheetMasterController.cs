using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data;
using ATS.Data.Model;
using ATS.Data.DAL;

namespace ATS.MVC.UI.Controllers
{
    public class TimeSheetMasterController : BaseController
    {
        //
        // GET: /TimeSheetMaster/

        public ActionResult Index()
        {
            var timeSheetMaster = TimesheetRepository.GetAllTimeSheetMasters();
            return View(timeSheetMaster.ToList());
        }

        //
        // GET: /TimeSheetMaster/Details/5

        public ActionResult Details(int id)
        {
            TimeSheetMaster master = TimesheetRepository.GetTimeSheetMasterById(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        //
        // GET: /TimeSheetMaster/Create

        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName");
            ViewBag.SupervisorId = new SelectList(TimesheetRepository.GetAllSupervisors(), "PersonId", "PersonName");
            ViewBag.CompanyId = new SelectList(TimesheetRepository.GetAllCompanies(), "CompanyId", "CompanyDescription");
            return View();
        }

        //
        // POST: /TimeSheetMaster/Create

        [HttpPost]
        public ActionResult Create(TimeSheetMaster master)
        {
            if (ModelState.IsValid)
            {
                TimeSheetMaster newMaster = master;
                newMaster.Save();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(TimesheetRepository.GetAllPersons(), "PersonId", "PersonName");
            ViewBag.ManagerId = new SelectList(TimesheetRepository.GetAllSupervisors(), "PersonId", "PersonName");
            ViewBag.CompanyId = new SelectList(TimesheetRepository.GetAllCompanies(), "CompanyId", "CompanyDescription");
            return View(master);
        }

        //
        // GET: /TimeSheetMaster/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeSheetMaster master = TimesheetRepository.GetTimeSheetMasterById(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        //
        // POST: /TimeSheetMaster/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeSheetMaster master)
        {
            TimeSheetMaster newMaster = master;
            if (ModelState.IsValid)
            {
                newMaster.Save();
                return RedirectToAction("Index");
            }
            return View(master);
        }

        //
        // GET: /TimeSheetMaster/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeSheetMaster master = TimesheetRepository.GetTimeSheetMasterById(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        //
        // POST: /TimeSheetMaster/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSheetMaster.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
