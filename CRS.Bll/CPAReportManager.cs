using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class CPAReportManager
    {

        public static IEnumerable<CPAReport> GetCPAReport(int Year, Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReport(Year,IncludeExceptions, Channels);
        
        }

        public static IEnumerable<CPAReportInputsAppPCH> GetCPAReportInputsAppPCH(int Year, Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReportInputsAppPCH(Year, IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportInputsAppSimul> GetCPAReportInputsAppSimul(int Year, Boolean IncludeExceptions ,string Channels)
        {
            return CPAReportManagerDB.GetCPAReportInputsAppSimul(Year, IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportInputsCommissions> GetCPAReportInputsCommissions(int Year, Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReportInputsCommissions(Year, IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportInputsRejects> GetCPAReportInputsRejects(int Year, Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReportInputsRejects(Year, IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportSummaryActualApprovalRate> GetCPAReportSummaryActualApprovalRate(int Year,Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReportSummaryActualApprovalRate(Year,IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportSummaryApprovalRate> GetCPAReportSummaryApprovalRate(int Year, Boolean IncludeExceptions, string Channels )
        {
            return CPAReportManagerDB.GetCPAReportSummaryApprovalRate(Year,IncludeExceptions, Channels);
        }

        public static IEnumerable<CPAReportSummaryApprovals> GetCPAReportSummaryApprovals(int Year,Boolean IncludeExceptions, string Channels)
        {
            return CPAReportManagerDB.GetCPAReportSummaryApprovals(Year, IncludeExceptions, Channels);
        }

         public static IEnumerable<CPAReportSummaryCommission> GetCPAReportSummaryCommission(int Year)
        {
            return CPAReportManagerDB.GetCPAReportSummaryCommission(Year);
        }
       


       






        

    }
}