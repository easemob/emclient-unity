package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMConversationListener;
import com.hyphenate.EMMessageListener;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessagePinInfo;
import com.hyphenate.chat.EMMessageReactionChange;
import com.hyphenate.chat.EMRecallMessageInfo;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMGroupAckHelper;
import com.hyphenate.wrapper.helper.EMMessageHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionChangeHelper;
import com.hyphenate.wrapper.helper.EMRecallMessageInfoHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMWrapperMessageListener implements EMMessageListener , EMConversationListener {
    @Override
    public void onMessageReceived(List<EMMessage> messages) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg : messages) {
                jsonArray.put(EMMessageHelper.toJson(msg));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagesReceived, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onCmdMessageReceived(List<EMMessage> messages) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg : messages) {
                jsonArray.put(EMMessageHelper.toJson(msg));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onCmdMessagesReceived, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMessageRead(List<EMMessage> messages) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg : messages) {
                jsonArray.put(EMMessageHelper.toJson(msg));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagesRead, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMessageDelivered(List<EMMessage> messages) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg : messages) {
                jsonArray.put(EMMessageHelper.toJson(msg));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagesDelivered, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMessageRecalled(List<EMMessage> messages) {
        /*JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessage msg : messages) {
                jsonArray.put(EMMessageHelper.toJson(msg));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagesRecalled, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }*/
    }

    @Override
    public void onMessageRecalledWithExt(List<EMRecallMessageInfo> recallMessageInfo)
    {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMRecallMessageInfo info : recallMessageInfo) {
                jsonArray.put(EMRecallMessageInfoHelper.toJson(info));
            }
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagesRecalledByExt, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onReadAckForGroupMessageUpdated() {
        post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onReadAckForGroupMessageUpdated, null));
    }

    @Override
    public void onGroupMessageRead(List<EMGroupReadAck> groupReadAcks) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMGroupReadAck ack : groupReadAcks) {
                jsonArray.put(EMGroupAckHelper.toJson(ack));
            }
            post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onGroupMessageRead, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onReactionChanged(List<EMMessageReactionChange> messageReactionChangeList) {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMMessageReactionChange change : messageReactionChangeList) {
                jsonArray.put(EMMessageReactionChangeHelper.toJson(change));
            }
            post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessageReactionDidChange, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMessageContentChanged(com.hyphenate.chat.EMMessage messageModified, java.lang.String operatorId, long operationTime) {
        JSONObject jo = new JSONObject();
        try {
            jo.put("msg", messageModified.toString());
            jo.put("operatorId", operatorId);
            jo.put("operationTime", operationTime);
            post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessageContentChanged, jo.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMessagePinChanged(String messageId, String conversationId, EMMessagePinInfo.PinOperation pinOperation, EMMessagePinInfo pinInfo) {
        JSONObject jo = new JSONObject();
        try {
            jo.put("msgId", messageId);
            jo.put("convId", conversationId);
            jo.put("isPinned", pinOperation == EMMessagePinInfo.PinOperation.PIN);
            jo.put("operatorId", pinInfo.operatorId());
            jo.put("ts", pinInfo.pinTime());
            post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessagePinChanged, jo.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onConversationUpdate() {
        post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onConversationsUpdate, null));
    }

    @Override
    public void onConversationRead(String from, String to) {
        try{
            JSONObject jo = new JSONObject();
            jo.put("from", from);
            jo.put("to", to);
            post(()->EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onConversationRead, jo.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void onTestReactionChanged(JSONArray jsonArray) {
        post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onMessageReactionDidChange, jsonArray.toString()));
    }

    public void onTestGroupMessageRead(JSONArray jsonArray) {
        post(()-> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatListener, EMSDKMethod.onGroupMessageRead, jsonArray.toString()));
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
