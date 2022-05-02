using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            return View();
        }

        public JsonResult GetEmployee(int employeeId)
        {
            return Json(EmployeeManager.GetItem(employeeId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeeList()
        {
            return Json(EmployeeManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmployeeNumberValid(string employeeNumber)
        {
            return Json(!EmployeeManager.AccountNumberExists(employeeNumber), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveEmployee(Employee employee)
        {
            employee.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(EmployeeManager.Save(employee));
        }

        [HttpPost]
        public JsonResult EditEmployee(Employee employee)
        {
            employee.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(EmployeeManager.Update(employee));
        }

        [HttpDelete]
        public JsonResult DeleteEmployee(int employeeId)
        {
            return Json(EmployeeManager.Delete(employeeId, SessionWrapper.CurrentUser.UserName));
        }
    }
}
