package com.hyphenate.wrapper;

import com.hyphenate.EMChatRoomChangeListener;
import com.hyphenate.EMError;
import com.hyphenate.EMResultCallBack;
import com.hyphenate.chat.EMChatRoom;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroup;
import com.hyphenate.chat.EMPageResult;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.helper.EMGroupHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMChatRoomHelper;
import com.hyphenate.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.wrapper.helper.EMPageResultHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMRoomManagerWrapper extends EMBaseWrapper{

    EMRoomManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method,  JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.joinChatRoom.equals(method)) { 
            ret = joinChatRoom(jsonObject, callback);
        } else if (EMSDKMethod.leaveChatRoom.equals(method)) {
            ret = leaveChatRoom(jsonObject, callback);
        } else if (EMSDKMethod.fetchPublicChatRoomsFromServer.equals(method)) {
            ret = fetchPublicChatRoomsFromServer(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomInfoFromServer.equals(method)) {
            ret = fetchChatRoomInfoFromServer(jsonObject, callback);
        } else if (EMSDKMethod.getChatRoom.equals(method)) {
            ret = getChatRoom(jsonObject);
        } else if (EMSDKMethod.getAllChatRooms.equals(method)) {
            ret = getAllChatRooms();
        } else if (EMSDKMethod.createChatRoom.equals(method)) {
            ret = createChatRoom(jsonObject, callback);
        } else if (EMSDKMethod.destroyChatRoom.equals(method)) {
            ret = destroyChatRoom(jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomSubject.equals(method)) {
            ret = changeChatRoomSubject(jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomDescription.equals(method)) {
            ret = changeChatRoomDescription(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomMembers.equals(method)) {
            ret = fetchChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.muteChatRoomMembers.equals(method)) {
            ret = muteChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.unMuteChatRoomMembers.equals(method)) {
            ret = unMuteChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.changeChatRoomOwner.equals(method)) {
            ret = changeChatRoomOwner(jsonObject, callback);
        } else if (EMSDKMethod.addChatRoomAdmin.equals(method)) {
            ret = addChatRoomAdmin(jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomAdmin.equals(method)) {
            ret = removeChatRoomAdmin(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomMuteList.equals(method)) {
            ret = fetchChatRoomMuteList(jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomMembers.equals(method)) {
            ret = removeChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.blockChatRoomMembers.equals(method)) {
            ret = blockChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.unBlockChatRoomMembers.equals(method)) {
            ret = unBlockChatRoomMembers(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomBlockList.equals(method)) {
            ret = fetchChatRoomBlockList(jsonObject, callback);
        } else if (EMSDKMethod.updateChatRoomAnnouncement.equals(method)) {
            ret = updateChatRoomAnnouncement(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomAnnouncement.equals(method)) {
            ret = fetchChatRoomAnnouncement(jsonObject, callback);
        } else if (EMSDKMethod.addMembersToChatRoomWhiteList.equals(method)) {
            ret = addMembersToChatRoomWhiteList(jsonObject, callback);
        } else if (EMSDKMethod.removeMembersFromChatRoomWhiteList.equals(method)) {
            ret = removeMembersFromChatRoomWhiteList(jsonObject, callback);
        } else if (EMSDKMethod.isMemberInChatRoomWhiteListFromServer.equals(method)) {
            ret = isMemberInChatRoomWhiteListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomWhiteListFromServer.equals(method)) {
            ret = fetchChatRoomWhiteListFromServer(jsonObject, callback);
        } else if (EMSDKMethod.muteAllChatRoomMembers.equals(method)) {
            ret = muteAllChatRoomsMembers(jsonObject, callback);
        } else if (EMSDKMethod.unMuteAllChatRoomMembers.equals(method)) {
            ret = unMuteAllChatRoomsMembers(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatRoomAttributes.equals(method)){
            ret = fetchChatRoomAttributes(jsonObject, callback);
        } else if (EMSDKMethod.setChatRoomAttributes.equals(method)){
            ret = setChatRoomAttributes(jsonObject, callback);
        } else if (EMSDKMethod.removeChatRoomAttributes.equals(method)){
            ret = removeChatRoomAttributes(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }
    
    private String joinChatRoom(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().joinChatRoom(roomId, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String leaveChatRoom(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            EMClient.getInstance().chatroomManager().leaveChatRoom(roomId);
            onSuccess(true, callback);
        });
        return null;
    }

    private String fetchPublicChatRoomsFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        int pageNum = params.getInt("pageNum");
        int pageSize = params.getInt("pageSize");

        EMClient.getInstance().chatroomManager().asyncFetchPublicChatRoomsFromServer(pageNum, pageSize,
                new EMCommonValueCallback<EMPageResult<EMChatRoom>>(callback) {
                    @Override
                    public void onSuccess(EMPageResult<EMChatRoom> object) {
                        JSONObject jsonObject = null;
                        try {
                            jsonObject = EMPageResultHelper.toJson(object);
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }finally {
                            updateObject(jsonObject);
                        }
                    }

                    @Override
                    public void onError(int error, String errorMsg) {
                        super.onError(error, errorMsg);
                    }
                });
        return null;
    }

    private String fetchChatRoomInfoFromServer(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        boolean fetchMembers = params.getBoolean("fetchMembers");
        asyncRunnable(() -> {
            EMChatRoom room;
            try {
                if (fetchMembers) {
                    room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId, true);
                }else {
                    room = EMClient.getInstance().chatroomManager().fetchChatRoomFromServer(roomId);
                }
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException error) {
                onError(error, callback);
            }
        });
        return null;
    }

    private String getChatRoom(JSONObject params) throws JSONException {
        String roomId = params.getString("roomId");
        EMChatRoom room = EMClient.getInstance().chatroomManager().getChatRoom(roomId);
        return EMHelper.getReturnJsonObject(EMChatRoomHelper.toJson(room)).toString();
    }

    private String getAllChatRooms() throws JSONException {
        List<EMChatRoom> list = EMClient.getInstance().chatroomManager().getAllChatRooms();
        JSONArray jsonArray = new JSONArray();
        for (EMChatRoom room : list) {
            jsonArray.put(EMChatRoomHelper.toJson(room));
        }
        return EMHelper.getReturnJsonObject(jsonArray).toString();
    }

    private String createChatRoom(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String subject = params.getString("name");
        int maxUserCount = params.getInt("count");
        String description = null;
        if (params.has("desc")){
            description = params.getString("desc");
        }
        String welcomeMessage = null;
        if (params.has("msg")){
            welcomeMessage = params.getString("msg");
        }
        List<String> membersList = new ArrayList<>();
        JSONArray members;
        if (params.has("userIds")){
            members = params.getJSONArray("userIds");
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
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String destroyChatRoom(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatroomManager().destroyChatRoom(roomId);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String changeChatRoomSubject(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String subject = params.getString("name");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatRoomSubject(roomId, subject);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String changeChatRoomDescription(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String description = params.getString("desc");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeChatroomDescription(roomId,
                        description);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String fetchChatRoomMembers(JSONObject params, EMWrapperCallback callback)
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
                JSONObject jo = null;
                try{
                    jo = EMCursorResultHelper.toJson(cursorResult);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String muteChatRoomMembers(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        int duration = -1;
        if (params.has("expireTime")){
            duration = params.getInt("expireTime");
        }
        List<String> members = null;
        if (params.has("userIds")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("userIds"));
        }
        EMCommonValueCallback<EMChatRoom> callBack = new EMCommonValueCallback<EMChatRoom>(callback) {
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().chatroomManager().asyncMuteChatRoomMembers(roomId, members,
                duration, callBack);

        return null;
    }

    private String unMuteChatRoomMembers(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray muteMembers = params.getJSONArray("userIds");
        List<String> unMuteMembersList = new ArrayList<>();
        for (int i = 0; i < muteMembers.length(); i++) {
            unMuteMembersList.add((String) muteMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unMuteChatRoomMembers(roomId,
                        unMuteMembersList);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String changeChatRoomOwner(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String newOwner = params.getString("userId");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().changeOwner(roomId, newOwner);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String addChatRoomAdmin(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String admin = params.getString("userId");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().addChatRoomAdmin(roomId, admin);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String removeChatRoomAdmin(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        String admin = params.getString("userId");

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomAdmin(roomId, admin);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String fetchChatRoomMuteList(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        int pageNum = params.getInt("pageNum");
        int pageSize = params.getInt("pageSize");

        asyncRunnable(() -> {
            try {
                Map<String, Long> map = EMClient.getInstance().chatroomManager().fetchChatRoomMuteList(roomId, pageNum, pageSize);
                JSONObject jsonObject = new JSONObject();
                try {
                    for (Map.Entry<String, Long> entry: map.entrySet()) {
                        jsonObject.put(entry.getKey(), entry.getValue());
                    }
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

    private String removeChatRoomMembers(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray members = params.getJSONArray("userIds");
        List<String> membersList = new ArrayList<>();
        for (int i = 0; i < members.length(); i++) {
            membersList.add((String) members.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().removeChatRoomMembers(roomId, membersList);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String blockChatRoomMembers(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray blockMembers = params.getJSONArray("userIds");
        List<String> blockMembersList = new ArrayList<>();
        for (int i = 0; i < blockMembers.length(); i++) {
            blockMembersList.add((String) blockMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().blockChatroomMembers(roomId,
                        blockMembersList);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String unBlockChatRoomMembers(JSONObject params, EMWrapperCallback callback)
            throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray blockMembers = params.getJSONArray("userIds");
        List<String> blockMembersList = new ArrayList<>();
        for (int i = 0; i < blockMembers.length(); i++) {
            blockMembersList.add((String) blockMembers.get(i));
        }

        asyncRunnable(() -> {
            try {
                EMChatRoom room = EMClient.getInstance().chatroomManager().unblockChatRoomMembers(roomId,
                        blockMembersList);
                JSONObject jo = null;
                try {
                    jo = EMChatRoomHelper.toJson(room);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String fetchChatRoomBlockList(JSONObject params, EMWrapperCallback callback)
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
        return null;
    }

    private String updateChatRoomAnnouncement(JSONObject params, EMWrapperCallback callback)
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
        return null;
    }

    private String fetchChatRoomAnnouncement(JSONObject params, EMWrapperCallback callback)
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
        return null;
    }

    private String addMembersToChatRoomWhiteList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray jsonAry = params.getJSONArray("userIds");
        List<String> members = new ArrayList<>();
        for (int i = 0; i < jsonAry.length(); i++) {
            members.add((String) jsonAry.get(i));
        }

        EMClient.getInstance().chatroomManager().addToChatRoomWhiteList(roomId, members, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String removeMembersFromChatRoomWhiteList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        JSONArray jsonAry = params.getJSONArray("userIds");
        List<String> members = new ArrayList<>();
        for (int i = 0; i < jsonAry.length(); i++) {
            members.add((String) jsonAry.get(i));
        }

        EMClient.getInstance().chatroomManager().removeFromChatRoomWhiteList(roomId, members, new EMCommonValueCallback<EMChatRoom>(callback){
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String isMemberInChatRoomWhiteListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().checkIfInChatRoomWhiteList(roomId, new EMCommonValueCallback<>(callback));
        return null;
    }

    private String fetchChatRoomWhiteListFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().fetchChatRoomWhiteList(roomId,  new EMCommonValueCallback<List<String>>(callback) {
            @Override
            public void onSuccess(List<String> object) {
                updateObject(EMHelper.stringListToJsonArray(object));
            }
        });
        return null;
    }

    private String muteAllChatRoomsMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().muteAllMembers(roomId, new EMCommonValueCallback<EMChatRoom>(callback) {
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String unMuteAllChatRoomsMembers(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        EMClient.getInstance().chatroomManager().unmuteAllMembers(roomId, new EMCommonValueCallback<EMChatRoom>(callback) {
            @Override
            public void onSuccess(EMChatRoom object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMChatRoomHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String fetchChatRoomAttributes(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        List<String> keys = new ArrayList<>();
        if (params.has("list")){
            JSONArray array = params.getJSONArray("list");
            for (int i = 0; i < array.length(); i++) {
                keys.add(array.getString(i));
            }
        }
        EMClient.getInstance().chatroomManager().asyncFetchChatroomAttributesFromServer(roomId, keys, new EMCommonValueCallback<Map<String,String>>(callback) {
            @Override
            public void onSuccess(Map<String,String> object) {
                JSONObject jsonObject = null;
                try{
                    jsonObject = EMHelper.stringMapToJsonObject(object);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String setChatRoomAttributes(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        Map<String, String> attributes = new HashMap<>();
        if (params.has("attributes")) {
            JSONObject jsonObject = params.getJSONObject("attributes");
            Iterator<String> iterator = jsonObject.keys();
            while (iterator.hasNext()) {
                String key = iterator.next();
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

        EMResultCallBack<Map<String, Integer>> resultCallBack = (code, value) -> asyncRunnable(()->{
            if (value.size() > 0 || code == EMError.EM_NO_ERROR) {
                JSONObject jsonObject = null;
                try{
                    jsonObject = EMHelper.intMapToJsonObject(value);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    current.onSuccess(jsonObject, callback);
                }
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
        return null;
    }

    private String removeChatRoomAttributes(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String roomId = params.getString("roomId");
        List<String> keys = new ArrayList<>();
        if (params.has("list")){
            JSONArray array = params.getJSONArray("list");
            for (int i = 0; i < array.length(); i++) {
                keys.add(array.getString(i));
            }
        }

        boolean forced = false;
        if(params.has("forced")) {
            forced = params.getBoolean("forced");
        }

        EMRoomManagerWrapper current = this;
        EMResultCallBack<Map<String, Integer>> resultCallBack = (code, value) -> asyncRunnable(()->{
            if (value.size() > 0 || code == EMError.EM_NO_ERROR) {
                JSONObject jsonObject = null;
                try{
                    jsonObject = EMHelper.intMapToJsonObject(value);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    current.onSuccess(jsonObject, callback);
                }
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
        return null;
    }
    
    private void registerEaseListener() {
        EMChatRoomChangeListener chatRoomChangeListener = new EMChatRoomChangeListener() {

            @Override
            public void onWhiteListAdded(String chatRoomId, List<String> whitelist) {
                JSONObject data = new JSONObject();
                try {
                    data.put("roomId", chatRoomId);
                    data.put("userIds", whitelist);
                    post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAddWhiteListMembersFromRoom, data.toString()));
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
                    post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onRemoveWhiteListMembersFromRoom, data.toString()));
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
                    data.put("roomName", roomName);
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
                    data.put("reason", reason);
                    post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onRemovedFromRoom, data.toString()));
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
                    data.put("from", from);
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
                    data.put("from", from);
                    post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.chatRoomListener, EMSDKMethod.onAttributesRemovedFromRoom, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().chatroomManager().addChatRoomChangeListener(chatRoomChangeListener);
    }
}
