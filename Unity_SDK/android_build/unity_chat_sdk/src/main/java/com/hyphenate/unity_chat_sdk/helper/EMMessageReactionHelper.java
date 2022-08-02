package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMessageReaction;

import java.util.HashMap;
import java.util.Map;

public class EMMessageReactionHelper {
    public static Map<String, Object> toJson(EMMessageReaction reaction) {
        Map<String, Object> data = new HashMap<>();
        data.put("reaction", reaction.getReaction());
        data.put("count", reaction.getUserCount());
        data.put("isAddedBySelf", reaction.isAddedBySelf());
        data.put("userList", reaction.getUserList());
        return data;
    }
}