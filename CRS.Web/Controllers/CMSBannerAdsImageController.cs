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
    public class CMSBannerAdsImageController : Controller
    {

        public ActionResult Index()
        {
            //var model = new Object();
            
            //model = CMSBannerAdsImageManager.GetList("MainBanner");
            //ViewBag.BannerAdsList = model;
            
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
        }

       

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new Object();

            model = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
            ViewBag.UserPageAccesses = model;

            model = ChannelManager.GetList();
            ViewBag.Channels = model;

            IEnumerable<string> files = Directory.EnumerateFiles(Server.MapPath("~/Media"));
            
            ViewBag.ImageFiles = files;
            ViewBag.DefaultImage = files.First<string>();

            return View();
        }

        
        [HttpPost]
        public ActionResult Create(CMSBannerAdsImage adsImage)
        {
            adsImage.RoleId = 3;
            try
            {
                int result = CMSBannerAdsImageManager.Save(adsImage);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       

        public ActionResult Edit(int id)
        {
            var model = new Object();

            model = ChannelManager.GetList();
            ViewBag.Channels = model;
            
            IEnumerable<string> files =  Directory.EnumerateFiles(Server.MapPath("~/Media"));
            
            ViewBag.ImageFiles = files;
            ViewBag.DefaultImage = files.First<string>();

            CMSBannerAdsImage selectedimage = CMSBannerAdsImageManager.GetItem(id);

            var data = RouteData.Values["Controller"].ToString();
            UserPageAccess usrpageacc = SessionWrapper.PageAccess.FirstOrDefault<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == data.ToString());

            ViewBag.PageAccess = usrpageacc;

            return View(selectedimage);
        }


        public JsonResult RoleList(string id)
        {
            var rolelist = RoleManager.GetList(id);
            return Json(new SelectList(rolelist.ToArray(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

       

        [HttpPost]
        public ActionResult Edit(CMSBannerAdsImage adsImage)
        {
            adsImage.RoleId = 3;
            try
            {
                int result = CMSBannerAdsImageManager.Save(adsImage);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

       
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                int result = CMSBannerAdsImageManager.Delete(id);
                return RedirectToAction("Index", "CMSBannerAdsImage");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Edit", new { id = id });
            }
        }

        public JsonResult GetCMSBannerAds()
        {
            return Json(CMSBannerAdsImageManager.GetList("MainBanner"), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteCMSBannerAds(int bannerAdsId)
        {
            string deletedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CMSBannerAdsImageManager.Delete(bannerAdsId, deletedBy));
        }
        [HttpPut]
        public JsonResult UpdateCMSBannerAds(CMSBannerAdsImage adsImage)
        {
            adsImage.ModifiedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CMSBannerAdsImageManager.Save(adsImage));
        }
        [HttpPost]
        public JsonResult SaveCMSBannerAd(CMSBannerAdsImage adsImage)
        {
            adsImage.CreatedBy = SessionWrapper.CurrentUser.UserName;
            return Json(CMSBannerAdsImageManager.Save(adsImage));
        }


     
    }
}
