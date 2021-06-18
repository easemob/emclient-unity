using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {
    public class ChatManagerListener : MonoBehaviour
    {
        internal WeakDelegater<IChatManagerDelegate> delegater;

        internal void OnMessageReceived(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMessagesReceived(list);
                }
            }
        }

        internal void OnCmdMessageReceived(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnCmdMessagesReceived(list);
                }
            }
        }

        internal void OnMessageRead(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMessagesRead(list);
                }
            }
        }

        internal void OnGroupMessageRead(string jsonString)
        {
            if (delegater != null)
            {
                List<GroupReadAck> list = TransformTool.JsonStringToGroupReadAckList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnGroupMessageRead(list);
                }
            }
        }

        internal void OnReadAckForGroupMessageUpdated()
        {
            if (delegater != null)
            {
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnReadAckForGroupMessageUpdated();
                }
            }
        }

        internal void OnMessageDelivered(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMessagesDelivered(list);
                }
            }
        }

        internal void OnMessageRecalled(string jsonString)
        {
            if (delegater != null)
            {
                List<Message> list = TransformTool.JsonStringToMessageList(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnMessagesRecalled(list);
                }
            }
        }

        internal void OnCoversationUpdate()
        {
            if (delegater != null)
            {
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnConversationsUpdate();
                }
            }
        }

        internal void OnConversationRead(string jsonString)
        {
            if (delegater != null)
            {
                Dictionary<string, string> dict = TransformTool.JsonStringToDictionary(jsonString);
                foreach (IChatManagerDelegate delegater in delegater.List)
                {
                    delegater.OnConversationRead(dict["from"], dict["to"]);
                }
            }
        }
    }
}