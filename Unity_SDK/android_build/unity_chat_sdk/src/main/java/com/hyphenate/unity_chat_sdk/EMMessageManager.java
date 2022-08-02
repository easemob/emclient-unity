package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMChatThread;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;
import com.hyphenate.unity_chat_sdk.helper.EMChatThreadHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageReactionHelper;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMMessageManager extends EMWrapper {
    static public EMMessageManager wrapper() {
        return new EMMessageManager();
    }

    private int getGroupAckCount(String messageId) {
        EMMessage msg = getMessage(messageId);
        if (msg == null) return 0;
        return msg.groupAckCount();
    }

    private boolean getHasDeliverAck(String messageId) {
        EMMessage msg = getMessage(messageId);
        if (msg == null) return false;
        return msg.isDelivered();
    }

    private boolean getHasReadAck(String messageId) {
        EMMessage msg = getMessage(messageId);
        if (msg == null) return false;
        return msg.isAcked();
    }

    private String getReactionList(String messageId) {

        HashMap<String, ArrayList> ret = new HashMap<>();

        EMMessage msg = getMessage(messageId);
        ArrayList<Map<String, Object>> list = new ArrayList<>();
        if (msg != null) {
            List<EMMessageReaction> reactions = msg.getMessageReaction();
            for (int i = 0; i < reactions.size(); i++) {
                list.add(EMMessageReactionHelper.toJson(reactions.get(i)));
            }
        }
        ret.put("reactionList", list);
        return ret.toString();
    }

    private String getChatThread(String messageId) {
        String ret = null;
        EMMessage msg = getMessage(messageId);
        EMChatThread thread = msg.getChatThread();
        try{
            ret = EMChatThreadHelper.toJson(thread).toString();
        }catch (JSONException e) {

        }
        return ret;
    }

    private EMMessage getMessage(String messageId) {
        return EMClient.getInstance().chatManager().getMessage(messageId);
    }
}
