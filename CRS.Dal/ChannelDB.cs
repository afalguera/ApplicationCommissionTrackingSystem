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
    public class ChannelDB
    {
        public enum ChannelFilters
        {
            channelCode,
            channelName
        }

        public static bool CheckIfExists(string filter, ChannelFilters channelFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetChannelByFilter", new[] { new SqlParameter(channelFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static bool Delete(int channelId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", channelId), new SqlParameter("@deletedBy", deletedBy) }, "spChannelDelete");
        }

        public static BusinessEntities.Channel GetItem(int id)
        {
            return DBSqlHelper.ExecuteGet<BusinessEntities.Channel>("spGetChannelByFilter", FillDataRecord, new[] { new SqlParameter("id", id) });
        }

        public static IEnumerable<BusinessEntities.Channel> GetList()
        {
            return DBSqlHelper.ExecuteGetList<BusinessEntities.Channel>("spChannelGetList", FillDataRecord, null);
        }

        public static IEnumerable<PositionDetails> GetPositionDetailsList()
        {
            return DBSqlHelper.ExecuteGetList<PositionDetails>("spGetPositionDetailsList", 
                (dataReader) => 
                {
                    return new PositionDetails()
                    {
                        Id = dataReader["PositionDetailsId"].AsInt32(),
                        Code = dataReader["PositionCode"] as string,
                        Name = dataReader["Name"] as string,
                        PositionType = dataReader["PositionType"] as string,
                    };
                }, null);
        }

        public static bool Save(BusinessEntities.Channel channel)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@channelCode", channel.Code),
                new SqlParameter("@channelName", channel.Name),
                new SqlParameter("@payeeName", (object) channel.PayeeName ?? DBNull.Value),
                new SqlParameter("@payeeTin", (object) channel.PayeeTin ?? DBNull.Value),
                new SqlParameter("@accountName", (object) channel.AccountName ?? DBNull.Value),
                new SqlParameter("@accountNumber", (object) channel.AccountNumber ?? DBNull.Value),
                new SqlParameter("@bankBranch", (object) channel.BankBranch ?? DBNull.Value),
                new SqlParameter("@channelRequestorId", (object) channel.ChannelRequestorId ?? DBNull.Value),
                new SqlParameter("@channelCheckerId", (object) channel.ChannelCheckerId ?? DBNull.Value),
                new SqlParameter("@channelNoterId", (object) channel.ChannelNoterId ?? DBNull.Value),
                new SqlParameter("@salesManagerId", (object) channel.SalesManagerId ?? DBNull.Value),
                new SqlParameter("@isYGC", (object) channel.IsYGC ?? DBNull.Value),
                new SqlParameter("@createdBy", (object) channel.CreatedBy ?? DBNull.Value),
                new SqlParameter("@eaprDescription",  (object)channel.EAPRDescription != null ? (object)channel.EAPRDescription.ToString().Replace("\n","<br/>") : DBNull.Value),
                new SqlParameter("@isGross", (object) channel.IsGross ?? DBNull.Value),
                new SqlParameter("@isVatable", (object) channel.IsVatable ?? DBNull.Value),
                new SqlParameter("@isEAPR", (object) channel.IsEAPR ?? DBNull.Value),
                new SqlParameter("@isRCBC", (object) channel.IsRCBC ?? DBNull.Value),
                new SqlParameter("@isMyOrange",(object) channel.IsMyOrange ?? DBNull.Value)
            }, "spChannelSave");           
        }

        public static bool Update(BusinessEntities.Channel channel)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@channelId", channel.ID),
                new SqlParameter("@channelCode", channel.Code),
                new SqlParameter("@channelName", channel.Name),
                new SqlParameter("@payeeName",(object) channel.PayeeName ?? DBNull.Value),
                new SqlParameter("@payeeTin",(object) channel.PayeeTin ?? DBNull.Value),
                new SqlParameter("@accountName",(object) channel.AccountName ?? DBNull.Value),
                new SqlParameter("@accountNumber",(object) channel.AccountNumber ?? DBNull.Value),
                new SqlParameter("@bankBranch",(object) channel.BankBranch ?? DBNull.Value),
                new SqlParameter("@channelRequestorId",(object) channel.ChannelRequestorId ?? DBNull.Value),
                new SqlParameter("@channelCheckerId",(object) channel.ChannelCheckerId ?? DBNull.Value),
                new SqlParameter("@channelNoterId",(object) channel.ChannelNoterId ?? DBNull.Value),
                new SqlParameter("@salesManagerId",(object) channel.SalesManagerId ?? DBNull.Value),
                new SqlParameter("@isYGC", (object) channel.IsYGC ?? DBNull.Value),
                new SqlParameter("@modifiedBy", channel.ModifiedBy),
                new SqlParameter("@eaprDescription",  (object)channel.EAPRDescription != null ? (object)channel.EAPRDescription.ToString().Replace("\n","<br/>") : DBNull.Value),
                new SqlParameter("@isGross", (object) channel.IsGross ?? DBNull.Value),
                new SqlParameter("@isVatable", (object) channel.IsVatable ?? DBNull.Value),
                new SqlParameter("@isEAPR", (object) channel.IsEAPR ?? DBNull.Value),
                new SqlParameter("@isRCBC", (object) channel.IsRCBC ?? DBNull.Value),
                new SqlParameter("@isMyOrange",(object) channel.IsMyOrange ?? DBNull.Value)
            }, "spChannelUpdate");
        }
        
        private static BusinessEntities.Channel FillDataRecord(IDataRecord dataRecord)
		{
            

            //return new Channel()
            var ch = new BusinessEntities.Channel()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ChannelId")),
                Code = dataRecord["ChannelCode"] as string,
                Name = dataRecord["ChannelName"] as string,
                PayeeName = dataRecord["PayeeName"] as string,
                PayeeTin = dataRecord["PayeeTin"] as string,
                AccountName = dataRecord["AccountName"] as string,
                AccountNumber = dataRecord["AccountNumber"] as string,
                BankBranch = dataRecord["BankBranch"] as string,
                ChannelRequestor = dataRecord["ChannelRequestor"] as string,
                ChannelRequestorId = dataRecord.GetInt32(dataRecord.GetOrdinal("ChannelRequestorId")),
                ChannelChecker = dataRecord["ChannelChecker"] as string,
                ChannelCheckerId = dataRecord.GetInt32(dataRecord.GetOrdinal("ChannelCheckerId")),
                ChannelNoter = dataRecord["ChannelNoter"] as string,
                ChannelNoterId = dataRecord.GetInt32(dataRecord.GetOrdinal("ChannelNoterId")),
                SalesManager = dataRecord["SalesManager"] as string,
                SalesManagerId = dataRecord.GetInt32(dataRecord.GetOrdinal("SalesManagerId")),
                IsYGC = dataRecord["IsYGC"].AsBoolean(),
                IsGross = dataRecord["IsGross"].AsBoolean(),
                IsVatable = dataRecord["IsVatable"].AsBoolean(),
                IsEAPR = dataRecord["IsEAPR"].AsBoolean(),
                IsRCBC = dataRecord["IsRCBC"].AsBoolean(),
                IsMyOrange = dataRecord["IsMyOrange"].AsBoolean(),
                EAPRDescription = dataRecord["EAPRDescription"] != null
                                ? dataRecord["EAPRDescription"].ToString().Replace("<br/>", System.Environment.NewLine)
                                : string.Empty
            };

            return ch;
		}
    }
}