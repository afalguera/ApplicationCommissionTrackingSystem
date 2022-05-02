using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CRS.Helper;
using System.Globalization;

namespace CRS.Bll
{
    public class EAPRChannelManager
    {
        #region Get EAPRChannel Results
        public static IEnumerable<CommissionDashboard> GetList(string dateFrom,
                                                            string dateTo,
                                                            string sourceCode,
                                                            string channelCode,
                                                            string reportType,
                                                            string regionCode,
                                                            string areaCode,
                                                            string districtCode,
                                                            string branchCode
                                                       )
        {

            return EAPRChannelDB.GetEAPRChannelList(dateFrom,
                                                            dateTo,
                                                            sourceCode,
                                                            channelCode,
                                                            reportType,
                                                            regionCode,
                                                            areaCode,
                                                            districtCode,
                                                            branchCode
                                                       );
        }
        #endregion

        //#region Get EAPRChannel Item
        //public static IEnumerable<EAPR> GetEAPRChannelItem(string dateFrom, string dateTo,
        //                                        string eaprAmount, string eaprChannel, string eaprProgram)
        //{
        //    EAPR dto = new EAPR();
        //    List<EAPR> list = new List<EAPR>();
        //    List<ApprovedByEntity> additionalApprovers = new List<ApprovedByEntity>();
        //    decimal expenseAmount = eaprAmount.Replace(",",string.Empty).AsDecimal();
        //    //decimal expenseAmount = 500000;
        //    string strPeriodCovered = string.Empty;
        //    var addlApprovers = EAPRManager.GetBudgetAllocationApprover("W");
        //    foreach (var item in addlApprovers)
        //    {
        //       decimal minAmount = item.ApproverAmountLower;
        //       if(expenseAmount >= minAmount)
        //       additionalApprovers.Add(new ApprovedByEntity
        //       {
        //           ApproverTitle = item.ApproverTitle,
        //           ApproverName = item.ApproverName
        //       });
        //    }

        //    var requestedBy = EAPRChannelDB.GetEAPRChannelSignatory(3) as List<Signatories>;
        //    var checkedBy = EAPRChannelDB.GetEAPRChannelSignatory(4) as List<Signatories>;
        //    var notedBy = EAPRChannelDB.GetEAPRChannelSignatory(6) as List<Signatories>;
        //    var approvedBy = EAPRChannelDB.GetEAPRChannelSignatory(7) as List<Signatories>;

        //    strPeriodCovered = dateFrom + " - " + dateTo;
        //    dto.EaprId = 0;
        //    dto.PaymentDateString = DateTime.Today.ToString("MM/dd/yy");
        //    dto.ControlNo = string.Empty;
        //    dto.ExpenseAmountString = String.Format("{0:#,###0.00}", expenseAmount);
        //    dto.ExpenseAmountInWords = NumbersToWords.DecimalToWords(expenseAmount);
        //    dto.PayeeName = eaprProgram;
        //    dto.PayeeTin = string.Empty;
        //    dto.OriginatingDepartment = "Card Sales";
        //    dto.DepartmentCode = "131";
        //    if (dateFrom == dateTo)
        //    {
        //        strPeriodCovered = dateFrom;
        //    }

        //    dto.Description = "Commission pay-out for " + strPeriodCovered + "<br/><br/>" + eaprChannel; 
        //    dto.ExpenseCategoryCode = "COMM";
        //    dto.ExpenseCategoryName = "COMMISSION";
        //    dto.isVatable = false;
        //    dto.isGross = true;
        //    dto.AdditionalApprovedBy = additionalApprovers;
        //    dto.RequestedBy = requestedBy;
        //    dto.CheckedBy = checkedBy;
        //    dto.NotedBy = notedBy;
        //    dto.ApprovedBy = approvedBy;

        //    list.Add(dto);
        //    return list.AsEnumerable();
        //}
        //#endregion

