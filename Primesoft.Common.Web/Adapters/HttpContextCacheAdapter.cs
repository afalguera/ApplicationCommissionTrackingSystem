using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using Primesoft.Common.Contracts;
//using Primesoft.Common.Web.Configurations;

namespace Primesoft.Common.Web
{
    //public class HttpContextCacheAdapter : ICacheStorable
    //{
    //    #region ICacheStorable Members

    //    public void Remove(string key)
    //    {
    //        HttpContext.Current.Cache.Remove(key);
    //    }

    //    public void Store(string key, object data)
    //    {
    //        if (data != null)
    //        {
    //            HttpContext.Current.Cache.Insert(key, data);
    //        }
    //    }

    //    public T Retrieve<T>(string key)
    //    {
    //        if (WebConfigApplicationSetting.IsObjectCachingOn)
    //        {
    //            T storedItem = (T)HttpContext.Current.Cache.Get(key);
    //            if (storedItem == null)
    //            {
    //                storedItem = default(T);
    //            }
    //            return storedItem;
    //        }
    //        else
    //        {
    //            return default(T);
    //        }
    //    }

    //    #endregion
    //}
}
