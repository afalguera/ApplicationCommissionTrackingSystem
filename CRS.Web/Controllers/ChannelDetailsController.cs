using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ChannelDetailsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetChannelDetailsList()
        {
            return Json(ChannelDetailsManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteChannelDetails(int channelDetailsId)
        {
            return Json(ChannelDetailsManager.Delete(channelDetailsId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveChannelDetails(ChannelDetails channelDetails)
        {
            channelDetails.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelDetailsManager.Save(channelDetails));
        }

        [HttpPost]
        public JsonResult EditChannelDetails(ChannelDetails channelDetails)
        {
            channelDetails.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelDetailsManager.Update(channelDetails));
        }
    }
}
