using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CRS.Bll;
using CRS.BusinessEntities;

namespace CRS.Models
{
    public class ProdRptOutrightRejectModel
    {

        
      
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }
        
       
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Select Report")]
        public string ReportName { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }


        [Display(Name = "Subreport")]
        public string CPASubReport { get; set; }

        [Display(Name = "Include Exceptions")]
        public Boolean IncludeExceptions { get; set; }
        
       
    }

  
}