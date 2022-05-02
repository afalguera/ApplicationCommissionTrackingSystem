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
    public class UserPageAccessDB
    {
        
        public static UserPageAccessCollection GetList(string userName)
        {
            UserPageAccessCollection tempList = new UserPageAccessCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spGetUserPageAccess", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@UserName", userName);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new UserPageAccessCollection();
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

        
        private static UserPageAccess FillDataRecord(IDataRecord dataRecord)
		{
            UserPageAccess userPageAccess = new UserPageAccess();
            userPageAccess.PageId = dataRecord.GetInt32(dataRecord.GetOrdinal("PageId"));
            userPageAccess.PageName = dataRecord.GetString(dataRecord.GetOrdinal("PageName"));
            userPageAccess.ModuleId = dataRecord.GetInt32(dataRecord.GetOrdinal("ModuleId"));
			userPageAccess.PageTitle = dataRecord.GetString(dataRecord.GetOrdinal("PageTitle"));
            userPageAccess.CanAdd = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanAdd"));
            userPageAccess.CanView = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanView"));
            userPageAccess.CanDelete = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanDelete"));
            userPageAccess.CanEdit = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanEdit"));
            userPageAccess.CanPrint = dataRecord.GetBoolean(dataRecord.GetOrdinal("CanPrint"));
            userPageAccess.PageTitle = dataRecord.GetString(dataRecord.GetOrdinal("PageTitle"));
            
            int ordValue = dataRecord.GetOrdinal("PageType");
            userPageAccess.PageType = dataRecord.IsDBNull(ordValue)  ? "" : dataRecord.GetString(ordValue) ;
            
            return userPageAccess;
		}

        
    }
}