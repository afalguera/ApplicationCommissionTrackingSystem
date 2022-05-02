using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class ExpenseCategoryManager
    {
        public static IEnumerable<ExpenseCategory> GetList()
        {
            return ExpenseCategoryDB.GetList();
        }

        public static bool ExpenseCategoryCodeExists(string expenseCategoryCode)
        {
            return ExpenseCategoryDB.CheckIfExists(expenseCategoryCode, ExpenseCategoryDB.ExpenseCategoryFilters.expenseCategoryCode);
        }

        public static bool ExpenseCategoryNameExists(string expenseCategoryName)
        {
            return ExpenseCategoryDB.CheckIfExists(expenseCategoryName, ExpenseCategoryDB.ExpenseCategoryFilters.expenseCategoryName);
        }

        public static bool Save(ExpenseCategory expenseCategory)
        {
            return ExpenseCategoryDB.Save(expenseCategory);
        }

        public static bool Update(ExpenseCategory expenseCategory)
        {
            return ExpenseCategoryDB.Update(expenseCategory);
        }

        public static bool Delete(int expenseCategoryId, string deletedBy)
        {
            return ExpenseCategoryDB.Delete(expenseCategoryId, deletedBy);
        }
    }
}