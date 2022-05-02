using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class BudgetAllocationApproverManager
    {
        public static IEnumerable<BudgetAllocationApprover> GetList()
        {
            return BudgetAllocationApproverDB.GetList();
        }

        public static bool BudgetAllocationApproverCodeExists(string budgetAllocationApproverCode)
        {
            return BudgetAllocationApproverDB.CheckIfExists(budgetAllocationApproverCode, BudgetAllocationApproverDB.BudgetAllocationApproverFilters.budgetAllocationApproverCode);
        }

        public static bool BudgetAllocationApproverTitleExists(string approverTitle)
        {
            return BudgetAllocationApproverDB.CheckIfExists(approverTitle, BudgetAllocationApproverDB.BudgetAllocationApproverFilters.approverTitle);
        }

        public static bool Save(BudgetAllocationApprover budgetAllocationApprover)
        {
            return BudgetAllocationApproverDB.Save(budgetAllocationApprover);
        }

        public static bool Update(BudgetAllocationApprover budgetAllocationApprover)
        {
            return BudgetAllocationApproverDB.Update(budgetAllocationApprover);
        }

        public static bool Delete(int budgetAllocationApproverId, string deletedBy)
        {
            return BudgetAllocationApproverDB.Delete(budgetAllocationApproverId, deletedBy);
        }
    }
}