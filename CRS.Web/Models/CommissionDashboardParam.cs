using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CommissionDashboardParam
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string SourceCode { get; set; }
        public string ChannelCode { get; set; }
        public string Keyword { get; set; }
        public bool IsSummary { get; set; }
        public bool IsReferror { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string DistrictCode { get; set; }
        public string BranchCode { get; set; }
        public string ReportType { get; set; }
    }
}