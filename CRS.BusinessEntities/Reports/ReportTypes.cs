using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities.Reports
{
    public static class ReportTypes
    {
        public enum ReportTypesAndPath
        {
            [Description("~/Reports/ApplicationStatus/ApplicationStatusSummary.rdlc")]
            ApplicationStatusSummary,
            [Description("~/Reports/ApplicationStatus/ApplicationStatusDetails.rdlc")]
            ApplicationStatusDetails,
            [Description("~/Reports/Productivity/ProductivityOthers.rdlc")]
            ProductivityOthers,
            [Description("~/Reports/Productivity/ProductivityYTD.rdlc")]
            ProductivityYTD,
            [Description("~/Reports/Productivity/ProductivityYearly.rdlc")]
            ProductivityYearly,
            [Description("~/Reports/Productivity/ProductivityQuarterly.rdlc")]
            ProductivityQuarterly,
            [Description("~/Reports/Productivity/ProductivityMonthly.rdlc")]
            ProductivityMonthly,
            [Description("~/Reports/Redemption/RedemptionDetail.rdlc")]
            RedemptionDetail,
            [Description("~/Reports/EAPR/EAPR.rdlc")]
            EAPRReport,
            [Description("~/Reports/EAPR/EAPRForm2.rdlc")]
            EAPRForm,
            [Description("~/Reports/Productivity/OutrightRejected.rdlc")]
            OutrightRejected,
            [Description("~/Reports/Productivity/IncompleteOthers.rdlc")]
            IncompleteOthers,
            [Description("~/Reports/Productivity/IncompleteQuarterly.rdlc")]
            IncompleteQuarterly,
            [Description("~/Reports/Productivity/IncompleteMonthly.rdlc")]
            IncompleteMonthly,
            [Description("~/Reports/Productivity/IncompleteDetail.rdlc")]
            IncompleteDetail,
            [Description("~/Reports/Productivity/ProductivityWeekly.rdlc")]
            ProductivityWeekly,
            [Description("~/Reports/Productivity/RejectedSummaryWeekly.rdlc")]
            RejectedSummaryWeekly,
            [Description("~/Reports/Productivity/RejectedSummaryMonthly.rdlc")]
            RejectedSummaryMonthly,
            [Description("~/Reports/Productivity/IncompleteWeekly.rdlc")]
            IncompleteWeekly,
            [Description("~/Reports/Productivity/ProductivityDaily.rdlc")]
            ProductivityDaily,
            [Description("~/Reports/Productivity/IncompleteDaily.rdlc")]
            IncompleteDaily,
            [Description("~/Reports/Productivity/ProductivityDetail.rdlc")]
            ProductivityDetail,
            [Description("~/Reports/Productivity/DailyProdSummary.rdlc")]
            DailyProdSummary,
            [Description("~/Reports/Productivity/CPAReport.rdlc")]
            CPAReport,
            [Description("~/Reports/Productivity/CPAReportSummaryApprovalRate.rdlc")]
            CPAReportSummaryApprovalRate,
            [Description("~/Reports/Productivity/CPAReportSummaryActualApprovalRate.rdlc")]
            CPAReportSummaryActualApprovalRate,
            [Description("~/Reports/Productivity/CPAReportSummaryApprovals.rdlc")]
            CPAReportSummaryApprovals,
            [Description("~/Reports/Productivity/CPAReportSummaryCommission.rdlc")]
            CPAReportSummaryCommission,
            [Description("~/Reports/Productivity/CPAReportInputsAppPCH.rdlc")]
            CPAReportInputsAppPCH,
            [Description("~/Reports/Productivity/CPAReportInputsAppSimul.rdlc")]
            CPAReportInputsAppSimul,
            [Description("~/Reports/Productivity/CPAReportInputsRejects.rdlc")]
            CPAReportInputsRejects,
            [Description("~/Reports/Productivity/CPAReportInputsCommissions.rdlc")]
            CPAReportInputsCommissions,
            [Description("~/Reports/EAPRChannel/EAPRChannelForm.rdlc")]
            EAPRChannelForm,
            [Description("~/Reports/CommissionDashboard/CommissionDashboardSummary.rdlc")]
            CommissionDashboardSummary,
            [Description("~/Reports/CommissionDashboard/CommissionDashboardDetails.rdlc")]
            CommissionDashboardDetails,
            [Description("~/Reports/Extension/ExtensionsSummary.rdlc")]
            ExtensionSummary,
            [Description("~/Reports/Extension/ExtensionsDetails.rdlc")]
            ExtensionDetails,
            [Description("~/Reports/EAPRChannel/EAPRChannelSummary.rdlc")]
            EAPRChannelSummary,
            [Description("~/Reports/Productivity/ProductivityMonthlyReport.rdlc")]
            ProductivityReportMonthly,
            [Description("~/Reports/Productivity/ProductivityMonthlyALLReport.rdlc")]
            ProductivityReportMonthlyALL,
            [Description("~/Reports/Productivity/ProductivityWeeklyReport.rdlc")]
            ProductivityReportWeekly,
            [Description("~/Reports/Productivity/ProductivityWeeklyReportALL.rdlc")]
            ProductivityReportWeeklyALL,
            [Description("~/Reports/Productivity/ProductivityMonthlyReasonReport.rdlc")]
            ProductivityMonthlyReasonReport,
            [Description("~/Reports/Productivity/ProductivityWeeklyReasonReport.rdlc")]
            ProductivityReportReasonWeekly,
            [Description("~/Reports/Productivity/TopRejectedReason.rdlc")]
            ProductivityReportTopRejectedReason,
           
            //[Description("~/Reports/ApplicationStatus/ApplicationStatusDaily.rdlc")]
            //ApplicationStatusMonthly,
            //[Description("~/Reports/ApplicationStatus/ApplicationStatusDaily.rdlc")]
            //ApplicationStatusQuarterly,
            //[Description("~/Reports/ApplicationStatus/ApplicationStatusDaily.rdlc")]
            //ApplicationStatusYearly,
        }

        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum) throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            var str = enumerationValue.ToString();
            var memberInfo = type.GetMember(str);
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return str;
        }


    }
}