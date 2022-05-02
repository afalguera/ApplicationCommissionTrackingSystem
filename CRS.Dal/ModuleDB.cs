using CRS.BusinessEntities;
using CRS.Helper;
using System.Collections.Generic;
using System.Data;

namespace CRS.Dal
{
    public class ModuleDB
    {
        public static IEnumerable<Module> GetList()
        {
            return DBSqlHelper.ExecuteGetList<Module>("spGetModuleList", FillDataRecord, null);
        }

        private static Module FillDataRecord(IDataReader dataRecord)
        {
            return new Module()
            {
                ID = dataRecord["ModuleId"].AsInt32(),
                Name = dataRecord["ModuleName"] as string
            };
        }
    }
}