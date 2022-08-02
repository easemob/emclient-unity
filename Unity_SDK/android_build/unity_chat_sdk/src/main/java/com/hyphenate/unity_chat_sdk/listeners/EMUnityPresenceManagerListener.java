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
        for (EMPresence presence : list) {
            Map<String, Object> map = EMPresenceHelper.toJson(presence);
            Iterator it = map.keySet().iterator();
            JSONObject jsonObject = new JSONObject();
            while (it.hasNext()) {
                String key = (String) it.next();
                try {
                    jsonObject.put(key , map.get(key));
                }catch (JSONException ignored) {}
            }
            if (jsonObject.length() > 0) {
                jsonArray.put(jsonObject);
            }
        }
        Log.d("unity_sdk","onPresenceUpdated");
        UnityPlayer.UnitySendMessage(EMSDKMethod.ChatListener_Obj, "OnPresenceUpdated", jsonArray.toString());
    }
}
