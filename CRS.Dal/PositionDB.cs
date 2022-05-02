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
    public class PositionDB
    {
        public enum PositionFilters
        {
            positionCode,
            positionName
        }

        public static bool CheckIfExists(string filter, PositionFilters positionFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetPositionByFilter", new[] { new SqlParameter(positionFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<Position> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Position>("spPositionGetList", FillDataRecord, null);
        }

        public static bool Save(Position position)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@positionCode", position.Code),
                new SqlParameter("@positionName", position.Name),
                new SqlParameter("@createdBy", position.CreatedBy)
            }, "spPositionSave");
        }

        public static bool Update(Position position)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@positionId", position.ID),
                new SqlParameter("@positionCode", position.Code),
                new SqlParameter("@positionName", position.Name),
                new SqlParameter("@modifiedBy", position.ModifiedBy)
            }, "spPositionUpdate");
        }

        public static bool Delete(int positionId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", positionId), new SqlParameter("@deletedBy", deletedBy) }, "spPositionDelete");
        }

        private static Position FillDataRecord(IDataRecord dataRecord)
        {
            return new Position()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("PositionId")),
                Code = dataRecord["PositionCode"] as string,
                Name = dataRecord["PositionName"] as string
            };
        }
    }
}