using ATS.Data;
using ATS.Data.Model;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Get this user
                int userId = 4;

                //Get leaves and credits from CodeTable
                IEnumerable<CodeTable> codes = TimesheetRepository.Instance.GetCodeTableByGroup("LEAVE_TYPE");
                CodeGridView.DataSource = codes.ToList();
                CodeGridView.DataBind();

                //Get leaves and credits from CodeTable
                IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
                CreditsGridView.DataSource = history.ToList();
                CreditsGridView.DataBind();

                IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(userId);
                LeavesGridView.DataSource = leaves.ToList();
                LeavesGridView.DataBind();
            }
        }

    }
}