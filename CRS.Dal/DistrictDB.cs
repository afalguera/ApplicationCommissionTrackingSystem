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
    public class DistrictDB
    {
        public enum DistrictFilters
        {
            districtCode,
            districtName
        }

        public static bool CheckIfExists(string filter, DistrictFilters districtFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetDistrictByFilter", new[] { new SqlParameter(districtFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<District> GetList()
        {
            return DBSqlHelper.ExecuteGetList<District>("spDistrictGetList", FillDataRecord, null);
        }

        public static bool Save(District district)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@districtCode", (object) district.Code ?? DBNull.Value),
                new SqlParameter("@districtName", (object) district.Name ?? DBNull.Value),
                new SqlParameter("@regionCode",(object) district.RegionCode ?? DBNull.Value),
                new SqlParameter("@channelCode",(object) district.ChannelCode ?? DBNull.Value),
                new SqlParameter("@tin",(object) district.DistrictTIN ?? DBNull.Value),
                new SqlParameter("@accountName",(object) district.DistrictAccountName ?? DBNull.Value),
                new SqlParameter("@accountNumber",(object) district.DistrictAccountNumber ?? DBNull.Value),
                new SqlParameter("@bankBranch",(object) district.DistrictBankBranch ?? DBNull.Value),
                new SqlParameter("@createdBy", district.CreatedBy)
            }, "spDistrictSave");
        }

        public static bool Update(District district)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@districtId", district.ID),
                new SqlParameter("@districtCode", district.Code),
                new SqlParameter("@districtName", district.Name),
                new SqlParameter("@regionCode", district.RegionCode),
                new SqlParameter("@channelCode",(object) district.ChannelCode ?? DBNull.Value),
                new SqlParameter("@tin",(object) district.DistrictTIN ?? DBNull.Value),
                new SqlParameter("@accountName",(object) district.DistrictAccountName ?? DBNull.Value),
                new SqlParameter("@accountNumber",(object) district.DistrictAccountNumber ?? DBNull.Value),
                new SqlParameter("@bankBranch",(object) district.DistrictBankBranch ?? DBNull.Value),
                new SqlParameter("@modifiedBy", district.ModifiedBy)
            }, "spDistrictUpdate");
        }

        public static bool Delete(int districtId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", districtId), new SqlParameter("@deletedBy", deletedBy) }, "spDistrictDelete");
        }

        private static District FillDataRecord(IDataRecord dataRecord)
        {
            return new District()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("DistrictId")),
                Code = dataRecord["DistrictCode"] as string,
                Name = dataRecord["DistrictName"] as string,
                RegionCode = dataRecord["RegionCode"] as string,
                RegionName = dataRecord["RegionName"] as string,
                ChannelCode = dataRecord["ChannelCode"] as string,
                ChannelName = dataRecord["ChannelName"] as string,
                DistrictTIN = dataRecord["DistrictTIN"].ToString(),
                DistrictAccountName = dataRecord["DistrictAccountName"].ToString(),
                DistrictAccountNumber = dataRecord["DistrictAccountNumber"].ToString(),
                DistrictBankBranch = dataRecord["DistrictBankBranch"].ToString()
            };
        }
    }
}