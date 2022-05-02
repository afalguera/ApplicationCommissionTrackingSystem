using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.Helpers;

namespace CRS.Controllers
{
    [ACTSAuthorize]
    public class RedeemPointsController : Controller
    {
        //
        // GET: /RedeemPoints/

        public ActionResult Index()
        {
            var model = new Object();

            
            ViewBag.UserPageAccesses = model;

            model = RedemptionItemsManager.GetList(0);
            ViewBag.RedeemPointsList = model;

            return View();
        }

        //
        // GET: /RedeemPoints/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RedeemPoints/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RedeemPoints/Create

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
        // GET: /RedeemPoints/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RedeemPoints/Edit/5

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
        // GET: /RedeemPoints/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RedeemPoints/Delete/5

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
