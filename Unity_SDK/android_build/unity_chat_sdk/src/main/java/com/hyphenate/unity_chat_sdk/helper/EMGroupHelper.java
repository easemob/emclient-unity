package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupManager;
import com.hyphenate.chat.EMGroupOptions;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMGroupHelper {

    public static JSONObject toJson(EMGroup group) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("groupId", group.getGroupId());
        data.put("name", group.getGroupName());
        data.put("desc", group.getDescription());
        data.put("owner", group.getOwner());
        data.put("announcement", group.getAnnouncement());
        data.put("memberCount", group.getMemberCount());
        data.put("memberList", group.getMembers());
        data.put("adminList", group.getAdminList());
        data.put("blockList", group.getBlackList());
        data.put("muteList", group.getMuteList());
//        data.put("sharedFileList", group.getShareFileList());
        if (group.getGroupId() != null && EMClient.getInstance().pushManager().getNoPushGroups() != null) {
            data.put("noticeEnable", !EMClient.getInstance().pushManager().getNoPushGroups().contains(group.getGroupId()));
        }
        data.put("messageBlocked", group.isMsgBlocked());
        data.put("isAllMemberMuted", group.isAllMemberMuted());
        data.put("permissionType", intTypeFromGroupPermissionType(group.getGroupPermissionType()));

        EMGroupOptions options = new EMGroupOptions();
        options.extField = group.getExtension();
        options.maxUsers = group.getMaxUserCount();

        if (group.isPublic()) {
            if (group.isMemberOnly()) {
                options.style = EMGroupManager.EMGroupStyle.EMGroupStylePublicJoinNeedApproval;
            } else {
                options.style = EMGroupManager.EMGroupStyle.EMGroupStylePublicOpenJoin;
            }
        } else {
            if (group.isMemberAllowToInvite()) {
                options.style = EMGroupManager.EMGroupStyle.EMGroupStylePrivateMemberCanInvite;
            } else {
                options.style = EMGroupManager.EMGroupStyle.EMGroupStylePrivateOnlyOwnerInvite;
            }
        }
        data.put("options", EMGroupOptionsHelper.toJson(options));
        return data;
    }

    static int intTypeFromGroupPermissionType(EMGroup.EMGroupPermissionType type) {
        int ret = -1;
        switch (type) {
            case none: {
                ret = -1;
            }
            break;
            case member: {
                ret = 0;
            }
            break;
            case admin: {
                ret = 1;
            }
            break;
            case owner: {
                ret = 2;
            }
            break;
        }
        return ret;
    }
}
