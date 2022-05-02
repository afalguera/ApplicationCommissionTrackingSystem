using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
    
{
    public class UserManager
    {

        public static int GetUserIdFromEmail(string email)
        {
            return UserManagerDB.GetUserIdFromEmail(email);
        }

        public static User GetItem(string username, string password)
        {
            return UserManagerDB.GetItem(username, password);
        }

        public static User GetItem(string username)
        {
            return UserManagerDB.GetItem(username);
        }

        public static User GetItem(int id)
        {
            return UserManagerDB.GetItem(id);
        }
        
        public static IEnumerable<User> GetPendingRegistrations()
        {
            return UserManagerDB.GetPendingRegistrations();
        
        }

        public static IEnumerable<User> GetUserList()
        {
            return UserManagerDB.GetList();
        }

        public static int Update(User usr)
        {
            return UserManagerDB.Update(usr);

        }

        public static int Save(User usr)
        {
            return UserManagerDB.Save(usr);

        }

        public static int ChangePassword(int id, string NewPassword)
        {
            return UserManagerDB.ChangePassword(id, NewPassword);

        }

        public static int ApproveUser(User usr)
        {
            return UserManagerDB.ApproveUser(usr);

        }

        public static bool Delete(int userId, string deletedBy)
        {
            return UserManagerDB.Delete(userId, deletedBy);
        }

        public static bool IsUserExists(string userName)
        {
            return UserManagerDB.IsUserExists(userName);
        } 

    }
}