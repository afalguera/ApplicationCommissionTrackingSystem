using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class UserPageAccessManager
    {
        public static UserPageAccessCollection GetList(string userName)
        {
            return UserPageAccessDB.GetList(userName);
        }
    }
}