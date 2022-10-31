package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMSilentModeTime;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMSilentModeTimeHelper {
    public static EMSilentModeTime fromJson(JSONObject obj) throws JSONException {
        int hour = obj.getInt("hour");
        int minute = obj.getInt("minute");
        EMSilentModeTime modeTime = new EMSilentModeTime(hour, minute);
        return modeTime;
    }

    public static JSONObject toJson(EMSilentModeTime modeTime) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("hour", modeTime.getHour());
        data.put("minute", modeTime.getMinute());
        return data;
    }
}
