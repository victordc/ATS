using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;

namespace ATS.MVC.UI.Controllers
{
    public class CalendarController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans();
            return View(leaves);
        }

        [HttpPost]
        public ActionResult GetLeavesForMonth(int month, int year)
        {
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansByMonth(month, year);
            return View(leaves);
        }

    }
}
