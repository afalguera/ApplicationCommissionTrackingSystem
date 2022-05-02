using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace CRS.Bll
{
    public class EAPRManager
    {
        #region Get EAPR List dashboard
        public static IEnumerable<EAPR> GetList(string dateFrom,
                                                    string dateTo,
                                                    string controlNo,
                                                    string payeeName,
                                                    string payeeTin,
                                                    string originatingDepartment,
                                                    string departmentCode,
                                                    string expenseCategoryCode
                                                )
        {
            return EAPRDB.GetEAPRList(dateFrom,
                                       dateTo,
                                       controlNo,
                                       payeeName,
                                       payeeTin,
                                       originatingDepartment,
                                       departmentCode,
                                       expenseCategoryCode
                                       );

        } 
        #endregion

        #region Get Expense Category
        public static IEnumerable<ExpenseCategory> GetExpenseCategory()
        {
            return EAPRDB.GetExpenseCategory();
        } 
        #endregion

        #region Payment List
        public static IEnumerable<Payment> GetPaymentList()
        {
            return EAPRDB.GetPaymentList();
        }
        #endregion

        #region Department List
        public static IEnumerable<EAPREntityPair> GetDepartmentList()
        {
            return EAPRDB.GetDepartmentList();
        }
        #endregion

        #region Signatories List
        public static IEnumerable<Signatories> GetSignatoriesList()
        {
            return EAPRDB.GetSignatoriesList();
        }
        #endregion

        #region Get Budget Allocation Approvers
        public static IEnumerable<BudgetAllocationApprover> GetBudgetAllocationApprover(string budgetType)
        {
            return EAPRDB.GetBudgetAllocationApprover().Where(x => x.BudgetType == budgetType);
        } 
        #endregion

        #region EAPR Create, Update, Delete
        public static bool EARPCRUD(EAPR eaprItem)
        {
            string remarks = !string.IsNullOrEmpty(eaprItem.Description)
                           ? eaprItem.Description.Replace("\n", "<br/>")
                           : string.Empty;
            return EAPRDB.EARPCRUD(eaprItem.Mode,
                                   eaprItem.EaprId,
                                   eaprItem.ControlNo,
                                   eaprItem.PaymentDate,
                                   eaprItem.ExpenseAmount,
                                   eaprItem.PayeeName,
                                   eaprItem.PayeeTin,
                                   eaprItem.OriginatingDepartment,
                                   eaprItem.DepartmentCode,
                                   remarks,
                                   eaprItem.ExpenseCategoryCode,
                //eaprItem.BudgetAllocation,
                //eaprItem.WithApprBudget,
                //eaprItem.ExceedsApprBudget,
                //eaprItem.NotInApprBudget,
                                   formatListToString(eaprItem.RequestedBy, 'R'),
                                   formatListToString(eaprItem.CheckedBy, 'C'),
                                   formatListToString(eaprItem.NotedBy, 'N'),
                                   formatListToString(eaprItem.ApprovedBy, 'A'),
                                   formatListToString(eaprItem.AdditionalApprovedBy, 'D'),
                                   eaprItem.UserBy
                                   );

        } 
        #endregion

        #region Get EAPR Item given Id
        public static IEnumerable<EAPR> GetEAPRItem(decimal eaprId)
        {
            var eaprItem = EAPRDB.GetEAPRItem(eaprId).ToList();
            foreach (var item in eaprItem)
            {
                item.RequestedBy = convertToList(item.RequestedByString);
                item.CheckedBy = convertToList(item.CheckedByString);
                item.NotedBy = convertToList(item.NotedByString);
                item.ApprovedBy = convertToList(item.ApprovedByString);
                item.AdditionalApprovedBy = convertToApproveList(item.AdditionalApprovedByString);

                //item.RequestedBy = convertToRequestList(item.RequestedByString);
                //item.CheckedBy = convertToCheckList(item.CheckedByString);
                //item.NotedBy = convertToNotedList(item.NotedByString);
                //item.ApprovedBy = convertToApproveList(item.ApprovedByString);
                //item.AdditionalApprovedBy = convertToApproveList(item.AdditionalApprovedByString);
            }
            return eaprItem;
        } 
        #endregion

        #region Format list to string
        private static string formatListToString<T>(List<T> data, char cType)
        {

            if (data == null)
            {
                return string.Empty;
            }

            string strItems = string.Empty;

            if (cType == 'D')
            {
                foreach (var item in data as List<ApprovedByEntity>)
                {
                    strItems += item.ApproverTitle + "|" + item.ApproverName + ",";
                }
            }
            else
            {
                foreach (var item in data as List<Signatories>)
                {
                    strItems += item.PositionName + "|" + item.Name + ",";
                }
            }

            



            //switch (cType)
            //{
            //    case 'R':
            //        foreach (var item in data as List<RequestedByEntity>)
            //        {
            //            strItems += item.requestedTitle + "|" + item.requestedName + ",";
            //        }
            //        break;
            //    case 'C':
            //        foreach (var item in data as List<CheckedByEntity>)
            //        {
            //            strItems += item.checkerTitle + "|" + item.checkerName + ",";
            //        }
            //        break;
            //    case 'N':
            //        foreach (var item in data as List<NotedByEntity>)
            //        {
            //            strItems += item.notedByTitle + "|" + item.notedByName + ",";
            //        }
            //        break;
            //    case 'A':
            //        foreach (var item in data as List<ApprovedByEntity>)
            //        {
            //            strItems += item.approverTitle + "|" + item.approverName + ",";
            //        }
            //        break;
            //    default:
            //        break;
            //}

            return strItems.EndsWith(",") ? strItems.Remove((strItems.Length - 1), 1) : strItems;
        } 
        #endregion

        #region Convert to List
        private static List<Signatories> convertToList(string strItem)
        {
            if (!string.IsNullOrEmpty(strItem))
            {
                string[] strArrayItem = strItem.Split(',');
                List<Signatories> sign = new List<Signatories>();

                foreach (var item in strArrayItem)
                {
                    string[] strArrayValue = item.Split('|');
                    sign.Add(new Signatories
                    {
                        PositionName = strArrayValue[0].ToString(),
                        Name = strArrayValue[1].ToString()
                    }
                            );
                }
                return sign;
            }
            else
            {
                return null;
            }

        } 
        #endregion      

        #region convertToApproveList
        private static List<ApprovedByEntity> convertToApproveList(string strItem)
        {
            if (!string.IsNullOrEmpty(strItem))
            {
                string[] strArrayItem = strItem.Split(',');
                List<ApprovedByEntity> apprv = new List<ApprovedByEntity>();

                foreach (var item in strArrayItem)
                {
                    string[] strArrayValue = item.Split('|');
                    apprv.Add(new ApprovedByEntity
                        {
                            ApproverTitle = strArrayValue[0].ToString(),
                            ApproverName = strArrayValue[1].ToString()
                        }
                    );
                }
                return apprv;
            }
            else
            {
                return null;
            }
        }    
        #endregion 

    }
}