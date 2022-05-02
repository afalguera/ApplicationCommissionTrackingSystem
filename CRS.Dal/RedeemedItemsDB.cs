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
    public class RedeemedItemsDB
    {




        public static RedeemedItemsCollection GetExpiredRedemptionList(int Id)
        {
            RedeemedItemsCollection tempList = new RedeemedItemsCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRedeemedItemsGetExpiredRedemptionList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@UserId", Id);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RedeemedItemsCollection();
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
        
        
        public static RedeemedItemsCollection GetList(int Id)
        {
            RedeemedItemsCollection tempList = new RedeemedItemsCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRedeemedItemsGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@UserId", Id);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RedeemedItemsCollection();
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

        
        private static RedeemedItems FillDataRecord(IDataRecord dataRecord)
		{
            RedeemedItems redeemeditems = new RedeemedItems();

            redeemeditems.RedemptionItemsID = dataRecord.GetInt32(dataRecord.GetOrdinal("RedemptionItemsId"));
            redeemeditems.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("RedeemedItemsId"));
            redeemeditems.UserID = dataRecord.GetInt32(dataRecord.GetOrdinal("UserId"));
            redeemeditems.Name = dataRecord.GetString(dataRecord.GetOrdinal("RedemptionItemsName"));          
            return redeemeditems;
		}


        public static int EmailSent(int UserId)
        {
            try
            {

                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRedeemedItemsEmailSent", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@UserId", UserId);

                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update Redeemed items");
                        }
                    }
                    myConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public static int Cancel(int UserId)
        {
            try
            {
                
                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRedeemedItemsCancel", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@UserId", UserId);

                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update Redeemed items");
                        }
                    }
                    myConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public static int Save(RedeemedItems redeemeditems)
        {
            try
            {
                if (!redeemeditems.Validate())
                {
                    throw new Exception("Can't save a RedeemedItems in an Invalid state.");
                }

                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRedeemedItemsSave", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@RedemptionItemsId", redeemeditems.RedemptionItemsID);
                        myCommand.Parameters.AddWithValue("@UserId", redeemeditems.UserID);
                        
                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update Redeemed items");
                        }
                    }
                    myConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            { 
                throw;
            }
        }

        
    }
}