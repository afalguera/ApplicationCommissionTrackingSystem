using System;

namespace CRS.BusinessEntities
{
    public class Branch : ICRSBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int ChannelId { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public int DistrictId { get; set; }

        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public string BranchName { get; set; }

        public string TIN { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }

        public string BankBranch { get; set; }

        public bool IsYGC { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime DateModified { get; set; }

        public string RegionCode { get; set; }

        public string RegionName { get; set; }

        public int EmployeeId { get; set; }

        public string ManagerName { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}