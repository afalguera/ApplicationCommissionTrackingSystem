using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class CardBrandManager
    {
        public static IEnumerable<CardBrand> GetList()
        {
            return CardBrandDB.GetList();
        }

        public static bool CardBrandCodeExists(string cardBrandCode)
        {
            return CardBrandDB.CheckIfExists(cardBrandCode, CardBrandDB.CardBrandFilters.cardBrandCode);
        }

        public static bool CardBrandNameExists(string cardBrandName)
        {
            return CardBrandDB.CheckIfExists(cardBrandName, CardBrandDB.CardBrandFilters.cardBrandName);
        }

        public static bool Save(CardBrand cardBrand)
        {
            return CardBrandDB.Save(cardBrand);
        }

        public static bool Update(CardBrand cardBrand)
        {
            return CardBrandDB.Update(cardBrand);
        }

        public static bool Delete(int cardBrandId, string deletedBy)
        {
            return CardBrandDB.Delete(cardBrandId, deletedBy);
        }
    }
}