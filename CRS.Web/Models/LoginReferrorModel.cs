using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;

namespace CRS.Models
{
    public class LoginReferrorModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [Display(Name = "What is")]
        public string Captcha { get; set; }


        public bool IsValid(string username, string password)
        {
            User user = UserManager.GetItem(username, password);
            
            if (user != null)
            {
                SessionWrapper.CurrentUser = user;
                return true;
            }
            else
            {
                
                return false;
            }

        }

        public bool IsValidCaptcha(string captcha, string sessioncaptcha)
        {
            if (sessioncaptcha == null || ! sessioncaptcha.Equals(captcha))
            {
                return false;
            }
            else {
                return true;
            }
        }
    }

  
}