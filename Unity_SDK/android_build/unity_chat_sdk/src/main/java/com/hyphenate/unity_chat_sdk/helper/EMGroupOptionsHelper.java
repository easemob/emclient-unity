package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMGroupManager;
import com.hyphenate.chat.EMGroupOptions;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMGroupOptionsHelper {
    public static EMGroupOptions fromJson(JSONObject json) throws JSONException{
        EMGroupOptions options = new EMGroupOptions();
        options.maxUsers = json.getInt("maxCount");
        options.inviteNeedConfirm = json.getBoolean("inviteNeedConfirm");
        options.extField = json.getString("ext");
        options.style = styleFromInt(json.getInt("style"));
        return options;
    }

    public static JSONObject toJson(EMGroupOptions options) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("maxCount", options.maxUsers);
        data.put("inviteNeedConfirm", options.inviteNeedConfirm);
        data.put("ext", options.extField);
        data.put("style", styleToInt(options.style));
        return data;
    }

    private static EMGroupManager.EMGroupStyle styleFromInt(int style) {
        switch (style) {
            case 0:
                return EMGroupManager.EMGroupStyle.EMGroupStylePrivateOnlyOwnerInvite;
            case 1:
                return EMGroupManager.EMGroupStyle.EMGroupStylePrivateMemberCanInvite;
            case 2:
                return EMGroupManager.EMGroupStyle.EMGroupStylePublicJoinNeedApproval;
            case 3:
                return EMGroupManager.EMGroupStyle.EMGroupStylePublicOpenJoin;
        }

        return EMGroupManager.EMGroupStyle.EMGroupStylePrivateOnlyOwnerInvite;
    }

    private static int styleToInt(EMGroupManager.EMGroupStyle style) {
        switch (style) {
            case EMGroupStylePrivateOnlyOwnerInvite:
                return 0;
            case EMGroupStylePrivateMemberCanInvite:
                return 1;
            case EMGroupStylePublicJoinNeedApproval:
                return 2;
            case EMGroupStylePublicOpenJoin:
                return 3;
        }

        return 0;
    }
}
