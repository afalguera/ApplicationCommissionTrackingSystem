using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class OutrightRejectedReport
    {


       
        public string ApplicationNo {get;set;}
        public string CardBrand {get;set;}
        public DateTime StatusDate {get;set;}
        public string ApplicantFirstName {get;set;}
        public string ApplicantLastName {get;set;}
        public string ApplicantMiddleName {get;set;}
        public DateTime DateOfBirth {get;set;}
        
        

        public bool Validate()
        {
            return true;
        }
    }
}
