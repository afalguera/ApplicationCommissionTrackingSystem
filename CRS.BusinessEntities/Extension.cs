using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.BusinessEntities
{
    public class Extension : ApplicationStatus
    {
        public string ExtensionTypeCode { get; set; }
        public string ExtensionTypeName {
            get
            {
                return this.ExtensionTypeCode.Trim() == "S" ? "Simultaneous" : "Late";
            }
        
        }
    }
}