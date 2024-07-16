package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMRecallMessageInfo;

import org.json.JSONException;
import org.json.JSONObject;

public class EMRecallMessageInfoHelper {
    public static JSONObject toJson(EMRecallMessageInfo info) throws JSONException {
        if (info == null) return null;
        JSONObject data = new JSONObject();
        data.put("recallBy", info.getRecallBy());
        data.put("recallMessageId", info.getRecallMessageId());
        data.put("ext", info.getExt());
        if (info.getRecallMessage() != null) {
            data.put("recallMessage", EMMessageHelper.toJson(info.getRecallMessage()));
        }
        return data;
    }
}
