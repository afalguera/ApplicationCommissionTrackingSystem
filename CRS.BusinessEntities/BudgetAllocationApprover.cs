using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class BudgetAllocationApprover : ICRSBase
    {
        public string ApproverTitle  { get; set; }
        public string ApproverName { get; set; }
        public decimal ApproverAmountLower { get; set; }
        public decimal ApproverAmountUpper { get; set; }
        public string BudgetType { get; set; }
        public string Remarks { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string ModifiedBy { get; set; }

        public string CreatedBy { get; set; }

        public int ID { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}