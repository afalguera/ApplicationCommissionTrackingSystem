using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class CardTypeManager
    {
        public static IEnumerable<BusinessEntities.CardType> GetList()
        {
            return CardTypeDB.GetList();
        }

        public static bool CardTypeCodeExists(string cardTypeCode, string cardTypeName, string cardSubTypeCode)
        {
            return CardTypeDB.CheckIfExists(cardTypeCode, cardTypeName, cardSubTypeCode);
        }

        public static bool CardTypeNameExists(string cardTypeCode, string cardTypeName, string cardSubTypeCode)
        {
            return CardTypeDB.CheckIfExists(cardTypeCode, cardTypeName, cardSubTypeCode);
        }

        public static bool Save(BusinessEntities.CardType cardType)
        {
            return CardTypeDB.Save(cardType);
        }

        public static bool Update(BusinessEntities.CardType cardType)
        {
            return CardTypeDB.Update(cardType);
        }

        public static bool Delete(int cardTypeId, string deletedBy)
        {
            return CardTypeDB.Delete(cardTypeId, deletedBy);
        }

        public static IEnumerable<EAPREntityPair> GetCardSubTypeList()
        {
            return CardTypeDB.GetCardSubTypeList();
        }
    }
}