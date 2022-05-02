using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    public class RolePageAccessController : Controller
    {
        //
        // GET: /RolePageAccess/

        public ActionResult Index()
        {
            var obj = new Object();

            obj = RoleManager.GetList();
            ViewBag.Roles = obj;

            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
            ViewBag.PageAccess = usrpageacc;

            return View();
    
        }

        public ActionResult RolePage(int Id,string Title)
        {
            var obj = new Object();
            ViewBag.Title = "Page access for " + Title;
            ViewBag.RoleId = Id;
            obj = RolePageAccessManager.GetList(Id);
            ViewBag.RolePageAccess = obj;
            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
            ViewBag.PageAccess = usrpageacc;
            return View();

        }
        
        //
        // GET: /RolePageAccess/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RolePageAccess/Create

        public ActionResult Create(int Id)
        {
            var model = new Object();

            // pages = PageManager.(id);
            ViewBag.AvailPages = PageManager.GetList(Id);
            return View();
        }

        //
        // POST: /RolePageAccess/Create

        [HttpPost]
        public ActionResult Create(RolePageAccess rolepageacc,int Id)
        {
            try
            {
                rolepageacc.RoleId = Id;
                rolepageacc.ID = 0;
                rolepageacc.CreatedBy = SessionWrapper.CurrentUser.UserName;
                int result = RolePageAccessManager.Save(rolepageacc);
                SessionWrapper.PageAccess = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
           
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // GET: /RolePageAccess/Edit/5

        public ActionResult Edit(int id)
        {
            var model = new Object();

            RolePageAccess selectedimage = RolePageAccessManager.GetItem(id);

            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
            ViewBag.PageAccess = usrpageacc;

            
            return View(selectedimage);

            
            
        }

        //
        // POST: /RolePageAccess/Edit/5

        [HttpPost]
        public ActionResult Edit(RolePageAccess rolepageacc)
        {
            try
            {
                rolepageacc.ModifiedBy = SessionWrapper.CurrentUser.UserName;
                int result = RolePageAccessManager.Save(rolepageacc);
                SessionWrapper.PageAccess = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
                var data = RouteData.Values["Controller"].ToString();
                UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
                ViewBag.PageAccess = usrpageacc;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // GET: /RolePageAccess/Delete/5

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                int result = RolePageAccessManager.Delete(id);
                SessionWrapper.PageAccess = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
                var data = RouteData.Values["Controller"].ToString();
                UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());
                ViewBag.PageAccess = usrpageacc;
                return RedirectToAction("Index", "RolePageAccess");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id = id });
            }
        }

        //
        // POST: /RolePageAccess/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
