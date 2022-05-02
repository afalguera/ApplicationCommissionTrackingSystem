using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class RoleManager
    {
        public static RoleCollection GetList(string ChannelCode)
        {
            return RoleDB.GetList(ChannelCode);
        }

        public static RoleCollection GetList()
        {
            return RoleDB.GetList();
        }

        public static Role GetItem(int Id)
        {
            return RoleDB.GetItem(Id);
        }

        //public static int Delete(int id)
        //{
        //    return RoleDB.Delete(id);
        //}
        public static bool Delete(int roleId, string deletedBy)
        {
            return RoleDB.Delete(roleId, deletedBy);
        }


        public static int Save(Role role)
        {
            return RoleDB.Save(role);

        }

        public static bool IsRoleExists(string roleName)
        {
            return RoleDB.IsRoleExists(roleName);
        }

        #region Role List
        public static IEnumerable<Role> GetRoleList()
        {
            return RoleDB.GetRoleList();
        }
        #endregion

    }
}