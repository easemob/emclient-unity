package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMPushConfigs;
import com.hyphenate.chat.EMPushManager;

import java.util.HashMap;
import java.util.Map;

public class EMPushConfigHelper {
    public static Map<String, Object> toJson(EMPushConfigs pushConfigs) {
        Map<String, Object> data = new HashMap<>();
        data.put("noDisturb", pushConfigs.isNoDisturbOn());
        data.put("noDisturbEndHour", pushConfigs.getNoDisturbEndHour());
        data.put("noDisturbStartHour", pushConfigs.getNoDisturbStartHour());
        data.put("pushStyle", pushConfigs.getDisplayStyle() != EMPushManager.DisplayStyle.SimpleBanner);
        return data;
    }
}
