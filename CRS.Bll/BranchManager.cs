using CRS.BusinessEntities;
using CRS.Dal;
using System.Collections.Generic;

namespace CRS.Bll
{
    public class BranchManager
    {
        public static bool BranchCodeExists(string branchCode)
        {
            return BranchDB.CheckIfExists(branchCode);
        }

        public static object GetCascadingLocation(int branchId)
        {
            return BranchDB.GetCascadingLocation(branchId);
        }

        public static IEnumerable<Branch> GetList()
        {
            return BranchDB.GetList();
        }

        public static bool Save(Branch branch)
        {
            return BranchDB.Save(branch);
        }

        public static bool Update(Branch branch)
        {
            return BranchDB.Update(branch);
        }

        public static bool Delete(int branchId, string deletedBy)
        {
            return BranchDB.Delete(branchId, deletedBy);
        }

        public static bool IsBranchExists(string channelCode, string branchCode, string branchName)
        {
            return BranchDB.IsBranchExists(channelCode, branchCode, branchName);
        } 
    }
}