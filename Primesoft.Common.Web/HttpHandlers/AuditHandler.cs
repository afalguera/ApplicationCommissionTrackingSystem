using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Primesoft.Common.Utilities;
using Primesoft.Common.Web.Configurations;

namespace Primesoft.Common.Web.HttpHandlers
{   
    public class AuditHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var bAuthenticated = context.User.Identity.IsAuthenticated;

            String tableName = String.Empty;
            Boolean showTrail = false;
            Int32 pageIndex = 1;
            Int32 rowsPerPage = 50;

            if (bAuthenticated)
            {
                if (context.Request.QueryString["st"] != null)
                {
                    Int16 convertedValue = 0;
                    Int16.TryParse(context.Request.QueryString["st"], out convertedValue);
                    showTrail = convertedValue == 1 ? true : false;
                }

                String headerTitle = showTrail ? "Trail" : "History";

                if (context.Request.QueryString["pi"] != null)
                {
                    Int32.TryParse(context.Request.QueryString["pi"], out pageIndex);
                }


                if (context.Request.QueryString["rpp"] != null)
                {
                    Int32.TryParse(context.Request.QueryString["rpp"], out rowsPerPage);
                }

                if (context.Request.RawUrl.Contains(".audit"))
                {
                    HttpRequest request = context.Request;
                    if (request != null)
                    {
                        tableName = String.IsNullOrEmpty(request.FilePath) ? String.Empty : request.FilePath.Replace("/", String.Empty).Replace(".audit", String.Empty);

                        if (!String.IsNullOrEmpty(tableName))
                        {
                            StringBuilder builder = new StringBuilder();

                            builder.Append("<html>");
                            builder.Append("<head><style type=\"text/css\">");
                            builder.AppendLine(".auditTable { border: 1px solid #DFDFDF; background-color: #F9F9F9; width: 100%; -moz-border-radius: 3px; -webkit-border-radius: 3px; border-radius: 3px; font-family: Tahoma,Arial,Verdana,Times,serif; color: #333;}");
                            builder.AppendLine(".auditTable td, .auditTable th { border-top-color: white; border-bottom: 1px solid #DFDFDF; color: #555;}");
                            builder.AppendLine(".auditTable th {border: 1px solid #D3CFCF; background-color: #D3CFCF; text-shadow: rgba(255, 255, 255, 0.796875) 0px 1px 0px; font-family: Tahoma,Arial,Verdana,Times,serif; padding: 7px 7px 8px; text-align: left; line-height: 1.3em; font-size: 12px;}");
                            builder.AppendLine(".auditTable td { font-size: 10px; padding: 4px 7px 2px; vertical-align: top; }");
                            builder.AppendLine(".link {font-family: Tahoma,Arial,Verdana,Times,serif; font-size: 12px; text-decoration: none; }");
                            builder.AppendLine(".pagination { position: absolute; top: 10; right: 10;}");
                            builder.AppendLine(".pager { text-decoration: none; font: menu;display: inline-block; padding: 2px 8px; background: ButtonFace; color: ButtonText; border-style: solid; border-width: 2px; border-color: ButtonHighlight ButtonShadow ButtonShadow ButtonHighlight;}");
                            builder.AppendLine(".pager:active {border-color: ButtonShadow ButtonHighlight ButtonHighlight ButtonShadow;}");
                            builder.AppendLine(".auditSubHeader {font-family: Tahoma,Arial,Verdana,Times,serif; font-size: 12px; font-weight: bold;}</style></head>");
                            builder.AppendLine("<body>");
                            String urlToUse = String.Format(context.Request.Url.AbsolutePath + "?st={0}", showTrail ? "0" : "1");

                            builder.AppendLine("Table to Audit: <select onchange=\"window.open(this.value,'_self','');\">");
                            builder.AppendLine(RenderSelection(context, tableName));
                            builder.AppendLine("</select><br />");

                            if (String.Compare(tableName, "Audit", true) != 0)
                            {
                                builder.AppendLine(String.Format("<span class=\"auditSubHeader\">{0}</span> <a class=\"link\" href=\"{4}\" title=\"This will show the {1} of {2}\">[ Show {3} ]</a><hr />", headerTitle, headerTitle.ToLower(), tableName, showTrail ? "History" : "Trail", urlToUse));

                                builder.AppendLine(RenderAudit(context, tableName, !showTrail, pageIndex, rowsPerPage));
                            }
                            builder.AppendLine("</body>");
                            builder.AppendLine("</html>");

                            context.Response.Write(builder.ToString());
                        }
                    }
                }
            }
            else
            {
                throw new HttpException(404, "HTTP/1.1 404 Not Found");
            }

          
        }

        private String RenderSelection(HttpContext context, String selected)
        {
            StringBuilder builder = new StringBuilder();
            List<Dictionary<String, String>> tables = AdoUtility<String>.RetrieveGenericItems(WebConfigApplicationSetting.ConnectionString, "RetrieveHistoryTables");
            if (tables != null && tables.Any())
            {
                if (String.Compare("Audit", selected, true) == 0)
                {
                    builder.AppendLine(String.Format("<option value=\"{0}\" selected>Select an item</option>", "/Audit.audit"));
                }
                else
                {
                    builder.AppendLine(String.Format("<option value=\"{0}\">Select an item</option>", "/Audit.audit"));
                }

                foreach (Dictionary<String, String> item in tables)
                {
                    foreach (KeyValuePair<String, String> pair in item)
                    {
                        if (pair.Value == selected && String.Compare("Audit", selected, true) != 0)
                        {
                            builder.AppendLine(String.Format("<option value=\"{0}\" selected>{0}</option>", String.Format("/{0}.audit", pair.Value)));                        
                        }
                        else if (String.Compare(pair.Value.Replace("History", String.Empty), selected) == 0)
                        {
                            builder.AppendLine(String.Format("<option value=\"{0}\" selected>{0}</option>", selected));                       
                        }
                        else
                        {
                            builder.AppendLine(String.Format("<option value=\"{0}\">{1}</option>", String.Format("/{0}.audit", pair.Value.Replace("History", String.Empty)), pair.Value.Replace("History", String.Empty)));
                        }
                    }
                }
            }
            return builder.ToString();
        }
        private String RenderAudit(HttpContext context, String tableName, Boolean isHistory, Int32 pageIndex = 1, Int32 rowsPerPage = 50)
        {
            Decimal totalRecord = Decimal.Zero;


            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<table class=\"auditTable\">");

            String storedProcedure = isHistory ? "RenderHistory" : "RenderTrail";

            List<SqlParameter> parameters = new List<SqlParameter> { new SqlParameter("tableName", tableName), new SqlParameter("pageIndex", pageIndex), new SqlParameter("rowsPerPage", rowsPerPage) };
            List<Dictionary<String, String>> auditItems = AdoUtility<String>.RetrieveGenericItems(WebConfigApplicationSetting.ConnectionString, storedProcedure, parameters);

            if (auditItems != null && auditItems.Any())
            {
                Boolean isHeaderRendered = false;


                foreach (Dictionary<String, String> item in auditItems)
                {
                    if (!isHeaderRendered)
                    {
                        builder.AppendLine("<thead><tr>");
                        foreach (KeyValuePair<String, String> pair in item)
                        {
                            builder.AppendFormat("<th>{0}</th>", pair.Key);
                        }
                        builder.AppendLine("</tr></thead>");
                        isHeaderRendered = true;
                    }
                    builder.AppendFormat("<tr>");
                    foreach (KeyValuePair<String, String> pair in item)
                    {
                        if (totalRecord == Decimal.Zero && pair.Key == "TotalRecord")
                        {
                            Decimal.TryParse(pair.Value, out totalRecord);
                        }

                        builder.AppendFormat("<td>{0}</td>", pair.Value);
                    }
                    builder.AppendFormat("</tr>");
                }
            }

            builder.AppendLine("</table>");

            Decimal raw = (totalRecord / (Decimal)rowsPerPage);

            Int32 noOfPages = (Int32)Math.Ceiling(raw);
            StringBuilder paginationLayout = new StringBuilder();

            String urlToUse = String.Format(context.Request.Url.AbsolutePath + "?st={0}", isHistory ? "0" : "1");

            if (pageIndex == 1 && noOfPages > 1)
            {
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go to the next page\" />Next</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex + 1, rowsPerPage)));
            }
            else if (pageIndex > 1 && pageIndex < noOfPages)
            {
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go back to the previous page\">Previous</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex - 1, rowsPerPage)));
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go to the next page\" />Next</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex + 1, rowsPerPage)));
            }
            else if (noOfPages > 1 && pageIndex != noOfPages)
            {
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go back to the previous page\">Previous</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex - 1, rowsPerPage)));
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go to the next page\" />Next</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex + 1, rowsPerPage)));
            }
            else if (noOfPages > 1 && pageIndex == noOfPages)
            {
                paginationLayout.Append(String.Format("<a class=\"pager\" href=\"{0}\" title=\"Go back to the previous page\">Previous</a>", String.Format(urlToUse + "&pi={0}&rpp={1}", pageIndex - 1, rowsPerPage)));
            }
            builder.AppendLine(String.Format("<div class=\"pagination\">{0}</div>", paginationLayout.ToString()));

            return builder.ToString();
        }
        #endregion
    }
}

