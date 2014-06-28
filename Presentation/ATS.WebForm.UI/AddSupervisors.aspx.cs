using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.WebForm.UI
{
    public partial class AddSupervisors : SetupCompanyBaseController
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            currentPage = "AddSupervisors.aspx";
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
            if (IsValid)
            {
                if (setupCompany.Supervisors == null)
                {
                    setupCompany.Supervisors = new List<RegisterModel>();
                }
                RegisterModel spv = new RegisterModel()
                                        {
                                            UserName = UserName.Text,
                                            RoleName = "Supervisor",
                                            Email = Email.Text
                                        };

                setupCompany.Supervisors.Add(spv);
                BindData();
            }
        }

        protected void ServerValidation(object source, ServerValidateEventArgs args)
        {
            try
            {
                // Test whether the value entered into the text box is even.
                args.IsValid = !DoesUserNameExist(args.Value, UserRole.Supervisor);
            }

            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }

        private void BindData()
        {
            dv.DataSource = setupCompany.Supervisors;
            dv.DataBind();
        }
    }
}