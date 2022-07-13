using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
    internal sealed class PresenceManagerListener : MonoBehaviour
#else
    internal sealed class PresenceManagerListener
#endif
    {
        internal List<IPresenceManagerDelegate> delegater;

        internal void OnPresenceUpdated(List<Presence> presences)
        {
            //TODO: Add code for processing json string from IOS/Android SDK
        }
    }
}
