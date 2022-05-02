using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class BudgetAllocationApproverController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetBudgetAllocationApproverList()
        {
            return Json(BudgetAllocationApproverManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteBudgetAllocationApprover(int budgetAllocationApproverId)
        {
            return Json(BudgetAllocationApproverManager.Delete(budgetAllocationApproverId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveBudgetAllocationApprover(BudgetAllocationApprover budgetAllocationApprover)
        {
            budgetAllocationApprover.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(BudgetAllocationApproverManager.Save(budgetAllocationApprover));
        }

        [HttpPut]
        public JsonResult UpdateBudgetAllocationApprover(BudgetAllocationApprover budgetAllocationApprover)
        {
            budgetAllocationApprover.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(BudgetAllocationApproverManager.Update(budgetAllocationApprover));
        }

        public JsonResult IsBudgetAllocationApproverCodeValid(string budgetAllocationApproverCode)
        {
            return Json(!BudgetAllocationApproverManager.BudgetAllocationApproverCodeExists(budgetAllocationApproverCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsBudgetAllocationApproverTitleValid(string budgetAllocationApproverTitle)
        {
            return Json(!BudgetAllocationApproverManager.BudgetAllocationApproverTitleExists(budgetAllocationApproverTitle), JsonRequestBehavior.AllowGet);
        }
    }
}
