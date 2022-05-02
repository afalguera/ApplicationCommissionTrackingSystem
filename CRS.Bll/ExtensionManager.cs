using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.Bll
{
    public class ExtensionManager
    {
        #region Application Status List
        public static IEnumerable<Extension> GetList(string dateFrom,
                                                             string dateTo,
                                                             string sourceCode,
                                                             string applicationNo,
                                                             string fullname,
                                                             string extensionType,
                                                             bool isSummary,
                                                             bool isReferror,
                                                             string channelCode,                                           
                                                             string regionCode,
                                                             string areaCode,
                                                             string districtCode,
                                                             string branchCode,
                                                             string referrorCode,           
                                                             //string lastName,
                                                             //string firstName,
                                                             //string middleName,
                                                             //string dateOfBirth,
                                                             string referrorName,
                                                             string cardBrandCode,
                                                             string cardTypeCode
                                                        )
        {
            return ExtensionDB.GetExtensionsList(dateFrom,
                                                                dateTo,
                                                                sourceCode,
                                                                applicationNo,
                                                                fullname,
                                                                extensionType,
                                                                isSummary,
                                                                isReferror,
                                                                channelCode,
                                                                regionCode,
                                                                areaCode,
                                                                districtCode,
                                                                branchCode,
                                                                referrorCode,
                                                                //lastName,
                                                                //firstName,
                                                                //middleName,
                                                                //dateOfBirth,
                                                                referrorName,
                                                                cardBrandCode,
                                                                cardTypeCode
                                                                );
            //
        }
        #endregion
    }
}