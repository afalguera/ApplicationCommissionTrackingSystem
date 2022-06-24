using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CRS.Bll
{
    public class ApplicationStatusManager
    {

        #region Fields
        private static List<string> fileNames; 
        #endregion

        #region Application Status List
        public static IEnumerable<ApplicationStatus> GetList(string dateFrom,
                                                             string dateTo,
                                                             string sourceCode,
                                                             string applicationNo,
                                                             string fullname,
                                                             string statusCode,
                                                             bool isSummary,
                                                             bool isReferror,
                                                             string channelCode,
                                                             string regionCode,
                                                             string areaCode,
                                                             string districtCode,
                                                             string branchCode,
                                                             string referrorCode,
                                                             string lastName,
                                                             string firstName,
                                                             string middleName,
                                                             string dateOfBirth,
                                                             string referrorName,
                                                             string cardBrandCode,
                                                             string cardTypeCode
                                                        )
        {
            
            var list = ApplicationStatusDB.GetApplicationStatusList(dateFrom,
                                                                dateTo,
                                                                sourceCode,
                                                                applicationNo,
                                                                fullname,
                                                                statusCode,
                                                                isSummary,
                                                                isReferror,
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
                                                                referrorName,
                                                                cardBrandCode,
                                                                cardTypeCode
                                                                );
            if (isSummary) {
                list = (from i in list
                        group i by new
                        {
                            i.SourceCode,
                            i.CardBrand,
                            i.CardType,
                            i.Status,
                            i.DateStatus,
                            i.ReferrorCode,
                            i.ReferrorName,
                            i.ChannelName,
                            i.BranchName,
                            i.ApplicationCount
                        } into grp
                        select new ApplicationStatus
                        {
                            SourceCode = grp.Key.SourceCode,
                            CardBrand = grp.Key.CardBrand,
                            CardType = grp.Key.CardType,
                            Status = grp.Key.Status,
                            DateStatus = grp.Key.DateStatus,
                            ReferrorCode = grp.Key.ReferrorCode,
                            ReferrorName = grp.Key.ReferrorName,
                            ChannelName = grp.Key.ChannelName,
                            BranchName = grp.Key.BranchName,
                            ApplicationCount = grp.Sum(x=> x.ApplicationCount)
                        }).ToList();
            }

            if (!isReferror)
            {
                foreach (var item in list)
                {
                    item.Status = GetStatusDescription(item.Status);
                }
            }

            return list;
        } 
        #endregion

        #region GetStatusTypes
        public static IEnumerable<StatusType> GetStatusTypes()
        {
            return ApplicationStatusDB.GetStatusType();
        } 
        #endregion

        #region GetCardBrands
        public static IEnumerable<BusinessEntities.CardBrand> GetCardBrands()
        {
            return ApplicationStatusDB.GetCardBrands();
        } 
        #endregion

        #region GetCardTypes
        public static IEnumerable<BusinessEntities.CardType> GetCardTypes(string cardBrandCode)
        {
            return ApplicationStatusDB.GetCardTypes(cardBrandCode);
        } 
        #endregion

        #region Send Email
        public static bool SendEmail(string networkHost, int port, string userName, string userPswd,
                string sender, string recipient, string subject, string bodyHtmlFilePath, string bodyHtmlFileName,
                string htmlPath, string fileLocation, ApplicationStatus appStatusParam, string currentUser)
        {
            bool isSent = false;

            var files =
             new DirectoryInfo(fileLocation)
                 .GetFiles("*", SearchOption.TopDirectoryOnly)
                 .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                 .Select(f => new FilesStatus(f))
                 .ToArray();

            fileNames = files.Where(x=> x.name.StartsWith(currentUser)).Select(x => x.name).ToList();

            string strRemarks = !String.IsNullOrEmpty(appStatusParam.Remarks) ? appStatusParam.Remarks.Replace("\n", "<br/>") : string.Empty;
            string strReasons = !String.IsNullOrEmpty(appStatusParam.Reasons) ? appStatusParam.Reasons.Replace("\n", "<br/>") : string.Empty;
            //with template
            Dictionary<string, string> templateKeys = new Dictionary<string, string>();
            templateKeys.Add("$EmailDate$", DateTime.Now.ToString("f"));
            templateKeys.Add("$ApplicantName$", appStatusParam.ApplicantFullName ?? string.Empty );
            templateKeys.Add("$ApplicationNo$", appStatusParam.ApplicationNo ?? string.Empty);
            templateKeys.Add("$SourceCode$", appStatusParam.SourceCode ?? string.Empty);
            templateKeys.Add("$EncodedDate$", appStatusParam.DateEncoded ?? string.Empty);
            templateKeys.Add("$StatusDate$", appStatusParam.DateStatus ?? string.Empty);
            templateKeys.Add("$CardBrand$", appStatusParam.CardBrand ?? string.Empty);
            templateKeys.Add("$CardType$", appStatusParam.CardType ?? string.Empty);
            templateKeys.Add("$Reasons$", strReasons);
            templateKeys.Add("$Remarks$", strRemarks);
            templateKeys.Add("$User$", currentUser);
            bodyHtmlFileName = htmlPath + @"\" + bodyHtmlFileName;
            string strTemplateHtml = File.ReadAllText(bodyHtmlFileName);
            foreach (KeyValuePair<string, string> pair in templateKeys)
            {
                strTemplateHtml = strTemplateHtml.Replace(pair.Key, templateKeys[pair.Key]);
            }
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage();
            client.EnableSsl = false;
            client.Timeout = int.MaxValue;
            msg.From = new MailAddress(sender);
            msg.To.Add(new MailAddress(recipient));
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = strTemplateHtml;

            foreach (var item in fileNames)
            {
                string fileLoc = fileLocation + item;
                msg.Attachments.Add(new System.Net.Mail.Attachment(fileLoc));
            }
            try
            {
                client.Send(msg);
                isSent = true;

            }
            catch (Exception ex)
            {
                isSent = false;
            }
            finally
            {
                // cleanup
                msg.Dispose();
                client = null;
                foreach (var file in fileNames)
                {
                    string actualFile = fileLocation + file;
                    if (File.Exists(actualFile))
                    {
                        File.Delete(actualFile);
                    }
                }
            }
            return isSent;
        } 
        #endregion

        private static string GetStatusDescription(string statusCode)
        {
            string strDesc = string.Empty;
            switch (statusCode)
            {
                case "Approved":
                    strDesc = "Congratulations! Your card will be delivered in fifteen (15) working days.*";
                    break;
                case "Rejected":
                    strDesc = "We regret that we are unable to issue a card due to an existing acceptance criteria.";
                    break;
                case "In Process":
                    strDesc = "Application is still being processed.";
                    break;
                default:
                    strDesc = statusCode;
                    break;

            }
            return strDesc;
        }
    } 
}