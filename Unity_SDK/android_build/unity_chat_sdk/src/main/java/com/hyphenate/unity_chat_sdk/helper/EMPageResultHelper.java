package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMPageResult;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMPageResultHelper {
    public static Map<String, Object> toJson(EMPageResult result) {
        Map<String, Object> data = new HashMap<>();
        data.put("count", result.getPageCount());
        List list = (List) result.getData();
        List<Map> jsonList = new ArrayList<>();
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
        }
        data.put("list", jsonList);
        return data;
    }
}
