package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMChatRoomHelper {


    public static JSONObject toJson(EMChatRoom chatRoom) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("roomId", chatRoom.getId());
        data.put("name", chatRoom.getName());
        data.put("desc", chatRoom.getDescription());
        data.put("owner", chatRoom.getOwner());
        data.put("maxUsers", chatRoom.getMaxUsers());
        data.put("memberCount", chatRoom.getMemberCount());
        data.put("adminList", EMTransformHelper.jsonArrayFromStringList(chatRoom.getAdminList()).toString());
        data.put("memberList", EMTransformHelper.jsonArrayFromStringList(chatRoom.getMemberList()).toString());
        data.put("blockList", EMTransformHelper.jsonArrayFromStringList(chatRoom.getBlackList()).toString());

        List<String> muteList = new ArrayList<>();
        Object[] objArray = chatRoom.getMuteList().keySet().toArray();
        for (Object obj: objArray) {
            muteList.add((String)obj);
        }

        data.put("muteList", EMTransformHelper.jsonArrayFromStringList(muteList).toString());
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
