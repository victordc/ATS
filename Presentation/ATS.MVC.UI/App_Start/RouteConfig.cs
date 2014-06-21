using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATS.MVC.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CalendarRoute",
                url: "{controller}/{action}/{year}/{month}",
                defaults: new { controller = "Calendar", action = "Index", year = "", month = "" },
                constraints: new { month = @"^(0?[1-9]|1[012])$" }
            );

            //Custom route for reports
            routes.MapPageRoute(
             "ReportRoute",                         // Route name
             "Reports/{reportname}",                // URL
             "~/Webforms/{reportname}.aspx"   // File
             );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}