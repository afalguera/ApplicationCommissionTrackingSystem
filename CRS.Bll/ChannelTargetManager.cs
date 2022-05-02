using CRS.BusinessEntities;
using CRS.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CRS.Bll
{
    public class ChannelTargetManager
    {
        public static IEnumerable<ChannelTarget> GetList()
        {
            return ChannelTargetDB.GetList();
        }

        public static bool CheckIfExists(string channelCode, string year)
        {
            return ChannelTargetDB.CheckIfExists(channelCode, year);
        }

        public static bool Save(ChannelTarget channelTarget)
        {
            return ChannelTargetDB.Save(channelTarget);
        }

        public static bool Update(ChannelTarget channelTarget)
        {
            return ChannelTargetDB.Update(channelTarget);
        }

        public static bool Delete(int channelTargetId, string deletedBy)
        {
            return ChannelTargetDB.Delete(channelTargetId, deletedBy);
        }
    }
}