using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRS.Bll;
using CRS.BusinessEntities;


namespace CRS.Controllers
{
    
    public class DataToReceive
    {
        public string ID { get; set; }
        public string UserID { get; set; }
    }
     

    public class CancelPoints
    {
        public string ID { get; set; }
        public string CancelUserID { get; set; }
    }
    
    public class RedeemedCntObj
    {
        public string UserID { get; set; }
    }
    
    
    public class RedeemPointsAPIController : ApiController
    {
        // GET api/redeemppointsapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/redeemppointsapi/5
        public string Get(int Id)
        {
            RedeemedItemsCollection redeemeditems = RedeemedItemsManager.GetList(Id);
            return redeemeditems.Count().ToString();
        }
        
        
        [HttpPost]
        [ActionName("save")]
        public void Post([FromBody]DataToReceive datatoreceive)
        {
            RedeemedItems items = new RedeemedItems();
            items.RedemptionItemsID = Convert.ToInt16(datatoreceive.ID);
            items.UserID = Convert.ToInt16(datatoreceive.UserID);
            int result = RedeemedItemsManager.Save(items);
        }


        [HttpPost]
        [ActionName("cancel")]
        public void Post([FromBody]CancelPoints cancelpoints)
        {
            int result = RedeemedItemsManager.Cancel(Convert.ToInt16(cancelpoints.CancelUserID));
        }
        
        
        // PUT api/redeemppointsapi/5
        public void Put(string title)
        {
            //System.Diagnostics.Debug.Assert(false);
        }

        // DELETE api/redeemppointsapi/5
        public void Delete(int id)
        {
        }
    }
}
