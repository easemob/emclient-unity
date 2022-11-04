using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    internal class ConversationManager : BaseManager
    {

        internal ConversationManager() : base(null, SDKMethod.conversationManager)
        {

        }

        internal Message LastMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            JSONNode jsonNode = NativeGet(SDKMethod.getLatestMessage, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jsonNode);
        }

        internal Message LastReceivedMessage(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            JSONNode jsonNode = NativeGet(SDKMethod.getLatestMessageFromOthers, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jsonNode);
        }

        internal Dictionary<string, string> GetExt(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            // TODO: dujiepeng 需要提供get方法？ 此处key值不对。
            JSONNode jsonNode = NativeGet(SDKMethod.syncConversationExt, jo_param).GetReturnJsonNode();
            return Dictionary.StringDictionaryFromJsonObject(jsonNode);
        }

        internal bool SetExt(string conversationId, ConversationType conversationType, Dictionary<string, string> ext)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("ext", JsonObject.JsonObjectFromDictionary(ext));
            JSONNode jn = NativeGet(SDKMethod.syncConversationExt, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal int UnReadCount(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.getConversationUnreadMsgCount, jo_param).GetReturnJsonNode();
            return jn.IsNumber ? jn.AsInt : 0;
        }

        internal int MessagesCount(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.messageCount, jo_param).GetReturnJsonNode();
            return jn.IsNumber ? jn.AsInt : 0;
        }

        internal void MarkMessageAsRead(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msgId", messageId);
            NativeGet(SDKMethod.markMessageAsRead, jo_param);
        }

        internal void MarkAllMessageAsRead(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            NativeGet(SDKMethod.markAllMessagesAsRead, jo_param);
        }

        internal bool InsertMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.insertMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool AppendMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.appendMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool UpdateMessage(string conversationId, ConversationType conversationType, Message message)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msg", message.ToJsonObject());
            JSONNode jn = NativeGet(SDKMethod.updateChatMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool DeleteMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.removeMessage, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal bool DeleteAllMessages(string conversationId, ConversationType conversationType)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            JSONNode jn = NativeGet(SDKMethod.clearAllMessages, jo_param).GetReturnJsonNode();
            return jn.IsBoolean ? jn.AsBool : false;
        }

        internal Message LoadMessage(string conversationId, ConversationType conversationType, string messageId)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("msgId", messageId);
            JSONNode jn = NativeGet(SDKMethod.loadMsgWithId, jo_param).GetReturnJsonNode();
            return ModelHelper.CreateWithJsonObject<Message>(jn);
        }

        internal void LoadMessagesWithMsgType(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("bodyType", bodyType.ToInt());
            jo_param.Add("sender", sender);
            jo_param.Add("count", count);
            jo_param.Add("direction", direction.ToInt());
            jo_param.Add("timestamp", timestamp);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithMsgType, jo_param, callback, process);
        }

        internal void LoadMessages(string conversationId, ConversationType conversationType, string startMessageId = "", int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("startMessageId", startMessageId);
            jo_param.Add("count", count);
            jo_param.Add("direction", direction.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithStartId, jo_param, callback, process);
        }

        internal void LoadMessagesWithKeyword(string conversationId, ConversationType conversationType, string keywords = "", string sender = "", long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("keywords", keywords);
            jo_param.Add("count", count);
            jo_param.Add("timestamp", timestamp);
            jo_param.Add("sender", sender);
            jo_param.Add("direction", direction.ToInt());

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithKeywords, jo_param, callback, process);
        }

        internal void LoadMessagesWithTime(string conversationId, ConversationType conversationType, long startTime, long endTime, int count = 20, ValueCallBack<List<Message>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("convId", conversationId);
            jo_param.Add("convType", conversationType.ToInt());
            jo_param.Add("startTime", startTime);
            jo_param.Add("endTime", endTime);
            jo_param.Add("count", count);


            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<Message>(jsonNode);
            };

            NativeCall<List<Message>>(SDKMethod.loadMsgWithTime, jo_param, callback, process);
        }

    }
}