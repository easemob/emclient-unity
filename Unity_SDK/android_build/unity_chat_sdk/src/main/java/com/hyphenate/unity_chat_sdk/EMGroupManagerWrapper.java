package com.hyphenate.unity_chat_sdk;

import android.util.Log;

import com.hyphenate.analytics.EMTimeTag;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMGroupInfo;
import com.hyphenate.chat.EMGroupOptions;
import com.hyphenate.chat.EMMucSharedFile;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMGroupHelper;
import com.hyphenate.unity_chat_sdk.helper.EMGroupOptionsHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMucSharedFileHelper;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityGroupManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMGroupManagerWrapper extends EMWrapper {

    static public EMGroupManagerWrapper wrapper() {
        return new EMGroupManagerWrapper();
    }

    public EMGroupManagerWrapper() {
        EMClient.getInstance().groupManager().addGroupChangeListener(new EMUnityGroupManagerListener());
    }

    private void acceptInvitationFromGroup(String groupId, String inviter, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || inviter == null || inviter.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or inviter is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    JSONObject obj = EMGroupHelper.toJson(object);
                    sendJsonObjectToUnity(obj.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncAcceptInvitation(groupId, inviter, callBack);
    }

    private void acceptJoinApplication(String groupId, String username, String callbackId) throws JSONException {
        if (groupId == null || groupId.length() == 0 || username == null || username.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or username is invalid");
            onError(callbackId, e);
            return;
        }
        EMClient.getInstance().groupManager().asyncAcceptApplication(username, groupId, new EMUnityCallback(callbackId));
    }

    private void addAdmin(String groupId, String admin, String callbackId) {

        if (groupId == null || groupId.length() == 0 || admin == null || admin.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or memberId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncAddGroupAdmin(groupId, admin, callBack);
    }

    private void addMembers(String groupId, String jsonString, String callbackId) throws JSONException {
        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        String[] allMembers = EMTransformHelper.stringArrayFromJsonString(jsonString);
        EMClient.getInstance().groupManager().asyncAddUsersToGroup(groupId, allMembers, new EMUnityCallback(callbackId));
    }

    private void addWhiteList(String groupId, String jsonString, String callbackId) {
        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);
        EMClient.getInstance().groupManager().addToGroupWhiteList(groupId, list, new EMUnityCallback(callbackId));
    }

    private void blockGroup(String groupId, String callbackId) {
        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncBlockGroupMessage(groupId, new EMUnityCallback(callbackId));
    }

    private void blockMembers(String groupId, String jsonString, String callbackId) throws JSONException {
        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);
        EMClient.getInstance().groupManager().asyncBlockUsers(groupId, list, new EMUnityCallback(callbackId));
    }

    private void changeGroupDescription(String groupId, String desc, String callbackId) {
        if (groupId == null || groupId.length() == 0 || desc == null) {
            HyphenateException e = new HyphenateException(500, "groupId or desc is invalid");
            onError(callbackId, e);
            return;
        }
        EMClient.getInstance().groupManager().asyncChangeGroupDescription(groupId, desc, new EMUnityCallback(callbackId));
    }

    private void changeGroupSubject(String groupId, String groupName, String callbackId) {
        if (groupId == null || groupId.length() == 0 || groupName == null) {
            HyphenateException e = new HyphenateException(500, "groupId or groupName is invalid");
            onError(callbackId, e);
            return;
        }
        EMClient.getInstance().groupManager().asyncChangeGroupName(groupId, groupName, new EMUnityCallback(callbackId));
    }

    private void changeGroupOwner(String groupId, String newOwner, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || newOwner == null || newOwner.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or newOwner is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callback  = new EMUnityValueCallback<EMGroup>("EMGroup",callbackId) {
            @Override
            public void onSuccess(EMGroup group) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(group).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncChangeOwner(groupId, newOwner, callback);
    }

    private void checkIfInGroupWhiteList(String groupId, String callbackId) {
        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<Boolean> callback = new EMUnityValueCallback<Boolean>("bool", callbackId) {
            @Override
            public void onSuccess(Boolean aBoolean) {
                JSONObject obj = new JSONObject();
                try {
                    obj.put("value", aBoolean);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().checkIfInGroupWhiteList(groupId, callback);
    }

    private void createGroup(String groupName, String optionsString, String desc, String memberListString, String reason, String callbackId) throws JSONException {

        JSONObject optionsJson = new JSONObject(optionsString);

        EMGroupOptions options = EMGroupOptionsHelper.fromJson(optionsJson);

        String[] allMembers = EMTransformHelper.stringArrayFromJsonString(memberListString);

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncCreateGroup(groupName, desc, allMembers, reason, options, callBack);
    }

    private void declineInvitationFromGroup(String groupId, String username, String reason, String callbackId) {

        if (groupId == null || groupId.length() == 0 || username == null || username.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or username is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncDeclineInvitation(groupId, username, reason, new EMUnityCallback(callbackId));
    }

    private void declineJoinApplication(String groupId, String username, String reason, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || username == null || username.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or username is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncDeclineApplication(username, groupId, reason, new EMUnityCallback(callbackId));
    }

    private void destroyGroup(String groupId, String callbackId)
    {
        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncDestroyGroup(groupId, new EMUnityCallback(callbackId));
    }

    private void downloadGroupSharedFile(String groupId, String fileId, String savePath, String callbackId) {
        if (groupId == null || groupId.length() == 0 || fileId == null || fileId.length() == 0 || savePath == null || savePath.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or fileId, savePath is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncDownloadGroupSharedFile(groupId, fileId, savePath, new EMUnityCallback(callbackId));
    }

    private void getGroupAnnouncementFromServer(String groupId, String callbackId)  throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<String> callback = new EMUnityValueCallback<String>("String", callbackId) {
            @Override
            public void onSuccess(String s) {
                JSONArray jsonArray = new JSONArray();
                jsonArray.put(s);
                sendJsonObjectToUnity(jsonArray.toString());
            }
        };
        EMClient.getInstance().groupManager().asyncFetchGroupAnnouncement(groupId, callback);
    }

    private void getGroupBlockListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "groupId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<List<String>> callback = new EMUnityValueCallback<List<String>>("List<String>", callbackId) {
            @Override
            public void onSuccess(List<String> strings) {
                JSONArray jsonArray = new JSONArray();
                for (String s: strings) {
                    jsonArray.put(s);
                }
                sendJsonObjectToUnity(jsonArray.toString());
            }
        };

        EMClient.getInstance().groupManager().asyncGetBlockedUsers(groupId, pageNum, pageSize, callback);
    }

    private void getGroupFileListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "groupId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<List<EMMucSharedFile>> callBack = new EMUnityValueCallback<List<EMMucSharedFile>>("List<EMMucSharedFile>", callbackId) {
            @Override
            public void onSuccess(List<EMMucSharedFile> object) {
                try {
                    JSONArray jsonArray = new JSONArray();
                    for (EMMucSharedFile file :object) {
                        jsonArray.put(EMMucSharedFileHelper.toJson(file));
                    }
                    sendJsonObjectToUnity(jsonArray.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().asyncFetchGroupSharedFileList(groupId, pageNum, pageSize, callBack);
    }

    private void getGroupMemberListFromServer(String groupId, int pageSize, String cursor, String callbackId)  throws JSONException {

        if (groupId == null || groupId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "groupId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMCursorResult<String>> callback = new EMUnityValueCallback<EMCursorResult<String>>("EMCursorResult<String>", callbackId) {
            @Override
            public void onSuccess(EMCursorResult<String> stringEMCursorResult) {
                try {
                    JSONObject jsonObject = EMCursorResultHelper.toJson(stringEMCursorResult);
                    sendJsonObjectToUnity(jsonObject.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().asyncFetchGroupMembers(groupId, cursor, pageSize, callback);
    }

    private void getGroupMuteListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "groupId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<Map<String, Long>> callback = new EMUnityValueCallback<Map<String, Long>>("List<String>", callbackId) {
            @Override
            public void onSuccess(Map<String, Long> stringLongMap) {
                String[] strings = (String[])stringLongMap.keySet().toArray();
                JSONArray jsonArray = new JSONArray();
                for (int i = 0; i < strings.length; i++) {
                    jsonArray.put(strings[i]);
                }
                sendJsonObjectToUnity(jsonArray.toString());
            }
        };

        EMClient.getInstance().groupManager().asyncFetchGroupMuteList(groupId, pageNum, pageSize, callback);
    }

    private void getGroupSpecificationFromServer(String groupId, String callbackId)  throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncGetGroupFromServer(groupId, callBack);
    }

    private void getGroupWhiteListFromServer(String groupId, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<List<String>> callback = new EMUnityValueCallback<List<String>>("List<String>", callbackId) {
            @Override
            public void onSuccess(List<String> strings) {
                sendJsonObjectToUnity(EMTransformHelper.stringListToJsonArray(strings).toString());
            }
        };

        EMClient.getInstance().groupManager().fetchGroupWhiteList(groupId, callback);
    }

    private String getGroupWithId(String groupId, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            return null;
        }

        EMGroup group = EMClient.getInstance().groupManager().getGroup(groupId);
        if (group == null) {
            return null;
        }else {
            return EMGroupHelper.toJson(group).toString();
        }
    }

    private String getJoinedGroups() throws JSONException {
        List<EMGroup> groups = EMClient.getInstance().groupManager().getAllGroups();
        JSONArray jsonArray = new JSONArray();
        for (EMGroup group : groups) {
            jsonArray.put(EMGroupHelper.toJson(group));
        }

        return jsonArray.toString();
    }

    private void getJoinedGroupsFromServer(int pageSize, int pageNum, String callbackId) throws JSONException {

        if (pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<List<EMGroup>> callback = new EMUnityValueCallback<List<EMGroup>>("List<EMGroup>", callbackId) {
            @Override
            public void onSuccess(List<EMGroup> emGroups) {
                sendJsonObjectToUnity(EMTransformHelper.groupListToJsonArray(emGroups).toString());
            }
        };
        EMClient.getInstance().groupManager().asyncGetJoinedGroupsFromServer(pageNum, pageSize, callback);
    }

    private void getPublicGroupsFromServer(int pageSize, String cursor, String callbackId) throws JSONException {

        if (pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback callback = new EMUnityValueCallback<EMCursorResult<EMGroupInfo>>("EMCursorResult<EMGroupInfo>", callbackId) {
            @Override
            public void onSuccess(EMCursorResult<EMGroupInfo> emGroupInfoEMCursorResult) {
                try {
                    JSONObject jsonObject = EMCursorResultHelper.toJson(emGroupInfoEMCursorResult);
                    sendJsonObjectToUnity(jsonObject.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().asyncGetPublicGroupsFromServer(pageSize, cursor, callback);
    }

    private void joinPublicGroup(String groupId, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncJoinGroup(groupId, new EMUnityCallback(callbackId));
    }

    private void leaveGroup(String groupId,  String callbackId) {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncLeaveGroup(groupId, new EMUnityCallback(callbackId));

    }

    private void muteAllMembers(String groupId, String callbackId) {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup group) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(group).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().muteAllMembers(groupId, callBack);
    }

    private void muteMembers(String groupId, String jsonString, String callbackId) {

        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup",callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().aysncMuteGroupMembers(groupId, list, -1, callBack);
    }

    private void removeAdmin(String groupId, String admin, String callbackId) {

        if (groupId == null || groupId.length() == 0 || admin == null || admin.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or admin is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup group) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(group).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().asyncRemoveGroupAdmin(groupId, admin, callBack);
    }

    private void removeGroupSharedFile(String groupId, String fileId, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || fileId == null || fileId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or fileId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncDeleteGroupSharedFile(groupId, fileId, new EMUnityCallback(callbackId));
    }

    private void removeMembers(String groupId, String stringList, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || stringList == null || stringList.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(stringList);
        EMClient.getInstance().groupManager().asyncRemoveUsersFromGroup(groupId, list, new EMUnityCallback(callbackId));
    }

    private void removeWhiteList(String groupId, String jsonString, String callbackId) {

        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);
        EMClient.getInstance().groupManager().removeFromGroupWhiteList(groupId, list, new EMUnityCallback(callbackId));
    }

    private void requestToJoinPublicGroup(String groupId, String callbackId) throws JSONException {
        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or is invalid");
            onError(callbackId, e);
            return;
        }
        EMClient.getInstance().groupManager().asyncJoinGroup(groupId, new EMUnityCallback(callbackId));
    }

    private void unblockGroup(String groupId, String callbackId) {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncUnblockGroupMessage(groupId, new EMUnityCallback(callbackId));
    }

    private void unblockMembers(String groupId, String jsonString, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);
        EMClient.getInstance().groupManager().asyncUnblockUsers(groupId, list, new EMUnityCallback(callbackId));
    }

    private void unMuteAllMembers(String groupId, String callbackId) {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup", callbackId) {
            @Override
            public void onSuccess(EMGroup group) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(group).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().groupManager().unmuteAllMembers(groupId, callBack);
    }

    private void unMuteMembers(String groupId, String jsonString, String callbackId) {
        if (groupId == null || groupId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or members is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMGroup> callBack = new EMUnityValueCallback<EMGroup>("EMGroup",callbackId) {
            @Override
            public void onSuccess(EMGroup object) {
                try {
                    sendJsonObjectToUnity(EMGroupHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        List<String> list = EMTransformHelper.stringListFromJsonString(jsonString);

        EMClient.getInstance().groupManager().asyncUnMuteGroupMembers(groupId, list, callBack);
    }

    private void updateGroupAnnouncement(String groupId, String announcement, String callbackId) throws JSONException {
        if (groupId == null || groupId.length() == 0 || announcement == null) {
            HyphenateException e = new HyphenateException(500, "groupId or announcement is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncUpdateGroupAnnouncement(groupId, announcement, new EMUnityCallback(callbackId));
    }

    private void updateGroupExt(String groupId, String ext, String callbackId) throws JSONException {

        if (groupId == null || groupId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMGroup group = EMClient.getInstance().groupManager().updateGroupExtension(groupId, ext);
                onSuccess("EMGroup", callbackId, EMGroupHelper.toJson(group).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch ( JSONException e) {

            }
        });
    }

    private void uploadGroupSharedFile(String groupId, String filePath, String callbackId) {

        if (groupId == null || groupId.length() == 0 || filePath == null || filePath.length() == 0) {
            HyphenateException e = new HyphenateException(500, "groupId or filePath is invalid");
            onError(callbackId, e);
            return;
        }

        EMClient.getInstance().groupManager().asyncUploadGroupSharedFile(groupId, filePath, new EMUnityCallback(callbackId));
    }

}
