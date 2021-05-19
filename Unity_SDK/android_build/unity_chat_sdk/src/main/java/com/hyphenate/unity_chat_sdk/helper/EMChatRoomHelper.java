package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;

import java.util.HashMap;
import java.util.Map;

public class EMChatRoomHelper {


    public static Map<String, Object> toJson(EMChatRoom chatRoom) {
        Map<String, Object> data = new HashMap<>();
        data.put("roomId", chatRoom.getId());
        data.put("name", chatRoom.getName());
        data.put("desc", chatRoom.getDescription());
        data.put("owner", chatRoom.getOwner());
        data.put("maxUsers", chatRoom.getMaxUsers());
        data.put("memberCount", chatRoom.getMemberCount());
        data.put("adminList", chatRoom.getAdminList());
        data.put("memberList", chatRoom.getMemberList());
        data.put("blockList", chatRoom.getBlackList());
        data.put("muteList", chatRoom.getMuteList().values());
        data.put("isAllMemberMuted", chatRoom.isAllMemberMuted());
        data.put("announcement", chatRoom.getAnnouncement());
        data.put("permissionType", intTypeFromPermissionType(chatRoom.getChatRoomPermissionType()));

        return data;
    }

    public static int intTypeFromPermissionType(EMChatRoom.EMChatRoomPermissionType type) {
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
            default:
                break;
        }
        return ret;
    }
}
