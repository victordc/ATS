using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace ATS.Webforms.UI
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
                    "Default",
                    "",
                    "~/Report.aspx"
            );
        }
    }
}
