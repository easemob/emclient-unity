package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMPageResult;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMChatRoomHelper;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMPageResultHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityRoomManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMChatRoomManagerWrapper extends EMWrapper {

    static public EMChatRoomManagerWrapper wrapper() {
        return new EMChatRoomManagerWrapper();
    }

    public EMChatRoomManagerWrapper() {
        EMClient.getInstance().chatroomManager().addChatRoomChangeListener(new EMUnityRoomManagerListener());
    }

    private void joinChatRoom(String roomId, String callbackId) throws JSONException {
        EMUnityValueCallback<EMChatRoom> callBack = new EMUnityValueCallback<EMChatRoom>("EMChatRoom", callbackId) {
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    sendJsonObjectToUnity(EMChatRoomHelper.toJson(object).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().chatroomManager().joinChatRoom(roomId, callBack);
    }
    //
    private void leaveChatRoom(String roomId, String callbackId) {

        asyncRunnable(() -> {
            EMClient.getInstance().chatroomManager().leaveChatRoom(roomId);
            onSuccess(null, callbackId, null);
        });
    }
    //
    private void fetchPublicChatRoomsFromServer(String param, int pageNum, int pageSize, String callbackId) {

        EMUnityValueCallback<EMPageResult<EMChatRoom>> callback = new EMUnityValueCallback<EMPageResult<EMChatRoom>>("EMPageResult<EMChatRoom>", callbackId) {
            @Override
            public void onSuccess(EMPageResult<EMChatRoom> emChatRoomEMPageResult) {
                try {
                    JSONObject jsonObject = EMPageResultHelper.toJson(emChatRoomEMPageResult);
                    sendJsonObjectToUnity(jsonObject.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().chatroomManager().asyncFetchPublicChatRoomsFromServer(pageNum, pageSize, callback);
    }
    //
    private void fetchChatRoomInfoFromServer(String roomId, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }
    //
    private void getChatRoom(String roomId, String callbackId) {
        asyncRunnable(() -> {
            EMChatRoom room = EMClient.getInstance().chatroomManager().getChatRoom(roomId);
            try {
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }
    //
    private void getAllChatRooms(String callbackId) {
        asyncRunnable(() -> {
            List<EMChatRoom> list = EMClient.getInstance().chatroomManager().getAllChatRooms();
            JSONArray jsonArray = new JSONArray();
            try {
                for (EMChatRoom room : list) {
                    jsonArray.put(EMChatRoomHelper.toJson(room));
                }
            } catch (JSONException e) {
                e.printStackTrace();
            }

            onSuccess("List<EMChatRoom>", callbackId, jsonArray.toString());
        });
    }
    //
//    private void createChatRoom(String param, String channelName, String callbackId)
//            throws JSONException {
//        String subject = param.getString("subject");
//        String description = param.getString("desc");
//        String welcomeMessage = param.getString("welcomeMsg");
//        int maxUserCount = param.getInt("maxUserCount");
//        JSONArray members = param.getJSONArray("members");
//        List<String> membersList = new ArrayList<>();
//        for (int i = 0; i < members.length(); i++) {
//            membersList.add((String) members.get(i));
//        }
//        asyncRunnable(() -> {
//            try {
//                EMChatRoom room = EMClient.getInstance().chatroomManager().createChatRoom(subject, description,
//                        welcomeMessage, maxUserCount, membersList);
//                onSuccess(result, channelName, EMChatRoomHelper.toJson(room));
//            } catch (HyphenateException e) {
//                onError(result, e);
//            }
//        });
//    }
//
    private void destroyChatRoom(String roomId,  String callbackId) {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().destroyChatRoom(roomId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void changeChatRoomSubject(String roomId, String subject, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatRoomSubject(roomId, subject);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {
            }
        });
    }
    //
    private void changeChatRoomDescription(String roomId, String description, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatroomDescription(roomId, description);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {
            }
        });
    }
    //
    private void fetchChatRoomMembers(String roomId, String cursor, int pageSize, String callbackId)  {
        asyncRunnable(() -> {
            try {
                EMCursorResult<String> cursorResult = EMClient.getInstance().chatroomManager().fetchChatRoomMembers(roomId, cursor, pageSize);
                onSuccess( "EMCursorResult<String>", callbackId, EMCursorResultHelper.toJson(cursorResult).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }
    //
    private void muteChatRoomMembers(String roomId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().muteChatRoomMembers(roomId, list,-1);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void unMuteChatRoomMembers(String roomId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unMuteChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void changeChatRoomOwner(String roomId, String newOwner, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeOwner(roomId, newOwner);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {
            }
        });
    }
    //
    private void addChatRoomAdmin(String roomId, String admin, String callbackId) {

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().addChatRoomAdmin(roomId, admin);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {
            }
        });
    }

    private void removeChatRoomAdmin(String roomId, String admin, String callbackId) {

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomAdmin(roomId, admin);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void fetchChatRoomMuteList(String roomId, int pageNum, int pageSize, String callbackId) {

        asyncRunnable(() -> {
            try {
                Map<String, Long> map = EMClient.getInstance().chatroomManager().fetchChatRoomMuteList(roomId, pageNum, pageSize);
                String[] strings = (String[])map.keySet().toArray();
                JSONArray jsonArray = new JSONArray();
                for (int i = 0; i < strings.length; i++) {
                    jsonArray.put(strings[i]);
                }
                onSuccess("List<String>", callbackId, jsonArray.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void removeChatRoomMembers(String roomId, String stringList, String callbackId)  {

        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void blockChatRoomMembers(String roomId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().blockChatroomMembers(roomId, list);
                onSuccess("EMChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }
    //
    private void unBlockChatRoomMembers(String roomId, String stringList, String callbackId) {
        String[] allMembers = stringList.split(",");
        List<String> list = new ArrayList<String>();
        for (int i = 0; i < allMembers.length; i++) {
            list.add(allMembers[i]);
        }
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unblockChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
    //
    private void fetchChatRoomBlockList(String roomId, int pageNum, int pageSize, String callbackId)
            throws JSONException {

        asyncRunnable(() -> {
            try {
                List<String> blockList = EMClient.getInstance().chatroomManager().fetchChatRoomBlackList(roomId, pageNum, pageSize);
                JSONArray jsonAry = new JSONArray();
                for (String member : blockList) {
                    jsonAry.put(member);
                }
                onSuccess("List<String>", callbackId, jsonAry.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void updateChatRoomAnnouncement(String roomId, String announcement, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().updateChatRoomAnnouncement(roomId, announcement);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void fetchChatRoomAnnouncement(String roomId, String callbackId) {
        asyncRunnable(() -> {
            try {
                String announcement = EMClient.getInstance().chatroomManager().fetchChatRoomAnnouncement(roomId);
                onSuccess("String", callbackId, announcement);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

}