        #region Get EAPRChannel Item
        public static IEnumerable<EAPR> GetEAPRChannelItem(string dateFrom, string dateTo,
                                               string eaprChannel, string reportType, bool isPerBranch,
                                                bool isDistrict)
        {
            
            List<EAPR> list = new List<EAPR>();
            List<ApprovedByEntity> additionalApprovers = new List<ApprovedByEntity>();
            string eaprAmount = string.Empty;
            string strDescription = string.Empty;
            string strPayeeName = string.Empty;
 
            var eaprItemList = EAPRChannelDB.GetEAPRChannelItem(dateFrom, dateTo, eaprChannel, isPerBranch).ToList();
            var isBranchIncentive = eaprItemList.Select(x => x.IsBranchIncentive).FirstOrDefault().AsBoolean();
            var isCarDealer = eaprItemList.Select(x => x.IsCarDealer).FirstOrDefault().AsBoolean();
            var isInflows = eaprItemList.Select(x => x.IsInflows).FirstOrDefault().AsBoolean();
            //var isCoreBrand = eaprItemList.Select(x => x.IsCoreBrand).FirstOrDefault().AsBoolean();
            var isTiering = eaprItemList.Select(x => x.IsTiering).FirstOrDefault().AsBoolean();
            var isRCBC = eaprItemList.Select(x => x.IsRCBC).FirstOrDefault().AsBoolean();
            var isMyOrange = eaprItemList.Select(x => x.IsMyOrange).FirstOrDefault().AsBoolean();
            var isForEveryCountIncentive =  eaprItemList.Select(x => x.IsForEveryCountIncentive).FirstOrDefault().AsBoolean();
            var isCardType = eaprItemList.Select(x => x.IsCardType).FirstOrDefault().AsBoolean();
            var isCardBrand = eaprItemList.Select(x => x.IsCardBrand).FirstOrDefault().AsBoolean();

            string strLowerDescription = string.Empty;
            decimal totalTieringAmount = 0;
            decimal totalNonTieringAmount = 0;
            bool nonTiering = false;
            decimal totalRefAmount = 0;
            decimal withRefAmount = 0;
            decimal noRefAmount = 0;
            int noOfMonths = 0;
         

            if (eaprItemList.Any())
            {

                if (isDistrict)
                {
                    var districtList = DistrictDB.GetList().Where(x => x.ChannelCode == eaprChannel).ToList();
                    var distEaprList = new List<EAPR>();
                                  
                    foreach (var dst in districtList)
                    {
                       
                        var dlist = eaprItemList.Where(x => x.DistrictCode == dst.Code).ToList();
                        var dTotal = dlist.Sum(x => x.NetAmount).AsDecimal();
                        var description = eaprItemList.Select(x => x.Description).FirstOrDefault() ?? string.Empty;
                        var totalApprovalCount = dlist.Sum(x => x.ApprovalCount).AsInt();
                        var totalInflowsCount = dlist.Sum(x => x.ApplicantInflowCount).AsInt();
                        var inflowsRate = eaprItemList.Select(x => x.InflowsRate).FirstOrDefault().AsDecimal();
                        var inflowsCount = eaprItemList.Select(x => x.InflowsCount).FirstOrDefault().AsInt();
                        var tieringRate = eaprItemList.Select(x => x.TieringRate).FirstOrDefault().AsDecimal();
                        var tieringCount = eaprItemList.Select(x => x.TieringCount).FirstOrDefault().AsInt();
                        

                          distEaprList.Add(new EAPR {
                                DistrictName = dst.Name, 
                                ExpenseAmount = dTotal,
                                ChannelCode = eaprChannel,
                                PayeeName = dst.DistrictAccountName,
                                PayeeTin = dst.DistrictTIN,
                                BankBranch = dst.DistrictBankBranch,
                                Description = description,
                                ApprovalCount = totalApprovalCount,
                                ApplicantInflowCount = totalInflowsCount,
                                InflowsRate = inflowsRate,
                                InflowsCount = inflowsCount,
                                TieringRate = tieringRate,
                                TieringCount = tieringCount,
                                ChannelRequestorId = eaprItemList.Select(x => x.ChannelRequestorId).FirstOrDefault().AsInt(),
                                ChannelCheckerId = eaprItemList.Select(x => x.ChannelCheckerId).FirstOrDefault().AsInt(),
                                ChannelNoterId = eaprItemList.Select(x => x.ChannelNoterId).FirstOrDefault().AsInt(),
                                SalesManagerId = eaprItemList.Select(x => x.SalesManagerId).FirstOrDefault().AsInt()
                         });

                    }
                    
                    eaprItemList.Clear();
                    eaprItemList.AddRange(distEaprList);
                }
    

              if (isRCBC)
                {
                    totalRefAmount = eaprItemList.Sum(x => x.NetAmount).AsDecimal();
                    withRefAmount = eaprItemList.Where(x => !string.IsNullOrEmpty(x.ReferrorName.Trim())
                                                          && !string.IsNullOrEmpty(x.ReferrorCode.Trim()))
                                                          .Sum(x => x.NetAmount).AsDecimal();
                     noRefAmount = totalRefAmount - withRefAmount;
                 }

              if (isBranchIncentive || isCarDealer || isInflows)
                {
                    if (!isInflows)
                    {
                        eaprItemList = eaprItemList.Where(x => x.ApprovalCount > 0).ToList();
                    }

                    if (isCarDealer)
                    {
                        var isNonYGC = eaprItemList.Where(x => x.IsYGC == false).ToList();
                        var isYGC = eaprItemList.Where(x => x.IsYGC).ToList();
                        eaprItemList.Clear();
                        eaprItemList.AddRange(isYGC);
                        eaprItemList.AddRange(isYGC);
                        eaprItemList.AddRange(isNonYGC);
                        eaprItemList = (from e in eaprItemList orderby e.BranchName ascending select e).ToList();
                    }
                    else if (isBranchIncentive)
                    {
                        nonTiering = true;
                        var totalApproved = eaprItemList.Sum(x => x.ApprovalCount).AsInt();
                        var commRate = eaprItemList.Select(x => x.CommissionRate).FirstOrDefault().AsDecimal();
                        totalNonTieringAmount = totalApproved * commRate;                    
                        var noTiering = eaprItemList.ToList().FirstOrDefault();
                        eaprItemList.Clear();
                        eaprItemList.Add(noTiering);
                        eaprItemList.Add(noTiering);
                    }
                    else
                    {
                        eaprItemList.AddRange(eaprItemList);
                    }

                    //eaprItemList = (from e in eaprItemList orderby e.BranchName ascending select e).ToList();
                }
                else if (isTiering)
                {
                    totalTieringAmount = eaprItemList.Sum(x => x.NetAmount).AsDecimal();
                    var tieringList = eaprItemList.ToList().FirstOrDefault();
                    eaprItemList.Clear();
                    eaprItemList.Add(tieringList);
                }
              else if (isCardType || isCardType)
              {
                  nonTiering = true;
                  var corebrandList = eaprItemList.ToList().FirstOrDefault();
                  totalNonTieringAmount = eaprItemList.Sum(x => x.CommissionRate * x.ApprovalCount).AsDecimal();
                  eaprItemList.Clear();
                  eaprItemList.Add(corebrandList);
              }
              //else if (isCoreBrand)
              //{
              //    nonTiering = true;
              //    var corebrandList = eaprItemList.ToList().FirstOrDefault();
              //    totalNonTieringAmount = eaprItemList.Sum(x => x.NetAmount).AsDecimal();
              //    eaprItemList.Clear();
              //    eaprItemList.Add(corebrandList);
              //}
              else
              {
                  nonTiering = true;
                  var totalApproved = eaprItemList.Sum(x => x.ApprovalCount).AsInt();
                  var commRate = eaprItemList.Select(x => x.CommissionRate).FirstOrDefault().AsDecimal();
                  totalNonTieringAmount = totalApproved * commRate;
                  var noTiering = eaprItemList.ToList().FirstOrDefault();
                  eaprItemList.Clear();
                  eaprItemList.Add(noTiering);

              }

              //rcbc
              if (isRCBC)
              {
                
                  var rcbcList = eaprItemList.ToList().FirstOrDefault();
                  eaprItemList.Clear();
                  
                  eaprItemList.Add(rcbcList);
                  eaprItemList.Add(rcbcList);
                  eaprItemList.Add(rcbcList);
              }

             
                 int ctr = 0;
                    foreach (var eaprItem in eaprItemList)
                    {
                        EAPR dto = new EAPR();
                        additionalApprovers.Clear();
                        //var netAmount = eaprItem.NetAmountString;
                        decimal expenseAmount = 0;
                        if (isDistrict)
                        {
                            expenseAmount = eaprItem.ExpenseAmount;
                        }
                        else
                        {
                            expenseAmount = isTiering ? totalTieringAmount : (nonTiering ? totalNonTieringAmount : eaprItem.NetAmount);
                        }
                        

                        string strPeriodCovered = string.Empty;
                        var addlApprovers = EAPRManager.GetBudgetAllocationApprover("W");
      

                        decimal taxRate = eaprItem.Tax > 0 ? (1 - eaprItem.Tax) : 1;
                        var requestedBy = EAPRChannelDB.GetEAPRChannelSignatory(eaprItem.ChannelRequestorId) as List<Signatories>;
                        var checkedBy = EAPRChannelDB.GetEAPRChannelSignatory(eaprItem.ChannelCheckerId) as List<Signatories>;
                        var notedBy = EAPRChannelDB.GetEAPRChannelSignatory(eaprItem.ChannelNoterId) as List<Signatories>;
                        var approvedBy = EAPRChannelDB.GetEAPRChannelSignatory(eaprItem.SalesManagerId) as List<Signatories>;

                        strPeriodCovered = dateFrom + " - " + dateTo;
                        dto.EaprId = 0;
                        dto.PaymentDateString = DateTime.Today.ToString("MM/dd/yyyy");
                        dto.ControlNo = string.Empty;
                        strPayeeName = isPerBranch ? eaprItem.BranchName : eaprItem.PayeeName;
                          
                        //Branch Incentive
                        if (isBranchIncentive)
                        {
                            var branchIncentiveCount = eaprItemList.Select(x => x.BranchIncentiveCount).FirstOrDefault().AsInt();
                            var branchIncentiveAmount = eaprItemList.Select(x => x.BranchIncentiveRate).FirstOrDefault().AsDecimal();
                            //var origEaprAmount = eaprItemList.Select(x => x.NetAmount).FirstOrDefault().AsDecimal();
                            var origEaprAmount = expenseAmount;
                            var approvedCount = eaprItemList.Select(x => x.ApprovalCount).FirstOrDefault().AsInt();
                            noOfMonths = ((dateTo.AsDateTime().Year - dateFrom.AsDateTime().Year) * 12)
                                        + dateTo.AsDateTime().Month - dateFrom.AsDateTime().Month;
                            noOfMonths = (noOfMonths > 0 ? (noOfMonths + 1) : 1);

                            decimal amt = 0m;
                            if (approvedCount >= branchIncentiveCount && ctr == 0)
                            {
                                amt = origEaprAmount + (branchIncentiveAmount * noOfMonths);
                            }
                            else
                            {
                                amt = origEaprAmount;
                            }
                            strPayeeName = ctr == 0 ? eaprItem.MainBranchName : eaprItem.SecondaryBranchName;
                            expenseAmount = amt;
                            //expenseAmount = ctr > 0 ? amt : expenseAmount;
                        }
                        //Car Dealer
                        else if (isCarDealer)
                        {
                            decimal totalAmount = eaprItem.NetAmount;
                            int totalCount = eaprItem.ApprovalCount;
                            int refCount = eaprItem.ReferrorCount;
                            decimal seAmount = eaprItem.SERate * refCount * taxRate;
                            decimal nonSEAmount = totalAmount - seAmount;

                            strPayeeName = (ctr == 0 && eaprItem.IsYGC) ? eaprItem.BranchName + " (Sales Executive) " : eaprItem.BranchName;
                            expenseAmount = eaprItem.IsYGC ? ((ctr == 0) ? seAmount : nonSEAmount) : totalAmount ;
                        }
                        //Inflows
                        else if (isInflows)
                        {
                            decimal inflowCount = eaprItem.ApplicantInflowCount;
                            decimal inflowsAmount = eaprItem.InflowsRate * inflowCount * (taxRate);
                           
                            if (ctr > 0)
                            {
                                expenseAmount = inflowsAmount;
                            }
                        }
                        //else if (isCoreBrand)
                        //{
                        //       expenseAmount = totalNonTieringAmount;
                        //}
                        else if (isForEveryCountIncentive)
                        {
                            int modForEvery = eaprItem.ForEveryCountIncentiveCount > 0 
                                              ? (eaprItem.ApprovalCount / eaprItem.ForEveryCountIncentiveCount)
                                              : 1;
                            decimal additionalAmount = eaprItem.ForEveryCountIncentiveRate * modForEvery;
                            expenseAmount = eaprItem.NetAmount + additionalAmount;
                            //incentiveAmount = (this.Variable * modForEvery);
                        }
                        else if (isRCBC)
                        {                        
                            if (ctr == 0)
                            {
                            
                                expenseAmount = totalRefAmount;
                            }
                            else if (ctr == 1)
                            {
                                strPayeeName += " (With Referror)";
                                expenseAmount = withRefAmount;
                            }
                            else
                            {
                                strPayeeName += " (No Referror)";
                                expenseAmount = noRefAmount;
                            }
                        }

                        dto.ExpenseAmount = expenseAmount;
                        dto.ExpenseAmountInWords = NumbersToWords.DecimalToWords(dto.ExpenseAmount);
                        dto.ExpenseAmountString = String.Format("{0:#,###0.00}", dto.ExpenseAmount);

                        dto.PayeeName = strPayeeName;
                        dto.PayeeTin = isPerBranch ? eaprItem.BranchTIN : eaprItem.PayeeTin;
                        string accountName = isPerBranch ? eaprItem.BranchAccountName : eaprItem.AccountName;
                        string accountNumber = isPerBranch ? eaprItem.BranchAccountNumber : eaprItem.AccountNumber;
                        string bankbranch = isPerBranch ? eaprItem.BranchBankBranch : eaprItem.BankBranch;

                        dto.OriginatingDepartment = "Card Sales";
                        dto.DepartmentCode = "131";
                        if (dateFrom == dateTo)
                        {
                            strPeriodCovered = dateFrom;
                        }

                        if (reportType == "M")
                        {
                            string month = dateFrom.AsDateTime().ToString("MMM", CultureInfo.InvariantCulture);
                            string year = dateFrom.AsDateTime().Year.ToString();
                            strPeriodCovered = month + " " + year;
                        }

                        strDescription = (isInflows && ctr > 0 )? "Inflows" : "Commission";
                        strLowerDescription = eaprItem.EAPRDescription;
                        if (isMyOrange)
                        {
                            strLowerDescription += "<br/><br/><br/>"
                                                + "Acct. Name:" + accountName + "<br/>"
                                                + "Acct. Number: " + accountNumber + "<br/>"
                                                + "Branch: " + bankbranch + "<br/>";

                        }
                     
                        dto.Description = strDescription + " pay-out for "
                                        + GetReportTypeString(reportType)
                                        + strPeriodCovered + "<br/><br/>"
                                        + eaprItem.ChannelName
                                        + "<br/><br/>"
                                        + strLowerDescription;
                                    
                        dto.ExpenseCategoryCode = "COMM";
                        dto.ExpenseCategoryName = "COMMISSION";
                        dto.IsVatable = eaprItem.IsVatable.AsBoolean();
                        dto.IsGross = eaprItem.IsGross.AsBoolean();
                        dto.AdditionalApprovedBy = additionalApprovers;
                        dto.RequestedBy = requestedBy;
                        dto.CheckedBy = checkedBy;
                        dto.NotedBy = notedBy;
                        dto.ApprovedBy = approvedBy;
                        //Get additional approvers
                        foreach (var item in addlApprovers)
                        {
                            decimal minAmount = item.ApproverAmountLower;
                            if (expenseAmount >= minAmount)
                                additionalApprovers.Add(new ApprovedByEntity
                                {
                                    ApproverTitle = item.ApproverTitle,
                                    ApproverName = item.ApproverName
                                });
                        }

                        list.Add(dto);

                        if (ctr == 0)
                        {
                            ctr++;
                        }
                        else if (ctr == 1 && isRCBC)
                        {
                            ctr++;
                        }
                        else
                        {
                            ctr = 0;
                        }

                        //if (isCoreBrand)
                        //{
                        //    break;
                        //}
                 
                                     
                }
            }

            if (isPerBranch || isDistrict)
            {
                list = list.OrderBy(x => x.PayeeName).ToList();
            }
              
            return list.AsEnumerable();
        }
        #endregion

        #region Get Report Type String
        public static string GetReportTypeString(string reportType)
        {
            string reportString = string.Empty;
            switch (reportType)
            {
                case "W":
                    reportString = "Week of          ";
                    break;
                case "M":
                    reportString = "Month of          ";
                    break;
                case "":
                    reportString = "Date:          ";
                    break;
                default:
                    break;

            }
            return reportString;
        } 
        #endregion
    }
}