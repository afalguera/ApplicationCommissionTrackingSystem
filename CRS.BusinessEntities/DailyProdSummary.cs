using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class DailyProdSummary
    {


       
        public string ChannelName {get;set;}
        
        public string DisplayMonth {get;set;}
        
        public int SortMonth {get;set;}

        public int TargetBooking {get;set;}

        public int MTDTargetBooking {get;set;} 
        public int FullYearTarget {get;set;}
        public int FullYearRunRate {get;set;}

        public int YTDTarget { get; set; }

        public int Inflows {get;set;}

        public int Bookings { get; set; }

        public int Year { get; set; }

        public int ID { get; set; }
        public bool Validate()
        {
            return true;
        }
    }

    public class DailyProdSummaryMTD
    {



        public string ChannelName { get; set; }

        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }

        public int TargetBooking { get; set; }

        public int MTDTargetBooking { get; set; }
        public int FullYearTarget { get; set; }
        public int FullYearRunRate { get; set; }

        public int Inflows { get; set; }

        public int Bookings { get; set; }

        public int Year { get; set; }

        public int ID { get; set; }
        public bool Validate()
        {
            return true;
        }
    }

    public class DailyProdSummaryInflowsBookingsChart
    {

        

        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }

        public int RecordCount { get; set; }

        public string InputType { get; set; }

        public int Year { get; set; }

        public int ID { get; set; }
        
       
        
        
        public bool Validate()
        {
            return true;
        }
    }

    public class DailyProdSummaryBookingsChart
    {



        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }

        
        public int Year { get; set; }

        public int ID { get; set; }

        public int Bookings { get; set; }

       
        public bool Validate()
        {
            return true;
        }
    }


    public class DailyProdSummaryInflowsChart
    {



        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }


        public int Year { get; set; }

        public int ID { get; set; }

        public int Inflows { get; set; }


        public bool Validate()
        {
            return true;
        }
    }


    public class DailyProdSummaryMTDIntraYGC
    {



        public string ChannelName { get; set; }

        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }

        public int TargetBooking { get; set; }

        public int MTDTargetBooking { get; set; }
        public int FullYearTarget { get; set; }
        public int FullYearRunRate { get; set; }

        public int Inflows { get; set; }

        public int Bookings { get; set; }

        public int Year { get; set; }

        public int ID { get; set; }
        public bool Validate()
        {
            return true;
        }
    }

    public class DailyProdSummaryYTDIntraYGC
    {


        public string ChannelName { get; set; }

        public string DisplayMonth { get; set; }

        public int SortMonth { get; set; }

        public int TargetBooking { get; set; }

        public int MTDTargetBooking { get; set; }
        public int FullYearTarget { get; set; }
        public int FullYearRunRate { get; set; }

        public int YTDTarget { get; set; }
        public int Inflows { get; set; }

        public int Bookings { get; set; }

        public int Year { get; set; }

        public int ID { get; set; }
        public bool Validate()
        {
            return true;
        }
    }


}
