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
    
    public partial class ChannelCommissionRate
    {
        public int CommissionRateId { get; set; }
        public string SourceCodePrefix { get; set; }
        public string SourceCodeSuffix { get; set; }
        public string SourceCode { get; set; }
        public string AgencyName { get; set; }
        public string Program { get; set; }
        public string SalesManager { get; set; }
        public string SalesManagerEmail { get; set; }
        public string AgencyEmail { get; set; }
        public string PayeeAccountNumber { get; set; }
        public string PayeeAccountName { get; set; }
        public string Tin { get; set; }
        public Nullable<int> Classic { get; set; }
        public Nullable<int> Gold { get; set; }
        public Nullable<int> Platinum { get; set; }
        public Nullable<int> Infinite { get; set; }
        public Nullable<int> World { get; set; }
        public Nullable<int> Diamond { get; set; }
        public Nullable<int> CreditLimit { get; set; }
        public Nullable<int> CoBrandBelowCreditLimit { get; set; }
        public Nullable<int> CoBrandAboveCreditLimit { get; set; }
        public Nullable<int> MasterCardAdditionalIncentives { get; set; }
        public Nullable<int> Approvals { get; set; }
        public Nullable<int> VisaClassic { get; set; }
        public Nullable<int> VisaGold { get; set; }
        public Nullable<int> VisaPlatinum { get; set; }
        public Nullable<int> VisaInfinite { get; set; }
        public Nullable<int> JCBClassic { get; set; }
        public Nullable<int> JCBGold { get; set; }
        public Nullable<int> JCBPlatinum { get; set; }
        public Nullable<int> Zalora { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifyDate { get; set; }
        public string LastModifyBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}