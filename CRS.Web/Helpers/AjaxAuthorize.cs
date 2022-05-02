using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.Helpers
{
    public class AjaxAuthorize: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result == null || (filterContext.Result.GetType() != typeof(HttpUnauthorizedResult)
                || !filterContext.HttpContext.Request.IsAjaxRequest()))
                return;

            //var redirectToUrl = "Security/LogOn?returnUrl=" + filterContext.HttpContext.Request.UrlReferrer.PathAndQuery;
            var redirectToUrl = "/";
            filterContext.Result = new JavaScriptResult() { Script = "window.location = '" + redirectToUrl + "'" };

        }
    }
}