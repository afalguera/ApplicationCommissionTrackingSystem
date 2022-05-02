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
    public class CardApplicantController : Controller
    {
        //
        // GET: /CardApplicant/

        public ActionResult Index()
        {
            var model = UserPageAccessManager.GetList(User.Identity.Name);
            ViewBag.UserPageAccesses = model;
            
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View();
        }

        //
        // GET: /CardApplicant/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CardApplicant/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CardApplicant/Create

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
        // GET: /CardApplicant/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CardApplicant/Edit/5

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
        // GET: /CardApplicant/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CardApplicant/Delete/5

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
