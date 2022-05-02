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
    public class CardTypeDetailsDB
    {
        public static bool CheckIfExists(string cardTypeCode, string channelCode)
        {
            bool isExists = false;
            SqlDataReader reader;
            string spName = "spGetCardTypeDetailsByFilter";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@cardTypeCode", cardTypeCode),
                            new SqlParameter("@channelCode", channelCode)
                        };

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    isExists = true;
                    break;
                }

            }

            reader.Close();
            reader.Dispose();
            reader = null;

            return isExists;
        }

        public static IEnumerable<CardTypeDetails> GetList()
        {
            return DBSqlHelper.ExecuteGetList<CardTypeDetails>("spCardTypeDetailsGetList", FillDataRecord, null);
        }

        public static bool Save(CardTypeDetails cardTypeDetails)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardBrandCode", (object) cardTypeDetails.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@cardTypeCode", (object) cardTypeDetails.CardTypeCode ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) cardTypeDetails.ChannelCode ?? DBNull.Value),
                new SqlParameter("@commissionRate", (object) cardTypeDetails.CommissionRate ?? DBNull.Value),
                new SqlParameter("@createdBy", cardTypeDetails.CreatedBy)
            }, "spCardTypeDetailsSave");
        }

        public static bool Update(CardTypeDetails cardTypeDetails)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardTypeDetailsId", (object) cardTypeDetails.ID ?? DBNull.Value),
                new SqlParameter("@cardBrandCode", (object) cardTypeDetails.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@cardTypeCode", (object) cardTypeDetails.CardTypeCode ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) cardTypeDetails.ChannelCode ?? DBNull.Value),
                new SqlParameter("@commissionRate", (object) cardTypeDetails.CommissionRate ?? DBNull.Value),
                new SqlParameter("@modifiedBy", cardTypeDetails.ModifiedBy)
            }, "spCardTypeDetailsUpdate");
        }

        public static bool Delete(int cardTypeDetailsId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", cardTypeDetailsId), new SqlParameter("@deletedBy", deletedBy) }, "spCardTypeDetailsDelete");
        }

        private static CardTypeDetails FillDataRecord(IDataRecord dataRecord)
        {
            return new CardTypeDetails()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CardTypeDetailsId")),
                CardBrandCode = (dataRecord["CardBrandCode"] as string).Trim(),
                CardBrandName = (dataRecord["CardBrandName"] as string).Trim(),
                CardTypeCode = (dataRecord["CardTypeCode"] as string).Trim(),
                CardTypeName = dataRecord["CardTypeName"].ToString().Trim()
                                     + " (" +  (dataRecord["CardTypeCode"] as string).Trim() + ") ",
                ChannelCode = (dataRecord["ChannelCode"] as string).Trim(),
                ChannelName = (dataRecord["ChannelName"] as string).Trim(),
                CommissionRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("CommissionRate"))
            };
        }
    }
}