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
        //Get leaves and credits from CodeTable
        IEnumerable<CodeTable> codes = null;
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
                codes = CodeTable.GetByGroupCode("LEAVE_TYPE"); 

                ReportBinder(currentUserId);
            }
        }
        private void ReportBinder(int id)
        {
            //First LeaveDetail entry at start of year with full eligibility
            ChartReport cr = new ChartReport();
            cr.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            List<CodeTable> cd = codes.ToList(); 
            foreach (CodeTable ct in cd)
            {
                if (ct.Code.ToLower() == "mc") { cr.MC = Convert.ToDouble(ct.CodeDesc); }
                else if (ct.Code.ToLower() == "annual") { cr.Annual = Convert.ToDouble(ct.CodeDesc); }
                else if (ct.Code.ToLower() == "exams") { cr.Exams = Convert.ToDouble(ct.CodeDesc); }
            }
            this.newHistory.Add(cr);

            foreach (LeavePlan lp in history)
            {
                cr = new ChartReport();
                if (lp.PersonId == id && lp.Admitted == true)
                {
                    cr.StartDate = lp.StartDate.ToString("yyyy-MM-dd");
                    double duration = lp.Duration;
                    foreach (CodeTable ct in cd)
                    {
                        if (ct.Code.ToLower() == lp.LeaveCategory.LeaveCategoryDesc.ToLower())
                        {
                            ct.CodeDesc = Convert.ToString(Convert.ToDouble(ct.CodeDesc) - duration);
                        }
                        if (ct.Code.ToLower() == "mc") { cr.MC = Convert.ToDouble(ct.CodeDesc); }
                        else if (ct.Code.ToLower() == "annual") { cr.Annual = Convert.ToDouble(ct.CodeDesc); }
                        else if (ct.Code.ToLower() == "exams") { cr.Exams = Convert.ToDouble(ct.CodeDesc); }
                    }    
                    this.newHistory.Add(cr);
                }
            }

            var jsonSerializer = new JavaScriptSerializer();
            JsonResult = jsonSerializer.Serialize(newHistory);
        }


    }

}