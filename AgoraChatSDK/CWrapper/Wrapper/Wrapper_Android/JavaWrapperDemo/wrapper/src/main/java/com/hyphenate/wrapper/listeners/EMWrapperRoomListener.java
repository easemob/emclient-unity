package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMChatRoomChangeListener;
import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMChatRoomHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

public class EMWrapperRoomListener implements EMChatRoomChangeListener {
    @Override
    public void onWhiteListAdded(String chatRoomId, List<String> whitelist) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userIds", whitelist);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAddAllowListMembersFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListRemoved(String chatRoomId, List<String> whitelist) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userIds", whitelist);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onRemoveAllowListMembersFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAllMemberMuteStateChanged(String chatRoomId, boolean isMuted) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("isAllMuted", isMuted);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAllMemberMuteChangedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onChatRoomDestroyed(String roomId, String roomName) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", roomId);
            data.put("name", roomName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onDestroyedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberJoined(String roomId, String participant) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", roomId);
            data.put("userId", participant);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onMemberJoinedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberExited(String roomId, String roomName, String participant) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", roomId);
            data.put("name", roomName);
            data.put("userId", participant);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onMemberExitedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRemovedFromChatRoom(int reason, String roomId, String roomName, String participant) {
        JSONObject data = new JSONObject();

        try {
            data.put("roomId", roomId);
            data.put("name", roomName);
            data.put("userId", participant);
            if (reason == 0) {
                post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onRemovedFromRoom, data.toString()));
            }else if (reason == 2) {
                post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onRemoveFromRoomByOffline, data.toString()));
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListAdded(String chatRoomId, List<String> mutes, long expireTime) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userIds", mutes);
            data.put("expireTime", String.valueOf(expireTime));
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onMuteListAddedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListRemoved(String chatRoomId, List<String> mutes) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userIds", mutes);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onMuteListRemovedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminAdded(String chatRoomId, String admin) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userId", admin);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAdminAddedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminRemoved(String chatRoomId, String admin) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("userId", admin);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAdminRemovedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onOwnerChanged(String chatRoomId, String newOwner, String oldOwner) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("newOwner", newOwner);
            data.put("oldOwner", oldOwner);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onOwnerChangedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAnnouncementChanged(String chatRoomId, String announcement) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("announcement", announcement);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAnnouncementChangedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSpecificationChanged(EMChatRoom room) {
        JSONObject data = new JSONObject();
        try {
            data.put("room", EMChatRoomHelper.toJson(room));
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onSpecificationChangedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAttributesUpdate(String chatRoomId, Map<String, String> attributeMap, String from) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("kv", attributeMap);
            data.put("userId", from);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAttributesChangedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAttributesRemoved(String chatRoomId, List<String> keyList, String from) {
        JSONObject data = new JSONObject();
        try {
            data.put("roomId", chatRoomId);
            data.put("list", keyList);
            data.put("userId", from);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAttributesRemovedFromRoom, data.toString()));
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
