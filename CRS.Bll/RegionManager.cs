using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class RegionManager
    {
        public static IEnumerable<Region> GetList()
        {
            return RegionDB.GetList();
        }

        //public static bool RegionCRUD(Region region)
        //{
        //    return RegionDB.RegionCRUD(region.Mode,
        //        region.ID, region.Code, region.Name, region.UserBy
        //                           );

        //}

        public static bool RegionCodeExists(string regionCode)
        {
            return RegionDB.CheckIfExists(regionCode, RegionDB.RegionFilters.regionCode);
        }

        public static bool RegionNameExists(string regionName)
        {
            return RegionDB.CheckIfExists(regionName, RegionDB.RegionFilters.regionName);
        }

        public static bool Save(Region region)
        {
            return RegionDB.Save(region);
        }

        public static bool Update(Region region)
        {
            return RegionDB.Update(region);
        }

        public static bool Delete(int regionId, string deletedBy)
        {
            return RegionDB.Delete(regionId, deletedBy);
        }
    }
}