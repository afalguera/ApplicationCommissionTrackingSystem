using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace CRS.Controllers
{   
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class BranchController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetBranchList()
        {
            return Json(CRS.Bll.BranchManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCascadingLocationList(int branchId)
        {
            return Json(CRS.Bll.BranchManager.GetCascadingLocation(branchId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBranchCodeValid(string branchCode)
        {
            return Json(!CRS.Bll.BranchManager.BranchCodeExists(branchCode), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBranch(Branch branch)
        {
            branch.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CRS.Bll.BranchManager.Save(branch));
        }

        [HttpPost]
        public JsonResult EditBranch(Branch branch)
        {
            branch.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CRS.Bll.BranchManager.Update(branch));
        }

        [HttpDelete]
        public JsonResult DeleteBranch(int branchId)
        {
            return Json(CRS.Bll.BranchManager.Delete(branchId, SessionWrapper.CurrentUser.UserName));
        }

        #region BranchExists
        [HttpGet]
        public JsonResult IsBranchExists(string channelCode, string branchCode, string branchName)
        {
            var isBranchExists = CRS.Bll.BranchManager.IsBranchExists(channelCode, branchCode, branchName);
            return Json(!isBranchExists, JsonRequestBehavior.AllowGet);
            //return Json(new { isDuplicate = isBranchExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
