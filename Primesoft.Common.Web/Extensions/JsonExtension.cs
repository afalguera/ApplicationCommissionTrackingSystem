using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace Primesoft.Common.Web.Extensions
{
    public static class JsonExtension
    {
        public static XmlDocument JsonAsXml(this String json)
        {
            return (XmlDocument)JsonConvert.DeserializeXmlNode("{\"root\":" + json + "}", "root");
        }

        public static XmlDocument AttributeJsonAsXml(this String json)
        {
            return (XmlDocument)JsonConvert.DeserializeXmlNode("{\"Attribute\":" + json + "}", "AttributeConfiguration");
        }

        public static String XmlAsJson(this String xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(doc);
        }

        public static Object FromJson<T>(this String json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize(json, typeof(T));
        }

        public static List<SqlParameter> JsonToSqlParameterList(this String json)
        {
            List<SqlParameter> result = new List<SqlParameter>();

            if (!String.IsNullOrEmpty(json))
            {
                json = json.Replace("}", String.Empty).Replace("{", String.Empty);

                String[] fieldList = json.Split(',');

                for (Int32 index = 0; index < fieldList.Length; index++)
                {

                    String[] field = fieldList[index].Split(':');
                    SqlParameter param = new SqlParameter("@" + field[0].Replace("\"", ""), field[1]);

                    result.Add(param);
                }
            }

            return result;
        }

        public static List<SqlParameter> JsonToSqlParameterList(this String json, List<SqlParameter> list)
        {
            if (list == null)
            {
                return JsonToSqlParameterList(json);
            }

            json = json.Replace("}", "").Replace("{", "");

            String[] fieldList = json.Split(',');

            for (Int32 index = 0; index < fieldList.Length; index++)
            {

                String[] field = fieldList[index].Split(':');
                SqlParameter param = new SqlParameter("@" + field[0], field[1]);

                list.Add(param);
            }
            return list;
        }

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
    }
}
