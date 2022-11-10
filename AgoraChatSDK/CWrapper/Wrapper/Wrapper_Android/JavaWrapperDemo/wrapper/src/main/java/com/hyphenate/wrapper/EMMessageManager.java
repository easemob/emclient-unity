package com.hyphenate.wrapper;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMChatThreadHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionHelper;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;


public class EMMessageManager extends EMBaseWrapper {
    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.getReactionList.equals(method)) {
            ret = reactionList(jsonObject, callback);
        }else if (EMSDKMethod.groupAckCount.equals(method)){
            ret = getAckCount(jsonObject, callback);
        }else if (EMSDKMethod.getChatThread.equals(method)) {
            ret = getChatThread(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String reactionList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        EMMessage msg = getMessageWithId(msgId);
        JSONArray jsonArray = new JSONArray();
        if (msg != null) {
            List<EMMessageReaction> reactions = msg.getMessageReaction();
            for (int i = 0; i < reactions.size(); i++) {
                jsonArray.put(EMMessageReactionHelper.toJson(reactions.get(i)));
            }
        }
        return EMHelper.getReturnJsonObject(jsonArray).toString();
    }


    private String getAckCount(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        EMMessage msg = getMessageWithId(msgId);
        return EMHelper.getReturnJsonObject(msg != null ? msg.groupAckCount() : 0).toString();
    }

    private String getChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        EMMessage msg = getMessageWithId(msgId);
        return EMHelper.getReturnJsonObject(EMChatThreadHelper.toJson(msg.getChatThread())).toString();
    }

    private EMMessage getMessageWithId(String msgId) {
        return EMClient.getInstance().chatManager().getMessage(msgId);
    }
}
