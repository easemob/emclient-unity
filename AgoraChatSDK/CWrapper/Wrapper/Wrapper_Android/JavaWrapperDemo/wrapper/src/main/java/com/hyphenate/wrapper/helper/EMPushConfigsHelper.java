package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMPushConfigs;
import com.hyphenate.chat.EMPushManager;

import org.json.JSONException;
import org.json.JSONObject;

public class EMPushConfigsHelper {
    public static JSONObject toJson(EMPushConfigs pushConfigs) throws JSONException {
        if (pushConfigs == null) return null;
        JSONObject data = new JSONObject();
        data.put("noDisturb", pushConfigs.isNoDisturbOn());
        data.put("noDisturbEndHour", pushConfigs.getNoDisturbEndHour());
        data.put("noDisturbStartHour", pushConfigs.getNoDisturbStartHour());
        data.put("pushStyle", pushConfigs.getDisplayStyle() != EMPushManager.DisplayStyle.SimpleBanner);
        data.put("displayName", pushConfigs.getDisplayNickname());
        return data;
    }
}