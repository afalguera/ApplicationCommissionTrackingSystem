using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ChannelController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetChannelList()
        {
            return Json(ChannelManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPositionDetailsList()
        {
            return Json(ChannelManager.GetPositionDetailsList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteChannel(int channelId)
        {
            return Json(ChannelManager.Delete(channelId, SessionWrapper.CurrentUser.UserName));
        }

        public JsonResult IsChannelCodeValid(string channelCode)
        {
            return Json(!ChannelManager.ChannelCodeExists(channelCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsChannelNameValid(string channelName)
        {
            return Json(!ChannelManager.ChannelNameExists(channelName), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveChannel(Channel channel)
        {
            channel.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelManager.Save(channel));
        }

        [HttpPost]
        public JsonResult EditChannel(Channel channel)
        {
            channel.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelManager.Update(channel));
        }
    }
}
