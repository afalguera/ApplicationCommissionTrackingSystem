using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;
using CRS.Helper;
using System.Globalization;

namespace CRS.Bll
    
{
    public class ProductivityReportManager
    {

        public static IEnumerable<IncompleteDetailReport> GetIncompleteDetail(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetIncompleteDetail(startdate, enddate);

        }
        
        
        public static IEnumerable<OutrightRejectedReport> GetOutrightRejected(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetOutrightRejected(startdate, enddate);

        }
        
        public static IEnumerable<ProductivityReport> GetProductivityOthers(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityOthers(startdate,enddate, statuscode);
        
        }

        public static IEnumerable<ProductivityReport> GetProductivityYTD()
        {
            return ProductivityReportManagerDB.GetProductivityYTD();

        }

        public static IEnumerable<ProductivityReportWeekly> GetProductivityWeekly(List<ReportWeek> reportweeks,
                                                                              string StatusCode,string NewStatus, string Channel)
        {
            return ProductivityReportManagerDB.GetProductivityWeekly(reportweeks,StatusCode,NewStatus, Channel);

        }

        public static IEnumerable<ProductivityReportMonthly> GetProductivityMonthly(int Year,
                                                                              string StatusCode, string Channel)
        {
            return ProductivityReportManagerDB.GetProductivityMonthly(Year, StatusCode, Channel);

        }

        public static IEnumerable<RejectedSummaryReportWeekly> GetRejectedSummaryWeekly(List<ReportWeek> reportweeks,
                                                                              string StatusCode, string NewStatus, string Channel)
        {
            return ProductivityReportManagerDB.GetRejectedSummaryWeekly(reportweeks, StatusCode, NewStatus, Channel);

        }
        
        public static IEnumerable<IncompleteWeeklyReport> GetIncompleteWeekly(DateTime? Week1_Start, DateTime? Week1_End,
                                                                              DateTime? Week2_Start, DateTime? Week2_End,
                                                                              DateTime? Week3_Start, DateTime? Week3_End,
                                                                              DateTime? Week4_Start, DateTime? Week4_End,
                                                                              DateTime? Week5_Start, DateTime? Week5_End
        )
        {
            return ProductivityReportManagerDB.GetIncompleteWeekly(Week1_Start,Week1_End,Week2_Start,Week2_End,
                                                                  Week3_Start,Week3_End,Week4_Start,Week4_End,
                                                                  Week5_Start, Week5_End);

        }

        public static IEnumerable<IncompleteDailyReport> GetIncompleteDaily(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetIncompleteDaily(startdate, enddate);

        }

        public static IEnumerable<IncompleteMonthlyReport> GetIncompleteMonthly(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetIncompleteMonthly(startdate, enddate);

        }

        public static IEnumerable<IncompleteQuarterlyReport> GetIncompleteQuarterly(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetIncompleteQuarterly(startdate, enddate);

        }

        public static IEnumerable<IncompleteOthersReport> GetIncompleteOthers(DateTime startdate, DateTime enddate)
        {
            return ProductivityReportManagerDB.GetIncompleteOthers(startdate, enddate);

        }

        public static IEnumerable<ProductivityReportDetail> GetProductivityDetail(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityDetail(startdate, enddate, statuscode);

        }
        
        public static IEnumerable<ProductivityReportYearly> GetProductivityYearly(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityYearly(startdate, enddate, statuscode);

        }

        public static IEnumerable<DailyProdSummaryMTD> GetDailyProdSummaryMTD()
        {
            return pDailyProdSummary;
        }

        public static IEnumerable<DailyProdSummaryMTD> pDailyProdSummary;


        public static IEnumerable<DailyProdSummaryMTDIntraYGC> GetDailyProdSummaryMTDIntraYGC()
        {
            return pDailyProdSummaryMTDIntraYGC;
        }

        public static IEnumerable<DailyProdSummaryMTDIntraYGC> pDailyProdSummaryMTDIntraYGC;


        public static IEnumerable<DailyProdSummaryYTDIntraYGC> GetDailyProdSummaryYTDIntraYGC()
        {
            return pDailyProdSummaryYTDIntraYGC;
        }

