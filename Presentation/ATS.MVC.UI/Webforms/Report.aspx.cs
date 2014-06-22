using ATS.Data;
using ATS.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATS.MVC.UI.Common;
using ATS.MVC.UI.Webforms;

namespace ATS.Webforms.UI
{
    public partial class Report : System.Web.UI.Page
    {
        //Get leaves and credits from CodeTable
        IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
        List<LeaveReport> newHistory = new List<LeaveReport>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                //Get this user
                int currentUserId = UserSetting.Current.PersonId; 

                ReportBinder(currentUserId);
            }
        }   

        private void ReportBinder(int id)
        {
            foreach (LeaveCategory lc in history)
            {
                    List<LeavePlan> llp = lc.LeavePlans.ToList();
                    foreach (LeavePlan lp in llp)
                    {
                        LeaveReport lr = new LeaveReport(); 
                        if (lp.PersonId == id || id==0)
                        {
                            lr.LeavePlanId = lp.LeavePlanId; 
                            lr.LeaveType = lc.LeaveCategoryDesc;
                            lr.Name = lp.Person.PersonName;
                            lr.StartDate = lp.StartDate;
                            lr.EndDate = lp.EndDate;
                            lr.Duration = lp.Duration;
                            lr.Admitted = lp.Admitted;
                            this.newHistory.Add(lr);
                        }
                    }
            }
            LeaveCategoryGridView.DataSource = newHistory;
            LeaveCategoryGridView.DataBind();
        }

    }
}