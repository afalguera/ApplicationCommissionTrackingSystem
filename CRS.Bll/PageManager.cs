using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class PageManager
    {
      

        public static PageCollection GetList(int roleid)
        {
            return PageDB.GetList(roleid);
        }

        public static RolePageAccess GetItem(int Id)
        {
            return RolePageAccessDB.GetItem(Id);
        }

        public static int Delete(int id)
        {
            return RolePageAccessDB.Delete(id);
        }

        public static int Save(RolePageAccess rolepageacc)
        {
            return RolePageAccessDB.Save(rolepageacc);

        }
    }
}