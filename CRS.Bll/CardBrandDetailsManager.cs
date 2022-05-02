using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace CRS.Bll
{
    public class CardBrandDetailsManager
    {
        public static IEnumerable<CardBrandDetails> GetList()
        {
            return CardBrandDetailsDB.GetList();
        }

        public static bool IsCardBrandDetailsExist(string cardBrandCode, string channelCode)
        {
            return CardBrandDetailsDB.CheckIfExists(cardBrandCode, channelCode);
        }

        public static bool Save(CardBrandDetails cardBrandDetails)
        {
            return CardBrandDetailsDB.Save(cardBrandDetails);
        }

        public static bool Update(CardBrandDetails cardBrandDetails)
        {
            return CardBrandDetailsDB.Update(cardBrandDetails);
        }

        public static bool Delete(int cardBrandDetailsId, string deletedBy)
        {
            return CardBrandDetailsDB.Delete(cardBrandDetailsId, deletedBy);
        }
    }
}