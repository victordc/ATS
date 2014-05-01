using ATS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATS.MVC.UI.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected override void OnException(ExceptionContext filterContext)
        {
            
            //LogManager.LogInfo("Khiem: test log 4 net");
            LogManager.LogError(filterContext.Exception.ToString());
            base.OnException(filterContext);
        }

    }
}
