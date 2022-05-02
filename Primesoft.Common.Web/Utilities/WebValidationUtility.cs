using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Primesoft.Common.Web
{
    public static class WebValidationUtility
    {
        public static Boolean ValidateEmailAddress(String emailAddress)
        {
            Boolean result = false;
            Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (regex.IsMatch(emailAddress))
            {
                result = true;
            }
            return result;
        }
    }
}
