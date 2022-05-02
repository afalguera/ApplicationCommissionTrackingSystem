using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.BusinessEntities.Reports;
using CRS.Models;
using CRS.Helpers;
using CRS.Helper;
using System.Net;


namespace CRS.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ProductivityReportController : Controller
    {
        private static string dateRange { get; set; }
        private static List<ProductivityReportEntity> productivityList;

        #region Index
        public ActionResult Index()
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Account?" + "ProductivityReport/Index");
            }
        }
        #endregion

        #region Productivity Report (Monthly and Weekly)
        public ActionResult GetProductivityReportList(string selectedYear, string statusCode, string channelCode,
                                                      string regionCode, string districtCode, string branchCode,
                                                      bool isMonthly)
        {
            var model = new Object();
            string currMonth = DateTime.Now.Month.ToString("0#");
            int currYear = DateTime.Now.Year;
            int selYear = selectedYear.AsInt();
            int selMonth = (selYear >= currYear) ? DateTime.Now.Month : 12;
            string startDate = string.Empty;
            startDate = "01/01/" + selectedYear;
            string endDate = string.Empty;
            List<EAPRChannel> channelList = new List<EAPRChannel>();
            DateTime dtEnd = new DateTime();
            ReportData.ReportParameters.Clear();
            bool isAll = (statusCode == "ALL" ? true : false);
            statusCode = (statusCode == "ALL" ? null: statusCode);
            string channelCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel)
                               ? SessionWrapper.CurrentUser.Channel
                               : (channelCode ?? string.Empty);
            string regionCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.RegionCode)
                            ? SessionWrapper.CurrentUser.RegionCode
                            : (regionCode ?? string.Empty);
            string districtCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.DistrictCode)
                            ? SessionWrapper.CurrentUser.DistrictCode
                            : (districtCode ?? string.Empty);
            string branchCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.BranchCode)
                            ? SessionWrapper.CurrentUser.BranchCode
                            : (branchCode ?? string.Empty);
            string referrorCode = SessionWrapper.CurrentUser.ReferrorCode ?? string.Empty;
        
            productivityList = null;

            if (selYear == currYear)
            {
                DateTime date = DateTime.Now;
                var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                endDate = currMonth + "/" + lastDayOfMonth + "/" + selectedYear;
                dtEnd = endDate.AsDateTime().AddDays(1);
                endDate = dtEnd.ToString("MM/dd/yyyy");
                dateRange = date.ToString("MMMM") + " "  + date.Day.ToString() + ", " + selectedYear;
            }
            else
            {
                endDate = "12/31/" + selectedYear;
                dtEnd = endDate.AsDateTime().AddDays(1);
                dateRange =  "December 31, " + selectedYear; 
            }
      
            if (!isMonthly)
            {
                if (isAll)
                {
                    ReportData.ReportPath = Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportWeeklyALL.GetDescription());
                }
                else
                {
                    ReportData.ReportPath = Server
                                        .MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportWeekly.GetDescription());
                }
                var wList = ProductivityReportManager.GetWeekList(selectedYear.AsInt());
                var pList = model as List<ProductivityReportEntity>;
                var prodWkRptList = new List<ProductivityReportEntity>();
                var temp = ProductivityReportManager.GetProductivityReportList(startDate,
                                                                               endDate,
                                                                               statusCode,
                                                                               channelCodeParam,
                                                                               regionCodeParam,
                                                                               districtCodeParam,
                                                                               branchCodeParam,
                                                                               referrorCode,
                                                                               isMonthly,
                                                                               selMonth,
                                                                               selYear,
                                                                               isAll
                                                 );

                var fItem = temp.Select(s => s).FirstOrDefault();

                foreach (var wk in wList)
                {
                    var tempWeekly = temp.Where(x => x.StatusDate >= wk.StartWkDate.AsDateTime()
                                              && x.StatusDate < wk.EndWkDate.AsDateTime().AddDays(1));
                    string weekRange = String.Format("{0:MM/dd}", wk.StartWkDate.AsDateTime()) + " to "
                                        + String.Format("{0:MM/dd}", wk.EndWkDate.AsDateTime());
                    tempWeekly.Select(c => { c.WeekRangeDisplay = weekRange; return c; }).ToList();

                    if (!tempWeekly.Any())
                    {

                        int monthNumber = wk.StartWkDate.AsDateTime().Month;
                        string monthName = new DateTime(selectedYear.AsInt(), monthNumber, 1)
                                  .ToString("MMM", CultureInfo.InvariantCulture);

                        prodWkRptList.Add(new ProductivityReportEntity()
                        {
                            ChannelCode = fItem != null ? fItem.ChannelCode : string.Empty,
                            ChannelName = fItem != null ? fItem.ChannelName : string.Empty,
                            RegionCode = fItem != null ? fItem.RegionCode : string.Empty,
                            RegionName = fItem != null ? fItem.RegionName : string.Empty,
                            DistrictCode = fItem != null ? fItem.DistrictCode : string.Empty,
                            DistrictName = fItem != null ? fItem.DistrictName : string.Empty,
                            BranchCode = fItem != null ? fItem.BranchCode : string.Empty,
                            BranchName = fItem != null ? fItem.BranchName : string.Empty,
                            WeekRangeDisplay = weekRange,
                            MonthDisplay = monthNumber,
                            MonthYearDisplay = monthName + " " + selectedYear,
                            YearDisplay = selectedYear.AsInt(),
                            RecordCount = 0,
                            YGCDescription = string.Empty,
                            IsYGC = false
                        });
                    }
                    else
                    {
                        prodWkRptList.AddRange(tempWeekly);
                    }
                }
                model = prodWkRptList.OrderBy(x => x.MonthDisplay).ToList();
            }
            else
            {
                if (isAll)
                {
                    ReportData.ReportPath = Server
                            .MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportMonthlyALL.GetDescription());
                }
                else
                {
                    ReportData.ReportPath = Server
                               .MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportMonthly.GetDescription());
                }
                model = ProductivityReportManager.GetProductivityReportList (startDate,
                                                                               endDate,
                                                                               statusCode,
                                                                               channelCodeParam,
                                                                               regionCodeParam,
                                                                               districtCodeParam,
                                                                               branchCodeParam,
                                                                               referrorCode,
                                                                               isMonthly,
                                                                               selMonth,
                                                                               selYear,
                                                                               isAll
                                                 ).OrderBy(x => x.MonthDisplay).ToList();
            }

            //ReportDataContents<ProductivityReportEntity>.Results = model as List<ProductivityReportEntity>;
            productivityList = model as List<ProductivityReportEntity>;
            ReportDataContents<ProductivityReportEntity>.Results = productivityList;
            if (ReportDataContents<ProductivityReportEntity>.Results.Count > 0)
            {
                ReportData.ReportType = "PRODUCTIVITY";
                ReportData.ReportDataSetName = "ProductivityReport";
                //ReportData.ReportParameters.Clear();
                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                paramReport.Add(new Parameter { ParameterName = "Status", Value = string.IsNullOrEmpty(statusCode) ? "INFLOWS" : statusCode });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            ViewBag.Message = "Welcome to CRS";
            var jsonResult = Json(productivityList.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
           
        } 
        #endregion

        #region -- Top Rejected Reason --
        public ActionResult GetTopRejectedReport(string selectedYear, string channelCode, string regionCode, string districtCode, string branchCode)
        {
            var startDate = new DateTime(selectedYear.AsInt(), 1, 1);
            var endDate = new DateTime(selectedYear.AsInt(), 12, 31);
            var rptTitle = string.Empty;

            if (selectedYear.AsInt() == DateTime.Now.Year)
            {
                rptTitle = string.Format("{0} as of {1} {2}, {3}", ProductivityReportManager.GetReportTitle("TOPREJ"), DateTime.Now.ToString("MMMM"), DateTime.Today.Day - 1, DateTime.Now.Year);
            }
            else
            {
                rptTitle = string.Format("{0} for Year {1}", ProductivityReportManager.GetReportTitle("TOPREJ"), selectedYear);
            }

            var list = ProductivityReportManager.GetTopRejectedReasonReport(startDate, endDate, channelCode, regionCode, districtCode, branchCode, SessionWrapper.CurrentUser.ReferrorCode == string.Empty ? null : SessionWrapper.CurrentUser.ReferrorCode);

            if (list.Count() > 0)
            {
                ReportDataContents<RejectedReportEntity>.Results = list as List<RejectedReportEntity>;
                ReportData.ReportPath = Server.MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportTopRejectedReason.GetDescription());
                ReportData.ReportType = "TOPREJECTED";
                ReportData.ReportDataSetName = "TopRejectedReason";
                ReportData.ReportParameters.Clear();

                var paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "ReportTitle", Value = rptTitle });
                ReportData.ReportParameters.AddRange(paramReport);
                
                ViewBag.Message = "Welcome to ACTS";
                var jsonResult = Json(ReportDataContents<RejectedReportEntity>.Results, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            else
            {
                return null;
            }

        }
        #endregion

        #region Productivity Report with Reason [Rejected, Incomplete, Outright Reject] (Monthly and Weekly)
        public ActionResult GetProductivityReasonReportList(string selectedYear, string statusCode,
                                                   string channelCode, string regionCode, string districtCode,
                                                    bool isMonthly, string reportType, string branchCode)
        {
            var model = new Object();
            //string regionCode = SessionWrapper.CurrentUser.RegionCode ?? null;
            //string districtCode = SessionWrapper.CurrentUser.DistrictCode ?? null;
            string currMonth = DateTime.Now.Month.ToString("0#");
            int currYear = DateTime.Now.Year;
            int selYear = selectedYear.AsInt();
            int selMonth = (selYear >= currYear) ?   DateTime.Now.Month : 12;
            string startDate = string.Empty;
            startDate = "01/01/" + selectedYear;
            string endDate = string.Empty;
            statusCode = statusCode ?? null;
            //channelCode = channelCode ?? null;
            //branchCode = branchCode ?? null;
            string strTitle = string.Empty;
            strTitle = ProductivityReportManager.GetReportTitle(reportType);
            string strChannelCodes = string.Empty;
            List<EAPRChannel> channelList = new List<EAPRChannel>();
            string channelCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel)
                               ? SessionWrapper.CurrentUser.Channel
                               : (channelCode ?? string.Empty);
            string regionCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.RegionCode)
                            ? SessionWrapper.CurrentUser.RegionCode
                            : (regionCode ?? string.Empty);
            string districtCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.DistrictCode)
                            ? SessionWrapper.CurrentUser.DistrictCode
                            : (districtCode ?? string.Empty);
            string branchCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.BranchCode)
                            ? SessionWrapper.CurrentUser.BranchCode
                            : (branchCode ?? string.Empty);
            string referrorCode = SessionWrapper.CurrentUser.ReferrorCode ?? string.Empty;


            if (selYear == currYear)
            {
                DateTime date = DateTime.Now;
                var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                endDate = currMonth + "/" + lastDayOfMonth + "/" + selectedYear;
                dateRange = date.ToString("MMMM") + " " + date.Day.ToString() + ", " + selectedYear;
            }
            else
            {
                endDate = "12/31/" + selectedYear;
                dateRange = "December 31, " + selectedYear; 
            }

            if (!isMonthly)
            {
                ReportData.ReportPath = Server
                                    .MapPath(ReportTypes.ReportTypesAndPath.ProductivityReportReasonWeekly.GetDescription());
                var wList = ProductivityReportManager.GetWeekList(selectedYear.AsInt());
                var pList = model as List<ProductivityReportEntity>;
                var prodWkRptList = new List<ProductivityReportEntity>();
                var temp = ProductivityReportManager.GetProductivityReasonReportList(channelCode,
                                                      regionCode,
                                                      districtCode,
                                                      branchCode,
                                                      referrorCode,
                                                      startDate,
                                                      endDate,
                                                      isMonthly,
                                                      statusCode,
                                                      selMonth,
                                                      selYear,
                                                      reportType
                                                 );

                var fItem = temp.Select(s => s).FirstOrDefault();

                foreach (var wk in wList)
                {
                    var tempWeekly = temp.Where(x => x.StatusDate > wk.StartWkDate.AsDateTime()
                                              && x.StatusDate < wk.EndWkDate.AsDateTime().AddDays(1) );
                    string weekRange = String.Format("{0:MM/dd}", wk.StartWkDate.AsDateTime()) + " to "
                                        + String.Format("{0:MM/dd}", wk.EndWkDate.AsDateTime());
                    tempWeekly.Select(c => { c.WeekRangeDisplay = weekRange; return c; }).ToList();

                    if (!tempWeekly.Any())
                    {
                        int monthNumber = wk.StartWkDate.AsDateTime().Month;
                        string monthName = new DateTime(selectedYear.AsInt(), monthNumber, 1)
                                  .ToString("MMM", CultureInfo.InvariantCulture);
                      
                        prodWkRptList.Add(new ProductivityReportEntity()
                                            {
                                                ChannelCode = fItem != null ? fItem.ChannelCode : string.Empty,
                                                ChannelName = fItem != null ? fItem.ChannelName : string.Empty,
                                                RegionCode = fItem != null ? fItem.RegionCode : string.Empty,
                                                RegionName = fItem != null ? fItem.RegionName : string.Empty,
                                                DistrictCode = fItem != null ? fItem.DistrictCode : string.Empty,
                                                DistrictName = fItem != null ? fItem.DistrictName : string.Empty,
                                                BranchCode = fItem != null ? fItem.BranchCode : string.Empty,
                                                BranchName = fItem != null ? fItem.BranchName : string.Empty,
                                                WeekRangeDisplay = weekRange,
                                                MonthDisplay = monthNumber,
                                                MonthYearDisplay = monthName + " " + selectedYear,
                                                YearDisplay = selectedYear.AsInt(),
                                                RecordCount = 0
                                            });
                    }
                    else
                    {
                        prodWkRptList.AddRange(tempWeekly);
                    }
                }
                model = prodWkRptList.OrderBy(x => x.MonthDisplay).ToList();
            }
            else
            {
                ReportData.ReportPath = Server
                           .MapPath(ReportTypes.ReportTypesAndPath.ProductivityMonthlyReasonReport.GetDescription());
                model = ProductivityReportManager.GetProductivityReasonReportList(channelCode,
                                                            regionCode,
                                                            districtCode,
                                                            branchCode,
                                                            referrorCode,
                                                            startDate,
                                                            endDate,
                                                            isMonthly,
                                                            statusCode,
                                                            selMonth,
                                                            selYear,
                                                            reportType
                                                       ).OrderBy(x => x.MonthDisplay).ToList();
            }

            ReportDataContents<ProductivityReportEntity>.Results = model as List<ProductivityReportEntity>;
            if (ReportDataContents<ProductivityReportEntity>.Results.Count > 0)
            {
                ReportData.ReportType = "PRODUCTIVITY";
                ReportData.ReportDataSetName = "ProductivityReport";
                ReportData.ReportParameters.Clear();

                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                paramReport.Add(new Parameter { ParameterName = "Status", Value = string.IsNullOrEmpty(statusCode) ? "INFLOWS" : statusCode });
                paramReport.Add(new Parameter { ParameterName = "ReportTitle", Value = strTitle });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            ViewBag.Message = "Welcome to CRS";
            var jsonResult = Json(ReportDataContents<ProductivityReportEntity>.Results.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region Productivity with Chart
        [HttpGet]
        public ActionResult ProductivitySummaryChart(string selectedYear, string channelCode,
                                                     string regionCode, string districtCode, string branchCode)
        {
            string strPeriodCovered;
            string channelCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel)
                              ? SessionWrapper.CurrentUser.Channel
                              : (channelCode ?? string.Empty);
            string regionCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.RegionCode)
                            ? SessionWrapper.CurrentUser.RegionCode
                            : (regionCode ?? string.Empty);
            string districtCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.DistrictCode)
                            ? SessionWrapper.CurrentUser.DistrictCode
                            : (districtCode ?? string.Empty);
            string branchCodeParam = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.BranchCode)
                            ? SessionWrapper.CurrentUser.BranchCode
                            : (branchCode ?? string.Empty);
            string referrorCode = SessionWrapper.CurrentUser.ReferrorCode ?? string.Empty;
            List<Parameter> paramReport;
            //ViewBag.IsButtonClicked = true;
            /*string sHideChart = "0";*/

            //List<DailyProdSummary> listDailyProdSummary = new List<DailyProdSummary>();
            //List<DailyProdSummaryMTD> listDailyProdSummaryMonthly = new List<DailyProdSummaryMTD>();
            //List<DailyProdSummaryInflowsBookingsChart> listDailyProdSummaryIBChart = new List<DailyProdSummaryInflowsBookingsChart>();
            //List<DailyProdSummaryBookingsChart> listDailyProdSummaryBookingsChart = new List<DailyProdSummaryBookingsChart>();
            //List<DailyProdSummaryInflowsChart> listDailyProdSummaryInflowsChart = new List<DailyProdSummaryInflowsChart>();
            //List<DailyProdSummaryMTDIntraYGC> listDailyProdSummaryMTDIntraYGC = new List<DailyProdSummaryMTDIntraYGC>();
            //List<DailyProdSummaryYTDIntraYGC> listDailyProdSummaryYTDIntraYGC = new List<DailyProdSummaryYTDIntraYGC>();

            //IEnumerable<DailyProdSummaryMTD> MTDSummary = new List<DailyProdSummaryMTD>();
            //IEnumerable<DailyProdSummaryInflowsBookingsChart> IBChart = new List<DailyProdSummaryInflowsBookingsChart>();

            //IEnumerable<DailyProdSummaryBookingsChart> BookingsChart = new List<DailyProdSummaryBookingsChart>();
            //IEnumerable<DailyProdSummaryInflowsChart> InflowsChart = new List<DailyProdSummaryInflowsChart>();
            //IEnumerable<DailyProdSummaryMTDIntraYGC> MTDIntraYGC = new List<DailyProdSummaryMTDIntraYGC>();
            //IEnumerable<DailyProdSummaryYTDIntraYGC> YTDIntraYGC = new List<DailyProdSummaryYTDIntraYGC>();

            //listDailyProdSummary = ProductivityReportManager.GetDailyProdSummary(selectedYear.AsInt(), ref MTDSummary, ref IBChart, ref BookingsChart, ref InflowsChart, ref MTDIntraYGC, ref YTDIntraYGC, 
            //                                                                     channelCodeParam, regionCodeParam, districtCodeParam, branchCodeParam, referrorCode).ToList();
            //listDailyProdSummaryMonthly = MTDSummary.ToList();
            //listDailyProdSummaryIBChart = IBChart.ToList();
            //listDailyProdSummaryBookingsChart = BookingsChart.ToList();
            //listDailyProdSummaryInflowsChart = InflowsChart.ToList();
            //listDailyProdSummaryMTDIntraYGC = MTDIntraYGC.ToList();
            //listDailyProdSummaryYTDIntraYGC = YTDIntraYGC.ToList();


            //ViewBag.ProductivityYearly = listDailyProdSummary;
            //ViewBag.ProductivityMonthly = listDailyProdSummaryMonthly;
            //ViewBag.ProductivityIBChart = listDailyProdSummaryIBChart;
            //ViewBag.ProductivityBookingsChart = listDailyProdSummaryBookingsChart;
            //ViewBag.ProductivityInflowsChart = listDailyProdSummaryInflowsChart;
            //ViewBag.ProductivityYTDIntraYGC = listDailyProdSummaryYTDIntraYGC;
            //ViewBag.ProductivityMTDIntraYGC = listDailyProdSummaryMTDIntraYGC;

            //ReportDataContents<DailyProdSummary>.Results = listDailyProdSummary as List<DailyProdSummary>;
            //ReportDataContents<DailyProdSummaryMTD>.Results = listDailyProdSummaryMonthly as List<DailyProdSummaryMTD>;
            //ReportDataContents<DailyProdSummaryInflowsBookingsChart>.Results = listDailyProdSummaryIBChart as List<DailyProdSummaryInflowsBookingsChart>;
            //ReportDataContents<DailyProdSummaryBookingsChart>.Results = listDailyProdSummaryBookingsChart as List<DailyProdSummaryBookingsChart>;
            //ReportDataContents<DailyProdSummaryInflowsChart>.Results = listDailyProdSummaryInflowsChart as List<DailyProdSummaryInflowsChart>;
            //ReportDataContents<DailyProdSummaryMTDIntraYGC>.Results = listDailyProdSummaryMTDIntraYGC as List<DailyProdSummaryMTDIntraYGC>;
            //ReportDataContents<DailyProdSummaryYTDIntraYGC>.Results = listDailyProdSummaryYTDIntraYGC as List<DailyProdSummaryYTDIntraYGC>;

            //var listDailyProdSummary = ProductivityReportManager.GetDailyProdSummary(selectedYear.AsInt(), ref mTDYTDSummary, ref IBChart, ref BookingsChart, ref InflowsChart, ref MTDIntraYGC, ref YTDIntraYGC, 
            //                                                                     channelCodeParam, regionCodeParam, districtCodeParam, branchCodeParam, referrorCode).ToList();


            ProductivityReportManager.GetDailyProdSummary(selectedYear.AsInt(), channelCode, regionCode, districtCode, branchCode, referrorCode);
            //ReportDataContents<ProductivitySummaryCollection>.Results = listDailyProdSummary;
            ReportDataContents<MTDYTDSummary>.Results = ProductivityReportManager.GetMTDYTDSummary();
            ReportDataContents<MTDYTDDetails>.Results = ProductivityReportManager.GetMTDYTDDetails();
            ReportDataContents<MTDYTDIntraYGC>.Results = ProductivityReportManager.GetMTDYTDIntraYGC();
            ReportDataContents<ProductivityGraph>.Results = ProductivityReportManager.GetProductivityGraph();

            ReportData.ReportType = "MTDYTDSummary";
            ReportData.ReportType2 = "MTDYTDDetails";
            ReportData.ReportType3 = "MTDYTDIntraYGC";
            ReportData.ReportType4 = "ProductivityGraph";


            //ReportData.ReportType = "DAILYPRODSUMMARY";
            //ReportData.ReportType2 = "DAILYPRODSUMMARYMTD";
            //ReportData.ReportType3 = "DAILYPRODSUMMARYIBCHART";
            //ReportData.ReportType4 = "DAILYPRODSUMMARYBOOKINGSCHART";
            //ReportData.ReportType5 = "DAILYPRODSUMMARYINFLOWSCHART";
            //ReportData.ReportType6 = "DAILYPRODSUMMARYMTDINTRAYGC";
            //ReportData.ReportType7 = "DAILYPRODSUMMARYYTDINTRAYGC";


            ReportData.ReportPath = Server.MapPath(ReportTypes.ReportTypesAndPath.DailyProdSummary.GetDescription());
            ReportData.ReportDataSetName = "MTDYTDSummary";
            ReportData.ReportDataSetName2 = "MTDYTDDetails";
            ReportData.ReportDataSetName3 = "MTDYTDIntraYGC";
            ReportData.ReportDataSetName4 = "ProductivityGraph";

            //ReportData.ReportDataSetName = "DailyProdSummary";
            //ReportData.ReportDataSetName2 = "DailyProdSummaryMTD";
            //ReportData.ReportDataSetName3 = "DailyProdSummaryIBChart";
            //ReportData.ReportDataSetName4 = "DailyProdSummaryBookingsChart";
            //ReportData.ReportDataSetName5 = "DailyProdSummaryInflowsChart";
            //ReportData.ReportDataSetName6 = "DailyProdSummaryMTDIntraYGC";
            //ReportData.ReportDataSetName7 = "DailyProdSummaryYTDIntraYGC";
            ReportData.ReportParameters.Clear();
            //strPeriodCovered = model.DateFrom.ToShortDateString() + " - " + model.DateTo.ToShortDateString();
            strPeriodCovered = string.Empty;
            ReportData.ReportParameters.Clear();
            paramReport = new List<Parameter>();

            paramReport.Add(new Parameter { ParameterName = "Year", Value = selectedYear });

            //ReportData.ReportParameters.Clear();
            ReportData.ReportParameters.AddRange(paramReport);

            //GenerateViewBagsDailyProdSummary();
            ViewBag.DataGenerated = true;
            //return View();
            ViewBag.Message = "Welcome to CRS";
            var temp = ProductivityReportManager.GetMTDYTDSummary().FirstOrDefault();
            var jsonResult = Json(temp, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        } 
        #endregion

        #region GetStatusTypes
        public JsonResult GetStatusTypes()
        {
            var model = new Object();
            model = ProductivityReportManager.GetStatusTypes();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        #region GetReportTypes
        public JsonResult GetReportTypes()
        {
            string userRole = SessionWrapper.CurrentUser.RoleName;
            var model = new Object();
            model = ProductivityReportManager.GetReportTypes(userRole);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion
      
    }
}

