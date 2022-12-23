package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMGroup;
import com.hyphenate.wrapper.util.EMHelper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMGroupHelper {
    public static JSONObject toJson(EMGroup group) throws JSONException {
        if (group == null) return null;
        JSONObject data = new JSONObject();
        data.put("groupId", group.getGroupId());
        data.put("name", group.getGroupName());
        data.put("desc", group.getDescription());
        data.put("owner", group.getOwner());
        data.put("announcement", group.getAnnouncement());
        data.put("memberCount", group.getMemberCount());
        data.put("memberList", EMHelper.stringListToJsonArray(group.getMembers()));
        data.put("adminList", EMHelper.stringListToJsonArray(group.getAdminList()));
        data.put("blockList", EMHelper.stringListToJsonArray(group.getBlackList()));
        data.put("muteList", EMHelper.stringListToJsonArray(group.getMuteList()));
        data.put("block", group.isMsgBlocked());
        data.put("isDisabled", group.isDisabled());
        data.put("isMuteAll", group.isAllMemberMuted());
        data.put("permissionType", intTypeFromGroupPermissionType(group.getGroupPermissionType()));
        data.put("maxUserCount", group.getMemberCount());
        data.put("isMemberOnly", group.isMemberOnly());
        data.put("isMemberAllowToInvite", group.isMemberAllowToInvite());
        data.put("ext", group.getGroupId());
        return data;
    }

    public static int intTypeFromGroupPermissionType(EMGroup.EMGroupPermissionType type) {
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
