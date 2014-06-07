using ATS.Data;
using ATS.Data.Model;
using ATS.MVC.UI.Common;
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
                //Get this user
                int currentUserId = UserSetting.Current.UserId;

                IEnumerable<LeavePlan> leaves = TimesheetRepository.GetLeavePlansForTeam(currentUserId);
                LeavesGridView.DataSource = leaves.ToList();
                LeavesGridView.DataBind();
            }
        }
    }
}