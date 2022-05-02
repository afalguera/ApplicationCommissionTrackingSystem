using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.Bll
{
    public class EmployeeManager
    {
        public static bool AccountNumberExists(string accountNumber)
        {
            return EmployeeDB.CheckIfExists(accountNumber);
        }

        public static Employee GetItem(int id)
        {
            return EmployeeDB.GetItem(id);
        }

        public static Employee GetItem(string employeeNumber)
        {
            return EmployeeDB.GetItem(employeeNumber);
        }

        public static IEnumerable<Employee> GetList()
        {
            return EmployeeDB.GetList();
        }

        public static bool Save(Employee employee)
        {
            return EmployeeDB.Save(employee);
        }

        public static bool Update(Employee employee)
        {
            return EmployeeDB.Update(employee);
        }

        public static bool Delete(int employeeId, string deletedBy)
        {
            return EmployeeDB.Delete(employeeId, deletedBy);
        }
    }
}
