using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Dal;
using CRS.Helpers;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using web.ui.viewmodel;

namespace MvcApplication1.Controllers
{
    [ACTSAuthorize]
    [AjaxAuthorize]
    public class ChannelDetailsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.PageAccess = SessionWrapper.PageAccess.FirstOrDefault(m => m.PageName == RouteData.Values["Controller"] as string);

            //return View();
            return View("~/Views/ChannelDetails/Index.cshtml");
        }


        #region EF

        public ActionResult ViewCreateChannelDetails()
        {
            return View();  
        }

        [HttpPost]
        public ActionResult CreateChannelDetailsEF(ChannelDetail channelDetail)
        {
            using (var context = new ACTSdbContext())
            {
                context.ChannelDetails.Add(channelDetail);
                context.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public ActionResult ReadChannelDetailsEF()
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelDetails.ToList();
                return Json(data);
            }
        }

        public ActionResult ViewEditChannelDetails(int channelDetailsId)
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelDetails.Where(x => x.ChannelDetailsId.Equals(channelDetailsId)).SingleOrDefault();
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditChannelDetailsEF(int channelDetailsId, ChannelDetail channelDetail)
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelDetails.FirstOrDefault(x => x.ChannelDetailsId.Equals(channelDetailsId));

                if (data != null)
                {
                    context.SaveChanges();
                    return View();
                }
                else return View();
            }
        }

        public ActionResult ViewDeleteChannelDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteChannelDetailsEF(int studentId)
        {
            using(var context = new ACTSdbContext())
            {
                var data = context.ChannelDetails.FirstOrDefault(x => x.ChannelDetailsId.Equals(studentId));
                if (data != null)
                {
                    context.ChannelDetails.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Index");   
                }
                else
                {
                    return View();
                }
            }    
        }

        #endregion

        #region ChannelCommissionRateEF

        [HttpGet]
        public ActionResult GetChannelCommissionRateEF(int id)
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelCommissionRates.Where(x => x.CommissionRateId.Equals(id)).FirstOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);  
            };
        }

        [HttpGet]
        public ActionResult GetChannelCommissionRateListEF()
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelCommissionRates.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            };
        }

        [HttpPost]
        public ActionResult CreateChannelCommissionRateEF(ChannelCommissionRate channelCommissionRate)
        {
            using (var context = new ACTSdbContext())
            {
                
                string dateTimeNow = System.DateTime.Now.ToString();
                channelCommissionRate.CreatedDate = System.DateTime.ParseExact(dateTimeNow, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                channelCommissionRate.LastModifyDate = System.DateTime.ParseExact(dateTimeNow, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                channelCommissionRate.IsDeleted = false;
                channelCommissionRate.CreatedBy = SessionWrapper.CurrentUser.UserName;
                channelCommissionRate.LastModifyBy = SessionWrapper.CurrentUser.UserName;

                var data = context.ChannelCommissionRates.Add(channelCommissionRate);
                context.SaveChanges();
                return Json(data, JsonRequestBehavior.AllowGet);    

            };
        }

        [HttpPost]
        public ActionResult UpdateChannelCommissionRateEF(int id, ChannelCommissionRate channelCommissionRate)
        {
            using (var context = new ACTSdbContext())
            {
                
                var dateTimeNow = System.DateTime.Now.ToString();
                channelCommissionRate.LastModifyDate = System.DateTime.ParseExact(dateTimeNow, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                channelCommissionRate.LastModifyBy = SessionWrapper.CurrentUser.UserName;

                var data = context.ChannelCommissionRates.First(x => x.CommissionRateId.Equals(id));
                context.Entry(data).CurrentValues.SetValues(channelCommissionRate);
                context.SaveChanges();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteChannelCommissionRateEF(int id)
        {
            using (var context = new ACTSdbContext())
            {
                var data = context.ChannelCommissionRates.FirstOrDefault(x => x.CommissionRateId.Equals(id));
                
                if(data != null)
                {
                    context.ChannelCommissionRates.Remove(data);
                    context.SaveChanges();
                }
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region SPCode
        public JsonResult GetChannelDetailsList()
        {
            return Json(ChannelDetailsManager.GetList(), JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public JsonResult DeleteChannelDetails(int channelDetailsId)
        {
            return Json(ChannelDetailsManager.Delete(channelDetailsId, SessionWrapper.CurrentUser.UserName));
        }

        [HttpPost]
        public JsonResult SaveChannelDetails(ChannelDetails channelDetails)
        {
            channelDetails.CreatedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelDetailsManager.Save(channelDetails));
        }

        [HttpPost]
        public JsonResult EditChannelDetails(ChannelDetails channelDetails)
        {
            channelDetails.ModifiedBy = SessionWrapper.CurrentUser.UserName;

            return Json(ChannelDetailsManager.Update(channelDetails));
        }
        #endregion


        //[HttpGet]
        //public ActionResult Test() 
        //{
        //    try
        //    {
        //        Test objTest = new Test()
        //        {
        //            name = "alfred",
        //            address = "Manila"
        //        };
        //        return PartialView("~/Views/_TestPartialView.cshtml", objTest);
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}


    }
}
