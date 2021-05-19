package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMMessage;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMCursorResultHelper {
    public static Map<String, Object> toJson(EMCursorResult result) {
        Map<String, Object> data = new HashMap<>();
        data.put("cursor", result.getCursor());
        List list = (List) result.getData();
        List<Object> jsonList = new ArrayList<>();
        for (Object obj : list) {
            if (obj instanceof EMMessage) {
                jsonList.add(EMMessageHelper.toJson((EMMessage) obj));
            }

            if (obj instanceof EMGroup) {
                jsonList.add(EMGroupHelper.toJson((EMGroup) obj));
            }

            if (obj instanceof EMChatRoom) {
                jsonList.add(EMChatRoomHelper.toJson((EMChatRoom) obj));
            }

            if (obj instanceof String) {
                jsonList.add(obj);
            }

            if (obj instanceof EMGroupInfo) {
                EMGroup group = EMClient.getInstance().groupManager().getGroup(((EMGroupInfo) obj).getGroupId());
                if (group != null) {
                    jsonList.add(EMGroupHelper
                            .toJson(EMClient.getInstance().groupManager().getGroup(((EMGroupInfo) obj).getGroupId())));
                } else {
                    jsonList.add(EMGroupInfoHelper.toJson((EMGroupInfo) obj));
                }
            }
        }
        data.put("list", jsonList);

        return data;
    }
}
