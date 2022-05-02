using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Primesoft.Common.Web.Extensions
{
    public static class HttpExtension
    {
        public static String ResolveUrl(String resource)
        {
            HttpRequest request = HttpContext.Current.Request;
            return String.Format("{0}://{1}{2}{3}",
                request.Url.Scheme,
                request.ServerVariables["HTTP_POST"],
                request.ApplicationPath.Equals("/") ? String.Empty : request.ApplicationPath,
                resource);
        }
    }
}
