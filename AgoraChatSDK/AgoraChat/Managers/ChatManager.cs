using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    public class ChatManager
    {
        CallbackManager callbackManager;
        internal List<IChatManagerDelegate> delegater;

        internal string NAME_CHATMANAGER = "ChatManager";

        Object msgMapLocker;
        Dictionary<string, Message> msgMap;

        internal ChatManager(NativeListener listener)
        {
            callbackManager = listener.callbackManager;
            listener.chatManagerEvent += NativeEventHandle;

            delegater = new List<IChatManagerDelegate>();

            msgMapLocker = new Object();
            msgMap = new Dictionary<string, Message>();
        }

        private void AddMsgMap(string cbid, Message msg)
        {
            lock (msgMapLocker)
            {
                msgMap.Add(cbid, msg);
            }
        }

        private void UpdatedMsg(string cbid, string json)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    var msg = msgMap[cbid];
                    if (json.Length > 0)
                    {
                        JSONNode jn = JSON.Parse(json);
                        if (null != jn && jn.IsObject)
                        {
                            msg.FromJsonObject(jn.AsObject);
                        }
                    }
                }
            }
        }

        private void DeleteFromMsgMap(string cbid)
        {
            lock (msgMapLocker)
            {
                if (msgMap.ContainsKey(cbid))
                {
                    msgMap.Remove(cbid);
                }
            }
        }

        /**
		 * \~chinese
		 * 发送消息。
		 *
		 * 异步方法。
		 *
		 * 对于语音、图片等带有附件的消息，SDK 在默认情况下会自动上传附件。请参见 {@link Options#ServerTransfer}。
		 *
		 * @param message   要发送的消息对象，必填。
		 * @param handle	发送结果回调，详见 {@link CallBack}。
		 *
		 * \~english
		 * Sends a message。
		 *
		 * This is an asynchronous method.
		 *
		 * For attachment messages such as voice or image, the SDK will automatically upload the attachment by default. See {@link Options#ServerTransfer}.
		 *
		 *
		 * @param msg		 The message object to be sent. Ensure that you set this parameter. 
		 * @param handle	 The result callback. See {@link CallBack}.
		 */
        public void SendMessage(ref Message message, CallBack callback = null)
        {
            Process process = (_cbid, _json) =>
            {
                UpdatedMsg(_cbid, _json);
                DeleteFromMsgMap(_cbid);
                return null;
            };

            callbackManager.AddCallbackAction<Object>(callback, process);

            string cbid = (null != callback) ? callback.callbackId : message.MsgId;

            AddMsgMap(cbid, message);

            JSONObject jo_param = message.ToJsonObject();

            string json = CWrapperNative.NativeGet(NAME_CHATMANAGER, "sendMessage", jo_param, (null != callback) ? callback.callbackId : "");

            if(json.Length > 0)
            {
                UpdatedMsg(cbid, json);
            }
        }

        /**
		 * \~chinese
		 * 注册聊天管理器的监听器。
		 *
		 * @param chatManagerDelegate 	要注册的聊天管理器的监听器，继承自 {@link IChatManagerDelegate}。
		 *
		 * \~english
		 * Adds a chat manager listener.
		 *
		 * @param chatManagerDelegate 	The chat manager listener to add. It is inherited from {@link IChatManagerDelegate}.
		 *
		 */
        public void AddChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            if (!delegater.Contains(chatManagerDelegate))
            {
                delegater.Add(chatManagerDelegate);
            }
        }

        /**
		 * 移除聊天管理器的监听器。
		 *
		 * @param chatManagerDelegate 	要移除的聊天管理器的监听器，继承自 {@link IChatManagerDelegate}。
		 *
		 * \~english
		 * Removes a chat manager listener.
		 *
		 * @param chatManagerDelegate 	The chat manager listener to remove. It is inherited from {@link IChatManagerDelegate}.
		 *
		 */
        public void RemoveChatManagerDelegate(IChatManagerDelegate chatManagerDelegate)
        {
            if (delegater.Contains(chatManagerDelegate))
            {
                delegater.Remove(chatManagerDelegate);
            }
        }

        internal void NativeEventHandle(string method, string jsonString)
        {
            if (delegater.Count == 0 || null == method || method.Length == 0) return;

            if (method.CompareTo("OnMessagesReceived") == 0)
            {
                List<Message> list = List.MessageListFromJson(jsonString);
                if(list.Count > 0)
                {
                    foreach (IChatManagerDelegate it in delegater)
                    {
                        it.OnMessagesReceived(list);
                    }
                }
            }
            else if (method.CompareTo("OnCmdMessagesReceived") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnMessagesRead") == 0)
            {
                List<Message> list = List.MessageListFromJson(jsonString);
                if (list.Count > 0)
                {
                    foreach (IChatManagerDelegate it in delegater)
                    {
                        it.OnMessagesRead(list);
                    }
                }
            }
            else if (method.CompareTo("OnMessagesDelivered") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnMessagesRecalled") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnReadAckForGroupMessageUpdated") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnGroupMessageRead") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnConversationsUpdate") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("OnConversationRead") == 0)
            {
                //TODO
            }
            else if (method.CompareTo("MessageReactionDidChange") == 0)
            {
                //TODO
            }
        }
    }
}