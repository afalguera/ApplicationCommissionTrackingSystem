using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class SourceManager
    {
        public static IEnumerable<Source> GetList()
        {
            return SourceDB.GetList();
        }

        public static bool SourceCodeExists(string sourceCode)
        {
            return SourceDB.CheckIfExists(sourceCode);
        }

        public static bool SourceNameExists(string sourceName)
        {
            return SourceDB.CheckIfExists(sourceName);
        }

        public static bool Save(Source source)
        {
            return SourceDB.Save(source);
        }

        public static bool Update(Source source)
        {
            return SourceDB.Update(source);
        }

        public static bool Delete(int sourceId, string deletedBy)
        {
            return SourceDB.Delete(sourceId, deletedBy);
        }
    }
}