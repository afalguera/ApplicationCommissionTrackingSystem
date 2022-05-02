using CRS.BusinessEntities;
using CRS.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CRS.Dal
{
	public class ChannelDetailsDB
	{
		enum Sample
		{
			trest
		}

		Dictionary<string, IList<Sample>> testing = new Dictionary<string,IList<Sample>>()
		{
			{
			 "testing", new[] 
				{
					Sample.trest
				}
			},
		};

		public static bool Delete(int channelDetailsId, string deletedBy)
		{
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@channelDetailsId", channelDetailsId), new SqlParameter("@deletedBy", deletedBy) }, "spChannelDetailsDelete");
		}

		public static ChannelDetails GetItem(int id)
		{
			return DBSqlHelper.ExecuteGet<ChannelDetails>("spGetChannelDetailsByFilter", FillDataRecord, new[] { new SqlParameter("id", id) });
		}

		public static IEnumerable<ChannelDetails> GetList()
		{
			return DBSqlHelper.ExecuteGetList<ChannelDetails>("spGetChannelDetailsListByFilter", FillDataRecord, null);
		}

		public static bool Save(ChannelDetails channelDetails)
		{
            //var maxDate = DateTime.MaxValue.ToShortDateString().AsDateTime();
            var maxEndDate =  DateTime.Parse(channelDetails.EffectiveEndDate.ToString()) == DateTime.Parse("01/01/0001") 
                   ? DBNull.Value
                   : (object)channelDetails.EffectiveEndDate;
			return DBSqlHelper.ExecuteCUD(new[]
			{
				new SqlParameter("@channelId", channelDetails.ChannelId),
				new SqlParameter("@isTiering", (object) channelDetails.IsTiering ?? DBNull.Value),
				new SqlParameter("@isUsage", (object) channelDetails.IsUsage ?? DBNull.Value),
				new SqlParameter("@isInflows", (object) channelDetails.IsInflows ?? DBNull.Value),
				new SqlParameter("@usageRate", (object) channelDetails.UsageRate ?? DBNull.Value),
				new SqlParameter("@usagePoints",(object) channelDetails.UsagePoints ?? DBNull.Value),
				new SqlParameter("@commRate", (object) channelDetails.CommRate ?? DBNull.Value),
				new SqlParameter("@commPoints", (object) channelDetails.CommPoints ?? DBNull.Value),
				new SqlParameter("@tieringRate", (object) channelDetails.TieringRate ?? DBNull.Value),
				new SqlParameter("@tieringPoints",(object) channelDetails.TieringPoints ?? DBNull.Value),
				new SqlParameter("@tieringCount", (object) channelDetails.TieringCount ?? DBNull.Value),
				new SqlParameter("@inflowsRate", (object) channelDetails.InflowsRate ?? DBNull.Value),
				new SqlParameter("@inflowsPoints", (object) channelDetails.InflowsPoints ?? DBNull.Value),
				new SqlParameter("@inflowsCount", (object) channelDetails.InflowsCount ?? DBNull.Value),
				//new SqlParameter("@isCoreBrand", (object) channelDetails.IsCoreBrand ?? DBNull.Value),
				//new SqlParameter("@coreBrandRate",(object) channelDetails.CoreBrandRate ?? DBNull.Value),
                new SqlParameter("@isCardBrand", (object) channelDetails.IsCardBrand ?? DBNull.Value),
                new SqlParameter("@isCardType", (object) channelDetails.IsCardType ?? DBNull.Value),
				new SqlParameter("@taxRate",(object) channelDetails.TaxRate ?? DBNull.Value),
				new SqlParameter("@isCreditToBranch", (object) channelDetails.IsCreditToBranch ?? DBNull.Value),
				new SqlParameter("@isCarDealer", (object) channelDetails.IsCarDealer ?? DBNull.Value),
				new SqlParameter("@seRate", (object) channelDetails.SERate ?? DBNull.Value),
				new SqlParameter("@nonSERate", (object) channelDetails.NonSERate ?? DBNull.Value),
				new SqlParameter("@isInflowIncentive", (object) channelDetails.IsInflowIncentive ?? DBNull.Value),
				new SqlParameter("@inflowIncentiveCount", (object) channelDetails.InflowIncentiveCount ?? DBNull.Value),
				new SqlParameter("@inflowIncentiveRate", (object) channelDetails.InflowIncentiveRate ?? DBNull.Value),
				new SqlParameter("@isForEveryCountIncentive", (object) channelDetails.IsForEveryCountIncentive ?? DBNull.Value),
				new SqlParameter("@forEveryCountIncentiveCount", (object) channelDetails.ForEveryCountIncentiveCount ?? DBNull.Value),
				new SqlParameter("@forEveryCountIncentiveRate", (object) channelDetails.ForEveryCountIncentiveRate ?? DBNull.Value),
				new SqlParameter("@isBranchIncentive", (object) channelDetails.IsBranchIncentive ?? DBNull.Value),
				new SqlParameter("@branchIncentiveRate", (object) channelDetails.BranchIncentiveRate ?? DBNull.Value),
				new SqlParameter("@branchIncentiveCount", (object) channelDetails.BranchIncentiveCount ?? DBNull.Value),
				new SqlParameter("@mainBranchName", (object) channelDetails.MainBranchName ?? DBNull.Value),
				new SqlParameter("@secondaryBranchName", (object) channelDetails.SecondaryBranchName ?? DBNull.Value),
				new SqlParameter("@effectiveStartDate", (object) channelDetails.EffectiveStartDate.AsDateTime().Date ??  SqlDateTime.MinValue.AsDateTime().Date),
				new SqlParameter("@effectiveEndDate", maxEndDate ),
				new SqlParameter("@createdBy", channelDetails.CreatedBy)
			}, "spChannelDetailsSave");
		}

		public static bool Update(ChannelDetails channelDetails)
		{
            var maxEndDate = DateTime.Parse(channelDetails.EffectiveEndDate.ToString()) == DateTime.Parse("01/01/0001")
                  ? DBNull.Value
                  : (object)channelDetails.EffectiveEndDate;

			return DBSqlHelper.ExecuteCUD(new[]
			{
                
				new SqlParameter("@channelDetailsId", channelDetails.ID),
				new SqlParameter("@channelId", channelDetails.ChannelId),
				new SqlParameter("@isTiering", (object) channelDetails.IsTiering ?? DBNull.Value),
				new SqlParameter("@isUsage", (object) channelDetails.IsUsage ?? DBNull.Value),
				new SqlParameter("@isInflows",(object) channelDetails.IsInflows ?? DBNull.Value),
				new SqlParameter("@usageRate",(object) channelDetails.UsageRate ?? DBNull.Value),
				new SqlParameter("@usagePoints",(object) channelDetails.UsagePoints ?? DBNull.Value),
				new SqlParameter("@commRate",(object) channelDetails.CommRate ?? DBNull.Value),
				new SqlParameter("@commPoints",(object) channelDetails.CommPoints ?? DBNull.Value),
				new SqlParameter("@tieringRate",(object) channelDetails.TieringRate ?? DBNull.Value),
				new SqlParameter("@tieringPoints",(object) channelDetails.TieringPoints ?? DBNull.Value),
				new SqlParameter("@tieringCount", (object)channelDetails.TieringCount ?? DBNull.Value),
				new SqlParameter("@inflowsRate", (object) channelDetails.InflowsRate ?? DBNull.Value),
				new SqlParameter("@inflowsPoints", (object) channelDetails.InflowsPoints ?? DBNull.Value),
				new SqlParameter("@inflowsCount", (object) channelDetails.InflowsCount ?? DBNull.Value),
                new SqlParameter("@isCardBrand", (object) channelDetails.IsCardBrand ?? DBNull.Value),
                new SqlParameter("@isCardType", (object) channelDetails.IsCardType ?? DBNull.Value),
                //new SqlParameter("@isCoreBrand", (object) channelDetails.IsCoreBrand ?? DBNull.Value),
                //new SqlParameter("@coreBrandRate", (object) channelDetails.CoreBrandRate ?? DBNull.Value),
				new SqlParameter("@taxRate", (object) channelDetails.TaxRate ?? DBNull.Value),
				new SqlParameter("@isCreditToBranch", (object) channelDetails.IsCreditToBranch ?? DBNull.Value),
				new SqlParameter("@isCarDealer", (object) channelDetails.IsCarDealer ?? DBNull.Value),
				new SqlParameter("@seRate", (object) channelDetails.SERate ?? DBNull.Value),
				new SqlParameter("@nonSERate", (object) channelDetails.NonSERate ?? DBNull.Value),
				new SqlParameter("@isInflowIncentive", (object) channelDetails.IsInflowIncentive ?? DBNull.Value),
				new SqlParameter("@inflowIncentiveCount",(object) channelDetails.InflowIncentiveCount ?? DBNull.Value),
				new SqlParameter("@inflowIncentiveRate", (object) channelDetails.InflowIncentiveRate ?? DBNull.Value),
				new SqlParameter("@isForEveryCountIncentive", (object) channelDetails.IsForEveryCountIncentive ?? DBNull.Value),
				new SqlParameter("@forEveryCountIncentiveCount",(object) channelDetails.ForEveryCountIncentiveCount ?? DBNull.Value),
				new SqlParameter("@forEveryCountIncentiveRate",(object) channelDetails.ForEveryCountIncentiveRate ?? DBNull.Value),
				new SqlParameter("@isBranchIncentive", (object) channelDetails.IsBranchIncentive ?? DBNull.Value),
				new SqlParameter("@branchIncentiveRate", (object) channelDetails.BranchIncentiveRate ?? DBNull.Value),
				new SqlParameter("@branchIncentiveCount", (object) channelDetails.BranchIncentiveCount ?? DBNull.Value),
				new SqlParameter("@mainBranchName", (object) channelDetails.MainBranchName ?? DBNull.Value),
				new SqlParameter("@secondaryBranchName",(object) channelDetails.SecondaryBranchName ?? DBNull.Value),
				new SqlParameter("@effectiveStartDate", (object) channelDetails.EffectiveStartDate.AsDateTime().Date ??  SqlDateTime.MinValue.AsDateTime().Date),
				new SqlParameter("@effectiveEndDate", maxEndDate),
				new SqlParameter("@modifiedBy", channelDetails.ModifiedBy)
			}, "spChannelDetailsUpdate");
		}

		private static ChannelDetails FillDataRecord(IDataRecord dataRecord)
		{

           var list = new ChannelDetails()
			//return new ChannelDetails()
			{
				ID = dataRecord["ChannelDetailsId"].AsInt32(),
                ChannelId = dataRecord["ChannelId"].AsInt32(),
				ChannelName = dataRecord["ChannelName"] as string,
				IsTiering = dataRecord["IsTiering"].AsBoolean(),
				IsUsage = dataRecord["IsUsage"].AsBoolean(),
				IsInflows = dataRecord["IsInflows"].AsBoolean(),
				UsageRate = dataRecord["UsageRate"].AsDecimal(),
				UsagePoints = dataRecord["UsagePoints"].AsDecimal(),
				CommRate = dataRecord["CommRate"].AsDecimal(),
				CommPoints = dataRecord["CommPoints"].AsDecimal(),
				TieringRate = dataRecord["TieringRate"].AsDecimal(),
				TieringPoints = dataRecord["TieringPoints"].AsDecimal(),
				TieringCount = dataRecord["TieringCount"].AsInt32(),
				InflowsRate = dataRecord["InflowsRate"].AsDecimal(),
				InflowsPoints = dataRecord["InflowsPoints"].AsDecimal(),
				InflowsCount = dataRecord["InflowsCount"].AsInt32(),
                IsCardBrand = dataRecord["IsCardBrand"].AsBoolean(),
                IsCardType = dataRecord["IsCardType"].AsBoolean(),
                //IsCoreBrand = dataRecord["IsCoreBrand"].AsBoolean(),
                //CoreBrandRate = dataRecord["CoreBrandRate"].AsDecimal(),
				TaxRate = dataRecord["TaxRate"].AsDecimal(),
				IsCreditToBranch = dataRecord["IsCreditToBranch"].AsBoolean(),
				IsCarDealer = dataRecord["IsCarDealer"].AsBoolean(),
				SERate = dataRecord["SERate"].AsDecimal(),
				NonSERate = dataRecord["NonSERate"].AsDecimal(),
				IsInflowIncentive = dataRecord["IsInflowIncentive"].AsBoolean(),
				InflowIncentiveCount = dataRecord["InflowIncentiveCount"].AsInt32(),
				InflowIncentiveRate = dataRecord["InflowIncentiveRate"].AsDecimal(),
				IsForEveryCountIncentive = dataRecord["IsForEveryCountIncentive"].AsBoolean(),
				ForEveryCountIncentiveCount = dataRecord["ForEveryCountIncentiveCount"].AsInt32(),
				ForEveryCountIncentiveRate = dataRecord["ForEveryCountIncentiveRate"].AsDecimal(),
				IsBranchIncentive = dataRecord["IsBranchIncentive"].AsBoolean(),
				BranchIncentiveRate = dataRecord["BranchIncentiveRate"].AsDecimal(),
				BranchIncentiveCount = dataRecord["BranchIncentiveCount"].AsInt32(),
				MainBranchName = dataRecord["MainBranchName"] as string,
				SecondaryBranchName = dataRecord["SecondaryBranchName"] as string,
				EffectiveStartDate = dataRecord["EffectiveStartDate"].AsDateTime(),
           		EffectiveEndDate = dataRecord["EffectiveEndDate"].AsDateTime()
			};

            return list;
		}
	}
}