using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CRS.Models;
using CRS.BusinessEntities;
using CRS.Bll;
using CRS.Helpers;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace CRS.Controllers
{
    [Authorize]
    [AjaxAuthorize]
    public class AccountController : Controller
    {
        private static string retUrl;
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public FileResult CaptchaImage()
        {
            string prefix = "";
            bool noisy = true;
            var rand = new Random((int)DateTime.Now.Ticks);

            //generate new question
            int a = rand.Next(10, 99);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer
            Session["Captcha" + prefix] = a + b;

            //image stream
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        //gfx.DrawEllipse(pen, x – r, y – r, r, r);
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);



                    }
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string id, string returnUrl)
        {
            if (id == "SessionTimeout")
            {
                ViewBag.SessionTimeout = "Your session has expired";
            }
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "--Please Select--", Value = "0" });
            items.Add(new SelectListItem { Text = "Applicant", Value = "1" });
            items.Add(new SelectListItem { Text = "Application User", Value = "2" });

            ViewBag.UserType = new SelectList(items, "Value", "Text",0);
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.UserPageAccesses = null;
            return View();

            
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            
            var vm = new CRS.Models.ChangePasswordModel();
            return View(vm);

        }

        
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ChangePassword(string id, CRS.Models.ChangePasswordModel model)
        {
            
                User usr = new User();
                int userid;
                int result = 0;
                bool isnumeric = int.TryParse(id.Substring(id.Length - 5, 5), out userid);
                if (isnumeric)
                {
                    result = UserManager.ChangePassword(userid, model.NewPassword);
                }
                if (result > 0)
                {
                    //ViewBag.PasswordChanged = "Your password has been changed.";
                    return RedirectToRoute("Default", new
                    {
                        controller = "Account",
                        action = "LoginReferror",
                        id = "passwordchanged"
                    });
                }
                else
                {
                    ViewBag.PasswordChanged = "Invalid user";
                    return View();
                    /*
                    return RedirectToRoute("Default", new
                    {
                        controller = "Account",
                        action = "ChangePassword"
                    });
                     */ 
                }


                
                
             
        }
        
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
           
            var vm = new CRS.Models.ForgotPasswordModel();
            return View(vm);

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(CRS.Models.ForgotPasswordModel model)
        {

            Guid g; 

            g = Guid.NewGuid();
            string[] a = g.ToString().Split('-');
            

            string curID = UserManager.GetUserIdFromEmail(model.Email).ToString();
            if (! curID.Equals("0"))
            {
                string curURL = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)
                       + "/Account/ChangePassword/" + a[0] + curID.ToString().PadLeft(5, '0');



                StringBuilder sb = new StringBuilder();
                sb.Append("A request to change password has been submitted." + Environment.NewLine);
                sb.Append("Please click on the link: " + Environment.NewLine);
                sb.Append(curURL);


                Helper.Mailer.Send(sb.ToString(), "ACTS change password", model.Email);
                ViewBag.PostBackMessage = "Please check your email";

            }
            else
            {
                ViewBag.PostBackMessage = "Email address is not existing";
            }

            
           

           
            
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            var vm = new CRS.BusinessEntities.User();
            return View(vm);
            
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User usr)
        {
            try
            {


                User storeduser = UserManager.GetItem(usr.UserName);
                if (storeduser != null)
                {
                    ModelState.AddModelError(string.Empty, "The user is already existing");
                }
                else
                {
                    int result = UserManager.Save(usr);
                    ViewBag.SuccessMessage = "Your registration has been submitted. A confirmation email will be sent once approved";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("New ACTS registration:" + Environment.NewLine);
                    sb.Append("First Name: " + usr.FirstName + Environment.NewLine);
                    sb.Append("Middle Name: " + usr.MiddleName + Environment.NewLine);
                    sb.Append("Last Name: " + usr.MiddleName + Environment.NewLine);

                    Helper.Mailer.Send(sb.ToString(), "New ACTS registration");
                
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        
        
        
        
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginReferror(LoginReferrorModel model, string returnUrl)
        {
            if (! (model.IsValid(model.UserName, model.Password)))
            {
                ModelState.AddModelError("", "Invalid user credentials.");
            }
            if (!(model.IsValidCaptcha(model.Captcha, Session["Captcha"].ToString())))
            {
                ModelState.AddModelError("", "Invalid result on captcha");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RedeemedItemsCollection obj = RedeemedItemsManager.GetExpiredRedemptionList(SessionWrapper.CurrentUser.ID);
            SessionWrapper.Modules = ModuleManager.GetList();
            SessionWrapper.PageAccess = UserPageAccessManager.GetList(SessionWrapper.CurrentUser.UserName);
            SessionWrapper.AllowedToChangePassword = true;

            if (obj.Count() > 0)
            {
                StringBuilder sbEmailMsg = new StringBuilder();
                sbEmailMsg.Append(SessionWrapper.CurrentUser.UserName).Append(" has redeemed the following items: " + Environment.NewLine);
                foreach (RedeemedItems itm in obj)
                {
                    sbEmailMsg.Append("* " + itm.Name + Environment.NewLine);
                }

                Helper.Mailer.Send(sbEmailMsg.ToString(), "Redemption of items from " + SessionWrapper.CurrentUser.UserName);
                RedeemedItemsManager.EmailSent(SessionWrapper.CurrentUser.ID);

                //refresh again the User session variable to reflect the Points remaining.
                User curuser = UserManager.GetItem(model.UserName, model.Password);
                SessionWrapper.CurrentUser = curuser;
            }

            if (model.Password == System.Configuration.ConfigurationManager.AppSettings["DefaultPassword"])
            {
                return RedirectToAction("Manage", "Account");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true); 
                SessionWrapper.UserType = UserType.Referror;
                var rUrl = retUrl;
                bool withAccess = false;
                string module = "ApplicationStatus";

                if (!string.IsNullOrEmpty(rUrl))
                {
                    rUrl = retUrl.Remove(0, 1);
                    var result = rUrl.ToString().Split('/');
                    module = result[0] ?? string.Empty;
                }


                withAccess = SessionWrapper.PageAccess.Where(x => x.PageName.Equals(module, StringComparison.InvariantCultureIgnoreCase))
                                              .Select(x => x.CanView)
                                              .FirstOrDefault();
                SessionWrapper.BannerAdsCollection = CMSBannerAdsImageManager.GetList("MainBanner", (int)SessionWrapper.CurrentUser.Role);
                if (withAccess)
                {
                    return RedirectToAction("Index", module);
                }
                else
                {
                    var firstPage = SessionWrapper.PageAccess.Where(x=> x.CanView)
                                              .Select(x => x.PageName)
                                              .FirstOrDefault();
                    return RedirectToAction("Index", firstPage);
                }
               
            }
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginReferror(string id,string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.UserPageAccesses = null;
            ViewBag.PasswordChanged = "";
            if (id != null)
            {
                ViewBag.PasswordChanged = "Your password has been changed";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginCardholder(LoginCardholderModel model, string returnUrl)
        {

            if (ModelState.IsValid && model.IsValid(model))
            {
                string sDateConcat = model.DateOfBirth.Day.ToString() + model.DateOfBirth.Month.ToString() + model.DateOfBirth.Year.ToString();
                FormsAuthentication.SetAuthCookie(model.FirstName + "_" + model.MiddleName + "_" + model.LastName + "_" + sDateConcat, false);
                SessionWrapper.UserType = UserType.Cardholder;
                
                User curuser = new User();
                curuser.FirstName = model.FirstName;
                curuser.MiddleName = model.MiddleName;
                curuser.LastName = model.LastName;
                curuser.DateOfBirth = model.DateOfBirth;
                
                SessionWrapper.CurrentUser = curuser;
                return RedirectToAction("Index", "CardApplication");
            }

            ModelState.AddModelError("", "The user information provided is incorrect or not existing");
            return View(model);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginCardholder(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.UserPageAccesses = null;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginType(int UserType , string returnUrl)
        {

            if (UserType == 1){
                return RedirectToAction("LoginCardholder");
            }
            else if (UserType == 2)
            {
                retUrl = returnUrl;
                return RedirectToAction("LoginReferror");
            }
            else
            {
                //return View();
                return RedirectToAction("Index", "Home");
            }
        }
        
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            
            if (ModelState.IsValid && model.IsValid(model.UserName,model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName,true);

                return RedirectToAction("Index", "CardApplication");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }


        
        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Manage()
        {
            if (SessionWrapper.AllowedToChangePassword)
            {
                return View(SessionWrapper.CurrentUser);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Manage(User usr)
        {
            try
            {
                User userData = SessionWrapper.CurrentUser;
                if (usr.NewPassword != null || usr.NewPassword != "")
                {
                    userData.Password = usr.NewPassword;
                }
                userData.Email = usr.Email;
                userData.FirstName = usr.FirstName;
                userData.MiddleName = usr.MiddleName;
                userData.LastName = usr.LastName;
                userData.ModifiedBy = SessionWrapper.CurrentUser.UserName;
                UserManager.Update(userData);
                SessionWrapper.CurrentUser = userData;
                FormsAuthentication.SetAuthCookie(userData.UserName, true);
                SessionWrapper.UserType = UserType.Referror;


                var withAccess = SessionWrapper.PageAccess.Where(x => x.PageName.Equals("ApplicationStatus", StringComparison.InvariantCultureIgnoreCase))
                                              .Select(x => x.CanView)
                                              .FirstOrDefault();

                string nextUrl = string.Empty;
                if (withAccess)
                {
                    nextUrl = "ApplicationStatus";
                }
                else
                {
                    var firstPage = SessionWrapper.PageAccess.Where(x => x.CanView)
                                              .Select(x => x.PageName)
                                              .FirstOrDefault();

                    if (string.IsNullOrEmpty(firstPage))
                    {
                        ModelState.AddModelError("", "No access on any page.");
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        nextUrl = firstPage;
                    }
                   
                }
                return Json(new { url = Url.Action("Index", nextUrl) });

            }
            catch
            {
                return View();
            }
        }
       
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

       
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
//Test Branch

//Jacent
//Test Branch
//JL Test
