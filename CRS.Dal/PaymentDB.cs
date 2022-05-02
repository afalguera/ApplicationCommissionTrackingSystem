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
    public class PaymentDB
    {
        public enum PaymentFilters
        {
            paymentCode,
            paymentName
        }

        public static bool CheckIfExists(string filter, PaymentFilters paymentFilter)
        {
            return new DBSqlHelper().ExecuteReaderSPDB("spGetPaymentByFilter", new[] { new SqlParameter(paymentFilter.ToString().Insert(0, "@"), filter) }).Read();
        }

        public static IEnumerable<Payment> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Payment>("spPaymentGetList", FillDataRecord, null);
        }

        public static bool Save(Payment payment)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@paymentCode", (object) payment.Code ?? DBNull.Value),
                new SqlParameter("@paymentName", (object) payment.Name ?? DBNull.Value),
                new SqlParameter("@tin", (object) payment.Tin ?? DBNull.Value),
                new SqlParameter("@isVatable", (object)payment.IsVatable ?? DBNull.Value),
                new SqlParameter("@isGross", (object) payment.IsGross ?? DBNull.Value),
                new SqlParameter("@createdBy", payment.CreatedBy)
            }, "spPaymentSave");
        }

        public static bool Update(Payment payment)
        {
            return DBSqlHelper.ExecuteCUD(new[]
            {
                new SqlParameter("@paymentId", payment.ID),
                new SqlParameter("@paymentCode", (object) payment.Code ?? DBNull.Value),
                new SqlParameter("@paymentName", (object) payment.Name ?? DBNull.Value),
                new SqlParameter("@tin", (object) payment.Tin ?? DBNull.Value),
                new SqlParameter("@isVatable", (object) payment.IsGross ?? DBNull.Value),
                new SqlParameter("@isGross", (object) payment.IsVatable ?? DBNull.Value),
                new SqlParameter("@modifiedBy", payment.ModifiedBy)
            }, "spPaymentUpdate");
        }

        public static bool Delete(int paymentId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", paymentId), new SqlParameter("@deletedBy", deletedBy) }, "spPaymentDelete");
        }

        private static Payment FillDataRecord(IDataRecord dataRecord)
        {
            return new Payment()
            {
                ID = dataRecord.GetInt32(dataRecord.GetOrdinal("PaymentId")),
                Code = dataRecord["PaymentCode"] as string,
                Name = dataRecord["PaymentName"] as string,
                Tin = dataRecord["TIN"] as string,
                IsVatable = dataRecord["IsVatable"].AsBoolean(),
                IsGross = dataRecord["IsGross"].AsBoolean()
            };
        }
    }
}