using CRS.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CRS.Dal
{
    public class BranchManagerDB
    {
        public static BranchManager GetItem(string code)
        {
            BranchManager myContactPerson = null;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spBranchManagerGetSingle", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@code", code);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            myContactPerson = FillDataRecord(myReader);
                        }
                        myReader.Close();
                    }
                }
                myConnection.Close();
            }
            return myContactPerson;
        }

        public static BranchManagerCollection GetList()
        {
            BranchManagerCollection tempList = new BranchManagerCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spBranchManagerGetList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new BranchManagerCollection();
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


        public static BranchManagerCollection GetList(BranchManagerCriteria branchManagerCriteria)
        {
            BranchManagerCollection tempList = new BranchManagerCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spBranchManagerSearchList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(branchManagerCriteria.FirstName))
                    {
                        myCommand.Parameters.AddWithValue("@firstName", branchManagerCriteria.FirstName);
                    }
                    if (!string.IsNullOrEmpty(branchManagerCriteria.LastName))
                    {
                        myCommand.Parameters.AddWithValue("@lastName", branchManagerCriteria.LastName);
                    }
                    

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new BranchManagerCollection();
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
        
        private static BranchManager FillDataRecord(IDataRecord dataRecord)
		{
			BranchManager branchManager = new BranchManager();

            
            branchManager.Code = dataRecord.GetString(dataRecord.GetOrdinal("BranchManagerCode"));
			branchManager.FirstName = dataRecord.GetString(dataRecord.GetOrdinal("BranchManagerFirstName"));
		    branchManager.MiddleName = dataRecord.GetString(dataRecord.GetOrdinal("BranchManagerMiddleName"));
            branchManager.LastName = dataRecord.GetString(dataRecord.GetOrdinal("BranchManagerLastName"));
			
			return branchManager;
		}

        public static int Save(BranchManager branchManager)
        {
            if (!branchManager.Validate())
            {
                throw new Exception("Can't save a BranchManager in an Invalid state.");
            }

            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spBranchManagerSave", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@firstName", branchManager.FirstName);
                    myCommand.Parameters.AddWithValue("@lastName",  branchManager.LastName);
                    if (string.IsNullOrEmpty(branchManager.MiddleName))
                    {
                        myCommand.Parameters.AddWithValue("@middleName", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@middleName", branchManager.MiddleName);
                    }
                    myCommand.Parameters.AddWithValue("@dateOfBirth", branchManager.DateOfBirth);
                    
                    

                    myConnection.Open();
                    int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
                    if (numberOfRecordsAffected == 0)
                    {
                        throw new Exception("Can't update contact person");
                    }

                    result = Helpers.GetBusinessBaseId(myCommand);
                }
                myConnection.Close();
            }
            return result;
        }
    }
}