using System;
using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    internal class ConversationManager : BaseManager
    {

        internal ConversationManager(NativeListener listener) : base(listener, SDKMethod.conversationManager)
        {

        }

        internal Message LastMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.getLatestMessage, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jn);

        }

        internal Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.getLatestMessageFromOthers, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jn);

        }

        internal bool SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("ext", JsonObject.JsonObjectFromDictionary(ext));
            JSONNode jn = NativeGet(SDKMethod.syncConversationExt, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal int UnReadCount(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.getConversationUnreadMsgCount, jo_param).GetReturnJsonNode();
            return jn.IsNumber ? jn.AsInt : 0;
        }

        internal int MessagesCount(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.messageCount, jo_param).GetReturnJsonNode();
            return jn.IsNumber ? jn.AsInt : 0;
        }

        internal void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msgId", messageId);
            NativeGet(SDKMethod.markMessageAsRead, jo_param);
        }

        internal void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            NativeGet(SDKMethod.markAllMessagesAsRead, jo_param);
        }

        internal bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.insertMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.appendMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.updateConversationMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.removeMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool DeleteMessages(string conversationId, ConversationType conversationType, long startTime, long endTime)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("startTime", startTime);
            jo_param.AddWithoutNull("endTime", endTime);
            JSONNode jn = NativeGet(SDKMethod.removeMessages, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.clearAllMessages, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.loadMsgWithId, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jn);
        }

        internal void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("bodyType", bodyType.ToInt());
            jo_param.AddWithoutNull("sender", sender);
            jo_param.AddWithoutNull("count", count);
            jo_param.AddWithoutNull("direction", direction.ToInt());
            jo_param.AddWithoutNull("timestamp", timestamp);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithMsgType, jo_param, callback, process);
        }

        internal void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("startMessageId", startMessageId);
            jo_param.AddWithoutNull("count", count);
            jo_param.AddWithoutNull("direction", direction.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithStartId, jo_param, callback, process);
        }

        internal void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("keywords", keywords);
            jo_param.AddWithoutNull("count", count);
            jo_param.AddWithoutNull("timestamp", timestamp);
            jo_param.AddWithoutNull("sender", sender);
            jo_param.AddWithoutNull("direction", direction.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithKeywords, jo_param, callback, process);
        }

        internal void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("startTime", startTime);
            jo_param.AddWithoutNull("endTime", endTime);
            jo_param.AddWithoutNull("count", count);


            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithTime, jo_param, callback, process);
        }

        internal void LoadMessagesWithScope(string conversationId, ConversationType conversationType, string keywords, long timestamp = 0, int maxCount = 20, string from = null, MessageSearchDirection direction = MessageSearchDirection.UP, MessageSearchScope scope = MessageSearchScope.CONTENT, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            jo_param.AddWithoutNull("keywords", keywords);
            jo_param.AddWithoutNull("from", from ?? "");
            jo_param.AddWithoutNull("count", maxCount);
            jo_param.AddWithoutNull("timestamp", timestamp.ToString());
            jo_param.AddWithoutNull("direction", direction.ToInt());
            jo_param.AddWithoutNull("scope", scope.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithScope, jo_param, callback, process);
        }

        internal List<Message> PinnedMessages(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.pinnedMessages, jo_param).GetReturnJsonNode();
            return List.BaseModelListFromJsonArray<Message>(jn);
        }

        internal List<MarkType> Marks(string conversationId, ConversationType conversationType)
        {
            List<MarkType> marks = new List<MarkType>();

            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("convId", conversationId);
            jo_param.AddWithoutNull("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.marks, jo_param).GetReturnJsonNode();
            List<int> marks_int = List.IntListFromJsonArray(jn);

            foreach (int i in marks_int)
            {
                marks.Add((MarkType)i);
            }

            return marks;
        }
    }
}