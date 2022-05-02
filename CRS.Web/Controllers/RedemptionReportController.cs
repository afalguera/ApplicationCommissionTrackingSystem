using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.BusinessEntities.Reports;
using CRS.Models;
using CRS.Helpers;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    public class RedemptionReportController : Controller
    {
        //
        // GET: /ProductivityReport/

        [HttpGet]
        public ActionResult Index()
        {

            ViewBag.IsButtonClicked = false;
            GenerateViewBags();
            ViewBag.DataGenerated = false;
            
            var viewModel = new RedemptionReportModel();
            viewModel.DateFrom = DateTime.Now.AddMonths(-1);
            viewModel.DateTo = DateTime.Now.AddMonths(1);
           
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(RedemptionReportModel model)
        {

            ViewBag.IsButtonClicked = true;
            List<RedemptionReport> listRedemption = new List<RedemptionReport>();


            listRedemption = RedemptionReportManager.GetRedemptionReport(model.DateFrom,model.DateTo).ToList();

                  
            ReportDataContents<RedemptionReport>.Results = listRedemption as List<RedemptionReport>;
                    
            ReportData.ReportType = "REDEMPTION";
            ReportData.ReportPath = Server.MapPath(ReportTypes.ReportTypesAndPath.RedemptionDetail.GetDescription());
            ReportData.ReportDataSetName = "RedemptionReport";
                    
            string strPeriodCovered = model.DateFrom.ToShortDateString() + " - " + model.DateTo.ToShortDateString();
            List<Parameter> paramReport;
            paramReport = new List<Parameter>();
            paramReport.Add(new Parameter { ParameterName = "PeriodCovered", Value = strPeriodCovered });

            ReportData.ReportParameters.Clear();
            ReportData.ReportParameters.AddRange(paramReport);
                    
                    
            GenerateViewBags();
            ViewBag.DataGenerated = true;
            return View();

        }

        private void GenerateViewBags()
        {
            List<SelectListItem> status_items = new List<SelectListItem>();
            status_items.Add(new SelectListItem() { Text = "All/Bookings", Value = "ALL", Selected = false });
            status_items.Add(new SelectListItem() { Text = "Approved", Value = "APPROVED", Selected = false });
            status_items.Add(new SelectListItem() { Text = "Rejected", Value = "REJECTED", Selected = true });
            //status_items.Add(new SelectListItem() { Text = "In-process", Value = "CURING", Selected = false });
            status_items.Add(new SelectListItem() { Text = "In-process", Value = "INPROCESS", Selected = false });
            status_items.Add(new SelectListItem() { Text = "Incomplete", Value = "INCOMPLETE", Selected = false });
            status_items.Add(new SelectListItem() { Text = "Pre-confirmed", Value = "PRECONFIRMED", Selected = false });
            status_items.Add(new SelectListItem() { Text = "Outright Rejected", Value = "OUTRIGHTREJECT", Selected = false });
            
            ViewBag.StatusID = status_items;

            List<SelectListItem> report_type = new List<SelectListItem>();
            /*
            report_type.Add(new SelectListItem() { Text = "Current Month", Value = "CURRENTMONTH", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Others", Value = "OTHERS", Selected = true });
            report_type.Add(new SelectListItem() { Text = "Quarterly", Value = "QUARTERLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Yearly", Value = "YEARLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "YTD Monthly", Value = "YTDMONTHLY", Selected = false });
            */
            
            report_type.Add(new SelectListItem() { Text = "Others", Value = "OTHERS", Selected = true });
            report_type.Add(new SelectListItem() { Text = "Quarterly", Value = "QUARTERLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Yearly", Value = "YEARLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Monthly", Value = "MONTHLY", Selected = false });
            
            ViewBag.ReportID = report_type;

             List<SelectListItem> datefiltermisc = new List<SelectListItem>();
            /*
            report_type.Add(new SelectListItem() { Text = "Current Month", Value = "CURRENTMONTH", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Others", Value = "OTHERS", Selected = true });
            report_type.Add(new SelectListItem() { Text = "Quarterly", Value = "QUARTERLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "Yearly", Value = "YEARLY", Selected = false });
            report_type.Add(new SelectListItem() { Text = "YTD Monthly", Value = "YTDMONTHLY", Selected = false });
            */
            datefiltermisc.Add(new SelectListItem() { Text = "-filter by-", Value = "OTHERFILTER", Selected = true });
            datefiltermisc.Add(new SelectListItem() { Text = "Year-To-Date", Value = "YEARTODATE", Selected = false });
            datefiltermisc.Add(new SelectListItem() { Text = "Current Month", Value = "CURRENTMONTH", Selected = false });
          
            
            ViewBag.DateFilterMisc = datefiltermisc;
        }

        }
    }

