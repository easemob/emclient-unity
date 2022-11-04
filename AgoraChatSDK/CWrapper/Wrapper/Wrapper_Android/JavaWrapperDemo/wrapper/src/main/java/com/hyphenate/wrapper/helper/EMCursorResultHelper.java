package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMChatThread;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMCursorResultHelper {
    public static JSONObject toJson(EMCursorResult result) throws JSONException {
        if (result == null) return null;
        JSONObject data = new JSONObject();
        data.put("cursor", result.getCursor());
        JSONArray jsonList = new JSONArray();
        if (result.getData() != null){
            List list = (List) result.getData();
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

                if (obj instanceof EMGroupReadAck) {
                    jsonList.put(EMGroupAckHelper.toJson((EMGroupReadAck) obj));
                }

                if (obj instanceof String) {
                    jsonList.put(obj);
                }

                if (obj instanceof EMGroupInfo) {
                    jsonList.put(EMGroupInfoHelper.toJson((EMGroupInfo) obj));
                }

                if (obj instanceof EMMessageReaction) {
                    jsonList.put(EMMessageReactionHelper.toJson((EMMessageReaction) obj));
                }

                if (obj instanceof EMChatThread) {
                    jsonList.put(EMChatThreadHelper.toJson((EMChatThread) obj));
                }
            }
        }
        data.put("list", jsonList);

        return data;
    }
}
