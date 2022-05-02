using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities.Reports
{
    public static class ReportData
    {
        static ReportData()
        {
            ReportParameters = new List<Parameter>();
        }
        
        public static bool IsLocal { get; set; }
        public static string ReportName { get; set; }
        public static List<Parameter> ReportParameters { get; set; }
        //public static List<Parameter> DataParameters { get; set; }
        public static string ReportPath { get; set; }
        public static string ReportType { get; set; }
        public static string ReportType2 { get; set; }
        public static string ReportType3 { get; set; }
        public static string ReportType4 { get; set; }
        public static string ReportType5 { get; set; }
        public static string ReportType6 { get; set; }
        public static string ReportType7 { get; set; }
        public static string ReportType8 { get; set; }
        public static string ReportType9 { get; set; }
        

        public static string ReportDataSetName { get; set; }
        public static string ReportDataSetName2 { get; set; }
        public static string ReportDataSetName3 { get; set; }
        public static string ReportDataSetName4 { get; set; }
        public static string ReportDataSetName5 { get; set; }
        public static string ReportDataSetName6 { get; set; }
        public static string ReportDataSetName7 { get; set; }
        public static string ReportDataSetName8 { get; set; }
        public static string ReportDataSetName9 { get; set; }
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public string Value { get; set; }
    }
    
}