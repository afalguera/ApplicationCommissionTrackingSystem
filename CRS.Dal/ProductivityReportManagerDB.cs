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
    public class ProductivityReportManagerDB
    {
        private static List<MTDYTDSummary> mTDYTDSummary;
        private static List<MTDYTDDetails> mTDYTDDetails;
        private static List<MTDYTDIntraYGC> mTDYTDIntraYGC;
        private static List<ProductivityGraph> productivityGraph;

        public static IEnumerable<IncompleteDetailReport> GetIncompleteDetail(DateTime startdate, DateTime enddate)
        {
            IncompleteDetailReportCollection tempList = new IncompleteDetailReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteDetailRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteDetailReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteDetail(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        
        public static IEnumerable<OutrightRejectedReport> GetOutrightRejected(DateTime startdate, DateTime enddate)
        {
            OutrightRejectedReportCollection tempList = new OutrightRejectedReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spOutrightRejected", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new OutrightRejectedReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordOutrightRejected(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<ProductivityReportDetail> GetProductivityDetail(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportDetailCollection tempList = new ProductivityReportDetailCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivityDetailRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);


                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportDetailCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordDetail(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        
        //public static IEnumerable<DailyProdSummary> GetDailyProdSummary(int Year,ref IEnumerable<DailyProdSummaryMTD> MTDSummary, ref IEnumerable<DailyProdSummaryInflowsBookingsChart> IBChart,
        //        ref IEnumerable<DailyProdSummaryBookingsChart> BookingsChart, ref IEnumerable<DailyProdSummaryInflowsChart> InflowsChart,
        //        ref IEnumerable<DailyProdSummaryMTDIntraYGC> MTDIntraYGC, ref IEnumerable<DailyProdSummaryYTDIntraYGC> YTDIntraYGC,
        //        string channelCode, string regionCode, string districtCode, string branchCode, string referrorCode)
        public static void GetDailyProdSummary(int Year, string channelCode, string regionCode, string districtCode,
                                                                       string branchCode, string referrorCode)
        {
            mTDYTDSummary = null;
            mTDYTDDetails = null;
            mTDYTDIntraYGC = null;
            productivityGraph = null;

            //List<MTDYTDSummary> tempList = new List<MTDYTDSummary>();

            //mTDYTDSummary = FillDataRecordDailyProdSummaryTestFields();
            //mTDYTDDetails = FillDataRecordDailyProdSummaryTestFields2();
            //mTDYTDIntraYGC = FillDataRecordDailyProdSummaryTestFields3();
            //productivityGraph = GetProductivityGraphValues();

            //DailyProdSummaryCollection tempList = new DailyProdSummaryCollection();
            //DailyProdSummaryMTDCollection tempListMTD = new DailyProdSummaryMTDCollection();
            //DailyProdSummaryInflowsBookingsChartCollection tempListInflowsBookingsChart = new DailyProdSummaryInflowsBookingsChartCollection();
            //DailyProdSummaryBookingsChartCollection tempListBookingsChart = new DailyProdSummaryBookingsChartCollection();
            //DailyProdSummaryInflowsChartCollection tempListInflowsChart = new DailyProdSummaryInflowsChartCollection();
            //DailyProdSummaryMTDIntraYGCCollection tempListMTDIntraYGC = new DailyProdSummaryMTDIntraYGCCollection();
            //DailyProdSummaryYTDIntraYGCCollection tempListYTDIntraYGC = new DailyProdSummaryYTDIntraYGCCollection();  

            //var prodCollection = new ProductivitySummaryCollection();
            //prodCollection.MTDYTDSummary = FillDataRecordDailyProdSummaryTestFields();
            //prodCollection.MTDYTDDetails = FillDataRecordDailyProdSummaryTestFields2();
            //prodCollection.MTDYTDIntraYGC = FillDataRecordDailyProdSummaryTestFields3();
            //prodCollection.ProductivityGraph = GetProductivityGraph();
            //return prodCollection;        

            //List<ProductivitySummaryCollection> prodCollection = new List<ProductivitySummaryCollection>();
            //prodCollection.Add(new ProductivitySummaryCollection
            //{
            //    MTDYTDSummary = FillDataRecordDailyProdSummaryTestFields(),
            //    MTDYTDDetails = FillDataRecordDailyProdSummaryTestFields2(),
            //    MTDYTDIntraYGC = FillDataRecordDailyProdSummaryTestFields3(),
            //    ProductivityGraph = GetProductivityGraph()
            //});

            //return prodCollection;        
            
          //      = FillDataRecordDailyProdSummaryTestFields() as IEnumerable<ProductivitySummary>;
       
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spDailyProductivitySummaryRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Year", Year);
                    if (channelCode != null && channelCode != "" )
                    myCommand.Parameters.AddWithValue("@channelCode", channelCode);
                    myCommand.Parameters.AddWithValue("@regionCode", regionCode);
                    myCommand.Parameters.AddWithValue("@districtCode", districtCode);
                    myCommand.Parameters.AddWithValue("@branchCode", branchCode);
                    myCommand.Parameters.AddWithValue("@referrorCode", referrorCode);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        //Summary
                        if (myReader.HasRows)
                        {
                            var tempListMTDYTDSummary = new List<MTDYTDSummary>();
                            while (myReader.Read())
                            {
                                tempListMTDYTDSummary.Add(FillDataRecordMTDYTDSummary(myReader));
                            }
                            mTDYTDSummary = tempListMTDYTDSummary;
                        }


                        //Graph
                        myReader.NextResult();
                        if (myReader.HasRows)
                        {
                            var tempListProductivityGraph = new List<ProductivityGraph>();
                            while (myReader.Read())
                            {
                                tempListProductivityGraph.Add(GetProductivityGraphValues(myReader));
                            }
                            productivityGraph = tempListProductivityGraph;
                        }

                        //Detail
                        myReader.NextResult();
                        if (myReader.HasRows)
                        {
                            var tempListMTDYTDDetails = new List<MTDYTDDetails>();
                            while (myReader.Read())
                            {
                                tempListMTDYTDDetails.Add(FillDataRecordMTDYTDDetails(myReader));
                            }
                            mTDYTDDetails = tempListMTDYTDDetails;
                        }

                        //IntraYGC
                        myReader.NextResult();
                        if (myReader.HasRows)
                        {
                            var tempListMTDYTDIntraYGC = new List<MTDYTDIntraYGC>();
                            while (myReader.Read())
                            {
                                tempListMTDYTDIntraYGC.Add(FillDataRecordMTDYTDIntraYGC(myReader));
                            }
                            mTDYTDIntraYGC = tempListMTDYTDIntraYGC;
                        }

                        //close datareader
                        myReader.Close();
                    }
                }
            }
        }
        
        public static IEnumerable<ProductivityReportYearly> GetProductivityYearly(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportYearlyCollection tempList = new ProductivityReportYearlyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptYearly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@DateFrom", startdate);
                    myCommand.Parameters.AddWithValue("@DateTo", enddate);


                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportYearlyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordYearly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<IncompleteQuarterlyReport> GetIncompleteQuarterly(DateTime startdate, DateTime enddate)
        {
            IncompleteQuarterlyReportCollection tempList = new IncompleteQuarterlyReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteQuarterlyRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                  
                    myCommand.Parameters.AddWithValue("@DateFrom", startdate);
                    myCommand.Parameters.AddWithValue("@DateTo", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteQuarterlyReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteQuarterly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<ProductivityReportQuarterly> GetProductivityQuarterly(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportQuarterlyCollection tempList = new ProductivityReportQuarterlyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptQuarterly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@DateFrom", startdate);
                    myCommand.Parameters.AddWithValue("@DateTo", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportQuarterlyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordQuarterly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<ProductivityReportDaily> GetProductivityDaily(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportDailyCollection tempList = new ProductivityReportDailyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptDaily", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportDailyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordDaily(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<IncompleteDailyReport> GetIncompleteDaily(DateTime startdate, DateTime enddate)
        {
            IncompleteDailyReportCollection tempList = new IncompleteDailyReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteDailyRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteDailyReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteDaily(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }
        
        
        public static IEnumerable<IncompleteMonthlyReport> GetIncompleteMonthly(DateTime startdate, DateTime enddate)
        {
            IncompleteMonthlyReportCollection tempList = new IncompleteMonthlyReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteMonthlyRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteMonthlyReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteMonthly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

       

        public static IEnumerable<ProductivityReportWeekly> GetProductivityWeekly(List<ReportWeek> reportweek, 
                                                                              string StatusCode = "Inflows",string NewStatus="", string Channel="")

        
        {
            ProductivityReportWeeklyCollection tempList = new  ProductivityReportWeeklyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptWeekly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    int ctr=1;
                    foreach (ReportWeek rpt in reportweek)
                    {
                        if (rpt.StartWeek == null)
                        {
                            myCommand.Parameters.Add("@Week" + ctr + "_Start", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@Week" + ctr.ToString() + "_Start", rpt.StartWeek);
                        }

                        if (rpt.EndWeek == null)
                        {
                            myCommand.Parameters.Add("@Week" + ctr.ToString() + "_End", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@Week" + ctr.ToString() + "_End", rpt.EndWeek);
                        }
                        ctr++;
                    }
                    
                    
                    myCommand.Parameters.AddWithValue("@StatusCode", StatusCode);
                    myCommand.Parameters.AddWithValue("@NewStatus", "");
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);
                    
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportWeeklyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordWeekly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<ProductivityReportMonthly> GetProductivityMonthly(int Year,
                                                                              string StatusCode = "Inflows", string Channel = "")
        {
            ProductivityReportMonthlyCollection tempList = new ProductivityReportMonthlyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptMonthly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    if (StatusCode == "ALL") StatusCode = null;
                     myCommand.Parameters.Add("@Year" , SqlDbType.Int).Value = Year;
                    
                    myCommand.Parameters.Add("@StatusCode", SqlDbType.VarChar).Value = StatusCode;
                    
                    if (Channel == "ALL") Channel = null; 
                    myCommand.Parameters.Add("@ChannelParam", SqlDbType.VarChar).Value = Channel;
                     

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportMonthlyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordMonthly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<RejectedSummaryReportMonthly> GetRejectedSummaryMonthly(int Year,
                                                                              string StatusCode = "Inflows", string Channel = "")
        {
            RejectedSummaryReportMonthlyCollection tempList = new RejectedSummaryReportMonthlyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRejectedRptMonthly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    if (StatusCode == "ALL") StatusCode = null;
                    myCommand.Parameters.Add("@Year", SqlDbType.Int).Value = Year;

                    myCommand.Parameters.Add("@StatusCode", SqlDbType.VarChar).Value = StatusCode;

                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.Add("@ChannelParam", SqlDbType.VarChar).Value = Channel;


                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RejectedSummaryReportMonthlyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordRejectedSummaryMonthly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<RejectedSummaryReportWeekly> GetRejectedSummaryWeekly(List<ReportWeek> reportweek,
                                                                              string StatusCode = "Inflows", string NewStatus = "", string Channel = "")
        {
            RejectedSummaryReportWeeklyCollection tempList = new RejectedSummaryReportWeeklyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRejectedRptWeekly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    int ctr = 1;
                    foreach (ReportWeek rpt in reportweek)
                    {
                        if (rpt.StartWeek == null)
                        {
                            myCommand.Parameters.Add("@Week" + ctr + "_Start", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@Week" + ctr.ToString() + "_Start", rpt.StartWeek);
                        }

                        if (rpt.EndWeek == null)
                        {
                            myCommand.Parameters.Add("@Week" + ctr.ToString() + "_End", SqlDbType.DateTime).Value = DBNull.Value;
                        }
                        else
                        {
                            myCommand.Parameters.AddWithValue("@Week" + ctr.ToString() + "_End", rpt.EndWeek);
                        }
                        ctr++;
                    }


                    myCommand.Parameters.AddWithValue("@StatusCode", StatusCode);
                    myCommand.Parameters.AddWithValue("@NewStatus", "");
                    if (Channel == "ALL") Channel = null;
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new RejectedSummaryReportWeeklyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordRejectedSummaryWeekly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }
       
        
        public static IEnumerable<IncompleteWeeklyReport> GetIncompleteWeekly(DateTime? Week1_Start, DateTime? Week1_End,
                                                                              DateTime? Week2_Start, DateTime? Week2_End,
                                                                              DateTime? Week3_Start, DateTime? Week3_End,
                                                                              DateTime? Week4_Start, DateTime? Week4_End,
                                                                              DateTime? Week5_Start, DateTime? Week5_End
            
        )
        {
            IncompleteWeeklyReportCollection tempList = new IncompleteWeeklyReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteWeeklyRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;

                    if (Week1_Start == null)
                    {
                        myCommand.Parameters.Add("@Week1_Start", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week1_Start", Week1_Start);
                    }

                    if (Week1_End == null)
                    {
                        myCommand.Parameters.Add("@Week1_End", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week1_End", Week1_End);
                    }

                    if (Week2_Start == null)
                    {
                        myCommand.Parameters.Add("@Week2_Start", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week2_Start", Week2_Start);
                    }

                    if (Week2_End == null)
                    {
                        myCommand.Parameters.Add("@Week2_End", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week2_End", Week2_End);
                    }


                    if (Week3_Start == null)
                    {
                        myCommand.Parameters.Add("@Week3_Start", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week3_Start", Week3_Start);
                    }

                    if (Week3_End == null)
                    {
                        myCommand.Parameters.Add("@Week3_End", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week3_End", Week3_End);
                    }

                    if (Week4_Start == null)
                    {
                        myCommand.Parameters.Add("@Week4_Start", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week4_Start", Week4_Start);
                    }

                    if (Week4_End == null)
                    {
                        myCommand.Parameters.Add("@Week4_End", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week4_End", Week4_End);
                    }


                    if (Week5_Start == null)
                    {
                        myCommand.Parameters.Add("@Week5_Start", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week5_Start", Week5_Start);
                    }


                    if (Week5_End == null)
                    {
                        myCommand.Parameters.Add("@Week5_End", SqlDbType.DateTime).Value = DBNull.Value;
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@Week5_End", Week5_End);
                    }
 
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteWeeklyReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteWeekly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }


        public static IEnumerable<ProductivityReportWeekly> GetProductivityWeekly(DateTime startdate, DateTime enddate, string statuscode, string Channel)
        {
            ProductivityReportWeeklyCollection tempList = new ProductivityReportWeeklyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptWeekly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);
                    if (Channel == "ALL") Channel = null; 
                    myCommand.Parameters.AddWithValue("@ChannelParam", Channel);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportWeeklyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordWeekly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }
        
        public static IEnumerable<ProductivityReportMonthly> GetProductivityMonthly(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportMonthlyCollection tempList = new ProductivityReportMonthlyCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptMonthly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@StartDate", startdate);
                    myCommand.Parameters.AddWithValue("@EndDate", enddate);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportMonthlyCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordMonthly(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public static IEnumerable<ProductivityReport> GetProductivityYTD()
        {
            ProductivityReportCollection tempList = new ProductivityReportCollection() ;
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptMonthly", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", "APPROVED");
                    myCommand.Parameters.AddWithValue("@Year", "2013");
                    
                   
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportCollection();
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

        public static IEnumerable<IncompleteOthersReport> GetIncompleteOthers(DateTime startdate, DateTime enddate)
        {
            IncompleteOthersReportCollection tempList = new IncompleteOthersReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spIncompleteOthersRpt", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@DateFrom", startdate);
                    myCommand.Parameters.AddWithValue("@DateTo", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new IncompleteOthersReportCollection();
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecordIncompleteOthers(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }
        
        public static IEnumerable<ProductivityReport> GetProductivityOthers(DateTime startdate, DateTime enddate, string statuscode)
        {
            ProductivityReportCollection tempList = new ProductivityReportCollection();
            using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("spProductivitySummaryRptOthers", myConnection))
                {

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@StatusCode", statuscode);
                    myCommand.Parameters.AddWithValue("@DateFrom", startdate);
                    myCommand.Parameters.AddWithValue("@DateTo", enddate);



                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            tempList = new ProductivityReportCollection();
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


        private static IncompleteDetailReport FillDataRecordIncompleteDetail(IDataRecord dataRecord)
        {
            IncompleteDetailReport rpt = new IncompleteDetailReport();
            int ordValue;

            rpt.ApplicationNo = dataRecord.GetString(dataRecord.GetOrdinal("ApplicationNo"));
            rpt.EntryDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("EntryDate"));
            rpt.ApplicantFirstName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantFirstName"));
            rpt.ApplicantLastName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantLastName"));
            rpt.ApplicantMiddleName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantMiddleName"));
            rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
            rpt.CardBrand = dataRecord.GetString(dataRecord.GetOrdinal("CardBrand"));

            
            ordValue = dataRecord.GetOrdinal("DateOfBirth");
            rpt.DateOfBirth = dataRecord.IsDBNull(ordValue) ? Convert.ToDateTime("01/01/1100") : dataRecord.GetDateTime(dataRecord.GetOrdinal("DateOfBirth"));
          

            return rpt;
        }
        
        
        private static OutrightRejectedReport FillDataRecordOutrightRejected(IDataRecord dataRecord)
        {
             OutrightRejectedReport rpt = new OutrightRejectedReport();


            rpt.ApplicationNo = dataRecord.GetString(dataRecord.GetOrdinal("ApplicationNo"));
            rpt.CardBrand = dataRecord.GetString(dataRecord.GetOrdinal("CardBrand"));
            rpt.ApplicantFirstName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantFirstName"));
            rpt.ApplicantLastName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantLastName"));
            rpt.ApplicantMiddleName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantMiddleName"));
            rpt.DateOfBirth = dataRecord.GetDateTime(dataRecord.GetOrdinal("DateOfBirth"));
            rpt.StatusDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("StatusDate"));
            return rpt;
        }

        private static IncompleteOthersReport FillDataRecordIncompleteOthers(IDataRecord dataRecord)
        {
            IncompleteOthersReport rpt = new IncompleteOthersReport();


            rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
            rpt.Total = dataRecord.GetInt32(dataRecord.GetOrdinal("Total"));

            return rpt;
        }
        
       private static ProductivityReport FillDataRecord(IDataRecord dataRecord)
		{
            ProductivityReport rpt = new ProductivityReport();


            rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceName"));
            rpt.Total = dataRecord.GetInt32(dataRecord.GetOrdinal("Total"));



            return rpt;
		}

       private static DailyProdSummaryMTD FillDataRecordDailyProdSummaryMTD(IDataRecord dataRecord)
       {
           DailyProdSummaryMTD rpt = new DailyProdSummaryMTD();

           rpt.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ID"));
           rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
           rpt.MTDTargetBooking = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBooking"));
           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
           rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));
           
           /*[ID],ChannelName,MTDTargetBooking,FullYearTarget,FullYearRunRate,[Year],[DisplayMonth],Inflows,Bookings*/

           return rpt;
       }

       private static DailyProdSummaryMTDIntraYGC FillDataRecordDailyProdSummaryMTDIntraYGC(IDataRecord dataRecord)
       {
           DailyProdSummaryMTDIntraYGC rpt = new DailyProdSummaryMTDIntraYGC();

           rpt.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ID"));
           rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
           rpt.MTDTargetBooking = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBooking"));
           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
           rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));

           /*[ID],ChannelName,MTDTargetBooking,FullYearTarget,FullYearRunRate,[Year],[DisplayMonth],Inflows,Bookings*/

           return rpt;
       }

       private static DailyProdSummaryYTDIntraYGC FillDataRecordDailyProdSummaryYTDIntraYGC(IDataRecord dataRecord)
       {


           DailyProdSummaryYTDIntraYGC rpt = new DailyProdSummaryYTDIntraYGC();


           rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));

           rpt.MTDTargetBooking = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBooking"));
           rpt.FullYearTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearTarget"));
           rpt.FullYearRunRate = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearRunRate"));
           rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
           rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));
           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ID"));
           rpt.YTDTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDTarget"));
           return rpt;
       }
       

       private static DailyProdSummaryInflowsBookingsChart FillDataRecordDailyProdSummaryIBChart(IDataRecord dataRecord)
       {
           DailyProdSummaryInflowsBookingsChart rpt = new DailyProdSummaryInflowsBookingsChart();

         
           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.RecordCount = dataRecord.GetInt32(dataRecord.GetOrdinal("RecordCount"));
           rpt.InputType = dataRecord.GetString(dataRecord.GetOrdinal("InputType"));
           rpt.SortMonth = dataRecord.GetInt32(dataRecord.GetOrdinal("SortMonth"));
           /*[ID],ChannelName,MTDTargetBooking,FullYearTarget,FullYearRunRate,[Year],[DisplayMonth],Inflows,Bookings*/

           return rpt;
       }

       private static DailyProdSummaryBookingsChart FillDataRecordDailyProdSummaryBookingsChart(IDataRecord dataRecord)
       {
           DailyProdSummaryBookingsChart rpt = new DailyProdSummaryBookingsChart();


           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));
           rpt.SortMonth = dataRecord.GetInt32(dataRecord.GetOrdinal("SortMonth"));
         

           return rpt;
       }

       private static DailyProdSummaryInflowsChart FillDataRecordDailyProdSummaryInflowsChart(IDataRecord dataRecord)
       {
           DailyProdSummaryInflowsChart rpt = new DailyProdSummaryInflowsChart();


           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
           rpt.SortMonth = dataRecord.GetInt32(dataRecord.GetOrdinal("SortMonth"));


           return rpt;
       }
     
        
       private static DailyProdSummary FillDataRecordDailyProdSummary(IDataRecord dataRecord)
       {
           

           DailyProdSummary rpt = new DailyProdSummary();


           rpt.ChannelName = dataRecord.GetString(dataRecord.GetOrdinal("ChannelName"));
           
           rpt.MTDTargetBooking = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBooking"));
           rpt.FullYearTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearTarget"));
           rpt.FullYearRunRate = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearRunRate"));
           rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
           rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));
           rpt.Year = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
           rpt.ID = dataRecord.GetInt32(dataRecord.GetOrdinal("ID"));
           rpt.YTDTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDTarget"));
           return rpt;
       }

       #region Productivity Summary Overall 
       private static MTDYTDSummary FillDataRecordMTDYTDSummary(IDataRecord dataRecord)
       {
           MTDYTDSummary rpt = new MTDYTDSummary();
           //MTD
           rpt.MTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.MTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDInflows"));
           rpt.MTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDBookings"));
           rpt.MTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBookings"));
           rpt.MTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDBW"));
           rpt.MTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDApprovalRate"));
           //YTD
           rpt.YTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.YTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDInflows"));
           rpt.YTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDBookings"));
           rpt.YTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDTargetBookings"));
           rpt.YTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("YTDBW"));
           rpt.YTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearApprovalRate"));
           rpt.YTDFullYearRateBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearRunRateBookings"));
           rpt.YTDFullYearTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearTarget"));
           rpt.YTDFullYearTargetBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearBW"));

           return rpt;
       } 
       #endregion

       #region Productivity Detail
       private static MTDYTDDetails FillDataRecordMTDYTDDetails(IDataRecord dataRecord)
       {
           MTDYTDDetails rpt = new MTDYTDDetails();
           //MTD
           rpt.MTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.MTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDInflows"));
           rpt.MTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDBookings"));
           rpt.MTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBookings"));
           rpt.MTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDBW"));
           rpt.MTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDApprovalRate"));
           //YTD
           rpt.YTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.YTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDInflows"));
           rpt.YTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDBookings"));
           rpt.YTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDTargetBookings"));
           rpt.YTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("YTDBW"));
           rpt.YTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("YTDApprovalRate"));
           rpt.YTDFullYearRateBookings = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearRunRate"));
           rpt.YTDFullYearTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearTarget"));
           rpt.YTDFullYearTargetBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearBW"));

           return rpt;
       } 
       #endregion

       #region IntraYGC
       private static MTDYTDIntraYGC FillDataRecordMTDYTDIntraYGC(IDataRecord dataRecord)
       {
           MTDYTDIntraYGC rpt = new MTDYTDIntraYGC();
           //MTD
           rpt.MTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.MTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDInflows"));
           rpt.MTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDBookings"));
           rpt.MTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("MTDTargetBookings"));
           rpt.MTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDBW"));
           rpt.MTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("MTDApprovalRate"));
           //YTD
           rpt.YTDSummaryChannelText = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
           rpt.YTDInflows = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDInflows"));
           rpt.YTDBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDBookings"));
           rpt.YTDTargetBookings = dataRecord.GetInt32(dataRecord.GetOrdinal("YTDTargetBookings"));
           rpt.YTDBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("YTDBW"));
           rpt.YTDApprovalRate = dataRecord.GetDecimal(dataRecord.GetOrdinal("YTDApprovalRate"));
           rpt.YTDFullYearRateBookings = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearRunRate"));
           rpt.YTDFullYearTarget = dataRecord.GetInt32(dataRecord.GetOrdinal("FullYearTarget"));
           rpt.YTDFullYearTargetBW = dataRecord.GetDecimal(dataRecord.GetOrdinal("FullYearBW"));

           return rpt;
       }
       #endregion
        

        #region Productivity Graph
        private static ProductivityGraph GetProductivityGraphValues(IDataRecord dataRecord)
        {
            ProductivityGraph rpt = new ProductivityGraph();
            rpt.ProdYear = dataRecord.GetInt32(dataRecord.GetOrdinal("Year"));
            rpt.ProdMonth = dataRecord.GetString(dataRecord.GetOrdinal("MonthName"));
            rpt.SortMonth = dataRecord.GetInt32(dataRecord.GetOrdinal("Month"));
            rpt.Inflows = dataRecord.GetInt32(dataRecord.GetOrdinal("Inflows"));
            rpt.Bookings = dataRecord.GetInt32(dataRecord.GetOrdinal("Bookings"));

            return rpt;
        } 
        #endregion
      
        private static ProductivityReportDetail FillDataRecordDetail(IDataRecord dataRecord)
       {
           ProductivityReportDetail rpt = new ProductivityReportDetail();


           
           rpt.ApplicationNo = dataRecord.GetString(dataRecord.GetOrdinal("ApplicationNo"));
           rpt.ApplicantFirstName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantFirstName"));
           rpt.ApplicantMiddleName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantMiddleName"));
           rpt.ApplicantLastName = dataRecord.GetString(dataRecord.GetOrdinal("ApplicantLastName"));
           rpt.CardBrand = dataRecord.GetString(dataRecord.GetOrdinal("CardBrand"));
           rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceName"));
           
           //rpt.DateOfBirth = dataRecord.GetDateTime(dataRecord.GetOrdinal("DateOfBirth"));
           
           
           rpt.EntryDate = dataRecord.GetDateTime(dataRecord.GetOrdinal("EntryDate"));

           int ordValue;

           ordValue = dataRecord.GetOrdinal("DateOfBirth");
           rpt.DateOfBirth = dataRecord.IsDBNull(ordValue) ? Convert.ToDateTime("01/01/1100") : dataRecord.GetDateTime(dataRecord.GetOrdinal("DateOfBirth"));


           
           return rpt;
       }
        
        private static ProductivityReportYearly FillDataRecordYearly(IDataRecord dataRecord)
       {
           ProductivityReportYearly rpt = new ProductivityReportYearly();


           rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceCode"));
           rpt.GroupYear = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupYear"));
           rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
           

           return rpt;
       }

        private static IncompleteQuarterlyReport FillDataRecordIncompleteQuarterly(IDataRecord dataRecord)
       {
           IncompleteQuarterlyReport rpt = new IncompleteQuarterlyReport();


           rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
           rpt.DateQuarter = dataRecord.GetString(dataRecord.GetOrdinal("DateQuarter"));
           rpt.DateQuarterLong = dataRecord.GetString(dataRecord.GetOrdinal("DateQuarterLong"));
           rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

          

           return rpt;
       }
        
        private static ProductivityReportQuarterly FillDataRecordQuarterly(IDataRecord dataRecord)
       {
           ProductivityReportQuarterly rpt = new ProductivityReportQuarterly();


           rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceName"));
           rpt.DateQuarter= dataRecord.GetString(dataRecord.GetOrdinal("DateQuarter"));
           rpt.DateQuarterLong = dataRecord.GetString(dataRecord.GetOrdinal("DateQuarterLong"));
           rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

           return rpt;
       }

        private static IncompleteWeeklyReport FillDataRecordIncompleteWeekly(IDataRecord dataRecord)
      {
          IncompleteWeeklyReport rpt = new IncompleteWeeklyReport();


          //rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
          //rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
          //rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
         // rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));
          rpt.DisplayWeek = dataRecord.GetString(dataRecord.GetOrdinal("DisplayWeek"));
          rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
          rpt.SortWeek = dataRecord.GetString(dataRecord.GetOrdinal("SortWeek"));
          rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));


          return rpt;
      }

        private static ProductivityReportDaily FillDataRecordDaily(IDataRecord dataRecord)
      {
          ProductivityReportDaily rpt = new ProductivityReportDaily();

          rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceName"));
          rpt.DisplayDay = dataRecord.GetString(dataRecord.GetOrdinal("DisplayDay"));
          rpt.SortDay = dataRecord.GetString(dataRecord.GetOrdinal("SortDay"));
          rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

          return rpt;
      }
 
        private static ProductivityReportWeekly FillDataRecordWeekly(IDataRecord dataRecord)
      {
          ProductivityReportWeekly rpt = new ProductivityReportWeekly();

          rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("Channel"));
          rpt.DisplayWeek = dataRecord.GetString(dataRecord.GetOrdinal("DisplayWeek"));
          rpt.SortWeek = dataRecord.GetString(dataRecord.GetOrdinal("SortWeek"));
          rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

          return rpt;
      }

        private static RejectedSummaryReportWeekly FillDataRecordRejectedSummaryWeekly(IDataRecord dataRecord)
     {
         RejectedSummaryReportWeekly rpt = new RejectedSummaryReportWeekly();

         rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
         rpt.DisplayWeek = dataRecord.GetString(dataRecord.GetOrdinal("DisplayWeek"));
         rpt.SortWeek = dataRecord.GetString(dataRecord.GetOrdinal("SortWeek"));
         rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

         return rpt;
     }
        
        private static IncompleteDailyReport FillDataRecordIncompleteDaily(IDataRecord dataRecord)
     {
         IncompleteDailyReport rpt = new IncompleteDailyReport();


         rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
         rpt.DisplayDay = dataRecord.GetString(dataRecord.GetOrdinal("DisplayDay"));
         rpt.SortDay = dataRecord.GetString(dataRecord.GetOrdinal("SortDay"));
         rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

         return rpt;
     }

        private static IncompleteMonthlyReport FillDataRecordIncompleteMonthly(IDataRecord dataRecord)
      {
          IncompleteMonthlyReport rpt = new IncompleteMonthlyReport();


          rpt.Reason = dataRecord.GetString(dataRecord.GetOrdinal("Reason"));
          rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
          rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
          rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

          return rpt;
      }

        private static ProductivityReportMonthly FillDataRecordMonthly(IDataRecord dataRecord)
       {
           ProductivityReportMonthly rpt = new ProductivityReportMonthly();


           rpt.SourceName = dataRecord.GetString(dataRecord.GetOrdinal("SourceName"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
           rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

           return rpt;
       }

       private static RejectedSummaryReportMonthly FillDataRecordRejectedSummaryMonthly(IDataRecord dataRecord)
       {
           RejectedSummaryReportMonthly rpt = new RejectedSummaryReportMonthly();


           rpt.ReasonName = dataRecord.GetString(dataRecord.GetOrdinal("ReasonName"));
           rpt.DisplayMonth = dataRecord.GetString(dataRecord.GetOrdinal("DisplayMonth"));
           rpt.SortMonth = dataRecord.GetString(dataRecord.GetOrdinal("SortMonth"));
           rpt.GroupCount = dataRecord.GetInt32(dataRecord.GetOrdinal("GroupCount"));

           return rpt;
       }

       #region GetProductivityReportList
       public static IEnumerable<ProductivityReportEntity> GetProductivityReportList(string startDate,
                                                            string endDate,
                                                            string statusCode,
                                                            string channelCode,
                                                            string regionCode,
                                                            string districtCode,
                                                            string branchCode,
                                                            string referrorCode,
                                                            bool isMonthly
                                                        )
       {
           string spName = "spProductivityMonthlyWeekly";
           DBSqlHelper sqlHelper = new DBSqlHelper();
           //SqlDataReader reader;
           List<ProductivityReportEntity> list = new List<ProductivityReportEntity>();
           endDate = String.Format("{0:MM/dd/yyyy}", endDate.AsDateTime().AddDays(1));
           isMonthly = isMonthly.AsBoolean();

           SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@startDate", startDate) ,
							new SqlParameter("@endDate",	endDate),
							new SqlParameter("@statusCode",	statusCode), 
							new SqlParameter("@channelCode", channelCode),
                            new SqlParameter("@regionCode", regionCode),
                            new SqlParameter("@districtCode", districtCode),
							new SqlParameter("@branchCode", branchCode ),
                            new SqlParameter("@referrorCode", referrorCode )
							//new SqlParameter("@isMonthly", isMonthly)
							};

           //reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

           using (SqlDataReader reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams))
           {
               if (reader.HasRows)
               {
                   while (reader.Read())
                   {
                       ProductivityReportEntity dto = new ProductivityReportEntity();
                       //dto.ChannelCode = reader["ChannelCode"].ToString();
                       dto.ChannelCode = reader[reader.GetOrdinal("ChannelCode")].ToString();
                       dto.ChannelName = reader[reader.GetOrdinal("ChannelName")].ToString();
                       dto.BranchCode = reader[reader.GetOrdinal("BranchCode")].ToString();
                       dto.BranchName = reader[reader.GetOrdinal("BranchName")].ToString();
                       dto.DistrictCode = reader[reader.GetOrdinal("DistrictCode")].ToString();
                       dto.DistrictName = reader[reader.GetOrdinal("DistrictName")].ToString();
                       dto.RegionCode = reader[reader.GetOrdinal("RegionCode")].ToString();
                       dto.RegionName = reader[reader.GetOrdinal("RegionName")].ToString();
                       dto.WithRecord = reader[reader.GetOrdinal("WithRecord")].AsBoolean();
                       dto.RecordCount = (dto.WithRecord == true ? 1 : 0);
                       dto.Status = reader[reader.GetOrdinal("StatusCode")].ToString();
                       dto.MonthYearDisplay = reader[reader.GetOrdinal("MonthYearDisplay")].ToString();
                       dto.IsCarDealer = reader[reader.GetOrdinal("IsCarDealer")].AsBoolean();
                       dto.IsYGC = reader[reader.GetOrdinal("IsYGC")].AsBoolean();
                       dto.YGCDescription = dto.IsCarDealer ? (dto.IsYGC ? "YGC" : "Non-YGC") : string.Empty;
                       dto.MonthDisplay = reader[reader.GetOrdinal("MonthDisplay")].AsInt();
                       dto.StatusDate = reader[reader.GetOrdinal("StatusDate")].AsDateTime();
                       dto.YearDisplay = dto.StatusDate.ToString("yyyy").AsInt();
                       dto.ApprovalCount = (dto.Status == "Approved" ? 1 : 0);
                       dto.RejectedCount = (dto.Status == "Rejected" ? 1 : 0);
                       dto.InProcessCount = (dto.Status == "Inprocess" ? 1 : 0);
                       dto.IncompleteCount = (dto.Status == "Incomplete" ? 1 : 0);
                       //if (isMonthly)
                       //{
                       //    //dto.MonthYearDate = reader["MonthYearDate"].AsDateTime();
                       //    dto.MonthDisplay = reader["MonthDisplay"].AsInt();
                       //    dto.YearDisplay = reader["YearDisplay"].AsInt();
                       //}
                       //else
                       //{
                       //    dto.StatusDate = reader["StatusDate"].AsDateTime();
                       //}

                       list.Add(dto);

                   }
               }
               reader.Close();
               reader.Dispose();
               GC.Collect();
           }
           //reader.Close();
           //reader.Dispose();
           //reader = null;
           return list.AsEnumerable();
       }
       #endregion

       #region -- Top Rejected Reason --
       public static IEnumerable<RejectedReportEntity> GetTopRejectedReasonReport(DateTime startDate, DateTime endDate, string channelCode, string regionCode, string districtCode, string branchCode, string referrorCode)
       {
           var spName = "spGetTopRejected";
           var sqlHelper = new DBSqlHelper();
           SqlDataReader reader;
           var list = new List<RejectedReportEntity>();

           SqlParameter[] sqlParams = new SqlParameter[] {
                            new SqlParameter("@DateFrom", (object) startDate ?? DBNull.Value ),
                            new SqlParameter("@DateTo",  (object) endDate ?? DBNull.Value ),
                            new SqlParameter("@ChannelCode",  (object) channelCode ?? DBNull.Value ),
                            new SqlParameter("@RegionCode",  (object) regionCode ?? DBNull.Value ),
                            new SqlParameter("@DistrictCode",  (object) districtCode ?? DBNull.Value ),
                            new SqlParameter("@BranchCode",  (object) branchCode ?? DBNull.Value ),
                            new SqlParameter("@ReferrorCode",  (object) referrorCode ?? DBNull.Value )
						};

           reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

           if (reader.HasRows)
           {
               while (reader.Read())
               {
                   var item = new RejectedReportEntity();
                   item.Reason = reader["Reason"].ToString();
                   item.RecordCount = Convert.ToInt32(reader["RecordCount"].ToString());

                   list.Add(item);
               }
           }

           reader.Close();
           reader.Dispose();
           reader = null;

           return list.AsEnumerable();
       }
       #endregion

       #region GetProductivityReasonReportList
       public static IEnumerable<ProductivityReportEntity> GetProductivityReasonReportList(string channelCode,
                                                                string regionCode,
                                                                string districtCode,
                                                                string branchCode,
                                                                string referrorCode,
                                                                string startDate,
                                                                string endDate,
                                                                bool isMonthly,
                                                                string statusCode
                                                        )
       {
           string spName = "spProductivityReasonMonthlyWeekly";
           DBSqlHelper sqlHelper = new DBSqlHelper();
           SqlDataReader reader;
           List<ProductivityReportEntity> list = new List<ProductivityReportEntity>();
           endDate = String.Format("{0:MM/dd/yyyy}", endDate.AsDateTime().AddDays(1));
           isMonthly = isMonthly.AsBoolean();

           SqlParameter[] sqlParams = new SqlParameter[] {
                            new SqlParameter("@channelCode", (object) channelCode ?? DBNull.Value ),
                            new SqlParameter("@regionCode",  (object) regionCode ?? DBNull.Value ),
                            new SqlParameter("@districtCode", (object) districtCode ?? DBNull.Value),
                            new SqlParameter("@branchCode",(object) branchCode ?? DBNull.Value ),
                            new SqlParameter("@referrorCode",(object) referrorCode ?? DBNull.Value ),
							new SqlParameter("@startDate", startDate) ,
							new SqlParameter("@endDate",	endDate),
                            new SqlParameter("@isMonthly", isMonthly),
							new SqlParameter("@statusCode",	(object) statusCode ?? DBNull.Value), 
						};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

           if (reader.HasRows)
           {
               while (reader.Read())
               {
                   ProductivityReportEntity dto = new ProductivityReportEntity();
                   dto.ChannelCode = reader["ChannelCode"].ToString();
                   dto.ChannelName = reader["ChannelName"].ToString();
                   dto.RegionCode = reader["RegionCode"].ToString();
                   dto.RegionName = reader["RegionName"].ToString();
                   dto.DistrictCode = reader["DistrictCode"].ToString();
                   dto.DistrictName = reader["DistrictName"].ToString();
                   dto.BranchCode = reader["BranchCode"].ToString();
                   dto.BranchName = reader["BranchName"].ToString();
                   //dto.RecordCount = reader["GroupCount"].AsInt();
                   dto.Status = reader["StatusCode"].ToString();
                   dto.MonthYearDisplay = reader["MonthYearDisplay"].ToString();
                   //dto.ReasonCode = reader["ReasonCode"].ToString();
                   dto.ReasonName = reader["ReasonName"].ToString().Trim();
                   //dto.IsCarDealer = reader["IsCarDealer"].AsBoolean();
                   //dto.IsYGC = reader["IsYGC"].AsBoolean();
                   //dto.YGCDescription = dto.IsCarDealer ? (dto.IsYGC ? "YGC" : "Non-YGC") : string.Empty;
                   dto.WithRecord = reader["WithRecord"].AsBoolean();
                   dto.RecordCount = dto.WithRecord == true ? 1 : 0;
                   dto.ApplicantName = reader["ApplicantName"].ToString();
                   dto.ReferrorCode = reader["ReferrorCode"].ToString();
                   if (isMonthly)
                   {
                       //dto.MonthYearDate = reader["MonthYearDate"].AsDateTime();
                       dto.MonthDisplay = reader["MonthDisplay"].AsInt();
                       dto.YearDisplay = reader["YearDisplay"].AsInt();
                   }
                   else
                   {
                       dto.StatusDate = reader["StatusDate"].AsDateTime();
                   }

                   list.Add(dto);

               }
           }
           reader.Close();
           reader.Dispose();
           reader = null;
           return list.AsEnumerable();
       }
       #endregion


       #region GetMTDYTDSummary
       public static List<MTDYTDSummary> GetMTDYTDSummary()
       {
           return mTDYTDSummary;
           //return FillDataRecordDailyProdSummaryTestFields();
       }      
       #endregion   
 
       #region GetMTDYTDDetails
       public static List<MTDYTDDetails> GetMTDYTDDetails()
       {
           return mTDYTDDetails;
       }
       #endregion

       #region GetMTDYTDIntraYGC
       public static List<MTDYTDIntraYGC> GetMTDYTDIntraYGC()
       {
           return mTDYTDIntraYGC;
           //return FillDataRecordDailyProdSummaryTestFields3();
       }
       #endregion

       #region GetProductivityGraph
       public static List<ProductivityGraph> GetProductivityGraph()
       {
           return productivityGraph;
           //return GetProductivityGraphValues();
       }
       #endregion
    }
}