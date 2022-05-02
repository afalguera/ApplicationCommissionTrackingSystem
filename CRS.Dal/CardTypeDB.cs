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
    public class CardTypeDB
    {
        public enum CardTypeFilters
        {
            cardTypeCode,
            cardTypeName,
            cardSubTypeCode
        }

        //public static bool CheckIfExists(string filter, CardTypeFilters cardTypeFilter)
        public static bool CheckIfExists(string cardTypeCode, string cardTypeName, string cardSubTypeCode)
        {
            //return new DBSqlHelper().ExecuteReaderSPDB("spGetCardTypeByFilter", new[] { new SqlParameter(cardTypeFilter.ToString().Insert(0, "@"), filter) }).Read();
            bool isExists = false;
            SqlDataReader reader;
            string spName = "spGetCardTypeByFilter";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@cardTypeCode", cardTypeCode),
                            new SqlParameter("@cardTypeName", cardTypeName),
                            new SqlParameter("@cardSubTypeCode", cardSubTypeCode),
                        };

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //userCount = reader["UserCount"].AsInt();
                    isExists = true;
                    break;
                }

                //if (userCount > 0)
                //{
                //    isExists = true;
                //}
            }

            reader.Close();
            reader.Dispose();
            reader = null;

            return isExists;
        
        
        }

        public static IEnumerable<CardType> GetList()
        {
            return DBSqlHelper.ExecuteGetList<CardType>("spCardTypeGetList", FillDataRecord, null);
        }

        public static bool Save(CardType cardType)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardTypeCode", (object) cardType.Code ?? DBNull.Value),
                new SqlParameter("@cardTypeName", (object) cardType.Name ?? DBNull.Value),
                new SqlParameter("@cardBrandCode", (object) cardType.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@cardSubTypeCode", (object) cardType.CardSubTypeCode ?? DBNull.Value),
                new SqlParameter("@createdBy", (object) cardType.CreatedBy ?? DBNull.Value)
            }, "spCardTypeSave");
        }

        public static bool Update(CardType cardType)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardTypeId", cardType.ID),
                new SqlParameter("@cardTypeCode", (object) cardType.Code ?? DBNull.Value),
                new SqlParameter("@cardTypeName", (object) cardType.Name ?? DBNull.Value),
                new SqlParameter("@cardBrandCode", (object) cardType.CardBrandCode ?? DBNull.Value),
                new SqlParameter("@cardSubTypeCode", (object) cardType.CardSubTypeCode ?? DBNull.Value),
                new SqlParameter("@modifiedBy", (object) cardType.ModifiedBy ?? DBNull.Value)
            }, "spCardTypeUpdate");
        }

        public static bool Delete(int cardTypeId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", cardTypeId), new SqlParameter("@deletedBy", deletedBy) }, "spCardTypeDelete");
        }

        private static CardType FillDataRecord(IDataRecord dataRecord)
        {
            return new CardType()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CardTypeId")),
                Code = dataRecord["CardTypeCode"] as string,
                Name = dataRecord["CardTypeName"] as string,
                CardBrandCode = dataRecord["CardBrandCode"] as string,
                CardBrandName = dataRecord["CardBrandName"] as string,
                CardSubTypeCode = dataRecord["CardSubTypeCode"] as string,
                CardSubTypeName = dataRecord["CardSubTypeName"] as string,
            };
        }


        #region Get Card Sub Type List
        public static IEnumerable<EAPREntityPair> GetCardSubTypeList()
        {
            string spName = "spCardSubType";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<EAPREntityPair> list = new List<EAPREntityPair>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EAPREntityPair dto = new EAPREntityPair();
                    dto.Code = reader["Code"].ToString();
                    dto.Name = reader["Name"].ToString();
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