using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{

    internal sealed class PresenceManagerListener : MonoBehaviour
    {
        internal List<IPresenceManagerDelegate> delegater;

        internal void OnPresenceUpdated(List<Presence> presences)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }
    }
}