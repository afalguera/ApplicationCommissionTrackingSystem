using System;

namespace CRS.BusinessEntities
{
    public class Region : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }
        

        public bool Validate()
        {
            return true;
        }
    }
}