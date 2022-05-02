using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Globalization;

namespace CRS.Helper
{
    public static class ObjectExtension
    {
        public static String ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static String ToJson(this object obj, Int32 recursiveDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursiveDepth;
            return serializer.Serialize(obj);
        }

        public static Object FromJson<T>(this String json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize(json, typeof(T));
        }

        /// <summary>
        /// Transform object into integer data type.
        /// </summary>
        /// <param name="item">The object to be transformed.</param>
        /// <param name="defaultId">Optional default value is default(int).</param>
        /// <returns>The integer value.</returns>
        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static Int16 AsInt16(this object item, Int16 defaultInt = default(Int16))
        {
            if (item == null)
                return defaultInt;

            Int16 result;
            if (!Int16.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static Int32 AsInt32(this object item, Int32 defaultInt = default(Int32))
        {
            if (item == null)
                return defaultInt;

            Int32 result;
            if (!Int32.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static Int64 AsInt64(this object item, Int64 defaultInt = default(Int64))
        {
            if (item == null)
                return defaultInt;

            Int64 result;
            if (!Int64.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static long AsLong(this object item, long defaultInt = default(long))
        {
            if (item == null)
                return defaultInt;

            long result;
            if (!long.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static short AsShort(this object item, short defaultInt = default(short))
        {
            if (item == null)
                return defaultInt;

            short result;
            if (!short.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static Decimal AsDecimal(this object item, Decimal defaultInt = default(Decimal))
        {
            if (item == null)
                return defaultInt;

            Decimal result;
            if (!Decimal.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static Double AsDouble(this object item, Double defaultDouble = default(Double))
        {
            if (item == null)
                return defaultDouble;

            Double result;
            if (!Double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }

        public static DateTime AsDateTime(this object item, DateTime defaultInt = default(DateTime))
        {
            if (item == null)
                return DateTime.Now;

            DateTime result;
            if (!DateTime.TryParse(item.ToString(), out result))
                return DateTime.Now;

            return result;
        }

        public static Boolean AsBoolean(this object item, Boolean defaultInt = default(Boolean))
        {
            if (item == null)
                return defaultInt;

            Boolean result;
            if (!Boolean.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }
        public static TimeSpan AsTimeSpan(this object item, TimeSpan defaultInt = default(TimeSpan))
        {
            if (item == null)
                return defaultInt;

            TimeSpan result;
            if (!TimeSpan.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        public static String StringToInt(this String item)
        {
            String result;

            if (String.IsNullOrWhiteSpace(item))
                result = "0";
            else
                result = item;

            return result;
        }

        public static String Translate12To24(this String entry)
        {
            String result = String.Empty;
            if (entry.Contains("PM"))
            {
                String hour = entry.Substring(0, entry.IndexOf(":"));
                int rawHourValue = Int32.Parse(hour);
                int hourValue = 12 + rawHourValue;
                String minute = entry.Substring(entry.IndexOf(":") + 1, 2);
                int minuteValue = int.Parse(minute);
                result = new TimeSpan(hourValue, minuteValue, 0).ToString();
            }
            else
            {
                DateTime t = DateTime.ParseExact(entry, "H:mm tt", CultureInfo.InvariantCulture);
                result = t.TimeOfDay.ToString();
            }

            return result;
        }
    }
}
