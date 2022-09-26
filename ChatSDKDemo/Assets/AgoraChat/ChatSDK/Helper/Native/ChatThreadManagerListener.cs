using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ChatThreadManagerListener : MonoBehaviour
#else
    internal sealed class ChatThreadManagerListener
#endif
    {
        internal List<IChatThreadManagerDelegate> delegater;
        internal void OnChatThreadCreate(string jsonString) {
            if (delegater != null)
            {
                ChatThreadEvent chatThreadEvent = ChatThreadEvent.FromJson(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatThreadManagerDelegate delegater in delegater)
                    {
                        delegater.OnChatThreadCreate(chatThreadEvent);
                    }
                });
            }
        }

        internal void OnChatThreadUpdate(string jsonString) {
            if (delegater != null)
            {
                ChatThreadEvent chatThreadEvent = ChatThreadEvent.FromJson(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatThreadManagerDelegate delegater in delegater)
                    {
                        delegater.OnChatThreadUpdate(chatThreadEvent);
                    }
                });
            }
        }

        internal void OnChatThreadDestroy(string jsonString) {
            if (delegater != null)
            {
                ChatThreadEvent chatThreadEvent = ChatThreadEvent.FromJson(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatThreadManagerDelegate delegater in delegater)
                    {
                        delegater.OnChatThreadDestroy(chatThreadEvent);
                    }
                });
            }
        }

        internal void OnUserKickOutOfChatThread(string jsonString) {
            if (delegater != null)
            {
                ChatThreadEvent chatThreadEvent = ChatThreadEvent.FromJson(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatThreadManagerDelegate delegater in delegater)
                    {
                        delegater.OnUserKickOutOfChatThread(chatThreadEvent);
                    }
                });
            }
        }
    }
}