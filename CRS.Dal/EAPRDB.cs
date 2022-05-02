using CRS.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using CRS.Helper;

namespace CRS.Dal
{
    public class EAPRDB
    {
        #region Get Expense Category List
        public static IEnumerable<ExpenseCategory> GetExpenseCategory()
        {
            string spName = "spGetExpenseCategoryList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<ExpenseCategory> list = new List<ExpenseCategory>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ExpenseCategory dto = new ExpenseCategory();
                    dto.ExpenseCategoryCode = reader["ExpenseCategoryCode"].ToString();
                    dto.ExpenseCategoryName = reader["ExpenseCategoryName"].ToString();

                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region EAPR Insert
        public static bool EARPCRUD(string mode, decimal eaprId, string controlNo, DateTime paymentDate,
                                decimal expenseAmount, string payeeName, string payeeTin, string originatingDepartment,
                                string departmentCode, string description, string expenseCategoryCode, string requestedBy,
                                string checkedBy, string notedBy, string approvedBy, string additionalApproveBy, string userBy
                           )
        {
            string spName = "spEAPRCRUD";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            bool bResult = false;
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@mode", mode) ,
							new SqlParameter("@eaprId",	eaprId.AsDecimal()),
							new SqlParameter("@controlNo",	controlNo), 
							new SqlParameter("@paymentDate", paymentDate.AsDateTime()),
							new SqlParameter("@expenseAmount", expenseAmount.AsDecimal() ),
							new SqlParameter("@payeeName", payeeName),
							new SqlParameter("@payeeTin", payeeTin),
							new SqlParameter("@originatingDepartment", originatingDepartment),
							new SqlParameter("@departmentCode", departmentCode),
							new SqlParameter("@description", description),
							new SqlParameter("@expenseCategoryCode", expenseCategoryCode),
                            //new SqlParameter("@budgetAllocation", budgetAllocation.AsDecimal()),
                            //new SqlParameter("@withApprBudget", withApprBudget.AsBoolean()),
                            //new SqlParameter("@exceedsApprBudget", exceedsApprBudget.AsBoolean()),
                            //new SqlParameter("@notInApprBudget", notInApprBudget.AsBoolean()),
							new SqlParameter("@requestedBy", requestedBy),
                            new SqlParameter("@checkedBy", checkedBy),
                            new SqlParameter("@notedBy", notedBy),
                            new SqlParameter("@approvedBy", approvedBy),
                            new SqlParameter("@additionalApprovedBy", additionalApproveBy),
                            new SqlParameter("@userBy", userBy)
							};

            try
            {
                bResult = sqlHelper.ExecuteNonQuerySPDB(spName, sqlParams);
            }
            catch (Exception ex)
            {
                return false;
            }
            return bResult;

        } 
        #endregion

