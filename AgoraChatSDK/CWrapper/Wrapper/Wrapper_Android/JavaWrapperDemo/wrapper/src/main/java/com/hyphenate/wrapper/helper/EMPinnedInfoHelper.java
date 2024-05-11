package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMMessagePinInfo;

import org.json.JSONException;
import org.json.JSONObject;

public class EMPinnedInfoHelper {
    public static JSONObject toJson(EMMessagePinInfo pinnedInfo) throws JSONException {
        if (pinnedInfo == null) return null;
        JSONObject data = new JSONObject();
        data.put("pinnedBy", pinnedInfo.operatorId());
        data.put("pinnedAt", pinnedInfo.pinTime());
        return data;
    }
}

