using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.MVC.UI.Common
{
    public class UserSetting
    {
        #region Singleton

        public static UserSetting Current 
        {
            get
            {
                if (HttpContext.Current.Session["UserSetting"] == null) 
                { 
                    HttpContext.Current.Session["UserSetting"] = new UserSetting();
                }

                return (UserSetting)HttpContext.Current.Session["UserSetting"];
            }
            set
            {
                HttpContext.Current.Session["UserSetting"] = value;
            }
        }

        #endregion

        #region Public Properties

        public string[] Controllers { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }

        private SetupCompany setupCompany;
        public SetupCompany SetupCompany
        {
            get
            {
                if (setupCompany == null)
                {
                    setupCompany = new SetupCompany();
                }
                return (SetupCompany)setupCompany;
            }
            set
            {
                setupCompany = value;
            }
        }

        #endregion
    }
}