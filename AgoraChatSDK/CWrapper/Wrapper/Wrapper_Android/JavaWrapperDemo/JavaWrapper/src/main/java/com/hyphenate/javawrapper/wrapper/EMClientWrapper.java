package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMDeviceInfo;
import com.hyphenate.chat.EMOptions;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMHelper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMDeviceInfoHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMGroupHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMOptionsHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMClientWrapper extends EMBaseWrapper {

    public EMChatManagerWrapper chatManagerWrapper;
    public EMContactManagerWrapper contactManagerWrapper;
    public EMRoomManagerWrapper roomManagerWrapper;
    public EMGroupManagerWrapper groupManagerWrapper;
    public EMUserInfoManagerWrapper userInfoManagerWrapper;
    public EMPresenceManagerWrapper presenceManagerWrapper;
    public EMChatThreadManagerWrapper chatThreadManagerWrapper;
    public EMPushManagerWrapper pushManagerWrapper;



    EMClientWrapper() {

    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String str = null;
        switch (method) {
            case EMSDKMethod.init:
                str = init(jsonObject, callback);
                break;
            case EMSDKMethod.createAccount:
                str = createAccount(jsonObject, callback);
                break;
            case EMSDKMethod.login:
                str = login(jsonObject, callback);
                break;
            case EMSDKMethod.logout:
                str = logout(jsonObject, callback);
                break;
            case EMSDKMethod.changeAppKey:
                str = changeAppKey(jsonObject, callback);
                break;
            case EMSDKMethod.uploadLog:
                str = uploadLog(callback);
                break;
            case EMSDKMethod.compressLogs:
                str = compressLogs(callback);
                break;
            case EMSDKMethod.getLoggedInDevicesFromServer:
                str = getLoggedInDevicesFromServer(jsonObject, callback);
                break;
            case EMSDKMethod.kickDevice:
                str = kickDevice(jsonObject, callback);
                break;
            case EMSDKMethod.kickAllDevices:
                str = kickAllDevices(jsonObject, callback);
                break;
            case EMSDKMethod.isLoggedInBefore:
                str = isLoggedInBefore();
                break;
            case EMSDKMethod.getCurrentUser:
                str = getCurrentUser();
                break;
            case EMSDKMethod.loginWithAgoraToken:
                str = loginWithAgoraToken(jsonObject, callback);
                break;
            case EMSDKMethod.getToken:
                str = getToken();
                break;
            case EMSDKMethod.isConnected:
                str = isConnected();
                break;
            case EMSDKMethod.renewToken:
                str = renewToken(jsonObject, callback);
                break;
            case EMSDKMethod.startCallback:
                break;
            default:
                super.onMethodCall(method, jsonObject, callback);
                break;
        }
        return str;
    }


    private String init(JSONObject param, EMWrapperCallback callback) throws JSONException {
        EMOptions options = EMOptionsHelper.fromJson(param, JavaWrapper.context);
        EMClient.getInstance().init(JavaWrapper.context, options);
        EMClient.getInstance().setDebugMode(param.getBoolean("debugModel"));
        bindingManagers();
        registerEaseListener();
        onSuccess(null, callback);
        return null;
    }

    private String createAccount(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("username");
        String password = param.getString("password");
        asyncRunnable(()->{
            try {
                EMClient.getInstance().createAccount(username, password);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String login(JSONObject param, EMWrapperCallback callback) throws JSONException {
        boolean isPwd = param.getBoolean("isPassword");
        String username = param.getString("username");
        String pwdOrToken = param.getString("pwdOrToken");

        if (isPwd){
            EMClient.getInstance().login(username, pwdOrToken, new EMCommonCallback(callback));
        } else {
            EMClient.getInstance().loginWithToken(username, pwdOrToken, new EMCommonCallback(callback));
        }
        return null;
    }


    private String logout(JSONObject param, EMWrapperCallback callback) throws JSONException {
        boolean unbindToken = param.getBoolean("unbindToken");
        EMClient.getInstance().logout(unbindToken, new EMCommonCallback(callback));
        return null;
    }

    private String changeAppKey(JSONObject param, EMWrapperCallback callback) throws JSONException{
        String appKey = param.getString("appKey");
        asyncRunnable(()-> {
            try {
                EMClient.getInstance().changeAppkey(appKey);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String getCurrentUser() throws JSONException {
        return EMHelper.getReturnJsonObject(EMClient.getInstance().getCurrentUser()).toString();
    }

    private String loginWithAgoraToken(JSONObject param, EMWrapperCallback callback) throws JSONException {

        String username = param.getString("username");
        String agoraToken = param.getString("agora_token");
        EMClient.getInstance().loginWithAgoraToken(username, agoraToken, new EMCommonCallback(callback));
        return null;
    }
    private String getToken() throws JSONException
    {
        return EMHelper.getReturnJsonObject(EMClient.getInstance().getAccessToken()).toString();
    }

    private String isLoggedInBefore() throws JSONException {
        return EMHelper.getReturnJsonObject(EMClient.getInstance().isLoggedInBefore()).toString();
    }

    private String isConnected() throws JSONException{
        return EMHelper.getReturnJsonObject(EMClient.getInstance().isConnected()).toString();
    }

    private String uploadLog(EMWrapperCallback callback) {
        EMClient.getInstance().uploadLog(new EMCommonCallback(callback));
        return null;
    }

    private String compressLogs(EMWrapperCallback callback) {
        asyncRunnable(()->{
            try {
                String path = EMClient.getInstance().compressLogs();
                onSuccess(path, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String kickDevice(JSONObject param, EMWrapperCallback callback) throws JSONException {

        String username = param.getString("username");
        String password = param.getString("password");
        String resource = param.getString("resource");
        asyncRunnable(()->{
            try {
                EMClient.getInstance().kickDevice(username, password, resource);
                onSuccess(true, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String kickAllDevices(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("username");
        String password = param.getString("password");

        asyncRunnable(()->{
            try {
                EMClient.getInstance().kickAllDevices(username, password);
                onSuccess(true, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String renewToken(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String agoraToken = param.getString("agora_token");
        asyncRunnable(()->{
            EMClient.getInstance().renewToken(agoraToken);
            onSuccess(null, callback);
        });
        return null;
    }

    private String getLoggedInDevicesFromServer(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("username");
        String password = param.getString("password");
        asyncRunnable(()->{
            try {
                List<EMDeviceInfo> devices = EMClient.getInstance().getLoggedInDevicesFromServer(username, password);
                JSONArray jsonArray = new JSONArray();
                try {
                    for (EMDeviceInfo info: devices) {
                        jsonArray.put(EMDeviceInfoHelper.toJson(info));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }
                finally {
                    onSuccess(jsonArray, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }


    private void registerEaseListener() {

        EMMultiDeviceListener multiDeviceListener = new EMMultiDeviceListener() {
            @Override
            public void onContactEvent(int event, String target, String ext) {
                JSONObject data = new JSONObject();
                try {
                    data.put("event", Integer.valueOf(event));
                    data.put("target", target);
                    data.put("ext", ext);
                    post(() -> JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onContactEvent", data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onGroupEvent(int event, String target, List<String> userNames) {
                JSONObject data = new JSONObject();
                try {
                    data.put("event", Integer.valueOf(event));
                    data.put("target", target);
                    data.put("users", userNames);
                    post(() -> JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onGroupEvent", data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            public void onChatThreadEvent(int event, String target, List<String> usernames) {
                JSONObject data = new JSONObject();
                try {
                    data.put("event", Integer.valueOf(event));
                    data.put("target", target);
                    data.put("users", usernames);
                    post(() -> JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onChatThreadEvent", data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMConnectionListener connectionListener = new EMConnectionListener() {
            @Override
            public void onConnected() {
                JSONObject data = new JSONObject();
                try {
                    data.put("connected", Boolean.TRUE);
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onContactEvent", data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onLogout(int errorCode) {

            }

            @Override
            public void onDisconnected(int errorCode) {
                if (errorCode == 206) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidLoginFromOtherDevice", null));
                } else if (errorCode == 207) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidRemoveFromServer", null));
                } else if (errorCode == 305) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidForbidByServer", null));
                } else if (errorCode == 216) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidChangePassword", null));
                } else if (errorCode == 214) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidLoginTooManyDevice", null));
                } else if (errorCode == 217) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserKickedByOtherDevice", null));
                } else if (errorCode == 202) {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onUserAuthenticationFailed", null));
                } else {
                    post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onDisconnected", null));
                }
            }

            @Override
            public void onTokenExpired() {
                post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onTokenDidExpire", null));
            }

            @Override
            public void onTokenWillExpire() {
                post(() -> JavaWrapper.listener.onReceive("EMConnectionListener", "onTokenWillExpire", null));
            }
        };

        EMClient.getInstance().addConnectionListener(connectionListener);
        EMClient.getInstance().addMultiDeviceListener(multiDeviceListener);

    }

    private void bindingManagers() {
        chatManagerWrapper = new EMChatManagerWrapper();
        contactManagerWrapper = new EMContactManagerWrapper();
        roomManagerWrapper = new EMRoomManagerWrapper();
        groupManagerWrapper = new EMGroupManagerWrapper();
        userInfoManagerWrapper = new EMUserInfoManagerWrapper();
        presenceManagerWrapper = new EMPresenceManagerWrapper();
        chatThreadManagerWrapper = new EMChatThreadManagerWrapper();
        pushManagerWrapper = new EMPushManagerWrapper();
    }
}
