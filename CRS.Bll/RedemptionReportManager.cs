using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class RedemptionReportManager
    {

        public static IEnumerable<RedemptionReport> GetRedemptionReport(DateTime startdate, DateTime enddate)
        {
            return RedemptionReportManagerDB.GetRedemptionReport(startdate,enddate);
        
        }

        

    }
}