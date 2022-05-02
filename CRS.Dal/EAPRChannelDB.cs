using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRS.Helper;

namespace CRS.Dal
{
	public class EAPRChannelDB
	{
		#region Get EAPR Channel List
		public static IEnumerable<CommissionDashboard> GetEAPRChannelList(string dateFrom,
															string dateTo,
															string sourceCode,
															string channelCode,
															string reportType,
															//string programCode,
															string regionCode,
															string areaCode,
															string districtCode,
															string branchCode
													   )
		{
			string spName = "spEAPRChannel";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<CommissionDashboard> list = new List<CommissionDashboard>();
			//DateTime dtFrom = dateFrom.AsDateTime();
			dateTo = String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1));
		  
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@sourceCode",	sourceCode), 
							new SqlParameter("@RegionCode", regionCode),
							new SqlParameter("@AreaCode", areaCode),
							new SqlParameter("@DistrictCode", districtCode),
							new SqlParameter("@BranchCode", branchCode),
							new SqlParameter("@ChannelCode", channelCode),
							//new SqlParameter("@ProgramCode", programCode)
							};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					CommissionDashboard dto = new CommissionDashboard();
					dto.SourceCode = reader["SourceCode"].ToString();
					dto.DateStatus = reader["StatusDate"].ToString();
					dto.ChannelCode = reader["ChannelCode"].ToString();
					dto.ChannelName = reader["ChannelName"].ToString();
					dto.ApprovalCount = reader["ApprovalCount"].AsInt();
					dto.CommissionPoints = reader["CommissionPoints"].AsDecimal();
					dto.CommissionRate = reader["CommissionRate"].AsDecimal();

					dto.IsTiering= reader["IsTiering"].AsBoolean();
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

					dto.Tax = reader["Tax"].AsDecimal();
					list.Add(dto);
				}
			}
			reader.Close();
			reader.Dispose();
			reader = null;
			return list.AsEnumerable();
		}
		#endregion

		#region Get EAPR Channel Signatory 
		public static IEnumerable<Signatories> GetEAPRChannelSignatory(int positionDetailsID)
		{
			string spName = "spGetEAPRChannelSignatory";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			List<Signatories> list = new List<Signatories>();
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@positionDetailsId", positionDetailsID) };
			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Signatories dto = new Signatories();
					dto.PositionName = reader["PositionName"].ToString();
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

		#region Get EAPR Channel Item
		public static IEnumerable<EAPR> GetEAPRChannelItem(string dateFrom, string dateTo,
											  string eaprChannel, bool isPerBranch)
		{
			string spName = "spGetEAPRChannelItem";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			SqlDataReader reader;
			dateTo = String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1));
			List<EAPR> list = new List<EAPR>();
		  
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@ChannelCode", eaprChannel), 
							new SqlParameter("@isPerBranch", isPerBranch)
							};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					EAPR item = new EAPR();
					//item.ChannelCode = reader["ChannelCode"].ToString();
					item.ChannelName = reader["ChannelName"].ToString();
                    item.DistrictCode = reader["DistrictCode"].ToString();
					//item.ReferrorCode = reader["ReferrorCode"].ToString();
					//item.ReferrorName = reader["ReferrorName"].ToString();

					item.CommissionPoints = reader["CommissionPoints"].AsDecimal();
					item.CommissionRate = reader["CommissionRate"].AsDecimal();

					item.Tax = reader["Tax"].AsDecimal();

					item.IsTiering = reader["IsTiering"].AsBoolean();
					item.TieringPoints = reader["TieringPoints"].AsDecimal();
					item.TieringRate = reader["TieringRate"].AsDecimal();
					item.TieringCount = reader["TieringCount"].AsInt();

					item.IsInflows = reader["IsInflows"].AsBoolean();
					item.InflowsPoints = reader["InflowsPoints"].AsDecimal();
					item.InflowsRate = reader["InflowsRate"].AsDecimal();
					item.InflowsCount = reader["InflowsCount"].AsInt();

					item.IsUsage = reader["IsUsage"].AsBoolean();
					item.UsagePoints = reader["UsagePoints"].AsDecimal();
					item.UsageRate = reader["UsageRate"].AsDecimal();

                    //item.IsCoreBrand = reader["IsCoreBrand"].AsBoolean();
                    //item.CoreBrandRate = reader["CoreBrandRate"].AsDecimal();

					item.IsCreditToBranch = reader["IsCreditToBranch"].AsBoolean();
					item.IsCarDealer = reader["IsCarDealer"].AsBoolean();

					item.SERate = reader["SERate"].AsDecimal();
					item.NonSERate = reader["NonSERate"].AsDecimal();

					item.IsInflowIncentive = reader["IsInflowIncentive"].AsBoolean();
					item.InflowIncentiveRate = reader["InflowIncentiveRate"].AsDecimal();
					item.InflowIncentiveCount = reader["InflowIncentiveCount"].AsInt();

					item.IsForEveryCountIncentive = reader["IsForEveryCountIncentive"].AsBoolean();
					item.ForEveryCountIncentiveRate = reader["ForEveryCountIncentiveRate"].AsDecimal();
					item.ForEveryCountIncentiveCount = reader["ForEveryCountIncentiveCount"].AsInt();

					item.ApprovalCount = reader["ApprovalCount"].AsInt();
					item.ApplicantInflowCount = reader["ApplicantInflowsCount"].AsInt();

					item.PayeeName = reader["PayeeName"].ToString();
					item.PayeeTin = reader["PayeeTin"].ToString();
					item.AccountName = reader["AccountName"].ToString();
					item.AccountNumber = reader["AccountNumber"].ToString();
					item.BankBranch = reader["BankBranch"].ToString();

					item.SalesManagerId = reader["SalesManagerId"].AsInt();
					item.ChannelRequestorId = reader["ChannelRequestorId"].AsInt();
					item.ChannelCheckerId = reader["ChannelCheckerId"].AsInt();
					item.ChannelNoterId = reader["ChannelNoterId"].AsInt();

					item.IsBranchIncentive = reader["IsBranchIncentive"].AsBoolean();
					item.BranchIncentiveCount = reader["BranchIncentiveCount"].AsInt();
					item.BranchIncentiveRate = reader["BranchIncentiveRate"].AsDecimal();

					item.MainBranchName = reader["MainBranchName"].ToString();
					item.SecondaryBranchName = reader["SecondaryBranchName"].ToString();

					item.AccountName = reader["AccountName"].ToString();
					item.AccountNumber = reader["AccountNumber"].ToString();
					item.BankBranch = reader["BankBranch"].ToString();

					item.IsVatable = reader["IsVatable"].AsBoolean();
					item.IsGross = reader["IsGross"].AsBoolean();
					item.IsRCBC = reader["IsRCBC"].AsBoolean();
					item.IsMyOrange = reader["IsMyOrange"].AsBoolean();
					item.EAPRDescription = reader["EAPRDescription"].ToString();
					//item.IsCore = reader["IsCore"].AsBoolean();
                    item.IsCardBrand = reader["IsCardBrand"].AsBoolean();
                    item.IsCardType = reader["IsCardType"].AsBoolean();
                    
                 

					if (isPerBranch)
					{
						item.BranchCode = reader["BranchCode"].ToString();
						item.BranchName = reader["BranchName"].ToString();
						item.BranchTIN = reader["BranchTIN"].ToString();
						item.IsYGC = reader["IsYGC"].AsBoolean();
						item.BranchAccountName = reader["BranchAccountName"].ToString();
						item.BranchAccountNumber = reader["BranchAccountNumber"].ToString();
						item.BranchBankBranch = reader["BranchBankBranch"].ToString();
						item.ReferrorCount = reader["ReferrorCount"].AsInt();
					}
					else
					{
						item.ReferrorCode = reader["ReferrorCode"].ToString();
						item.ReferrorName = reader["ReferrorName"].ToString();
					}

					list.Add(item);
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