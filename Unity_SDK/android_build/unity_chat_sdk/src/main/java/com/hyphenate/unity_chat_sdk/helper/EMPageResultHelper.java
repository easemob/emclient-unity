package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMPageResult;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMPageResultHelper {
    public static JSONObject toJson(EMPageResult result) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("count", result.getPageCount());
        List list = (List) result.getData();
        JSONArray jsonList = new JSONArray();
        for (Object obj : list) {
            if (obj instanceof EMMessage) {
                jsonList.put(EMMessageHelper.toJson((EMMessage) obj));
            }

            if (obj instanceof EMGroup) {
                jsonList.put(EMGroupHelper.toJson((EMGroup) obj));
            }

            if (obj instanceof EMChatRoom) {
                jsonList.put(EMChatRoomHelper.toJson((EMChatRoom) obj));
            }
        }
        data.put("list", jsonList);
        return data;
    }
}
