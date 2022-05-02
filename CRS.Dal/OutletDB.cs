using CRS.BusinessEntities;
using CRS.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRS.Dal
{
    public class OutletDB
    {
        public static bool CheckIfExists(string outletCode)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetOutletByFilter", new[] { new SqlParameter("@outletCode", outletCode) }).Read();
        }

        public static bool Delete(int outletId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@outletId", outletId), new SqlParameter("@deletedBy", deletedBy) }, "spOutletDelete");
        }

        public static Outlet GetItem(int id)
        {
            return DBSqlHelper.ExecuteGet<Outlet>("spGetOutletByFilter", FillDataRecord, new[] { new SqlParameter("@id", id) });
        }

        public static Outlet GetItem(string outletCode)
        {
            return DBSqlHelper.ExecuteGet<Outlet>("spGetOutletByFilter", FillDataRecord, new[] { new SqlParameter("@outletCode", outletCode) });
        }

        public static IEnumerable<Outlet> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Outlet>("spGetOutletListByFilter", FillDataRecord, null);
        }

        public static bool Save(Outlet outlet)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@outletCode", outlet.Code),
                new SqlParameter("@outletName", outlet.Name),
                new SqlParameter("@branchId", outlet.BranchId),
                new SqlParameter("@createdBy", outlet.CreatedBy)
                
            }, "spOutletSave");
        }

        public static bool Update(Outlet outlet)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@outletId", outlet.ID),
                new SqlParameter("@outletCode", outlet.Code),
                new SqlParameter("@outletName", outlet.Name),
                new SqlParameter("@branchId", outlet.BranchId),
                new SqlParameter("@modifiedBy", outlet.ModifiedBy)
                
            }, "spOutletUpdate");
        }

        private static Outlet FillDataRecord(IDataRecord dataRecord)
        {
            return new Outlet()
            {
                ID = dataRecord["OutletId"].AsInt32(),
                Code = dataRecord["OutletCode"] as string,
                Name = dataRecord["OutletName"] as string,
                BranchId = dataRecord["BranchId"].AsInt32(),
                BranchName = dataRecord["BranchName"] as string
            };
        }

        public static bool IsOutletExists(string outletName)
		{
			bool isOutlet = false;
            SqlDataReader reader;
            string spName = "spOutletExisting";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            int outletCount = 0;
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@outletName", outletName)};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    outletCount = reader["OutletCount"].AsInt();
                }

                if (outletCount > 0)
                {
                    isOutlet = true;
                }
            }

            reader = null;

			return isOutlet;
		}

    }   
}