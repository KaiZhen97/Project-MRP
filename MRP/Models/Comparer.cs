using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MRP.Database;

namespace MRP.Models
{
    public class PRListWatcherComparer : IEqualityComparer<V_PRList_Watcher>
    {
        public bool Equals(V_PRList_Watcher x, V_PRList_Watcher y)
        {
            if (x.PRID != y.PRID)
                return false;

            return true;
        }

        public int GetHashCode(V_PRList_Watcher obj)
        {
            return obj.PRID.GetHashCode() ^ obj.PRItemID.GetHashCode() ^ obj.Watchers_AccessID.GetHashCode();
        }
    }

    public class V_RFQListComparer : IEqualityComparer<V_RFQList>
    {
        public bool Equals(V_RFQList x, V_RFQList y)
        {
            if (x.ID != y.ID)
                return false;

            return true;
        }

        public int GetHashCode(V_RFQList obj)
        {
            return obj.ID.GetHashCode() ^ obj.Watchers_AccessID.GetHashCode();
        }
    }
}