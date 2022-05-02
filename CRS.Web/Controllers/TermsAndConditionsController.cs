using CRS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    //[ACTSAuthorize]
    [AllowApplicant]
    public class TermsAndConditionsController : Controller
    {
        //
        // GET: /TermsAndConditions/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcceptTerms()
        {
            //return View();
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var withAccess = SessionWrapper.PageAccess.Where(x => x.PageName.Equals("ApplicationStatus", StringComparison.InvariantCultureIgnoreCase))
                                                .Select(x => x.CanView)
                                                .FirstOrDefault();
                if (withAccess)
                {
                    return RedirectToAction("Index", "ApplicationStatus");
                }
                else
                {
                    var firstPage = SessionWrapper.PageAccess.Where(x => x.CanView)
                                              .Select(x => x.PageName)
                                              .FirstOrDefault();
                    return RedirectToAction("Index", firstPage);
                }
            }
            else if (SessionWrapper.UserType == UserType.Cardholder)
            {
                return RedirectToAction("Index", "ApplicationStatus");
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

    }
}
