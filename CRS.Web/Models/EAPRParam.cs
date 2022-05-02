using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class EAPRParam
    {
        public string ControlNo { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string PayeeName { get; set; }
        public string PayeeTin { get; set; }
        public string OriginatingDepartment { get; set; }
        public string DepartmentCode { get; set; }
        public string ExpenseCategoryCode { get; set; }
    }
}