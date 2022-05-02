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
    public class RoleDB
    {


        public static RoleCollection GetList()
        {
            RoleCollection tempList = new RoleCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRoleGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@ChannelCode", DBNull.Value);
                    
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RoleCollection();
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

        //public static int Delete(int id)
        //{
        //    int result = 0;
        //    try
        //    {

        //        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
        //        {
        //            using (SqlCommand myCommand = new SqlCommand("spRoleDelete", myConnection))
        //            {
        //                myCommand.CommandType = CommandType.StoredProcedure;
        //                myCommand.Parameters.AddWithValue("@id", id);
        //                myConnection.Open();
        //                int retvalue = myCommand.ExecuteNonQuery();
        //                if (retvalue == 1)  //deleted 1 record?
        //                {
        //                    result = 0; //success
        //                }
        //                else
        //                {
        //                    result = -1; //unsuccessful 
        //                };
        //            }
        //            myConnection.Close();
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -1;
        //        throw;
        //    }

        //}
        public static bool Delete(int roleId, string deletedBy)
        {
            return DBSqlHelper.ExecuteCUD(new[] { new SqlParameter("@id", roleId), new SqlParameter("@deletedBy", deletedBy) }, "spRoleDelete");
        }
        
        public static Role GetItem(int Id)
        {
            Role tempItem = new Role();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRoleGetItem", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@id", Id);
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
        
        public static int Save(Role role)
        {
            try
            {
                //if (!role.Validate())
                //{
                //    throw new Exception("Can't save a Role in an Invalid state.");
                //}

                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRoleSave", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@id", role.ID);
                        myCommand.Parameters.AddWithValue("@Name", role.Name);
                        myCommand.Parameters.AddWithValue("@userName", (role.ID > 0 ? role.ModifiedBy : role.CreatedBy));
                        
                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update Role");
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
        
        
        public static RoleCollection GetList(string channelcode)
        {
            RoleCollection tempList = new RoleCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRoleGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@ChannelCode", channelcode);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RoleCollection();
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

        
        private static Role FillDataRecord(IDataRecord dataRecord)
		{
			Role role = new Role();

            
            role.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("RoleId"));
			role.Name= dataRecord.GetString(dataRecord.GetOrdinal("RoleName"));
            //role.CreatedBy = dataRecord.GetString(dataRecord.GetOrdinal("CreatedBy"));
            //role.DateCreated = dataRecord.GetDateTime(dataRecord.GetOrdinal("CreatedDate"));

            
			return role;
		}

        public static bool IsRoleExists(string roleName)
        {
            bool isRole = false;
            SqlDataReader reader;
            string spName = "spRoleExisting";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            int roleCount = 0;
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@roleName", roleName)};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    roleCount = reader["RoleCount"].AsInt();
                }

                if (roleCount > 0)
                {
                    isRole = true;
                }
            }

            reader = null;

            return isRole;
        }


        #region Get Role List
        public static IEnumerable<Role> GetRoleList()
        {
            string spName = "spRoleList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<Role> list = new List<Role>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Role dto = new Role();
                    dto.ID = reader["RoleId"].AsInt();
                    dto.Name = reader["RoleName"].ToString();
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