using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class ProductivityReportEntity
    {
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonName { get; set; }
        public DateTime StatusDate { get; set; }
        public string Status { get; set; }
        public int RecordCount { get; set; }
        public int DummyYear { get; set; } 

        public string MonthYearDisplay { get; set; }
        public DateTime MonthYearDate { get; set; }
        public string WeekRangeDisplay { get; set; }

        public int YearDisplay { get; set; }
        public int MonthDisplay { get; set; }
       
        public bool IsCarDealer { get; set; }
        public bool IsDistrict { get; set; }
        public bool IsYGC { get; set; }
        public string YGCDescription { get; set; }
        public string ApplicantName { get; set; }
        public string ReferrorCode { get; set; }

        public bool WithRecord { get; set; }
        public int ApprovalCount { get; set; }
        public int RejectedCount { get; set; }
        public int InProcessCount { get; set; }
        public int IncompleteCount { get; set; }
    }

    public class WeekRangeEntity
    {
        public string StartWkDate { get; set; }
        public string EndWkDate { get; set; }
    }

    public class ChannelDistrictBranchEntity
    {
        public EAPRChannel Channel { get; set; }
        public List<EAPRDistrict> Districts { get; set; }
        public List<EAPREntityPair> Branches { get; set; }
    }

    public class RejectedReportEntity
    {
        public string Reason { get; set; }
        public int RecordCount { get; set; }
    }
}