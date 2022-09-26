using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class PresenceManagerListener : MonoBehaviour
#else
    internal sealed class PresenceManagerListener
#endif
    {
        internal List<IPresenceManagerDelegate> delegater;

        internal void OnPresenceUpdated(string jsonString)
        {
            if (delegater != null)
            {
                List<Presence> list = TransformTool.JsonStringToPresenceList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IPresenceManagerDelegate delegater in delegater)
                    {
                        delegater.OnPresenceUpdated(list);
                    }
                });
            }
        }
    }
}
