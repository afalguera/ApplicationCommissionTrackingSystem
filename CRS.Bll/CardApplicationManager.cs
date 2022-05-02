using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class CardApplicationManager
    {
        /*
        public static CardApplicationCollection GetList(string UserName)
        {
            return CardApplicationDB.GetList(UserName);
        }

        public static CardApplicationCollection GetList(CardApplicantCriteria cardApplicant)
        {
            return CardApplicationDB.GetList(cardApplicant);
        }
         */

        public static IEnumerable<CardApplication> GetList(string UserName)
        {
            return CardApplicationDB.GetList(UserName);
        }

        public static IEnumerable<CardApplication> GetList(CardApplicantCriteria cardApplicant)
        {
            return CardApplicationDB.GetList(cardApplicant);
        }
    }
}