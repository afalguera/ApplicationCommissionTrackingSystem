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
    public class ExpenseCategoryDB
    {
        public enum ExpenseCategoryFilters
        {
            expenseCategoryCode,
            expenseCategoryName
        }

        public static bool CheckIfExists(string filter, ExpenseCategoryFilters expenseCategoryFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetExpenseCategoryByFilter", new[] { new SqlParameter(expenseCategoryFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<ExpenseCategory> GetList()
        {
            return DBSqlHelper.ExecuteGetList<ExpenseCategory>("spExpenseCategoryGetList", FillDataRecord, null);
        }

        public static bool Save(ExpenseCategory expenseCategory)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@expenseCategoryCode", expenseCategory.Code),
                new SqlParameter("@expenseCategoryName", expenseCategory.Name),
                new SqlParameter("@createdBy", expenseCategory.CreatedBy)
            }, "spExpenseCategorySave");
        }

        public static bool Update(ExpenseCategory expenseCategory)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@expenseCategoryId", expenseCategory.ID),
                new SqlParameter("@expenseCategoryCode", expenseCategory.Code),
                new SqlParameter("@expenseCategoryName", expenseCategory.Name),
                new SqlParameter("@modifiedBy", expenseCategory.ModifiedBy)
            }, "spExpenseCategoryUpdate");
        }

        public static bool Delete(int expenseCategoryId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", expenseCategoryId), new SqlParameter("@deletedBy", deletedBy) }, "spExpenseCategoryDelete");
        }

        private static ExpenseCategory FillDataRecord(IDataRecord dataRecord)
        {
            return new ExpenseCategory()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ExpenseCategoryId")),
                Code = dataRecord["ExpenseCategoryCode"] as string,
                Name = dataRecord["ExpenseCategoryName"] as string
            };
        }
    }
}