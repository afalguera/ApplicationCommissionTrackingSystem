using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace MvcApplication1
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //AreaRegistration.RegisterAllAreas();

        }

        protected void Session_OnEnd(object sender, EventArgs e)
        {
            //Response.Redirect("~/Account/Login/SessionTimeout");
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    if (Request.Cookies["Ashely"] != null)
            //    {
            //        var c = new HttpCookie("Ashely");
            //        c.Expires = DateTime.Now.AddDays(-1);
            //        Response.Cookies.Add(c);
            //    }
            //}
        }

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("elmah.axd");
        //}
    }
}