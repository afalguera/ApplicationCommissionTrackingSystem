using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using CRS.Helper;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class MediaLibraryController : Controller
    {
        //
        // GET: /Media/

        [HttpGet]
        public ActionResult Index()
        {
            //var model = new Object();
            //model = UserPageAccessManager.GetList(User.Identity.Name);
            //ViewBag.UserPageAccesses = model;

            
            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());

            ViewBag.PageAccess = usrpageacc;

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView();
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Media"), fileName);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }
        
        
        //
        // GET: /Media/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Media/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Media/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Media/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Media/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Media/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Media/Delete/5

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

        public JsonResult GetMediaLibraryList()
        {
            string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Media");
            DirectoryInfo d = new DirectoryInfo(strPath);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles(); //Getting files
            List<MediaLibrary> list = new List<MediaLibrary>();
            foreach (FileInfo file in Files)
            {    list.Add(new MediaLibrary {
                        FileName = file.Name,
                        FileType = file.Extension,
                        FilePath = strPath + "/" + file.Name,
                        FileSize = file.Length.ToFileSize()
                });                 
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public void DeleteMediaLibrary(string imgPath)
        {
            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }
        }
        #region FileExists
        [HttpGet]
        public JsonResult IsFileExists(string filePath)
        {
            string filename = string.Empty;
            Uri uri = new Uri(filePath);
            if (uri.IsFile)
            {
                filename = System.IO.Path.GetFileName(uri.LocalPath);
            }

            string strPath = System.Web.HttpContext.Current.Server.MapPath("~/Media") + "/" + filename;
            var isFileExists = System.IO.File.Exists(strPath) || Directory.Exists(strPath);
            return Json(new { isDuplicate = isFileExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void SaveMediaLibrary(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        var path = Path.Combine(Server.MapPath("~/Media"), fileName);
                        file.SaveAs(path);
                    }
                }
            }

            Response.Redirect("Index");
        }
    }
}
