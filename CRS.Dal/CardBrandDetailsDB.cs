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
    public class CardBrandDetailsDB
    {
        public enum CardBrandFilters
        {
            cardBrandCode,
            cardBrandName
        }

        public static bool CheckIfExists(string cardBrandCode, string channelCode)
        {
            bool isExists = false;
            SqlDataReader reader;
            string spName = "spGetCardBrandDetailsByFilter";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@cardBrandCode", cardBrandCode),
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

        public static IEnumerable<CardBrandDetails> GetList()
        {
            return DBSqlHelper.ExecuteGetList<CardBrandDetails>("spCardBrandDetailsGetList", FillDataRecord, null);
        }

        public static bool Save(CardBrandDetails cardBrandDetails)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardBrandCode", (object) cardBrandDetails.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) cardBrandDetails.ChannelCode ?? DBNull.Value),
                new SqlParameter("@commissionRate", (object) cardBrandDetails.CommissionRate ?? DBNull.Value),
                new SqlParameter("@createdBy", cardBrandDetails.CreatedBy)
            }, "spCardBrandDetailsSave");
        }

        public static bool Update(CardBrandDetails cardBrandDetails)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardBrandDetailsId", (object) cardBrandDetails.ID ?? DBNull.Value),
                new SqlParameter("@cardBrandCode", (object) cardBrandDetails.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@channelCode", (object) cardBrandDetails.ChannelCode ?? DBNull.Value),
                new SqlParameter("@commissionRate", (object) cardBrandDetails.CommissionRate ?? DBNull.Value),
                new SqlParameter("@modifiedBy", cardBrandDetails.ModifiedBy)
            }, "spCardBrandDetailsUpdate");
        }

        public static bool Delete(int cardBrandDetailsId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", cardBrandDetailsId), new SqlParameter("@deletedBy", deletedBy) }, "spCardBrandDetailsDelete");
        }

        private static CardBrandDetails FillDataRecord(IDataRecord dataRecord)
        {
            return new CardBrandDetails()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CardBrandDetailsId")),
                CardBrandCode = dataRecord["CardBrandCode"] as string,
                CardBrandName = dataRecord["CardBrandName"] as string,
                ChannelCode = dataRecord["ChannelCode"] as string,
                ChannelName = dataRecord["ChannelName"] as string,
                CommissionRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("CommissionRate")) 
            };
        }
    }
}