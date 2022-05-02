using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CRS.BusinessEntities
{
    public class CPAReport
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string SalesManager { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        public Boolean IncludeExceptions { get; set; }
        
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportInputsAppPCH
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string SalesManager { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        public Boolean IncludeExceptions { get; set; }

        
        
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportInputsAppSimul
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string SalesManager { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        public Boolean IncludeExceptions { get; set; }

        
        
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportInputsCommissions
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string SalesManager { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public int GroupCount { get; set; }
        public decimal CommValue { get; set; }
        public decimal TierValue { get; set; }
        
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportInputsRejects
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string SalesManager { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        
        
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportSummaryActualApprovalRate
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public int GroupCountApprovedPCH { get; set; }
        public int GroupCountRejected { get; set; }
        public string SalesManager { get; set; }
        
        public bool Validate()
        {
            return true;
        }
    }
    
    public class  CPAReportSummaryApprovalRate
    {


        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public int GroupCountApprovedPCH { get; set; }
        public int GroupCountRejected { get; set; }
        public int GroupCountApprovedSimul { get; set; }
        public string SalesManager { get; set; }
       
      
        public bool Validate()
        {
            return true;
        }
    }

    public class  CPAReportSummaryApprovals
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        public string SalesManager { get; set; }
        
        
        public bool Validate()
        {
            return true;
        }
    }

    public class CPAReportSummaryCommission
    {


        
        public string ChannelCode {get;set;}
        public string ChannelName { get; set; }
        public string DisplayMonth {get;set;}
        public string SortMonth {get;set;}
        public decimal CostPerApplication {get;set;}
        public int GroupCount { get; set; }
        public int Commission { get; set; }
        
        
        public bool Validate()
        {
            return true;
        }
    }





}
