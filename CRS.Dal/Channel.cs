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
    
    public partial class Channel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Channel()
        {
            this.ChannelDetails = new HashSet<ChannelDetail>();
        }
    
        public int ChannelId { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelName { get; set; }
        public string PayeeName { get; set; }
        public string PayeeTin { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankBranch { get; set; }
        public Nullable<int> ChannelRequestorId { get; set; }
        public Nullable<int> ChannelCheckerId { get; set; }
        public Nullable<int> ChannelNoterId { get; set; }
        public Nullable<int> SalesManagerId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<bool> IsYGC { get; set; }
        public Nullable<bool> IsBranchName { get; set; }
        public Nullable<bool> IsGross { get; set; }
        public Nullable<bool> IsVatable { get; set; }
        public Nullable<bool> IsEAPR { get; set; }
        public Nullable<bool> IsRCBC { get; set; }
        public Nullable<bool> IsMyOrange { get; set; }
        public string EAPRDescription { get; set; }
        public Nullable<bool> IsDistrict { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChannelDetail> ChannelDetails { get; set; }
    }
}
