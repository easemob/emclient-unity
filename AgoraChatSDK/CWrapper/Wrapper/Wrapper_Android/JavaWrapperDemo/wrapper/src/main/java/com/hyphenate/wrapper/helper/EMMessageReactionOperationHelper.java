package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMMessageReactionOperation;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMMessageReactionOperationHelper {
    static JSONObject toJson(EMMessageReactionOperation operation) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("userId", operation.getUserId());
        data.put("reaction", operation.getReaction());
        data.put("operate", operation.getOperation() == EMMessageReactionOperation.Operation.REMOVE ? 0 : 1);

        return data;
    }
}
