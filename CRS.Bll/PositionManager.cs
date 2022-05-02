using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class PositionManager
    {
        public static IEnumerable<Position> GetList()
        {
            return PositionDB.GetList();
        }

        public static bool PositionCodeExists(string positionCode)
        {
            return PositionDB.CheckIfExists(positionCode, PositionDB.PositionFilters.positionCode);
        }

        public static bool PositionNameExists(string positionName)
        {
            return PositionDB.CheckIfExists(positionName, PositionDB.PositionFilters.positionName);
        }

        public static bool Save(Position position)
        {
            return PositionDB.Save(position);
        }

        public static bool Update(Position position)
        {
            return PositionDB.Update(position);
        }

        public static bool Delete(int positionId, string deletedBy)
        {
            return PositionDB.Delete(positionId, deletedBy);
        }
    }
}