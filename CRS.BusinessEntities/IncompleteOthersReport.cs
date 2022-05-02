using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class IncompleteOthersReport
    {


        
        public string Reason {get;set;}
        public int Total { get; set; }

        
        

        public bool Validate()
        {
            return true;
        }
    }
}
