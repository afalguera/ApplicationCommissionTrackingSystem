using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using MvcApplication1.Models;
using CRS.Helpers;
using CRS.BusinessEntities.Reports;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ExtensionController : Controller
    {
        private static string dateRange { get; set; }
        private static List<Extension> extSummaryList;
        private static List<Extension> extDetailList;
        private static int totalCount;
        //
        // GET: /Extension/

        //Initial Load show view
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

                return RedirectToAction("Login", "Account?" + "Extension/Index");
            }
        }
        #endregion

        //Show search results
        // GET: /ApplicationStatus/
        #region GetApplicationStatusResults
        [HttpGet]
        public ActionResult GetExtensionResults(ExtensionParam param)
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
                param.SourceCode = param.SourceCode ?? string.Empty;
                param.ApplicationNo = param.ApplicationNo ?? string.Empty;
                param.ApplicantFullName = param.ApplicantFullName ?? string.Empty;
                param.ExtensionType = param.ExtensionType ?? string.Empty;
                param.ReportType = param.ReportType ?? string.Empty;
                strPeriodCovered = param.DateFrom + " - " + param.DateTo;
                //string channelCode = SessionWrapper.CurrentUser.Channel ?? string.Empty;
                //string regionCode = SessionWrapper.CurrentUser.RegionCode ?? string.Empty;
                //string areaCode = SessionWrapper.CurrentUser.AreaCode ?? string.Empty;
                //string districtCode = SessionWrapper.CurrentUser.DistrictCode ?? string.Empty;
                //string branchCode = SessionWrapper.CurrentUser.BranchCode ?? string.Empty;
                string channelCode = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.Channel)
                               ? SessionWrapper.CurrentUser.Channel
                               : (param.ChannelCode ?? string.Empty);
                string regionCode = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.RegionCode)
                                ? SessionWrapper.CurrentUser.RegionCode
                                : (param.RegionCode ?? string.Empty);
                string areaCode = string.Empty;
                string districtCode = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.DistrictCode)
                                ? SessionWrapper.CurrentUser.DistrictCode
                                : (param.DistrictCode ?? string.Empty);
                string branchCode = !String.IsNullOrEmpty(SessionWrapper.CurrentUser.BranchCode)
                                ? SessionWrapper.CurrentUser.BranchCode
                                : (param.BranchCode ?? string.Empty);
                string referrorCode = SessionWrapper.CurrentUser.ReferrorCode ?? string.Empty;
                           
                param.ReferrorName = param.ReferrorName != null ? param.ReferrorName : string.Empty;
                param.CardBrandCode = param.CardBrandCode != null ? param.CardBrandCode : string.Empty;
                param.CardTypeCode = param.CardTypeCode != null ? param.CardTypeCode : string.Empty;

                if (param.ReportType == "D")
                {
                    strPeriodCovered = param.DateFrom;
                }

                model = ExtensionManager.GetList(param.DateFrom,
                                                    param.DateTo,
                                                    param.SourceCode,
                                                    param.ApplicationNo,
                                                    param.ApplicantFullName,
                                                    param.ExtensionType,
                                                    param.IsSummary,
                                                    param.IsReferror,
                                                    channelCode,
                                                    regionCode,
                                                    areaCode,
                                                    districtCode,
                                                    branchCode,
                                                    referrorCode,
                                                    param.ReferrorName,
                                                    param.CardBrandCode,
                                                    param.CardTypeCode
                                                    ).ToList();

                ReportDataContents<Extension>.Results = model as List<Extension>;
                if (param.IsSummary)
                {
                    extSummaryList = ReportDataContents<Extension>.Results;
                    totalCount = extSummaryList.Sum(x => x.ApplicationCount);
                }
                else
                {
                    extDetailList = ReportDataContents<Extension>.Results;
                    totalCount = extDetailList.Count();
                }
                dateRange = strPeriodCovered;
                ViewBag.Message = "Welcome to CRS";
                var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                return RedirectToAction("Login", "Account?" + "ApplicationStatus/Index");
            }
        }
        #endregion

        #region GetTotalCount
        public JsonResult GetTotalCount()
        {
            var totalCnt = totalCount.ToString("n0");
            totalCnt = !string.IsNullOrEmpty(totalCnt) ? totalCnt : "0";
            return Json(new { totalCount = totalCnt }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ASPXView
        public ActionResult ASPXView(bool isSummary)
        {
            ReportDataContents<Extension>.Results = isSummary ? extSummaryList : extDetailList;
            
            if (ReportDataContents<Extension>.Results.Count > 0)
            {
                ReportData.ReportType = "EXT";
                ReportData.ReportDataSetName = "Extension";
                ReportData.ReportParameters.Clear();
                ReportData.ReportPath = isSummary
                                        ? Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.ExtensionSummary.GetDescription())
                                       : Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.ExtensionDetails.GetDescription())
                                       ;
                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            var jsonResult = Json(ReportDataContents<Extension>.Results, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

    }
}
