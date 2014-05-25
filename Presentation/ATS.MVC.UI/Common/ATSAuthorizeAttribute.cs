using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.MVC.UI.Common
{
    public class ATSAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");
            if (httpContext.User.Identity.IsAuthenticated == false) return false;
            try
            {
                //string controllName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                return Helper.IsAccessible(httpContext);

            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Account/UnauthorizedAccess");
        }
    }
}