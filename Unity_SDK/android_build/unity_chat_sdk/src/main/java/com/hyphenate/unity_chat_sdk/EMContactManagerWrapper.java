package com.hyphenate.unity_chat_sdk;

import android.util.Log;

import com.hyphenate.chat.EMClient;
import com.hyphenate.exceptions.HyphenateException;
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
            try {
                EMClient.getInstance().contactManager().addContact(username, reason);
                onSuccess(callbackId, null,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void deleteContact(String username, boolean keepConversation, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().deleteContact(username, keepConversation);
                onSuccess(callbackId, null,null);
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
                JSONArray jsonAry = new JSONArray();
                for (String contact : contacts) {
                    jsonAry.put(contact);
                }
                onSuccess(callbackId, "List<String>",jsonAry.toString());
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
                JSONArray jsonAry = new JSONArray();
                for (String contact : contacts) {
                    jsonAry.put(contact);
                }
                onSuccess(callbackId, "List<String>",jsonAry.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void addUserToBlockList(String username, String callbackId) throws JSONException {
        Log.d("chat_sdk", "addUserToBlockList");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().addUserToBlackList(username, false);
                onSuccess(callbackId, null,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void removeUserFromBlockList(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().removeUserFromBlackList(username);
                onSuccess(callbackId, null,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getBlockListFromServer(String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                List<String> contacts = EMClient.getInstance().contactManager().getBlackListFromServer();
                JSONArray jsonAry = new JSONArray();
                for (String contact : contacts) {
                    jsonAry.put(contact);
                }
                onSuccess(callbackId, "List<String>",jsonAry.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void acceptInvitation(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().acceptInvitation(username);
                onSuccess(callbackId, null,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void declineInvitation(String username, String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().declineInvitation(username);
                onSuccess(callbackId, null,null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void getSelfIdsOnOtherPlatform(String callbackId) throws JSONException {
        asyncRunnable(() -> {
            try {
                List<String> platforms = EMClient.getInstance().contactManager().getSelfIdsOnOtherPlatform();
                JSONArray jsonAry = new JSONArray();
                for (String contact : platforms) {
                    jsonAry.put(contact);
                }
                onSuccess(callbackId, "List<String>",jsonAry.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }
}
