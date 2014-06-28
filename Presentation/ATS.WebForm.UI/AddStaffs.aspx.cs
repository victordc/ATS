using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.WebForm.UI
{
    public partial class AddStaffs : SetupCompanyBaseController
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            currentPage = "AddStaffs.aspx";
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ConfirmSetupPage();
        }

        protected void btnSubmitPrev_Click(object sender, EventArgs e)
        {
            PrevPage();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (setupCompany.Staffs == null)
            {
                setupCompany.Staffs = new List<RegisterModel>();
            }
            RegisterModel agn = new RegisterModel()
            {
                UserName = UserName.Text,
                RoleName = "Staff",
                Email = Email.Text,
                AgentName = Agent.SelectedValue,
                SupervisorName = Supervisor.SelectedValue
            };

            setupCompany.Staffs.Add(agn);
            BindData();
        }

        private void BindData()
        {
            dv.DataSource = setupCompany.Staffs;
            dv.DataBind();

            Supervisor.DataSource = setupCompany.Supervisors;
            Supervisor.DataValueField = "UserName";
            Supervisor.DataTextField = "UserName";
            Supervisor.DataBind();

            Agent.DataSource = setupCompany.Agents;
            Agent.DataValueField = "UserName";
            Agent.DataTextField = "UserName";
            Agent.DataBind();
        }

    }
}