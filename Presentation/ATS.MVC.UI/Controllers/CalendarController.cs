using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.Data.Model;
using ATS.Data;
using ATS.MVC.UI.Common;

namespace ATS.MVC.UI.Controllers
{
    public class CalendarController : Controller
    {

        public ActionResult Index()
        {
            int currentUserId = UserSetting.Current.PersonId;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetUnrejectedLeavePlans(currentUserId);
            return View(leaves);
        }

        public ActionResult Team()
        {
            int currentUserId = UserSetting.Current.PersonId;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetUnrejectedLeavePlansForTeam(currentUserId); 
            return View(leaves);
        }

        public ActionResult MyDetail(int year, int month)
        {
            int currentUserId = UserSetting.Current.PersonId;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(currentUserId, year, month);
            return View(leaves);
        }

        public ActionResult TeamDetail(int year, int month)
        {
            int currentUserId = UserSetting.Current.PersonId;
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(currentUserId, year, month);
            return View(leaves);
            
        }

        public ActionResult OpenReport()
        {
            return Redirect("~/Reports/TeamReport");
        }

    }
}
