using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Primesoft.Common;
using Primesoft.Common.Web.Configurations;
using Primesoft.Common.Web.Extensions;

namespace Primesoft.Common.Web.Securities
{
    public static class WebAuthObject
    {
        static HttpCookie cookie = null;

        public static Boolean IsAuthenticated
        {
            get
            {
                cookie = HttpContext.Current.Request.Cookies[Constant.AUTHENTICATION_KEY];
                return cookie != null;
            }
        }

        public static AuthenticationTicket AuthenticatedUser
        {
            get
            {
                AuthenticationTicket result = null;
                if (IsAuthenticated)
                {
                    if (!String.IsNullOrEmpty(cookie.Value))
                    {
                        result = (AuthenticationTicket)cookie.Value.FromJson<AuthenticationTicket>();
                    }
                }
                return result;
            }
        }

        public static void RemoveAuthenticationCookie()
        {
            if (IsAuthenticated)
            {
                cookie.Expires = DateTime.Now.AddDays(WebConfigApplicationSetting.AuthenticationExpirationDayCount);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
