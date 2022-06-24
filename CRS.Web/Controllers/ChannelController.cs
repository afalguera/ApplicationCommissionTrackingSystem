using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Dal;
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

        #region EF
        [HttpGet]
        public JsonResult ViewChannelEF()
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.Channels.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetChannelDetailsEF(int id)
        {
            using (var context = new ACTSdbContext())
            {
                var channelId = context.Channels.Select(m => m.ChannelId);
                var data = context.ChannelDetails.Where(x => x.ChannelId.Equals(channelId)).ToList();
                return PartialView("~/Views/ChannelDetails/_GetChannelDetailsFromChannel" , data);
            };
        }

        #endregion

        #region SPCode
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
        public JsonResult SaveChannel(CRS.BusinessEntities.Channel channel)
        {
            channel.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelManager.Save(channel));
        }

        [HttpPost]
        public JsonResult EditChannel(CRS.BusinessEntities.Channel channel)
        {
            channel.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelManager.Update(channel));
        }
        #endregion
    }

    //public class Test
    //{
    //    public int ChannelCode { get; set; }
    //    public string ChannelName { get; set; }
    //    public int CommissionRate { get; set; }
    //}
}
