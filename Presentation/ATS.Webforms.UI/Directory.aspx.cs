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
    public partial class Directory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IEnumerable<Person> persons = TimesheetRepository.GetAllPersons();
                PersonGridView.DataSource = persons.ToList();
                PersonGridView.DataBind();
            }
        }
    }
}