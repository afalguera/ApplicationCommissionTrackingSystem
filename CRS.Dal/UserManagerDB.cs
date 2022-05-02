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
	public class UserManagerDB
	{


		public static int GetUserIdFromEmail(string email)
		{
			try
			{
				int? result = null;
				int resultint = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spGetUserIdFromEmail", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@Email", email);
					   
						myConnection.Open();

						if (myCommand.ExecuteScalar() == null)
						{
							result = 0;
						}
						else
						{
							result = (int)myCommand.ExecuteScalar();
						}
						if (result == null)
						{
							resultint = 0;
						}
						else
						{
							resultint = (int)result;
						}
					}
					myConnection.Close();
				}
				return resultint;
			}
			catch (Exception ex)
			{
				throw;
			}
		}


		public static int ChangePassword(int id, string NewPassword)
		{
			try
			{
			   

				int result = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spChangePassword", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@id", id);
						myCommand.Parameters.AddWithValue("@NewPassword", NewPassword);


						myConnection.Open();
						result = myCommand.ExecuteNonQuery();
						
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
		
		
		public static int ApproveUser(User usr)
		{
			try
			{
				if (!usr.Validate())
				{
					throw new Exception("Can't save a User in an Invalid state.");
				}

				int result = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spApproveUser", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@id", usr.ID);
						myCommand.Parameters.AddWithValue("@RoleId", usr.Role);
						myCommand.Parameters.AddWithValue("@ChannelCode", usr.Channel);


						myConnection.Open();
						int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
						if (numberOfRecordsAffected == 0)
						{
							throw new Exception("Can't update User items");
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


		public static int Update(User usr)
		{
			try
			{
				if (!usr.Validate())
				{
					throw new Exception("Can't save a User in an Invalid state.");
				}

				int result = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spUserUpdate", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@id", usr.ID);
						myCommand.Parameters.AddWithValue("@Password", usr.Password == null ? "" : usr.Password);
						myCommand.Parameters.AddWithValue("@FirstName", usr.FirstName);
						myCommand.Parameters.AddWithValue("@MiddleName", usr.MiddleName);
						myCommand.Parameters.AddWithValue("@LastName", usr.LastName);
						myCommand.Parameters.AddWithValue("@Email", usr.Email);
						myCommand.Parameters.AddWithValue("@RoleId", usr.Role);
						myCommand.Parameters.AddWithValue("@ChannelCode", usr.Channel);
						myCommand.Parameters.AddWithValue("@RegionCode", usr.RegionCode);
						//myCommand.Parameters.AddWithValue("@AreaCode", usr.AreaCode);
						myCommand.Parameters.AddWithValue("@DistrictCode", usr.DistrictCode);
						myCommand.Parameters.AddWithValue("@BranchCode", usr.BranchCode);
						myCommand.Parameters.AddWithValue("@ReferrorCode", usr.ReferrorCode);
						myCommand.Parameters.AddWithValue("@ModifiedBy", usr.ModifiedBy);

						myConnection.Open();
						int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
						if (numberOfRecordsAffected == 0)
						{
							throw new Exception("Can't update User items");
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
		
		public static int Save(User usr)
		{
			try
			{
				if (!usr.Validate())
				{
					throw new Exception("Can't save a User in an Invalid state.");
				}

				int result = 0;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spUserSave", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;

						myCommand.Parameters.AddWithValue("@id", usr.ID);
						myCommand.Parameters.AddWithValue("@UserName", usr.UserName);
						myCommand.Parameters.AddWithValue("@Password", usr.Password == null ? "" : usr.Password);
						myCommand.Parameters.AddWithValue("@FirstName", usr.FirstName);
						myCommand.Parameters.AddWithValue("@MiddleName", (object) usr.MiddleName ?? DBNull.Value);
						myCommand.Parameters.AddWithValue("@LastName", usr.LastName);
						myCommand.Parameters.AddWithValue("@Email", usr.Email);
						myCommand.Parameters.AddWithValue("@RoleId", usr.Role);
						myCommand.Parameters.AddWithValue("@ChannelCode", (object) usr.Channel ?? DBNull.Value);
						myCommand.Parameters.AddWithValue("@RegionCode", (object) usr.RegionCode ?? DBNull.Value);
						//myCommand.Parameters.AddWithValue("@AreaCode", usr.AreaCode);
						myCommand.Parameters.AddWithValue("@DistrictCode", (object) usr.DistrictCode ?? DBNull.Value);
						myCommand.Parameters.AddWithValue("@BranchCode", (object) usr.BranchCode ?? DBNull.Value);
						myCommand.Parameters.AddWithValue("@ReferrorCode", (object) usr.ReferrorCode ?? DBNull.Value );
						myCommand.Parameters.AddWithValue("@CreatedBy", usr.CreatedBy);

						 // new SqlParameter("@payeeName", (object) channel.PayeeName ?? DBNull.Value),

						myConnection.Open();
						int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
						if (numberOfRecordsAffected == 0)
						{
							throw new Exception("Can't update User items");
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

		public static User GetItem(int id)
		{
			try
			{

				User user = null;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spUserGetListById", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;
						myCommand.Parameters.AddWithValue("@ID", id);
						
						myConnection.Open();
						using (SqlDataReader myReader = myCommand.ExecuteReader())
						{
							if (myReader.Read())
							{
								user = FillDataRecord(myReader);
							}
							myReader.Close();
						}
					}
					myConnection.Close();
				}
				return user;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		
		public static User GetItem(string username)
		{
			try
			{

				User user = null;
				using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
				{
					using (SqlCommand myCommand = new SqlCommand("spUserGetList", myConnection))
					{
						myCommand.CommandType = CommandType.StoredProcedure;
						myCommand.Parameters.AddWithValue("@username", username);
						myCommand.Parameters.AddWithValue("@password", DBNull.Value);

						myConnection.Open();
						using (SqlDataReader myReader = myCommand.ExecuteReader())
						{
							if (myReader.Read())
							{
								user = FillDataRecord(myReader);
							}
							myReader.Close();
						}
					}
					myConnection.Close();
				}
				return user;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		
		public static User GetItem(string username,string password)
		{
			User user = null;
			using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
			{
				using (SqlCommand myCommand = new SqlCommand("spUserGetList", myConnection))
				{
					myCommand.CommandType = CommandType.StoredProcedure;
					myCommand.Parameters.AddWithValue("@username", username);
					myCommand.Parameters.AddWithValue("@password", password);

					myConnection.Open();
					using (SqlDataReader myReader = myCommand.ExecuteReader())
					{
						if (myReader.Read())
						{
							user = FillDataRecord(myReader);
						}
						myReader.Close();
					}
				}
				myConnection.Close();
			}
			return user;
		}

		public static UserCollection GetList()
		{
			UserCollection tempList = new UserCollection();
			using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
			{
				using (SqlCommand myCommand = new SqlCommand("spGetUserList", myConnection))
				{
					myConnection.Open();
					using (SqlDataReader myReader = myCommand.ExecuteReader())
					{
						if (myReader.HasRows)
						{
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

		public static UserCollection GetPendingRegistrations()
		{
			UserCollection tempList = new UserCollection();
			using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
			{
				using (SqlCommand myCommand = new SqlCommand("spPendingRegistrationGetList", myConnection))
				{
				
					myConnection.Open();
					using (SqlDataReader myReader = myCommand.ExecuteReader())
					{
						if (myReader.HasRows)
						{
							tempList = new UserCollection();
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
	   

		
		
		private static User FillDataRecord(IDataRecord dataRecord)
		{
			User user = new User();
			int ordValue;

			user.UserName = dataRecord.GetString(dataRecord.GetOrdinal("UserName"));
			//user.Password = dataRecord.GetString(dataRecord.GetOrdinal("Password"));
			user.LoginAttempts = dataRecord.GetInt32(dataRecord.GetOrdinal("LoginAttempts"));
			user.IsLocked = dataRecord.GetBoolean(dataRecord.GetOrdinal("IsLocked"));
			user.Role = dataRecord.GetInt32(dataRecord.GetOrdinal("RoleId"));

			user.Email = dataRecord.GetString(dataRecord.GetOrdinal("Email"));

			ordValue = dataRecord.GetOrdinal("ChannelCode");
			user.Channel = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("RedemptionPoints");
			user.RedemptionPoints = dataRecord.IsDBNull(ordValue) ? 0 : dataRecord.GetInt32(ordValue);
 
			user.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("UserId"));
			
			user.FirstName = dataRecord.GetString(dataRecord.GetOrdinal("FirstName"));

			ordValue = dataRecord.GetOrdinal("MiddleName");
			user.MiddleName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			user.LastName = dataRecord.GetString(dataRecord.GetOrdinal("LastName"));

			ordValue = dataRecord.GetOrdinal("DistrictCode");
			user.DistrictCode = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			//ordValue = dataRecord.GetOrdinal("AreaCode");
			//user.AreaCode = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("BranchCode");
			user.BranchCode = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("RegionCode");
			user.RegionCode = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			//user.ReferrorCode = dataRecord.GetInt32(dataRecord.GetOrdinal("ReferrorCode"));
			ordValue = dataRecord.GetOrdinal("ReferrorCode");
			user.ReferrorCode = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);
			ordValue = dataRecord.GetOrdinal("ReferrorName");
			user.ReferrorName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("RoleName");
			user.RoleName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("ChannelName");
			user.ChannelName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("RegionName");
			user.RegionName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			//ordValue = dataRecord.GetOrdinal("AreaName");
			//user.AreaName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("DistrictName");
			user.DistrictName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			ordValue = dataRecord.GetOrdinal("BranchName");
			user.BranchName = dataRecord.IsDBNull(ordValue) ? "" : dataRecord.GetString(ordValue);

			return user;
		}

		public static bool Delete(int userId, string deletedBy)
		{
			return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", userId), new SqlParameter("@deletedBy", deletedBy) }, "spUserDelete");
		}

		public static bool IsUserExists(string userName)
		{
			bool isUser = false;
            SqlDataReader reader;
            string spName = "spUserExisting";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            int userCount = 0;
			SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@userName", userName)};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userCount = reader["UserCount"].AsInt();
                }

                if (userCount > 0)
                {
                    isUser = true;
                }
            }

            reader.Close();
            reader.Dispose();
            reader = null;

			return isUser;
		}
		
	}
}