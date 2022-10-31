package com.hyphenate.javawrapper.wrapper.helper;


import com.hyphenate.chat.EMPushManager;
import com.hyphenate.chat.EMSilentModeParam;
import com.hyphenate.chat.EMSilentModeTime;

import org.json.JSONException;
import org.json.JSONObject;

public class EMSilentModeParamHelper {
    public static EMSilentModeParam fromJson(JSONObject obj) throws JSONException {
        EMSilentModeParam.EMSilentModeParamType type = paramTypeFromInt(obj.getInt("paramType"));
        EMSilentModeParam param = new EMSilentModeParam(type);;
        if (obj.has("startTime") && obj.has("endTime")) {
            EMSilentModeTime startTime = EMSilentModeTimeHelper.fromJson(obj.getJSONObject("startTime"));
            EMSilentModeTime endTime = EMSilentModeTimeHelper.fromJson(obj.getJSONObject("endTime"));
            param.setSilentModeInterval(startTime, endTime);
        }

        if (obj.has("remindType")) {
            param.setRemindType(pushRemindFromInt(obj.getInt("remindType")));
        }

        if (obj.has("duration")) {
            int duration = obj.getInt("duration");
            param.setSilentModeDuration(duration);
        }
        return param;
    }

    public static EMSilentModeParam.EMSilentModeParamType paramTypeFromInt(int iParamType) {
        EMSilentModeParam.EMSilentModeParamType ret = EMSilentModeParam.EMSilentModeParamType.REMIND_TYPE;
        if (iParamType == 0) {
            ret = EMSilentModeParam.EMSilentModeParamType.REMIND_TYPE;
        }else if (iParamType == 1) {
            ret = EMSilentModeParam.EMSilentModeParamType.SILENT_MODE_DURATION;
        }else if (iParamType == 2) {
            ret = EMSilentModeParam.EMSilentModeParamType.SILENT_MODE_INTERVAL;
        }
        return ret;
    }

    public static int pushRemindTypeToInt(EMPushManager.EMPushRemindType type) {
        int ret = 0;
        if (type == EMPushManager.EMPushRemindType.ALL) {
            ret = 0;
        }else if (type == EMPushManager.EMPushRemindType.MENTION_ONLY) {
            ret = 1;
        }else if (type == EMPushManager.EMPushRemindType.NONE) {
            ret = 2;
        }
        return ret;
    }

    public static EMPushManager.EMPushRemindType pushRemindFromInt(int iType) {
        EMPushManager.EMPushRemindType type = EMPushManager.EMPushRemindType.ALL;
        if (iType == 0) {
            type = EMPushManager.EMPushRemindType.ALL;
        }else if (iType == 1) {
            type = EMPushManager.EMPushRemindType.MENTION_ONLY;
        }else if (iType == 2) {
            type = EMPushManager.EMPushRemindType.NONE;
        }
        return type;
    }
}