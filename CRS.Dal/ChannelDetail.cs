//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRS.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChannelDetail
    {
        public int ChannelDetailsId { get; set; }
        public int ChannelId { get; set; }
        public Nullable<bool> IsTiering { get; set; }
        public Nullable<bool> IsUsage { get; set; }
        public Nullable<bool> IsInflows { get; set; }
        public Nullable<decimal> UsageRate { get; set; }
        public Nullable<decimal> UsagePoints { get; set; }
        public Nullable<decimal> CommRate { get; set; }
        public Nullable<decimal> CommPoints { get; set; }
        public Nullable<decimal> TieringRate { get; set; }
        public Nullable<decimal> TieringPoints { get; set; }
        public Nullable<int> TieringCount { get; set; }
        public Nullable<decimal> InflowsRate { get; set; }
        public Nullable<decimal> InflowsPoints { get; set; }
        public Nullable<int> InflowsCount { get; set; }
        public Nullable<decimal> TaxRate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsCreditToBranch { get; set; }
        public Nullable<bool> IsCarDealer { get; set; }
        public Nullable<decimal> SERate { get; set; }
        public Nullable<decimal> NonSERate { get; set; }
        public Nullable<bool> IsInflowIncentive { get; set; }
        public Nullable<int> InflowIncentiveCount { get; set; }
        public Nullable<decimal> InflowIncentiveRate { get; set; }
        public Nullable<bool> IsForEveryCountIncentive { get; set; }
        public Nullable<int> ForEveryCountIncentiveCount { get; set; }
        public Nullable<decimal> ForEveryCountIncentiveRate { get; set; }
        public Nullable<bool> IsBranchIncentive { get; set; }
        public Nullable<decimal> BranchIncentiveRate { get; set; }
        public Nullable<int> BranchIncentiveCount { get; set; }
        public string MainBranchName { get; set; }
        public string SecondaryBranchName { get; set; }
        public Nullable<System.DateTime> EffectiveStartDate { get; set; }
        public Nullable<System.DateTime> EffectiveEndDate { get; set; }
        public Nullable<bool> IsCardBrand { get; set; }
        public Nullable<bool> IsCardType { get; set; }
        public Nullable<int> CardBrandId { get; set; }
        public Nullable<int> CardTypeId { get; set; }
    
        public virtual CardBrand CardBrand { get; set; }
        public virtual CardType CardType { get; set; }
        public virtual Channel Channel { get; set; }
    }
}