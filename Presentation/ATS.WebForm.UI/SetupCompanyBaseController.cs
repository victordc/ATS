using ATS.BusinessFacade;
using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
//using WebMatrix.WebData;

namespace ATS.WebForm.UI
{
    public class SetupCompanyBaseController : System.Web.UI.Page
    {
        // Navigator keeps the flow map for the reservation 
        // The key is the "currentpage.aspx" string the value is the "nextpage.aspx"
        private Dictionary<string, string> navigator;
        private Dictionary<string, string> revertNavigator;

        // The current page 'id' would be set in this variable by the deriving page
        protected string currentPage;

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


            //navigator.Add("SpecifyItinerary.aspx", "SelectFlight.aspx");
            //navigator.Add("SelectFlight.aspx", "SpecifyPassenger.aspx");
            //navigator.Add("SpecifyPassenger.aspx", "ConfirmBooking.aspx");
            //navigator.Add("ConfirmBooking.aspx", "Default.aspx");

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

            adminFacade.SetupCompany(setupCompany); 
            
            Response.Redirect(navigator[currentPage]);
        }

        //[InitializeSimpleMembership]
        private int CreateUserAccount(RegisterModel model)
        {
            WebSecurity.CreateUserAndAccount(model.UserName, "p@ssword");
            //Membership.CreateUser(model.UserName, "p@ssword1");
            Roles.AddUserToRole(model.UserName, model.RoleName);
            return 1;
        }
    }
}