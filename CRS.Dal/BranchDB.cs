using CRS.BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRS.Helper;
using System;

namespace CRS.Dal
{
    public class BranchDB
    {
        public static object GetCascadingLocation(int branchId)
        {
            using (SqlDataReader dbReader = new DBSqlHelper().ExecuteReaderSPDB("spGetCascadingLocation", new[] { new SqlParameter("@branchId", branchId) }))
            {
                while (dbReader.Read())
                {
                    return new
                    {
                        BranchId = dbReader.GetInt32(dbReader.GetOrdinal("BranchId")),
                        BranchCode = dbReader["BranchCode"] as string,
                        DistrictId = dbReader.GetInt32(dbReader.GetOrdinal("DistrictId")),
                        DistrictCode = dbReader["DistrictCode"] as string,
                        ChannelId = dbReader.GetInt32(dbReader.GetOrdinal("ChannelId")),
                        ChannelCode = dbReader["ChannelCode"] as string,
                        //AreaId = dbReader.GetInt32(dbReader.GetOrdinal("AreaId")),
                        //AreaCode = dbReader["AreaCode"] as string,
                        RegionId = dbReader.GetInt32(dbReader.GetOrdinal("RegionId")),
                        RegionCode = dbReader["RegionCode"] as string
                    };
                }

                return null;
            }
        }

        public static bool CheckIfExists(string branchCode)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetBranchByFilter", new[] { new SqlParameter("@branchCode", branchCode) }).Read();
        }

        public static bool Delete(int branchId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@branchId", branchId), new SqlParameter("@deletedBy", deletedBy) }, "spBranchDelete");
        }

        public static Branch GetItem(int id)
        {
            return DBSqlHelper.ExecuteGet<Branch>("spGetBranchByFilter", FillDataRecord, new[] { new SqlParameter("@id", id) });
        }

        public static Branch GetItem(string branchCode)
        {
            return DBSqlHelper.ExecuteGet<Branch>("spGetBranchByFilter", FillDataRecord, new[] { new SqlParameter("@branchCode", branchCode) });
        }

        public static IEnumerable<Branch> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Branch>("spGetBranchListByFilter", FillDataRecord, null);
        }

        public static bool Save(Branch branch)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@branchCode", (object) branch.Code ?? DBNull.Value),
                new SqlParameter("@branchName", (object) branch.Name ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) branch.ChannelCode ?? DBNull.Value),
                new SqlParameter("@districtCode", (object) branch.DistrictCode ?? DBNull.Value),
                new SqlParameter("@tin", (object) branch.TIN ?? DBNull.Value),
                new SqlParameter("@accountName", (object) branch.AccountName ?? DBNull.Value),
                new SqlParameter("@accountNumber", (object) branch.AccountNumber ?? DBNull.Value),
                new SqlParameter("@bankBranch", (object) branch.BankBranch ?? DBNull.Value),
                new SqlParameter("@isYGC", (object) branch.IsYGC ?? DBNull.Value),
                new SqlParameter("@employeeId", (object) branch.EmployeeId ?? DBNull.Value),
                new SqlParameter("@createdBy", branch.CreatedBy)
            }, "spBranchSave");
        }

        public static bool Update(Branch branch)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@branchId", branch.ID),
                new SqlParameter("@branchCode", (object) branch.Code ?? DBNull.Value),
                new SqlParameter("@branchName", (object) branch.Name ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) branch.ChannelCode ?? DBNull.Value),
                new SqlParameter("@districtCode", (object) branch.DistrictCode ?? DBNull.Value),
                new SqlParameter("@tin", (object) branch.TIN  ?? DBNull.Value),
                new SqlParameter("@accountName", (object) branch.AccountName  ?? DBNull.Value),
                new SqlParameter("@accountNumber", (object) branch.AccountNumber  ?? DBNull.Value),
                new SqlParameter("@bankBranch", (object) branch.BankBranch  ?? DBNull.Value),
                new SqlParameter("@isYGC", (object) branch.IsYGC  ?? DBNull.Value),
                new SqlParameter("@employeeId", (object) branch.EmployeeId ?? DBNull.Value),
                new SqlParameter("@modifiedBy", branch.ModifiedBy)
            }, "spBranchUpdate");
        }

        private static Branch FillDataRecord(IDataRecord dataRecord)
        {
            return new Branch()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("BranchId")),
                Code = dataRecord["BranchCode"] as string,
                Name = dataRecord["BranchName"] as string,
                ChannelId = dataRecord.GetInt32(dataRecord.GetOrdinal("ChannelId")),
                ChannelName = dataRecord["ChannelName"] as string,
                DistrictId = dataRecord.GetInt32(dataRecord.GetOrdinal("DistrictId")),
                DistrictName = dataRecord["DistrictName"] as string,
                TIN = dataRecord["TIN"] as string,
                AccountName = dataRecord["AccountName"] as string,
                AccountNumber = dataRecord["AccountNumber"] as string,
                BankBranch = dataRecord["BankBranch"] as string,
                IsYGC = dataRecord["IsYGC"].AsBoolean(),
                RegionCode = dataRecord["RegionCode"] as string,
                RegionName = dataRecord["RegionName"] as string,
                ManagerName = dataRecord["ManagerName"] as string,
                EmployeeId = dataRecord.GetInt32(dataRecord.GetOrdinal("EmployeeId")),
                DistrictCode = dataRecord["DistrictCode"] as string,
                ChannelCode = dataRecord["ChannelCode"] as string,
            };
        }


        public static bool IsBranchExists(string channelCode, string branchCode, string branchName)
		{
			bool isBranch = false;
            SqlDataReader reader;
            string spName = "spBranchCodeNameExists";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            //int branchCount = 0;
			SqlParameter[] sqlParams = new SqlParameter[] {
                            new SqlParameter("@channelCode", channelCode),
                            new SqlParameter("@branchCode", branchCode),
							new SqlParameter("@branchName", branchName)};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //branchCount = reader["BranchCount"].AsInt();
                    isBranch = true;
                    break;
                }

                //if (branchCount > 0)
                //{
                //    isBranch = true;
                //}
            }
            reader.Close();
            reader.Dispose();
            reader = null;

			return isBranch;
		}

    }
}