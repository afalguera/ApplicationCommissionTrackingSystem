using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
{
    public class RedemptionItemsManager
    {

        public static RedemptionItemsCollection GetList(byte isForAdmin)
        {
            return RedemptionItemsDB.GetList(isForAdmin);
        }

        public static int Delete(int id)
        {
            return RedemptionItemsDB.Delete(id);
        }

        public static int Save(RedemptionItems redemptionitems)
        {
            return RedemptionItemsDB.Save(redemptionitems);

        }

        public static bool IsRedemptionItemNameExists(string redemptionItemName)
        {
            return RedemptionItemsDB.IsRedemptionItemNameExists(redemptionItemName);
        }

        public static bool IsRedemptionItemCodeExists(string redemptionItemCode)
        {
            return RedemptionItemsDB.IsRedemptionItemCodeExists(redemptionItemCode);
        }

        public static bool Delete(int itemId, string deletedBy)
        {
            return RedemptionItemsDB.Delete(itemId, deletedBy);
        }


    }
}