package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMPageResult;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMChatRoomHelper;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMPageResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
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

    private void addChatRoomAdmin(String roomId, String admin, String callbackId) {

        if (roomId == null || roomId.length() == 0 || admin == null || admin.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or memberId is invalid");
            onError(callbackId, e);
            return;
        }
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().addChatRoomAdmin(roomId, admin);
                EMChatRoom room = EMClient.getInstance().chatroomManager().getChatRoom(roomId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void blockChatRoomMembers(String roomId, String jsonString, String callbackId) {

        if (roomId == null || roomId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.jsonStringToStringList(jsonString);

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().blockChatroomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void changeChatRoomOwner(String roomId, String newOwner, String callbackId) {

        if (roomId == null || roomId.length() == 0 || newOwner == null || newOwner.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or newOwner is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeOwner(roomId, newOwner);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void changeChatRoomDescription(String roomId, String description, String callbackId) {

        if (roomId == null || roomId.length() == 0 || description == null) {
            HyphenateException e = new HyphenateException(500, "roomId or description is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatroomDescription(roomId, description);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void changeChatRoomSubject(String roomId, String subject, String callbackId) {

        if (roomId == null || roomId.length() == 0 || subject == null) {
            HyphenateException e = new HyphenateException(500, "roomId or subject is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatRoomSubject(roomId, subject);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void createChatRoom(String subject, String description, String welcomeMessage, int maxUserCount, String jsonString, String callbackId)
            throws JSONException {
        asyncRunnable(() -> {

            List<String> membersList = null;
            if (jsonString != null && jsonString.length() > 0) {
                membersList = EMTransformHelper.jsonStringToStringList(jsonString);
            }

            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().createChatRoom(subject, description, welcomeMessage, maxUserCount, membersList);
                onSuccess("ChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void destroyChatRoom(String roomId,  String callbackId) {

        if (roomId == null || roomId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().destroyChatRoom(roomId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void fetchPublicChatRoomsFromServer(int pageNum, int pageSize, String callbackId) {

        if (pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMPageResult<EMChatRoom>> callback = new EMUnityValueCallback<EMPageResult<EMChatRoom>>("PageResult<ChatRoom>", callbackId) {
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

    private void fetchChatRoomAnnouncement(String roomId, String callbackId) {

        if (roomId == null || roomId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                String announcement = EMClient.getInstance().chatroomManager().fetchChatRoomAnnouncement(roomId);
                onSuccess("String", callbackId, announcement);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void fetchChatRoomBlockList(String roomId, int pageNum, int pageSize, String callbackId)
            throws JSONException {

        if (roomId == null || roomId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "roomId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                List<String> blockList = EMClient.getInstance().chatroomManager().fetchChatRoomBlackList(roomId, pageNum, pageSize);
                onSuccess("List<String>", callbackId, EMTransformHelper.jsonArrayFromStringList(blockList).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void fetchChatRoomInfoFromServer(String roomId, String callbackId) {

        if (roomId == null || roomId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId);
                onSuccess("ChatRoom", callbackId, EMChatRoomHelper.toJson(room).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }

    private void fetchChatRoomMembers(String roomId, String cursor, int pageSize, String callbackId)  {

        if (roomId == null || roomId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "roomId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMCursorResult<String> cursorResult = EMClient.getInstance().chatroomManager().fetchChatRoomMembers(roomId, cursor, pageSize);
                onSuccess( "CursorResult<String>", callbackId, EMCursorResultHelper.toJson(cursorResult).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException e) {

            }
        });
    }

    private void fetchChatRoomMuteList(String roomId, int pageNum, int pageSize, String callbackId) {

        if (roomId == null || roomId.length() == 0 || pageSize <= 0) {
            HyphenateException e = new HyphenateException(500, "roomId or pageSize is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                Map<String, Long> map = EMClient.getInstance().chatroomManager().fetchChatRoomMuteList(roomId, pageNum, pageSize);
                JSONArray jsonArray = new JSONArray();
                for (Map.Entry<String, Long> entry : map.entrySet()) {
                    jsonArray.put(entry.getKey());
                }
                onSuccess("List<String>", callbackId, jsonArray.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getAllChatRooms(String callbackId) {

        asyncRunnable(() -> {
            List<EMChatRoom> list = EMClient.getInstance().chatroomManager().getAllChatRooms();
            onSuccess("List<ChatRoom>", callbackId, EMTransformHelper.jsonArrayFromChatRoomList(list).toString());
        });
    }

    private String getChatRoom(String roomId) throws JSONException {

        if (roomId == null || roomId.length() == 0) {
            return null;
        }

        EMChatRoom room = new EMChatRoom(roomId);
        if (room == null) {
            return  null;
        }

        return EMChatRoomHelper.toJson(room).toString();
    }

    private void joinChatRoom(String roomId, String callbackId) throws JSONException {

        if (roomId == null || roomId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId is invalid");
            onError(callbackId, e);
            return;
        }

        EMUnityValueCallback<EMChatRoom> callBack = new EMUnityValueCallback<EMChatRoom>("ChatRoom", callbackId) {
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

    private void leaveChatRoom(String roomId, String callbackId) {

        if (roomId == null || roomId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            EMClient.getInstance().chatroomManager().leaveChatRoom(roomId);
            onSuccess(null, callbackId, null);
        });
    }

    private void muteChatRoomMembers(String roomId, String jsonString, String callbackId) {

        if (roomId == null || roomId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.jsonStringToStringList(jsonString);

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().muteChatRoomMembers(roomId, list,-1);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void removeChatRoomAdmin(String roomId, String adminId, String callbackId) {

        if (roomId == null || roomId.length() == 0 || adminId == null || adminId.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or adminId is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().removeChatRoomAdmin(roomId, adminId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void removeChatRoomMembers(String roomId, String jsonString, String callbackId)  {

        if (roomId == null || roomId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.jsonStringToStringList(jsonString);

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void unBlockChatRoomMembers(String roomId, String jsonString, String callbackId) {

        if (roomId == null || roomId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.jsonStringToStringList(jsonString);
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unblockChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void unMuteChatRoomMembers(String roomId, String jsonString, String callbackId) {
        if (roomId == null || roomId.length() == 0 || jsonString == null || jsonString.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or members is invalid");
            onError(callbackId, e);
            return;
        }

        List<String> list = EMTransformHelper.jsonStringToStringList(jsonString);

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unMuteChatRoomMembers(roomId, list);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void updateChatRoomAnnouncement(String roomId, String announcement, String callbackId) {

        if (roomId == null || roomId.length() == 0 || announcement == null || announcement.length() == 0) {
            HyphenateException e = new HyphenateException(500, "roomId or announcement is invalid");
            onError(callbackId, e);
            return;
        }

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().updateChatRoomAnnouncement(roomId, announcement);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

}
