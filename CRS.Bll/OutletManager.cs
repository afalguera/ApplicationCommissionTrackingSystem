using CRS.BusinessEntities;
using CRS.Dal;
using System.Collections.Generic;

namespace CRS.Bll
{
    public class OutletManager
    {
        public static bool OutletCodeExists(string outletCode)
        {
            return OutletDB.CheckIfExists(outletCode);
        }

        public static IEnumerable<Outlet> GetList()
        {
            return OutletDB.GetList();
        }

        public static bool Save(Outlet outlet)
        {
            return OutletDB.Save(outlet);
        }

        public static bool Update(Outlet outlet)
        {
            return OutletDB.Update(outlet);
        }

        public static bool Delete(int outletId, string deletedBy)
        {
            return OutletDB.Delete(outletId, deletedBy);
        }

        public static bool IsOutletExists(string outletName)
        {
            return OutletDB.IsOutletExists(outletName);
        } 
    }
}