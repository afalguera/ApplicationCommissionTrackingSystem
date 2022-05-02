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
    public class ForgotPasswordModel
    {

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(30)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        

        public bool IsValid()
        {
            return true;
        }

       
    }

  
}