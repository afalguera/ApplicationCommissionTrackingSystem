using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ApplicationStatusParam
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string SourceCode { get; set; }
        public string ApplicationNo { get; set; }
        public string ApplicantFullName { get; set; }
        public string StatusCode { get; set; }
        public bool IsSummary { get; set; }
        public string ReportType { get; set; }
        public bool IsReferror { get; set; }
        public string RegionCode { get; set; }
        public string AreaCode { get; set; }
        public string DistrictCode { get; set; }
        public string BranchCode { get; set; }
        public string ReferrorName { get; set; }
        public string CardBrandCode { get; set; }
        public string CardTypeCode { get; set; }
        public string ChannelCode { get; set; }
        public string ReferrorCode { get; set; }
    }
}