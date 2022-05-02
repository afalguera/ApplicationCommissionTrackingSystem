using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRS.BusinessEntities
{
    public class CMSBannerAdsImage : ICRSBase
    {

        public string Code {get;set;}
       

        public string Name{get;set;}
       

        public DateTime DateModified {get;set;}
        public DateTime DateCreated { get; set; }
        
        public string ModifiedBy{get;set;}
       
        
        public string CreatedBy{get;set;}
        
        
        public int ID{get;set;}

        [Required]
        [Display(Name = "Target Url")]
        [RegularExpression(@"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$",
                          ErrorMessage = "Enter a valid URL eg: http://www.google.com")]
        public string TargetUrl {get;set;}
        
        [Required]
        [Display(Name="Image Name")]
        public string ImageName {get;set;}

        [Required]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set;}

        
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Channel")]
        public string Channel { get; set; }
        
        [Required(ErrorMessage="Please enter date from value")]
        [Display(Name = "Period From")]
        public DateTime PeriodFrom{ get; set; }

        [Required(ErrorMessage = "Please enter date to value")]
        [Display(Name = "Period To")]
        public DateTime PeriodTo { get; set; }

        public string RoleName { get; set; }
        public string PeriodToString { get; set; }
        public string PeriodFromString { get; set; }
        
        public bool Validate()
        {
            return true;
        }
    }

}
