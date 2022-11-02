package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMPresenceListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMPresence;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMHelper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMPresenceHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class EMPresenceManagerWrapper extends EMBaseWrapper{
    EMPresenceManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String str = null;
        if (EMSDKMethod.presenceWithDescription.equals(method)) {
            str = publishPresenceWithDescription(jsonObject, callback);
        } else if (EMSDKMethod.presenceSubscribe.equals(method)) {
            str = subscribe(jsonObject, callback);
        } else if (EMSDKMethod.presenceUnsubscribe.equals(method)) {
            str = unsubscribe(jsonObject, callback);
        } else if (EMSDKMethod.fetchPresenceStatus.equals(method)) {
            str = fetchPresenceStatus(jsonObject, callback);
        } else if (EMSDKMethod.fetchSubscribedMembersWithPageNum.equals(method)) {
            str = fetchSubscribedMembersWithPageNum(jsonObject, callback);
        } else {
            str = super.onMethodCall(method, jsonObject, callback);
        }
        return str;
    }

    private String publishPresenceWithDescription(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String desc = params.getString("desc");
        EMClient.getInstance().presenceManager().publishPresence(desc, new EMCommonCallback(callback));
        return null;
    }

    private String subscribe(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<String> members = new ArrayList<>();
        if (params.has("members")){
            JSONArray array = params.getJSONArray("members");
            for (int i = 0; i < array.length(); i++) {
                members.add(array.getString(i));
            }
        }
        int expiry = 0;
        if (params.has("expiry")) {
            expiry = params.getInt("expiry");
        }

        EMCommonValueCallback<List<EMPresence>> callBack = new EMCommonValueCallback<List<EMPresence>>(callback) {
            @Override
            public void onSuccess(List<EMPresence> object) {
                JSONArray jsonArray = new JSONArray();
                try{
                    for (EMPresence presence: object) {
                        jsonArray.put(EMPresenceHelper.toJson(presence));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonArray);
                }
            }
        };

        EMClient.getInstance().presenceManager().subscribePresences(members, expiry, callBack);
        return null;
    }

    private String unsubscribe(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }
        EMClient.getInstance().presenceManager().unsubscribePresences(members, new EMCommonCallback(callback));
        return null;
    }

    private String fetchSubscribedMembersWithPageNum(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = params.getInt("pageSize");
        int pageNum = params.getInt("pageNum");
        EMClient.getInstance().presenceManager().fetchSubscribedMembers(pageNum, pageSize, new EMCommonValueCallback<List<String>>(callback){
            @Override
            public void onSuccess(List<String> object) {
                updateObject(EMHelper.stringListToJsonArray(object));
            }
        });
        return null;
    }

    private String fetchPresenceStatus(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<String> members = null;
        if (params.has("members")){
            members = EMHelper.stringListFromJsonArray(params.getJSONArray("members"));
        }
        EMClient.getInstance().presenceManager().fetchPresenceStatus(members, new EMCommonValueCallback<List<EMPresence>>(callback){
            @Override
            public void onSuccess(List<EMPresence> object) {
                JSONArray jsonArray = new JSONArray();
                try{
                    for (EMPresence presence: object) {
                        jsonArray.put(EMPresenceHelper.toJson(presence));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonArray);
                }
            }
        });
        return null;
    }
    
    private void registerEaseListener(){
        EMPresenceListener presenceListener = presences -> {
            JSONArray jsonArray = new JSONArray();
            try{
                for (EMPresence presence: presences) {
                    jsonArray.put(EMPresenceHelper.toJson(presence));
                }
                post(() -> JavaWrapper.listener.onReceive("EMPresenceListener", EMSDKMethod.onPresenceStatusChanged, jsonArray.toString()));
            }catch (JSONException e) {
                e.printStackTrace();
            }
        };
        EMClient.getInstance().presenceManager().addListener(presenceListener);
    }
}
