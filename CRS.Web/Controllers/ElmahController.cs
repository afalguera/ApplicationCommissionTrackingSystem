using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Helpers;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    public class ElmahController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
    }
}
