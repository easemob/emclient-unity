package com.hyphenate.javawrapper.util;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

public class EMHelper {
    public static JSONArray stringListToJsonArray(List<String> list){
        if (list == null) {
            return null;
        }

        JSONArray jsonArray = new JSONArray();
        for (String str: list) {
            jsonArray.put(str);
        }
        return jsonArray;
    }

    public static JSONObject stringMapToJsonObject(Map<String, String> map) {
        if (map == null) {
            return null;
        }

        JSONObject jsonObject = new JSONObject();
        try {
            for (Map.Entry<String, String> entry: map.entrySet()) {
                jsonObject.put(entry.getKey(), entry.getValue());
            }
        }catch (JSONException e) {
            e.printStackTrace();
        }
        return jsonObject;
    }
}
