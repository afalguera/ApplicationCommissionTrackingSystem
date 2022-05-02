using System;

namespace CRS.BusinessEntities
{
    public class Outlet : ICRSBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; }

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