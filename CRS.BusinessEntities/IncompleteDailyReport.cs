using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class IncompleteDailyReport
    {


        
        public string Reason {get;set;}
        public string DisplayDay {get;set;}
        public string SortDay {get;set;}
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
