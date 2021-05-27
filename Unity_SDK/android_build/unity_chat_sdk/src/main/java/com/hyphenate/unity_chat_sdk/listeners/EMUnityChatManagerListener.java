package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMConversationListener;
import com.hyphenate.EMMessageListener;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMMessage;
import com.unity3d.player.UnityPlayer;

import java.util.List;

import util.EMSDKMethod;

public class EMUnityChatManagerListener implements EMMessageListener, EMConversationListener {
    @Override
    public void onMessageReceived(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageReceived");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnMessageReceived", "");
    }

    @Override
    public void onCmdMessageReceived(List<EMMessage> list) {
        Log.d("unity_sdk","onCmdMessageReceived");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnCmdMessageReceived", "");
    }

    @Override
    public void onMessageRead(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageRead");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnMessageRead", "");
    }

    @Override
    public void onGroupMessageRead(List<EMGroupReadAck> list) {
        Log.d("unity_sdk","onGroupMessageRead");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnGroupMessageRead", "");
    }

    @Override
    public void onReadAckForGroupMessageUpdated() {
        Log.d("unity_sdk","onReadAckForGroupMessageUpdated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnReadAckForGroupMessageUpdated", "");
    }

    @Override
    public void onMessageDelivered(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageDelivered");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnMessageDelivered", "");
    }

    @Override
    public void onMessageRecalled(List<EMMessage> list) {
        Log.d("unity_sdk","onMessageRecalled");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnMessageRecalled", "");
    }

    @Override
    public void onMessageChanged(EMMessage emMessage, Object o) {
        Log.d("unity_sdk","onMessageChanged");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnMessageChanged", "");
    }

    @Override
    public void onCoversationUpdate() {
        Log.d("unity_sdk","onConversationUpdate");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnConversationUpdate", "");
    }

    @Override
    public void onConversationRead(String s, String s1) {
        Log.d("unity_sdk","onConversationRead");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnConversationRead", "");
    }
}
