using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CRS.BusinessEntities;
using System.Data.Common;
using CRS.Helper;


namespace CRS.Dal
{
    public class BudgetAllocationApproverDB
    {
        public enum BudgetAllocationApproverFilters
        {
            budgetAllocationApproverCode,
            approverTitle
        }

        public static bool CheckIfExists(string filter, BudgetAllocationApproverFilters budgetAllocationApproverFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetBudgetAllocationApproverByFilter", new[] { new SqlParameter(budgetAllocationApproverFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<BudgetAllocationApprover> GetList()
        {
            return DBSqlHelper.ExecuteGetList<BudgetAllocationApprover>("spBudgetAllocationApproverGetList", FillDataRecord, null);
        }

        public static bool Save(BudgetAllocationApprover budgetAllocationApprover)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@budgetAllocationApproverCode", budgetAllocationApprover.Code),
                new SqlParameter("@approverTitle", budgetAllocationApprover.ApproverTitle),
                new SqlParameter("@approverName", budgetAllocationApprover.ApproverName),
                new SqlParameter("@approverAmountLower", budgetAllocationApprover.ApproverAmountLower),
                new SqlParameter("@approverAmountUpper", (object) budgetAllocationApprover.ApproverAmountUpper ?? DBNull.Value),
                new SqlParameter("@remarks", (object) budgetAllocationApprover.Remarks ?? DBNull.Value),
                new SqlParameter("@createdBy", budgetAllocationApprover.CreatedBy)
            }, "spBudgetAllocationApproverSave");
        }

        public static bool Update(BudgetAllocationApprover budgetAllocationApprover)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@budgetAllocationApproverId", budgetAllocationApprover.ID),
                new SqlParameter("@budgetAllocationApproverCode", budgetAllocationApprover.Code),
                new SqlParameter("@approverTitle", budgetAllocationApprover.ApproverTitle),
                new SqlParameter("@approverName", budgetAllocationApprover.ApproverName),
                new SqlParameter("@approverAmountLower", budgetAllocationApprover.ApproverAmountLower),
                new SqlParameter("@approverAmountUpper", (object) budgetAllocationApprover.ApproverAmountUpper ?? DBNull.Value),
                new SqlParameter("@remarks", (object) budgetAllocationApprover.Remarks ?? DBNull.Value),
                new SqlParameter("@modifiedBy", budgetAllocationApprover.ModifiedBy)
            }, "spBudgetAllocationApproverUpdate");
        }

        public static bool Delete(int budgetAllocationApproverId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", budgetAllocationApproverId), new SqlParameter("@deletedBy", deletedBy) }, "spBudgetAllocationApproverDelete");
        }

        private static BudgetAllocationApprover FillDataRecord(IDataRecord dataRecord)
        {
            return new BudgetAllocationApprover()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("BudgetAllocationApproverId")),
                Code = dataRecord["BudgetAllocationApproverCode"] as string,
                ApproverTitle = dataRecord["ApproverTitle"] as string,
                ApproverName = dataRecord["ApproverName"] as string,
                ApproverAmountLower = dataRecord["ApproverAmountLower"].AsDecimal(),
                ApproverAmountUpper = dataRecord["ApproverAmountUpper"].AsDecimal(),
                Remarks = dataRecord["Remarks"] as string
            };
        }
    }
}