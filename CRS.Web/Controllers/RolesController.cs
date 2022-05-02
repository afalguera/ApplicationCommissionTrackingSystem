using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Helpers;
using CRS.Bll;
using CRS.BusinessEntities;
using System.IO;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class RolesController : Controller
    {
        public ActionResult Index()
        {
            //var obj = new Object();

            //obj = RoleManager.GetList();
            //ViewBag.Roles = obj;

            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());

            ViewBag.PageAccess = usrpageacc;

            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //public ActionResult Create()
        //{
        //    var obj = new Object();

        //    return View();
        //}


        //[HttpPost]
        //public ActionResult Create(Role role)
        //{
        //    try
        //    {
        //        role.CreatedBy = SessionWrapper.CurrentUser.UserName;
        //        int result = RoleManager.Save(role);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        //public ActionResult Edit(int id)
        //{
        //    var model = new Object();

        //    Role selectedimage = RoleManager.GetItem(id);
            
        //    var data = RouteData.Values["Controller"].ToString();
        //    UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
        //    ViewBag.PageAccess = usrpageacc;

        //    return View(selectedimage);
        //}


        public JsonResult RoleList(string id)
        {
            var rolelist = RoleManager.GetList(id);
            return Json(new SelectList(rolelist.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetRoleList()
        //{
        //    return Json(new SelectList(RoleManager.GetList().ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult Edit(Role role)
        //{
        //    try
        //    {
        //        role.ModifiedBy = SessionWrapper.CurrentUser.UserName;
        //        int result = RoleManager.Save(role);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //}


        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        int result = RoleManager.Delete(id);
        //        return RedirectToAction("Index", "Roles");
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Edit", new { id = id });
        //    }
        //}

        [HttpPost]
        public JsonResult SaveRole(Role role)
        {
            role.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RoleManager.Save(role));
        }

        [HttpPost]
        public JsonResult EditRole(Role role)
        {
            role.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RoleManager.Save(role));
        }


        [HttpDelete]
        public JsonResult DeleteRole(int roleId)
        {
            string deletedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RoleManager.Delete(roleId, deletedBy));
        }

        public JsonResult GetRoleList()
        {
            return Json(RoleManager.GetRoleList(), JsonRequestBehavior.AllowGet);
        }

        #region RoleExists
        [HttpGet]
        public JsonResult IsRoleExists(string roleName)
        {
            var isRoleExists = RoleManager.IsRoleExists(roleName);
            return Json(new { isDuplicate = isRoleExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