        public static IEnumerable<DailyProdSummaryYTDIntraYGC> pDailyProdSummaryYTDIntraYGC;

        
        public static IEnumerable<DailyProdSummaryInflowsBookingsChart> GetDailyProdSummaryIBChart()
        {
            return pDailyProdSummaryIBChart;
        }

        public static IEnumerable<DailyProdSummaryInflowsBookingsChart> pDailyProdSummaryIBChart;


        public static IEnumerable<DailyProdSummaryBookingsChart> GetDailyProdSummaryBookingsChart()
        {
            return pDailyProdSummaryBookingsChart;
        }

        public static IEnumerable<DailyProdSummaryBookingsChart> pDailyProdSummaryBookingsChart;
        
        public static IEnumerable<DailyProdSummaryInflowsChart> GetDailyProdSummaryInflowsChart()
        {
            return pDailyProdSummaryInflowsChart;
        }

        public static IEnumerable<DailyProdSummaryInflowsChart> pDailyProdSummaryInflowsChart;
        
        
        
        //public static IEnumerable<DailyProdSummary> GetDailyProdSummary(int Year,ref IEnumerable<DailyProdSummaryMTD> MTDSummary,ref IEnumerable<DailyProdSummaryInflowsBookingsChart> IBChart,
        //    ref IEnumerable<DailyProdSummaryBookingsChart> BookingsChart, ref IEnumerable<DailyProdSummaryInflowsChart> InflowsChart,
        //    ref IEnumerable<DailyProdSummaryMTDIntraYGC> MTDIntraYGC, ref IEnumerable<DailyProdSummaryYTDIntraYGC> YTDIntraYGC, 
        //    string channelCode, string regionCode, string districtCode, string branchCode, string referrorCode)
        //public static ProductivitySummaryCollection GetDailyProdSummary(int Year, string channelCode, string regionCode, string districtCode, 
        //                                                                string branchCode, string referrorCode)
        public static void GetDailyProdSummary(int Year, string channelCode, string regionCode, string districtCode,
                                                                    string branchCode, string referrorCode)
        {
           ProductivityReportManagerDB.GetDailyProdSummary(Year, channelCode, regionCode, districtCode, branchCode, referrorCode);
            //IEnumerable<DailyProdSummary> tempProdSummary =  ProductivityReportManagerDB.GetDailyProdSummary(Year, ref MTDSummary,ref IBChart, ref BookingsChart, ref InflowsChart, ref MTDIntraYGC, ref YTDIntraYGC,
            //                                                                                                 channelCode, regionCode, districtCode, branchCode, referrorCode);
            //pDailyProdSummary = MTDSummary;
            //pDailyProdSummaryIBChart = IBChart;
            //pDailyProdSummaryBookingsChart = BookingsChart;
            //pDailyProdSummaryInflowsChart = InflowsChart;
            //pDailyProdSummaryYTDIntraYGC = YTDIntraYGC;
            //pDailyProdSummaryMTDIntraYGC = MTDIntraYGC;

            //return tempProdSummary;
           //return prodCollList;


        }

        #region GetMTDYTDSummary
        public static List<MTDYTDSummary> GetMTDYTDSummary()
        {
            return ProductivityReportManagerDB.GetMTDYTDSummary();
        } 
        #endregion

        #region GetMTDYTDDetails
        public static List<MTDYTDDetails> GetMTDYTDDetails()
        {
            return ProductivityReportManagerDB.GetMTDYTDDetails();
        } 
        #endregion

        #region GetMTDYTDIntraYGC
        public static List<MTDYTDIntraYGC> GetMTDYTDIntraYGC()
        {
            return ProductivityReportManagerDB.GetMTDYTDIntraYGC();
        }
        #endregion

        #region GetProductivityGraph
        public static List<ProductivityGraph> GetProductivityGraph()
        {
            return ProductivityReportManagerDB.GetProductivityGraph();
        }
        #endregion

        public static IEnumerable<ProductivityReportQuarterly> GetProductivityQuarterly(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityQuarterly(startdate,enddate, statuscode);

        }


        public static IEnumerable<ProductivityReportMonthly> GetProductivityMonthly(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityMonthly(startdate,enddate, statuscode);

        }

