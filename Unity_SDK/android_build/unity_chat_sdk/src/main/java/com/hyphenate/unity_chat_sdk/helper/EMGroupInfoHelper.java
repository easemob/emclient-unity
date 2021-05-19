package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMGroupInfo;

import java.util.HashMap;
import java.util.Map;

public class EMGroupInfoHelper {
    public static Map<String, Object> toJson(EMGroupInfo group) {
        Map<String, Object> data = new HashMap<>();
        data.put("groupId", group.getGroupId());
        data.put("name", group.getGroupName());
        return data;
    }
}
