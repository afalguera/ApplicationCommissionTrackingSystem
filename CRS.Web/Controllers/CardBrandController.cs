using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class CardBrandController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        #region SPCode
        public JsonResult GetCardBrandList()
        {
            return Json(CardBrandManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteCardBrand(int cardBrandId)
        {
            return Json(CardBrandManager.Delete(cardBrandId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveCardBrand(CardBrand cardBrand)
        {
            cardBrand.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardBrandManager.Save(cardBrand));
        }

        [HttpPut]
        public JsonResult UpdateCardBrand(CardBrand cardBrand)
        {
            cardBrand.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardBrandManager.Update(cardBrand));
        }

        public JsonResult IsCardBrandCodeValid(string cardBrandCode)
        {
            return Json(!CardBrandManager.CardBrandCodeExists(cardBrandCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCardBrandNameValid(string cardBrandName)
        {
            return Json(!CardBrandManager.CardBrandNameExists(cardBrandName), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
