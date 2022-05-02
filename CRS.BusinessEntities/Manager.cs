using System;

namespace CRS.BusinessEntities
{
    public class Manager : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

        public int OutletId { get; set; }

        public string OutletName { get; set; }

        public int EmployeeId { get; set; }

        public int ManagerTypeId { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime DateModified { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}