using CRS.BusinessEntities;
using CRS.Dal;
using System.Collections.Generic;

namespace CRS.Bll
{
    public class ManagerManager
    {
        public static IEnumerable<Manager> GetList()
        {
            return ManagerDB.GetList();
        }

        public static IEnumerable<object> GetManagerTypeList()
        {
            return ManagerDB.GetManagerTypeList();
        }

        public static bool Save(Manager manager)
        {
            return ManagerDB.Save(manager);
        }

        public static bool Update(Manager manager)
        {
            return ManagerDB.Update(manager);
        }

        public static bool Delete(int managerId, string deletedBy)
        {
            return ManagerDB.Delete(managerId, deletedBy);
        }

        //public static IEnumerable<Outlet> GetOutletList(int branchId)
        //{
        //    return ManagerDB.GetOutletList(branchId);
        //}
    }
}