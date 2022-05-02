using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRS.BusinessEntities
{
    public class RedemptionItems : ICRSBase
    {

        public string Code {get;set;}
       
        public string Name{get;set;}

        [Required]
        [Display(Name="Image Path")]
        [DataType(DataType.ImageUrl,ErrorMessage="Please enter a valid image URL")]
        public string ImagePath { get; set; }


        [Required]
        [Display(Name = "Period From")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateTime PeriodFrom { get; set; }

        [Required]
        [Display(Name = "Period To")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date")]
        public DateTime PeriodTo { get; set; }

        [Required]
        //[Range(0, 10000, ErrorMessage = "Can only be between 0 to 10000")]
        public decimal PointsRequired { get; set; }
        
        public string PointsRequiredString { get; set; }
        
        public DateTime DateModified {get;set;}
        
        public DateTime DateCreated { get; set; }
        
        public string ModifiedBy{get;set;}
       
        public string CreatedBy{get;set;}
        
        public int ID{get;set;}

        public string PeriodFromString { get; set; }
        public string PeriodToString { get; set; }

        public bool Validate()
        {
            return true;
        }
    }

}
