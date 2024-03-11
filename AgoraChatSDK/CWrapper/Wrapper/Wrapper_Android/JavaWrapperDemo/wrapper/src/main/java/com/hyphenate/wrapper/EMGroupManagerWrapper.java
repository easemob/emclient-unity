package com.hyphenate.wrapper;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMGroupOptions;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.listeners.EMWrapperGroupListener;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.callback.EMCommonCallback;
import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.wrapper.helper.EMGroupHelper;
import com.hyphenate.wrapper.helper.EMGroupOptionsHelper;
import com.hyphenate.wrapper.helper.EMMucSharedFileHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMGroupManagerWrapper extends EMBaseWrapper{

    public EMWrapperGroupListener wrapperGroupListener;

    EMGroupManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.getGroupWithId.equals(method)) {
            ret = getGroupWithId(jsonObject);
        } else if (EMSDKMethod.getJoinedGroups.equals(method)) {
            ret = getJoinedGroups();
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
        } else if (EMSDKMethod.requestToJoinGroup.equals(method)) {
            ret = requestToJoinPublicGroup(jsonObject, callback);
        } else if (EMSDKMethod.acceptJoinApplication.equals(method)) {
            ret = acceptJoinApplication(jsonObject, callback);
        } else if (EMSDKMethod.declineJoinApplication.equals(method)) {
            ret = declineJoinApplication(jsonObject, callback);
        } else if (EMSDKMethod.acceptInvitationFromGroup.equals(method)) {
            ret = acceptInvitationFromGroup(jsonObject, callback);
        } else if (EMSDKMethod.declineInvitationFromGroup.equals(method)) {
            ret = declineInvitationFromGroup(jsonObject, callback);
        } else if (EMSDKMethod.getJoinedGroupsFromServerSimple.equals(method)) {
            ret = getJoinedGroupsFromServerSimple(jsonObject, callback);
        } else if (EMSDKMethod.setMemberAttributes.equals(method)) {
            ret = setMemberAttributes(jsonObject, callback);
        } else if (EMSDKMethod.fetchMemberAttributes.equals(method)) {
            ret = fetchMemberAttributes(jsonObject, callback);
        } else if (EMSDKMethod.fetchMyGroupsCount.equals(method)) {
            ret = fetchMyGroupsCount(callback);
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
        if (params.has("needAffiliations")) {
            needMemberCount = params.getBoolean("needAffiliations");
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

        if (params.has("name")){
            groupName = params.getString("name");
        }

        String desc = null;
        if(params.has("desc")){
            desc = params.getString("desc");
        }

        String[] members = null;
        if(params.has("userIds")){
            JSONArray inviteMembers = params.getJSONArray("userIds");
            members = new String[inviteMembers.length()];
            for (int i = 0; i < inviteMembers.length(); i++) {
                members[i] = inviteMembers.getString(i);
            }
        }
        if (members == null) {
            members = new String[0];
        }
        String inviteReason = null;

        if (params.has("msg")){
            inviteReason = params.getString("msg");
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
        boolean fetchMembers = false;
        if (params.has("fetchMembers")) {
            fetchMembers = params.getBoolean("fetchMembers");
        }
        boolean finalFetchMembers = fetchMembers;
        asyncRunnable(() -> {
            try {
                EMGroup group = EMClient.getInstance().groupManager().getGroupFromServer(groupId, finalFetchMembers);
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
        EMClient.getInstance().groupManager().checkIfInGroupWhiteList(groupId, new EMCommonValueCallback<Boolean>(callback){
            @Override
            public void onSuccess(Boolean object) {
                JSONObject jo = new JSONObject();
                try {
                    jo.put("ret", object);
                    super.updateObject(jo);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
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
        if (params.has("msg")) {
            reason = params.getString("msg");
        }
        String[] members = null;
        if (params.has("userIds")){
            JSONArray array = params.getJSONArray("userIds");
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
        if (params.has("userIds")){
            JSONArray array = params.getJSONArray("userIds");
            members = new String[array.length()];
            for (int i = 0; i < array.length(); i++) {
                members[i] = array.getString(i);
            }
        }
        if (members == null) {
            members = new String[0];
        }

        String welcome = null;
        if (params.has("msg")){
            welcome = params.getString("msg");
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
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
        }

        EMClient.getInstance().groupManager().asyncRemoveUsersFromGroup(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String blockMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
        }

        EMClient.getInstance().groupManager().asyncBlockUsers(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String unblockMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
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
        String newOwner = params.getString("userId");

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
        String admin = params.getString("userId");

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
        String admin = params.getString("userId");

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

        int duration = -1;
        if (params.has("expireTime")){
            duration = params.getInt("expireTime");
        }
        List<String> members = null;
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
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

        EMClient.getInstance().groupManager().asyncMuteGroupMembers(groupId, members, duration, callBack);

        return null;
    }

    private String unMuteMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = null;
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
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
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
        }
        EMClient.getInstance().groupManager().addToGroupWhiteList(groupId, members,
                new EMCommonCallback(callback));

        return null;
    }

    private String removeWhiteList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        List<String> members = new ArrayList<>();
        if (params.has("userIds")){
            JSONArray array = params.getJSONArray("userIds");
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
        String groupId = params.getString("groupId");
        String fileId = null;
        if (params.has("fileId")) {
            fileId = params.getString("fileId");
        }
        String savePath = null;
        if (params.has("savePath")) {
            savePath = params.getString("savePath");
        }
        EMClient.getInstance().groupManager().asyncDownloadGroupSharedFile(groupId, fileId, savePath,
                new EMCommonCallback(callback));

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
        if (params.has("msg")){
            reason = params.getString("msg");
        }

        EMClient.getInstance().groupManager().asyncApplyJoinToGroup(groupId, reason, new EMCommonCallback(callback));

        return null;
    }

    private String acceptJoinApplication(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String username = null;
        if (params.has("userId")){
            username = params.getString("userId");
        }

        EMClient.getInstance().groupManager().asyncAcceptApplication(username, groupId, new EMCommonCallback(callback));

        return null;
    }

    private String declineJoinApplication(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String username = null;
        if (params.has("userId")){
            username = params.getString("userId");
        }
        String reason = null;
        if (params.has("msg")){
            reason = params.getString("msg");
        }

        EMClient.getInstance().groupManager().asyncDeclineApplication(username, groupId, reason, new EMCommonCallback(callback));

        return null;
    }

    private String acceptInvitationFromGroup(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");

        String inviter = null;
        if (params.has("userId")){
            inviter = params.getString("userId");
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
        if (params.has("userId")){
            username = params.getString("userId");
        }
        String reason = null;
        if (params.has("msg")){
            reason = params.getString("msg");
        }

        EMClient.getInstance().groupManager().asyncDeclineInvitation(groupId, username, reason, new EMCommonCallback(callback));

        return null;
    }

    private String getJoinedGroupsFromServerSimple(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = 0;
        if (params.has("pageSize")){
            pageSize = params.getInt("pageSize");
        }
        int pageNum = 0;
        if (params.has("pageNum")){
            pageNum = params.getInt("pageNum");
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

        EMClient.getInstance().groupManager().asyncGetJoinedGroupsFromServer(pageNum, pageSize,callBack);
        return null;
    }

    private String setMemberAttributes(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        String userId = params.getString("userId");
        JSONObject attrs = params.optJSONObject("attrs");
        Map<String, String> map = EMHelper.getMapStrStrFromJsonObject(attrs);
        EMClient.getInstance().groupManager().asyncSetGroupMemberAttributes(groupId, userId, map, new EMCommonCallback(callback));
        return null;
    }

    private String fetchMemberAttributes(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String groupId = params.getString("groupId");
        JSONArray usersJa = params.getJSONArray("userIds");
        JSONArray attrsJa = params.getJSONArray("attrs");
        List<String> userIds = EMHelper.stringListFromJsonArray(usersJa);
        List<String> attrs = EMHelper.stringListFromJsonArray(attrsJa);
        EMClient.getInstance().groupManager().asyncFetchGroupMembersAttributes(groupId, userIds, attrs, new EMCommonValueCallback<Map<String, java.util.Map<String, String>>>(callback){
            @Override
            public void onSuccess(Map<String, Map<String, String>> object) {
                try {
                    JSONObject jsonObject = new JSONObject();
                    for (Map.Entry<String, Map<String,String>> entry: object.entrySet()) {
                        JSONObject tmpObject = new JSONObject();
                        for (Map.Entry<String,String> entry1: entry.getValue().entrySet()) {
                            tmpObject.put(entry1.getKey(), entry1.getValue());
                        }
                        jsonObject.put(entry.getKey(), tmpObject);
                    }
                    updateObject(jsonObject);
                }catch (JSONException e) {}
            }
        });
        return null;
    }

    private String fetchMyGroupsCount(EMWrapperCallback callback) {
        EMCommonValueCallback<Integer> callBack = new EMCommonValueCallback<Integer>(callback) {
            @Override
            public void onSuccess(Integer object) {
                try {
                    updateObject(EMHelper.getReturnJsonObject(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncGetJoinedGroupsCountFromServer(callBack);
        return null;
    }
    
    private void registerEaseListener(){
        wrapperGroupListener = new EMWrapperGroupListener();
        EMClient.getInstance().groupManager().addGroupChangeListener(wrapperGroupListener);
    }
}
