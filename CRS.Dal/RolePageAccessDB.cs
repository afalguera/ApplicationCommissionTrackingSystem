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
    public class RolePageAccessDB
    {


        public static RolePageAccessCollection GetList(int roleid)
        {
            RolePageAccessCollection tempList = new RolePageAccessCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRolePageAccessGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@RoleId", roleid);
                    
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RolePageAccessCollection();
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

        public static int Delete(int id)
        {
            int result = 0;
            try
            {

                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRolePageAccessDelete", myConnection))
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
        
        public static RolePageAccess GetItem(int Id)
        {
            RolePageAccess tempItem = new RolePageAccess();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spRolePageAccessGetItem", myConnection))
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
        
        public static int Save(RolePageAccess rolepageacc)
        {
            try
            {
                if (!rolepageacc.Validate())
                {
                    throw new Exception("Can't save a Role Page Access in an Invalid state.");
                }

                int result = 0;
                using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("spRolePageAccessSave", myConnection))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;

                        myCommand.Parameters.AddWithValue("@Id", rolepageacc.ID);
                        myCommand.Parameters.AddWithValue("@PageId", rolepageacc.PageId);
                        myCommand.Parameters.AddWithValue("@RoleId", rolepageacc.RoleId);
                       
                        myCommand.Parameters.AddWithValue("@CanAdd", rolepageacc.CanAdd);
                        myCommand.Parameters.AddWithValue("@CanEdit", rolepageacc.CanEdit);
                        myCommand.Parameters.AddWithValue("@CanDelete", rolepageacc.CanDelete);
                        myCommand.Parameters.AddWithValue("@CanView", rolepageacc.CanView);
                        myCommand.Parameters.AddWithValue("@CanPrint", rolepageacc.CanPrint);
                        myCommand.Parameters.AddWithValue("@userBy", rolepageacc.ID > 0 ? rolepageacc.ModifiedBy : rolepageacc.CreatedBy);

                       
                        myConnection.Open();
                        int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                        if (numberOfRecordsAffected == 0)
                        {
                            throw new Exception("Can't update Role Page Access");
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
        
        
       private static RolePageAccess FillDataRecord(IDataRecord dataRecord)
		{
			RolePageAccess rolepageacc = new RolePageAccess();

            
            rolepageacc.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("RolePageAccessId"));
			rolepageacc.RoleId = dataRecord.GetInt32(dataRecord.GetOrdinal("RoleId"));
            rolepageacc.PageId = dataRecord.GetInt32(dataRecord.GetOrdinal("PageId"));
            rolepageacc.CanView = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanView"));
            rolepageacc.CanEdit = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanEdit"));
            rolepageacc.CanDelete = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanDelete"));
            rolepageacc.CanPrint = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanPrint"));
            rolepageacc.CanAdd = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanAdd"));
            rolepageacc.PageTitle = dataRecord.GetString(dataRecord.GetOrdinal("PageTitle"));
       	    return rolepageacc;
		}

        
    }
}