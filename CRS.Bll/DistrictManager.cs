using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class DistrictManager
    {
        public static IEnumerable<District> GetList()
        {
            return DistrictDB.GetList();
        }

        public static bool DistrictCodeExists(string districtCode)
        {
            return DistrictDB.CheckIfExists(districtCode, DistrictDB.DistrictFilters.districtCode);
        }

        public static bool DistrictNameExists(string districtName)
        {
            return DistrictDB.CheckIfExists(districtName, DistrictDB.DistrictFilters.districtName);
        }

        public static bool Save(District district)
        {
            return DistrictDB.Save(district);
        }

        public static bool Update(District district)
        {
            return DistrictDB.Update(district);
        }

        public static bool Delete(int districtId, string deletedBy)
        {
            return DistrictDB.Delete(districtId, deletedBy);
        }
    }
}