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
    public class CMSBannerAdsImageModel
    {
        
        [Required]
        [Display(Name = "Name")]
        public string TitleName{get;set;}

        [Required]
        [Display(Name = "Target Url")]
        public string TargetUrl{get;set;}

        [Required]
        [Display(Name = "Role")]
        public int RoleId{get;set;}

        [Required]
        [Display(Name = "Period From")]
        public DateTime PeriodFrom{get;set;}
    
        [Required]
        [Display(Name = "Period To")]
        public DateTime PeriodTo { get; set; }

        
        
        public bool IsValid()
        {
            return true;

        }
    }

  
}