package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMPresence;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMPresenceHelper {

    public static JSONObject toJson(EMPresence presence) throws JSONException {
        if (presence == null) return null;
        JSONObject data = new JSONObject();
        data.put("publisher", presence.getPublisher());
        data.put("statusDescription", presence.getExt());
        data.put("lastTime", presence.getLatestTime());
        data.put("expiryTime", presence.getExpiryTime());
        JSONObject statusList = new JSONObject();
        for (Map.Entry<String, Integer> entry: presence.getStatusList().entrySet()) {
            statusList.put(entry.getKey(), entry.getValue());
        }
        data.put("statusDetails", statusList);
        return data;
    }

}
