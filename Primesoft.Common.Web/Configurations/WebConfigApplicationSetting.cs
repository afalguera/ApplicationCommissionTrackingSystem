using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primesoft.Common.Web.Configurations
{
    public static class WebConfigApplicationSetting
    {
        public static String ConnectionString { get { return ConfigurationManager.ConnectionStrings["CRSConn"] != null ? ConfigurationManager.ConnectionStrings["CRSConn"].ConnectionString.Replace("\\\\", "\\") : String.Empty; } }

        public static string LoggerName
        {
            get { return ConfigurationManager.AppSettings["LoggerName"] != null ? ConfigurationManager.AppSettings["LoggerName"].ToString() : String.Empty; }
        }

        public static Int16 AuthenticationExpirationDayCount
        {
            get
            {
                Int16 dayCount = 1;
                String setting = ConfigurationManager.AppSettings["AuthenticationExpirationDayCount"] != null ? ConfigurationManager.AppSettings["AuthenticationExpirationDayCount"].ToString() : "1";
                Int16.TryParse(setting, out dayCount);
                return dayCount;
            }
        }

        public static Boolean IsObjectCachingOn
        {
            get
            {
                Boolean result = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["IsObjectCachingOn"], out result);
                return result;
            }
        }

        public static String ClientScriptVersion
        {
            get
            {
                Boolean setting = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["IsDevelopment"], out setting);
                return setting ? Guid.NewGuid().ToString() : ConfigurationManager.AppSettings["ClientScriptVersion"] != null ? ConfigurationManager.AppSettings["ClientScriptVersion"].ToString() : "j@0";
            }
        }

        public static String CustomLoggingPath
        {
            get { return ConfigurationManager.AppSettings["CustomLoggingPath"] != null ? ConfigurationManager.AppSettings["CustomLoggingPath"].ToString() : @"C:\temp\logs"; }
        }
    }
}
