using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace CRS.Bll
{
    public class CommissionDashboardManager
    {
        #region Channel List
        public static IEnumerable<EAPRChannel> GetChannelList(string channelCode)
        {
            List<EAPRChannel> list = new List<EAPRChannel>();
            list = CommissionDashboardDB.GetChannelList(channelCode)
                    .Where(x=> x.IsEAPR == true)
                    .ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region Channel List (All)
        public static IEnumerable<EAPRChannel> GetChannelAllList(string channelCode)
        {
            List<EAPRChannel> list = new List<EAPRChannel>();
            list = CommissionDashboardDB.GetChannelList(channelCode)
                  .ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region Region List
        public static IEnumerable<EAPREntityPair> GetRegionList(string regionCode)
        {
            List<EAPREntityPair> list = new List<EAPREntityPair>();
            list = CommissionDashboardDB.GetRegionList(regionCode).ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region Area List
        public static IEnumerable<EAPREntityPair> GetAreaList(string regionCode, string areaCode)
        {
            List<EAPREntityPair> list = new List<EAPREntityPair>();
            list = CommissionDashboardDB.GetAreaList(regionCode, areaCode).ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region District List
        public static IEnumerable<EAPRDistrict> GetDistrictList(string channelCode, string regionCode, string districtCode)
        {
            List<EAPRDistrict> list = new List<EAPRDistrict>();
            list = CommissionDashboardDB.GetDistrictList(channelCode, regionCode, districtCode).ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region Branch List
        public static IEnumerable<EAPREntityPair> GetBranchList(string channelCode, string districtCode, string branchCode)
        {
            List<EAPREntityPair> list = new List<EAPREntityPair>();
            list = CommissionDashboardDB.GetBranchList(channelCode, districtCode, branchCode).ToList();
            return list.AsEnumerable();
        }
        #endregion

        #region Get CommissionDashboard Results
        public static IEnumerable<CommissionDashboard> GetList(string dateFrom,
                                                            string dateTo,
                                                            bool isSummary,
                                                            string channelCode,
                                                            string regionCode,
                                                            string areaCode,
                                                            string districtCode,
                                                            string branchCode,
                                                            string referrorCode,
                                                            string keyword                                         
                                                       )
        {
           
            return CommissionDashboardDB.GetCommissionDashboardList(dateFrom,
                                                            dateTo,
                                                            isSummary,
                                                            channelCode,
                                                            regionCode,
                                                            areaCode,
                                                            districtCode,
                                                            branchCode,
                                                            referrorCode,
                                                            keyword
                                                       );
        } 
        #endregion     
    }
}