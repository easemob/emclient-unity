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

        public override void FetchHistoryMessagesFromServer(string conversationId, ConversationType type, string startMessageId = null, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<CursorResult<Message>> handle = null)
        {
            wrapper.Call("fetchHistoryMessages", conversationId, TransformTool.ConversationTypeToInt(type), startMessageId, count, direction == MessageSearchDirection.UP ? 0 : 1, handle?.callbackId);
        }

        public override Conversation GetConversation(string conversationId, ConversationType type, bool createIfNeed = true)
        {
            string jsonString = wrapper.Call<string>("getConversation", conversationId, TransformTool.ConversationTypeToInt(type), createIfNeed, false);
            if (jsonString == null || jsonString.Length == 0) {
                return null;
            }

            return new Conversation(jsonString);
        }

        public override Conversation GetThreadConversation(string threadId)
        {
            string jsonString = wrapper.Call<string>("getConversation", threadId, TransformTool.ConversationTypeToInt(ConversationType.Group), true, false);
            if (jsonString == null || jsonString.Length == 0)
            {
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
            wrapper.Call("sendReadAckForGroupMessage", messageId, ackContent, callback?.callbackId);
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

        public override void FetchSupportLanguages(ValueCallBack<List<SupportLanguage>> handle = null)
        {
            wrapper.Call("fetchSupportLanguages", handle?.callbackId);
        }

        public override void TranslateMessage(Message message, List<string> targetLanguages, ValueCallBack<Message> handle = null)
        {
            wrapper.Call("translateMessage", message.ToJson().ToString(), TransformTool.JsonStringFromStringList(targetLanguages), handle?.callbackId);
        }

        public override void FetchGroupReadAcks(string messageId, string groupId, int pageSize = 20, string startAckId = null, ValueCallBack<CursorResult<GroupReadAck>> handle = null)
        {
            wrapper.Call("fetchGroupReadAcks", messageId, pageSize, startAckId, handle?.callbackId);
        }

        public override void ReportMessage(string messageId, string tag, string reason, CallBack handle = null)
        {
            wrapper.Call("reportMessage", messageId, tag, reason, handle?.callbackId);
        }
        public override void AddReaction(string messageId, string reaction, CallBack handle = null)
        {
            wrapper.Call("addReaction", messageId, reaction, handle?.callbackId);
        }
        public override void RemoveReaction(string messageId, string reaction, CallBack handle = null)
        {
            wrapper.Call("removeReaction", messageId, reaction, handle?.callbackId);
        }
        public override void GetReactionList(List<string> messageIdList, MessageType chatType, string groupId, ValueCallBack<Dictionary<string, List<MessageReaction>>> handle = null)
        {
            wrapper.Call("getReactionList", TransformTool.JsonStringFromStringList(messageIdList), (int)chatType, groupId, handle?.callbackId);
        }
        public override void GetReactionDetail(string messageId, string reaction, string cursor = null, int pageSize = 20, ValueCallBack<CursorResult<MessageReaction>> handle = null)
        {
            wrapper.Call("getReactionDetail", messageId, reaction, cursor ?? "", pageSize, handle?.callbackId);
        }
    }
    
}