using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    public class ChatManagerListener : MonoBehaviour
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

        internal void OnReadAckForGroupMessageUpdated()
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
    }
}