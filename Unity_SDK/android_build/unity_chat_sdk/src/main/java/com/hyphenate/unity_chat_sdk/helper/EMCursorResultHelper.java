package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;

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
                jsonArray.put(EMMessageHelper.toJson((EMMessage) obj).toString());
            }

            if (obj instanceof EMGroup) {
                jsonArray.put(EMGroupHelper.toJson((EMGroup) obj).toString());
            }

            if (obj instanceof EMChatRoom) {
                jsonArray.put(EMChatRoomHelper.toJson((EMChatRoom) obj).toString());
            }

            if (obj instanceof String) {
                jsonArray.put(obj);
            }

            if (obj instanceof EMGroupInfo) {
                jsonArray.put(EMGroupInfoHelper.toJson((EMGroupInfo) obj).toString());
            }

            if (obj instanceof EMGroupReadAck) {
                jsonArray.put(EMGroupReadAckHelper.toJson((EMGroupReadAck) obj).toString());
            }

            if (obj instanceof EMMessageReaction) {
                jsonArray.put(EMMessageReactionHelper.toJson((EMMessageReaction) obj).toString());
            }
        }
        data.put("list", jsonArray.toString());

        return data;
    }
}
