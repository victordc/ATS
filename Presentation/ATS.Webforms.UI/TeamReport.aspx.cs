using ATS.Data;
using ATS.Data.Model;
using ATS.Framework;
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
                int currentUserId = UserSetting.Current.PersonId;
                //LogManager.LogInfo("CurrentUserId = " + currentUserId);
                TeamReportBinder(currentUserId);

            }
        }
        private void TeamReportBinder(int id)
        {
            IEnumerable<LeavePlan> leaves = null;

            //If admin, show all
            if (id == 0)
            {
                leaves = TimesheetRepository.GetLeavePlans();
            }
            else
            {
                leaves = TimesheetRepository.GetLeavePlansForTeam(id);
            }
            LeavesGridView.DataSource = leaves.ToList();
            LeavesGridView.DataBind();

        }
    }
}