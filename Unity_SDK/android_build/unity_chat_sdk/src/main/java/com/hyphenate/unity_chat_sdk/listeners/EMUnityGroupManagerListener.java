package com.hyphenate.unity_chat_sdk.listeners;

import android.util.Log;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.EMGroupChangeListener;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.unity_chat_sdk.helper.EMMucSharedFileHelper;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

import util.EMSDKMethod;

public class EMUnityGroupManagerListener implements EMGroupChangeListener {
    @Override
    public void onInvitationReceived(String groupId, String groupName, String inviter, String reason) {
        Log.d("chat_sdk", "onInvitationReceived");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            jsonObject.put("inviter", inviter);
            jsonObject.put("reason", reason);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnInvitationReceived", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinReceived(String groupId, String groupName, String applicant, String reason) {
        Log.d("chat_sdk", "onRequestToJoinReceived");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            jsonObject.put("applicant", applicant);
            jsonObject.put("reason", reason);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnRequestToJoinReceived", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinAccepted(String groupId, String groupName, String accepter) {
        Log.d("chat_sdk", "onRequestToJoinAccepted");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            jsonObject.put("accepter", accepter);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnRequestToJoinAccepted", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinDeclined(String groupId, String groupName, String decliner, String reason) {
        Log.d("chat_sdk", "onRequestToJoinDeclined");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            jsonObject.put("decliner", decliner);
            jsonObject.put("reason", reason);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnRequestToJoinDeclined", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onInvitationAccepted(String groupId, String invitee, String reason) {
        Log.d("chat_sdk", "onInvitationAccepted");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("invitee", invitee);
            jsonObject.put("reason", reason);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnInvitationAccepted", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onInvitationDeclined(String groupId, String invitee, String reason) {
        Log.d("chat_sdk", "onInvitationDeclined");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("invitee", invitee);
            jsonObject.put("reason", reason);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnInvitationDeclined", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onUserRemoved(String groupId, String groupName) {
        Log.d("chat_sdk", "onUserRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnUserRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onGroupDestroyed(String groupId, String groupName) {
        Log.d("chat_sdk", "onGroupDestroyed");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("groupName", groupName);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnGroupDestroyed", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAutoAcceptInvitationFromGroup(String groupId, String inviter, String inviteMessage) {
        Log.d("chat_sdk", "onAutoAcceptInvitationFromGroup");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("inviter", inviter);
            jsonObject.put("inviteMessage", inviteMessage);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAutoAcceptInvitationFromGroup", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListAdded(String groupId, List<String> mutes, long muteExpire) {
        Log.d("chat_sdk", "onMuteListAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("list", EMTransformHelper.jsonArrayFromStringList(mutes).toString());
            jsonObject.put("muteExpire", muteExpire);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnMuteListAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListRemoved(String groupId, List<String> mutes) {
        Log.d("chat_sdk", "onMuteListRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("list", EMTransformHelper.jsonArrayFromStringList(mutes).toString());
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnMuteListRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListAdded(String groupId, List<String> whitelist) {
        Log.d("chat_sdk", "onWhiteListAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("list", EMTransformHelper.jsonArrayFromStringList(whitelist).toString());
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAddWhiteListMembersFromGroup", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListRemoved(String groupId, List<String> whitelist) {
        Log.d("chat_sdk", "onWhiteListRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("list", EMTransformHelper.jsonArrayFromStringList(whitelist).toString());
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnRemoveWhiteListMembersFromGroup", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAllMemberMuteStateChanged(String groupId, boolean isMuted) {
        Log.d("chat_sdk", "onAllMemberMuteStateChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("muted", isMuted);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAllMemberMuteChangedFromGroup", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminAdded(String groupId, String admin) {
        Log.d("chat_sdk", "onAdminAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("admin", admin);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAdminAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminRemoved(String groupId, String administrator) {
        Log.d("chat_sdk", "onAdminRemoved");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("admin", administrator);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAdminRemoved", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onOwnerChanged(String groupId, String newOwner, String oldOwner) {
        Log.d("chat_sdk", "onOwnerChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("newOwner", newOwner);
            jsonObject.put("oldOwner", oldOwner);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnOwnerChanged", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberJoined(String groupId, String member) {
        Log.d("chat_sdk", "onMemberJoined");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("member", member);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnMemberJoined", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberExited(String groupId, String member) {
        Log.d("chat_sdk", "onMemberExited");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("member", member);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnMemberExited", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAnnouncementChanged(String groupId, String announcement) {
        Log.d("chat_sdk", "onAnnouncementChanged");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("announcement", announcement);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnAnnouncementChanged", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSharedFileAdded(String groupId, EMMucSharedFile sharedFile) {
        Log.d("chat_sdk", "onSharedFileAdded");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("sharedFile", EMMucSharedFileHelper.toJson(sharedFile).toString());
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnSharedFileAdded", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSharedFileDeleted(String groupId, String fileId) {
        Log.d("chat_sdk", "onSharedFileDeleted");
        try {
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("groupId", groupId);
            jsonObject.put("fileId", fileId);
            UnityPlayer.UnitySendMessage(EMSDKMethod.GroupListener_Obj, "OnSharedFileDeleted", jsonObject.toString());
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }
}
