using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class RolePageAccessManager
    {
      

        public static RolePageAccessCollection GetList(int roleid)
        {
            return RolePageAccessDB.GetList(roleid);
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