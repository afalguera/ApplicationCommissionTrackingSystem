using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;
using web.ui.viewmodel;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ChannelDetailsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            //return View();
            return View("~/ChannelDetails/Index.cshtml");
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

        [HttpGet]
        public ActionResult Test() 
        {
            try
            {
                Test objTest = new Test()
                {
                    name = "alfred",
                    address = "Manila"
                };
                return PartialView("~/Views/_TestPartialView.cshtml", objTest);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


    }
}
