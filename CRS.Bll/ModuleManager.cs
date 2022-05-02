using CRS.BusinessEntities;
using CRS.Dal;
using System.Collections.Generic;

namespace CRS.Bll
{
    public class ModuleManager
    {
        public static IEnumerable<Module> GetList()
        {
            return ModuleDB.GetList();
        }
    }
}