        public static IEnumerable<RejectedSummaryReportMonthly> GetRejectedSummaryMonthly(int Year, string StatusCode, string Channel)
        {
            return ProductivityReportManagerDB.GetRejectedSummaryMonthly(Year, StatusCode, Channel);

        }
        
        public static IEnumerable<ProductivityReportDaily> GetProductivityDaily(DateTime startdate, DateTime enddate, string statuscode)
        {
            return ProductivityReportManagerDB.GetProductivityDaily(startdate, enddate, statuscode);

        }

        public static IEnumerable<RejectedReportEntity> GetTopRejectedReasonReport(DateTime startDate, DateTime endDate, string channelCode, string regionCode, string districtCode, string branchCode, string referrorCode)
        {
            return ProductivityReportManagerDB.GetTopRejectedReasonReport(startDate, endDate, channelCode, regionCode, districtCode, branchCode, referrorCode);
        }

        #region Get Productivity Report List (Monthly and Weekly)
        public static IEnumerable<ProductivityReportEntity> GetProductivityReportList(string startDate,
                                                            string endDate,
                                                            string statusCode,
                                                            string channelCode,
                                                            string regionCode,
                                                            string districtCode,
                                                            string branchCode,
                                                            string referrorCode,
                                                            bool isMonthly,
                                                            int selectedMonth,     
                                                            int selectedYear, 
                                                            bool isAll
                                                      )
        {
            var list = ProductivityReportManagerDB.GetProductivityReportList(startDate,
                                                           endDate,
                                                           statusCode,
                                                           channelCode,
                                                           regionCode,
                                                           districtCode,
                                                           branchCode,
                                                           referrorCode,
                                                           isMonthly
                                                      ).ToList();

            if (isMonthly && !string.IsNullOrEmpty(channelCode))
            {
                var fItem = list.Select(s => s).FirstOrDefault();
                for (int i = 1; i <= selectedMonth; i++)
                {
                    string monthName = new DateTime(selectedYear, i, 1)
                                    .ToString("MMM", CultureInfo.InvariantCulture);
                    var withMonthValue = list.Where(x => x.MonthDisplay == i).Any();
                    if (!withMonthValue)
                    {
                        list.Add(new ProductivityReportEntity()
                        {
                            ChannelCode = fItem.ChannelCode,
                            ChannelName = fItem.ChannelName,
                            RegionCode = fItem.RegionCode,
                            RegionName = fItem.RegionName,
                            DistrictCode = fItem.DistrictCode,
                            DistrictName = fItem.DistrictName,
                            BranchCode = fItem.BranchCode,
                            BranchName = fItem.BranchName,
                            WithRecord = false,
                            YGCDescription = string.Empty,
                            MonthDisplay = i,
                            MonthYearDisplay = monthName + " " + selectedYear,
                            Status = null,
                            YearDisplay = selectedYear,
                            RecordCount = 0,
                            ApprovalCount = 0,
                            RejectedCount = 0,
                            InProcessCount = 0,
                            IncompleteCount = 0
                        });
                    }
                }
            }


            return list;
            //if (!string.IsNullOrEmpty(regionCode))
            //{
            //    list = list.Where(x => x.RegionCode == regionCode);
            //}

            //if (!string.IsNullOrEmpty(districtCode))
            //{
            //    list = list.Where(x => x.DistrictCode == districtCode);
            //}

            //if (!string.IsNullOrEmpty(branchCode))
            //{
            //    list = list.Where(x => x.BranchCode == branchCode);
            //}

            //var withMatchList = list.Where(x => x.WithRecord);
            //var noMatchList = list.Where(x => !x.WithRecord);


 
            //if (!list.Any())
            //{
            //    var channel = !string.IsNullOrEmpty(channelCode) ? CommissionDashboardDB.GetChannelList(channelCode).FirstOrDefault() : null;
            //    var district = !string.IsNullOrEmpty(districtCode) ? CommissionDashboardDB.GetDistrictList(channelCode, regionCode, districtCode).FirstOrDefault() : null;
            //    var branch = !string.IsNullOrEmpty(branchCode) ? CommissionDashboardDB.GetBranchList(channelCode, districtCode, branchCode).FirstOrDefault() : null;
            //    var tempList = new List<ProductivityReportEntity>();

            //    tempList.Add(new ProductivityReportEntity()
            //    {
            //        ChannelCode = channel != null ? channel.Code : string.Empty,
            //        ChannelName = channel != null ? channel.Name : string.Empty,
            //        RegionCode = string.Empty,
            //        RegionName = string.Empty,
            //        DistrictCode = district != null ? district.Code : string.Empty,
            //        DistrictName = district != null ? district.Name : string.Empty,
            //        BranchCode = branch != null ? branch.Code : string.Empty,
            //        BranchName = branch != null ? branch.Name : string.Empty,
            //    });

            //    noMatchList = tempList;

            //    noMatchList = noMatchList.Select(i =>
            //    {
            //        i.MonthDisplay = selectedMonth;
            //        i.MonthYearDisplay = new DateTime(selectedYear, selectedMonth, 1)
            //                            .ToString("MMM", CultureInfo.InvariantCulture) + " " + selectedYear;
            //        i.YearDisplay = selectedYear;
            //        i.RecordCount = 0;
            //        return i;
            //    });
            //}



            //var combined = withMatchList.Concat(noMatchList).ToList();

            //if (isMonthly)
            //{
            //    var fItem = combined.Select(s => s).FirstOrDefault();

            //    for (int i = 1; i <= selectedMonth; i++)
            //    {
            //        string monthName = new DateTime(selectedYear, i, 1)
            //                        .ToString("MMM", CultureInfo.InvariantCulture);
            //        var withMonthValue = combined.Where(x => x.MonthDisplay == i).Any();
            //        if (!withMonthValue)
            //        {
            //            combined.Add(new ProductivityReportEntity()
            //            {
            //                ChannelCode = fItem.ChannelCode,
            //                ChannelName = fItem.ChannelName,
            //                RegionCode = fItem.RegionCode,
            //                RegionName = fItem.RegionName,
            //                DistrictCode = fItem.DistrictCode,
            //                DistrictName = fItem.DistrictName,
            //                BranchCode = fItem.BranchCode,
            //                BranchName = fItem.BranchName,
            //                MonthDisplay = i,
            //                MonthYearDisplay = monthName + " " + selectedYear,
            //                YearDisplay = selectedYear,
            //                RecordCount = 0,
            //                ApplicantName = fItem.ApplicantName,
            //                ReasonCode = string.Empty,
            //                ReasonName = string.Empty

            //            });
            //        }
            //    }
            //}
            //return combined;
        } 
        #endregion

