package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMPresence;
import com.hyphenate.unity_chat_sdk.helper.EMPresenceHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityChatManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityPresenceManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMPresenceManagerWrapper extends EMWrapper {
    static public EMPresenceManagerWrapper wrapper() {
        return new EMPresenceManagerWrapper();
    }


    EMUnityPresenceManagerListener listener;

    public EMPresenceManagerWrapper() {
        listener = new EMUnityPresenceManagerListener();
        EMClient.getInstance().presenceManager().addListener(listener);
    }

    private void publishPresence(String desc, String callbackId) {
        EMClient.getInstance().presenceManager().publishPresence(desc, new EMUnityCallback(callbackId));
    }

    private void subscribePresences(String jsonString, long expiry, String callbackId) throws JSONException {

        JSONArray jsonArray = new JSONArray(jsonString);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jsonArray.length(); i++) {
            list.add(jsonArray.getString(i));
        }

        EMClient.getInstance().presenceManager().subscribePresences(list, expiry, new EMUnityValueCallback<List<EMPresence>>("List<Presence>", callbackId){
            @Override
            public void onSuccess(List<EMPresence> emPresences) {
                JSONArray jsonArray1 = new JSONArray();
                for (EMPresence presence: emPresences) {
                    JSONObject jsonObject = new JSONObject();
                    Map<String, Object> map = EMPresenceHelper.toJson(presence);
                    Iterator<String> it = map.keySet().iterator();
                    while (it.hasNext()) {
                        String key = (String) it.next();
                        try {
                            jsonObject.put(key, map.get(key));
                        } catch (JSONException ignored) {

                        }
                    }
                    if (jsonObject.length() > 0) {
                        jsonArray1.put(jsonObject);
                    }
                }
               sendJsonObjectToUnity(jsonArray1.toString());
            }
        });
    }

    private void unsubscribePresences(String jsonString, String callbackId) throws JSONException {
        JSONArray jsonArray = new JSONArray(jsonString);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jsonArray.length(); i++) {
            list.add(jsonArray.getString(i));
        }

        EMClient.getInstance().presenceManager().unsubscribePresences(list, new EMUnityCallback(callbackId));
    }

    private void fetchSubscribedMembers(int pageNum, int pageSize, String callbackId) {
        EMClient.getInstance().presenceManager().fetchSubscribedMembers(pageNum, pageNum, new EMUnityValueCallback<List<String>>("List<String>", callbackId));
    }

    private void fetchPresenceStatus(String jsonString, String callbackId) throws JSONException {
        JSONArray jsonArray = new JSONArray(jsonString);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jsonArray.length(); i++) {
            list.add(jsonArray.getString(i));
        }
        EMClient.getInstance().presenceManager().fetchPresenceStatus(list, new EMUnityValueCallback<List<EMPresence>>("List<Presence>", callbackId) {
            @Override
            public void onSuccess(List<EMPresence> emPresences) {
                JSONArray jsonArray1 = new JSONArray();
                for (EMPresence presence: emPresences) {
                    JSONObject jsonObject = new JSONObject();
                    Map<String, Object> map = EMPresenceHelper.toJson(presence);
                    Iterator<String> it = map.keySet().iterator();
                    while (it.hasNext()) {
                        String key = (String) it.next();
                        try {
                            jsonObject.put(key, map.get(key));
                        } catch (JSONException ignored) {

                        }
                    }
                    if (jsonObject.length() > 0) {
                        jsonArray1.put(jsonObject);
                    }
                }
                sendJsonObjectToUnity(jsonArray1.toString());
            }
        });
    }
}
