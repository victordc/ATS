﻿using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.WebForm.UI
{
    public partial class AddAgents : SetupCompanyBaseController
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            currentPage = "AddAgents.aspx";
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            NextPage();
        }

        protected void btnSubmitPrev_Click(object sender, EventArgs e)
        {
            PrevPage();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (setupCompany.Agents == null)
            {
                setupCompany.Agents = new List<RegisterModel>();
            }
            RegisterModel agn = new RegisterModel()
            {
                UserName = UserName.Text,
                RoleName = "Agent",
                Email = Email.Text
            };

            setupCompany.Agents.Add(agn);
            BindData();
        }

        private void BindData()
        {
            dv.DataSource = setupCompany.Agents;
            dv.DataBind();
        }
    }
}