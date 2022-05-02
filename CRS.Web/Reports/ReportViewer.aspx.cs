using CRS.BusinessEntities;
using CRS.BusinessEntities.Reports;
using CRS.Helpers;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRS.Helper;

namespace MvcApplication1.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        
        {
            //plain
            if (!Page.IsPostBack)
            {
                int currUserId = SessionWrapper.CurrentUser != null
                                            ? SessionWrapper.CurrentUser.ID.AsInt() : 0;
                if (currUserId > 0 && !string.IsNullOrEmpty(ReportData.ReportPath))
                {
                    ACTSReportViewer.Reset();
                    ACTSReportViewer.LocalReport.DataSources.Clear();
                    ACTSReportViewer.LocalReport.ReportPath = ReportData.ReportPath;
                    ACTSReportViewer.LocalReport.EnableExternalImages = true;

                    //setting the parameters for the report
                    if (ReportData.ReportParameters != null)
                    {
                        foreach (var item in ReportData.ReportParameters)
                        {
                            ACTSReportViewer.LocalReport.SetParameters(new ReportParameter
                                    (item.ParameterName, item.Value));
                        }
                    }
                    //dataSource
                    //ACTSReportViewer.LocalReport.DataSources.Add(new ReportDataSource
                    //                        (ReportData.DataSetName, dataSetList));
                    ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType));
                    //if (ReportData.ReportType2.Length > 0) 
                    if (!string.IsNullOrEmpty(ReportData.ReportType2)) 
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType2,2));

                    }
                    if (!string.IsNullOrEmpty(ReportData.ReportType3))
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType3, 3));

                    }
                    if (!string.IsNullOrEmpty(ReportData.ReportType4))
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType4, 4));

                    }
                    if (!string.IsNullOrEmpty(ReportData.ReportType5))
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType5, 5));

                    }
                    if (!string.IsNullOrEmpty(ReportData.ReportType6))
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType6, 6));

                    }
                    if (!string.IsNullOrEmpty(ReportData.ReportType7))
                    {
                        ACTSReportViewer.LocalReport.DataSources.Add(GetDataSource(ReportData.ReportType7, 7));

                    }
                    
                    ACTSReportViewer.LocalReport.Refresh();
                    //ReportData.ReportParameters.Clear();
                }
                else
                {
                    Response.Redirect("/", false);
                }
            }
        }

        private ReportDataSource GetDataSource(string reportType)
        {
            return GetDataSource(reportType, 0);
        }
        
        private ReportDataSource GetDataSource(string reportType, int DataSetNum)
        {
            ReportDataSource rpSource = new ReportDataSource();
            switch (reportType)
            {
                case "AW":
                    rpSource.Value = ReportDataContents<ApplicationStatus>.Results;
                    ReportDataContents<ApplicationStatus>.Results = null;
                    break;
                case "PROD":
                    rpSource.Value = ReportDataContents<ProductivityReport>.Results;
                    break;
                case "PRODYEARLY":
                    rpSource.Value = ReportDataContents<ProductivityReportYearly>.Results;
                    break;
                case "PRODQUARTERLY":
                    rpSource.Value = ReportDataContents<ProductivityReportQuarterly>.Results;
                    break;
                case "PRODMONTHLY":
                    rpSource.Value = ReportDataContents<ProductivityReportMonthly>.Results;
                    break;
                case "PRODWEEKLY":
                    rpSource.Value = ReportDataContents<ProductivityReportWeekly>.Results;
                    break;
                case "REJECTEDSUMMARYWEEKLY":
                    rpSource.Value = ReportDataContents<RejectedSummaryReportWeekly>.Results;
                    break;
                case "REJECTEDSUMMARYMONTHLY":
                    rpSource.Value = ReportDataContents<RejectedSummaryReportMonthly>.Results;
                    break;
                case "PRODDAILY":
                    rpSource.Value = ReportDataContents<ProductivityReportDaily>.Results;
                    break;
                case "REDEMPTION":
                    rpSource.Value = ReportDataContents<RedemptionReport>.Results;
                    break;
                case "OUTRIGHTREJECTED":
                    rpSource.Value = ReportDataContents<OutrightRejectedReport>.Results;
                    break;
                case "INCOMPLETEWEEKLY":
                    rpSource.Value = ReportDataContents<IncompleteWeeklyReport>.Results;
                    break;
                case "INCOMPLETEDAILY":
                    rpSource.Value = ReportDataContents<IncompleteDailyReport>.Results;
                    break;
                case "INCOMPLETEMONTHLY":
                    rpSource.Value = ReportDataContents<IncompleteMonthlyReport>.Results;
                    break;
                case "INCOMPLETEOTHERS":
                    rpSource.Value = ReportDataContents<IncompleteOthersReport>.Results;
                    break;
                case "INCOMPLETEQUARTERLY":
                    rpSource.Value = ReportDataContents<IncompleteQuarterlyReport>.Results;
                    break;
                case "INCOMPLETEDETAIL":
                    rpSource.Value = ReportDataContents<IncompleteDetailReport>.Results;
                    break;
                case "PRODDETAIL":
                    rpSource.Value = ReportDataContents<ProductivityReportDetail>.Results;
                    break;
                case "DAILYPRODSUMMARY":
                    rpSource.Value = ReportDataContents<DailyProdSummary>.Results;
                    break;
                case "DAILYPRODSUMMARYMTD":
                    rpSource.Value = ReportDataContents<DailyProdSummaryMTD>.Results;
                    break;
                case "DAILYPRODSUMMARYIBCHART":
                    rpSource.Value = ReportDataContents<DailyProdSummaryInflowsBookingsChart>.Results;
                    break;
                case "DAILYPRODSUMMARYBOOKINGSCHART":
                    rpSource.Value = ReportDataContents<DailyProdSummaryBookingsChart>.Results;
                    break;
                case "DAILYPRODSUMMARYINFLOWSCHART":
                    rpSource.Value = ReportDataContents<DailyProdSummaryInflowsChart>.Results;
                    break;
                case "DAILYPRODSUMMARYMTDINTRAYGC":
                    rpSource.Value = ReportDataContents<DailyProdSummaryMTDIntraYGC>.Results;
                    break;
                case "DAILYPRODSUMMARYYTDINTRAYGC":
                    rpSource.Value = ReportDataContents<DailyProdSummaryYTDIntraYGC>.Results;
                    break;
                case "CPAREPORT":
                    rpSource.Value = ReportDataContents<CPAReport>.Results;
                    break;
                case "CPAREPORTINPUTSAPPPCH":
                    rpSource.Value = ReportDataContents<CPAReportInputsAppPCH>.Results;
                    break;
                case "CPAREPORTSUMMARYAPPROVALRATE":
                    rpSource.Value = ReportDataContents<CPAReportSummaryApprovalRate>.Results;
                    break;
                case "CPAREPORTSUMMARYACTUALAPPROVALRATE":
                    rpSource.Value = ReportDataContents<CPAReportSummaryActualApprovalRate>.Results;
                    break;
                case "CPAREPORTSUMMARYAPPROVALS":
                    rpSource.Value = ReportDataContents<CPAReportSummaryApprovals>.Results;
                    break;
                case "CPAREPORTSUMMARYCOMMISSION":
                    rpSource.Value = ReportDataContents<CPAReportSummaryCommission>.Results;
                    break;
                case "CPAREPORTINPUTSAPPSIMUL":
                    rpSource.Value = ReportDataContents<CPAReportInputsAppSimul>.Results;
                    break;
                case "CPAREPORTINPUTSREJECTS":
                    rpSource.Value = ReportDataContents<CPAReportInputsRejects>.Results;
                    break;
                case "CPAREPORTINPUTSCOMMISSIONS":
                    rpSource.Value = ReportDataContents<CPAReportInputsCommissions>.Results;
                    break;
                case "EF":
                    rpSource.Value = ReportDataContents<EAPR>.Results;
                    break;
                case "EAPRForm":
                    rpSource.Value = ReportDataContents<EAPR>.Results;
                    break;
                case "COMMDASH":
                    rpSource.Value = ReportDataContents<CommissionDashboard>.Results;
                    break;
                case "EXT":
                    rpSource.Value = ReportDataContents<Extension>.Results;
                    break;
                case "EAPRCH":
                    rpSource.Value = ReportDataContents<EAPR>.Results;
                    break;
                case "PRODUCTIVITY":
                    rpSource.Value = ReportDataContents<ProductivityReportEntity>.Results;
                    ReportDataContents<ProductivityReportEntity>.Results = null;
                    break;
                case "TOPREJECTED":
                    rpSource.Value = ReportDataContents<RejectedReportEntity>.Results;
                    break;
                case "MTDYTDSummary":
                    rpSource.Value = ReportDataContents<MTDYTDSummary>.Results;
                    break;
                case "MTDYTDDetails":
                    rpSource.Value = ReportDataContents<MTDYTDDetails>.Results;
                    break;
                case "MTDYTDIntraYGC":
                    rpSource.Value = ReportDataContents<MTDYTDIntraYGC>.Results;
                    break;
               case "ProductivityGraph":
                    rpSource.Value = ReportDataContents<ProductivityGraph>.Results;
                    break;
                default:
                    break;
            }

            if (DataSetNum == 2)
            {
                rpSource.Name = ReportData.ReportDataSetName2;
            }
            else if (DataSetNum == 3)
            {
                rpSource.Name = ReportData.ReportDataSetName3;
            }
            else if (DataSetNum == 4)
            {
                rpSource.Name = ReportData.ReportDataSetName4;
            }
            else if (DataSetNum == 5)
            {
                rpSource.Name = ReportData.ReportDataSetName5;
            }
            else if (DataSetNum == 6)
            {
                rpSource.Name = ReportData.ReportDataSetName6;
            }
            else if (DataSetNum == 7)
            {
                rpSource.Name = ReportData.ReportDataSetName7;
            }
            else
            {
                rpSource.Name = ReportData.ReportDataSetName;
            }
            
           
            return rpSource;
        }
    }
}