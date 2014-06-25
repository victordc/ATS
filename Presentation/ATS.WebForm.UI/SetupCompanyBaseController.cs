using ATS.BusinessFacade;
using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.WebForm.UI
{
    public class SetupCompanyBaseController : System.Web.UI.Page
    {
        // Navigator keeps the flow map for the reservation 
        // The key is the "currentpage.aspx" string the value is the "nextpage.aspx"
        private Dictionary<string, string> navigator;

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
            navigator.Add("SpecifyItinerary.aspx", "SelectFlight.aspx");
            navigator.Add("SelectFlight.aspx", "SpecifyPassenger.aspx");
            navigator.Add("SpecifyPassenger.aspx", "ConfirmBooking.aspx");
            navigator.Add("ConfirmBooking.aspx", "Default.aspx");

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

        //// Makes a call when a list of flights are required for the page
        //protected List<FlightSchedule> GetAvailableFlights(string origin, string destination, DateTime travelDate)
        //{
        //    return adminFacade.GetAvailableFlights(origin, destination, travelDate);
        //}

        //// Makes a call to save the reservation as a final step in the reservation process.
        //protected void ConfirmBooking()
        //{
        //    adminFacade.ConfirmBooking(); // This is a dummy call - you would need to pass attributes as reservation details as argument (not the view object).
        //    Response.Redirect(navigator[currentPage]);
        //}
    }
}