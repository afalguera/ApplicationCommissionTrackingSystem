using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;
using System;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class DistrictController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetDistrictList()
        {
            return Json(DistrictManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteDistrict(int districtId)
        {
            return Json(DistrictManager.Delete(districtId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveDistrict(District district)
        {
            district.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(DistrictManager.Save(district));
        }

        [HttpPost]
        public JsonResult EditDistrict(District district)
        {
            district.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(DistrictManager.Update(district));
        }

        public JsonResult IsDistrictCodeValid(string districtCode)
        {
            return Json(!DistrictManager.DistrictCodeExists(districtCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDistrictNameValid(string districtName)
        {
            return Json(!DistrictManager.DistrictNameExists(districtName), JsonRequestBehavior.AllowGet);
        }

        #region Region List
        public JsonResult GetRegionList()
        {
            var model = new Object();
            string regionCode = SessionWrapper.CurrentUser.RegionCode ?? string.Empty;
            model = CommissionDashboardManager.GetRegionList(regionCode);
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
    }
}
