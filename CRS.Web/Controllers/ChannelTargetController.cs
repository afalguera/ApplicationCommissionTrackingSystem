using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;
using System;

namespace MvcApplication1.Controllers
{
    //[ACTSAuthorize]
    public class ChannelTargetController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetChannelTargetList()
        {
            return Json(ChannelTargetManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteChannelTarget(int channelTargetId)
        {
            return Json(ChannelTargetManager.Delete(channelTargetId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveChannelTarget(ChannelTarget channelTarget)
        {
            channelTarget.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelTargetManager.Save(channelTarget));
        }

        [HttpPost]
        public JsonResult EditChannelTarget(ChannelTarget channelTarget)
        {
            channelTarget.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelTargetManager.Update(channelTarget));
        }

        public JsonResult IsChannelTargetValid(string channelCode, string year)
        {
            return Json(!ChannelTargetManager.CheckIfExists(channelCode, year), JsonRequestBehavior.AllowGet);
        }

        #region Channel List
        public JsonResult GetChannelList()
        {
            var model = new Object();
            model = ChannelManager.GetList();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
    }
}
