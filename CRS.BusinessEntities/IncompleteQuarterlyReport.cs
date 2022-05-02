using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class IncompleteQuarterlyReport
    {


        
        public string Reason {get;set;}
        public string DateQuarterLong {get;set;}
        public string DateQuarter {get;set;}
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
