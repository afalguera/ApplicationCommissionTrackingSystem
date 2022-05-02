using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CRS.Bll;
using CRS.BusinessEntities;

namespace CRS.Models
{
    public class RedemptionReportModel
    {
       
        

      
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }
        
       
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        public string LastTab { get; set; }

       
       
       
    }

  
}