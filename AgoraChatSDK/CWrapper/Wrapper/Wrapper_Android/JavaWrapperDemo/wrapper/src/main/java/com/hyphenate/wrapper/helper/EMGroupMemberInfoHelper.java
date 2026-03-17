package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupMemberInfo;

import org.json.JSONException;
import org.json.JSONObject;

public class EMGroupMemberInfoHelper {
    public static EMGroupMemberInfo fromJson(JSONObject json) throws JSONException {
        // Note: EMGroupMemberInfo doesn't have a public constructor,
        // so we cannot create an instance from JSON in the wrapper layer.
        // This method is kept for potential future use if the SDK provides
        // a way to create EMGroupMemberInfo instances.
        return null;
    }

    public static JSONObject toJson(EMGroupMemberInfo info) throws JSONException {
        if (info == null) return null;
        
        JSONObject data = new JSONObject();
        data.put("memberId", info.getMemberId());
        data.put("joinedTimestamp", info.getJoinTime());
        data.put("role", roleToInt(info.getRole()));
        return data;
    }

    public static int roleToInt(EMGroup.EMGroupPermissionType role) {
        switch (role) {
            case none:
                return -1;
            case member:
                return 0;
            case admin:
                return 1;
            case owner:
                return 2;
        }
        return -1;
    }

    public static EMGroup.EMGroupPermissionType roleFromInt(int role) {
        switch (role) {
            case -1:
                return EMGroup.EMGroupPermissionType.none;
            case 0:
                return EMGroup.EMGroupPermissionType.member;
            case 1:
                return EMGroup.EMGroupPermissionType.admin;
            case 2:
                return EMGroup.EMGroupPermissionType.owner;
        }
        return EMGroup.EMGroupPermissionType.none;
    }
}

