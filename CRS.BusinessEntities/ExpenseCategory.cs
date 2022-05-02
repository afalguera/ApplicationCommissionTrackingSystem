using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class ExpenseCategory : ICRSBase
    {
        public string ExpenseCategoryCode { get; set; }
        public string ExpenseCategoryName { get; set; }

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