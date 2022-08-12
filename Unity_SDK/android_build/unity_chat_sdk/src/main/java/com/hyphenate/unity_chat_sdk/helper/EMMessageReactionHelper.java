package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMessageReaction;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMMessageReactionHelper {
    public static JSONObject toJson(EMMessageReaction reaction) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("reaction", reaction.getReaction());
        data.put("count", reaction.getUserCount());
        data.put("state", reaction.isAddedBySelf());
        JSONArray jsonArray = new JSONArray();
        for (String username: reaction.getUserList()) {
            jsonArray.put(username);
        }
        data.put("userList", jsonArray);
        return data;
    }
}