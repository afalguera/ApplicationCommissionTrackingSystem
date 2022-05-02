using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace MvcApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
           // routes.IgnoreRoute("{resource}.audit");
           // routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           // routes.MapRoute(
           //    name: "FailWhale",
           //    url: "FailWhale/{action}/{id}",
           //    defaults: new
           //    {
           //        controller = "Error",
           //        action = "FailWhale",
           //        id = UrlParameter.Optional
           //    }
           //);

            /*
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
             */
            /*
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
             */

            //routes.MapRoute(
            //      "Admin_elmah",
            //      "Admin/elmah/{type}",
            //           new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
            //);

            
           

            /*
            routes.MapRoute(
                "ReportViewer",
                "Reports/ReportViewer.aspx/{type}",
                new { Action="Index", controller="ProductivityReport", type = UrlParameter.Optional }
                
            );*/
             

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "JSONResults",
            //    url: "JSONResult/{controller}/{action}/{id}"
            //);            
        }
    }
}