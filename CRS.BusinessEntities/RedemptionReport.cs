using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class RedemptionReport
    {


       
        public string Item {get;set;}
        public string LastName {get;set;}
        public string FirstName {get;set;}
        public string MiddleName {get;set;}
        public string Email {get;set;}
        public DateTime RedemptionDate {get;set;}
        
        

        public bool Validate()
        {
            return true;
        }
    }
}
