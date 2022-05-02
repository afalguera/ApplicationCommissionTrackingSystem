using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CRS.BusinessEntities;
using System.Data.Common;


namespace CRS.Dal
{
    public class RedemptionReportManagerDB
    {


        public static IEnumerable<RedemptionReport> GetRedemptionReport(DateTime startdate, DateTime enddate)
        {
            RedemptionReportCollection tempList = new RedemptionReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRedemptionRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);


                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RedemptionReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillData(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

       
       private static RedemptionReport FillData(IDataRecord dataRecord)
       {
           RedemptionReport rpt = new RedemptionReport();


           rpt.Item = dataRecord.GetString(dataRecord.GetOrdinal("Item"));
           rpt.LastName = dataRecord.GetString(dataRecord.GetOrdinal("Last Name"));
           rpt.FirstName = dataRecord.GetString(dataRecord.GetOrdinal("First Name"));
           rpt.MiddleName = dataRecord.GetString(dataRecord.GetOrdinal("Middle Name"));
           rpt.RedemptionDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("Redemption Date"));
           rpt.Email = dataRecord.GetString(dataRecord.GetOrdinal("Email"));

           return rpt;
       }
        
        
        

        
    }
}