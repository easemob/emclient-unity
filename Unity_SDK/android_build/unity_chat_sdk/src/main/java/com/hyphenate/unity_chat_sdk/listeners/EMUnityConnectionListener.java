package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.util.EMLog;
import com.unity3d.player.UnityPlayer;

import util.EMSDKMethod;

public class EMUnityConnectionListener implements EMConnectionListener {

    @Override
    public void onConnected() {
        Log.d("unity_sdk","连接成功");
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnConnected", "");
    }

    @Override
    public void onDisconnected(int i)
    {
        Log.e("unity_sdk","连接断开 " + i);
        UnityPlayer.UnitySendMessage(EMSDKMethod.Connection_Obj, "OnDisconnected", String.valueOf(i));
    }
}
