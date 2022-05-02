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
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class EAPRChannelController : Controller
    {
        private static string dateRange { get; set; }

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

                return RedirectToAction("Login", "Account?" + "EAPRChannel/Index");
            }
        }
        #endregion

        #region Get EAPR Channel Results
        [HttpGet]
        public ActionResult GetEAPRChannelResults(CommissionDashboardParam param)
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
                param.SourceCode = param.SourceCode != null ? param.SourceCode : string.Empty;
                param.ChannelCode = param.ChannelCode != null ? param.ChannelCode : string.Empty;
                param.ReportType = param.ReportType != null ? param.ReportType : string.Empty;
                strPeriodCovered = param.DateFrom + " - " + param.DateTo;
                string regionCode = SessionWrapper.CurrentUser.RegionCode != null
                                    ? SessionWrapper.CurrentUser.RegionCode.ToString()
                                    : string.Empty;
                //string areaCode = SessionWrapper.CurrentUser.AreaCode != null
                //                  ? SessionWrapper.CurrentUser.AreaCode.ToString()
                //                  : string.Empty;
                string areaCode = string.Empty;
                string districtCode = SessionWrapper.CurrentUser.DistrictCode != null
                                 ? SessionWrapper.CurrentUser.DistrictCode.ToString()
                                 : string.Empty;
                string branchCode = SessionWrapper.CurrentUser.BranchCode != null
                               ? SessionWrapper.CurrentUser.BranchCode.ToString()
                               : string.Empty;

                if (param.ReportType == "D")
                {
                    strPeriodCovered = param.DateFrom;
                }

                model = EAPRChannelManager.GetList(param.DateFrom,
                                                    param.DateTo,
                                                    param.SourceCode,
                                                    param.ChannelCode,
                                                    param.Keyword,
                                                    regionCode,
                                                    areaCode,
                                                    districtCode,
                                                    branchCode
                                                    ).ToList();

                ReportDataContents<CommissionDashboard>.Results = model as List<CommissionDashboard>;
                dateRange = strPeriodCovered;
                ViewBag.Message = "Welcome to CRS";
                var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            else
            {
                return RedirectToAction("Login", "Account?" + "EAPRChannel/Index");
            }
        }
        #endregion

        #region EAPR Form per Channel
        public ActionResult ViewEAPRChannelForm(string dateFrom, string dateTo, string eaprChannel,
                                                string reportType, bool isPerBranch, bool isDistrict)
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;
                string strPeriodCovered = string.Empty;
                isPerBranch = isPerBranch.AsBoolean();
                isDistrict = isDistrict.AsBoolean();
                model = EAPRChannelManager.GetEAPRChannelItem(dateFrom, dateTo, eaprChannel, 
                                                              reportType, isPerBranch, isDistrict);

                ReportDataContents<EAPR>.Results = model as List<EAPR>;
                strPeriodCovered = dateFrom + " - " + dateTo;
              
                if (ReportDataContents<EAPR>.Results.Count > 0)
                {
                    ReportData.ReportType = "EAPRCH";
                    ReportData.ReportDataSetName = "EAPRChannel";
                    ReportData.ReportParameters.Clear();
                    ReportData.ReportPath = Server
                                           .MapPath(ReportTypes.ReportTypesAndPath.EAPRChannelForm.GetDescription());

                    if (dateFrom == dateTo)
                    {
                        strPeriodCovered = dateFrom;
                    }

                    var requestedBy = ReportDataContents<EAPR>.Results.Select(x => x.RequestedBy).FirstOrDefault();
                    var requestedByTitle = requestedBy.Any() ? requestedBy.Select(x => x.PositionName).FirstOrDefault() : string.Empty;
                    var requestedByName = requestedBy.Any() ? requestedBy.Select(x => x.Name).FirstOrDefault() : string.Empty;

                    var checkedBy = ReportDataContents<EAPR>.Results.Select(x => x.CheckedBy).FirstOrDefault();
                    var checkedByTitle1 = checkedBy.Any() ? checkedBy.ElementAt(0).PositionName : string.Empty;
                    var checkedByName1 = checkedBy.Any() ? checkedBy.ElementAt(0).Name : string.Empty;

                    var notedBy = ReportDataContents<EAPR>.Results.Select(x => x.NotedBy).FirstOrDefault();
                    var notedByTitle = notedBy.Any() ? notedBy.Select(x => x.PositionName).FirstOrDefault() : string.Empty;
                    var notedByName = notedBy.Any() ? notedBy.Select(x => x.Name).FirstOrDefault() : string.Empty;

                    var approvedBy = ReportDataContents<EAPR>.Results.Select(x => x.ApprovedBy).FirstOrDefault();
                    var approvedByTitle1 =  approvedBy.Any() ? approvedBy.ElementAt(0).PositionName : string.Empty;
                    var approvedByName1 = approvedBy.Any() ? approvedBy.ElementAt(0).Name : string.Empty;

                    var additionalApprovedBy = ReportDataContents<EAPR>.Results.Select(x => x.AdditionalApprovedBy).FirstOrDefault();
                    var additionalApprovedByTitle1 = additionalApprovedBy.ElementAt(0).ApproverTitle;
                    var additionalAprovedByName1 = additionalApprovedBy.ElementAt(0).ApproverName;
                    var additionalApprovedByTitle2 = additionalApprovedBy.Count() > 1 ? additionalApprovedBy.ElementAt(1).ApproverTitle : " ";
                    var additionalApprovedByName2 = additionalApprovedBy.Count() > 1 ? additionalApprovedBy.ElementAt(1).ApproverName : " ";
                    var additionalApprovedByCOOCFOTitle = additionalApprovedBy.Count() > 1 ? additionalApprovedBy.ElementAt(1).ApproverTitle : " ";
                    var additionalApprovedByCOOCFOName = additionalApprovedBy.Count() > 1 ? additionalApprovedBy.ElementAt(1).ApproverName : " ";

                    string[] strCooCfoTitle = additionalApprovedByCOOCFOTitle.IndexOf("or") > 0 ?
                            additionalApprovedByCOOCFOTitle.Split(new string[] { "or" }, StringSplitOptions.None)
                            : null;
                    string[] strCooCfoName = additionalApprovedByCOOCFOName.IndexOf("or") > 0 ?
                           additionalApprovedByCOOCFOName.Split(new string[] { "or" }, StringSplitOptions.None)
                           : null;

                    var strCOOTitle = strCooCfoTitle != null ? (strCooCfoTitle.Length > 0 ? strCooCfoTitle[0]
                                                                : " ") : " ";

                    var strCFOTitle = strCooCfoTitle != null ? (strCooCfoTitle.Length > 1 ? strCooCfoTitle[1]
                                                    : " ") : " ";

                    var strCOOName = strCooCfoName != null ? (strCooCfoName.Length > 0 ? strCooCfoName[0]
                                                    : " ") : " ";

                    var strCFOName = strCooCfoName != null ? (strCooCfoName.Length > 1 ? strCooCfoName[1]
                                                    : " ") : " ";

                    var additionalApprovedByTitle4 = additionalApprovedBy.Count() > 3 ? additionalApprovedBy.ElementAt(3).ApproverTitle : " ";
                    var additionalApprovedByName4 = additionalApprovedBy.Count() > 3 ? additionalApprovedBy.ElementAt(3).ApproverName : " ";

                    string strVatable = ReportDataContents<EAPR>.Results.Select(x => x.IsVatable).FirstOrDefault() == true ? "V" : "NV";
                    string strGross = ReportDataContents<EAPR>.Results.Select(x => x.IsGross).FirstOrDefault() == true ? "Gross" : "Net";

                    List<Parameter> paramReport = new List<Parameter>();
                    paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = strPeriodCovered });
                    paramReport.Add(new Parameter { ParameterName = "RequestByTitle", Value = requestedByTitle });
                    paramReport.Add(new Parameter { ParameterName = "RequestByName", Value = requestedByName });
                    paramReport.Add(new Parameter { ParameterName = "CheckedByTitle1", Value = checkedByTitle1 });
                    paramReport.Add(new Parameter { ParameterName = "CheckedByName1", Value = checkedByName1 });
                    paramReport.Add(new Parameter { ParameterName = "NotedByTitle", Value = notedByTitle });
                    paramReport.Add(new Parameter { ParameterName = "NotedByName", Value = notedByName });
                    paramReport.Add(new Parameter { ParameterName = "ApproverTitle1", Value = approvedByTitle1 });
                    paramReport.Add(new Parameter { ParameterName = "ApproverName1", Value = approvedByName1 });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle1", Value = additionalApprovedByTitle1 });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName1", Value = additionalAprovedByName1 });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle2", Value = strCOOTitle });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName2", Value = strCOOName });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle3", Value = strCFOTitle });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName3", Value = strCFOName });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle4", Value = additionalApprovedByTitle4 });
                    paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName4", Value = additionalApprovedByName4 });
                    paramReport.Add(new Parameter { ParameterName = "GrossNet", Value = strGross });
                    paramReport.Add(new Parameter { ParameterName = "VatNonVat", Value = strVatable });

                    ReportData.ReportParameters.AddRange(paramReport);
                }

                ViewBag.Message = "Welcome to CRS";
                var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            else
            {
                return RedirectToAction("Login", "Account?" + "EAPR/Index");
            }

        }
        #endregion


        public ActionResult ASPXView()
        {
            if (ReportDataContents<CommissionDashboard>.Results.Count > 0)
            {
                ReportData.ReportType = "EAPRCH";
                ReportData.ReportDataSetName = "EAPRChannel";
                ReportData.ReportPath = Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.EAPRChannelSummary.GetDescription());

                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            ViewBag.Message = "Welcome to CRS";
            var jsonResult = Json(ReportDataContents<CommissionDashboard>.Results, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}
