using System;

namespace CRS.BusinessEntities
{
    public class ChannelDetails : ICRSBase
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int ChannelId { get; set; }

        public string ChannelName { get; set; }

        public bool IsTiering { get; set; }

        public bool IsUsage { get; set; }

        public bool IsInflows { get; set; }

        public decimal UsageRate { get; set; }

        public decimal UsagePoints { get; set; }

        public decimal CommRate { get; set; }

        public decimal CommPoints { get; set; }

        public decimal TieringRate { get; set; }

        public decimal TieringPoints { get; set; }

        public int TieringCount { get; set; }

        public decimal InflowsRate { get; set; }

        public decimal InflowsPoints { get; set; }

        public int InflowsCount { get; set; }

        //public bool IsCoreBrand { get; set; }

        //public decimal CoreBrandRate { get; set; }

        public bool IsCardBrand { get; set; }

        public bool IsCardType { get; set; }

        public decimal TaxRate { get; set; }

        public bool IsCreditToBranch { get; set; }

        public bool IsCarDealer { get; set; }

        public decimal SERate { get; set; }

        public decimal NonSERate { get; set; }

        public bool IsInflowIncentive { get; set; }

        public int InflowIncentiveCount { get; set; }

        public decimal InflowIncentiveRate { get; set; }

        public bool IsForEveryCountIncentive { get; set; }

        public int ForEveryCountIncentiveCount { get; set; }

        public decimal ForEveryCountIncentiveRate { get; set; }

        public bool IsBranchIncentive { get; set; }

        public decimal BranchIncentiveRate { get; set; }

        public int BranchIncentiveCount { get; set; }

        public string MainBranchName { get; set; }

        public string SecondaryBranchName { get; set; }

        public DateTime EffectiveStartDate { get; set; }

        public DateTime EffectiveEndDate { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}