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
    public class CardApplicationDB
    {
        
        public static CardApplicationCollection GetList(string UserName)
        {
            CardApplicationCollection tempList = new CardApplicationCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCardApplicationGetListByUser", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@UserName", UserName);
                   
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CardApplicationCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecord(myReader) );
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static CardApplicationCollection GetList(CardApplicantCriteria sCardApplicant)
        {
            CardApplicationCollection tempList = new CardApplicationCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCardApplicationGetListByApplicant", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@firstname", sCardApplicant.FirstName);
                    myCommand.Parameters.AddWithValue("@middlename", sCardApplicant.MiddleName);
                    myCommand.Parameters.AddWithValue("@lastname", sCardApplicant.LastName);
                    myCommand.Parameters.AddWithValue("@dateofbirth", sCardApplicant.DateOfBirth);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CardApplicationCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecord(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


       private static CardApplication FillDataRecord(IDataRecord dataRecord)
		{
			CardApplication cardApplication = new CardApplication();
            int ordValue;

            ordValue = dataRecord.GetOrdinal("DateOfBirth");
            
            cardApplication.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CardApplicationId"));
			cardApplication.StatusCode = dataRecord.GetString(dataRecord.GetOrdinal("ApplicationStatusCode"));
		    cardApplication.Number = dataRecord.GetString(dataRecord.GetOrdinal("ApplicationNo"));
            cardApplication.LastName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantLastName"));
            cardApplication.FirstName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantFirstName"));
            cardApplication.MiddleName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantMiddleName"));


            cardApplication.DateOfBirth = dataRecord.IsDBNull(ordValue) ? Convert.ToDateTime("01/01/1100") : dataRecord.GetDateTime(dataRecord.GetOrdinal("DateOfBirth"));
            
	        return cardApplication;
		}

        
    }
}