        #region Get Productivity Reason Report List (Monthly and Weekly)
        //public static IEnumerable<ProductivityReportEntity> GetProductivityReasonReportList(string startDate,
        //                                                  string endDate,
        //                                                  string statusCode,
        //                                                  string channelCode,
        //                                                  string branchCode,
        //                                                  bool isMonthly
        //                                              )
        //{
        //    return ProductivityReportManagerDB.GetProductivityReasonReportList(startDate,
        //                                                   endDate,
        //                                                   statusCode,
        //                                                   channelCode,
        //                                                   branchCode,
        //                                                   isMonthly
        //                                              );
        //}
        public static IEnumerable<ProductivityReportEntity> GetProductivityReasonReportList(string channelCode,
                                                                string regionCode,
                                                                string districtCode,
                                                                string branchCode,
                                                                string referrorCode,
                                                                string startDate,
                                                                string endDate,
                                                                bool isMonthly,
                                                                string statusCode,
                                                                int selectedMonth,     
                                                                int selectedYear,
                                                                string reportType
                                                        )
        {
            var list = ProductivityReportManagerDB.GetProductivityReasonReportList(channelCode,
                                                                regionCode,
                                                                districtCode,
                                                                branchCode,
                                                                referrorCode,
                                                                startDate,
                                                                endDate,
                                                                isMonthly,
                                                                statusCode
                                                        );


            if (!string.IsNullOrEmpty(regionCode))
            {
                list = list.Where(x => x.RegionCode == regionCode);
            }

            if (!string.IsNullOrEmpty(districtCode))
            {
                list = list.Where(x => x.DistrictCode == districtCode);
            }

            if (!string.IsNullOrEmpty(branchCode))
            {
                list = list.Where(x => x.BranchCode == branchCode);
            }

            if (!string.IsNullOrEmpty(referrorCode))
            {
                list = list.Where(x => x.ReferrorCode == referrorCode);
            }


            var withMatchList = list.Where(x => x.WithRecord);
            var noMatchList = list.Where(x => !x.WithRecord);

           

            if (!list.Any())
            {
                var channel = !string.IsNullOrEmpty(channelCode)  ? CommissionDashboardDB.GetChannelList(channelCode).FirstOrDefault() : null;
                var district = !string.IsNullOrEmpty(districtCode)  ? CommissionDashboardDB.GetDistrictList(channelCode, regionCode, districtCode).FirstOrDefault() : null;
                var branch = !string.IsNullOrEmpty(branchCode)  ? CommissionDashboardDB.GetBranchList(channelCode, districtCode, branchCode).FirstOrDefault() : null;
                var tempList = new List<ProductivityReportEntity>();

                tempList.Add(new ProductivityReportEntity()
                {
                    ChannelCode = channel != null ? channel.Code : string.Empty,
                    ChannelName = channel != null ? channel.Name : string.Empty,
                    RegionCode = string.Empty,
                    RegionName = string.Empty,
                    DistrictCode = district != null ? district.Code : string.Empty,
                    DistrictName = district != null ? district.Name : string.Empty,
                    BranchCode = branch != null ? branch.Code : string.Empty,
                    BranchName = branch != null ? branch.Name : string.Empty,
                    ApplicantName = string.Empty,
                    ReasonCode = string.Empty,
                    ReasonName = string.Empty

                });

                noMatchList = tempList;

                noMatchList = noMatchList.Select(i =>
                {
                    i.MonthDisplay = selectedMonth;
                    i.MonthYearDisplay = new DateTime(selectedYear, selectedMonth, 1)
                                        .ToString("MMM", CultureInfo.InvariantCulture) + " " + selectedYear;
                    i.YearDisplay = selectedYear;
                    i.RecordCount = 0;
                    return i;
                });
            }
           
           

            var combined = withMatchList.Concat(noMatchList).ToList();

            if (isMonthly)
            {
                var fItem = combined.Select(s => s).FirstOrDefault();
                
                for (int i = 1; i <= selectedMonth; i++)
                {
                    string monthName = new DateTime(selectedYear, i, 1)
                                    .ToString("MMM", CultureInfo.InvariantCulture);
                    var withMonthValue = combined.Where(x => x.MonthDisplay == i).Any();
                    if (!withMonthValue)
                    {
                        combined.Add(new ProductivityReportEntity()
                        {
                            ChannelCode = fItem.ChannelCode,
                            ChannelName = fItem.ChannelName,
                            RegionCode = fItem.RegionCode,
                            RegionName = fItem.RegionName,
                            DistrictCode = fItem.DistrictCode,
                            DistrictName = fItem.DistrictName,
                            BranchCode = fItem.BranchCode,
                            BranchName = fItem.BranchName,
                            MonthDisplay = i,
                            MonthYearDisplay = monthName + " " + selectedYear,
                            YearDisplay = selectedYear,
                            RecordCount = 0,
                            ApplicantName = string.Empty,
                            ReasonCode = string.Empty,
                            ReasonName = string.Empty

                        });
                     }
                }
            }

            if (reportType == "INCRT99")
            {
                combined = combined.Where(x => x.ReasonName.StartsWith("RT99")).ToList();
            }

            if (reportType == "INCPDOC")
            {
                combined = combined.Where(x => x.ReasonName.StartsWith("PDOC")).ToList();
            }
            return combined;
        }
        #endregion

