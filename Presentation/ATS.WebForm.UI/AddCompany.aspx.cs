using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.WebForm.UI
{
    public partial class AddCompany : SetupCompanyBaseController
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            currentPage = "AddCompany.aspx";
            if (!IsPostBack)
            {
                if (setupCompany.Company != null)
                {
                    txtName.Text = setupCompany.Company.CompanyDescription;
                    txtAddress.Text = setupCompany.Company.Address;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            setupCompany.Company = 
                new Company() 
                { 
                    CompanyDescription = txtName.Text,
                    Address = txtAddress.Text
                };
            NextPage();
        }
    }
}