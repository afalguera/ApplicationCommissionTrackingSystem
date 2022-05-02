using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace CRS.BusinessEntities
{
    public class CardType : ICRSBase
    {
        public string CardTypeCode { get; set; }
        public string CardTypeName { get; set; }
        public string CardBrandCode { get; set; }

        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string CardBrandName { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }

        public string CardSubTypeCode { get; set; }

        public string CardSubTypeName { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
