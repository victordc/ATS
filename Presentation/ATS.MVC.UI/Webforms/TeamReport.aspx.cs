using ATS.Data;
using ATS.Data.Model;
using ATS.Framework;
using ATS.MVC.UI.Common;
using ATS.MVC.UI.Webforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.Webforms.UI
{
    public partial class TeamReport : System.Web.UI.Page
    {
        IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
        List<LeaveReport> newHistory = new List<LeaveReport>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Get this user
                int currentUserId = UserSetting.Current.PersonId;

                TeamReportBinder(currentUserId);
            }
        }
        private void TeamReportBinder(int id)
        {
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(id);
            List<LeavePlan> llp = leaves.ToList();

            foreach (LeavePlan lp in llp)
            {
                LeaveReport lr = new LeaveReport();
                lr.LeavePlanId = lp.LeavePlanId;
                lr.LeaveType = lp.LeaveCategory.LeaveCategoryDesc;
                lr.Name = lp.Person.PersonName;
                lr.StartDate = lp.StartDate;
                lr.EndDate = lp.EndDate;
                lr.Duration = lp.Duration;
                lr.Admitted = lp.Admitted;
                this.newHistory.Add(lr);
            }
            LeavesGridView.DataSource = newHistory;
            LeavesGridView.DataBind();

            if (!leaves.Any())
            {
                TeamReportLabel.Text = "No leave approved yet for this year.";
            }
        }
    }
}