using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
     public class ProductivitySummary
    {
    //MTD
        public string MTDSummaryChannelText { get; set; }
        public int MTDInflows { get; set; }
        public int MTDBookings { get; set; }
        public int MTDTargetBookings { get; set; }
        public decimal  MTDBW { get; set; }
        public decimal MTDApprovalRate { get; set; }
    //YTD
        public string YTDSummaryChannelText { get; set; }
        public int YTDInflows { get; set; }
        public int YTDBookings { get; set; }
        public int YTDTargetBookings { get; set; }
        public decimal YTDBW { get; set; }
        public decimal YTDApprovalRate { get; set; }
        public decimal YTDFullYearRateBookings { get; set; }
        public int YTDFullYearTarget { get; set; }
        public decimal YTDFullYearTargetBW { get; set; }
    }

    public class ProductivityGraph
    {
        public int ProdYear { get; set; }
        public string ProdMonth { get; set; }
        public int Inflows { get; set; }
        public int Bookings { get; set; }
        public int SortMonth { get; set; }
    }

    public class MTDYTDSummary : ProductivitySummary
    {

    }

    public class MTDYTDDetails : ProductivitySummary
    {

    }

    public class MTDYTDIntraYGC : ProductivitySummary
    {

    }
}