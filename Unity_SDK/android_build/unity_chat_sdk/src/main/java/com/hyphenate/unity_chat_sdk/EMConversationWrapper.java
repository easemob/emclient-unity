package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.unity_chat_sdk.helper.EMConversationHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMConversationWrapper extends EMWrapper{
    static public EMConversationWrapper wrapper() {
        return new EMConversationWrapper();
    }

    private boolean appendMessage(String conversationId, int conversationType, String messageString) throws JSONException {

        if (conversationId == null || conversationId.length() == 0) {
            return false;
        }

        EMConversation conversation = getConversation(conversationId, conversationType);
        EMMessage message = EMMessageHelper.fromJson(new JSONObject(messageString));
        conversation.appendMessage(message);
        return true;
    }

    private boolean deleteAllMessages(String conversationId, int conversationType) throws JSONException {
        EMConversation conversation = getConversation(conversationId, conversationType);
        conversation.clearAllMessages();
        return true;
    }

    private boolean removeMessage(String conversationId, int conversationType, String messageId){
        if (conversationId == null || conversationId.length() == 0) {
            return false;
        }
        if (messageId == null || messageId.length() == 0) {
            return false;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        conversation.removeMessage(messageId);
        return true;
    }

    private String getExt(String conversationId, int conversationType) {
        if (conversationId == null || conversationId.length() == 0) {
            return null;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        if (conversation == null) return null;
        return conversation.getExtField();
    }

    private boolean insertMessage(String conversationId, int conversationType, String messageString) throws JSONException {
        if (conversationId == null || conversationId.length() == 0) {
            return false;
        }

        if (messageString == null || messageString.length() == 0) {
            return false;
        }

        EMConversation conversation = getConversation(conversationId, conversationType);
        EMMessage message = EMMessageHelper.fromJson(new JSONObject(messageString));
        conversation.insertMessage(message);
        return true;
    }

    private String lastMessage(String conversationId, int conversationType){

        if (conversationId == null || conversationId.length() == 0) {
            return null;
        }

        String ret = null;

        EMConversation conversation = getConversation(conversationId, conversationType);
        EMMessage msg = conversation.getLastMessage();
        if (msg != null) {
            try {
                ret = EMMessageHelper.toJson(msg).toString();
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return ret;
    }

    private String lastReceivedMessage(String conversationId, int conversationType){

        if (conversationId == null || conversationId.length() == 0) {
            return null;
        }

        String ret = null;
        EMConversation conversation = getConversation(conversationId, conversationType);
        EMMessage msg = conversation.getLatestMessageFromOthers();
        if (msg != null) {
            try {
                ret = EMMessageHelper.toJson(msg).toString();
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return ret;
    }

    private String loadMsgWithId(String conversationId, int conversationType, String msgId) {
        if (conversationId == null || conversationId.length() == 0) {
            return null;
        }

        if (msgId == null || msgId.length() == 0) {
            return null;
        }

        String ret = null;
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
        if (msg != null) {
            try {
                ret = EMMessageHelper.toJson(msg).toString();
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return ret;
    }

    private void loadMsgWithStartId(String conversationId, int conversationType, String startId, int count, String directionString, String callbackId) {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        String ret = null;
        EMConversation conversation = getConversation(conversationId, conversationType);
        List<EMMessage> msgList = conversation.loadMoreMsgFromDB(startId, count);
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg: msgList) {
                jsonArray.put(EMMessageHelper.toJson(msg).toString());
            }
            ret = jsonArray.toString();
        }  catch (JSONException e) {
            e.printStackTrace();
        }
        onSuccess("List<EMMessage>", callbackId, ret);
    }

    private void loadMsgWithKeywords(String conversationId, int conversationType, String keywords, String sender, long timestamp, int count, String directionString, String callbackId) {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        String ret = null;
        EMConversation conversation = getConversation(conversationId, conversationType);
        EMConversation.EMSearchDirection direction = searchDirectionFromString(directionString);
        List<EMMessage> msgList = conversation.searchMsgFromDB(keywords, timestamp, count, sender, direction);
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg: msgList) {
                jsonArray.put(EMMessageHelper.toJson(msg).toString());
            }
            ret = jsonArray.toString();
        }  catch (JSONException e) {
            e.printStackTrace();
        }

        onSuccess("List<EMMessage>", callbackId, ret);
    }

    private void loadMsgWithMsgType(String conversationId, int conversationType, String typeString, String sender, long timestamp, int count, String directionString, String callbackId) {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        String ret = null;
        EMConversation conversation = getConversation(conversationId, conversationType);
        EMConversation.EMSearchDirection direction = searchDirectionFromString(directionString);
        EMMessage.Type type = EMMessage.Type.TXT;
        switch (typeString) {
            case "txt" : type = EMMessage.Type.TXT; break;
            case "loc" : type = EMMessage.Type.LOCATION; break;
            case "cmd" : type = EMMessage.Type.CMD; break;
            case "custom" : type = EMMessage.Type.CUSTOM; break;
            case "file" : type = EMMessage.Type.FILE; break;
            case "img" : type = EMMessage.Type.IMAGE; break;
            case "video" : type = EMMessage.Type.VIDEO; break;
            case "voice" : type = EMMessage.Type.VOICE; break;
        }
        List<EMMessage> msgList = conversation.searchMsgFromDB(type, timestamp, count, sender, direction);
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg: msgList) {
                jsonArray.put(EMMessageHelper.toJson(msg).toString());
            }
            ret = jsonArray.toString();
        }  catch (JSONException e) {
            e.printStackTrace();
        }
        onSuccess("List<EMMessage>", callbackId, ret);
    }

    private void loadMsgWithTime(String conversationId, int conversationType, long startTime, long endTime, int count, String callbackId ) throws JSONException {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        String ret = null;
        EMConversation conversation = getConversation(conversationId, conversationType);
        List<EMMessage> msgList = conversation.searchMsgFromDB(startTime, endTime, count);
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg: msgList) {
                jsonArray.put(EMMessageHelper.toJson(msg).toString());
            }
            ret = jsonArray.toString();
        }  catch (JSONException e) {
            e.printStackTrace();
        }
        onSuccess("List<EMMessage>", callbackId, ret);
    }

    private void markAllMessagesAsRead(String conversationId, int conversationType) throws JSONException {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        conversation.markAllMessagesAsRead();
    }

    private void markMessageAsRead(String conversationId, int conversationType, String messageId) throws JSONException {
        if (conversationId == null || conversationId.length() == 0 || messageId == null || messageId.length() == 0) {
            return;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        conversation.markMessageAsRead(messageId);
    }

    private void SetExt(String conversationId, int conversationType, String ext) {
        if (conversationId == null || conversationId.length() == 0) {
            return;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        conversation.setExtField(ext);
    }

    private int unreadCount(String conversationId, int conversationType) {
        if (conversationId == null || conversationId.length() == 0) {
            return 0;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        return conversation.getUnreadMsgCount();
    }

    private int messageCount(String conversationId, int conversationType) {
        if (conversationId == null || conversationId.length() == 0) {
            return 0;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        return conversation.getAllMsgCount();
    }

    private boolean updateConversationMessage(String conversationId, int conversationType, String messageString) throws JSONException {
        if (conversationId == null || conversationId.length() == 0 || messageString == null || messageString.length() == 0) {
            return false;
        }
        EMConversation conversation = getConversation(conversationId, conversationType);
        EMMessage message = EMMessageHelper.fromJson(new JSONObject(messageString));
        conversation.updateMessage(message);
        return true;
    }

    private EMConversation getConversation(String conversationId, int conType ){

        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(conType);
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conversationId, type, true);

        return conversation;
    }

    private EMConversation.EMSearchDirection searchDirectionFromString(String direction) {
        return direction.equals("up")  ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
    }
}
