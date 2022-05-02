using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class CommissionDashboard : ApplicationStatus
    {
      
        public string ChannelCode { get; set; }
        //public string ChannelName { get; set; }
        public int ApprovalCount { get; set; }
        public decimal Tax { get; set; }
        public string Mode { get; set; }

        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string BranchCode { get; set; }
        //public string BranchName { get; set; }
        public string BranchManagerName { get; set; }
        public string BranchTIN { get; set; }

        public string OutletCode { get; set; }
        public string OutletName { get; set; }

        public decimal CommissionPoints { get; set; }
        public decimal CommissionRate { get; set; }

       
        public bool IsTiering { get; set; }
        public decimal TieringPoints { get; set; }
        public decimal TieringRate { get; set; }
        public int TieringCount { get; set; }
        
        public bool IsInflows { get; set; }
        public decimal InflowsPoints { get; set; }
        public decimal InflowsRate { get; set; }

        public bool IsUsage { get; set; }
        public decimal UsagePoints { get; set; }
        public decimal UsageRate { get; set; }
        public int InflowsCount { get; set; }

        public bool IsCoreBrand { get; set; }
        public decimal CoreBrandRate { get; set; }

        public bool IsCreditToBranch { get; set; }

        public int ApplicantInflowCount { get; set; }
        
        public bool IsCarDealer { get; set; }
        public decimal SERate { get; set; }
        public decimal NonSERate { get; set; }

        public bool IsInflowIncentive { get; set; }
        public int InflowIncentiveCount { get; set; }
        public decimal InflowIncentiveRate { get; set; }
      
        public bool IsForEveryCountIncentive { get; set; }
        public int ForEveryCountIncentiveCount { get; set; }
        public decimal ForEveryCountIncentiveRate { get; set; }

        public int ApplicationInflowsCount { get; set; }

        public bool IsBranchIncentive { get; set; }
        public decimal BranchIncentiveRate { get; set; }
        public int BranchIncentiveCount { get; set; }
        public string MainBranchName { get; set; }
        public string SecondaryBranchName { get; set; }

        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }

        public int ReferrorCount { get; set; }
        public bool IsGross { get; set; }
        public bool IsVatable { get; set; }
        public bool IsYGC { get; set; }

        public bool IsCore { get; set; }
        public bool IsRCBC { get; set; }
        public bool IsMyOrange { get; set; }

        public bool IsCardType { get; set; }
        public bool IsCardBrand { get; set; }

        public decimal Variable
        {
            get
            {
                decimal varAmount = 0;
                varAmount = this.CommissionRate;
                
                //Tiering
                if (IsTiering)
                {
                    if (this.ApprovalCount >= this.TieringCount)
                    {
                        varAmount = this.TieringRate;
                    }
                }
                //Inflows
                //else if (IsInflows)
                //{
                //    if (this.ApplicantInflowCount >= this.InflowsCount)
                //    {
                //        varAmount = this.InflowsRate;
                //    }
                //}
                //CarDealer
                //else if (IsCarDealer)
                //{
                //    varAmount = this.NonSERate;
                //    if (string.IsNullOrEmpty(this.ReferrorCode))
                //    {
                //        varAmount = this.SERate;
                //    }
                //}
                //Inflows Incentive
                else if (IsInflowIncentive)
                {
                    if (this.ApplicantInflowCount >= this.InflowIncentiveCount)
                    {
                        varAmount = this.InflowIncentiveRate;
                    }
                }
                //For Every Incentive
                //else if (IsForEveryCountIncentive)
                //{
                //    if (this.ApprovalCount >= this.InflowIncentiveCount)
                //    {
                //        varAmount = this.InflowIncentiveRate;
                //    }
                //}
                //Corebrand
                else if (IsCoreBrand)
                {
                    if (this.IsCore)
                    {
                        varAmount = this.CoreBrandRate;
                    }
                }
                else if (IsUsage)
                {
                    varAmount = UsageRate;
                }
                return varAmount;
            }
        }

        public decimal TotalIncentive
        {
            get
            {
                decimal incentiveAmount = 0;
                if (this.IsInflowIncentive)
                {
                    incentiveAmount = (this.Variable * this.ApplicantInflowCount);
                }
                //else if (this.IsForEveryCountIncentive)
                //{
                //    int modForEvery = this.ApprovalCount / this.ForEveryCountIncentiveCount;
                //    incentiveAmount = (this.Variable * modForEvery);
                //}
                return incentiveAmount;
            }
        }

        public decimal TaxAmount
        {
            get
            {
                decimal txAmt = 0;
                
                if( this.Tax > 0 ){
                    txAmt = this.Variable * this.ApprovalCount * Tax;
                }
                return txAmt;
            }
        }

        public decimal GrossAmount
        {
            get
            {
                return (this.Variable * this.ApprovalCount) + this.TotalIncentive;
            }
        }

        public decimal NetAmount
        {
            get
            {
                return this.GrossAmount - this.TaxAmount;
            }
        }

        public string TaxAmountString
        {
            get
            {
                return String.Format("{0:#,###0.00}", this.TaxAmount);
            }
        }

        public string GrossAmountString
        {
            get
            {
                return String.Format("{0:#,###0.00}", this.GrossAmount);
            }
        }

        public string NetAmountString
        {
            get
            {
                return String.Format("{0:#,###0.00}", this.NetAmount);
            }
        }

        public string VariableString
        {
            get
            {
                return String.Format("{0:#,###0.00}", this.Variable);
            }
        }

        public string TotalIncentiveString
        {
            get
            {
                return String.Format("{0:#,###0.00}", this.TotalIncentive);
            }
        }     
    }
}