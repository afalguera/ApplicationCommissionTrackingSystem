using CRS.BusinessEntities;
using CRS.Dal;
using System.Collections.Generic;

namespace CRS.Bll
{
    public class ChannelDetailsManager
    {
        public static ChannelDetails GetItem(int channelDetailsId)
        {
            return ChannelDetailsDB.GetItem(channelDetailsId);
        }

        public static IEnumerable<ChannelDetails> GetList()
        {
            return ChannelDetailsDB.GetList();
        }

        public static bool Save(ChannelDetails channelDetails)
        {
            return ChannelDetailsDB.Save(channelDetails);
        }

        public static bool Update(ChannelDetails channelDetails)
        {
            return ChannelDetailsDB.Update(channelDetails);
        }

        public static bool Delete(int channelDetailsId, string deletedBy)
        {
            return ChannelDetailsDB.Delete(channelDetailsId, deletedBy);
        }
    }
}