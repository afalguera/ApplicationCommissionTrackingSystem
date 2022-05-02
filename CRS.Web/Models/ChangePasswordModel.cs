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
    public class ChangePasswordModel
    {


        [Display(Name = "New Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5)]
        public string NewPassword { get; set; }


        [Display(Name = "Verify Password")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 5)]
        [Compare("NewPassword")]
        public string NewPasswordConfirm { get; set; }

        public bool IsValid()
        {
            return true;
        }

       
    }

  
}