using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.Helpers
{
    
    public class ACTSAuthorizeAttribute : AuthorizeAttribute
    {
        private Boolean _isApplicantAllowed = false;
        private String _controllerName = String.Empty;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            _controllerName = filterContext.RouteData.Values["controller"].ToString();
            
            _isApplicantAllowed = filterContext.ActionDescriptor.IsDefined(typeof(AllowApplicantAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowApplicantAttribute), true);

            base.OnAuthorization(filterContext);
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Generally authenticated to the site
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            if (_isApplicantAllowed)
            {
                if(SessionWrapper.UserType.Equals(UserType.Cardholder))
                    return true;
            }

            if (SessionWrapper.PageAccess != null && SessionWrapper.PageAccess.Count > 0)
                return SessionWrapper.PageAccess.Any<CRS.BusinessEntities.UserPageAccess>(m => m.PageName == _controllerName);

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}