        #region Get Additional Budget Approvers
        public static IEnumerable<BudgetAllocationApprover> GetBudgetAllocationApprover()
        {
            string spName = "spGetBudgetApproverList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<BudgetAllocationApprover> list = new List<BudgetAllocationApprover>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BudgetAllocationApprover dto = new BudgetAllocationApprover();
                    dto.ApproverTitle = reader["ApproverTitle"].ToString();
                    dto.ApproverName = reader["ApproverName"].ToString();
                    dto.ApproverAmountLower = reader["ApproverAmountLower"].AsDecimal();
                    dto.ApproverAmountUpper = reader["ApproverAmountUpper"].AsDecimal();
                    dto.BudgetType = reader["BudgetType"].ToString();
                    dto.Remarks = reader["Remarks"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region Get EAPR Item
        public static IEnumerable<EAPR> GetEAPRItem(decimal eaprId)
        {
            string spName = "spGetEAPRItem";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@eaprId", eaprId.AsDecimal())};
            List<EAPR> list = new List<EAPR>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EAPR dto = new EAPR();
                    dto.EaprId = eaprId;
                    dto.PaymentDateString = reader["PaymentDate"].ToString();
                    dto.ControlNo = reader["ControlNo"].ToString();
                    dto.ExpenseAmount = reader["ExpenseAmount"].AsDecimal();
                    dto.ExpenseAmountString = String.Format("{0:#,###0.00}", reader["ExpenseAmount"].AsDecimal());
                    dto.ExpenseAmountInWords = NumbersToWords.DecimalToWords(reader["ExpenseAmount"].AsDecimal());
                    dto.PayeeName = reader["PayeeName"].ToString();
                    dto.PayeeTin = reader["PayeeTin"].ToString();
                    dto.OriginatingDepartment = reader["OriginatingDepartment"].ToString();
                    dto.DepartmentCode = reader["DepartmentCode"].ToString();
                    dto.Description = reader["Description"].ToString();
                    dto.ExpenseCategoryCode = reader["ExpenseCategoryCode"].ToString();
                    dto.ExpenseCategoryName = reader["ExpenseCategoryName"].ToString();
                    //dto.BudgetAllocation = reader["BudgetAllocation"].AsDecimal();
                    //dto.BudgetAllocationString = String.Format("{0:#,###0.00}", reader["BudgetAllocation"].AsDecimal());
                    //dto.WithApprBudget = reader["WithApprBudget"].AsBoolean();
                    //dto.ExceedsApprBudget = reader["ExceedsApprBudget"].AsBoolean();
                    //dto.NotInApprBudget = reader["NotInApprBudget"].AsBoolean();
                    dto.RequestedByString = reader["RequestedBy"].ToString();
                    dto.CheckedByString = reader["CheckedBy"].ToString();
                    dto.NotedByString = reader["NotedBy"].ToString();
                    dto.ApprovedByString = reader["ApprovedBy"].ToString();
                    dto.AdditionalApprovedByString = reader["AdditionalApprovedBy"].ToString();
                    dto.IsVatable = reader["isVatable"].AsBoolean();
                    dto.IsGross = reader["isGross"].AsBoolean();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region Get EAPR List
        public static IEnumerable<EAPR> GetEAPRList(string dateFrom,
                                                    string dateTo,
                                                    string controlNo,
                                                    string payeeName,
                                                    string payeeTin,
                                                    string originatingDepartment,
                                                    string departmentCode,
                                                    string expenseCategoryCode
                                                )
        {
            string spName = "spEAPR";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<EAPR> list = new List<EAPR>();
            //DateTime dtFrom = dateFrom.AsDateTime();
            dateTo = String.Format("{0:MM/dd/yyyy}", dateTo.AsDateTime().AddDays(1));

            SqlParameter[] sqlParams = new SqlParameter[] {
							new SqlParameter("@dateFrom", dateFrom) ,
							new SqlParameter("@dateTo",	dateTo),
							new SqlParameter("@controlNo",	controlNo), 
							new SqlParameter("@payeeName", payeeName),
							new SqlParameter("@payeeTin", payeeTin ),
							new SqlParameter("@originatingDepartment", originatingDepartment),
							new SqlParameter("@departmentCode", departmentCode),
							new SqlParameter("@expenseCategoryCode", expenseCategoryCode)				
							};

            reader = sqlHelper.ExecuteReaderSPDB(spName, sqlParams);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EAPR dto = new EAPR();
                    dto.EaprId = reader["EAPRId"].AsDecimal();
                    dto.ControlNo = reader["ControlNo"].ToString();
                    dto.ExpenseAmount =  reader["ExpenseAmount"].AsDecimal();
                    dto.PaymentDateString = reader["PaymentDate"].ToString();
                    dto.ExpenseAmountString = String.Format("{0:#,###0.00}", reader["ExpenseAmount"].AsDecimal());
                    dto.PayeeName = reader["PayeeName"].ToString();
                    dto.PayeeTin = reader["PayeeTin"].ToString();
                    dto.OriginatingDepartment = reader["OriginatingDepartment"].ToString();
                    dto.DepartmentCode = reader["DepartmentCode"].ToString();
                    dto.Description = reader["Description"].ToString().Replace("<br/>", Environment.NewLine);
                    dto.ExpenseCategoryCode = reader["ExpenseCategoryCode"].ToString();
                    //dto.BudgetAllocation = reader["BudgetAllocation"].AsDecimal();
                    //dto.BudgetAllocationString = reader["BudgetAllocation"].ToString();
                    //dto.WithApprBudget = reader["WithApprBudget"].AsBoolean();
                    //dto.ExceedsApprBudget = reader["ExceedsApprBudget"].AsBoolean();
                    //dto.NotInApprBudget = reader["NotInApprBudget"].AsBoolean();
                    dto.RequestedByString = reader["RequestedBy"].ToString();
                    dto.CheckedByString = reader["CheckedBy"].ToString();
                    dto.NotedByString = reader["NotedBy"].ToString();
                    dto.ApprovedByString = reader["ApprovedBy"].ToString();
                    dto.AdditionalApprovedByString = reader["AdditionalApprovedBy"].ToString();
                    dto.ExpenseCategoryName = reader["ExpenseCategoryName"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        } 
        #endregion

        #region Get PaymentList List
        public static IEnumerable<Payment> GetPaymentList()
        {
            string spName = "spGetPaymentList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<Payment> list = new List<Payment>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Payment dto = new Payment();
                    dto.Code = reader["Code"].ToString();
                    dto.Name = reader["Name"].ToString();
                    dto.Tin = reader["TIN"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        }
        #endregion

        #region Get Department List
        public static IEnumerable<EAPREntityPair> GetDepartmentList()
        {
            string spName = "spGetDepartmentList";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<EAPREntityPair> list = new List<EAPREntityPair>();
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    EAPREntityPair dto = new EAPREntityPair();
                    dto.Code = reader["Code"].ToString();
                    dto.Name = reader["Name"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        }
        #endregion

        #region Get Signatories List
        public static IEnumerable<Signatories> GetSignatoriesList()
        {
            string spName = "spGetSignatories";
            DBSqlHelper sqlHelper = new DBSqlHelper();
            SqlDataReader reader;
            List<Signatories> list = new List<Signatories>();
    
            reader = sqlHelper.ExecuteReaderSPDB(spName, null);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Signatories dto = new Signatories();
                    dto.Code = reader["Code"].ToString();
                    dto.Name = reader["Name"].ToString();
                    dto.PositionName = reader["PositionName"].ToString();
                    dto.PositionType = reader["PositonType"].ToString();
                    list.Add(dto);
                }
            }
            reader.Close();
            reader.Dispose();
            reader = null;
            return list.AsEnumerable();
        }
        #endregion
    }
}