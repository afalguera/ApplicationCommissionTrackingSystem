using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRS.Helper;

namespace CRS.Dal
{
    public class ExtensionDB
    {
        #region GetExtensionsList
        public static IEnumerable<Extension> GetExtensionsList(string dateFrom,
                                                             string dateTo,
                                                             string sourceCode,
                                                             string applicationNo,
                                                             string fullname,
                                                             string extensionType,
                                                             bool isSummary,
                                                             bool isReferror,
                                                             string channelCode,
                                                             string regionCode,
                                                             string areaCode,
                                                             string districtCode,
                                                             string branchCode,
                                                             string referrorCode,  
                                                             //string lastName,
                                                             //string firstName,
                                                             //string middleName,
                                                             //string dateOfBirth,
                                                             string referrorName,
                                                             string cardBrandCode,
                                                             string cardTypeCode
                                                    )
        {
            string spName = "spExtensions";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<Extension> list = new List<Extension>();
            //DateTime dtFrom = dateFrom.AsDateTime();
            dateTo = String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1));
            isSummary = isSummary.AsBoolean();
            isReferror = isReferror.AsBoolean();


            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@sourceCode",	sourceCode), 
							new SqlParameter("@applicationNo", applicationNo),
							new SqlParameter("@fullname", fullname ),
							new SqlParameter("@extensionType", extensionType),
							new SqlParameter("@isSummary", isSummary),
                            new SqlParameter("@ChannelCode", channelCode),
							new SqlParameter("@RegionCode", regionCode),
							new SqlParameter("@AreaCode", areaCode),
							new SqlParameter("@DistrictCode", districtCode),
							new SqlParameter("@BranchCode", branchCode),
                            new SqlParameter("@ReferrorCode", referrorCode),
						    new SqlParameter("@ReferrorName", referrorName),
                            new SqlParameter("@CardBrandCode", cardBrandCode),
                            new SqlParameter("@CardTypeCode", cardTypeCode),
							};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                  while (reader.Read())
                {
                    Extension dto = new Extension();
                    dto.SourceCode = reader["SourceCode"].ToString();
                    dto.CardBrand = reader["CardBrand"].ToString();
                    dto.CardType = reader["CardType"].ToString();
                    dto.Status = reader["Status"].ToString();
                    dto.DateStatus = reader["StatusDate"].ToString();
                    dto.ReferrorName = isReferror ? reader["ReferrorName"].ToString() : string.Empty;
                    dto.ReferrorCode = isReferror ? reader["ReferrorCode"].ToString() : string.Empty;
                    dto.ExtensionTypeCode = reader["ExtensionTypeCode"].ToString();
                    dto.ChannelName = isReferror ? reader["ChannelName"].ToString() : string.Empty;
                    dto.BranchName = isReferror ? reader["BranchName"].ToString() : string.Empty;

                    if (isSummary)
                    {
                        dto.ApplicationCount = reader["ApplicationCount"].AsInt();
                    }
                    else
                    {
                        dto.ApplicationNo = reader["ApplicationNo"].ToString();
                        dto.ApplicantFullName = reader["ApplicantFullName"].ToString();
                        dto.DateEncoded = reader["DateEncoded"].ToString();
                    }

                 list.Add(dto);

                //if (isSummary && isReferror)
                //{
                //    while (reader.Read())
                //    {
                //        Extension dto = new Extension();
                //        dto.SourceCode = reader["SourceCode"].ToString();
                //        dto.CardBrand = reader["CardBrand"].ToString();
                //        dto.DateStatus = reader["StatusDate"].ToString();
                //        dto.Status = reader["Status"].ToString();
                //        dto.ApplicationCount = reader["ApplicationCount"].AsInt();
                //        //dto.ReferrorLastName = reader["ReferrorLastName"].ToString();
                //        //dto.ReferrorFirstName = reader["ReferrorFirstName"].ToString();
                //        //dto.ReferrorMiddleName = reader["ReferrorMiddleName"].ToString();
                //        dto.ReferrorCode = reader["ReferrorCode"].ToString();
                //        dto.CardType = reader["CardType"].ToString();
                //        dto.ExtensionTypeCode = reader["ExtensionTypeCode"].ToString();
                //        list.Add(dto);
                //    }
                //}
                //else
                //{
                //    while (reader.Read())
                //    {
                //        Extension dto = new Extension();
                //        dto.SourceCode = reader["SourceCode"].ToString();
                //        dto.ApplicationNo = reader["ApplicationNo"].ToString();
                //        dto.ApplicantLastName = reader["ApplicantLastName"].ToString();
                //        dto.ApplicantFirstName = reader["ApplicantFirstName"].ToString();
                //        dto.ApplicantMiddleName = reader["ApplicantMiddleName"].ToString();
                //        dto.CardBrand = reader["CardBrand"].ToString();
                //        dto.DateEncoded = reader["DateEncoded"].ToString();
                //        dto.Status = reader["Status"].ToString();
                //        dto.CardType = reader["CardType"].ToString();
                //        dto.DateStatus = reader["StatusDate"].ToString();
                //        //dto.ReferrorLastName = isReferror ? reader["ReferrorLastName"].ToString() : string.Empty;
                //        //dto.ReferrorFirstName = isReferror ? reader["ReferrorFirstName"].ToString() : string.Empty;
                //        //dto.ReferrorMiddleName = isReferror ? reader["ReferrorMiddleName"].ToString() : string.Empty;
                //        dto.ReferrorCode = isReferror ? reader["ReferrorCode"].ToString() : string.Empty;
                //        dto.ExtensionTypeCode = reader["ExtensionTypeCode"].ToString();
                //        list.Add(dto);
                //    }
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