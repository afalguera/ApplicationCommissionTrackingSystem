using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class TableParameter
    {
        public string sEcho { get; set; }
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
    }
}