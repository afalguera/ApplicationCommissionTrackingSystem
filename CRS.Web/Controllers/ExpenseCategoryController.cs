using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class ExpenseCategoryController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetExpenseCategoryList()
        {
            return Json(ExpenseCategoryManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteExpenseCategory(int expenseCategoryId)
        {
            return Json(ExpenseCategoryManager.Delete(expenseCategoryId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveExpenseCategory(ExpenseCategory expenseCategory)
        {
            expenseCategory.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ExpenseCategoryManager.Save(expenseCategory));
        }

        [HttpPut]
        public JsonResult UpdateExpenseCategory(ExpenseCategory expenseCategory)
        {
            expenseCategory.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ExpenseCategoryManager.Update(expenseCategory));
        }

        public JsonResult IsExpenseCategoryCodeValid(string expenseCategoryCode)
        {
            return Json(!ExpenseCategoryManager.ExpenseCategoryCodeExists(expenseCategoryCode), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsExpenseCategoryNameValid(string expenseCategoryName)
        {
            return Json(!ExpenseCategoryManager.ExpenseCategoryNameExists(expenseCategoryName), JsonRequestBehavior.AllowGet);
        }
    }
}
