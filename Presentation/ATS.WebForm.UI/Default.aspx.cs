using ATS.BusinessFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATS.WebForm.UI
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                BindData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/AddCompany.aspx"));
        }

        protected void BindData()
        {
            IAdminFacade adminFacade = new AdminFacade();
            // Get all users
            var users = adminFacade.GetAllUsers();
            foreach (var item in users)
            {
                item.RoleName = Roles.GetRolesForUser(item.UserName).FirstOrDefault();
            }
            var query = from ur in users
                        select new ATS.Data.Model.UserProfile
                        {
                            UserId = ur.UserId,
                            UserName = ur.UserName,
                            RoleName = Roles.GetRolesForUser(ur.UserName).FirstOrDefault()
                        };
            dv.DataSource = query.ToList();//.OrderBy(r => r.UserId);
            dv.DataBind();
        }
    }
}