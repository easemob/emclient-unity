package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMContactListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.util.EMHelper;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMContactManagerWrapper extends EMBaseWrapper{

    private EMContactListener contactListener;

    EMContactManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        if (EMSDKMethod.addContact.equals(method)) {
            addContact(method, jsonObject, callback);
        } else if (EMSDKMethod.deleteContact.equals(method)) {
            deleteContact(method, jsonObject, callback);
        } else if (EMSDKMethod.getAllContactsFromServer.equals(method)) {
            getAllContactsFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.getAllContactsFromDB.equals(method)) {
            getAllContactsFromDB(method, jsonObject, callback);
        } else if (EMSDKMethod.addUserToBlockList.equals(method)) {
            addUserToBlockList(method, jsonObject, callback);
        } else if (EMSDKMethod.removeUserFromBlockList.equals(method)) {
            removeUserFromBlockList(method, jsonObject, callback);
        } else if (EMSDKMethod.getBlockListFromServer.equals(method)) {
            getBlockListFromServer(method, jsonObject, callback);
        } else if (EMSDKMethod.getBlockListFromDB.equals(method)) {
            getBlockListFromDB(method, jsonObject, callback);
        } else if (EMSDKMethod.acceptInvitation.equals(method)) {
            acceptInvitation(method, jsonObject, callback);
        } else if (EMSDKMethod.declineInvitation.equals(method)) {
            declineInvitation(method, jsonObject, callback);
        } else if (EMSDKMethod.getSelfIdsOnOtherPlatform.equals(method)) {
            getSelfIdsOnOtherPlatform(method, jsonObject, callback);
        }
        return null;
    }

    private void addContact(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        String reason = null;
        if(params.has("reason")) {
            reason = params.getString("reason");
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
    }

    private void deleteContact(String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        boolean keepConversation = params.getBoolean("keepConversation");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().deleteContact(username, keepConversation);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void getAllContactsFromServer( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(() -> {
            try {
                List contacts = EMClient.getInstance().contactManager().getAllContactsFromServer();
                onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void getAllContactsFromDB( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(() -> {
            try {
                List contacts = EMClient.getInstance().contactManager().getContactsFromLocal();
                onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void addUserToBlockList( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().addUserToBlackList(username, false);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void removeUserFromBlockList( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().removeUserFromBlackList(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void getBlockListFromServer( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(() -> {
            try {
                List contacts = EMClient.getInstance().contactManager().getBlackListFromServer();
                onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void getBlockListFromDB( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        List contacts = EMClient.getInstance().contactManager().getBlackListUsernames();
        onSuccess(EMHelper.stringListToJsonArray(contacts), callback);
    }

    private void acceptInvitation( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().acceptInvitation(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void declineInvitation( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = params.getString("username");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().contactManager().declineInvitation(username);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void getSelfIdsOnOtherPlatform( String method, JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(() -> {
            try {
                List platforms = EMClient.getInstance().contactManager().getSelfIdsOnOtherPlatform();
                onSuccess(EMHelper.stringListToJsonArray(platforms), callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void registerEaseListener() {
        contactListener = new EMContactListener() {
            @Override
            public void onContactAdded(String userName) {
                JSONObject data = new JSONObject();
                try {
                    data.put("type", "onContactAdded");
                    data.put("username", userName);
                    post(()-> JavaWrapper.listener.onReceive("EMContactListener", EMSDKMethod.onContactChanged, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onContactDeleted(String userName) {
                JSONObject data = new JSONObject();
                try {
                    data.put("type", "onContactDeleted");
                    data.put("username", userName);
                    post(()-> JavaWrapper.listener.onReceive("EMContactListener", EMSDKMethod.onContactChanged, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onContactInvited(String userName, String reason) {
                JSONObject data = new JSONObject();
                try {
                    data.put("type", "onContactInvited");
                    data.put("username", userName);
                    data.put("reason", reason);
                    post(()-> JavaWrapper.listener.onReceive("EMContactListener", EMSDKMethod.onContactChanged, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onFriendRequestAccepted(String userName) {
                JSONObject data = new JSONObject();
                try {
                    data.put("type", "onFriendRequestAccepted");
                    data.put("username", userName);
                    post(()-> JavaWrapper.listener.onReceive("EMContactListener", EMSDKMethod.onContactChanged, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onFriendRequestDeclined(String userName) {
                JSONObject data = new JSONObject();
                try {
                    data.put("type", "onFriendRequestDeclined");
                    data.put("username", userName);
                    post(()-> JavaWrapper.listener.onReceive("EMContactListener", EMSDKMethod.onContactChanged, data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().contactManager().setContactListener(contactListener);
    }
}
