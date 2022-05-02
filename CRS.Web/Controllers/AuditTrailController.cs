using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Helpers;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class AuditTrailController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
