package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class EMTransformHelper {

    static public JSONArray jsonArrayFromStringList(List<String> list) {
        if (list == null) return  null;
        JSONArray jsonAry = new JSONArray();
        if (list != null) {
            for (String str : list) {
                jsonAry.put(str);
            }
        }
        return jsonAry;
    }


    static public JSONArray jsonArrayFromChatRoomList(List<EMChatRoom> list) {
        if (list == null) return  null;
        JSONArray jsonArray = new JSONArray();
        for (EMChatRoom ite: list) {
            try {
                jsonArray.put(EMChatRoomHelper.toJson(ite));
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }
        return jsonArray;
    }

    static public JSONArray jsonArrayFromGroupList(List<EMGroup> list) {
        if (list == null) return  null;
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

    static public String[] jsonStringToStringArray(String jsonString) {
        if (jsonString == null) return  null;
        try {
            JSONArray ja = new JSONArray(jsonString);
            String[] strings = new String[ja.length()];
            for (int i = 0; i < ja.length(); i++) {
                strings[i] = ja.getString(i);
            }
            return strings;
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }

        return null;
    }

    static public List<String> jsonStringToStringList(String jsonString) {
        if (jsonString == null) return  null;
        try {
            JSONArray ja = new JSONArray(jsonString);
            List<String> list = new ArrayList<String>();
            for (int i = 0; i < ja.length(); i++) {
                list.add(ja.getString(i));
            }
            return list;
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }

        return null;
    }

}
