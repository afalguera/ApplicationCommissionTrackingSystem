using System;

namespace CRS.BusinessEntities
{
    public class Employee : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public string EmployeeNumber { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime DateModified { get; set; }

        public string ChannelCode { get; set; }

        public string RegionCode { get; set; }

        public string DistrictCode { get; set; }

        public string BranchCode { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}