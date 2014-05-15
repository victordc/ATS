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
            int currentUserId = 1;
            //IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(currentUserId);
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(currentUserId); 
            return View(leaves);
        }

    }
}
