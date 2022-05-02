using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class PositionDetailsManager
    {
        public static IEnumerable<PositionDetails> GetList()
        {
            return PositionDetailsDB.GetList();
        }

        public static bool PositionDetailsCodeExists(string positionCode)
        {
            return PositionDetailsDB.CheckIfExists(positionCode, PositionDetailsDB.PositionDetailsFilters.positionCode);
        }

        public static bool PositionDetailsNameExists(string positionName)
        {
            return PositionDetailsDB.CheckIfExists(positionName, PositionDetailsDB.PositionDetailsFilters.positionName);
        }

        public static bool Save(PositionDetails positionDetails)
        {
            return PositionDetailsDB.Save(positionDetails);
        }

        public static bool Update(PositionDetails positionDetails)
        {
            return PositionDetailsDB.Update(positionDetails);
        }

        public static bool Delete(int positionId, string deletedBy)
        {
            return PositionDetailsDB.Delete(positionId, deletedBy);
        }

        public static IEnumerable<PositionType> GetPositionTypeList()
        {
            return PositionDetailsDB.GetPositionTypeList();
        }
    }
}