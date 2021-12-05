package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMUserInfo;

import org.json.JSONException;
import org.json.JSONObject;

public class EMUserInfoHelper {

    public static EMUserInfo fromJson(JSONObject json) throws JSONException {
        EMUserInfo userinfo = new EMUserInfo();
        if (json.has("userId")) {
            userinfo.setUserId(json.getString("userId"));
        }

        if (json.has("nickName")) {
            userinfo.setNickName(json.getString("nickName"));
        }
        if (json.has("avatarUrl")) {
            userinfo.setAvatarUrl(json.getString("avatarUrl"));
        }

        if (json.has("mail")){
            userinfo.setEmail(json.getString("mail"));
        }

        if (json.has("phone")){
            userinfo.setPhoneNumber(json.getString("phone"));
        }
        if (json.has("sign")){
            userinfo.setSignature(json.getString("sign"));
        }

        if (json.has("birth")) {
            userinfo.setBirth(json.getString("birth"));
        }
        if (json.has("ext")) {
            userinfo.setExt(json.getString("ext"));
        }

        userinfo.setGender(json.getInt("gender"));

        return userinfo;
    }

    public static JSONObject toJson(EMUserInfo userInfo) throws JSONException {
        JSONObject jo = new JSONObject();
        if (userInfo.getUserId() != null) {
            jo.put("userId", userInfo.getUserId());
        }

        if (userInfo.getAvatarUrl() != null) {
            jo.put("avatarUrl", userInfo.getAvatarUrl());
        }

        if (userInfo.getNickName() != null) {
            jo.put("nickName", userInfo.getNickName());
        }

        if (userInfo.getEmail() != null) {
            jo.put("mail", userInfo.getEmail());
        }

        if (userInfo.getPhoneNumber() != null) {
            jo.put("phone", userInfo.getPhoneNumber());
        }

        if (userInfo.getSignature() != null) {
            jo.put("sign", userInfo.getSignature());
        }

        if (userInfo.getBirth() != null) {
            jo.put("birth", userInfo.getBirth());
        }

        if (userInfo.getExt() != null) {
            jo.put("ext", userInfo.getExt());
        }

        jo.put("gender", userInfo.getGender());

        return jo;
    }
}