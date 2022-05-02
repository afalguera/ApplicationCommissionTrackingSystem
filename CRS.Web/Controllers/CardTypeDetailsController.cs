using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;


namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class CardTypeDetailsController : Controller
    {
        //
        // GET: /CardTypeDetails/

        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
            return View();
        }

        public JsonResult GetCardTypeDetailsList()
        {
            var jsonResult = Json(CardTypeDetailsManager.GetList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpDelete]
        public JsonResult DeleteCardTypeDetails(int CardTypeDetailsId)
        {
            return Json(CardTypeDetailsManager.Delete(CardTypeDetailsId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveCardTypeDetails(CardTypeDetails CardTypeDetails)
        {
            CardTypeDetails.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardTypeDetailsManager.Save(CardTypeDetails));
        }

        [HttpPut]
        public JsonResult UpdateCardTypeDetails(CardTypeDetails CardTypeDetails)
        {
            CardTypeDetails.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardTypeDetailsManager.Update(CardTypeDetails));
        }

        public JsonResult IsCardTypeDetailsExist(string cardTypeCode, string channelCode)
        {
            return Json(!CardTypeDetailsManager.IsCardTypeDetailsExist(cardTypeCode, channelCode), JsonRequestBehavior.AllowGet);
        }
    }
}
