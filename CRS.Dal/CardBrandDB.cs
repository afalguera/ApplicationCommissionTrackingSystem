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
    public class CardBrandDB
    {
        public enum CardBrandFilters
        {
            cardBrandCode,
            cardBrandName
        }

        public static bool CheckIfExists(string filter, CardBrandFilters cardBrandFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetCardBrandByFilter", new[] { new SqlParameter(cardBrandFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<BusinessEntities.CardBrand> GetList()
        {
            return DBSqlHelper.ExecuteGetList<BusinessEntities.CardBrand>("spCardBrandGetList", FillDataRecord, null);
        }

        public static bool Save(BusinessEntities.CardBrand cardBrand)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardBrandCode", cardBrand.Code),
                new SqlParameter("@cardBrandName", cardBrand.Name),
                new SqlParameter("@createdBy", cardBrand.CreatedBy)
            }, "spCardBrandSave");
        }

        public static bool Update(BusinessEntities.CardBrand cardBrand)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@cardBrandId", cardBrand.ID),
                new SqlParameter("@cardBrandCode", cardBrand.Code),
                new SqlParameter("@cardBrandName", cardBrand.Name),
                new SqlParameter("@modifiedBy", cardBrand.ModifiedBy)
            }, "spCardBrandUpdate");
        }

        public static bool Delete(int cardBrandId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", cardBrandId), new SqlParameter("@deletedBy", deletedBy) }, "spCardBrandDelete");
        }

        private static BusinessEntities.CardBrand FillDataRecord(IDataRecord dataRecord)
        {
            return new BusinessEntities.CardBrand()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CardBrandId")),
                Code = dataRecord["CardBrandCode"] as string,
                Name = dataRecord["CardBrandName"] as string
            };
        }
    }
}