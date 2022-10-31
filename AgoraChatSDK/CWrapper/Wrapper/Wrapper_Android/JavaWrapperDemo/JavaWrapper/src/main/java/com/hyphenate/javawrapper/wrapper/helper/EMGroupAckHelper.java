package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMGroupReadAck;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMGroupAckHelper {
    public static JSONObject toJson(EMGroupReadAck ack) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("msg_id", ack.getMsgId());
        data.put("ack_id", ack.getAckId());
        data.put("from", ack.getFrom());
        data.put("count", ack.getCount());
        data.put("timestamp", ack.getTimestamp());
        if (ack.getContent() != null) {
            data.put("content", ack.getContent());
        }
        return data;
    }
}
