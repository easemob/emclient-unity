package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMPresenceListener;
import com.hyphenate.chat.EMPresence;
import com.hyphenate.unity_chat_sdk.helper.EMPresenceHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Iterator;
import java.util.List;
import java.util.Map;

import util.EMSDKMethod;

public class EMUnityPresenceManagerListener implements EMPresenceListener {
    @Override
    public void onPresenceUpdated(List<EMPresence> list)  {
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMPresence presence : list) {
                JSONObject jsonObject  = EMPresenceHelper.toJson(presence);
                jsonArray.put(jsonObject);
            }
        }catch (JSONException ignored) {}
        Log.d("unity_sdk","onPresenceUpdated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnPresenceUpdated", jsonArray.toString());
    }
}
