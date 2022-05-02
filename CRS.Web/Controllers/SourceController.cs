using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;


namespace CRS.Controllers
{
    [ACTSAuthorize]
    public class SourceController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetSourceList()
        {
            var jsonResult = Json(SourceManager.GetList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpDelete]
        public JsonResult DeleteSource(int sourceId)
        {
            return Json(SourceManager.Delete(sourceId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveSource(Source source)
        {
            source.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(SourceManager.Save(source));
        }

        [HttpPost]
        public JsonResult EditSource(Source source)
        {
            source.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(SourceManager.Update(source));
        }

        public JsonResult IsSourceCodeValid(string sourceCode)
        {
            return Json(!SourceManager.SourceCodeExists(sourceCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsSourceNameValid(string sourceName)
        {
            return Json(!SourceManager.SourceNameExists(sourceName), JsonRequestBehavior.AllowGet);
        }

    }
}
