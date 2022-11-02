package com.hyphenate.javawrapper.util;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
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

    public static List<String> stringListFromJsonArray(JSONArray jsonArray) {
        ArrayList<String> list = new ArrayList<>();
        try {
            for (int i = 0; i < jsonArray.length(); i++) {
                String str = jsonArray.getString(i);
                list.add(str);
            }
        }catch (JSONException e) {
            e.printStackTrace();
        }

        return list;
    }

    public static JSONObject stringMapToJsonObject(Map<String, String> map) throws JSONException{
        if (map == null) {
            return null;
        }

        JSONObject jsonObject = new JSONObject();
        for (Map.Entry<String, String> entry: map.entrySet()) {
            jsonObject.put(entry.getKey(), entry.getValue());
        }
        return jsonObject;
    }

    public static JSONObject longMapToJsonObject(Map<String, Long> map) throws JSONException {
        if (map == null) {
            return null;
        }

        JSONObject jsonObject = new JSONObject();
        for (Map.Entry<String, Long> entry: map.entrySet()) {
            jsonObject.put(entry.getKey(), entry.getValue());
        }
        return jsonObject;
    }

    public static JSONObject intMapToJsonObject(Map<String, Integer> map) throws JSONException{
        if (map == null) {
            return null;
        }

        JSONObject jsonObject = new JSONObject();
        for (Map.Entry<String, Integer> entry: map.entrySet()) {
            jsonObject.put(entry.getKey(), entry.getValue());
        }
        return jsonObject;
    }

    public static JSONObject getReturnJsonObject(Object jsonObject) throws JSONException{
        JSONObject jo = new JSONObject();
        if (jsonObject != null) {
            jo.put("value", jsonObject);
        }
        return jo;
    }
}
