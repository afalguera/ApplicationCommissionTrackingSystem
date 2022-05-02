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
    public class LoginCardholderModel
    {
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }


        public bool IsValid(LoginCardholderModel model)
        {
            CardApplicantCriteria criteria = new CardApplicantCriteria();
            criteria.FirstName = model.FirstName;
            criteria.LastName = model.LastName;
            criteria.MiddleName = model.MiddleName;
            criteria.DateOfBirth = model.DateOfBirth;
            CardApplicationCollection cardApplicationColl = (CardApplicationCollection)CardApplicationManager.GetList(criteria)  ;
                
            if (cardApplicationColl.Count >= 1)
            {
                SessionWrapper.CurrentCardHolder = cardApplicationColl[0];
                return true;
            }
            else
            {
                return false;
            }

        }
    }

  
}