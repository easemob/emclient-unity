package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMConversationListener;
import com.hyphenate.EMMessageListener;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReactionChange;
import com.hyphenate.unity_chat_sdk.helper.EMGroupReadAckHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageReactionChangeHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Iterator;
import java.util.List;
import java.util.Map;

import util.EMSDKMethod;

public class EMUnityChatManagerListener implements EMMessageListener, EMConversationListener {
    @Override
    public void onMessageReceived(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageReceived");
        JSONArray ja = new JSONArray();
        try {
            for (EMMessage msg : list) {
                ja.put(EMMessageHelper.toJson(msg).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnMessageReceived", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onCmdMessageReceived(List<EMMessage> list) {
        Log.d("unity_sdk","onCmdMessageReceived");
        JSONArray ja = new JSONArray();
        try {
            for (EMMessage msg : list) {
                ja.put(EMMessageHelper.toJson(msg).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnCmdMessageReceived", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onMessageRead(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageRead");
        JSONArray ja = new JSONArray();
        try {
            for (EMMessage msg : list) {
                ja.put(EMMessageHelper.toJson(msg).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnMessageRead", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onGroupMessageRead(List<EMGroupReadAck> list) {
        Log.d("unity_sdk","onGroupMessageRead");
        JSONArray ja = new JSONArray();
        try {
            for (EMGroupReadAck ack : list) {
                ja.put(EMGroupReadAckHelper.toJson(ack).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnGroupMessageRead", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onReadAckForGroupMessageUpdated() {
        Log.d("unity_sdk","onReadAckForGroupMessageUpdated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnReadAckForGroupMessageUpdated", null);
    }

    @Override
    public void onMessageDelivered(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageDelivered");
        JSONArray ja = new JSONArray();
        try {
            for (EMMessage msg : list) {
                ja.put(EMMessageHelper.toJson(msg).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnMessageDelivered", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onMessageRecalled(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageRecalled");
        JSONArray ja = new JSONArray();
        try {
            for (EMMessage msg : list) {
                ja.put(EMMessageHelper.toJson(msg).toString());
            }
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnMessageRecalled", ja.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onMessageChanged(EMMessage emMessage, Object o) {
//        Log.d("unity_sdk","onMessageChanged");
//        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnMessageChanged", "");
    }

    @Override
    public void onCoversationUpdate() {
        Log.d("unity_sdk","onConversationUpdate");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnConversationUpdate", "");
    }

    @Override
    public void onConversationRead(String s, String s1) {
        Log.d("unity_sdk","onConversationRead");
        JSONObject object = new JSONObject();
        try {
            object.put("from", s);
            object.put("to", s1);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnConversationRead", object.toString());
        } catch (JSONException jsonException) {
            jsonException.printStackTrace();
        }
    }

    @Override
    public void onReactionChanged(List<EMMessageReactionChange> list)  {
        Log.d("unity_sdk","onReactionChanged");
        JSONArray jsonArray = new JSONArray();
        for (EMMessageReactionChange change: list) {
            try{
                JSONObject jsonObject = EMMessageReactionChangeHelper.toJson(change);
                jsonArray.put(jsonObject);
            }catch (JSONException ignored) { }
        }
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "MessageReactionDidChange", jsonArray.toString());
    }
}
