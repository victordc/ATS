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
            int currentUserId = 1;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(currentUserId);
            return View(leaves);
        }

        public ActionResult Team()
        {
            int supId = 4;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(supId); 
            return View(leaves);
        }

        public ActionResult MyDetail(int Year, int Month)
        {
            int currentUserId = 1;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(currentUserId);
            return View(leaves);
        }

        public ActionResult TeamDetail(int Year, int Month)
        {
            int supId = 4;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(supId);
            return View(leaves);
        }

    }
}
