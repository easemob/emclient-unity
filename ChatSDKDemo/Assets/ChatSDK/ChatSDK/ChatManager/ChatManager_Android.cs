using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ChatSDK
{
    internal sealed class ChatManager_Android : IChatManager
    {

        private AndroidJavaObject wrapper;

        public ChatManager_Android()
        {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMChatManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override bool DeleteConversation(string conversationId, bool deleteMessages)
        {
            return wrapper.Call<bool>("deleteConversation", conversationId, deleteMessages);
        }

        public override void DownloadAttachment(string messageId, CallBack handle = null)
        {
            wrapper.Call("downloadAttachment", messageId, handle?.callbackId);
        }

        public override void DownloadThumbnail(string messageId, CallBack handle = null)
        {
            wrapper.Call("downloadThumbnail", messageId, handle?.callbackId);
        }

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack < CursorResult<Message>> handle = null)
        {
            //TODO: need to add direction
            wrapper.Call("fetchHistoryMessages", conversationId, TransformTool.ConversationTypeToInt(type), startMessageId, count, handle?.callbackId);
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            string jsonString = wrapper.Call<string>("getConversation", conversationId, TransformTool.ConversationTypeToInt(type), createIfNeed);
            if (jsonString == null || jsonString.Length == 0) {
                return null;
            }

            return new Conversation(jsonString);
        }

        public override void GetConversationsFromServer(ValueCallBack<List<Conversation>> handle = null)
        {
            wrapper.Call("getConversationsFromServer", handle?.callbackId);
        }

        public override int GetUnreadMessageCount()
        {
            return wrapper.Call<int>("getUnreadMessageCount");
        }

        public override bool ImportMessages(List<Message> messages)
        {
            return wrapper.Call<bool>("importMessages", TransformTool.JsonObjectFromMessageList(messages).ToString());
        }

        public override List<Conversation> LoadAllConversations()
        {
            string jsonString = wrapper.Call<string>("loadAllConversations");
            return TransformTool.JsonStringToConversationList(jsonString);
        }

        public override Message LoadMessage(string messageId)
        {
            string jsonString = wrapper.Call<string>("loadMessage", messageId);
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return new Message(jsonString);
        }

        public override bool MarkAllConversationsAsRead()
        {
            return wrapper.Call<bool>("markAllConversationsAsRead");
        }

        public override void RecallMessage(string messageId, CallBack handle = null)
        {
            wrapper.Call("recallMessage", messageId, handle?.callbackId);
        }

        public override Message ResendMessage(string messageId, CallBack handle = null)
        {
            string jsonString = wrapper.Call<string>("resendMessage", messageId, handle?.callbackId);
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }

            return new Message(jsonString);
        }

        public override List<Message> SearchMsgFromDB(string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP)
        {
            string jsonString = wrapper.Call<string>("searchChatMsgFromDB", keywords, timestamp, maxCount, from, direction == MessageSearchDirection.UP ? "up" : "down");
            return TransformTool.JsonStringToMessageList(jsonString);
        }

        public override void SendConversationReadAck(string conversationId, CallBack handle = null)
        {
            wrapper.Call("ackConversationRead", conversationId, handle?.callbackId);
        }

        public override void SendMessage(ref Message message, CallBack handle = null)
        {

            CallbackManager.Instance().tempMsgDict.Add(message.LocalTime.ToString(), message);

            wrapper.Call<string>("sendMessage", message.ToJson().ToString(), handle?.callbackId);
        }

        public override void SendMessageReadAck(string messageId, CallBack handle = null)
        {
            wrapper.Call("ackMessageRead", messageId, handle?.callbackId);
        }

        public override void SendReadAckForGroupMessage(string messageId, string ackContent, CallBack callback = null)
        {
            //TODO: Add code
        }

        public override bool UpdateMessage(Message message)
        {
            return wrapper.Call<bool>("updateChatMessage", message.ToJson().ToString());
        }

        public override void RemoveMessagesBeforeTimestamp(long timeStamp, CallBack callback = null)
        {
            wrapper.Call("removeMessageBeforeTimestamp", timeStamp, callback?.callbackId);
        }

        public override void DeleteConversationFromServer(string conversationId, ConversationType conversationType, bool isDeleteServerMessages, CallBack callback = null)
        {
            wrapper.Call("deleteConversationFromServer", conversationId, TransformTool.ConversationTypeToInt(conversationType), isDeleteServerMessages, callback?.callbackId);
        }

        public override void FetchSupportLanguages(ValueCallBack<List<SupportLanguages>> handle = null)
        {
            //TO-DO
        }

        public override void TranslateMessage(ref Message message, List<string> targetLanguages, CallBack handle = null)
        {
            //TO-DO
        }
    }
    
}