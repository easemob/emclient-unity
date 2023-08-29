package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMGroupChangeListener;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMMessageReaction;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMGroupHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionHelper;
import com.hyphenate.wrapper.helper.EMMucSharedFileHelper;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

public class EMWrapperGroupListener implements EMGroupChangeListener {
    @Override
    public void onWhiteListAdded(String groupId, List<String> whitelist) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userIds", whitelist);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAddAllowListMembersFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onWhiteListRemoved(String groupId, List<String> whitelist) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userIds", whitelist);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onRemoveAllowListMembersFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAllMemberMuteStateChanged(String groupId, boolean isMuted) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("isMuteAll", isMuted);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAllMemberMuteChangedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onInvitationReceived(String groupId, String groupName, String inviter, String reason) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("name", groupName);
            data.put("userId", inviter);
            data.put("msg", reason);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onInvitationReceivedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinReceived(String groupId, String groupName, String applicant, String reason) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("name", groupName);
            data.put("userId", applicant);
            data.put("msg", reason);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onRequestToJoinReceivedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinAccepted(String groupId, String groupName, String accept) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("name", groupName);
            data.put("userId", accept);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onRequestToJoinAcceptedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onRequestToJoinDeclined(String groupId, String groupName, String decliner, String reason) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("msg", reason);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onRequestToJoinDeclinedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onInvitationAccepted(String groupId, String invitee, String reason) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", invitee);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onInvitationAcceptedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onInvitationDeclined(String groupId, String invitee, String reason) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", invitee);
            data.put("msg", reason);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onInvitationDeclinedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onUserRemoved(String groupId, String groupName) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("name", groupName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onUserRemovedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onGroupDestroyed(String groupId, String groupName) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("name", groupName);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onDestroyedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAutoAcceptInvitationFromGroup(String groupId, String inviter, String inviteMessage) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", inviter);
            data.put("msg", inviteMessage);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAutoAcceptInvitationFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListAdded(String groupId, List<String> mutes, long muteExpire) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userIds", mutes);
            data.put("expireTime", muteExpire);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onMuteListAddedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMuteListRemoved(String groupId, List<String> mutes) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userIds", mutes);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onMuteListRemovedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminAdded(String groupId, String administrator) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", administrator);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAdminAddedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAdminRemoved(String groupId, String administrator) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", administrator);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAdminRemovedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onOwnerChanged(String groupId, String newOwner, String oldOwner) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("newOwner", newOwner);
            data.put("oldOwner", oldOwner);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onOwnerChangedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberJoined(String groupId, String member) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", member);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onMemberJoinedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onMemberExited(String groupId, String member) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", member);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onMemberExitedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onAnnouncementChanged(String groupId, String announcement) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("announcement", announcement);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onAnnouncementChangedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSharedFileAdded(String groupId, EMMucSharedFile sharedFile) {
        try {
            JSONObject data = new JSONObject();
            /* test:
            JSONObject file = new JSONObject();
            file.put("fileId", "file.getFileId()");
            file.put("name", "file.getFileName()");
            file.put("owner", "file.getFileOwner()");
            file.put("createTime", Integer.valueOf(200000000));
            file.put("fileSize", Integer.valueOf(100000000));
            data.put("file", file);
            */

            data.put("groupId", groupId);
            data.put("file", EMMucSharedFileHelper.toJson(sharedFile));
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onSharedFileAddedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSharedFileDeleted(String groupId, String fileId) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("fileId", fileId);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onSharedFileDeletedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onSpecificationChanged(EMGroup group) {
        try {
            JSONObject data = new JSONObject();
            data.put("group", EMGroupHelper.toJson(group));
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onSpecificationChangedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onStateChanged(EMGroup group, boolean isDisabled) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", group.getGroupId());
            data.put("isDisabled", isDisabled);

            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener, EMSDKMethod.onStateChangedFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onGroupMemberAttributeChanged(String groupId, String userId, Map<String, String> attribute, String from) {
        try {
            JSONObject data = new JSONObject();
            data.put("groupId", groupId);
            data.put("userId", userId);
            data.put("from", from);
            JSONObject attr = EMHelper.stringMapToJsonObject(attribute);
            data.put("attrs", attr);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener, EMSDKMethod.onUpdateMemberAttributesFromGroup, data.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }



    // 测试用假数据
    public void onTestSharedFileAdded(String groupId, JSONObject data) {
        post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.groupListener,EMSDKMethod.onSharedFileAddedFromGroup, data.toString()));
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
