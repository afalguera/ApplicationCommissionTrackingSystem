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
	public class RedemptionItemsDB
	{

		public static RedemptionItemsCollection GetList(byte IsForAdmin)
		{
			RedemptionItemsCollection tempList = new RedemptionItemsCollection();
			using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
			{
				using (SqlCommand myCommand = new SqlCommand("spRedemptionItemsGetList", myConnection))
				{
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.AddWithValue("@IsForAdmin", IsForAdmin);
				   
					
					myConnection.Open();
					using (SqlDataReader myReader = myCommand.ExecuteReader())
					{
						if (myReader.HasRows)
						{
							tempList = new RedemptionItemsCollection();
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

	   
		private static RedemptionItems FillDataRecord(IDataRecord dataRecord)
		{
			RedemptionItems redemptionitems = new RedemptionItems();         
			redemptionitems.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("RedemptionItemsId"));
			redemptionitems.Code = dataRecord.GetString(dataRecord.GetOrdinal("RedemptionItemsCode"));
			redemptionitems.Name = dataRecord.GetString(dataRecord.GetOrdinal("RedemptionItemsName"));
			redemptionitems.PointsRequired = dataRecord.GetDecimal(dataRecord.GetOrdinal("PointsRequired"));
			redemptionitems.PointsRequiredString = String.Format("{0:#,###0.00}", redemptionitems.PointsRequired);
			redemptionitems.ImagePath = dataRecord.GetString(dataRecord.GetOrdinal("ImagePath"));
			redemptionitems.PeriodFromString = dataRecord.GetString(dataRecord.GetOrdinal("PeriodFrom"));
			redemptionitems.PeriodToString = dataRecord.GetString(dataRecord.GetOrdinal("PeriodTo"));
			return redemptionitems;
		}

		public static int Delete(int id)
		{
			int result = 0;
			try
			{
				
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spRedemptionItemsDelete", myConnection))
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
		public static int Save(RedemptionItems redemptionitems)
		{
			try
			{
				if (!redemptionitems.Validate())
				{
					throw new Exception("Can't save a RedemptionItems in an Invalid state.");
				}

				int result = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spRedemptionItemsSave", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@id", redemptionitems.ID);
						myCommand.Parameters.AddWithValue("@RedemptionItemsName", redemptionitems.Name);
                        myCommand.Parameters.AddWithValue("@RedemptionItemsCode", redemptionitems.Code);
						myCommand.Parameters.AddWithValue("@PointsRequired", redemptionitems.PointsRequired);
						myCommand.Parameters.AddWithValue("@PeriodFrom", redemptionitems.PeriodFrom.AsDateTime());
						myCommand.Parameters.AddWithValue("@PeriodTo", redemptionitems.PeriodTo.AsDateTime());
						myCommand.Parameters.AddWithValue("@ImagePath", redemptionitems.ImagePath);
                        myCommand.Parameters.AddWithValue("@userName", (redemptionitems.ID > 0 ? redemptionitems.ModifiedBy : redemptionitems.CreatedBy));
						
						myConnection.Open();
						int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
						if (numberOfRecordsAffected == 0)
						{
							throw new Exception("Can't update Redemption items");
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

		public static bool IsRedemptionItemNameExists(string redemptionItemName)
		{
			bool isValid = false;
			SqlDataReader reader;
			string spName = "spRedemptionItemNameExisting";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			int rCount = 0;
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@name", redemptionItemName)};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					rCount = reader["RedemptionCount"].AsInt();
				}

				if (rCount > 0)
				{
					isValid = true;
				}
			}
            reader.Close();
            reader.Dispose();
			reader = null;
			return isValid;
		}		

        public static bool IsRedemptionItemCodeExists(string redemptionItemCode)
		{
			bool isValid = false;
			SqlDataReader reader;
			string spName = "spRedemptionItemCodeExisting";
			DBSqlHelper sqlHelper = new DBSqlHelper();
			int rCount = 0;
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@code", redemptionItemCode)};

			reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					rCount = reader["RedemptionCount"].AsInt();
				}

				if (rCount > 0)
				{
					isValid = true;
				}
			}
            reader.Close();
            reader.Dispose();
			reader = null;
			return isValid;
		}

        public static bool Delete(int itemId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", itemId), new SqlParameter("@deletedBy", deletedBy) }, "spRedemptionItemsDelete");
        }
    }
	
}