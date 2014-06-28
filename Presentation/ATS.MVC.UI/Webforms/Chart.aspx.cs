using ATS.Data;
using ATS.Data.Model;
using ATS.MVC.UI.Common;
using ATS.MVC.UI.Webforms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.Webforms.UI
{
    public partial class Chart : System.Web.UI.Page
    {
        //Get leaves and credits tables
        IEnumerable<LeaveCategory> categories = null;
        IEnumerable<LeavePlan> history = null; 
        List<ChartReport> newHistory = new List<ChartReport>();
        public string JsonResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get this user
                int currentUserId = UserSetting.Current.PersonId;
                history = TimesheetRepository.GetLeavePlans(currentUserId, DateTime.Now.Year);

                //Get leaves and credits from CodeTable
                categories = TimesheetRepository.GetLeaveCategories();

                ReportBinder(currentUserId);
            }
        }
        private void ReportBinder(int id)
        {
            //First LeaveDetail entry at start of year with full eligibility
            ChartReport cr = new ChartReport();
            cr.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            List<LeaveCategory> cd = categories.ToList();
            foreach (LeaveCategory ct in cd)
            {
                if (ct.LeaveCategoryDesc.ToLower() == "mc") { cr.MC = Convert.ToDouble(ct.LeaveCategoryLimit); }
                else if (ct.LeaveCategoryDesc.ToLower() == "annual") { cr.Annual = Convert.ToDouble(ct.LeaveCategoryLimit); }
                else if (ct.LeaveCategoryDesc.ToLower() == "exams") { cr.Exams = Convert.ToDouble(ct.LeaveCategoryLimit); }
                else if (ct.LeaveCategoryDesc.ToLower() == "business") { cr.Business = Convert.ToDouble(ct.LeaveCategoryLimit); }
            }
            this.newHistory.Add(cr);

            foreach (LeavePlan lp in history)
            {
                cr = new ChartReport();
                if (lp.PersonId == id && lp.Admitted == true)
                {
                    cr.StartDate = lp.StartDate.ToString("yyyy-MM-dd");
                    double duration = lp.Duration;
                    foreach (LeaveCategory ct in cd)
                    {
                        if (ct.LeaveCategoryDesc.ToLower() == lp.LeaveCategory.LeaveCategoryDesc.ToLower())
                        {
                            ct.LeaveCategoryLimit = Convert.ToInt32(Convert.ToDouble(ct.LeaveCategoryLimit) - duration);
                        }
                        if (ct.LeaveCategoryDesc.ToLower() == "mc") { cr.MC = Convert.ToDouble(ct.LeaveCategoryLimit); }
                        else if (ct.LeaveCategoryDesc.ToLower() == "annual") { cr.Annual = Convert.ToDouble(ct.LeaveCategoryLimit); }
                        else if (ct.LeaveCategoryDesc.ToLower() == "exams") { cr.Exams = Convert.ToDouble(ct.LeaveCategoryLimit); }
                        else if (ct.LeaveCategoryDesc.ToLower() == "business") { cr.Business = Convert.ToDouble(ct.LeaveCategoryLimit); }
                    }    
                    this.newHistory.Add(cr);
                }
            }

            var jsonSerializer = new JavaScriptSerializer();
            JsonResult = jsonSerializer.Serialize(newHistory);
        }


    }

}