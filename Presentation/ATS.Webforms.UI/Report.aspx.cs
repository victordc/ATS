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
                //Get leaves and credits from CodeTable
                IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
                LeaveCategoryGridView.DataSource = history.ToList();
                LeaveCategoryGridView.DataBind();
            }
        }

        protected void LeaveCategoryGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
            LeaveCategoryGridView.DataSource = history.OrderByDescending(x => DataBinder.Eval(x, e.SortExpression)).ToList();
            LeaveCategoryGridView.DataBind();
        }

    }
}