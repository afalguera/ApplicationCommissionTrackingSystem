using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class RedeemPointsManager
    {


        public static CMSBannerAdsImageCollection GetList(string BannerAdsName, int RoleId)
        {
            return CMSBannerAdsImageDB.GetList(BannerAdsName,RoleId);
        }

        public static CMSBannerAdsImageCollection GetList(string BannerAdsName)
        {
            return CMSBannerAdsImageDB.GetList(BannerAdsName);
        }

        public static CMSBannerAdsImage GetItem(int id)
        {
            return CMSBannerAdsImageDB.GetItem(id);
        }

        public static int Delete(int id)
        {
            return CMSBannerAdsImageDB.Delete(id);
        }
        
        public static int Save(CMSBannerAdsImage adsimage)
        {
            return CMSBannerAdsImageDB.Save(adsimage);

        }

    }
}