package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMChatRoomChangeListener;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

import util.EMSDKMethod;

public class EMUnityRoomManagerListener implements EMChatRoomChangeListener {
    @Override
    public void onChatRoomDestroyed(String roomId, String roomName) {
        Log.d("chat_sdk", "onChatRoomDestroyed");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("roomName", roomName);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnChatRoomDestroyed", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberJoined(String roomId, String participant) {
        Log.d("chat_sdk", "onMemberJoined");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("participant", participant);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnMemberJoined", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberExited(String roomId, String roomName, String participant) {
        Log.d("chat_sdk", "onMemberExited");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("roomName", roomName);
            jsonObject.put("participant", participant);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnMemberExited", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRemovedFromChatRoom(int reason, String roomId, String roomName, String participant) {
        Log.d("chat_sdk", "onRemovedFromChatRoom");
        try {
            JSONObject jsonObject = new JSONObject();
//            jsonObject.put("reason", reason);
            jsonObject.put("roomId", roomId);
            jsonObject.put("roomName", roomName);
            jsonObject.put("participant", participant);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnRemovedFromChatRoom", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListAdded(String roomId, List<String> mutes, long expireTime) {
        Log.d("chat_sdk", "onMuteListAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("list", EMTransformHelper.stringListToString(mutes));
            jsonObject.put("expireTime", expireTime);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnMuteListAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListRemoved(String roomId, List<String> mutes) {
        Log.d("chat_sdk", "onMuteListRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("list", EMTransformHelper.stringListToString(mutes));
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnMuteListRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListAdded(String roomId, List<String> list) {
        Log.d("chat_sdk", "onWhiteListAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("list", EMTransformHelper.stringListToString(list));
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnWhiteListAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListRemoved(String roomId, List<String> list) {
        Log.d("chat_sdk", "onWhiteListRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("list", EMTransformHelper.stringListToString(list));
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnWhiteListRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAllMemberMuteStateChanged(String roomId, boolean isMuted) {
        Log.d("chat_sdk", "onAllMemberMuteStateChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("isMuted", isMuted);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnAllMemberMuteStateChanged", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminAdded(String roomId, String admin) {
        Log.d("chat_sdk", "onAdminAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("admin", admin);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnAdminAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminRemoved(String roomId, String admin) {
        Log.d("chat_sdk", "onAdminRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("admin", admin);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnAdminRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onOwnerChanged(String roomId, String newOwner, String oldOwner) {
        Log.d("chat_sdk", "onOwnerChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("newOwner", newOwner);
            jsonObject.put("oldOwner", oldOwner);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnOwnerChanged", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAnnouncementChanged(String roomId, String announcement) {
        Log.d("chat_sdk", "onAnnouncementChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("roomId", roomId);
            jsonObject.put("announcement", announcement);
            UnityPlayer.UnitySendMessage(EMSDKMethod.RoomListener_Obj, "OnAnnouncementChanged", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }
}