        #region Get WeekList
        public static List<WeekRangeEntity> GetWeekList(int selectedYear)
        {
            List<WeekRangeEntity> wkList = new List<WeekRangeEntity>();
            int currYear = DateTime.Now.Year;
            //DateTime.Now.Month;
            int selectedMonth = 12;
            int lastDayOfMonth = 31;
            int iMonth = 1;
            string lDay = string.Empty;
            DateTime dtEnd = DateTime.Now;

            if (selectedYear == currYear)
            {
                DateTime date = DateTime.Now;
                selectedMonth = DateTime.Now.Month;
                lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
            }

            DateTime startDate = new DateTime(selectedYear, 1, 1);
            DateTime endDate = new DateTime(selectedYear, selectedMonth, lastDayOfMonth);
            int days = 0;
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(days))
            {
                DayOfWeek dw = date.DayOfWeek;
                days = GetAdditionalDays((int)dw); 
                if (iMonth != date.Month)
                {
                    startDate = new DateTime(date.Year, date.Month, 1);
                    dtEnd = date.AddDays(days);
                }
                else
                {
                    var dt = date.AddDays(days);
                    int dtMonth = dt.Month;
                    if (dt.Month != iMonth)
                    {
                        int endDt =  DateTime.DaysInMonth(date.Year, date.Month);
                        dtEnd = new DateTime(date.Year, date.Month, endDt);
                        
                    }
                    else
                    {
                        dtEnd = date.AddDays(days);
                    }
                }

                lDay = String.Format("{0:MM/dd/yyyy}", dtEnd);
                wkList.Add(new WeekRangeEntity
                    {
                        StartWkDate = String.Format("{0:MM/dd/yyyy}", startDate),
                        EndWkDate = lDay
                    });
                iMonth = date.Month;
                startDate = date.AddDays(days + 1);
            }
            return wkList;
        } 
        #endregion

