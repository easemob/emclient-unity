package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMChatThread;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMChatThreadHelper {
    public static JSONObject toJson(EMChatThread thread) throws JSONException {
        JSONObject data = new JSONObject();
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
