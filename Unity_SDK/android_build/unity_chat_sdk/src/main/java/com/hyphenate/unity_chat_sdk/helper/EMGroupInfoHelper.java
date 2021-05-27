package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMGroupInfo;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMGroupInfoHelper {
    public static JSONObject toJson(EMGroupInfo group) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("groupId", group.getGroupId());
        data.put("groupName", group.getGroupName());
        return data;
    }
}
