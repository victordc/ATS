using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ATS.Data.Model
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        [Remote("DoesUserNameExist", "Admin", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Select Role")]
        public string RoleName { get; set; }

        [Display(Name = "Select Supervisor")]
        public string SupervisorName { get; set; }
        [Display(Name = "Select Agent")]
        public string AgentName { get; set; }


    }
}
