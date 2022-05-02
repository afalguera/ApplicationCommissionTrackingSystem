using System;

namespace CRS.BusinessEntities
{
    public class Channel : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }
        
        public string Name { get; set; }

        public string PayeeName { get; set; }

        public string PayeeTin { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BankBranch { get; set; }

        public string ChannelRequestor { get; set; }

        public int ChannelRequestorId { get; set; }

        public string ChannelChecker { get; set; }

        public int ChannelCheckerId { get; set; }

        public string ChannelNoter { get; set; }

        public int ChannelNoterId { get; set; }

        public string SalesManager { get; set; }

        public int SalesManagerId { get; set; }

        public bool IsYGC { get; set; }

        public bool IsGross { get; set; }

        public bool IsVatable { get; set; }

        public bool IsEAPR { get; set; }

        public bool IsRCBC { get; set; }

        public bool IsMyOrange { get; set; }

        public string EAPRDescription { get; set; }

        public DateTime DateModified { get; set; }
      
        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        
        public bool Validate()
        {
            return true;
        }
    }
}
