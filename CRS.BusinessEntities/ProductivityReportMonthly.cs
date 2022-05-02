using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class ProductivityReportMonthly
    {


        
        public string SourceName {get;set;}
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
