using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class OutletController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetOutletList()
        {
            return Json(CRS.Bll.OutletManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsOutletCodeValid(string outletCode)
        {
            return Json(!CRS.Bll.OutletManager.OutletCodeExists(outletCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveOutlet(Outlet outlet)
        {
            outlet.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CRS.Bll.OutletManager.Save(outlet));
        }

        [HttpPut]
        public JsonResult UpdateOutlet(Outlet outlet)
        {
            outlet.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CRS.Bll.OutletManager.Update(outlet));
        }

        [HttpDelete]
        public JsonResult DeleteOutlet(int outletId)
        {
            return Json(CRS.Bll.OutletManager.Delete(outletId, SessionWrapper.CurrentUser.UserName));
        }

        #region OutletExists
        [HttpGet]
        public JsonResult IsOutletExists(string outletName)
        {
            var isOutletExists = CRS.Bll.OutletManager.IsOutletExists(outletName);
            return Json(new { isDuplicate = isOutletExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
