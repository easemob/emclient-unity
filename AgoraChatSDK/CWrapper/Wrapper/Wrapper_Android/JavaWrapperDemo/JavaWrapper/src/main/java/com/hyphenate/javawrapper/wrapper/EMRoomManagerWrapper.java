package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMChatRoomChangeListener;
import com.hyphenate.EMError;
import com.hyphenate.EMResultCallBack;
import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMPageResult;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.util.EMHelper;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMChatRoomHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMPageResultHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMRoomManagerWrapper extends EMBaseWrapper{
    private EMChatRoomChangeListener chatRoomChangeListener;

    EMRoomManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        if (EMSDKMethod.joinChatRoom.equals(method)) {
            joinChatRoom(method, jsonObject, callback);
        } else if (EMSDKMethod.leaveChatRoom.equals(method)) {
            leaveChatRoom(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchPublicChatRoomsFromServer.equals(method)) {
            fetchPublicChatRoomsFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomInfoFromServer.equals(method)) {
            fetchChatRoomInfoFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.getChatRoom.equals(method)) {
            getChatRoom(method, jsonObject, callback);
        } else if (EMSDKMethod.getAllChatRooms.equals(method)) {
            getAllChatRooms(method, jsonObject, callback);
        } else if (EMSDKMethod.createChatRoom.equals(method)) {
            createChatRoom(method, jsonObject, callback);
        } else if (EMSDKMethod.destroyChatRoom.equals(method)) {
            destroyChatRoom(method, jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomSubject.equals(method)) {
            changeChatRoomSubject(method, jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomDescription.equals(method)) {
            changeChatRoomDescription(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomMembers.equals(method)) {
            fetchChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.muteChatRoomMembers.equals(method)) {
            muteChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.unMuteChatRoomMembers.equals(method)) {
            unMuteChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomOwner.equals(method)) {
            changeChatRoomOwner(method, jsonObject, callback);
        } else if (EMSDKMethod.addChatRoomAdmin.equals(method)) {
            addChatRoomAdmin(method, jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomAdmin.equals(method)) {
            removeChatRoomAdmin(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomMuteList.equals(method)) {
            fetchChatRoomMuteList(method, jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomMembers.equals(method)) {
            removeChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.blockChatRoomMembers.equals(method)) {
            blockChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.unBlockChatRoomMembers.equals(method)) {
            unBlockChatRoomMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomBlockList.equals(method)) {
            fetchChatRoomBlockList(method, jsonObject, callback);
        } else if (EMSDKMethod.updateChatRoomAnnouncement.equals(method)) {
            updateChatRoomAnnouncement(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomAnnouncement.equals(method)) {
            fetchChatRoomAnnouncement(method, jsonObject, callback);
        } else if (EMSDKMethod.addMembersToChatRoomWhiteList.equals(method)) {
            addMembersToChatRoomWhiteList(method, jsonObject, callback);
        } else if (EMSDKMethod.removeMembersFromChatRoomWhiteList.equals(method)) {
            removeMembersFromChatRoomWhiteList(method, jsonObject, callback);
        } else if (EMSDKMethod.isMemberInChatRoomWhiteListFromServer.equals(method)) {
            isMemberInChatRoomWhiteListFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomWhiteListFromServer.equals(method)) {
            fetchChatRoomWhiteListFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.muteAllChatRoomMembers.equals(method)) {
            muteAllChatRoomsMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.unMuteAllChatRoomMembers.equals(method)) {
            unMuteAllChatRoomsMembers(method, jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomAttributes.equals(method)){
            fetchChatRoomAttributes(method, jsonObject, callback);
        } else if (EMSDKMethod.setChatRoomAttributes.equals(method)){
            setChatRoomAttributes(method, jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomAttributes.equals(method)){
            removeChatRoomAttributes(method, jsonObject, callback);
        }
        return null;
    }
    
    private void joinChatRoom(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().joinChatRoom(roomId, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    updateObject(EMChatRoomHelper.toJson(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void leaveChatRoom(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            EMClient.getInstance().chatroomManager().leaveChatRoom(roomId);
            onSuccess(true, callback);
        });
    }

    private void fetchPublicChatRoomsFromServer(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        int pageNum = params.getInt("pageNum");
        int pageSize = params.getInt("pageSize");

        EMClient.getInstance().chatroomManager().asyncFetchPublicChatRoomsFromServer(pageNum, pageSize,
                new EMCommonValueCallback<EMPageResult<EMChatRoom>>(callback) {
                    @Override
                    public void onSuccess(EMPageResult<EMChatRoom> object) {
                        try {
                            updateObject(EMPageResultHelper.toJson(object));
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }

                    @Override
                    public void onError(int error, String errorMsg) {
                        super.onError(error, errorMsg);
                    }
                });
    }

    private void fetchChatRoomInfoFromServer(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        boolean fetchMembers = params.getBoolean("fetchMembers");
        asyncRunnable(() -> {
            EMChatRoom room = null;
            try {
                if (fetchMembers) {
                    room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId, true);
                }else {
                    room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId);
                }
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException error) {
                onError(error, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void getChatRoom(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().getChatRoom(roomId);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            }catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void getAllChatRooms(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        asyncRunnable(() -> {
            List<EMChatRoom> list = EMClient.getInstance().chatroomManager().getAllChatRooms();
            JSONArray jsonArray = new JSONArray();
            try {
                for (EMChatRoom room : list) {
                    jsonArray.put(EMChatRoomHelper.toJson(room));
                }
                onSuccess(jsonArray, callback);
            }catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void createChatRoom(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String subject = params.getString("subject");
        int maxUserCount = params.getInt("maxUserCount");
        String description = null;
        if (params.has("desc")){
            description = params.getString("desc");
        }
        String welcomeMessage = null;
        if (params.has("welcomeMsg")){
            welcomeMessage = params.getString("welcomeMsg");
        }
        List<String> membersList = new ArrayList<>();
        JSONArray members = null;
        if (params.has("members")){
            members = params.getJSONArray("members");
            for (int i = 0; i < members.length(); i++) {
                membersList.add((String) members.get(i));
            }
        }

        String finalDescription = description;
        String finalWelcomeMessage = welcomeMessage;
        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().createChatRoom(subject, finalDescription,
                        finalWelcomeMessage, maxUserCount, membersList);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void destroyChatRoom(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().destroyChatRoom(roomId);
                onSuccess(true, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void changeChatRoomSubject(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String subject = params.getString("subject");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatRoomSubject(roomId, subject);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void changeChatRoomDescription(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String description = params.getString("description");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatroomDescription(roomId,
                        description);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void fetchChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String cursor = null;
        if(params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        int pageSize = params.getInt("pageSize");

        String finalCursor = cursor;
        asyncRunnable(() -> {
            try {
                EMCursorResult<String> cursorResult = EMClient.getInstance().chatroomManager()
                        .fetchChatRoomMembers(roomId, finalCursor, pageSize);
                onSuccess(EMCursorResultHelper.toJson(cursorResult), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void muteChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        long duration = Long.parseLong(params.getString("duration"));
        JSONArray muteMembers = params.getJSONArray("muteMembers");
        List<String> muteMembersList = new ArrayList<>();
        for (int i = 0; i < muteMembers.length(); i++) {
            muteMembersList.add((String) muteMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().muteChatRoomMembers(roomId, muteMembersList,
                        duration);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void unMuteChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray muteMembers = params.getJSONArray("unMuteMembers");
        List<String> unMuteMembersList = new ArrayList<>();
        for (int i = 0; i < muteMembers.length(); i++) {
            unMuteMembersList.add((String) muteMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unMuteChatRoomMembers(roomId,
                        unMuteMembersList);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void changeChatRoomOwner(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String newOwner = params.getString("newOwner");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeOwner(roomId, newOwner);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void addChatRoomAdmin(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String admin = params.getString("admin");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().addChatRoomAdmin(roomId, admin);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void removeChatRoomAdmin(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String admin = params.getString("admin");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomAdmin(roomId, admin);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void fetchChatRoomMuteList(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        int pageNum = params.getInt("pageNum");
        int pageSize = params.getInt("pageSize");

        asyncRunnable(() -> {
            try {
                Map<String, Long> map = EMClient.getInstance().chatroomManager().fetchChatRoomMuteList(roomId, pageNum, pageSize);
                JSONObject jsonObject = new JSONObject();
                for (Map.Entry<String, Long> entry: map.entrySet()) {
                    jsonObject.put(entry.getKey(), entry.getValue());
                }
                onSuccess(jsonObject, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void removeChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray members = params.getJSONArray("members");
        List<String> membersList = new ArrayList<>();
        for (int i = 0; i < members.length(); i++) {
            membersList.add((String) members.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomMembers(roomId, membersList);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void blockChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray blockMembers = params.getJSONArray("members");
        List<String> blockMembersList = new ArrayList<>();
        for (int i = 0; i < blockMembers.length(); i++) {
            blockMembersList.add((String) blockMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().blockChatroomMembers(roomId,
                        blockMembersList);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void unBlockChatRoomMembers(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray blockMembers = params.getJSONArray("members");
        List<String> blockMembersList = new ArrayList<>();
        for (int i = 0; i < blockMembers.length(); i++) {
            blockMembersList.add((String) blockMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unblockChatRoomMembers(roomId,
                        blockMembersList);
                onSuccess(EMChatRoomHelper.toJson(room), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            } catch (JSONException e) {
                e.printStackTrace();
            }
        });
    }

    private void fetchChatRoomBlockList(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        int pageNum = params.getInt("pageNum");
        int pageSize = params.getInt("pageSize");

        asyncRunnable(() -> {
            try {
                List<String> blockList = EMClient.getInstance().chatroomManager().fetchChatRoomBlackList(roomId,
                        pageNum, pageSize);
                onSuccess(EMHelper.stringListToJsonArray(blockList), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void updateChatRoomAnnouncement(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String announcement = params.getString("announcement");

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().updateChatRoomAnnouncement(roomId, announcement);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void fetchChatRoomAnnouncement(String method, JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            try {
                String announcement = EMClient.getInstance().chatroomManager().fetchChatRoomAnnouncement(roomId);
                onSuccess(announcement, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void addMembersToChatRoomWhiteList(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray jsonAry = params.getJSONArray("members");
        List<String> members = new ArrayList<>();
        for (int i = 0; i < jsonAry.length(); i++) {
            members.add((String) jsonAry.get(i));
        }


        EMClient.getInstance().chatroomManager().addToChatRoomWhiteList(roomId, members, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    updateObject(EMChatRoomHelper.toJson(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });

    }

    private void removeMembersFromChatRoomWhiteList(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray jsonAry = params.getJSONArray("members");
        List<String> members = new ArrayList<>();
        for (int i = 0; i < jsonAry.length(); i++) {
            members.add((String) jsonAry.get(i));
        }

        EMClient.getInstance().chatroomManager().removeFromChatRoomWhiteList(roomId, members, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    updateObject(EMChatRoomHelper.toJson(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void isMemberInChatRoomWhiteListFromServer(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().checkIfInChatRoomWhiteList(roomId, new EMCommonValueCallback<Boolean>(callback));
    }

    private void fetchChatRoomWhiteListFromServer(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().fetchChatRoomWhiteList(roomId,  new EMCommonValueCallback<List<String>>(callback) {
            @Override
            public void onSuccess(List<String> object) {
                updateObject(EMHelper.stringListToJsonArray(object));
            }
        });
    }

    private void muteAllChatRoomsMembers(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().muteAllMembers(roomId, new EMCommonValueCallback<EMChatRoom>(callback) {
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    updateObject(EMChatRoomHelper.toJson(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    private void unMuteAllChatRoomsMembers(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().unmuteAllMembers(roomId, new EMCommonValueCallback<EMChatRoom>(callback) {
            @Override
            public void onSuccess(EMChatRoom object) {
                try {
                    updateObject(EMChatRoomHelper.toJson(object));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public void fetchChatRoomAttributes(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        List<String> keys = new ArrayList<>();
        if (params.has("keys")){
            JSONArray array = params.getJSONArray("keys");
            for (int i = 0; i < array.length(); i++) {
                keys.add(array.getString(i));
            }
        }
        EMClient.getInstance().chatroomManager().asyncFetchChatroomAttributesFromServer(roomId, keys, new EMCommonValueCallback<Map<String,String>>(callback) {
            @Override
            public void onSuccess(Map<String,String> object) {
                updateObject(EMHelper.stringMapToJsonObject(object));
            }
        });
    }

    public void setChatRoomAttributes(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        Map<String, String> attributes = new HashMap<>();
        if (params.has("attributes")) {
            JSONObject jsonObject = params.getJSONObject("attributes");
            Iterator iterator = jsonObject.keys();
            while (iterator.hasNext()) {
                String key = iterator.next().toString();
                attributes.put(key, jsonObject.getString(key));
            }
        }
        boolean autoDelete = false;
        if (params.has("autoDelete")) {
            autoDelete = params.getBoolean("autoDelete");
        }
        boolean forced = false;
        if(params.has("forced")) {
            forced = params.getBoolean("forced");
        }

        EMRoomManagerWrapper current = this;

        EMResultCallBack resultCallBack = (EMResultCallBack<Map<String, Integer>>) (code, value) -> asyncRunnable(()->{
            if (value.size() > 0 || code == EMError.EM_NO_ERROR) {
                current.onSuccess(value, callback);
            }else {
                HyphenateException e = new HyphenateException(code, "");
                current.onError(e, callback);
            }
        });


        if (forced) {
            EMClient.getInstance().chatroomManager().asyncSetChatroomAttributesForced(roomId, attributes, autoDelete, resultCallBack);
        }else {
            EMClient.getInstance().chatroomManager().asyncSetChatroomAttributes(roomId, attributes, autoDelete, resultCallBack);
        }
    }

    public void removeChatRoomAttributes(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        List<String> keys = new ArrayList<String>();
        if (params.has("keys")){
            JSONArray array = params.getJSONArray("keys");
            for (int i = 0; i < array.length(); i++) {
                keys.add(array.getString(i));
            }
        }

        boolean forced = false;
        if(params.has("forced")) {
            forced = params.getBoolean("forced");
        }

        EMRoomManagerWrapper current = this;
        EMResultCallBack resultCallBack = (EMResultCallBack<Map<String, Integer>>) (code, value) -> asyncRunnable(()->{
            if (value.size() > 0 || code == EMError.EM_NO_ERROR) {
                current.onSuccess(value, callback);
            }else {
                HyphenateException e = new HyphenateException(code, "");
                current.onError(e, callback);
            }
        });
        if (forced){
            EMClient.getInstance().chatroomManager().asyncRemoveChatRoomAttributesFromServerForced(roomId, keys, resultCallBack);
        }else {
            EMClient.getInstance().chatroomManager().asyncRemoveChatRoomAttributesFromServer(roomId, keys, resultCallBack);
        }
    }
    
    private void registerEaseListener() {
        chatRoomChangeListener = new EMChatRoomChangeListener() {

            @Override
            public void onWhiteListAdded(String chatRoomId, List<String> whitelist) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("whitelist", whitelist);
                    data.put("type", "chatroomWhiteListAdded");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onWhiteListRemoved(String chatRoomId, List<String> whitelist) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", chatRoomId);
                    data.put("whitelist", whitelist);
                    data.put("type", "chatroomWhiteListRemoved");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAllMemberMuteStateChanged(String chatRoomId, boolean isMuted) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", chatRoomId);
                    data.put("isMuted", isMuted);
                    data.put("type", "chatroomAllMemberMuteStateChanged");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onChatRoomDestroyed(String roomId, String roomName) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", roomId);
                    data.put("roomName", roomName);
                    data.put("type", "chatroomDestroyed");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMemberJoined(String roomId, String participant) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", roomId);
                    data.put("participant", participant);
                    data.put("type", "chatroomMemberJoined");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMemberExited(String roomId, String roomName, String participant) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", roomId);
                    data.put("roomName", roomName);
                    data.put("participant", participant);
                    data.put("type", "chatroomMemberExited");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onRemovedFromChatRoom(int reason, String roomId, String roomName, String participant) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", roomId);
                    data.put("roomName", roomName);
                    data.put("participant", participant);
                    data.put("type", "chatroomRemovedFromChatRoom");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMuteListAdded(String chatRoomId, List<String> mutes, long expireTime) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("mutes", mutes);
                    data.put("expireTime", String.valueOf(expireTime));
                    data.put("type", "chatroomMuteListAdded");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onMuteListRemoved(String chatRoomId, List<String> mutes) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("mutes", mutes);
                    data.put("type", "chatroomMuteListRemoved");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAdminAdded(String chatRoomId, String admin) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("admin", admin);
                    data.put("type", "chatroomAdminAdded");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAdminRemoved(String chatRoomId, String admin) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("admin", admin);
                    data.put("type", "chatroomAdminRemoved");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
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
                    data.put("type", "chatroomOwnerChanged");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
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
                    data.put("type", "chatroomAnnouncementChanged");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onSpecificationChanged(EMChatRoom room) {
                JSONObject data = new JSONObject();
                try {
                    data.put("room", EMChatRoomHelper.toJson(room));
                    data.put("type", "onSpecificationChanged");
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAttributesUpdate(String chatRoomId, Map<String, String> attributeMap, String from) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", chatRoomId);
                    data.put("type", "chatroomAttributesDidUpdated");
                    data.put("attributes", attributeMap);
                    data.put("fromId", from);
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onAttributesRemoved(String chatRoomId, List<String> keyList, String from) {
                JSONObject data = new JSONObject();

                try {
                    data.put("roomId", chatRoomId);
                    data.put("keys", keyList);
                    data.put("type", "chatroomAttributesDidRemoved");
                    data.put("fromId", from);
                    post(()-> JavaWrapper.listener.onReceive("EMChatRoomChangeListener", EMSDKMethod.chatRoomChange, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().chatroomManager().addChatRoomChangeListener(chatRoomChangeListener);
    }
}
