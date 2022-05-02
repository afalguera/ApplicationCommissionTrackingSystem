using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class CardTypeDetails
    {
        public string CardBrandCode { get; set; }

        public string CardBrandName { get; set; }

        public string CardTypeCode { get; set; }

        public string CardTypeName { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public decimal CommissionRate { get; set; }

        public int ID { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }
    }
}