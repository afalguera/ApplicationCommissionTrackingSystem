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
    public class RegionDB
    {
        public enum RegionFilters
        {
            regionCode,
            regionName
        }

        public static bool CheckIfExists(string filter, RegionFilters regionFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetRegionByFilter", new[] { new SqlParameter(regionFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<Region> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Region>("spRegionGetList", FillDataRecord, null);
        }

        public static bool Save(Region region)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@regionCode", region.Code),
                new SqlParameter("@regionName", region.Name),
                new SqlParameter("@createdBy", region.CreatedBy)
            }, "spRegionSave");
        }

        public static bool Update(Region region)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@regionId", region.ID),
                new SqlParameter("@regionCode", region.Code),
                new SqlParameter("@regionName", region.Name),
                new SqlParameter("@modifiedBy", region.ModifiedBy)
            }, "spRegionUpdate");
        }

        public static bool Delete(int regionId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", regionId), new SqlParameter("@deletedBy", deletedBy) }, "spRegionDelete");
        }

        private static Region FillDataRecord(IDataRecord dataRecord)
        {
            return new Region()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("RegionId")),
                Code = dataRecord["RegionCode"] as string,
                Name = dataRecord["RegionName"] as string
            };
        }
    }
}