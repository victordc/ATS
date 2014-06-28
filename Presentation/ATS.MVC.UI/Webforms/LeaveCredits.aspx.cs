using ATS.BusinessFacade;
using ATS.Data;
using ATS.Data.Model;
using ATS.MVC.UI.Common;
using ATS.MVC.UI.Webforms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.Webforms.UI
{
    public partial class LeaveCredits : System.Web.UI.Page
    {
        //Get leaves and credits from CodeTable
        IEnumerable<LeaveCategory> categories = null;
        List<LeaveReport> newHistory = new List<LeaveReport>();
        IEnumerable<LeavePlan> history = null;
        IAdminFacade adminFacade = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                categories = TimesheetRepository.GetLeaveCategories();

                //Get this user
                int currentUserId = UserSetting.Current.PersonId;

                //Get leaves and credits from CodeTable
                this.history = TimesheetRepository.GetLeavePlans(currentUserId, DateTime.Now.Year);

                ReportBinder(currentUserId);
            }
        }

        private void ReportBinder(int id)
        {
            //Bind leave eligibility first
            LeaveCategoryGridView.DataSource = categories.ToList();
            LeaveCategoryGridView.DataBind();
            
            foreach (LeavePlan lp in history)
            {
                LeaveReport lr = new LeaveReport();
                List<LeaveCategory> cd = categories.ToList();

                    if (lp.PersonId == id && lp.Admitted == true)
                    {
                        lr.LeavePlanId = lp.LeavePlanId;
                        lr.LeaveType = lp.LeaveCategory.LeaveCategoryDesc;
                        lr.Name = lp.Person.PersonName;
                        lr.StartDate = lp.StartDate;
                        lr.EndDate = lp.EndDate;
                        lr.Duration = lp.Duration;
                        lr.Admitted = lp.Admitted;
                        //Get leave credits
                        var cr = cd.FirstOrDefault(x => x.LeaveCategoryDesc.ToLower() == lp.LeaveCategory.LeaveCategoryDesc.ToLower());
                        //Decrement by this duration
                        lr.Credit = Convert.ToDouble(cr.LeaveCategoryLimit) - lr.Duration;
                        //Update new credits
                        cr.LeaveCategoryLimit = Convert.ToInt32(lr.Credit);
                        this.newHistory.Add(lr);
                    }
            }

            CreditsGridView.DataSource = newHistory;
            CreditsGridView.DataBind();
        }

    }
}