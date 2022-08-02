package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatThread;

import org.json.JSONException;

import java.util.HashMap;
import java.util.Map;

public class EMChatThreadHelper {
    public static Map<String, Object> toJson(EMChatThread thread) throws JSONException {
        Map<String, Object> data = new HashMap<>();
        data.put("threadId", thread.getChatThreadId());
        if (thread.getChatThreadName() != null) {
            data.put("threadName", thread.getChatThreadName());
        }
        data.put("owner", thread.getOwner());
        data.put("msgId", thread.getMessageId());
        data.put("parentId", thread.getParentId());
        data.put("memberCount", thread.getMemberCount());
        data.put("messageCount", thread.getMessageCount());
        data.put("createAt", thread.getCreateAt());
        if (thread.getLastMessage() != null) {
            data.put("lastMessage", EMMessageHelper.toJson(thread.getLastMessage()));
        }
        return data;
    }
}
