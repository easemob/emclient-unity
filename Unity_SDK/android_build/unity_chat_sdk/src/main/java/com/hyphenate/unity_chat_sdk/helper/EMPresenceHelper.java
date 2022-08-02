package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMPresence;

import java.util.HashMap;
import java.util.Map;

public class EMPresenceHelper {
    public static Map<String, Object> toJson(EMPresence presence) {
        Map<String, Object> data = new HashMap<>();
        data.put("publisher", presence.getPublisher());
        data.put("statusDescription", presence.getExt());
        data.put("lastTime", presence.getLatestTime());
        data.put("expiryTime", presence.getExpiryTime());
        Map<String, Integer> statusList = new HashMap<String, Integer>();
        statusList.putAll(presence.getStatusList());
        data.put("statusDetails", statusList);
        return data;
    }
}
