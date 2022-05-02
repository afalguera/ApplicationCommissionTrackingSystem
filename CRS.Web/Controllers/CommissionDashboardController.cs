using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Helper;
using CRS.BusinessEntities;
using CRS.Bll;
using CRS.Helpers;
using CRS.BusinessEntities.Reports;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    //[ACTSAuthorize]
    //[AjaxAuthorize]
    public class CommissionDashboardController : Controller
    {
        private static string dateRange { get; set; }
        private static List<CommissionDashboard> commSummaryList;
        private static List<CommissionDashboard> commDetailList;
        private static int totalCount;
        private static decimal totalCommAmout;
        
        #region Index
        //
        // GET: /CommissionDashboard/

        //Initial Load show view
        public ActionResult Index()
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
                return View();
            }
            else
            {

                return RedirectToAction("Login", "Account?" + "CommissionDashboard/Index");
            }
        } 
        #endregion

        #region GetCommissionDashboardResults
        //Show search results
        // GET: /GetCommissionDashboardResults/
        [HttpGet]
        public ActionResult GetCommissionDashboardResults(CommissionDashboardParam param)
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;

                string initialDateFrom = DateTime.Now.AddDays(-1).ToShortDateString();
                string initialDateTo = DateTime.Now.AddDays(-1).ToShortDateString();
                string strPeriodCovered = string.Empty;
                param.DateFrom = param.DateFrom != null ? param.DateFrom : initialDateFrom;
                param.DateTo = param.DateTo != null ? param.DateTo : initialDateTo;
                param.ReportType =  param.ReportType ?? string.Empty;
                string channelCode = param.ChannelCode ?? string.Empty;
                strPeriodCovered = param.DateFrom + " - " + param.DateTo;
                string regionCode = param.RegionCode ?? string.Empty;
                string areaCode = param.AreaCode ?? string.Empty;
                string districtCode = param.DistrictCode ?? string.Empty;
                string branchCode = param.BranchCode ?? string.Empty;
                string referrorCode = SessionWrapper.CurrentUser.ReferrorCode ?? string.Empty;
                string keyword = param.Keyword ?? string.Empty;

                if (param.ReportType == "D")
                {
                    strPeriodCovered = param.DateFrom;
                }

                //actual
                model = CommissionDashboardManager.GetList(param.DateFrom,
                                                    param.DateTo,
                                                    param.IsSummary,
                                                    channelCode,
                                                    regionCode,
                                                    areaCode,
                                                    districtCode,
                                                    branchCode,
                                                    referrorCode,
                                                    keyword
                                                    ).ToList();
                dateRange = strPeriodCovered;
                ReportDataContents<CommissionDashboard>.Results = model as List<CommissionDashboard>;
                if (param.IsSummary)
                {
                    commSummaryList = ReportDataContents<CommissionDashboard>.Results;
                    totalCount = commSummaryList.Sum(x => x.ApprovalCount);
                    totalCommAmout = commSummaryList.Sum(x => x.NetAmount);
                }
                else
                {
                    commDetailList = ReportDataContents<CommissionDashboard>.Results;
                    totalCount = commDetailList.Sum(x => x.ApprovalCount);
                    totalCommAmout = commDetailList.Sum(x => x.NetAmount);
                    //totalCount = commDetailList.Where(x => x.Status.Trim().Equals("Approved", StringComparison.CurrentCultureIgnoreCase)).Sum(x=> x.ApprovalCount);
                    //totalCommAmout = commDetailList.Where(x => x.Status.Trim().Equals("Approved", StringComparison.CurrentCultureIgnoreCase)).Sum(x => x.NetAmount);
                }


              
                ViewBag.Message = "Welcome to CRS";
                var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            else
            {
                return RedirectToAction("Login", "Account?" + "CommissionDashboard/Index");
            }
        } 
        #endregion

        #region Channel List
        public JsonResult GetChannelList()
        {
            var model = new Object();
            string channelCode = SessionWrapper.CurrentUser.Channel ?? string.Empty;
                          
            model = CommissionDashboardManager.GetChannelList(channelCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region Channel All List
        public JsonResult GetChannelAllList()
        {
            var model = new Object();
            string channelCode = SessionWrapper.CurrentUser.Channel ?? string.Empty;

            model = CommissionDashboardManager.GetChannelAllList(channelCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region Region List
        public JsonResult GetRegionList()
        {
            var model = new Object();
            string regionCode = SessionWrapper.CurrentUser.RegionCode ?? string.Empty;
            model = CommissionDashboardManager.GetRegionList(regionCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region GetTotalCount
        public JsonResult GetTotalCount()
        {
            var totalCnt = totalCount.ToString("n0");
            var totalAmt = String.Format("{0:#,###0.00}", totalCommAmout);
            totalCnt = !string.IsNullOrEmpty(totalCnt) ? totalCnt : "0";
            return Json(new { totalCount = totalCnt, totalAmount = totalAmt }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //#region Area List
        //public JsonResult GetAreaList(string regionCode)
        //{
        //    var model = new Object();
        //    string areaCode = SessionWrapper.CurrentUser.AreaCode ?? string.Empty;
        //    model = CommissionDashboardManager.GetAreaList(regionCode, areaCode);
        //    var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
        //    return jsonResult;
        //}
        //#endregion

        #region District List
        public JsonResult GetDistrictList(string regionCode, string channelCode)
        {
            var model = new Object();
            string channelCodeValue = !string.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel) ? SessionWrapper.CurrentUser.Channel : channelCode;
            string districtCode = SessionWrapper.CurrentUser.DistrictCode ?? string.Empty;
            model = CommissionDashboardManager.GetDistrictList(channelCode, regionCode, districtCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region Branch List
        public JsonResult GetBranchList(string channelCode, string districtCode)
        {
            var model = new Object();
            string branchCode = SessionWrapper.CurrentUser.BranchCode ?? string.Empty;
            string districtCodeValue = !string.IsNullOrEmpty(SessionWrapper.CurrentUser.DistrictCode) ? SessionWrapper.CurrentUser.DistrictCode : districtCode;
            string channelCodeValue = !string.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel) ? SessionWrapper.CurrentUser.Channel : channelCode;
            model = CommissionDashboardManager.GetBranchList(channelCodeValue, districtCodeValue, branchCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
   
        #region Dummy View
        public ActionResult ASPXView(bool isSummary)
        {
            ReportDataContents<CommissionDashboard>.Results = isSummary ? commSummaryList : commDetailList;

            if (ReportDataContents<CommissionDashboard>.Results.Count > 0)
            {
                ReportData.ReportType = "COMMDASH";
                ReportData.ReportDataSetName = "CommissionDashboard";
                ReportData.ReportParameters.Clear();
                ReportData.ReportPath = isSummary
                                        ? Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.CommissionDashboardSummary.GetDescription())
                                       : Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.CommissionDashboardDetails.GetDescription())
                                       ;
                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            var jsonResult = Json(ReportDataContents<CommissionDashboard>.Results, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        } 
        #endregion

    }
}
