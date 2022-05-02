using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CRS.BusinessEntities;
using System.Data.Common;
using CRS.Helper;


namespace CRS.Dal
{
    public class CMSBannerAdsImageDB
    {

        public static CMSBannerAdsImageCollection GetList(string BannerAdsName,int RoleId)
        {
            CMSBannerAdsImageCollection tempList = new CMSBannerAdsImageCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCMSBannerAdsImageGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@BannerAdsName", BannerAdsName);
                    myCommand.Parameters.AddWithValue("@RoleId", RoleId);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CMSBannerAdsImageCollection();
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

        public static CMSBannerAdsImageCollection GetList(string BannerAdsName)
        {
            CMSBannerAdsImageCollection tempList = new CMSBannerAdsImageCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCMSBannerAdsImageGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@BannerAdsName", BannerAdsName);
                    myCommand.Parameters.AddWithValue("@RoleId", DBNull.Value);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CMSBannerAdsImageCollection();
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

        public static CMSBannerAdsImage GetItem(int id)
        {
            CMSBannerAdsImage tempItem = new CMSBannerAdsImage();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCMSBannerAdsImageGetItem", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@id", id);
                    myConnection.Open();
                    
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            tempItem = FillDataRecord(myReader);
                            
                        }
                        myReader.Close();
                    }
                }
            }
            return tempItem;
 
        }


        private static CMSBannerAdsImage FillDataRecord(IDataRecord dataRecord)
		{
            CMSBannerAdsImage adsimage = new CMSBannerAdsImage();

            adsimage.ImagePath = dataRecord.GetString(dataRecord.GetOrdinal("ImagePath"));
            adsimage.TargetUrl = dataRecord.GetString(dataRecord.GetOrdinal("TargetUrl"));
            adsimage.ImageName = dataRecord.GetString(dataRecord.GetOrdinal("CMSBannerAdsImageName"));
            adsimage.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("CMSBannerAdsImageId"));
            //adsimage.PeriodFrom = dataRecord.GetDateTime(dataRecord.GetOrdinal("PeriodFrom"));
            //adsimage.PeriodTo = dataRecord.GetDateTime(dataRecord.GetOrdinal("PeriodTo"));
            adsimage.RoleId = dataRecord.GetInt32(dataRecord.GetOrdinal("RoleId"));
            adsimage.RoleName = dataRecord.GetString(dataRecord.GetOrdinal("RoleName"));
            adsimage.PeriodFromString = dataRecord.GetString(dataRecord.GetOrdinal("PeriodFrom"));
            adsimage.PeriodToString = dataRecord.GetString(dataRecord.GetOrdinal("PeriodTo"));
            return adsimage;
		}

        public static int Delete(int id)
        {
            int result = 0;
            try
            {
                
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spCMSBannerAdsImageDelete", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.Parameters.AddWithValue("@id", id);
                        myConnection.Open();
                        int retvalue = myCommand.ExecuteNonQuery();
                        if (retvalue == 1)  //deleted 1 record?
                        {
                            result = 0; //success
                        }
                        else
                        { 
                            result = -1; //unsuccessful 
                        };
                    }
                    myConnection.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                result = -1;
                throw;
            }    
        
        }
        
        public static int Save(CMSBannerAdsImage adsimage)
        {
            try
            {
                if (!adsimage.Validate())
                {
                    throw new Exception("Can't save a CMSBannerAds in an Invalid state.");
                }

                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spCMSBannerAdsImageSave", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@id", adsimage.ID);
                        myCommand.Parameters.AddWithValue("@CMSBannerAdsImageName", (object) adsimage.ImageName ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@TargetURL", (object) adsimage.TargetUrl ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@RoleId", (object) adsimage.RoleId ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@PeriodFrom", (object) adsimage.PeriodFrom ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@PeriodTo", (object) adsimage.PeriodTo ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@ImagePath", (object)adsimage.ImagePath ?? DBNull.Value);
                        myCommand.Parameters.AddWithValue("@userName", (adsimage.ID > 0 ? adsimage.ModifiedBy : adsimage.CreatedBy));
                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update CMS Banner ads");
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

        public static bool Delete(int bannerAdsId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", bannerAdsId), new SqlParameter("@deletedBy", deletedBy) }, "spCMSBannerAdsImageDelete");
        }
        

        
    }
}