

using System.Collections.Generic;

namespace ChatSDK
{
    public interface IPresenceManagerDelegate
    {
        void OnPresenceUpdated(List<Presence> presences);
    }
}