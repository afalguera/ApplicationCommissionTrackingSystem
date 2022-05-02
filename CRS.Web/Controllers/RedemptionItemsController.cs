using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;


namespace CRS.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class RedemptionItemsController : Controller
    {
        //
        // GET: /RedemptionItems/

        public ActionResult Index()
        {
            //var model = new Object();

           
            //ViewBag.UserPageAccesses = model;

            //model = RedemptionItemsManager.GetList(1);
            //ViewBag.RedemptionItemsList = model;

            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());

            ViewBag.PageAccess = usrpageacc;

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView();
            //}
            IEnumerable<string> files = Directory.EnumerateFiles(Server.MapPath("~/Media"));

            ViewBag.ImageFiles = files;
            ViewBag.DefaultImage = files.First<string>();

            return View();

            return View();
            
            
        }

        //
        // GET: /RedemptionItems/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RedemptionItems/Create

        public ActionResult Create()
        {
            var model = new Object();

            model = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
            ViewBag.UserPageAccesses = model;

            
            IEnumerable<string> files = Directory.EnumerateFiles(Server.MapPath("~/Media"));

            ViewBag.ImageFiles = files;
            ViewBag.DefaultImage = files.First<string>();

            return View();
            
            
        }

        //
        // POST: /RedemptionItems/Create

        [HttpPost]
        public ActionResult Create(RedemptionItems redemptionitems)
        {
            try
            {
                int result = RedemptionItemsManager.Save(redemptionitems);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /RedemptionItems/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RedemptionItems/Edit/5

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
        // GET: /RedemptionItems/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RedemptionItems/Delete/5

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

        public JsonResult GetRedemptionItems()
        {
            return Json(RedemptionItemsManager.GetList(1), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteRedemptionItem(int itemId)
        {
            string deletedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RedemptionItemsManager.Delete(itemId, deletedBy));
        }

        #region Redemption Item Name Exists
        [HttpGet]
        public JsonResult IsRedemptionItemNameExists(string redemptionItemName)
        {
            var isNameExists = RedemptionItemsManager.IsRedemptionItemNameExists(redemptionItemName);
            return Json(new { isDuplicate = isNameExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Redemption Item Code Exists
        [HttpGet]
        public JsonResult IsRedemptionItemCodeExists(string redemptionItemCode)
        {
            var isCodeExists = RedemptionItemsManager.IsRedemptionItemCodeExists(redemptionItemCode);
            return Json(new { isDuplicate = isCodeExists }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public JsonResult SaveRedemptionItem(RedemptionItems redemptionItem)
        {
            redemptionItem.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RedemptionItemsManager.Save(redemptionItem));
        }

        [HttpPut]
        public JsonResult UpdateRedemptionItem(RedemptionItems redemptionItem)
        {
            redemptionItem.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(RedemptionItemsManager.Save(redemptionItem));
        }
    }
}
