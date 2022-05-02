using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class PositionController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetPositionList()
        {
            return Json(PositionManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeletePosition(int positionId)
        {
            return Json(PositionManager.Delete(positionId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SavePosition(Position position)
        {
            position.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(PositionManager.Save(position));
        }

        [HttpPut]
        public JsonResult UpdatePosition(Position position)
        {
            position.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(PositionManager.Update(position));
        }

        public JsonResult IsPositionCodeValid(string positionCode)
        {
            return Json(!PositionManager.PositionCodeExists(positionCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPositionNameValid(string positionName)
        {
            return Json(!PositionManager.PositionNameExists(positionName), JsonRequestBehavior.AllowGet);
        }
    }
}
