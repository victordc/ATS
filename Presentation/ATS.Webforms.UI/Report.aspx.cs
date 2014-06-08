using ATS.Data;
using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATS.MVC.UI.Common;

namespace ATS.Webforms.UI
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Store this user when called by MVC
                if (HttpContext.Current.Session["UserSetting"] == null)
                {
                    if (Request.QueryString["personid"] == null) //Session timeout?
                    {
                        Response.Redirect("http://localhost:1409/Home/Index");
                    }
                    else
                    {
                        HttpContext.Current.Session["UserSetting"] = new UserSetting();
                        UserSetting.Current.PersonId = int.Parse(Request.QueryString["personid"]);
                    }
                }
                //else
                //{
                //    if ((int.Parse(Request.QueryString["personid"]) != UserSetting.Current.PersonId)) //Hacker?
                //    {
                //        Response.Redirect("http://localhost:1409/Home/Index");
                //    }
                //}

                //Get leaves and credits from CodeTable
                IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
                LeaveCategoryGridView.DataSource = history.ToList();
                LeaveCategoryGridView.DataBind();
            }
        }

        protected void LeaveCategoryGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<LeaveCategory> history = TimesheetRepository.GetLeaveCategories();
            LeaveCategoryGridView.DataSource = history.OrderByDescending(x => DataBinder.Eval(x, e.SortExpression)).ToList();
            LeaveCategoryGridView.DataBind();
        }

    }
}