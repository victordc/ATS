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
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Get this user
                int userId = 4;

                //Get leaves and credits from CodeTable
                IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
                LeaveCategoryGridView.DataSource = history.ToList();
                LeaveCategoryGridView.DataBind();
            }
        }

        protected void LeavesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans();
            LeaveCategoryGridView.DataSource = leaves.OrderBy(x => DataBinder.Eval(x, e.SortExpression)).ToList();
            LeaveCategoryGridView.DataBind();
        }

    }
}