package com.hyphenate.wrapper;

import com.hyphenate.EMContactListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.listeners.EMWrapperContactListener;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.callback.EMWrapperCallback;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMContactManagerWrapper extends EMBaseWrapper{

    public EMWrapperContactListener wrapperContactListener;

    EMContactManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.addContact.equals(method)) {
            ret = addContact(jsonObject, callback);
        } else if (EMSDKMethod.deleteContact.equals(method)) {
            ret = deleteContact(jsonObject, callback);
        } else if (EMSDKMethod.getAllContactsFromServer.equals(method)) {
            ret = getAllContactsFromServer(callback);
        } else if (EMSDKMethod.getAllContactsFromDB.equals(method)) {
            ret = getAllContactsFromDB(callback);
        } else if (EMSDKMethod.addUserToBlockList.equals(method)) {
            ret = addUserToBlockList(jsonObject, callback);
        } else if (EMSDKMethod.removeUserFromBlockList.equals(method)) {
            ret = removeUserFromBlockList(jsonObject, callback);
        } else if (EMSDKMethod.getBlockListFromServer.equals(method)) {
            ret = getBlockListFromServer(callback);
        } else if (EMSDKMethod.getBlockListFromDB.equals(method)) {
            ret = getBlockListFromDB(callback);
        } else if (EMSDKMethod.acceptInvitation.equals(method)) {
            ret = acceptInvitation(jsonObject, callback);
        } else if (EMSDKMethod.declineInvitation.equals(method)) {
            ret = declineInvitation(jsonObject, callback);
        } else if (EMSDKMethod.getSelfIdsOnOtherPlatform.equals(method)) {
            ret = getSelfIdsOnOtherPlatform(callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String addContact(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        String reason = null;
        if(params.has("msg")) {
            reason = params.getString("msg");
        }
        String finalReason = reason;
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().addContact(username, finalReason);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String deleteContact(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        boolean keepConversation = params.getBoolean("keepConversation");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().deleteContact(username, keepConversation);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getAllContactsFromServer(EMWrapperCallback callback) {
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getAllContactsFromServer();
                onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getAllContactsFromDB(EMWrapperCallback callback) throws JSONException{
        List<String> contacts = null;
        try {
            contacts = EMClient.getInstance().contactManager().getContactsFromLocal();
        }catch (HyphenateException e) {

        }
        return EMHelper.getReturnJsonObject(contacts).toString();
    }

    private String addUserToBlockList( JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().addUserToBlackList(username, false);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String removeUserFromBlockList( JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().removeUserFromBlackList(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getBlockListFromServer(EMWrapperCallback callback) {
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getBlackListFromServer();
                onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getBlockListFromDB(EMWrapperCallback callback) throws JSONException{
        List<String> contacts = EMClient.getInstance().contactManager().getBlackListUsernames();
        return EMHelper.getReturnJsonObject(EMHelper.stringListToJsonArray(contacts)).toString();
    }

    private String acceptInvitation( JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().acceptInvitation(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String declineInvitation( JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("userId");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().declineInvitation(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getSelfIdsOnOtherPlatform(EMWrapperCallback callback) {
        asyncRunnable(() -> {
            try {
                List<String> platforms = EMClient.getInstance().contactManager().getSelfIdsOnOtherPlatform();
                onSuccess(EMHelper.stringListToJsonArray(platforms), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private void registerEaseListener() {
        wrapperContactListener = new EMWrapperContactListener();
        EMClient.getInstance().contactManager().setContactListener(wrapperContactListener);
    }
}
