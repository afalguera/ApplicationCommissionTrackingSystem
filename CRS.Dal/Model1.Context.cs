﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ACTSdbContext : DbContext
    {
        public ACTSdbContext()
            : base("name=ACTSdbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CardBrand> CardBrands { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelDetail> ChannelDetails { get; set; }
        public virtual DbSet<ChannelCommissionRate> ChannelCommissionRates { get; set; }
    }
}
