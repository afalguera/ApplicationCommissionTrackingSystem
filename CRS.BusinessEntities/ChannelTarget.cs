using System;

namespace CRS.BusinessEntities
{
    public class ChannelTarget : ICRSBase
    {
        public int ID { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelName { get; set; }

        public string Year { get; set; }

        public decimal FullYearRunRate { get; set; }

        public decimal MTDTargetBooking { get; set; }

        public decimal FullYearTarget { get; set; }

        public decimal YTDTarget { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int MTM1 { get; set; }
        public int MTM2 { get; set; }
        public int MTM3 { get; set; }
        public int MTM4 { get; set; }
        public int MTM5 { get; set; }
        public int MTM6 { get; set; }
        public int MTM7 { get; set; }
        public int MTM8 { get; set; }
        public int MTM9 { get; set; }
        public int MTM10 { get; set; }
        public int MTM11 { get; set; }
        public int MTM12 { get; set; }



        public bool Validate()
        {
            return true;
        }
    }
}