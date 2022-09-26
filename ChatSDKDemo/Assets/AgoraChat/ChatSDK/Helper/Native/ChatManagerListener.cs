using System;
using System.Collections.Generic;
using SimpleJSON;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
    internal sealed class ChatManagerListener : MonoBehaviour
#else
    internal sealed class ChatManagerListener
#endif
    {
        internal List<IChatManagerDelegate> delegater;

        internal void OnMessageReceived(string jsonString)
        {

            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnMessagesReceived(list);
                    }
                });
            }
        }

        internal void OnCmdMessageReceived(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnCmdMessagesReceived(list);
                    }
                });
            }
        }

        internal void OnMessageRead(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnMessagesRead(list);
                    }
                });
            }
        }

        internal void OnGroupMessageRead(string jsonString)
        {
            if (delegater != null)
            {
                List<GroupReadAck> list = TransformTool.JsonStringToGroupReadAckList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnGroupMessageRead(list);
                    }
                });
            }
        }

        internal void OnReadAckForGroupMessageUpdated(string jsonString)
        {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnReadAckForGroupMessageUpdated();
                    }
                });
            }
        }

        internal void OnMessageDelivered(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnMessagesDelivered(list);
                    }
                });
            }
        }

        internal void OnMessageRecalled(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnMessagesRecalled(list);
                    }
                });
            }
        }
        
        internal void OnConversationUpdate(string str)
        {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnConversationsUpdate();
                    }
                });
            }
        }

        internal void OnConversationRead(string jsonString)
        {
            if (delegater != null)
            {
                Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.OnConversationRead(dict["from"], dict["to"]);
                    }
                });
            }
        }

        internal void MessageReactionDidChange(string jsonString) {
            if (delegater != null) {
                List<MessageReactionChange> list = MessageReactionChange.ListFromJson(jsonString);
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IChatManagerDelegate delegater in delegater)
                    {
                        delegater.MessageReactionDidChange(list);
                    }
                });
            }
        }
    }
}