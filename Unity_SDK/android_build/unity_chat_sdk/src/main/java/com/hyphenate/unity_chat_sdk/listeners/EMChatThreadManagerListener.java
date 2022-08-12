package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.unity_chat_sdk.helper.EMChatThreadEventHelper;
import com.unity3d.player.UnityPlayer;


import org.json.JSONException;

import util.EMSDKMethod;


public class EMChatThreadManagerListener implements EMChatThreadChangeListener {
    @Override
    public void onChatThreadCreated(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadCreated");
        try {
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatThreadListener_Obj, "OnChatThreadCreate", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
        }catch (JSONException ignored){}
    }

    @Override
    public void onChatThreadUpdated(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadUpdated");
        try {
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatThreadListener_Obj, "OnChatThreadUpdate", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
        }catch (JSONException ignored){}
    }

    @Override
    public void onChatThreadDestroyed(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadDestroyed");
        try {
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatThreadListener_Obj, "OnChatThreadDestroy", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
        }catch (JSONException ignored){}
    }

    @Override
    public void onChatThreadUserRemoved(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadUserRemoved");
        try {
            UnityPlayer.UnitySendMessage(EMSDKMethod.ChatThreadListener_Obj, "OnUserKickOutOfChatThread", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
        }catch (JSONException ignored){}
    }
}