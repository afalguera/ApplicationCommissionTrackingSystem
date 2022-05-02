using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class PaymentManager
    {
        public static IEnumerable<Payment> GetList()
        {
            return PaymentDB.GetList();
        }

        public static bool PaymentCodeExists(string paymentCode)
        {
            return PaymentDB.CheckIfExists(paymentCode, PaymentDB.PaymentFilters.paymentCode);
        }

        public static bool PaymentNameExists(string paymentName)
        {
            return PaymentDB.CheckIfExists(paymentName, PaymentDB.PaymentFilters.paymentName);
        }

        public static bool Save(Payment payment)
        {
            return PaymentDB.Save(payment);
        }

        public static bool Update(Payment payment)
        {
            return PaymentDB.Update(payment);
        }

        public static bool Delete(int paymentId, string deletedBy)
        {
            return PaymentDB.Delete(paymentId, deletedBy);
        }
    }
}