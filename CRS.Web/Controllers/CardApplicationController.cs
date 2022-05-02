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
    public class CardApplicationController : Controller
    {
        //
        // GET: /CardApplication/
        [AllowApplicantAttribute]
        [HttpGet]
        public ActionResult Index()
        {
            if (SessionWrapper.UserType == UserType.Referror)
            {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;

                SessionWrapper.BannerAdsCollection = CMSBannerAdsImageManager.GetList("MainBanner", (int)SessionWrapper.CurrentUser.Role);
                
                model = CardApplicationManager.GetList(User.Identity.Name);
                ViewBag.CardApplications = model;
            }
            else {
                var model = new Object();
                model = UserPageAccessManager.GetList(User.Identity.Name);
                ViewBag.UserPageAccesses = model;

                //SessionWrapper.BannerAdsCollection = CMSBannerAdsImageManager.GetList("MainBanner", (int)SessionWrapper.CurrentUser.Role);
                
                //model = CMSBannerAdsImageManager.GetList("MainBanner", (int)SessionWrapper.CurrentUser.Role);
               // ViewBag.BannerAds = model;
                
                CardApplicationCollection col = new CardApplicationCollection();
                col.Add(SessionWrapper.CurrentCardHolder);
                model = col;
                ViewBag.CardApplications = model;

            }
            
            
            if (Request.IsAjaxRequest())
            {
                return PartialView();
                
            }

            //return View();

            return RedirectToAction("Index", "ApplicationStatus");

        }

        
        //
        // GET: /CardApplication/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CardApplication/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CardApplication/Create

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
        // GET: /CardApplication/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /CardApplication/Edit/5

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
        // GET: /CardApplication/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /CardApplication/Delete/5

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
