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
            loadMsgWithKeywords(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithMsgType.equals(method)) {
            loadMsgWithMsgType(jsonObject, callback);
        }
        else if (EMSDKMethod.loadMsgWithTime.equals(method)) {
            loadMsgWithTime(jsonObject, callback);
        }
        else if(EMSDKMethod.messageCount.equals(method)) {
            messageCount(jsonObject, callback);
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
        String msg_id = params.getString("msgId");
        conversation.markMessageAsRead(msg_id);
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
        return null;
    }

    private String removeMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        String msg_id = params.getString("msgId");
        conversation.removeMessage(msg_id);
        return null;
    }

    private String getLatestMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        EMMessage msg = conversation.getLastMessage();
        return EMHelper.getReturnJsonObject(EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg))).toString();
    }

    private String getLatestMessageFromOthers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        EMMessage msg = conversation.getLatestMessageFromOthers();
        return EMHelper.getReturnJsonObject(EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg))).toString();
    }

    private String clearAllMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        conversation.clearAllMessages();
        return null;
    }

    private String insertMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        conversation.insertMessage(message);
        return null;
    }

    private String appendMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        conversation.appendMessage(message);
        return null;
    }

    private String updateConversationMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        JSONObject msg = params.getJSONObject("msg");
        EMMessage message = EMMessageHelper.fromJson(msg);
        conversation.updateMessage(message);
        return null;
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

        return EMHelper.getReturnJsonObject(jsonArray).toString();
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

        return EMHelper.getReturnJsonObject(jsonArray).toString();
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
        }

        EMMessage.Type finalType = type;
        String finalSender = sender;
        List<EMMessage> msgList = conversation.searchMsgFromDB(finalType, timestamp, count, finalSender, direction);
        JSONArray jsonArray = new JSONArray();
        for(EMMessage msg: msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }
        return EMHelper.getReturnJsonObject(jsonArray).toString();
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
        return EMHelper.getReturnJsonObject(jsonArray).toString();
    }

    private String messageCount(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMConversation conversation = conversationWithParam(params);
        return EMHelper.getReturnJsonObject(conversation.getAllMsgCount()).toString();
    }

    private EMConversation conversationWithParam(JSONObject params ) throws JSONException {
        String con_id = params.getString("convId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("convType"));
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(con_id, type, true);
        return conversation;
    }

}