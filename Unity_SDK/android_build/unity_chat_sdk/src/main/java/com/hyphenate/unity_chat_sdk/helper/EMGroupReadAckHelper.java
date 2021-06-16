package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMGroupOptions;
import com.hyphenate.chat.EMGroupReadAck;

import org.json.JSONException;
import org.json.JSONObject;

public class EMGroupReadAckHelper {
    public static JSONObject toJson(EMGroupReadAck readAck) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("ackId", readAck.getAckId());
        data.put("count", readAck.getCount());
        data.put("content", readAck.getContent());
        data.put("msgId", readAck.getMsgId());
        data.put("from", readAck.getFrom());
        data.put("timestamp", readAck.getTimestamp());
        return data;
    }
}
