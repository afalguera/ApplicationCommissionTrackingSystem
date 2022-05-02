using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class RegionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetRegionList()
        {
            return Json(RegionManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteRegion(int regionId)
        {
            return Json(RegionManager.Delete(regionId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveRegion(Region region)
        {
            region.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(RegionManager.Save(region));
        }

        [HttpPost]
        public JsonResult EditRegion(Region region)
        {
            region.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(RegionManager.Update(region));
        }

        public JsonResult IsRegionCodeValid(string regionCode)
        {
            return Json(!RegionManager.RegionCodeExists(regionCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsRegionNameValid(string regionName)
        {
            return Json(!RegionManager.RegionNameExists(regionName), JsonRequestBehavior.AllowGet);
        }
    }
}
