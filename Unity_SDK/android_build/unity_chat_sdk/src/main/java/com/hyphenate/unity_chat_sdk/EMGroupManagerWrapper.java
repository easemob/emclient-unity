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

    private void getGroupWithId(String groupId, String callbackId) throws JSONException {
        EMGroup group = EMClient.getInstance().groupManager().getGroup(groupId);
        if (group != null) {
            onSuccess(callbackId, "EMGroup", EMGroupHelper.toJson(group).toString());
        }else {
            onSuccess(callbackId, "EMGroup", null);
        }
    }

    private void getJoinedGroups(String callbackId) throws JSONException {
        asyncRunnable(()->{
            List<EMGroup> groups = EMClient.getInstance().groupManager().getAllGroups();
            JSONArray jsonArray = new JSONArray();
            for (EMGroup group : groups) {
                try {
                    jsonArray.put(EMGroupHelper.toJson(group));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
            onSuccess(callbackId, "List<EMGroup>", jsonArray.toString());
        });
    }

    private void getGroupsWithoutPushNotification(String callbackId)  throws JSONException {
        asyncRunnable(() -> {
            List<String> groups = EMClient.getInstance().pushManager().getNoPushGroups();
            JSONArray jsonAry = new JSONArray();
            for (String group : groups) {
                jsonAry.put(group);
            }
            onSuccess(callbackId, "List<String>", jsonAry.toString());
        });
    }

    private void getJoinedGroupsFromServer(int pageSize, int pageNum, String callbackId) throws JSONException {

        EMUnityValueCallback<List<EMGroup>> callback = new EMUnityValueCallback<List<EMGroup>>("List<EMGroup>", callbackId) {
            @Override
            public void onSuccess(List<EMGroup> emGroups) {
                sendJsonObjectToUnity(EMTransformHelper.groupListToJsonArray(emGroups).toString());
            }
        };
        EMClient.getInstance().groupManager().asyncGetJoinedGroupsFromServer(pageNum, pageSize, callback);
    }

    private void checkIfInGroupWhiteList(String groupId, String callbackId) {
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

    private void getPublicGroupsFromServer(int pageSize, String cursor, String callbackId) throws JSONException {
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
//
    private void createGroup(String groupName, String optionsString, String desc, String memberListString, String reason, String callbackId) throws JSONException {

        JSONObject optionsJson = new JSONObject(optionsString);

        EMGroupOptions options = EMGroupOptionsHelper.fromJson(optionsJson);

        String[] allMembers = memberListString.split(",");

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
//
    // ?
    private void getGroupSpecificationFromServer(String groupId, String callbackId)  throws JSONException {
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
    // ?
    private void getGroupMemberListFromServer(String groupId, int pageSize, String cursor, String callbackId)  throws JSONException {

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
//
    // ?
    private void getGroupBlockListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {

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
    // ?
    private void getGroupMuteListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {

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
    // ?
    private void getGroupWhiteListFromServer(String groupId, String callbackId) throws JSONException {

        EMUnityValueCallback<List<String>> callback = new EMUnityValueCallback<List<String>>("List<String>", callbackId) {
            @Override
            public void onSuccess(List<String> strings) {
                sendJsonObjectToUnity(EMTransformHelper.stringListToJsonArray(strings).toString());
            }
        };

        EMClient.getInstance().groupManager().fetchGroupWhiteList(groupId, callback);
    }
//
    // ?
    private void getGroupFileListFromServer(String groupId, int pageSize, int pageNum, String callbackId) throws JSONException {
        EMUnityValueCallback<List<EMMucSharedFile>> callBack = new EMUnityValueCallback<List<EMMucSharedFile>>("List<EMMucSharedFile>", callbackId) {
            @Override
            public void onSuccess(List<EMMucSharedFile> object) {
                try {
                    JSONArray jsonArray = new JSONArray();
                    for (EMMucSharedFile file :object) {
                        jsonArray.put(EMMucSharedFileHelper.toJson(file).toString());
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };
        EMClient.getInstance().groupManager().asyncFetchGroupSharedFileList(groupId, pageNum, pageSize, callBack);
    }
    // ?
    private void getGroupAnnouncementFromServer(String groupId, String callbackId)  throws JSONException {
        EMUnityValueCallback<String> callback = new EMUnityValueCallback<String>("String", callbackId) {
            @Override
            public void onSuccess(String s) {
                sendJsonObjectToUnity(s);
            }
        };
        EMClient.getInstance().groupManager().asyncFetchGroupAnnouncement(groupId, callback);
    }
//
    // ?
    private void addMembers(String groupId, String stringList, String callbackId) throws JSONException {
        String[] allMembers = stringList.split(",");
        EMClient.getInstance().groupManager().asyncAddUsersToGroup(groupId, allMembers, new EMUnityCallback(callbackId));
    }
    // ？
    private void removeMembers(String groupId, String stringList, String callbackId) throws JSONException {

        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }

        EMClient.getInstance().groupManager().asyncRemoveUsersFromGroup(groupId, list, new EMUnityCallback(callbackId));
    }

    // ?
    private void blockMembers(String groupId, String stringList, String callbackId) throws JSONException {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        EMClient.getInstance().groupManager().asyncBlockUsers(groupId, list, new EMUnityCallback(callbackId));
    }
    // ?
    private void unblockMembers(String groupId, String stringList, String callbackId) throws JSONException {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        EMClient.getInstance().groupManager().asyncUnblockUsers(groupId, list, new EMUnityCallback(callbackId));
    }
//
    // ?
    private void updateGroupSubject(String groupId, String groupName, String callbackId) {
        EMClient.getInstance().groupManager().asyncChangeGroupName(groupId, groupName, new EMUnityCallback(callbackId));
    }

    // ?
    private void updateDescription(String groupId, String desc, String callbackId) {
        EMClient.getInstance().groupManager().asyncChangeGroupDescription(groupId, desc, new EMUnityCallback(callbackId));
    }
    // ?
    private void leaveGroup(String groupId,  String callbackId) {
        EMClient.getInstance().groupManager().asyncLeaveGroup(groupId, new EMUnityCallback(callbackId));
    }
    // ?
    private void destroyGroup(String groupId, String callbackId) {
        EMClient.getInstance().groupManager().asyncDestroyGroup(groupId, new EMUnityCallback(callbackId));
    }

    // ?
    private void blockGroup(String groupId, String callbackId) {
        EMClient.getInstance().groupManager().asyncBlockGroupMessage(groupId, new EMUnityCallback(callbackId));
    }
    // ?
    private void unblockGroup(String groupId, String callbackId) {
        EMClient.getInstance().groupManager().asyncUnblockGroupMessage(groupId, new EMUnityCallback(callbackId));
    }
    // ?
    private void updateGroupOwner(String groupId, String member, String callbackId) throws JSONException {

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

        EMClient.getInstance().groupManager().asyncChangeOwner(groupId, member, callback);
    }

    // ?
    private void addAdmin(String groupId, String admin, String callbackId) {

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

    // ?
    private void removeAdmin(String groupId, String admin, String callbackId) {
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
    // ?
    private void muteMembers(String groupId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
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

        EMClient.getInstance().groupManager().aysncMuteGroupMembers(groupId, list, -1, callBack);
    }
    // ?
    private void unMuteMembers(String groupId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
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

        EMClient.getInstance().groupManager().asyncUnMuteGroupMembers(groupId, list, callBack);
    }
     // ?
    private void muteAllMembers(String groupId, String callbackId) {

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
    // ?
    private void unMuteAllMembers(String groupId, String callbackId) {
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
//
    // ?
    private void addWhiteList(String groupId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        EMClient.getInstance().groupManager().addToGroupWhiteList(groupId, list, new EMUnityCallback(callbackId));
    }
    // ？
    private void removeWhiteList(String groupId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }

        EMClient.getInstance().groupManager().removeFromGroupWhiteList(groupId, list, new EMUnityCallback(callbackId));
    }
    // ?
    private void uploadGroupSharedFile(String groupId, String filePath, String callbackId) {
        EMClient.getInstance().groupManager().asyncUploadGroupSharedFile(groupId, filePath, new EMUnityCallback(callbackId));
    }
    // ?
    private void downloadGroupSharedFile(String groupId, String fileId, String savePath, String callbackId) {
        EMClient.getInstance().groupManager().asyncDownloadGroupSharedFile(groupId, fileId, savePath, new EMUnityCallback(callbackId));
    }
    // ?
    private void removeGroupSharedFile(String groupId, String fileId, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncDeleteGroupSharedFile(groupId, fileId, new EMUnityCallback(callbackId));
    }
    // ?
    private void updateGroupAnnouncement(String groupId, String announcement, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncUpdateGroupAnnouncement(groupId, announcement, new EMUnityCallback(callbackId));
    }
    // ?
    private void updateGroupExt(String groupId, String ext, String callbackId) throws JSONException {
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
    // ?
    private void joinPublicGroup(String groupId, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncJoinGroup(groupId, new EMUnityCallback(callbackId));
    }
    // ?
    private void requestToJoinPublicGroup(String groupId, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncJoinGroup(groupId, new EMUnityCallback(callbackId));
    }
//
    // ？
    private void acceptJoinApplication(String groupId, String username, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncAcceptApplication(groupId, username, new EMUnityCallback(callbackId));
    }
    // ?
    private void declineJoinApplication(String groupId, String username, String reason, String callbackId) throws JSONException {
        EMClient.getInstance().groupManager().asyncDeclineApplication(groupId, username, reason, new EMUnityCallback(callbackId));
    }
//
    // ?
    private void acceptInvitationFromGroup(String groupId, String inviter, String callbackId) throws JSONException {

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
    // ?
    private void declineInvitationFromGroup(String groupId, String username, String reason, String callbackId) {
        EMClient.getInstance().groupManager().asyncDeclineInvitation(groupId, username, reason, new EMUnityCallback(callbackId));
    }
    // ?
    private void ignoreGroupPush(String groupId, boolean enable, String callbackId) throws JSONException {
        List<String> list = new ArrayList<>();
        list.add(groupId);

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().pushManager().updatePushServiceForGroup(list, enable);
                EMGroup group = EMClient.getInstance().groupManager().getGroup(groupId);
                onSuccess("EMGroup", callbackId, EMGroupHelper.toJson(group).toString());
            } catch (HyphenateException e ) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }
}
