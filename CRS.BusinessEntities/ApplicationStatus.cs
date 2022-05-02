using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    //public class ApplicationStatus : ICRSBase
    public class ApplicationStatus 
    {
        public string SourceCode { get; set; }
        public string ApplicationNo { get; set; }
        public string ApplicantLastName { get; set; }
        public string ApplicantFirstName { get; set; }
        public string ApplicantMiddleName { get; set; }
        public string CardBrand { get; set; }
        public string CardType { get; set; }
        public string DateEncoded { get; set; }
        public string DateStatus { get; set; }
        public string Status { get; set; }
        public int ApplicationCount { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonName { get; set; }
        public string Reasons { get; set; }
        public string ApplicantFullName { get; set; }
        public string Remarks { get; set; }
        //public string FullName
        //{
        //    get
        //    {
        //        return  GetLastName(this.ApplicantLastName)
        //            + GetFirstName(this.ApplicantFirstName) 
        //            + GetMiddleName(this.ApplicantMiddleName);
        //    }
        //}


        //public string ReferrorLastName { get; set; }
        //public string ReferrorFirstName { get; set; }
        //public string ReferrorMiddleName { get; set; }
        public string ReferrorCode { get; set; }
        public string ReferrorName { get; set; }
        public string AgingString { get; set; }
        public string ChannelName { get; set; }
        public string BranchName { get; set; }
            //get
            //{
            //    return GetLastName(this.ReferrorLastName)
            //        + GetFirstName(this.ReferrorFirstName) 
            //        + GetMiddleName(this.ReferrorMiddleName);
            //}
        //}

        //private string GetLastName(string lastName)
        //{
        //    string strLastName = string.IsNullOrEmpty(lastName) ? string.Empty : (UppercaseFirst(lastName) + ", ");
        //    return strLastName;
        //}

        //private string GetFirstName(string firstName)
        //{
        //    string strFirstName = string.IsNullOrEmpty(firstName) ? string.Empty : (UppercaseFirst(firstName) + " ");
        //    return strFirstName;
        //}

        //private string GetMiddleName(string middleName)
        //{
        //    string strMiddleName = string.IsNullOrEmpty(middleName) ? string.Empty : (UppercaseFirst(middleName) + " ");
        //    return strMiddleName;
        //}

        //private string UppercaseFirst(string s)
        //{
        //    char[] a = s.ToCharArray();
        //    a[0] = char.ToUpper(a[0]);
        //    return new string(a);
        //}           
        
    }
}
