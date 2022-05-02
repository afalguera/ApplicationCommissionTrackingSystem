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
    public class ChannelTargetDB
    {
        public static bool CheckIfExists(string channelCode, string year)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetChannelTargetByFilter", new[] 
            { 
                new SqlParameter("@channelCode", channelCode),
                new SqlParameter("@channelYear", year)
            }).Read();
        }

        public static IEnumerable<ChannelTarget> GetList()
        {
            return DBSqlHelper.ExecuteGetList<ChannelTarget>("spChannelTargetGetList", FillDataRecord, null);
        }

        public static bool Save(ChannelTarget channelTarget)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@channelCode", (object) channelTarget.ChannelCode ?? DBNull.Value),
                new SqlParameter("@year", (object) channelTarget.Year ?? DBNull.Value),
                //new SqlParameter("@ytdTarget", (object) channelTarget.YTDTarget ?? DBNull.Value),
                //new SqlParameter("@ytdTarget", DBNull.Value),
                new SqlParameter("@mtm1", (object) channelTarget.MTM1 ?? DBNull.Value),
                new SqlParameter("@mtm2", (object) channelTarget.MTM2 ?? DBNull.Value),
                new SqlParameter("@mtm3", (object) channelTarget.MTM3 ?? DBNull.Value),
                new SqlParameter("@mtm4", (object) channelTarget.MTM4 ?? DBNull.Value),
                new SqlParameter("@mtm5", (object) channelTarget.MTM5 ?? DBNull.Value),
                new SqlParameter("@mtm6", (object) channelTarget.MTM6 ?? DBNull.Value),
                new SqlParameter("@mtm7", (object) channelTarget.MTM7 ?? DBNull.Value),
                new SqlParameter("@mtm8", (object) channelTarget.MTM8 ?? DBNull.Value),
                new SqlParameter("@mtm9", (object) channelTarget.MTM9 ?? DBNull.Value),
                new SqlParameter("@mtm10", (object) channelTarget.MTM10 ?? DBNull.Value),
                new SqlParameter("@mtm11", (object) channelTarget.MTM11 ?? DBNull.Value),
                new SqlParameter("@mtm12", (object) channelTarget.MTM12 ?? DBNull.Value),
                new SqlParameter("@createdBy", channelTarget.CreatedBy)
            }, "spChannelTargetSave");
        }

        public static bool Update(ChannelTarget channelTarget)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@channelTargetId", channelTarget.ID),
                new SqlParameter("@channelCode", (object) channelTarget.ChannelCode ?? DBNull.Value),
                new SqlParameter("@year", (object) channelTarget.Year ?? DBNull.Value),
                new SqlParameter("@mtm1", (object) channelTarget.MTM1 ?? DBNull.Value),
                new SqlParameter("@mtm2", (object) channelTarget.MTM2 ?? DBNull.Value),
                new SqlParameter("@mtm3", (object) channelTarget.MTM3 ?? DBNull.Value),
                new SqlParameter("@mtm4", (object) channelTarget.MTM4 ?? DBNull.Value),
                new SqlParameter("@mtm5", (object) channelTarget.MTM5 ?? DBNull.Value),
                new SqlParameter("@mtm6", (object) channelTarget.MTM6 ?? DBNull.Value),
                new SqlParameter("@mtm7", (object) channelTarget.MTM7 ?? DBNull.Value),
                new SqlParameter("@mtm8", (object) channelTarget.MTM8 ?? DBNull.Value),
                new SqlParameter("@mtm9", (object) channelTarget.MTM9 ?? DBNull.Value),
                new SqlParameter("@mtm10", (object) channelTarget.MTM10 ?? DBNull.Value),
                new SqlParameter("@mtm11", (object) channelTarget.MTM11 ?? DBNull.Value),
                new SqlParameter("@mtm12", (object) channelTarget.MTM12 ?? DBNull.Value),
                //new SqlParameter("@ytdTarget", (object) channelTarget.YTDTarget ?? DBNull.Value),
                new SqlParameter("@modifiedBy", channelTarget.ModifiedBy)
            }, "spChannelTargetUpdate");
        }

        public static bool Delete(int channelTargetId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", channelTargetId), new SqlParameter("@deletedBy", deletedBy) }, "spChannelTargetDelete");
        }

        private static ChannelTarget FillDataRecord(IDataRecord dataRecord)
        {
            return new ChannelTarget()
            {
                ID = dataRecord["ChannelTargetId"].AsInt32(),
                ChannelCode = dataRecord["ChannelCode"] as string,
                ChannelName = dataRecord["ChannelName"] as string,
                Year = dataRecord["Year"] as string,
                //FullYearRunRate = dataRecord["FullYearRunRate"].AsInt32(),
                //MTDTargetBooking = dataRecord["MTDTargetBooking"].AsInt32(),
                //FullYearTarget = dataRecord["FullYearTarget"].AsInt32(),
                //YTDTarget = dataRecord["YTDTarget"].AsDecimal(),
                MTM1 = dataRecord["MTM1"].AsInt(),
                MTM2 = dataRecord["MTM2"].AsInt(),
                MTM3 = dataRecord["MTM3"].AsInt(),
                MTM4 = dataRecord["MTM4"].AsInt(),
                MTM5 = dataRecord["MTM5"].AsInt(),
                MTM6 = dataRecord["MTM6"].AsInt(),
                MTM7 = dataRecord["MTM7"].AsInt(),
                MTM8 = dataRecord["MTM8"].AsInt(),
                MTM9 = dataRecord["MTM9"].AsInt(),
                MTM10 = dataRecord["MTM10"].AsInt(),
                MTM11 = dataRecord["MTM11"].AsInt(),
                MTM12 = dataRecord["MTM12"].AsInt()
            };
        }
    }
}