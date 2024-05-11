package com.hyphenate.wrapper;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMConversationHelper;
import com.hyphenate.wrapper.helper.EMMessageHelper;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Set;

public class EMConversationWrapper extends EMBaseWrapper {
    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.getConversationUnreadMsgCount.equals(method)) {
            ret = getUnreadMsgCount(jsonObject, callback);
        }
        else if (EMSDKMethod.markAllMessagesAsRead.equals(method)) {
            ret = markAllMessagesAsRead(jsonObject, callback);
        }
        else if (EMSDKMethod.markMessageAsRead.equals(method)) {
            ret = markMessageAsRead(jsonObject, callback);
        }
        else if (EMSDKMethod.syncConversationExt.equals(method)){
            ret = syncConversationExt(jsonObject, callback);
        }
        else if (EMSDKMethod.removeMessage.equals(method))
        {
            ret = removeMessage(jsonObject, callback);
        }
        else if (EMSDKMethod.getLatestMessage.equals(method)) {
            ret = getLatestMessage(jsonObject, callback);
        }
        else if (EMSDKMethod.getLatestMessageFromOthers.equals(method)) {
            ret = getLatestMessageFromOthers(jsonObject, callback);
        }
        else if (EMSDKMethod.clearAllMessages.equals(method)) {
            ret = clearAllMessages(jsonObject, callback);
        }
        else if (EMSDKMethod.insertMessage.equals((method))) {
            ret = insertMessage(jsonObject, callback);
        }
        else if (EMSDKMethod.appendMessage.equals(method)) {
            ret = appendMessage(jsonObject, callback);
        }
        else if (EMSDKMethod.updateConversationMessage.equals(method)) {
            ret = updateConversationMessage(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithId.equals(method)) {
            ret = loadMsgWithId(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithStartId.equals(method)) {
            ret = loadMsgWithStartId(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithKeywords.equals(method)) {
            ret = loadMsgWithKeywords(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithMsgType.equals(method)) {
            ret = loadMsgWithMsgType(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithTime.equals(method)) {
            ret = loadMsgWithTime(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithScope.equals(method)) {
            ret = loadMsgWithScope(jsonObject, callback);
        }
        else if(EMSDKMethod.messageCount.equals(method)) {
            ret = messageCount(jsonObject, callback);
        }
        else if(EMSDKMethod.removeMessages.equals(method)) {
            ret = removeMessages(jsonObject, callback);
        } else if(EMSDKMethod.pinnedMessages.equals(method)) {
            ret = pinnedMessages(jsonObject, callback);
        } else if(EMSDKMethod.marks.equals(method)) {
            ret = marks(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String getUnreadMsgCount(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        return EMHelper.getReturnJsonObject(conversation.getUnreadMsgCount()).toString();
    }

    private String markAllMessagesAsRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        conversation.markAllMessagesAsRead();
        return null;
    }

    private String markMessageAsRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String msgId = params.getString("msgId");
        conversation.markMessageAsRead(msgId);
        return null;
    }

    private String syncConversationExt(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject ext = params.getJSONObject("ext");
        String jsonStr = "";
        if (ext.length() != 0) {
            jsonStr = ext.toString();
        }
        conversation.setExtField(jsonStr);
        return EMHelper.getReturnJsonObject(true).toString();
    }

    private String removeMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String msgId = params.getString("msgId");
        conversation.removeMessage(msgId);
        return EMHelper.getReturnJsonObject(true).toString();
    }

    private String getLatestMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        EMMessage msg = conversation.getLastMessage();
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String getLatestMessageFromOthers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        EMMessage msg = conversation.getLatestMessageFromOthers();
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String clearAllMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        conversation.clearAllMessages();
        return EMHelper.getReturnJsonObject(true).toString();
    }

    private String insertMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        boolean ret = conversation.insertMessage(message);
        return EMHelper.getReturnJsonObject(ret).toString();
    }

    private String appendMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        boolean ret = conversation.appendMessage(message);
        return EMHelper.getReturnJsonObject(ret).toString();
    }

    private String updateConversationMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        boolean ret = conversation.updateMessage(message);
        return EMHelper.getReturnJsonObject(ret).toString();
    }

    private String loadMsgWithId(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
       
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String loadMsgWithStartId(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String startId = params.getString("startMessageId");
        int pageSize = params.getInt("count");
        EMConversation.EMSearchDirection direction = params.getInt("direction") == 0 ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        List<EMMessage> msgList = conversation.loadMoreMsgFromDB(startId, pageSize, direction);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }

        onSuccess(jsonArray, callback);
        return null;
    }

    private String loadMsgWithKeywords(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String keywords = params.getString("keywords");
        String sender = null;
        if (params.has("sender")) {
            sender = params.getString("sender");
        }
        final String name = sender;
        int count = params.getInt("count");
        long timestamp = params.getLong("timestamp");
        EMConversation.EMSearchDirection direction = params.getInt("direction") == 0 ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        List<EMMessage> msgList = conversation.searchMsgFromDB(keywords, timestamp, count, name, direction);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }

        onSuccess(jsonArray, callback);
        return null;
    }

    private String loadMsgWithMsgType(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        long timestamp = params.getLong("timestamp");
        String sender = null;
        if (params.has("sender")) {
            sender = params.getString("sender");
        }
        int count = params.getInt("count");
        EMConversation.EMSearchDirection direction = params.getInt("direction") == 0 ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        int iType = params.getInt("bodyType");
        EMMessage.Type type = EMMessage.Type.TXT;
        switch (iType) {
            case 0 : type = EMMessage.Type.TXT; break;
            case 1 : type = EMMessage.Type.IMAGE; break;
            case 2 : type = EMMessage.Type.VIDEO; break;
            case 3 : type = EMMessage.Type.LOCATION; break;
            case 4 : type = EMMessage.Type.VOICE; break;
            case 5 : type = EMMessage.Type.FILE; break;
            case 6 : type = EMMessage.Type.CMD; break;
            case 7 : type = EMMessage.Type.CUSTOM; break;
            case 8 : type = EMMessage.Type.COMBINE; break;
        }

        EMMessage.Type finalType = type;
        String finalSender = sender;
        List<EMMessage> msgList = conversation.searchMsgFromDB(finalType, timestamp, count, finalSender, direction);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }
        onSuccess(jsonArray, callback);
        return null;
    }

    private String loadMsgWithTime(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        long startTime = params.getLong("startTime");
        long endTime = params.getLong("endTime");
        int count = params.getInt("count");
        List<EMMessage> msgList = conversation.searchMsgFromDB(startTime, endTime, count);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }
        onSuccess(jsonArray, callback);
        return null;
    }

    private String loadMsgWithScope(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String keywords = params.getString("keywords");
        String sender = params.getString("from");
        int count = params.getInt("count");
        long timestamp = params.getLong("timestamp");
        EMConversation.EMSearchDirection direction = params.getInt("direction") == 0 ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        EMConversation.EMMessageSearchScope scope = EMMode.searchScopeFromInt(params.getInt("scope"));

        List<EMMessage> msgList = conversation.searchMsgFromDB(keywords, timestamp, count, sender, direction, scope);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }
        onSuccess(jsonArray, callback);
        return null;
    }

    private String messageCount(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        return EMHelper.getReturnJsonObject(conversation.getAllMsgCount()).toString();
    }

    private String removeMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        long startTs = params.getLong("startTime");
        long endTs = params.getLong("endTime");
        return EMHelper.getReturnJsonObject(conversation.removeMessages(startTs, endTs)).toString();
    }

    private String pinnedMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        List<EMMessage> msgList = conversation.pinnedMessages();
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }
        return EMHelper.getReturnJsonObject(jsonArray).toString();
    }

    private String marks(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        Set<EMConversation.EMMarkType> marks = conversation.marks();
        JSONArray jsonArray = new JSONArray();
        for(EMConversation.EMMarkType mark: marks) {
            jsonArray.put(EMMode.intFromMarkType(mark));
        }
        return EMHelper.getReturnJsonObject(jsonArray).toString();
    }

    private EMConversation conversationWithParam(JSONObject params ) throws JSONException {
        String con_id = params.getString("convId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("convType"));
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(con_id, type, true);
        return conversation;
    }

}
