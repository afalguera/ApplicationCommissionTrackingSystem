using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;


namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    public class BranchManagerController : Controller
    {
        //
        // GET: /BranchManager/

        public ActionResult Index()
        {
            var model = UserPageAccessManager.GetList(User.Identity.Name);
            ViewBag.UserPageAccesses = model;
           
            return View();
        }

        //
        // GET: /BranchManager/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BranchManager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BranchManager/Create

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
        // GET: /BranchManager/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /BranchManager/Edit/5

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
        // GET: /BranchManager/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BranchManager/Delete/5

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

        public ActionResult List()
        {
             var model = BranchManagerManager.GetList();
             ViewBag.BranchManagers = model;

           
             return View();

        }
    }
}
