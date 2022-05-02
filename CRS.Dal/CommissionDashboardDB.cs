using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRS.Helper;

namespace CRS.Dal
{
	public class CommissionDashboardDB
	{

		#region Get Commission List
		public static IEnumerable<CommissionDashboard> GetCommissionDashboardList(string dateFrom,
															string dateTo,
															bool isSummary,
															string channelCode,																	
															string regionCode,
															string areaCode,
															string districtCode,
															string branchCode, 
															string referrorCode,
															string keyword                                         
													   )
		{
			string spName = "spCommissionDashboard";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<CommissionDashboard> list = new List<CommissionDashboard>();
			//DateTime dtFrom = dateFrom.AsDateTime();
			dateTo = String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1));
			isSummary = isSummary.AsBoolean();
	
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@isSummary", isSummary),
							new SqlParameter("@ChannelCode", channelCode),
							new SqlParameter("@RegionCode", regionCode),
							new SqlParameter("@AreaCode", areaCode),
							new SqlParameter("@DistrictCode", districtCode),
							new SqlParameter("@BranchCode", branchCode),
							new SqlParameter("@ReferrorCode", referrorCode),
							new SqlParameter("@keyword", keyword),
							};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				while (reader.Read())
					{
						CommissionDashboard dto = new CommissionDashboard();
						
						dto.ChannelCode = reader["ChannelCode"].ToString();
						dto.ChannelName = reader["ChannelName"].ToString();
						dto.RegionCode = reader["RegionCode"].ToString();
						dto.RegionName = reader["RegionName"].ToString();
						//dto.AreaCode = reader["AreaCode"].ToString();
						//dto.AreaName = reader["AreaName"].ToString();
						dto.DistrictCode = reader["DistrictCode"].ToString();
						dto.DistrictName = reader["DistrictName"].ToString();
						dto.BranchCode = reader["BranchCode"].ToString();
						dto.BranchName = reader["BranchName"].ToString();
						

						dto.CommissionPoints = reader["CommissionPoints"].AsDecimal();
						dto.CommissionRate = reader["CommissionRate"].AsDecimal();
						dto.Tax = reader["Tax"].AsDecimal();

						dto.IsTiering = reader["IsTiering"].AsBoolean();
						dto.TieringPoints = reader["TieringPoints"].AsDecimal();
						dto.TieringRate = reader["TieringRate"].AsDecimal();
						dto.TieringCount = reader["TieringCount"].AsInt();

						dto.IsInflows = reader["IsInflows"].AsBoolean();
						dto.InflowsPoints = reader["InflowsPoints"].AsDecimal();
						dto.InflowsRate = reader["InflowsRate"].AsDecimal();
						dto.InflowsCount = reader["InflowsCount"].AsInt();

						dto.IsUsage = reader["IsUsage"].AsBoolean();
						dto.UsagePoints = reader["UsagePoints"].AsDecimal();
						dto.UsageRate = reader["UsageRate"].AsDecimal();

                        //dto.IsCoreBrand = reader["IsCoreBrand"].AsBoolean();
                        //dto.CoreBrandRate = reader["CoreBrandRate"].AsDecimal();

						dto.IsCreditToBranch = reader["IsCreditToBranch"].AsBoolean();
						dto.IsCarDealer = reader["IsCarDealer"].AsBoolean();

						dto.SERate = reader["SERate"].AsDecimal();
						dto.NonSERate = reader["NonSERate"].AsDecimal();

						dto.IsInflowIncentive = reader["IsInflowIncentive"].AsBoolean();
						dto.InflowIncentiveRate = reader["InflowIncentiveRate"].AsDecimal();
						dto.InflowIncentiveCount = reader["InflowIncentiveCount"].AsInt();

						dto.IsForEveryCountIncentive = reader["IsForEveryCountIncentive"].AsBoolean();
						dto.ForEveryCountIncentiveRate = reader["ForEveryCountIncentiveRate"].AsDecimal();
						dto.ForEveryCountIncentiveCount = reader["ForEveryCountIncentiveCount"].AsInt();

						
						dto.BranchManagerName = reader["ManagerName"].ToString();
						//dto.IsCore = reader["IsCore"].AsBoolean();

						dto.Tax = reader["Tax"].AsDecimal();
						dto.ReferrorCode = reader["ReferrorCode"].ToString();
						dto.ReferrorName = reader["ReferrorName"].ToString();
                        //dto.Status = reader["ApplicationStatusCode"].ToString();

						if (isSummary)
						{
							//dto.ApplicationInflowsCount = reader["ApplicationInflowsCount"].AsInt();
							dto.ApprovalCount = reader["ApprovalCount"].AsInt();
						   
						}
						else
						{
							dto.ApprovalCount = 1;
                            //dto.ApprovalCount = dto.Status.Equals("Approved", StringComparison.CurrentCultureIgnoreCase) ? 1 : 0;
							//dto.Status = reader["ApplicationStatusCode"].ToString();
							dto.SourceCode = reader["SourceCode"].ToString();
							dto.CardBrand = reader["CardBrand"].ToString();
							dto.CardType = reader["CardType"].ToString();
							dto.DateEncoded = reader["DateEncoded"].ToString();
							dto.DateStatus = reader["StatusDate"].ToString();
							dto.ApplicantFullName = reader["ApplicantFullName"].ToString();
							//dto.OutletName = reader["OutletName"].ToString();
							dto.ApplicationNo = reader["ApplicationNo"].ToString();
						}
					   
					   
						list.Add(dto);
					}
                reader.Close();
                reader.Dispose();
                reader = null;
			}

			//if (!isSummary && list.Any())
            if(list.Any())
			{
				if(!string.IsNullOrEmpty(channelCode)){
					var isInflows = list.Select(x=> x.IsInflows).FirstOrDefault();
					var isInflowIncentive = list.Select(x=> x.IsInflowIncentive).FirstOrDefault();
					var isFilteredInflows = (isInflows || isInflowIncentive) ? true : false;
					var isTiering = list.Select(x => x.IsTiering).FirstOrDefault();
					var tearingCount = list.Select(x => x.TieringCount).FirstOrDefault().AsInt();
					var tearingRate = list.Select(x => x.TieringRate).FirstOrDefault().AsDecimal();
                    //var isCoreBrand = list.Select(x => x.IsCoreBrand).FirstOrDefault().AsBoolean();

									
					//if (!isFilteredInflows) {
                        if (isSummary)
                        {
                            list = list.Where(x => x.ApprovalCount > 0).ToList();
                            list = (from i in list
                                    group i by new
                                    {
                                        i.ChannelCode,
                                        i.ChannelName,
                                        i.RegionCode,
                                        i.RegionName,
                                        i.DistrictCode,
                                        i.DistrictName,
                                        i.BranchCode,
                                        i.BranchName,
                                        i.ReferrorCode,
                                        i.ReferrorName,
                                        i.BranchManagerName,
                                        i.ApprovalCount,
                                        i.ApplicantInflowCount,
                                        i.CommissionRate,
                                        i.Tax
                                    } into grp
                                    select new CommissionDashboard
                                    {
                                        ChannelCode = grp.Key.ChannelCode,
                                        ChannelName = grp.Key.ChannelName,
                                        RegionCode = grp.Key.RegionCode,
                                        RegionName = grp.Key.RegionName,
                                        DistrictCode = grp.Key.DistrictCode,
                                        DistrictName = grp.Key.DistrictName,
                                        BranchCode = grp.Key.BranchCode,
                                        BranchName = grp.Key.BranchName,
                                        BranchManagerName = grp.Key.BranchManagerName,
                                        ReferrorCode = grp.Key.ReferrorCode,
                                        ReferrorCount = grp.Count(),
                                        ApprovalCount = grp.Sum(x => x.ApprovalCount),
                                        ApplicationInflowsCount = grp.Sum(x => x.ApplicationInflowsCount),
                                        CommissionRate = grp.Key.CommissionRate,
                                        Tax = grp.Key.Tax

                                    })
                                      .ToList();
                           
                        }
                        //else
                        //{
                        //    list = list.Where(x => x.Status.Trim().Equals("Approved", StringComparison.CurrentCultureIgnoreCase)).ToList();
                        //    //list = list.Where(x => x.Status.Equals("Approved", StringComparison.CurrentCultureIgnoreCase)
                        //    //         && (x.DateStatus.AsDateTime() >= dateFrom.AsDateTime()
                        //    //         && x.DateStatus.AsDateTime() < dateTo.AsDateTime())).ToList();
                        //}
                        			   
					//}
                    //if (isSummary)
                    //{
                    //    list = (from i in list
                    //            group i by new
                    //            {
                    //                i.ChannelCode,
                    //                i.ChannelName,
                    //                i.RegionCode,
                    //                i.RegionName,
                    //                i.DistrictCode,
                    //                i.DistrictName,
                    //                i.BranchCode,
                    //                i.BranchName,
                    //                i.ReferrorCode,
                    //                i.ReferrorName,
                    //                i.BranchManagerName,
                    //                i.ApprovalCount,
                    //                i.ApplicantInflowCount,
                    //                i.CommissionRate,
                    //                i.Tax
                    //            } into grp
                    //            select new CommissionDashboard
                    //            {
                    //                ChannelCode = grp.Key.ChannelCode,
                    //                ChannelName = grp.Key.ChannelName,
                    //                RegionCode = grp.Key.RegionCode,
                    //                RegionName = grp.Key.RegionName,
                    //                DistrictCode = grp.Key.DistrictCode,
                    //                DistrictName = grp.Key.DistrictName,
                    //                BranchCode = grp.Key.BranchCode,
                    //                BranchName = grp.Key.BranchName,
                    //                BranchManagerName = grp.Key.BranchManagerName,
                    //                ReferrorCode = grp.Key.ReferrorCode,
                    //                ReferrorCount = grp.Count(),
                    //                ApprovalCount = grp.Sum(x => x.ApprovalCount),
                    //                ApplicationInflowsCount = grp.Sum(x => x.ApplicationInflowsCount),
                    //                CommissionRate = grp.Key.CommissionRate,
                    //                Tax  = grp.Key.Tax
                                    
                    //            })
                    //              .ToList();
                    //}
					if (isTiering)
					{
						var referrorList = (from i in list
											group i by new {
												i.RegionCode,
												//i.AreaCode,
												i.DistrictCode,
												i.BranchCode,
												i.ReferrorCode,
											} into grp
											select new
											{
												RegionCode = grp.Key.RegionCode,
												//AreaCode = grp.Key.AreaCode,
												DistrictCode = grp.Key.DistrictCode,
												BranchCode = grp.Key.BranchCode,
												ReferrorCode = grp.Key.ReferrorCode,
												ReferrorCount = grp.Count(),
											}).Where(x => x.ReferrorCount >= tearingCount)
										  .ToList();

						if (referrorList.Any())
						{
							foreach (var item in list)
							{
								bool bContains = referrorList.Where(x => x.RegionCode == item.RegionCode
																   //&& x.AreaCode == item.AreaCode
																   && x.DistrictCode == item.DistrictCode
																   && x.BranchCode == item.BranchCode
																   && x.ReferrorCode == item.ReferrorCode).Any();
								if (bContains)
								{
									item.CommissionRate = tearingRate;
								}

							}
						}
					}
				}
			}
			
			return list.AsEnumerable();
		} 
		#endregion

		#region Get Channel List
		public static IEnumerable<EAPRChannel> GetChannelList(string channelCode)
		{
			string spName = "spGetChannelList";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<EAPRChannel> list = new List<EAPRChannel>();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@channelCode", channelCode) 
							};
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					EAPRChannel dto = new EAPRChannel();
					dto.Id = reader["Id"].AsInt();
					dto.Code = reader["Code"].ToString();
					dto.Name = reader["Name"].ToString();
					dto.IsBranchName = reader["IsBranchName"].AsBoolean();
					dto.IsEAPR = reader["IsEAPR"].AsBoolean();
                    dto.IsDistrict = reader["IsDistrict"].AsBoolean();
					list.Add(dto);
				}
			}
            reader.Close();
            reader.Dispose();
            reader = null;
			return list.AsEnumerable();
		}
		#endregion

		#region Get Region List
		public static IEnumerable<EAPREntityPair> GetRegionList(string regionCode)
		{
			string spName = "spGetRegionList";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<EAPREntityPair> list = new List<EAPREntityPair>();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@regionCode", regionCode) 
							};
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					EAPREntityPair dto = new EAPREntityPair();
					dto.Id = reader["Id"].AsInt();
					dto.Code = reader["Code"].ToString();
					dto.Name = reader["Name"].ToString();
					list.Add(dto);
				}
			}
            reader.Close();
            reader.Dispose();
            reader = null;
			return list.AsEnumerable();
		}
		#endregion

		#region Get Area List
		public static IEnumerable<EAPREntityPair> GetAreaList(string regionCode, string areaCode)
		{
			string spName = "spGetAreaList";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<EAPREntityPair> list = new List<EAPREntityPair>();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@regionCode", regionCode),
							new SqlParameter("@areaCode", areaCode) 
							};
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					EAPREntityPair dto = new EAPREntityPair();
					dto.Id = reader["Id"].AsInt();
					dto.Code = reader["Code"].ToString();
					dto.Name = reader["Name"].ToString();
					list.Add(dto);
				}
			}
            reader.Close();
            reader.Dispose();
            reader = null;
			return list.AsEnumerable();
		}
		#endregion

		#region Get District List
        public static IEnumerable<EAPRDistrict> GetDistrictList(string channelCode, string regionCode, string districtCode)
		{
			string spName = "spGetDistrictList";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
            List<EAPRDistrict> list = new List<EAPRDistrict>();
			SqlParameter[] sqlParams = new SqlParameter[] {
                            new SqlParameter("@channelCode", channelCode),
							new SqlParameter("@regionCode", regionCode),
							new SqlParameter("@districtCode", districtCode) 
							};
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
                    EAPRDistrict dto = new EAPRDistrict();
					dto.Id = reader["Id"].AsInt();
					dto.Code = reader["Code"].ToString();
					dto.Name = reader["Name"].ToString();
                    dto.ChannelCode = reader["ChannelCode"].ToString();
					list.Add(dto);
				}
			}
            reader.Close();
            reader.Dispose();
            reader = null;
			return list.AsEnumerable();
		}
		#endregion

		#region Get Branch List
		public static IEnumerable<EAPREntityPair> GetBranchList(string channelCode, string districtCode, string branchCode)
		{
			string spName = "spGetBranchList";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<EAPREntityPair> list = new List<EAPREntityPair>();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@channelCode", channelCode),
							new SqlParameter("@districtCode", districtCode), 
							new SqlParameter("@branchCode", branchCode)
							};
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					EAPREntityPair dto = new EAPREntityPair();
					dto.Id = reader["Id"].AsInt();
					dto.Code = reader["Code"].ToString();
					dto.Name = reader["Name"].ToString();
					list.Add(dto);
				}
			}
            reader.Close();
            reader.Dispose();
            reader = null;
			return list.AsEnumerable();
		}
		#endregion

	}
}