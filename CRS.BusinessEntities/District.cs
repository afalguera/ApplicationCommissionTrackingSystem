using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class District : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string RegionCode { get; set; }

        public string RegionName { get; set; }

        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }

        public string DistrictTIN { get; set; }
        public string DistrictAccountName { get; set; }
        public string DistrictAccountNumber { get; set; }
        public string DistrictBankBranch { get; set; }

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