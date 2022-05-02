using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class UserManagementController : Controller
    {
        //
        // GET: /UserManagement/

        public ActionResult Index()
        {
            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<UserPageAccess>(m => m.PageName == data.ToString());
            ViewBag.PageAccess = usrpageacc;
            return View();
            //return View(UserManager.GetUserList());
        }

        public JsonResult GetUserList()
        {
            string currUser = User.Identity.Name;
            var list = UserManager.GetUserList().Where(x => x.UserName != currUser);
            //return Json(UserManager.GetUserList(), JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Maintenance(String username = null)
        {
            ViewBag.Mode = "Add";
            ViewBag.Title = "Add User";
            User user = new User();
            if (!String.IsNullOrEmpty(username))
            {
                ViewBag.Mode = "Edit";
                ViewBag.Title = "Edit User";
                user = UserManager.GetItem(username);
                if (user == null)
                {
                    return HttpNotFound();
                }
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Maintenance(User user)
        {
            if (user.ID > 0)
            {
                user.ModifiedBy = SessionWrapper.CurrentUser.UserName;
                UserManager.Update(user);
            }
            else
            {
                user.Password = System.Configuration.ConfigurationManager.AppSettings["DefaultPassword"];
                user.CreatedBy = SessionWrapper.CurrentUser.UserName;
                UserManager.Save(user);
            }

            return RedirectToAction("GetUserList");
        }

        public JsonResult IsUserNameValid(string username)
        {
            return Json(UserManager.GetItem(username) == null, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            return Json(UserManager.Delete(userId, SessionWrapper.CurrentUser.UserName));
        }

        #region UserExists
        [HttpGet]
        public JsonResult IsUserExists(string userName)
        {
            var isUserExists = UserManager.IsUserExists(userName);
            return Json(!isUserExists, JsonRequestBehavior.AllowGet);
            //return Json(new { isDuplicate = isUserExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
