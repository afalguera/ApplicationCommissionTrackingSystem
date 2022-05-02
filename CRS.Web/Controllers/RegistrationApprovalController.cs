using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRS.Bll;
using CRS.BusinessEntities;
using CRS.Helpers;
using System.Text;

namespace CRS.Controllers
{
    
    [ACTSAuthorize]
    public class RegistrationApprovalController : Controller
    {
        //
        // GET: /RegistrationApproval/

        public ActionResult Index()
        {
            var obj = new Object();

            ViewBag.UserPageAccesses = obj;

            
            obj = UserManager.GetPendingRegistrations();
            ViewBag.PendingRegistrations = obj;

            return View();
        }

        //
        // GET: /RegistrationApproval/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RegistrationApproval/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RegistrationApproval/Create

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
        // GET: /RegistrationApproval/Edit/5

        public ActionResult Edit(int id)
        {
            var obj = new Object();

            obj = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
            ViewBag.UserPageAccesses = obj;

            obj = ChannelManager.GetList();
            ViewBag.Channels = obj;

            obj = RoleManager.GetList();
            ViewBag.Roles = obj;

            User selecteduser = UserManager.GetItem(id);
            return View(selecteduser);
            }

        //
        // POST: /RegistrationApproval/Edit/5

        [HttpPost]
        public ActionResult Edit(User usr)
        {
            {
                try
                {
                    int result = UserManager.ApproveUser(usr);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Your registration has been approved. You may now login using your preferred password and loginname");
                    CRS.Helper.Mailer.Send(sb.ToString(),"ACTS registration",usr.Email);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
        }

        //
        // GET: /RegistrationApproval/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RegistrationApproval/Delete/5

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
