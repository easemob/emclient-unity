package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMMessage;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMCursorResultHelper {
    public static JSONObject toJson(EMCursorResult result) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("cursor", result.getCursor());
        List list = (List) result.getData();
        JSONArray jsonArray = new JSONArray ();
        for (Object obj : list) {
            if (obj instanceof EMMessage) {
                jsonArray.put(EMMessageHelper.toJson((EMMessage) obj));
            }

            if (obj instanceof EMGroup) {
                jsonArray.put(EMGroupHelper.toJson((EMGroup) obj));
            }

            if (obj instanceof EMChatRoom) {
                jsonArray.put(EMChatRoomHelper.toJson((EMChatRoom) obj));
            }

            if (obj instanceof String) {
                jsonArray.put(obj);
            }

            if (obj instanceof EMGroupInfo) {
                jsonArray.put(EMGroupInfoHelper.toJson((EMGroupInfo) obj));
            }
        }
        data.put("list", jsonArray);

        return data;
    }
}
