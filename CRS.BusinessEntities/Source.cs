using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    public class Source : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }

        public bool IsForCommission { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}