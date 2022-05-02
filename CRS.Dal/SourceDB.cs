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
	public class SourceDB
	{
		public enum SourceFilters
		{
			sourceCode,
			sourceName
		}

		public static bool CheckIfExists(string filter)
		{
			bool isSource = false;
			SqlDataReader reader;
			string spName = "spGetSourceByFilter";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@id", 0),
							new SqlParameter("@SourceCode", (object)filter ?? DBNull.Value),
							new SqlParameter("@SourceName", (object)DBNull.Value)};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				isSource = true;
			}
			reader.Close();
			reader.Dispose();
			reader = null;
			return isSource;
		}

		public static IEnumerable<Source> GetList()
		{
			return DBSqlHelper.ExecuteGetList<Source>("spSourceGetList", FillDataRecord, null);
		}

		public static bool Save(Source source)
		{
			bool isSaved = false;
			try
			{
				isSaved = DBSqlHelper.ExecuteCUD(new[]
			{
				new SqlParameter("@SourceCode", (object) source.Code ?? DBNull.Value),
				new SqlParameter("@SourceName", (object) source.Name ?? DBNull.Value),
				new SqlParameter("@ChannelCode",(object) source.ChannelCode ?? DBNull.Value),
				new SqlParameter("@createdBy", (object) source.CreatedBy ?? DBNull.Value),
                new SqlParameter("@IsForCommission", (object) source.IsForCommission ?? DBNull.Value),
			}, "spSourceSave");
			}
			catch (Exception ex)
			{
				
			}
			return isSaved;
		}

		public static bool Update(Source source)
		{
			return DBSqlHelper.ExecuteCUD(new[]
			{
				new SqlParameter("@SourceId", source.ID),
				new SqlParameter("@SourceCode",(object) source.Code ?? DBNull.Value),
				new SqlParameter("@SourceName", (object) source.Name ?? DBNull.Value),
				new SqlParameter("@ChannelCode", (object) source.ChannelCode ?? DBNull.Value),
				new SqlParameter("@modifiedBy", (object) source.ModifiedBy ?? DBNull.Value),
                new SqlParameter("@IsForCommission", (object) source.IsForCommission ?? DBNull.Value),
			}, "spSourceUpdate");
		}

		public static bool Delete(int sourceId, string deletedBy)
		{
			return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", sourceId), new SqlParameter("@deletedBy", deletedBy) }, "spSourceDelete");
		}

		private static Source FillDataRecord(IDataRecord dataRecord)
		{
			return new Source()
			{
				ID = dataRecord.GetInt32(dataRecord.GetOrdinal("SourceId")),
				Code = dataRecord["SourceCode"] as string,
				Name = dataRecord["SourceName"] as string,
				ChannelCode = dataRecord["ChannelCode"] as string,
				ChannelName = dataRecord["ChannelName"] as string,
                IsForCommission = dataRecord["IsForCommission"].AsBoolean()
			};
		}
	}
}