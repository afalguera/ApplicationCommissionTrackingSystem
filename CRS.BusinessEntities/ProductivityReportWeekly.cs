using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class ReportWeek
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }

    }
    public class ProductivityReportWeekly
    {

        public string SourceName { get; set; }
        public string DisplayWeek { get; set; }
        public string SortWeek { get; set; }
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
    /*
    public class ProductivityReportMonthly
    {

        public string SourceName { get; set; }
        public string DisplayMonth { get; set; }
        public string SortMonth{ get; set; }
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }*/
    public class RejectedSummaryReportWeekly
    {

        public string SourceName { get; set; }
        public string DisplayWeek { get; set; }
        public string SortWeek { get; set; }
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }

    public class RejectedSummaryReportMonthly
    {

        public string ReasonName { get; set; }
        public string DisplayMonth { get; set; }
        public string SortMonth { get; set; }
        public int GroupCount { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
