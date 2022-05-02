using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
{
    public class RedeemedItemsManager
    {

        public static RedeemedItemsCollection GetList(int userid)
        {
            return RedeemedItemsDB.GetList(userid);
        }

        public static RedeemedItemsCollection GetExpiredRedemptionList(int userid)
        {
            return RedeemedItemsDB.GetExpiredRedemptionList(userid);
        }
        
        public static int Save(RedeemedItems redeemeditems)
        {
            
            return RedeemedItemsDB.Save(redeemeditems);
        }

        public static int Cancel(int userid)
        {

            return RedeemedItemsDB.Cancel(userid);
        }

        public static int EmailSent(int userid)
        {

            return RedeemedItemsDB.EmailSent(userid);
        }
    }
}