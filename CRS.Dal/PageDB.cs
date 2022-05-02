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
    public class PageDB
    {


        public static PageCollection GetList(int RoleId)
        {
            PageCollection tempList = new PageCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spPageGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@RoleId", RoleId);
                    
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new PageCollection();
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

        
        
        private static Page FillDataRecord(IDataRecord dataRecord)
		{
			Page page = new Page();

            page.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("PageId"));
			page.Name= dataRecord.GetString(dataRecord.GetOrdinal("PageName"));
            page.Title = dataRecord.GetString(dataRecord.GetOrdinal("PageTitle"));
            
            
			return page;
		}

        
    }
}