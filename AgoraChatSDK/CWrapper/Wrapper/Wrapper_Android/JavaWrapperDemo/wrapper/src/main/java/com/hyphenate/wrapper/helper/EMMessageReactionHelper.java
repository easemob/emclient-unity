package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMMessageReaction;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class EMMessageReactionHelper {
    public static JSONObject toJson(EMMessageReaction reaction) throws JSONException {
        if (reaction == null) return null;
        JSONObject data = new JSONObject();
        data.put("reaction", reaction.getReaction());
        data.put("count", reaction.getUserCount());
        data.put("isAddedBySelf", reaction.isAddedBySelf());
        JSONArray jsonArray = new JSONArray();
        for (String userId: reaction.getUserList()) {
            jsonArray.put(userId);
        }
        data.put("userList", jsonArray);
        return data;
    }
}

