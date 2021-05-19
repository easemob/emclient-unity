package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMContactListener;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import util.EMSDKMethod;

public class EMUnityContactListener implements EMContactListener {
    @Override
    public void onContactAdded(String s) {
        Log.d("chat_sdk", "onContactAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("username", s);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ContactListener_Obj, "OnContactAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
            Log.d("chat_sdk", "onContactAdded");
        }
    }

    @Override
    public void onContactDeleted(String s) {
        Log.d("chat_sdk", "onContactDeleted");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("username", s);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ContactListener_Obj, "OnContactDeleted", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
            Log.d("chat_sdk", "onContactAdded");
        }
    }

    @Override
    public void onContactInvited(String s, String s1) {
        Log.d("chat_sdk", "onContactInvited");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("username", s);
            jsonObject.put("reason", s1);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ContactListener_Obj, "OnContactInvited", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onFriendRequestAccepted(String s) {
        Log.d("chat_sdk", "onFriendRequestAccepted");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("username", s);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ContactListener_Obj, "OnFriendRequestAccepted", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onFriendRequestDeclined(String s) {
        Log.d("chat_sdk", "onFriendRequestDeclined");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("username", s);
            UnityPlayer.UnitySendMessage(EMSDKMethod.ContactListener_Obj, "OnFriendRequestDeclined", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }
}