        #region Add additional days
        private static int GetAdditionalDays(int dw)
        {
            int retVal = 0;
            switch (dw)
            {
                case 0:
                    retVal = 7;
                    break;
                case 1:
                    retVal = 6;
                    break;
                default:
                    retVal = 14 - dw;
                    break;

            }
            return retVal;
        } 
        #endregion

        #region Get Report Title
        public static string GetReportTitle(string reportType)
        {
            string strTitle = string.Empty;
            switch (reportType)
            {
                case "REJ":
                    strTitle = "Rejected Per Reason";
                    break;
                case "OUTREJ":
                    strTitle = "Outright Rejected";
                    break;
                case "INC":
                    strTitle = "Incomplete (PDOC & RT99)";
                    break;
                case "INCRT99":
                    strTitle = "Incomplete (RT99)";
                    break;
                case "INCPDOC":
                    strTitle = "Incomplete (PDOC)";
                    break;
                case "TOPREJ":
                    strTitle = "Top Rejected Reason";
                    break;
                default:
                    break;
            }

            return strTitle;
        } 
        #endregion

        #region GetReportTypes
        public static IEnumerable<EAPREntityPair> GetReportTypes(string userRole)
        {
            var list = new List<EAPREntityPair>();
            list.Add(new EAPREntityPair { Code = "PSUMMARY", Name = "Productivity" });
            if (userRole == "Administrator" || userRole == "Channel Head")
            {
                list.Add(new EAPREntityPair { Code = "PCHART", Name = "Productivity (Charts)" });
            }
            list.Add(new EAPREntityPair { Code = "TOPREJ", Name = "Top Rejected Reason" });
            list.Add(new EAPREntityPair { Code = "REJ", Name = "Rejected Per Reason" });
            list.Add(new EAPREntityPair { Code = "INC", Name = "Incomplete Referrals" });
            list.Add(new EAPREntityPair { Code = "INCRT99", Name = "Incomplete RT99" });
            list.Add(new EAPREntityPair { Code = "INCPDOC", Name = "Incomplete PDOC" });
            return list as IEnumerable<EAPREntityPair>; 
        }
        #endregion


        #region GetStatusTypes
        public static IEnumerable<StatusType> GetStatusTypes()
        {
            var list = new List<StatusType>();
            list = ApplicationStatusDB.GetStatusType().ToList();
            list.Add(new StatusType { StatusCode = "ALL", StatusName = "ALL" });
            return list as IEnumerable<StatusType>;
        }
        #endregion

        public static string GetChannelCodes(List<EAPRChannel> channelList)
        {
            string strCodes = string.Empty;
            foreach (var item in channelList)
            {
                strCodes += EncloseInQuotes(item.Code) + ",";
            }
            strCodes = strCodes.EndsWith(",") ? strCodes.Remove(0,1).Remove(strCodes.Length-3) : strCodes;
            
            return strCodes;

        }

        public static string EncloseInQuotes(string str)
        {
            string strItem = @"'" + str   + "'";
            return strItem;
        }
      
    }
}