using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CRS.Bll;
using CRS.BusinessEntities;

namespace CRS.Models
{
    public class ProdRptIncompleteModel
    {

        public string LastTab { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

      
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }
        
       
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }

       
        [Display(Name = "Date Filter")]
        public string ReportType { get; set; }

        [Display(Name = "Other Date Filter")]
        public string DateFilterMisc { get; set; }

        [Display(Name = "Show Chart")]
        public bool ShowChart { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Week")]
        public string Week { get; set; }

        [Display(Name = "Select Report")]
        public string ReportName { get; set; }
       
    }

  
}