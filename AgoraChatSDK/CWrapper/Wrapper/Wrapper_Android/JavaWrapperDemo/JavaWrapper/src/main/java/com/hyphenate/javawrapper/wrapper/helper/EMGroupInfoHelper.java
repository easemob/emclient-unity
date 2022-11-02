package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMGroupInfo;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMGroupInfoHelper {
    public static JSONObject toJson(EMGroupInfo group) throws JSONException {
        if (group == null) return null;
        JSONObject data = new JSONObject();
        data.put("groupId", group.getGroupId());
        data.put("name", group.getGroupName());
        return data;
    }
}
