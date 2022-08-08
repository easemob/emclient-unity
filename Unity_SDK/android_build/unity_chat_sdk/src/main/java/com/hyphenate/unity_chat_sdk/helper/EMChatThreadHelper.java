package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatThread;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMChatThreadHelper {
    public static JSONObject toJson(EMChatThread thread) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("tId", thread.getChatThreadId());
        if (thread.getChatThreadName() != null) {
            data.put("name", thread.getChatThreadName());
        }
        data.put("owner", thread.getOwner());
        data.put("messageId", thread.getMessageId());
        data.put("parentId", thread.getParentId());
        data.put("membersCount", thread.getMemberCount());
        data.put("messageCount", thread.getMessageCount());
        data.put("createTimestamp", thread.getCreateAt());
        if (thread.getLastMessage() != null) {
            data.put("lastMessage", EMMessageHelper.toJson(thread.getLastMessage()));
        }
        return data;
    }
}
