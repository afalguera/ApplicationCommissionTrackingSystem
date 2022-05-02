using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ManagerController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetManagerList()
        {
            return Json(ManagerManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetManagerTypeList()
        {
            return Json(ManagerManager.GetManagerTypeList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveManager(Manager manager)
        {
            manager.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(ManagerManager.Save(manager));
        }

        [HttpPut]
        public JsonResult UpdateManager(Manager manager)
        {
            manager.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(ManagerManager.Update(manager));
        }

        [HttpDelete]
        public JsonResult DeleteManager(int managerId)
        {
            return Json(ManagerManager.Delete(managerId, SessionWrapper.CurrentUser.UserName));
        }
        //[HttpGet]
        //public JsonResult GetOutletList(int branchId)
        //{
        //    return Json(ManagerManager.GetOutletList(branchId), JsonRequestBehavior.AllowGet);
        //}
    }
}
