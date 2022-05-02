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
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Web.Hosting;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    [AllowApplicant]
    public class ApplicationStatusController : Controller
    {
        private static string dateRange { get; set; }
        private static List<ApplicationStatus> appStatusSummaryList;
        private static List<ApplicationStatus> appStatusDetailList;
        private static int totalCount;
        #region StorageRoot
        private static string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Files/")); } //Path should! always end with '/'
        }

        #endregion
        
        #region Index
        //Initial Load show view
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (SessionWrapper.UserType == UserType.Referror || SessionWrapper.UserType == UserType.Cardholder)
            {
                if (SessionWrapper.UserType == UserType.Referror)
                {
                    ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
                }
                //delete before attachment
                var files =
                    new DirectoryInfo(StorageRoot)
                        .GetFiles("*", SearchOption.TopDirectoryOnly)
                        .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                        .Select(f => new FilesStatus(f))
                        .ToArray();

                string username = string.Empty;

                if (SessionWrapper.UserType == UserType.Referror)
                {
                    username = SessionWrapper.CurrentUser.UserName + SessionWrapper.CurrentUser.ID;
                }
                else
                {
                    string bday = SessionWrapper.CurrentUser.DateOfBirth.ToShortDateString().Replace("/", string.Empty); 
                    username = SessionWrapper.CurrentUser.LastName
                   + SessionWrapper.CurrentUser.FirstName
                   + (SessionWrapper.CurrentUser.MiddleName ?? string.Empty)
                   + bday; 
                }
                
              
                var fileNames = files.Where(x => x.name.StartsWith(username)).Select(x => x.name).ToList();

                if (fileNames.Any())
                {
                    foreach (var item in fileNames)
                    {
                        Delete(item);
                    }
                }
                //end
                             
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account?" + "ApplicationStatus/Index");
            }
        } 
        #endregion
      
        #region GetApplicationStatusResults
        //Show search results
        // GET: /ApplicationStatus/
        //mvc approach
        //ViewBag.ApplicationStatus = model;
        [HttpGet]
        public ActionResult GetApplicationStatusResults(ApplicationStatusParam param)
        {
            if (SessionWrapper.UserType == UserType.Referror || SessionWrapper.UserType == UserType.Cardholder)
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
                param.StatusCode = param.StatusCode ?? string.Empty;
                param.ReportType = param.ReportType ?? string.Empty;
                strPeriodCovered = param.DateFrom + " - " + param.DateTo;
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
                string lastName = SessionWrapper.CurrentCardHolder != null
                               ? SessionWrapper.CurrentCardHolder.LastName.ToString()
                               : string.Empty;
                string firstName = SessionWrapper.CurrentCardHolder != null
                              ? SessionWrapper.CurrentCardHolder.FirstName.ToString()
                              : string.Empty;
                string middleName = SessionWrapper.CurrentCardHolder != null
                              ? SessionWrapper.CurrentCardHolder.MiddleName.ToString()
                              : string.Empty;
                string dateOfBirth = SessionWrapper.CurrentCardHolder != null
                             ? String.Format("{0:MM/dd/yyyy}", SessionWrapper.CurrentCardHolder.DateOfBirth)
                             : string.Empty;

                param.ReferrorName = param.ReferrorName != null ? param.ReferrorName : string.Empty;
                param.CardBrandCode = param.CardBrandCode != null ? param.CardBrandCode : string.Empty;
                param.CardTypeCode = param.CardTypeCode != null ? param.CardTypeCode : string.Empty;

                if (param.ReportType == "D")
                {
                    strPeriodCovered = param.DateFrom;
                }
                ReportDataContents<ApplicationStatus>.Results = null;
                model = ApplicationStatusManager.GetList(param.DateFrom,
                                                    param.DateTo,
                                                    param.SourceCode,
                                                    param.ApplicationNo,
                                                    param.ApplicantFullName,
                                                    param.StatusCode,
                                                    param.IsSummary,
                                                    param.IsReferror,
                                                    channelCode,
                                                    regionCode,
                                                    areaCode,
                                                    districtCode,
                                                    branchCode,
                                                    referrorCode,
                                                    lastName,
                                                    firstName,
                                                    middleName,
                                                    dateOfBirth,
                                                    param.ReferrorName,
                                                    param.CardBrandCode,
                                                    param.CardTypeCode
                                                    ).ToList();

                //ReportDataContents<ApplicationStatus>.Results = null;
                //ReportDataContents<ApplicationStatus>.Results = model as List<ApplicationStatus>;
                dateRange = strPeriodCovered;

                if (param.IsSummary)
                {
                    //appStatusSummaryList = ReportDataContents<ApplicationStatus>.Results;
                    appStatusSummaryList = model as List<ApplicationStatus>;
                    totalCount = appStatusSummaryList.Sum(x => x.ApplicationCount);
                }
                else
                {
                   //appStatusDetailList = ReportDataContents<ApplicationStatus>.Results;
                    appStatusDetailList = model as List<ApplicationStatus>;
                   totalCount = appStatusDetailList.Count();
                }

                ViewBag.Message = "Welcome to CRS";
                var temp = (param.IsSummary ? appStatusSummaryList : appStatusDetailList);
                var jsonResult = Json(temp, JsonRequestBehavior.AllowGet);
                model = null;
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
            else
            {
                return RedirectToAction("Login", "Account?" + "ApplicationStatus/Index");
            }
        } 
        #endregion     

        #region GetStatusTypes
        public JsonResult GetStatusTypes()
        {
            var model = new Object();
            model = ApplicationStatusManager.GetStatusTypes();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        
        #endregion

        #region GetCardBrands
        public JsonResult GetCardBrands()
        {
            var model = new Object();
            model = ApplicationStatusManager.GetCardBrands();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        } 
        #endregion

        #region GetCardTypes
        public JsonResult GetCardTypes(string cardBrandCode)
        {
            var model = new Object();
            model = ApplicationStatusManager.GetCardTypes(cardBrandCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        } 
        #endregion

        #region IsReferror
        public JsonResult IsReferror()
        {
            var isRef = SessionWrapper.UserType == UserType.Referror ? true : false;
            return Json(new { isValid = isRef }, JsonRequestBehavior.AllowGet);
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
             ReportDataContents<ApplicationStatus>.Results = isSummary ? appStatusSummaryList : appStatusDetailList;
            if (ReportDataContents<ApplicationStatus>.Results.Count > 0)
            {
                ReportData.ReportType = "AW";
                ReportData.ReportDataSetName = "ApplicationStatus";
                ReportData.ReportParameters.Clear();
                ReportData.ReportPath = isSummary
                                        ? Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.ApplicationStatusSummary.GetDescription())
                                       : Server
                                       .MapPath(ReportTypes.ReportTypesAndPath.ApplicationStatusDetails.GetDescription())
                                       ;
                List<Parameter> paramReport = new List<Parameter>();
                paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = dateRange });
                ReportData.ReportParameters.AddRange(paramReport);
            }
            var temp = (isSummary ? appStatusSummaryList : appStatusDetailList).FirstOrDefault();
            var jsonResult = Json(temp, JsonRequestBehavior.AllowGet);
            appStatusSummaryList = null;
            appStatusDetailList = null;
            //jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
          
        } 
        #endregion

        #region AppStatusDetail 
        public ActionResult AppStatusDetail()
        {
            return View();
        } 
        #endregion

        #region SendEmail
        [HttpPost]
        public JsonResult SendEmail(ApplicationStatus appStatusParam)
        {
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            string networkHost = mailSettings != null ? mailSettings.Smtp.Network.Host.ToString() : string.Empty;
            int port = mailSettings != null ? mailSettings.Smtp.Network.Port : 0;
            string userName = mailSettings != null ? mailSettings.Smtp.Network.UserName : string.Empty;
            string userPswd = mailSettings != null ? mailSettings.Smtp.Network.Password : string.Empty;
            string sender = mailSettings != null ? mailSettings.Smtp.From : string.Empty;
            string recipient = ConfigurationManager.AppSettings.Get("EmailApplicationStatusRecipient") ?? "";
            string subject = ConfigurationManager.AppSettings.Get("EmailApplicationStatusSubject") ?? "";
            string bodyHtmlFilePath = ConfigurationManager.AppSettings.Get("EmailApplicationStatusBodyHtmlFormatFilePath") ?? "";
            string bodyHtmlFileName = ConfigurationManager.AppSettings.Get("EmailApplicationStatusBodyHtmlFormatFileName") ?? "";
            string htmlPath = HostingEnvironment.MapPath(bodyHtmlFilePath);
            string currentUser = string.Empty;

            if (SessionWrapper.UserType == UserType.Referror)
            {
                currentUser = SessionWrapper.CurrentUser.UserName +  SessionWrapper.CurrentUser.ID.ToString();
            }
            else
            {
                string bday = SessionWrapper.CurrentUser.DateOfBirth.ToShortDateString().Replace("/", string.Empty);
                currentUser = SessionWrapper.CurrentUser.LastName
               + SessionWrapper.CurrentUser.FirstName
               + (SessionWrapper.CurrentUser.MiddleName ?? string.Empty)
               + bday; 
            }

           
            var isRef = ApplicationStatusManager.SendEmail(networkHost, port, userName, userPswd, sender,
                                        recipient, subject, bodyHtmlFilePath, bodyHtmlFileName,
                                        htmlPath, StorageRoot, appStatusParam, currentUser);
            return Json(new { isValid = isRef }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Delete(string id)
        {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        } 
        #endregion

        #region Download
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(string id)
        {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

            var context = HttpContext;

            if (System.IO.File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
                context.Response.StatusCode = 404;
        } 
        #endregion

        #region UploadFiles
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpPost]
        public ActionResult UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                var statuses = new List<ViewDataUploadFilesResult>();
                var headers = Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(Request, statuses);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses);
                }

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        } 
        #endregion

        #region EncodeFile
        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        } 
        #endregion

        #region UploadPartialFile
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot, Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new ViewDataUploadFilesResult()
            {
                name = fileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = "/ApplicationStatus/Download/" + fileName,
                delete_url = "/ApplicationStatus/Delete/" + fileName,
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullName),
                delete_type = "GET",
            });
        } 
        #endregion

        #region UploadWholeFile
        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/ApplicationStatus/Download/" + file.FileName,
                    delete_url = "/ApplicationStatus/Delete/" + file.FileName,
                    thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                    delete_type = "GET",
                });
            }
        }
        #endregion

    }           
}
