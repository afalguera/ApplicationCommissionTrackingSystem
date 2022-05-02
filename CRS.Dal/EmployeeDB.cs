using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRS.Dal
{
    public class EmployeeDB
    {
        public static bool Delete(int employeeId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD("spEmployeeDelete", new Dictionary<string, object>() {{ "@employeeId", employeeId }, { "@deletedBy", deletedBy }});
        }

        public static bool CheckIfExists(string employeeNumber)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetEmployeeByFilter", new[] { new SqlParameter("@employeeNumber", employeeNumber) }).Read();
        }

        public static Employee GetItem(int id)
        {
            return DBSqlHelper.ExecuteGet<Employee>("spGetEmployeeByFilter", new Dictionary<string, object>() { { "id", id } }, FillDataRecord);
        }

        public static Employee GetItem(string employeeNumber)
        {
            return DBSqlHelper.ExecuteGet<Employee>("spGetEmployeeByFilter", new Dictionary<string, object>() { { "employeeNumber", employeeNumber } }, FillDataRecord);
        }

        public static IEnumerable<Employee> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Employee>("spGetEmployeeList", null, FillDataRecord);
        }

        public static IEnumerable<Employee> GetList(string employeeNumber)
        {
            return DBSqlHelper.ExecuteGetList<Employee>("spGetEmployeeListByFilter", new Dictionary<string, object> { { "employeeNumber", employeeNumber } }, FillDataRecord);
        }
        
        public static bool Save(Employee employee)
        {
            return DBSqlHelper.ExecuteCUD("spEmployeeSave", new Dictionary<string, object>()
            {
                { "@branchCode", (object) employee.BranchCode  ?? DBNull.Value},
                { "@employeeNumber", (object) employee.EmployeeNumber ?? DBNull.Value },
                { "@lastName", (object) employee.LastName ?? DBNull.Value },
                { "@firstName", (object) employee.FirstName ?? DBNull.Value},
                { "@middleName", (object) employee.MiddleName ?? DBNull.Value },
                { "@createdBy", employee.CreatedBy }
            });
        }

        public static bool Update(Employee employee)
        {
            return DBSqlHelper.ExecuteCUD("spEmployeeUpdate", new Dictionary<string, object>()
            {
                { "@id", employee.ID },
                { "@branchCode", (object) employee.BranchCode  ?? DBNull.Value},
                { "@employeeNumber", (object) employee.EmployeeNumber ?? DBNull.Value },
                { "@lastName", (object) employee.LastName ?? DBNull.Value},
                { "@firstName", (object) employee.FirstName ?? DBNull.Value },
                { "@middleName", (object) employee.MiddleName ?? DBNull.Value },
                { "@modifiedBy", employee.ModifiedBy }
            });
        }

        private static Employee FillDataRecord(IDataReader dataRecord)
        {
            Employee employee = new Employee();
            
            int ordValue = -1;
            employee.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("EmployeeId"));
            employee.BranchId = dataRecord.GetInt32(dataRecord.GetOrdinal("BranchId"));

            ordValue = dataRecord.GetOrdinal("BranchName");
            employee.BranchName = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("EmployeeNumber");
            employee.EmployeeNumber = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("Name");
            employee.Name = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("LastName");
            employee.LastName = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("FirstName");
            employee.FirstName = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("MiddleName");
            employee.MiddleName = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            //ordValue = dataRecord.GetOrdinal("CreatedBy");
            //employee.CreatedBy = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            //ordValue = dataRecord.GetOrdinal("CreatedDate");
            //employee.DateCreated = dataRecord.IsDBNull(ordValue) ? DateTime.MinValue : dataRecord.GetDateTime(ordValue);

            //ordValue = dataRecord.GetOrdinal("ModifiedBy");
            //employee.ModifiedBy = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            //ordValue = dataRecord.GetOrdinal("ModifiedDate");
            //employee.DateModified = dataRecord.IsDBNull(ordValue) ? DateTime.MinValue : dataRecord.GetDateTime(ordValue);

            ordValue = dataRecord.GetOrdinal("ChannelCode");
            employee.ChannelCode = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("RegionCode");
            employee.RegionCode = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("DistrictCode");
            employee.DistrictCode = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            ordValue = dataRecord.GetOrdinal("BranchCode");
            employee.BranchCode = dataRecord.IsDBNull(ordValue) ? string.Empty : dataRecord.GetString(ordValue);

            return employee;
        }
    }
}