using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll   
{
    public class BranchManagerManager
    {
        public static BranchManagerCollection GetList()
        {
            return BranchManagerDB.GetList();
        }
    }
}