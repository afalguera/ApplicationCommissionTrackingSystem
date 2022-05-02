using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRS.Controllers;
using System.Web;
using System.Web.Mvc;
using CRS;
using System.Security.Principal;
using CRS.BusinessEntities;
using CRS.Helpers;

using System.IO;




namespace CRS.UnitTest
{
    [TestClass]
    public class CMSBannerAdsImageControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            User usr = new User { UserName = "Ashely", Role = 23 };
            SessionWrapper.CurrentUser = usr;
            
            CMSBannerAdsImageController controller = new CMSBannerAdsImageController();
            
            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            UserPageAccessCollection col = result.ViewBag.UserPageAccesses as UserPageAccessCollection;
            Assert.IsTrue(col.Count  > 0, "Total Page Access " + col.Count);
        }
    }
}
