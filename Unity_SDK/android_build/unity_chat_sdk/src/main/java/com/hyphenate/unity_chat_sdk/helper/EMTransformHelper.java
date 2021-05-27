package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMTransformHelper {
    static public JSONArray stringListToJsonArray(List<String> list) {
        JSONArray jsonAry = new JSONArray();
        if (list != null) {
            for (String str : list) {
                jsonAry.put(str);
            }
        }
        return jsonAry;
    }


    static public JSONArray groupListToJsonArray(List<EMGroup> list) {
        JSONArray jsonArray = new JSONArray();
        for (EMGroup group: list) {
            try {
                jsonArray.put(EMGroupHelper.toJson(group));
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return jsonArray;
    }

    static public JSONArray groupInfoListToJsonArray(List<EMGroupInfo> list) {
        JSONArray jsonArray = new JSONArray();
        for (EMGroupInfo group: list) {
            try {
                jsonArray.put(EMGroupInfoHelper.toJson(group));
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return jsonArray;
    }

    static public String stringListToString(List<String> list) {
        String ret = "";
        for (String s: list) {
            ret = ret + ",";
        }
        ret = ret.substring(0, ret.length() - 1);
        return ret;
    }

}
