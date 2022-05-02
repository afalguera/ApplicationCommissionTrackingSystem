using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRS.Helper;


namespace CRS.Dal
{
	public class ApplicationStatusDB
	{
        #region GetApplicationStatusList
        public static IEnumerable<ApplicationStatus> GetApplicationStatusList(string dateFrom,
                                                             string dateTo,
                                                             string sourceCode,
                                                             string applicationNo,
                                                             string fullname,
                                                             string statusCode,
                                                             bool isSummary,
                                                             bool isReferror,
                                                             string channelCode,
                                                             string regionCode,
                                                             string areaCode,
                                                             string districtCode,
                                                             string branchCode,
                                                             string referrorCode,
                                                             string lastName,
                                                             string firstName,
                                                             string middleName,
                                                             string dateOfBirth,
                                                             string referrorName,
                                                             string cardBrandCode,
                                                             string cardTypeCode
                                                    )
        {
            string spName = "spApplicationStatus";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<ApplicationStatus> list = new List<ApplicationStatus>();
            dateFrom = isReferror ? dateFrom : string.Empty;
            dateTo =   isReferror ?  String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1)) : string.Empty;
            isSummary = isSummary.AsBoolean();
            isReferror = isReferror.AsBoolean();


            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@sourceCode",	sourceCode), 
							new SqlParameter("@applicationNo", applicationNo),
							new SqlParameter("@fullname", fullname ),
							new SqlParameter("@statusCode", statusCode),
							new SqlParameter("@isSummary", isSummary),
							new SqlParameter("@isReferror", isReferror),
                            new SqlParameter("@ChannelCode", channelCode),
							new SqlParameter("@RegionCode", regionCode),
							new SqlParameter("@AreaCode", areaCode),
							new SqlParameter("@DistrictCode", districtCode),
							new SqlParameter("@BranchCode", branchCode),
                            new SqlParameter("@ReferrorCode", referrorCode),
							new SqlParameter("@LastName", lastName),
							new SqlParameter("@FirstName", firstName),
							new SqlParameter("@MiddleName", middleName),
							new SqlParameter("@DateOfBirth", dateOfBirth),
                            new SqlParameter("@ReferrorName", referrorName),
                            new SqlParameter("@CardBrandCode", cardBrandCode),
                            new SqlParameter("@CardTypeCode", cardTypeCode),
							};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ApplicationStatus dto = new ApplicationStatus();
                    dto.SourceCode = reader["SourceCode"].ToString();
                    dto.CardBrand = reader["CardBrand"].ToString();
                    dto.CardType = reader["CardType"].ToString();
                    dto.Status = reader["Status"].ToString();
                    dto.DateStatus = reader["StatusDate"].ToString();
                    dto.ReferrorCode =  isReferror ? reader["ReferrorCode"].ToString() : string.Empty;
                    dto.ReferrorName = isReferror ? reader["ReferrorName"].ToString(): string.Empty;
                    dto.ChannelName = isReferror ? reader["ChannelName"].ToString() : string.Empty;
                    dto.BranchName = isReferror ? reader["BranchName"].ToString() : string.Empty;

                    if (isSummary)
                    {
                        dto.ApplicationCount = 1;
                    }
                    else
                    {
                        dto.ApplicationNo = reader["ApplicationNo"].ToString();
                        dto.ApplicantFullName = reader["ApplicantFullName"].ToString();
                        dto.DateEncoded = reader["DateEncoded"].ToString();
                        dto.ReasonName = reader["ReasonName"].ToString();
                        //dto.ReasonCode = reader["ReasonCode"].ToString();
                        dto.AgingString = dto.Status.Equals("Incomplete",StringComparison.InvariantCultureIgnoreCase) 
                                                    ?   String.Format("{0:n0}", reader["Aging"].AsInt())
                                                    : string.Empty;
                    }

                 list.Add(dto);

                }
             }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region GetStatusType
        public static IEnumerable<StatusType> GetStatusType()
        {
            string spName = "spGetStatusTypes";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<StatusType> list = new List<StatusType>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StatusType dto = new StatusType();
                    dto.StatusCode = reader["StatusCode"].ToString();
                    dto.StatusName = reader["StatusName"].ToString();

                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region GetReasons
        public static IEnumerable<Reason> GetReasons()
        {
            string spName = "spGetReasonsList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<Reason> list = new List<Reason>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Reason dto = new Reason();
                    dto.ReasonCode = reader["ReasonCode"].ToString();
                    dto.ReasonName = reader["ReasonName"].ToString();

                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region GetCardBrands
        public static IEnumerable<CardBrand> GetCardBrands()
        {
            string spName = "spGetCardBrands";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<CardBrand> list = new List<CardBrand>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CardBrand dto = new CardBrand();
                    dto.CardBrandCode = reader["CardBrandCode"].ToString();
                    dto.CardBrandName = reader["CardBrandName"].ToString();

                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region GetCardTypes
        public static IEnumerable<CardType> GetCardTypes(string cardBrandCode)
        {
            string spName = "spGetCardTypes";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<CardType> list = new List<CardType>();
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@cardBrandCode", cardBrandCode)};
            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CardType dto = new CardType();
                    dto.CardBrandCode = reader["CardBrandCode"].ToString();
                    dto.CardTypeCode = reader["CardTypeCode"].ToString();
                    dto.CardTypeName = reader["CardTypeName"].ToString()
                                     + " (" + dto.CardTypeCode + ") ";
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