using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class CardTypeDetailsManager
    {
        public static IEnumerable<CardTypeDetails> GetList()
        {
            return CardTypeDetailsDB.GetList();
        }

        public static bool IsCardTypeDetailsExist(string cardTypeCode, string channelCode)
        {
            return CardTypeDetailsDB.CheckIfExists(cardTypeCode, channelCode);
        }

        public static bool Save(CardTypeDetails cardTypeDetails)
        {
            return CardTypeDetailsDB.Save(cardTypeDetails);
        }

        public static bool Update(CardTypeDetails cardTypeDetails)
        {
            return CardTypeDetailsDB.Update(cardTypeDetails);
        }

        public static bool Delete(int cardTypeDetailsId, string deletedBy)
        {
            return CardTypeDetailsDB.Delete(cardTypeDetailsId, deletedBy);
        }
    }
}