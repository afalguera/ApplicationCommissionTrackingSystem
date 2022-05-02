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
    public class PositionDetailsDB
    {
        public enum PositionDetailsFilters
        {
            positionCode,
            positionName
        }

        public static bool CheckIfExists(string filter, PositionDetailsFilters positionFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetPositionDetailsByFilter", new[] { new SqlParameter(positionFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<PositionDetails> GetList()
        {
            return DBSqlHelper.ExecuteGetList<PositionDetails>("spPositionDetailGetList", FillDataRecord, null);
        }

        public static bool Save(PositionDetails positionDetail)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@name", positionDetail.Name),
                new SqlParameter("@positionType", positionDetail.PositionType),
                new SqlParameter("@positionCode", positionDetail.Code),
                new SqlParameter("@createdBy", positionDetail.CreatedBy)
            }, "spPositionDetailSave");
        }

        public static bool Update(PositionDetails positionDetail)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@positionDetailsId", positionDetail.ID),
                new SqlParameter("@name", positionDetail.Name),
                new SqlParameter("@positionType", positionDetail.PositionType),
                new SqlParameter("@positionCode", positionDetail.Code),
                new SqlParameter("@modifiedBy", positionDetail.ModifiedBy)
            }, "spPositionDetailUpdate");
        }

        public static bool Delete(int positionDetailsId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", positionDetailsId), new SqlParameter("@deletedBy", deletedBy) }, "spPositionDetailDelete");
        }

        private static PositionDetails FillDataRecord(IDataRecord dataRecord)
        {
            return new PositionDetails()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("PositionDetailsId")),
                Code = dataRecord["PositionCode"] as string,
                Name = dataRecord["Name"] as string,
                PositionType = dataRecord["PositionType"] as string,
                PositionTypeName = dataRecord["PositionTypeName"] as string,
                PositionName = dataRecord["PositionName"] as string
            };
        }

        #region Get Position Type List
        public static IEnumerable<PositionType> GetPositionTypeList()
        {
            string spName = "spPositionTypeGetList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<PositionType> list = new List<PositionType>();
            SqlParameter[] sqlParams = new SqlParameter[]{};
            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PositionType dto = new PositionType();
                    dto.ID = reader["PositionTypeId"].AsInt();
                    dto.Code = reader["PositionTypeCode"].ToString();
                    dto.Name = reader["PositionTypeName"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        }
        #endregion
    }
}