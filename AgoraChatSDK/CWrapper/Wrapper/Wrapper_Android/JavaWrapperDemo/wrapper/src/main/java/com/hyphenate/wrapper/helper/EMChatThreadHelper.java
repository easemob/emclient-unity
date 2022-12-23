package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMChatThread;

import org.json.JSONException;
import org.json.JSONObject;

public class EMChatThreadHelper {
    public static JSONObject toJson(EMChatThread thread) throws JSONException {
        if (thread == null) return null;
        JSONObject data = new JSONObject();
        data.put("threadId", thread.getChatThreadId());
        if (thread.getChatThreadName() != null) {
            data.put("name", thread.getChatThreadName());
        }
        data.put("owner", thread.getOwner());
        data.put("msgId", thread.getMessageId());
        data.put("parentId", thread.getParentId());
        data.put("memberCount", thread.getMemberCount());
        data.put("msgCount", thread.getMessageCount());
        data.put("createAt", thread.getCreateAt());
        if (thread.getLastMessage() != null) {
            data.put("lastMsg", EMMessageHelper.toJson(thread.getLastMessage()));
        }
        return data;
    }
}
