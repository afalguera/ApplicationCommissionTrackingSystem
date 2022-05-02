using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;
using System;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class PositionDetailsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetPositionDetailsList()
        {
            return Json(PositionDetailsManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeletePositionDetails(int positionDetailsId)
        {
            return Json(PositionDetailsManager.Delete(positionDetailsId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SavePositionDetails(string name, string positionCode, string positionType)//(PositionDetails positionDetails)
        {
            var positionDetails = new PositionDetails
            {
                Name = name,
                PositionType = positionType,
                Code = positionCode,
                CreatedBy = SessionWrapper.CurrentUser.UserName
            };
            //positionDetails.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(PositionDetailsManager.Save(positionDetails));
        }

        [HttpPut]
        public JsonResult UpdatePositionDetails(int id, string name, string positionCode, string positionType)//(PositionDetails positionDetails)
        {
            var positionDetails = new PositionDetails
            {
                ID = id,
                Name = name,
                PositionType = positionType,
                Code = positionCode,
                ModifiedBy = SessionWrapper.CurrentUser.UserName
            };
            //positionDetails.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(PositionDetailsManager.Update(positionDetails));
        }

        public JsonResult IsPositionDetailsCodeValid(string positionDetailsCode)
        {
            return Json(!PositionDetailsManager.PositionDetailsCodeExists(positionDetailsCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPositionDetailsNameValid(string positionDetailsName)
        {
            return Json(!PositionDetailsManager.PositionDetailsNameExists(positionDetailsName), JsonRequestBehavior.AllowGet);
        }

        #region Position List
        public JsonResult GetPositionList()
        {
            var model = new Object();
            model = PositionManager.GetList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion



        #region PositionType List
        public JsonResult GetPositionTypeList()
        {
            var model = new Object();
            model = PositionDetailsManager.GetPositionTypeList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
    }
}
