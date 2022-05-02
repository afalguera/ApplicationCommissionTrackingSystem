using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Bll;
using CRS.Helpers;
using System.Web.SessionState;
using System.IO;
using System.Reflection;


namespace CRS.Helpers
{
    
    public static class SessionWrapper
    {
        private static HttpContext fakecontext
        {
            get
            {

                var httpRequest = new HttpRequest("", "http://mySomething/", "");
                var stringWriter = new StringWriter();
                var httpResponce = new HttpResponse(stringWriter);
                var httpContext = new HttpContext(httpRequest, httpResponce);

                var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                                     new HttpStaticObjectsCollection(), 10, true,
                                                                     HttpCookieMode.AutoDetect,
                                                                     SessionStateMode.InProc, false);

                httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                                         BindingFlags.NonPublic | BindingFlags.Instance,
                                                         null, CallingConventions.Standard,
                                                         new[] { typeof(HttpSessionStateContainer) },
                                                         null)
                                                    .Invoke(new object[] { sessionContainer });

                return httpContext;
            }
        }

        public static IEnumerable<CRS.BusinessEntities.Module> Modules
        {
            get
            {
                if (null != HttpContext.Current.Session["Modules"])
                    return HttpContext.Current.Session["Modules"] as IEnumerable<CRS.BusinessEntities.Module>;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Modules"] = value;
            }
        }

        public static UserPageAccessCollection PageAccess
        {
            get
            {
                if (null != HttpContext.Current.Session["PageAccess"])
                    return HttpContext.Current.Session["PageAccess"] as UserPageAccessCollection;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["PageAccess"] = value;
            }
        }
        
        public static UserType? UserType
        {
            get
            {
                if (null != HttpContext.Current.Session["UserType"])
                    return HttpContext.Current.Session["UserType"] as UserType?;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["UserType"] = value;
            }
        }

        public static CardApplication Applicant
        {
            get
            {
                if (null != HttpContext.Current.Session["Applicant"])
                    return HttpContext.Current.Session["Applicant"] as CardApplication;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["Applicant"] = value;
            }
        }

        public static Boolean AllowedToChangePassword
        {
            get
            {
                return Convert.ToBoolean(HttpContext.Current.Session["AllowedToChangePassword"] ?? false);
            }
            set
            {
                HttpContext.Current.Session["AllowedToChangePassword"] = value;
            }
        }
        
        public static User CurrentUser
        {
            get
            {
                if (null != HttpContext.Current.Session["CurrentUser"])
                    return HttpContext.Current.Session["CurrentUser"] as User;
                else
                    return null;
            }
            set
            {
                if (HttpContext.Current == null)
                {
                    HttpContext.Current = fakecontext;
                }
                
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
        public static string LoginFirstName
        {
            get
            {
                if (null != HttpContext.Current.Session["LoginFirstName"])
                    return HttpContext.Current.Session["LoginFirstName"] as string;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["LoginFirstName"] = value;
            }
        }

        public static string LoginMiddleName
        {
            get
            {
                if (null != HttpContext.Current.Session["LoginMiddleName"])
                    return HttpContext.Current.Session["LoginMiddleName"] as string;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["LoginMiddleName"] = value;
            }
        }

        public static int? RoleId
        {
            get
            {
                if (null != HttpContext.Current.Session["RoleId"])
                    return HttpContext.Current.Session["RoleId"] as int?;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["RoleId"] = value;
            }
        }
        
        public static CardApplication CurrentCardHolder
        {
            get
            {
                if (null != HttpContext.Current.Session["CurrentCardHolder"])
                    return HttpContext.Current.Session["CurrentCardHolder"] as CardApplication;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["CurrentCardHolder"] = value;
            }
        }

        public static CMSBannerAdsImageCollection BannerAdsCollection
        {
            get
            {
                if (null != HttpContext.Current.Session["BannerAdsCollection"])
                    return HttpContext.Current.Session["BannerAdsCollection"] as CMSBannerAdsImageCollection;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["BannerAdsCollection"] = value;
            }
        }
    
        
    
    }


    public enum UserType
    { Anonymous = 0, Cardholder = 1, Referror = 2 } 
}