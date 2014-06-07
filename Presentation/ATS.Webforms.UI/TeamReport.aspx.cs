using ATS.Data;
using ATS.Data.Model;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = 1;
                IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(userId);
                LeavesGridView.DataSource = leaves.ToList();
                LeavesGridView.DataBind();
            }
        }
    }
}