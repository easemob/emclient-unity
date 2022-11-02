package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMGroupChangeListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMGroupOptions;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMHelper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMGroupHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMGroupOptionsHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMMucSharedFileHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMGroupManagerWrapper extends EMBaseWrapper{
    EMGroupManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.getGroupWithId.equals(method)) {
            ret = getGroupWithId(jsonObject);
        } else if (EMSDKMethod.getJoinedGroups.equals(method)) {
            ret = getJoinedGroups();
        } else if (EMSDKMethod.getGroupsWithoutPushNotification.equals(method)) {
            ret = getGroupsWithoutPushNotification(callback);
        } else if (EMSDKMethod.getJoinedGroupsFromServer.equals(method)) {
            ret = getJoinedGroupsFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getPublicGroupsFromServer.equals(method)) {
            ret = getPublicGroupsFromServer(jsonObject, callback);
        } else if (EMSDKMethod.createGroup.equals(method)) {
            ret = createGroup(jsonObject, callback);
        } else if (EMSDKMethod.getGroupSpecificationFromServer.equals(method)) {
            ret = getGroupSpecificationFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupMemberListFromServer.equals(method)) {
            ret = getGroupMemberListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupMuteListFromServer.equals(method)) {
            ret = getGroupMuteListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupWhiteListFromServer.equals(method)) {
            ret = getGroupWhiteListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.isMemberInWhiteListFromServer.equals(method)) {
            ret = isMemberInWhiteListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupFileListFromServer.equals(method)) {
            ret = getGroupFileListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupAnnouncementFromServer.equals(method)) {
            ret = getGroupAnnouncementFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getGroupBlockListFromServer.equals(method)) {
            ret = getGroupBlockListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.addMembers.equals(method)) {
            ret = addMembers(jsonObject, callback);
        } else if (EMSDKMethod.inviterUser.equals(method)){
            ret = inviterUser(jsonObject, callback);
        } else if (EMSDKMethod.removeMembers.equals(method)) {
            ret = removeMembers(jsonObject, callback);
        } else if (EMSDKMethod.blockMembers.equals(method)) {
            ret = blockMembers(jsonObject, callback);
        } else if (EMSDKMethod.unblockMembers.equals(method)) {
            ret = unblockMembers(jsonObject, callback);
        } else if (EMSDKMethod.updateGroupSubject.equals(method)) {
            ret = updateGroupSubject(jsonObject, callback);
        } else if (EMSDKMethod.updateDescription.equals(method)) {
            ret = updateDescription(jsonObject, callback);
        } else if (EMSDKMethod.leaveGroup.equals(method)) {
            ret = leaveGroup(jsonObject, callback);
        } else if (EMSDKMethod.destroyGroup.equals(method)) {
            ret = destroyGroup(jsonObject, callback);
        } else if (EMSDKMethod.blockGroup.equals(method)) {
            ret = blockGroup(jsonObject, callback);
        } else if (EMSDKMethod.unblockGroup.equals(method)) {
            ret = unblockGroup(jsonObject, callback);
        } else if (EMSDKMethod.updateGroupOwner.equals(method)) {
            ret = updateGroupOwner(jsonObject, callback);
        } else if (EMSDKMethod.addAdmin.equals(method)) {
            ret = addAdmin(jsonObject, callback);
        } else if (EMSDKMethod.removeAdmin.equals(method)) {
            ret = removeAdmin(jsonObject, callback);
        } else if (EMSDKMethod.muteMembers.equals(method)) {
            ret = muteMembers(jsonObject, callback);
        } else if (EMSDKMethod.unMuteMembers.equals(method)) {
            ret = unMuteMembers(jsonObject, callback);
        } else if (EMSDKMethod.muteAllMembers.equals(method)) {
            ret = muteAllMembers(jsonObject, callback);
        } else if (EMSDKMethod.unMuteAllMembers.equals(method)) {
            ret = unMuteAllMembers(jsonObject, callback);
        } else if (EMSDKMethod.addWhiteList.equals(method)) {
            ret = addWhiteList(jsonObject, callback);
        } else if (EMSDKMethod.removeWhiteList.equals(method)) {
            ret = removeWhiteList(jsonObject, callback);
        } else if (EMSDKMethod.uploadGroupSharedFile.equals(method)) {
            ret = uploadGroupSharedFile(jsonObject, callback);
        } else if (EMSDKMethod.downloadGroupSharedFile.equals(method)) {
            ret = downloadGroupSharedFile(jsonObject, callback);
        } else if (EMSDKMethod.removeGroupSharedFile.equals(method)) {
            ret = removeGroupSharedFile(jsonObject, callback);
        } else if (EMSDKMethod.updateGroupAnnouncement.equals(method)) {
            ret = updateGroupAnnouncement(jsonObject, callback);
        } else if (EMSDKMethod.updateGroupExt.equals(method)) {
            ret = updateGroupExt(jsonObject, callback);
        } else if (EMSDKMethod.joinPublicGroup.equals(method)) {
            ret = joinPublicGroup(jsonObject, callback);
        } else if (EMSDKMethod.requestToJoinPublicGroup.equals(method)) {
            ret = requestToJoinPublicGroup(jsonObject, callback);
        } else if (EMSDKMethod.acceptJoinApplication.equals(method)) {
            ret = acceptJoinApplication(jsonObject, callback);
        } else if (EMSDKMethod.declineJoinApplication.equals(method)) {
            ret = declineJoinApplication(jsonObject, callback);
        } else if (EMSDKMethod.acceptInvitationFromGroup.equals(method)) {
            ret = acceptInvitationFromGroup(jsonObject, callback);
        } else if (EMSDKMethod.declineInvitationFromGroup.equals(method)) {
            ret = declineInvitationFromGroup(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String getGroupWithId(JSONObject params) throws JSONException {
        String groupId = params.getString("groupId");
        EMGroup group = EMClient.getInstance().groupManager().getGroup(groupId);
        return EMHelper.getReturnJsonObject(EMGroupHelper.toJson(group)).toString();
    }

    private String getJoinedGroups()  {
        EMClient.getInstance().groupManager().loadAllGroups();
        List<EMGroup> groups = EMClient.getInstance().groupManager().getAllGroups();
        JSONObject jo = null;
        try {
            JSONArray jsonArray = new JSONArray();
            for (EMGroup group : groups) {
                jsonArray.put(EMGroupHelper.toJson(group));
            }
            jo = EMHelper.getReturnJsonObject(jsonArray);
        }catch (JSONException e) {
            e.printStackTrace();
        } finally {
            if (jo == null) {
                return null;
            }else {
                return jo.toString();
            }
        }
    }

    private String getGroupsWithoutPushNotification(EMWrapperCallback callback) {

        asyncRunnable(() -> {
            List<String> groups = EMClient.getInstance().pushManager().getNoPushGroups();
            onSuccess(EMHelper.stringListToJsonArray(groups), callback);
        });
        return null;
    }

    private String getJoinedGroupsFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {

        int pageSize = 0;
        if (params.has("pageSize")){
            pageSize = params.getInt("pageSize");
        }
        int pageNum = 0;
        if (params.has("pageNum")){
            pageNum = params.getInt("pageNum");
        }

        boolean needMemberCount = false;
        if (params.has("needMemberCount")) {
            needMemberCount = params.getBoolean("needMemberCount");
        }

        boolean needRole = false;
        if (params.has("needRole")) {
            needRole = params.getBoolean("needRole");
        }

        EMCommonValueCallback<List<EMGroup>> callBack = new EMCommonValueCallback<List<EMGroup>>(callback) {
            @Override
            public void onSuccess(List<EMGroup> object) {
                JSONArray arrayList = new JSONArray();
                try {
                    for (EMGroup group : object) {
                        arrayList.put(EMGroupHelper.toJson(group));
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(arrayList);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncGetJoinedGroupsFromServer(pageNum, pageSize, needMemberCount, needRole,callBack);
        return null;
    }

    private String getPublicGroupsFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = 0;
        if (params.has("pageSize")){
            pageSize = params.getInt("pageSize");
        }
        String cursor = null;
        if (params.has("cursor")){
            cursor = params.getString("cursor");
        }
        EMCommonValueCallback<EMCursorResult<EMGroupInfo>> callBack = new EMCommonValueCallback<EMCursorResult<EMGroupInfo>>(
               callback) {
            @Override
            public void onSuccess(EMCursorResult<EMGroupInfo> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncGetPublicGroupsFromServer(pageSize, cursor, callBack);
        return null;
    }

    private String createGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupName = null;

        if (params.has("groupName")){
            groupName = params.getString("groupName");
        }

        String desc = null;
        if(params.has("desc")){
            desc = params.getString("desc");
        }

        String[] members = null;
        if(params.has("inviteMembers")){
            JSONArray inviteMembers = params.getJSONArray("inviteMembers");
            members = new String[inviteMembers.length()];
            for (int i = 0; i < inviteMembers.length(); i++) {
                members[i] = inviteMembers.getString(i);
            }
        }
        if (members == null) {
            members = new String[0];
        }
        String inviteReason = null;

        if (params.has("inviteReason")){
            inviteReason = params.getString("inviteReason");
        }

        EMGroupOptions options = null;
        if (params.has("options")) {
            options = EMGroupOptionsHelper.fromJson(params.getJSONObject("options"));
        }

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jo = null;
                try {
                    jo = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncCreateGroup(groupName, desc, members, inviteReason, options,
                callBack);
        return null;
    }

    private String getGroupSpecificationFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String groupId = params.getString("groupId");
        boolean fetchMembers = params.getBoolean("fetchMembers");
        asyncRunnable(() -> {
            try {
                EMGroup group = EMClient.getInstance().groupManager().getGroupFromServer(groupId, fetchMembers);
                JSONObject jo = null;
                try {
                    jo = EMGroupHelper.toJson(group);
                }catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getGroupMemberListFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String groupId = params.getString("groupId");
        String cursor = null;
        if(params.has("cursor")){
            cursor = params.getString("cursor");
        }
        int pageSize = params.getInt("pageSize");

        EMCommonValueCallback<EMCursorResult<String>> callBack = new EMCommonValueCallback<EMCursorResult<String>>(
                callback) {
            @Override
            public void onSuccess(EMCursorResult<String> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncFetchGroupMembers(groupId, cursor, pageSize, callBack);
        return null;
    }

    private String getGroupBlockListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        int pageSize = 0;
        if (params.has("pageSize")){
            pageSize = params.getInt("pageSize");
        }
        int pageNum = 0;
        if (params.has("pageNum")){
            pageNum = params.getInt("pageNum");
        }

        EMClient.getInstance().groupManager().asyncGetBlockedUsers(groupId, pageNum, pageSize,
                new EMCommonValueCallback<List<String>>(callback) {
                    @Override
                    public void onSuccess(List<String> object) {
                        updateObject(EMHelper.stringListToJsonArray(object));
                    }
                });
        return null;
    }

    private String getGroupMuteListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        int pageSize = 0;
        if (params.has("pageSize")){
            pageSize = params.getInt("pageSize");
        }
        int pageNum = 0;
        if (params.has("pageNum")){
            pageNum = params.getInt("pageNum");
        }


        EMCommonValueCallback<Map<String, Long>> callBack = new EMCommonValueCallback<Map<String, Long>>(callback) {
            @Override
            public void onSuccess(Map<String, Long> object) {
                JSONObject jsonObject = null;
                try{
                    jsonObject = EMHelper.longMapToJsonObject(object);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncFetchGroupMuteList(groupId, pageNum, pageSize, callBack);
        return null;
    }

    private String getGroupWhiteListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().fetchGroupWhiteList(groupId,
                new EMCommonValueCallback<List<String>>(callback) {
                    @Override
                    public void onSuccess(List<String> object) {
                        updateObject(EMHelper.stringListToJsonArray(object));
                    }
                });
        return null;
    }

    private String isMemberInWhiteListFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().checkIfInGroupWhiteList(groupId,
                new EMCommonValueCallback<>(callback));
        return null;
    }

    private String getGroupFileListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        int pageNum = 0;
        if (params.has("pageNum")){
            pageNum = params.getInt("pageNum");
        }
        int pageSize = 0;
        if (params.has("pageSize")) {
            pageSize = params.getInt("pageSize");
        }

        EMCommonValueCallback<List<EMMucSharedFile>> callBack = new EMCommonValueCallback<List<EMMucSharedFile>>(
                callback) {
            @Override
            public void onSuccess(List<EMMucSharedFile> object) {
                JSONArray jsonArray = new JSONArray();
                try {
                    for (EMMucSharedFile file : object) {
                        jsonArray.put(EMMucSharedFileHelper.toJson(file));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonArray);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncFetchGroupSharedFileList(groupId, pageNum, pageSize, callBack);
        return null;
    }

    private String getGroupAnnouncementFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().asyncFetchGroupAnnouncement(groupId,
                new EMCommonValueCallback<>(callback));

        return null;
    }

    private String inviterUser(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String reason = null;
        if (params.has("reason")) {
            reason = params.getString("reason");
        }
        String[] members = null;
        if (params.has("members")){
            JSONArray array = params.getJSONArray("members");
            members = new String[array.length()];
            for (int i = 0; i < array.length(); i++) {
                members[i] = array.getString(i);
            }
        }
        if (members == null) {
            members = new String[0];
        }
        EMClient.getInstance().groupManager().asyncInviteUser(groupId, members, reason,
                new EMCommonCallback(callback));

        return null;
    }

    private String addMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String[] members = null;
        if (params.has("members")){
            JSONArray array = params.getJSONArray("members");
            members = new String[array.length()];
            for (int i = 0; i < array.length(); i++) {
                members[i] = array.getString(i);
            }
        }
        if (members == null) {
            members = new String[0];
        }

        String welcome = null;
        if (params.has("welcome")){
            welcome = params.getString("welcome");
        }
        String finalWelcome = welcome;
        String[] finalMembers = members;
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().groupManager().addUsersToGroup(groupId, finalMembers, finalWelcome);
                onSuccess(true, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });

        return null;
    }

    private String removeMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }

        EMClient.getInstance().groupManager().asyncRemoveUsersFromGroup(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String blockMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }

        EMClient.getInstance().groupManager().asyncBlockUsers(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String unblockMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }

        EMClient.getInstance().groupManager().asyncUnblockUsers(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String updateGroupSubject(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String name = "";
        if (params.has("name")){
            name = params.getString("name");
        }

        EMClient.getInstance().groupManager().asyncChangeGroupName(groupId, name, new EMCommonCallback(callback));

        return null;
    }

    private String updateDescription(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String desc = "";
        if (params.has("desc")){
            desc = params.getString("desc");
        }

        EMClient.getInstance().groupManager().asyncChangeGroupDescription(groupId, desc, new EMCommonCallback(callback));

        return null;
    }

    private String leaveGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().asyncLeaveGroup(groupId,
                new EMCommonCallback(callback));

        return null;
    }

    private String destroyGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().asyncDestroyGroup(groupId,
                new EMCommonCallback(callback));

        return null;
    }

    private String blockGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        EMClient.getInstance().groupManager().asyncBlockGroupMessage(groupId, new EMCommonCallback(callback));

        return null;
    }

    private String unblockGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        EMClient.getInstance().groupManager().asyncUnblockGroupMessage(groupId,  new EMCommonCallback(callback));

        return null;
    }

    private String updateGroupOwner(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String newOwner = params.getString("owner");

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncChangeOwner(groupId, newOwner, callBack);

        return null;
    }

    private String addAdmin(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String admin = params.getString("admin");

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncAddGroupAdmin(groupId, admin, callBack);

        return null;
    }

    private String removeAdmin(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String admin = params.getString("admin");

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncRemoveGroupAdmin(groupId, admin, callBack);

        return null;
    }

    private String muteMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        int duration = 0;
        if (params.has("duration")){
            duration = params.getInt("duration");
        }
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }
        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().aysncMuteGroupMembers(groupId, members, duration, callBack);

        return null;
    }

    private String unMuteMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }
        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncUnMuteGroupMembers(groupId, members, callBack);

        return null;
    }

    private String muteAllMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().muteAllMembers(groupId, callBack);

        return null;
    }

    private String unMuteAllMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().unmuteAllMembers(groupId, callBack);

        return null;
    }

    private String addWhiteList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }
        EMClient.getInstance().groupManager().addToGroupWhiteList(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String removeWhiteList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = new ArrayList<>();
        if (params.has("members")){
            JSONArray array = params.getJSONArray("members");
            for (int i = 0; i < array.length(); i++) {
                members.add(array.getString(i));
            }
        }
        EMClient.getInstance().groupManager().removeFromGroupWhiteList(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String uploadGroupSharedFile(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String filePath = null;
        if (params.has("filePath")){
            filePath = params.getString("filePath");
        }

        EMClient.getInstance().groupManager().asyncUploadGroupSharedFile(groupId, filePath,
                new EMCommonCallback(callback));

        return null;
    }

    private String downloadGroupSharedFile(JSONObject params, EMWrapperCallback callback) throws JSONException {
        // TODO: 下载文件更新状态
//        String groupId = params.getString("groupId");
//        String fileId = null;
//        if (params.has("fileId")) {
//            fileId = params.getString("fileId");
//        }
//        String savePath = null;
//        if (params.has("savePath")) {
//            savePath = params.getString("savePath");
//        }
//        EMClient.getInstance().groupManager().asyncDownloadGroupSharedFile(groupId, fileId, savePath,
//                new EMDownloadCallback(fileId, savePath));
//
//        post(()->{
//            onSuccess(result, channelName, true);
//        });
        return null;
    }

    private String removeGroupSharedFile(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String fileId = null;
        if (params.has("fileId")) {
            fileId = params.getString("fileId");
        }
        EMClient.getInstance().groupManager().asyncDeleteGroupSharedFile(groupId, fileId,
                new EMCommonCallback(callback));

        return null;
    }

    private String updateGroupAnnouncement(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String announcement = null;
        if (params.has("announcement")) {
            announcement = params.getString("announcement");
        }

        EMClient.getInstance().groupManager().asyncUpdateGroupAnnouncement(groupId, announcement, new EMCommonCallback(callback));

        return null;
    }

    private String updateGroupExt(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String ext = null;
        if (params.has("ext")) {
            ext = params.getString("ext");
        }

        String finalExt = ext;
        asyncRunnable(() -> {
            try {
                EMGroup group = EMClient.getInstance().groupManager().updateGroupExtension(groupId, finalExt);
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(group);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jsonObject, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });

        return null;
    }

    private String joinPublicGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {

        String groupId = params.getString("groupId");
        asyncRunnable(()->{
            try{
                EMGroup group = EMClient.getInstance().groupManager().getGroupFromServer(groupId);
                if (group.isMemberOnly()){
                    throw new HyphenateException(603,"User has no permission for this operation");
                }
                EMClient.getInstance().groupManager().joinGroup(groupId);
                onSuccess(null, callback);
            }catch (HyphenateException e){
                onError(e, callback);
            }
        });

        return null;
    }

    private String requestToJoinPublicGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String reason = null;
        if (params.has("reason")){
            reason = params.getString("reason");
        }

        EMClient.getInstance().groupManager().asyncApplyJoinToGroup(groupId, reason, new EMCommonCallback(callback));

        return null;
    }

    private String acceptJoinApplication(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String username = null;
        if (params.has("username")){
            username = params.getString("username");
        }

        EMClient.getInstance().groupManager().asyncAcceptApplication(username, groupId, new EMCommonCallback(callback));

        return null;
    }

    private String declineJoinApplication(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String username = null;
        if (params.has("username")){
            username = params.getString("username");
        }
        String reason = null;
        if (params.has("reason")){
            reason = params.getString("reason");
        }

        EMClient.getInstance().groupManager().asyncDeclineApplication(username, groupId, reason, new EMCommonCallback(callback));

        return null;
    }

    private String acceptInvitationFromGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String inviter = null;
        if (params.has("inviter")){
            inviter = params.getString("inviter");
        }
        EMCommonValueCallback<EMGroup> callBack = new EMCommonValueCallback<EMGroup>(callback) {
            @Override
            public void onSuccess(EMGroup object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMGroupHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().groupManager().asyncAcceptInvitation(groupId, inviter, callBack);

        return null;
    }

    private String declineInvitationFromGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String username = null;
        if (params.has("username")){
            username = params.getString("username");
        }
        String reason = null;
        if (params.has("reason")){
            reason = params.getString("reason");
        }

        EMClient.getInstance().groupManager().asyncDeclineInvitation(groupId, username, reason, new EMCommonCallback(callback));

        return null;
    }
    
    private void registerEaseListener(){
        EMGroupChangeListener groupChangeListener = new EMGroupChangeListener() {

            @Override
            public void onWhiteListAdded(String groupId, List<String> whitelist) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupWhiteListAdded");
                    data.put("groupId", groupId);
                    data.put("whitelist", whitelist);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onWhiteListRemoved(String groupId, List<String> whitelist) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupWhiteListRemoved");
                    data.put("groupId", groupId);
                    data.put("whitelist", whitelist);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAllMemberMuteStateChanged(String groupId, boolean isMuted) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupAllMemberMuteStateChanged");
                    data.put("groupId", groupId);
                    data.put("isMuted", isMuted);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onInvitationReceived(String groupId, String groupName, String inviter, String reason) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupInvitationReceived");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    data.put("inviter", inviter);
                    data.put("reason", reason);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onRequestToJoinReceived(String groupId, String groupName, String applicant, String reason) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupRequestToJoinReceived");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    data.put("applicant", applicant);
                    data.put("reason", reason);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onRequestToJoinAccepted(String groupId, String groupName, String accept) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupRequestToJoinAccepted");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    data.put("accept", accept);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onRequestToJoinDeclined(String groupId, String groupName, String decliner, String reason) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupRequestToJoinDeclined");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    data.put("decliner", decliner);
                    data.put("reason", reason);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onInvitationAccepted(String groupId, String invitee, String reason) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupInvitationAccepted");
                    data.put("groupId", groupId);
                    data.put("invitee", invitee);
                    data.put("reason", reason);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onInvitationDeclined(String groupId, String invitee, String reason) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupInvitationDeclined");
                    data.put("groupId", groupId);
                    data.put("invitee", invitee);
                    data.put("reason", reason);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onUserRemoved(String groupId, String groupName) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupUserRemoved");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onGroupDestroyed(String groupId, String groupName) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupGroupDestroyed");
                    data.put("groupId", groupId);
                    data.put("groupName", groupName);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAutoAcceptInvitationFromGroup(String groupId, String inviter, String inviteMessage) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupAutoAcceptInvitationFromGroup");
                    data.put("groupId", groupId);
                    data.put("inviter", inviter);
                    data.put("inviteMessage", inviteMessage);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMuteListAdded(String groupId, List<String> mutes, long muteExpire) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupMuteListAdded");
                    data.put("groupId", groupId);
                    data.put("mutes", mutes);
                    data.put("muteExpire", muteExpire);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMuteListRemoved(String groupId, List<String> mutes) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupMuteListRemoved");
                    data.put("groupId", groupId);
                    data.put("mutes", mutes);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAdminAdded(String groupId, String administrator) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupAdminAdded");
                    data.put("groupId", groupId);
                    data.put("administrator", administrator);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAdminRemoved(String groupId, String administrator) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupAdminRemoved");
                    data.put("groupId", groupId);
                    data.put("administrator", administrator);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onOwnerChanged(String groupId, String newOwner, String oldOwner) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupOwnerChanged");
                    data.put("groupId", groupId);
                    data.put("newOwner", newOwner);
                    data.put("oldOwner", oldOwner);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMemberJoined(String groupId, String member) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupMemberJoined");
                    data.put("groupId", groupId);
                    data.put("member", member);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMemberExited(String groupId, String member) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupMemberExited");
                    data.put("groupId", groupId);
                    data.put("member", member);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAnnouncementChanged(String groupId, String announcement) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupAnnouncementChanged");
                    data.put("groupId", groupId);
                    data.put("announcement", announcement);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onSharedFileAdded(String groupId, EMMucSharedFile sharedFile) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupSharedFileAdded");
                    data.put("groupId", groupId);
                    data.put("sharedFile", EMMucSharedFileHelper.toJson(sharedFile));
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onSharedFileDeleted(String groupId, String fileId) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupSharedFileDeleted");
                    data.put("groupId", groupId);
                    data.put("fileId", fileId);
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onSpecificationChanged(EMGroup group) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupSpecificationDidUpdate");
                    data.put("group", EMGroupHelper.toJson(group));
                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager,EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onStateChanged(EMGroup group, boolean isDisabled) {
                try {
                    JSONObject data = new JSONObject();
                    data.put("type", "groupStateChanged");
                    data.put("groupId", group.getGroupId());
                    data.put("isDisabled", isDisabled);

                    post(() -> JavaWrapper.listener.onReceive(EMSDKMethod.groupManager, EMSDKMethod.onGroupChanged, data.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().addGroupChangeListener(groupChangeListener);
    }
}
