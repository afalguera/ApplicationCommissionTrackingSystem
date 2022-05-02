using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;
using System;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class CardTypeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetCardTypeList()
        {
            return Json(CardTypeManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteCardType(int cardTypeId)
        {
            return Json(CardTypeManager.Delete(cardTypeId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveCardType(CardType cardType)
        {
            cardType.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardTypeManager.Save(cardType));
        }

        [HttpPut]
        public JsonResult UpdateCardType(CardType cardType)
        {
            cardType.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(CardTypeManager.Update(cardType));
        }

        public JsonResult IsCardTypeCodeValid(string cardTypeCode)
        {
            return Json(!CardTypeManager.CardTypeCodeExists(cardTypeCode, null, null), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult IsCardTypeNameValid(string cardTypeName, string cardSubTypeCode)
        //{
        //    return Json(!CardTypeManager.CardTypeNameExists(null, cardTypeName, cardSubTypeCode), JsonRequestBehavior.AllowGet);
        //}

        #region GetCardTypes
        public JsonResult GetCardBrandList()
        {
            var model = new Object();
            model = ApplicationStatusManager.GetCardBrands();
            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion

        #region GetCardSubTypes
        public JsonResult GetCardSubTypes()
        {
            return Json(CardTypeManager.GetCardSubTypeList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
