using CRS.BusinessEntities;
using CRS.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRS.Dal
{
    public class ManagerDB
    {
        public static bool Delete(int managerId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@managerId", managerId), new SqlParameter("@deletedBy", deletedBy) }, "spManagerDelete");
        }

        public static Manager GetItem(int id)
        {
            return DBSqlHelper.ExecuteGet<Manager>("spGetManagerByFilter", FillDataRecord, new[] { new SqlParameter("@id", id) });
        }

        public static IEnumerable<Manager> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Manager>("spGetManagerListByFilter", FillDataRecord, null);
        }

        public static IEnumerable<object> GetManagerTypeList()
        {
            return DBSqlHelper.ExecuteGetList<object>("spGetManagerTypeList", 
                (dataReader) => 
                {
                    return new 
                    {
                        ManagerTypeId = dataReader["ManagerTypeId"].AsInt32(),
                        Description = dataReader["Description"] as string
                    };
                }, null);
        }

        public static bool Save(Manager manager)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@branchId", manager.BranchId),
                //new SqlParameter("@outletId", manager.OutletId),
                new SqlParameter("@employeeId", manager.EmployeeId),
                new SqlParameter("@managerTypeId", manager.ManagerTypeId),
                new SqlParameter("@createdBy", manager.CreatedBy)
                
            }, "spManagerSave");
        }

        public static bool Update(Manager manager)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@managerId", manager.ID),
                new SqlParameter("@branchId", manager.BranchId),
                //new SqlParameter("@outletId", manager.OutletId),
                new SqlParameter("@employeeId", manager.EmployeeId),
                new SqlParameter("@managerTypeId", manager.ManagerTypeId),
                new SqlParameter("@modifiedBy", manager.ModifiedBy)
                
            }, "spManagerUpdate");
        }

        private static Manager FillDataRecord(IDataRecord dataRecord)
        {
            return new Manager()
            {
                ID = dataRecord["ManagerId"].AsInt32(),
                BranchId = dataRecord["BranchId"].AsInt32(),
                BranchName = dataRecord["BranchName"] as string,
                //OutletId = dataRecord["OutletId"].AsInt32(),
                //OutletName = dataRecord["OutletName"] as string,
                Name = dataRecord["EmployeeName"] as string,
                EmployeeId = dataRecord["EmployeeId"].AsInt32(),
                ManagerTypeId = dataRecord["ManagerTypeId"].AsInt32(),
                Description = dataRecord["Description"] as string
            };
        }

        //#region Get Outlets
        //public static IEnumerable<Outlet> GetOutletList(int branchId)
        //{
        //    string spName = "spGetOutletListByBranchId";
        //    DBSqlHelper sqlHelper = new DBSqlHelper();
        //    SqlDataReader reader;
        //    List<Outlet> list = new List<Outlet>();
        //    SqlParameter[] sqlParams = new SqlParameter[] {
        //                    new SqlParameter("@branchId", branchId) };
        //    reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            Outlet dto = new Outlet();
        //            dto.ID = reader["OutletId"].AsInt();
        //            dto.Name = reader["OutletName"].ToString();
        //            list.Add(dto);
        //        }
        //    }
        //    return list.AsEnumerable();
        //}
        //#endregion
    }
}