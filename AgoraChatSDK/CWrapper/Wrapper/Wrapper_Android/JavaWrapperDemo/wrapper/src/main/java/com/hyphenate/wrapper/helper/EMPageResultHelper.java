package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMPageResult;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMPageResultHelper {
    public static JSONObject toJson(EMPageResult result) throws JSONException {
        if (result == null) return null;
        JSONObject data = new JSONObject();
        data.put("count", result.getPageCount());
        JSONArray jsonArray = new JSONArray();
        if (result.getData() != null){
            List list = (List) result.getData();
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
            }
        }
        data.put("list", jsonArray);
        return data;
    }
}
