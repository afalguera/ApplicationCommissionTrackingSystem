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
    public class CPAReportManagerDB
    {


        public static IEnumerable<CPAReport> GetCPAReport(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportCollection tempList = new CPAReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPA(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<CPAReportInputsAppPCH> GetCPAReportInputsAppPCH(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportInputsAppPCHCollection tempList = new CPAReportInputsAppPCHCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_InputsAppPCH", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);




                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportInputsAppPCHCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPAInputsAppPCH(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<CPAReportInputsAppSimul> GetCPAReportInputsAppSimul(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportInputsAppSimulCollection tempList = new CPAReportInputsAppSimulCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_InputsAppSimul", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);





                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportInputsAppSimulCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPAInputsAppSimul(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<CPAReportInputsCommissions> GetCPAReportInputsCommissions(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportInputsCommissionsCollection tempList = new CPAReportInputsCommissionsCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_InputsCommissions", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);




                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportInputsCommissionsCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPAInputsCommissions(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<CPAReportInputsRejects> GetCPAReportInputsRejects(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportInputsRejectsCollection tempList = new CPAReportInputsRejectsCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_InputsRejects", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);




                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportInputsRejectsCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPAInputsRejects(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<CPAReportSummaryActualApprovalRate> GetCPAReportSummaryActualApprovalRate(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportSummaryActualApprovalRateCollection tempList = new CPAReportSummaryActualApprovalRateCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_SummaryActualApprovalRate", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportSummaryActualApprovalRateCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPASummaryActualApprovalRate(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<CPAReportSummaryApprovalRate> GetCPAReportSummaryApprovalRate(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportSummaryApprovalRateCollection tempList = new CPAReportSummaryApprovalRateCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_SummaryApprovalRate", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);




                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportSummaryApprovalRateCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPASummaryApprovalRate(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<CPAReportSummaryApprovals> GetCPAReportSummaryApprovals(int Year, Boolean IncludeExceptions, string Channel)
        {
            CPAReportSummaryApprovalsCollection tempList = new CPAReportSummaryApprovalsCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_SummaryApprovals", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    myCommand.Parameters.AddWithValue("@IncludeExceptions", IncludeExceptions);
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportSummaryApprovalsCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPASummaryApprovals(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<CPAReportSummaryCommission> GetCPAReportSummaryCommission(int Year)
        {
            CPAReportSummaryCommissionCollection tempList = new CPAReportSummaryCommissionCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spCostPerApplicationRpt_SummaryCommission", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    


                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new CPAReportSummaryCommissionCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordCPASummaryCommission(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        
        private static CPAReport FillDataRecordCPA(IDataRecord dataRecord)
        {
            CPAReport rpt = new CPAReport();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            rpt.Commission = dataRecord.GetInt32(dataRecord.GetOrdinal("Commission"));
            rpt.CostPerApplication = dataRecord.GetDecimal(dataRecord.GetOrdinal("CostPerApplication"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            return rpt;
        }

        private static CPAReportInputsAppPCH FillDataRecordCPAInputsAppPCH(IDataRecord dataRecord)
        {
            CPAReportInputsAppPCH rpt = new CPAReportInputsAppPCH();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.CostPerApplication = 300;
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            return rpt;
        }


        private static CPAReportInputsAppSimul FillDataRecordCPAInputsAppSimul(IDataRecord dataRecord)
        {
            CPAReportInputsAppSimul rpt = new CPAReportInputsAppSimul();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            return rpt;
        }

        private static CPAReportInputsCommissions FillDataRecordCPAInputsCommissions(IDataRecord dataRecord)
        {
            CPAReportInputsCommissions rpt = new CPAReportInputsCommissions();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            rpt.TierValue = dataRecord.GetDecimal(dataRecord.GetOrdinal("TierValue"));
            rpt.CommValue = dataRecord.GetDecimal(dataRecord.GetOrdinal("CommValue"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            return rpt;
        }

        private static CPAReportInputsRejects FillDataRecordCPAInputsRejects(IDataRecord dataRecord)
        {
            CPAReportInputsRejects rpt = new CPAReportInputsRejects();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            return rpt;
        }

        private static CPAReportSummaryActualApprovalRate FillDataRecordCPASummaryActualApprovalRate(IDataRecord dataRecord)
        {
            CPAReportSummaryActualApprovalRate rpt = new CPAReportSummaryActualApprovalRate();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCountApprovedPCH = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCountApprovedPCH"));
            rpt.GroupCountRejected = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCountRejected"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            return rpt;
        }

        private static CPAReportSummaryApprovalRate FillDataRecordCPASummaryApprovalRate(IDataRecord dataRecord)
        {
            CPAReportSummaryApprovalRate rpt = new CPAReportSummaryApprovalRate();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCountApprovedPCH = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCountApprovedPCH"));
            rpt.GroupCountRejected = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCountRejected"));
            rpt.GroupCountApprovedSimul = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCountApprovedSimul"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            return rpt;
        }

        private static CPAReportSummaryApprovals FillDataRecordCPASummaryApprovals(IDataRecord dataRecord)
        {
            CPAReportSummaryApprovals rpt = new CPAReportSummaryApprovals();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
            rpt.SalesManager = dataRecord.GetString(dataRecord.GetOrdinal("SalesManager"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            return rpt;
        }

        private static CPAReportSummaryCommission FillDataRecordCPASummaryCommission(IDataRecord dataRecord)
        {
            CPAReportSummaryCommission rpt = new CPAReportSummaryCommission();

            rpt.ChannelCode = dataRecord.GetString(dataRecord.GetOrdinal("ChannelCode"));
            rpt.Commission = dataRecord.GetInt32(dataRecord.GetOrdinal("Commission"));
            rpt.CostPerApplication = dataRecord.GetDecimal(dataRecord.GetOrdinal("CostPerApplication"));
            rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
            rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
            rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
            return rpt;
        }




    }     
  
}