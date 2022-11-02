package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMUserInfo;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMUserInfoHelper {
    public static EMUserInfo fromJson(JSONObject obj) throws JSONException {
        EMUserInfo userInfo = new EMUserInfo();
        if (obj.has("nickName")){
            userInfo.setNickname(obj.getString("nickName"));
        }
        if (obj.has("avatarUrl")){
            userInfo.setAvatarUrl(obj.optString("avatarUrl"));
        }
        if (obj.has("mail")){
            userInfo.setEmail(obj.optString("mail"));
        }
        if (obj.has("phone")){
            userInfo.setPhoneNumber(obj.optString("phone"));
        }
        if (obj.has("gender")){
            userInfo.setGender(obj.getInt("gender"));
        }
        if (obj.has("sign")){
            userInfo.setSignature(obj.optString("sign"));
        }
        if (obj.has("birth")){
            userInfo.setBirth(obj.getString("birth"));
        }
        if (obj.has("ext")){
            userInfo.setExt(obj.getString("ext"));
        }

        return userInfo;
    }

    public static JSONObject toJson(EMUserInfo userInfo) throws JSONException{
        if (userInfo == null) return null;
        JSONObject data = new JSONObject();
        data.put("userId", userInfo.getUserId());
        data.put("nickName", userInfo.getNickname());
        data.put("avatarUrl", userInfo.getAvatarUrl());
        data.put("mail", userInfo.getEmail());
        data.put("phone", userInfo.getPhoneNumber());
        data.put("gender", userInfo.getGender());
        data.put("sign", userInfo.getSignature());
        data.put("birth", userInfo.getBirth());
        data.put("ext", userInfo.getExt());

        return data;
    }
}


