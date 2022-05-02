using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetPaymentList()
        {
            return Json(PaymentManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeletePayment(int paymentId)
        {
            return Json(PaymentManager.Delete(paymentId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SavePayment(string code, string name, string tin, bool isVatable = false, bool isGross = false)
        {
            var payment = new Payment
            {
                Code = code,
                Name = name,
                Tin = tin,
                IsGross = isGross,
                IsVatable = isVatable,
                CreatedBy = SessionWrapper.CurrentUser.UserName
            };
            //payment.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(PaymentManager.Save(payment));
        }

        [HttpPut]
        public JsonResult UpdatePayment(int id, string code, string name, string tin, bool isVatable = false, bool isGross = false)
        {
            var payment = new Payment
            {
                ID = id,
                Code = code,
                Name = name,
                Tin = tin,
                IsGross = isGross,
                IsVatable = isVatable,
                ModifiedBy = SessionWrapper.CurrentUser.UserName
            };
            //payment.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(PaymentManager.Update(payment));
        }

        public JsonResult IsPaymentCodeValid(string paymentCode)
        {
            return Json(!PaymentManager.PaymentCodeExists(paymentCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsPaymentNameValid(string paymentName)
        {
            return Json(!PaymentManager.PaymentNameExists(paymentName), JsonRequestBehavior.AllowGet);
        }
    }
}
