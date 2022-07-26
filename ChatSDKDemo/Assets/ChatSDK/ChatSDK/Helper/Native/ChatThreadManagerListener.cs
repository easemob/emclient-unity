using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ChatThreadManagerListener : MonoBehaviour
#else
    internal sealed class ChatThreadManagerListener
#endif
    {
        internal List<IChatThreadManagerDelegate> delegater;
        internal void OnChatThreadCreate(ChatThreadEvent threadEvent) { }
        internal void OnChatThreadUpdate(ChatThreadEvent threadEvent) { }
        internal void OnChatThreadDestroy(ChatThreadEvent threadEvent) { }
        internal void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent) { }
    }
}