package com.hyphenate.wrapper.util;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;


public class EMHelper {

    public static  Map<String, String> getMapStrStrFromJsonObject(JSONObject jsonObject)  {

        HashMap<String, String> map = new HashMap<>();
        if (jsonObject == null) {
            return map;
        }
        Iterator<String> iterator = jsonObject.keys();
        try {
            while (iterator.hasNext()) {
                String key = iterator.next();
                map.put(key, jsonObject.getString(key));
            }
        }catch (JSONException e){}

        return map;
    }

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
        if (jsonArray == null) {
            return list;
        }

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
        JSONObject jsonObject = new JSONObject();
        if (map == null) {
            return jsonObject;
        }

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

    public static JSONObject getReturnJsonObject(Object obj) throws JSONException{
        JSONObject jo = new JSONObject();
        if (obj != null) {
            jo.put("ret", obj);
        }
        return jo;
    }
}
