package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMMessageReaction;

import org.json.JSONException;
import org.json.JSONObject;

public class EMMessageReactionHelper {
    public static JSONObject toJson(EMMessageReaction reaction) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("reaction", reaction.getReaction());
        data.put("count", reaction.getUserCount());
        data.put("isAddedBySelf", reaction.isAddedBySelf());
        data.put("userList", reaction.getUserList());
        return data;
    }
}

