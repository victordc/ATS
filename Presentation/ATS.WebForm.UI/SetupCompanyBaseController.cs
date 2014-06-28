using ATS.BusinessFacade;
using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
//using WebMatrix.WebData;

namespace ATS.WebForm.UI
{
    public class SetupCompanyBaseController : System.Web.UI.Page
    {
        // Navigator keeps the flow map for the setup company 
        // The key is the "currentpage.aspx" string the value is the "nextpage.aspx"
        private Dictionary<string, string> navigator;
        private Dictionary<string, string> revertNavigator;

        // The current page 'id' would be set in this variable by the deriving page
        protected string currentPage;

        protected enum UserRole
        { 
            Supervisor,
            Agent,
            Staff
        }

        // The reservation facade is a Business Interface providing services for accessing BLL functionality 
        protected IAdminFacade adminFacade;

        // The reservation view object is a strongly typed session state object structure 
        // for storing reservation information that is shared by all pages participating in this flow
        protected SetupCompany setupCompany;

        // To be called for intialisation by the deriving class Page_Load
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            // This could be improved by keeping navigator as a singleton
            // This could be improved by using enumerations instead of string
            // Shown here for clarity of purpose for page controller pattern
            navigator = new Dictionary<string, string>();
            navigator.Add("AddCompany.aspx", "AddSupervisors.aspx");
            navigator.Add("AddSupervisors.aspx", "AddAgents.aspx");
            navigator.Add("AddAgents.aspx", "AddStaffs.aspx");
            navigator.Add("AddStaffs.aspx", "Default.aspx");

            revertNavigator = new Dictionary<string, string>();
            revertNavigator.Add("AddSupervisors.aspx", "AddCompany.aspx");
            revertNavigator.Add("AddAgents.aspx", "AddSupervisors.aspx");
            revertNavigator.Add("AddStaffs.aspx", "AddAgents.aspx");

            adminFacade = new AdminFacade();

            if (Session["SetupCompany"] == null)
            {
                setupCompany = new SetupCompany();
                Session["SetupCompany"] = setupCompany;
            }
            else
                setupCompany = (SetupCompany)Session["SetupCompany"];
        }

        // Navigator to be used to move to next step (view) in the process
        protected void NextPage()
        {
            Response.Redirect(navigator[currentPage]);
        }

        protected void PrevPage()
        {
            Response.Redirect(revertNavigator[currentPage]);
        }

        // Set up company.
        protected void ConfirmSetupPage()
        {
            try
            {
                // sql ce cannot support multiple connection string.
                //using (TransactionScope scope = new TransactionScope())
                //{
                    //1. Create user account for supervisor/agent/staff
                    for (int i = 0; i < setupCompany.Supervisors.Count; i++)
                    {
                        CreateUserAccount(setupCompany.Supervisors[i]);
                    }
                    for (int i = 0; i < setupCompany.Agents.Count; i++)
                    {
                        CreateUserAccount(setupCompany.Agents[i]);
                    }
                    for (int i = 0; i < setupCompany.Staffs.Count; i++)
                    {
                        CreateUserAccount(setupCompany.Staffs[i]);
                    }

                    //2. Setup company, insert staff/agent/supervior to database
                    adminFacade.SetupCompany(setupCompany);

                    //scope.Complete();
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
            
            Response.Redirect(navigator[currentPage]);
        }

        private int CreateUserAccount(RegisterModel model)
        {
            WebSecurity.CreateUserAndAccount(model.UserName, "p@ssword1");
            Roles.AddUserToRole(model.UserName, model.RoleName);
            return 1;
        }

        protected bool DoesUserNameExist(string userName, UserRole role)
        {
            var user = Membership.GetUser(userName);
            if (user != null)
                return true;

            switch (role)
            {
                case UserRole.Supervisor:
                    if (setupCompany.Supervisors != null)
                    {
                        var query = setupCompany.Supervisors.Where(r => r.UserName == userName).FirstOrDefault();
                        if (query != null)
                            return true;
                    }
                    break;
                case UserRole.Agent:
                    if (setupCompany.Agents != null)
                    {
                        var query = setupCompany.Agents.Where(r => r.UserName == userName).FirstOrDefault();
                        if (query != null)
                            return true;
                    }
                    break;
                case UserRole.Staff:
                    if (setupCompany.Staffs != null)
                    {
                        var query = setupCompany.Staffs.Where(r => r.UserName == userName).FirstOrDefault();
                        if (query != null)
                            return true;
                    }
                    break;
                    break;
                default:
                    break;
            }
            

            return false;
        }
    }
}