using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class EAPR : CommissionDashboard
    {
        //public string Mode { get; set; }
        public decimal EaprId { get; set; }
        public string ControlNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentDateString { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string ExpenseAmountString { get; set; }
        public string ExpenseAmountInWords { get; set; }
        public string PayeeName { get; set; }
        public string PayeeTin { get; set; }
        public string OriginatingDepartment { get; set; }
        public string DepartmentCode { get; set; }
        public string Description { get; set; }
        public string ExpenseCategoryCode { get; set; }
        public string ExpenseCategoryName { get; set; }
        //public decimal BudgetAllocation  { get; set; }
        //public string BudgetAllocationString { get; set; }
        //public bool WithApprBudget  { get; set; }
        //public bool ExceedsApprBudget { get; set; }
        //public bool NotInApprBudget { get; set; }
        public List<Signatories> RequestedBy { get; set; }
        public List<Signatories> CheckedBy { get; set; }
        public List<Signatories> NotedBy { get; set; }
        public List<Signatories> ApprovedBy { get; set; }
        public List<ApprovedByEntity> AdditionalApprovedBy { get; set; }
        public string UserBy { get; set; }
        //public bool isVatable { get; set; }
        //public bool isGross { get; set; }

        public string RequestedByString { get; set; }
        public string CheckedByString { get; set; }
        public string NotedByString { get; set; }
        public string ApprovedByString { get; set; }
        public string AdditionalApprovedByString { get; set; }

        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankBranch { get; set; }

        public string BranchAccountName { get; set; }
        public string BranchAccountNumber { get; set; }
        public string BranchBankBranch { get; set; }

        public int SalesManagerId { get; set; }
        public int ChannelRequestorId { get; set; }
        public int ChannelCheckerId { get; set; }
        public int ChannelNoterId { get; set; }

        public string EAPRDescription { get; set; }
       
     }

    public class ApprovedByEntity
    {
        public string ApproverTitle { get; set; }
        public string ApproverName { get; set; }
    }

    public class EAPREntityPair
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Payment : EAPREntityPair, ICRSBase
    {
        public string Tin { get; set; }

        public int ID { get; set; }

        //public string Code { get; set; }

        //public string Name { get; set; }

        public bool IsVatable { get; set; }

        public bool IsGross { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
    }
    }

    public class PositionDetails : EAPREntityPair, ICRSBase
    {
        public string PositionType { get; set; }

        public int ID { get; set; }

        //public string Code { get; set; }

        //public string Name { get; set; }

        public string PositionTypeName { get; set; }

        public string PositionName { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string UserBy { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }


    public class Signatories : EAPREntityPair
    {
        public string PositionName { get; set; }
        public string PositionType { get; set; }
    }

    public class EAPRChannel : EAPREntityPair
    {
        public bool IsBranchName { get; set; }
        public bool IsEAPR { get; set; }
        public bool IsDistrict { get; set; }
    }

    public class EAPRDistrict : EAPREntityPair
    {
        public string ChannelCode { get; set; }
    }
}