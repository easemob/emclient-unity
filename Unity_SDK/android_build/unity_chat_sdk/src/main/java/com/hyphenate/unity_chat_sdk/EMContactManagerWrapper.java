package com.hyphenate.unity_chat_sdk;

import android.util.Log;

import com.hyphenate.chat.EMClient;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMTransformHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityContactListener;

import org.json.JSONArray;
import org.json.JSONException;

import java.util.List;

public class EMContactManagerWrapper extends  EMWrapper {

    static public EMContactManagerWrapper wrapper() {
        return new EMContactManagerWrapper();
    }

    public EMContactManagerWrapper() {
        EMClient.getInstance().contactManager().setContactListener(new EMUnityContactListener());
    }

    private void addContact(String username, String reason, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().addContact(username, reason);
                onSuccess(null, callbackId,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void deleteContact(String username, boolean keepConversation, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().deleteContact(username, keepConversation);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getAllContactsFromServer(String callbackId) throws JSONException {
        Log.d("chat_sdk", "getAllContactsFromServer");
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getAllContactsFromServer();
                Log.d("chat_sdk", contacts.toString());
                onSuccess("List<String>", callbackId, EMTransformHelper.jsonArrayFromStringList(contacts).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getAllContactsFromDB(String callbackId) throws JSONException {
        Log.d("chat_sdk", "getAllContactsFromDB");
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getContactsFromLocal();
                Log.d("chat_sdk", contacts.toString());
                onSuccess("List<String>", callbackId, EMTransformHelper.jsonArrayFromStringList(contacts).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void addUserToBlockList(String username, String callbackId) throws JSONException {
        Log.d("chat_sdk", "addUserToBlockList");
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().addUserToBlackList(username, false);
                onSuccess(null,callbackId,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void removeUserFromBlockList(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().removeUserFromBlackList(username);
                onSuccess(null, callbackId,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getBlockListFromServer(String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getBlackListFromServer();
                onSuccess("List<String>", callbackId, EMTransformHelper.jsonArrayFromStringList(contacts).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void acceptInvitation(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().acceptInvitation(username);
                onSuccess(null,callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void declineInvitation(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            if (username == null || username.length() == 0) {
                onError(callbackId, new HyphenateException(501, "username invalid"));
                return;
            }
            try {
                EMClient.getInstance().contactManager().declineInvitation(username);
                onSuccess(null, callbackId,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getSelfIdsOnOtherPlatform(String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                List<String> platforms = EMClient.getInstance().contactManager().getSelfIdsOnOtherPlatform();
                onSuccess("List<String>", callbackId, EMTransformHelper.jsonArrayFromStringList(platforms).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
}
