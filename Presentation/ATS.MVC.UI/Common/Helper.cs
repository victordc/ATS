using ATS.Data;
using ATS.Data.Model;
using ATS.MVC.UI.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace ATS.MVC.UI.Common
{
    [InitializeSimpleMembership]
    public class Helper
    {
        /// <summary>
        /// Get list of role by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsAccessible(HttpContextBase httpContext)
        {
            bool result = false;
            IPrincipal user = httpContext.User;
            string controlName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();

            if (user.Identity.IsAuthenticated)
            {
                var name = user.Identity.Name;

                var roles = (SimpleRoleProvider)Roles.Provider;
                var userRoles = roles.GetRolesForUser(name);


                for (int i = 0; i < userRoles.Count(); i++)
                {
                    IEnumerable<ObjectAccess> oAccessesByRole = TimesheetRepository.Instance.GetObjectAccessByRole(userRoles[i]);
                    bool isAccessToAction = Convert.ToBoolean(ConfigurationManager.AppSettings["IsCheckAccessToAction"]);

                    List<ObjectAccess> objAccessFilter = null;
                    if (isAccessToAction)
                    {
                        objAccessFilter = oAccessesByRole.Where(x => x.Controller == controlName && x.Action == actionName).ToList();
                    }
                    else
                    {
                        objAccessFilter = oAccessesByRole.Where(x => x.Controller == controlName).ToList();
                    }

                    if (objAccessFilter.Count > 0)
                    {
                        result = true;
                        break;
                    }
                }

            }

            return result;
        }

        
        public static int CurrentUserId 
        {
            get
            {
                if (WebSecurity.IsAuthenticated)
                {
                    return WebSecurity.CurrentUserId;
                }
                return 0;
            }
        }

        public static string CurrentUserName
        {
            get
            {
                if (WebSecurity.IsAuthenticated)
                {
                    return WebSecurity.CurrentUserName;
                }
                return string.Empty;
            }
        
        }
    }
}