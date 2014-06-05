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
                IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans(); 
                LeavesGridView.DataSource = leaves.ToList();
                LeavesGridView.DataBind();
            }
        }

        protected void LeavesGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlans();
            LeavesGridView.DataSource = leaves.OrderBy(x => DataBinder.Eval(x, e.SortExpression)).ToList();
            LeavesGridView.DataBind();
        }

    }
}