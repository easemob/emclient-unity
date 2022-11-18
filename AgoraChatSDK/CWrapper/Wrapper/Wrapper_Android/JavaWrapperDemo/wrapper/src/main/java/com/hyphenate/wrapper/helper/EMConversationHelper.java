package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMConversation;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Iterator;

public class EMConversationHelper {
    public static JSONObject toJson(EMConversation conversation) throws JSONException{
        if (conversation == null) return null;
        JSONObject data = new JSONObject();
        data.put("convId", conversation.conversationId());
        data.put("type", typeToInt(conversation.getType()));
        data.put("isThread", conversation.isChatThread());
        try {
            data.put("ext", jsonStringToMap(conversation.getExtField()));
        } catch (JSONException e) {

        } finally {
            // 不返回，每次取得时候都从原生取最新的
//            data.put("unreadCount", conversation.getUnreadMsgCount());
//            data.put("latestMessage", EMMessageHelper.toJson(conversation.getLastMessage()));
//            data.put("lastReceivedMessage", EMMessageHelper.toJson(conversation.getLatestMessageFromOthers()));
            return data;
        }
    }

    public static EMConversation.EMConversationType typeFromInt(int type) {
        switch (type) {
            case 0:
                return EMConversation.EMConversationType.Chat;
            case 1:
                return EMConversation.EMConversationType.GroupChat;
            case 2:
                return EMConversation.EMConversationType.ChatRoom;
        }

        return EMConversation.EMConversationType.Chat;
    }

    public static int typeToInt(EMConversation.EMConversationType type) {
        switch (type) {
            case Chat:
                return 0;
            case GroupChat:
                return 1;
            case ChatRoom:
                return 2;
        }

        return 0;
    }

    public static JSONObject jsonStringToMap(String content) throws JSONException {
        if (content == null) return null;
        content = content.trim();
        JSONObject result = new JSONObject();
        try {
            if (content.charAt(0) == '[') {
                JSONArray jsonArray = new JSONArray(content);
                for (int i = 0; i < jsonArray.length(); i++) {
                    Object value = jsonArray.get(i);
                    if (value instanceof JSONArray || value instanceof JSONObject) {
                        result.put(i + "", jsonStringToMap(value.toString().trim()));
                    } else {
                        result.put(i + "", jsonArray.getString(i));
                    }
                }
            } else if (content.charAt(0) == '{') {
                JSONObject jsonObject = new JSONObject(content);
                Iterator<String> iterator = jsonObject.keys();
                while (iterator.hasNext()) {
                    String key = iterator.next();
                    Object value = jsonObject.get(key);
                    if (value instanceof JSONArray || value instanceof JSONObject) {
                        result.put(key, jsonStringToMap(value.toString().trim()));
                    } else {
                        result.put(key, value.toString().trim());
                    }
                }
            } else {
                throw new JSONException("");
            }
        } catch (JSONException e) {
            throw new JSONException("");
        }
        return result;
    }
}
