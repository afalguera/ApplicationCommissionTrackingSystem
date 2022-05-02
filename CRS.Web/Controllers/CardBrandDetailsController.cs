using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;


namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class CardBrandDetailsController : Controller
    {
        //
        // GET: /CardBrandDetails/

        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);
            return View();
        }

        public JsonResult GetCardBrandDetailsList()
        {
            var jsonResult = Json(CardBrandDetailsManager.GetList(), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
       }

        [HttpDelete]
        public JsonResult DeleteCardBrandDetails(int cardBrandDetailsId)
        {
            return Json(CardBrandDetailsManager.Delete(cardBrandDetailsId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveCardBrandDetails(CardBrandDetails cardBrandDetails)
        {
            cardBrandDetails.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardBrandDetailsManager.Save(cardBrandDetails));
        }

        [HttpPut]
        public JsonResult UpdateCardBrandDetails(CardBrandDetails cardBrandDetails)
        {
            cardBrandDetails.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardBrandDetailsManager.Update(cardBrandDetails));
        }

        public JsonResult IsCardBrandDetailsExist(string cardBrandCode, string channelCode)
        {
            return Json(!CardBrandDetailsManager.IsCardBrandDetailsExist(cardBrandCode,channelCode), JsonRequestBehavior.AllowGet);
        }      
    }
}
