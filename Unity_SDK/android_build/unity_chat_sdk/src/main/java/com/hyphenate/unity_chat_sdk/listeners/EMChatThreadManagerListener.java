package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.unity_chat_sdk.helper.EMChatThreadEventHelper;
import com.unity3d.player.UnityPlayer;


import util.EMSDKMethod;


public class EMChatThreadManagerListener implements EMChatThreadChangeListener {
    @Override
    public void onChatThreadCreated(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadCreated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnChatThreadCreate", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
    }

    @Override
    public void onChatThreadUpdated(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadUpdated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnChatThreadUpdate", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
    }

    @Override
    public void onChatThreadDestroyed(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadDestroyed");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnChatThreadDestroy", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
    }

    @Override
    public void onChatThreadUserRemoved(EMChatThreadEvent emChatThreadEvent) {
        Log.d("unity_sdk","onChatThreadUserRemoved");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnUserKickOutOfChatThread", EMChatThreadEventHelper.toJson(emChatThreadEvent).toString());
    }
}