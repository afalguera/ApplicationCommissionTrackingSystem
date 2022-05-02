using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities.Reports
{
    public static class ReportDataContents<T>
    {
        public static List<T> Results { get; set; }
    }
}