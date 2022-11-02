package com.hyphenate.javawrapper.wrapper.helper;

import com.hyphenate.chat.EMSilentModeResult;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMSilentModeResultHelper {
    public static JSONObject toJson(EMSilentModeResult modeResult) throws JSONException {
        if (modeResult == null) return null;
        JSONObject data = new JSONObject();
        data.put("expireTs", modeResult.getExpireTimestamp());
        if (modeResult.getConversationId() != null) {
            data.put("conversationId", modeResult.getConversationId());
        }
        if (modeResult.getConversationType() != null) {
            data.put("conversationType", EMConversationHelper.typeToInt(modeResult.getConversationType()));
        }
        if (modeResult.getSilentModeStartTime() != null) {
            data.put("startTime", EMSilentModeTimeHelper.toJson(modeResult.getSilentModeStartTime()));
        }
        if (modeResult.getSilentModeEndTime() != null) {
            data.put("endTime", EMSilentModeTimeHelper.toJson(modeResult.getSilentModeEndTime()));
        }if (modeResult.getRemindType() != null) {
            data.put("remindType", EMSilentModeParamHelper.pushRemindTypeToInt(modeResult.getRemindType()));
        }

        return data;
    }
}