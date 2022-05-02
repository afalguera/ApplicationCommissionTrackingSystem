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
    public class EAPRController : Controller
    {
        private static string dateRange { get; set; }
        private static int totalCount;
        //
        // GET: /EAPR/

        #region Initial Load
        public ActionResult Index()
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
                return View();
            }
            else
            {
                return RedirectToAction("Login", @"Account?" + "EAPR/Index");
            }
        } 
        #endregion

        #region Amount In Words

        public JsonResult GetAmountInWords(string strAmount)
        {
            strAmount = string.IsNullOrEmpty(strAmount) ? "0" : strAmount.Replace(",", string.Empty);
            decimal dAmount = strAmount != null ? strAmount.AsDecimal() : 0;
            string strConvert = NumbersToWords.DecimalToWords(dAmount);
            return Json(new { amount = strConvert }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        #region EAPR Insert, Update, Delete
        [HttpPost]
        public ActionResult EAPRCRUD(EAPR eaprItem)
        {
            eaprItem.PaymentDate = eaprItem.Mode != "DELETE" ? eaprItem.PaymentDate : DateTime.Now;
            eaprItem.ControlNo = eaprItem.ControlNo != null ? eaprItem.ControlNo : string.Empty;
            eaprItem.PayeeName = eaprItem.PayeeName != null ? eaprItem.PayeeName : string.Empty;
            eaprItem.PayeeTin = eaprItem.PayeeTin != null ? eaprItem.PayeeTin : string.Empty;
            eaprItem.OriginatingDepartment = eaprItem.OriginatingDepartment != null
                                            ? eaprItem.OriginatingDepartment
                                            : string.Empty;
            eaprItem.DepartmentCode = eaprItem.DepartmentCode != null ? eaprItem.DepartmentCode : string.Empty;
            eaprItem.Description = eaprItem.Description != null ? eaprItem.Description : string.Empty;
            eaprItem.ExpenseCategoryCode = eaprItem.ExpenseCategoryCode != null ? eaprItem.ExpenseCategoryCode : string.Empty;
            eaprItem.UserBy = SessionWrapper.CurrentUser != null
                              ? SessionWrapper.CurrentUser.UserName
                              : string.Empty;
            var bResult = EAPRManager.EARPCRUD(eaprItem);
            return Json(new { success = bResult });
        }
        
        #endregion

        #region Expense Category
        public JsonResult GetExpenseCategory()
        {
            var model = new Object();
            model = EAPRManager.GetExpenseCategory();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        } 
        #endregion

        #region Payment List
        public JsonResult GetPaymentList()
        {
            var model = new Object();
            model = EAPRManager.GetPaymentList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;

        } 
        #endregion

        #region Department List
        public JsonResult GetDepartmentList()
        {
            var model = new Object();
            model = EAPRManager.GetDepartmentList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;

        }
        #endregion

        #region Signatories List
        public JsonResult GetSignatoriesList(string positionType, string positionCode)
        {
            var model = new Object();
            model = EAPRManager.GetSignatoriesList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;

        }
        #endregion

        #region Approver List based on expense amout
        public JsonResult GetBudgetAllocationApprover(string budgetType)
        {
            var model = new Object();
            model = EAPRManager.GetBudgetAllocationApprover(budgetType);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;

        } 
        #endregion

        #region EAPR Item based on Id
        [HttpGet]
        public ActionResult GetEAPRItem(decimal eaprId)
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;
                model = EAPRManager.GetEAPRItem(eaprId);
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

        #region EAPR Form
        public ActionResult ViewEAPRForm(decimal eaprId)
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;
                string strPeriod = DateTime.Now.ToShortDateString();
                model = EAPRManager.GetEAPRItem(eaprId);
                ReportDataContents<EAPR>.Results = model as List<EAPR>;
                if (ReportDataContents<EAPR>.Results.Count >= 0)
                {
                    ReportData.ReportType = "EAPRForm";
                    ReportData.ReportDataSetName = "EAPR";
                    ReportData.ReportParameters.Clear();
                    ReportData.ReportPath = Server
                                           .MapPath(ReportTypes.ReportTypesAndPath.EAPRForm.GetDescription());

                    var requestedBy = ReportDataContents<EAPR>.Results.Select(x => x.RequestedBy).FirstOrDefault();
                    var requestedByTitle = requestedBy.Select(x => x.PositionName).FirstOrDefault();
                    var requestedByName = requestedBy.Select(x => x.Name).FirstOrDefault();

                    var checkedBy = ReportDataContents<EAPR>.Results.Select(x => x.CheckedBy).FirstOrDefault();

                    var checkedByTitle1 = checkedBy.ElementAt(0).PositionName;
                    var checkedByName1 =  checkedBy.ElementAt(0).Name;
                    //var checkedByTitle2 = checkedBy.Count() > 1 ? checkedBy.ElementAt(1).PositionName : string.Empty;
                    //var checkedByName2 =  checkedBy.Count() > 1 ? checkedBy.ElementAt(1).Name : string.Empty;

                    var notedBy = ReportDataContents<EAPR>.Results.Select(x => x.NotedBy).FirstOrDefault();
                    var notedByTitle = notedBy.Select(x => x.PositionName).FirstOrDefault();
                    var notedByName = notedBy.Select(x => x.Name).FirstOrDefault();

                    var approvedBy = ReportDataContents<EAPR>.Results.Select(x => x.ApprovedBy).FirstOrDefault();
                    var approvedByTitle1 = approvedBy != null ?  approvedBy.ElementAt(0).PositionName : string.Empty;
                    var approvedByName1 = approvedBy != null ?  approvedBy.ElementAt(0).Name : string.Empty;
                    //var approvedByTitle2 = approvedBy.Count() > 1 ? approvedBy.ElementAt(1).PositionName : string.Empty;
                    //var approvedByName2 = approvedBy.Count() > 1 ? approvedBy.ElementAt(1).Name : string.Empty;
                    //var approvedByTitle3 = approvedBy.Count() > 2 ? approvedBy.ElementAt(2).PositionName : string.Empty;
                    //var approvedByName3 = approvedBy.Count() > 2 ? approvedBy.ElementAt(2).Name : string.Empty;

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
                    //var additionalApprovedByTitle6 = additionalApprovedBy.Count() > 5 ? additionalApprovedBy.ElementAt(5).ApproverTitle : string.Empty;
                    //var additionalApprovedByName6 = additionalApprovedBy.Count() > 5 ? additionalApprovedBy.ElementAt(5).ApproverName : string.Empty;
                    //var additionalApprovedByTitle7 = additionalApprovedBy.Count() > 6 ? additionalApprovedBy.ElementAt(6).ApproverTitle : string.Empty;
                    //var additionalApprovedByName7 = additionalApprovedBy.Count() > 6 ? additionalApprovedBy.ElementAt(6).ApproverName : string.Empty;

                    string strVatable = ReportDataContents<EAPR>.Results.Select(x => x.IsVatable).FirstOrDefault() ? "V" : "NV";
                    string strGross = ReportDataContents<EAPR>.Results.Select(x => x.IsGross).FirstOrDefault() ? "Gross" : "Net";  
                     
                    List<Parameter> paramReport = new List<Parameter>();
                        paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = strPeriod });
                        paramReport.Add(new Parameter { ParameterName = "RequestByTitle", Value = requestedByTitle });           
                        paramReport.Add(new Parameter { ParameterName = "RequestByName", Value = requestedByName });
                        paramReport.Add(new Parameter { ParameterName = "CheckedByTitle1", Value = checkedByTitle1 });
                        paramReport.Add(new Parameter { ParameterName = "CheckedByName1", Value = checkedByName1 });
                        //paramReport.Add(new Parameter { ParameterName = "CheckedByTitle2", Value = checkedByTitle2 });
                        //paramReport.Add(new Parameter { ParameterName = "CheckedByName2", Value = checkedByName2 });
                        paramReport.Add(new Parameter { ParameterName = "NotedByTitle", Value = notedByTitle });
                        paramReport.Add(new Parameter { ParameterName = "NotedByName", Value = notedByName });
                        paramReport.Add(new Parameter { ParameterName = "ApproverTitle1", Value = approvedByTitle1 });
                        paramReport.Add(new Parameter { ParameterName = "ApproverName1", Value = approvedByName1 });
                        //paramReport.Add(new Parameter { ParameterName = "ApproverTitle2", Value = approvedByTitle2 });
                        //paramReport.Add(new Parameter { ParameterName = "ApproverName2", Value = approvedByName2 });
                        //paramReport.Add(new Parameter { ParameterName = "ApproverTitle3", Value = approvedByTitle3 });
                        //paramReport.Add(new Parameter { ParameterName = "ApproverName3", Value = approvedByName3 });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle1", Value = additionalApprovedByTitle1 });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName1", Value = additionalAprovedByName1 });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle2", Value = strCOOTitle });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName2", Value = strCOOName });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle3", Value = strCFOTitle });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName3", Value = strCFOName });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle4", Value = additionalApprovedByTitle4 });
                        paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName4", Value = additionalApprovedByName4 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle5", Value = additionalApprovedByTitle5 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName5", Value = additionalApprovedByName5 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle6", Value = additionalApprovedByTitle6 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName6", Value = additionalApprovedByName6 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverTitle7", Value = additionalApprovedByTitle7 });
                        //paramReport.Add(new Parameter { ParameterName = "AdditionalApproverName7", Value = additionalApprovedByName7 });
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

        #region Dummy view for report

        public ActionResult ASPXView()
        {
            //to do: reporting part of dashboard
            if (ReportDataContents<EAPR>.Results.Count > 0)
            {
                ReportData.ReportType = "EF";
                ReportData.ReportDataSetName = "EAPR";
                ReportData.ReportParameters.Clear();
                ReportData.ReportPath = Server.MapPath(ReportTypes.ReportTypesAndPath.EAPRReport.GetDescription());

                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                ReportData.ReportParameters.AddRange(paramReport);


            }
            ViewBag.Message = "Welcome to CRS";
            var jsonResult = Json(ReportDataContents<EAPR>.Results, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        
        #endregion
      
        #region EAPR List
        //Show search results
        // GET: /GetEAPRResults/
        [HttpGet]
        public ActionResult GetEAPRResults(EAPRParam param)
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
                param.ControlNo = param.ControlNo != null ? param.ControlNo : string.Empty;
                param.PayeeName = param.PayeeName != null ? param.PayeeName : string.Empty;
                param.PayeeTin = param.PayeeTin != null ? param.PayeeTin : string.Empty;
                param.OriginatingDepartment = param.OriginatingDepartment != null ? param.OriginatingDepartment : string.Empty;
                param.DepartmentCode = param.DepartmentCode != null ? param.DepartmentCode : string.Empty;
                param.ExpenseCategoryCode = param.ExpenseCategoryCode != null ? param.ExpenseCategoryCode : string.Empty;
                strPeriodCovered = param.DateFrom + " - " + param.DateTo;

                model = EAPRManager.GetList(param.DateFrom,
                                                param.DateTo,
                                                param.ControlNo,
                                                param.PayeeName,
                                                param.PayeeTin,
                                                param.OriginatingDepartment,
                                                param.DepartmentCode,
                                                param.ExpenseCategoryCode
                                            ).ToList();

                ReportDataContents<EAPR>.Results = model as List<EAPR>;
                totalCount = ReportDataContents<EAPR>.Results.Count();	
                dateRange = strPeriodCovered;
                ////to do: reporting part of dashboard
                //if (ReportDataContents<EAPR>.Results.Count > 0)
                //{
                //    ReportData.ReportType = "EF";
                //    ReportData.ReportDataSetName = "EAPR";
                //    ReportData.ReportParameters.Clear();
                //    ReportData.ReportPath = Server.MapPath(ReportTypes.ReportTypesAndPath.EAPRReport.GetDescription());

                //    List<Parameter> paramReport = new List<Parameter>();
                //    paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = strPeriodCovered });
                //    ReportData.ReportParameters.AddRange(paramReport);


                //}
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

        #region GetTotalCount
        public JsonResult GetTotalCount()
        {
            var totalCnt = totalCount.ToString("n0");
            totalCnt = !string.IsNullOrEmpty(totalCnt) ? totalCnt : "0";
            return Json(new { totalCount = totalCnt }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
