package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMGroupReadAck;

import org.json.JSONException;
import org.json.JSONObject;

public class EMGroupAckHelper {
    public static JSONObject toJson(EMGroupReadAck ack) throws JSONException {
        if (ack == null) return null;
        JSONObject data = new JSONObject();
        data.put("msgId", ack.getMsgId());
        data.put("ackId", ack.getAckId());
        data.put("from", ack.getFrom());
        data.put("count", ack.getCount());
        data.put("timestamp", ack.getTimestamp());
        if (ack.getContent() != null) {
            data.put("content", ack.getContent());
        }
        return data;
    }
}
