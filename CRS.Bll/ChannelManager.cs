using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRS.BusinessEntities;
using CRS.Dal;

namespace CRS.Bll
{
    public class ChannelManager
    {
        public static bool ChannelCodeExists(string channelCode)
        {
            return ChannelDB.CheckIfExists(channelCode, ChannelDB.ChannelFilters.channelCode);
        }

        public static bool ChannelNameExists(string channelName)
        {
            return ChannelDB.CheckIfExists(channelName, ChannelDB.ChannelFilters.channelName);
        }

        public static Channel GetItem(int channelId)
        {
            return ChannelDB.GetItem(channelId);
        }

        public static IEnumerable<Channel> GetList()
        {
            return ChannelDB.GetList();
        }

        public static IEnumerable<EAPREntityPair> GetPositionDetailsList()
        {
            return ChannelDB.GetPositionDetailsList();
        }

        public static bool Save(Channel channel)
        {
            return ChannelDB.Save(channel);
        }

        public static bool Update(Channel channel)
        {
            return ChannelDB.Update(channel);
        }

        public static bool Delete(int channelId, string deletedBy)
        {
            return ChannelDB.Delete(channelId, deletedBy);
        }
    }
}