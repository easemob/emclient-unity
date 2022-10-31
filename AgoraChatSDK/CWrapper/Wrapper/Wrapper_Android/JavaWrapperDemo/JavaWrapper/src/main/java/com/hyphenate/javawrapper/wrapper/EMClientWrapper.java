package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMDeviceInfo;
import com.hyphenate.chat.EMOptions;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMDeviceInfoHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMOptionsHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;

public class EMClientWrapper extends EMBaseWrapper {

    public EMContactManagerWrapper contactManagerWrapper;
    private EMRoomManagerWrapper roomManagerWrapper;

    private EMMultiDeviceListener multiDeviceListener;
    private EMConnectionListener connectionListener;


    EMClientWrapper() {

    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String str = null;
        if (method.equals(EMSDKMethod.init)) {
            init(method, jsonObject, callback);
        }
        else if (EMSDKMethod.createAccount.equals(method))
        {
            createAccount(method, jsonObject, callback);
        }
        else if (EMSDKMethod.login.equals(method))
        {
            login(method, jsonObject, callback);
        }
        else if (EMSDKMethod.logout.equals(method))
        {
            logout(method, jsonObject, callback);
        }
        else if (EMSDKMethod.changeAppKey.equals(method))
        {
            changeAppKey(method, jsonObject, callback);
        }
        else if (EMSDKMethod.uploadLog.equals(method))
        {
            uploadLog(method, jsonObject, callback);
        }
        else if (EMSDKMethod.compressLogs.equals(method))
        {
            compressLogs(method, jsonObject, callback);
        }
        else if (EMSDKMethod.getLoggedInDevicesFromServer.equals(method))
        {
            getLoggedInDevicesFromServer(method, jsonObject, callback);
        }
        else if (EMSDKMethod.kickDevice.equals(method))
        {
            kickDevice(method, jsonObject, callback);
        }
        else if (EMSDKMethod.kickAllDevices.equals(method))
        {
            kickAllDevices(method, jsonObject, callback);
        }
        else if (EMSDKMethod.isLoggedInBefore.equals(method))
        {
            isLoggedInBefore(method, jsonObject, callback);
        }
        else if (EMSDKMethod.getCurrentUser.equals(method))
        {
            getCurrentUser(method, jsonObject, callback);
        }
        else if (EMSDKMethod.loginWithAgoraToken.equals(method))
        {
            loginWithAgoraToken(method, jsonObject, callback);
        }
        else if (EMSDKMethod.getToken.equals(method))
        {
            getToken(method, jsonObject, callback);
        }
        else if (EMSDKMethod.isConnected.equals(method)) {
            isConnected(method, jsonObject, callback);
        }
        else if (EMSDKMethod.renewToken.equals(method)){
            renewToken(method, jsonObject, callback);
        } else if (EMSDKMethod.startCallback.equals(method)) {

        }
        else {
            super.onMethodCall(method, jsonObject, callback);
        }
        return str;
    }


    private void init(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        EMOptions options = EMOptionsHelper.fromJson(param, JavaWrapper.context);
        EMClient.getInstance().init(JavaWrapper.context, options);
        EMClient.getInstance().setDebugMode(param.getBoolean("debugModel"));
        bindingManagers();
        registerEaseListener();
        onSuccess(null, callback);
    }

    private void createAccount(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
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
    }

    private void login(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        boolean isPwd = param.getBoolean("isPassword");
        String username = param.getString("username");
        String pwdOrToken = param.getString("pwdOrToken");

        if (isPwd){
            EMClient.getInstance().login(username, pwdOrToken, new EMCommonCallback(callback));
        } else {
            EMClient.getInstance().loginWithToken(username, pwdOrToken, new EMCommonCallback(callback));
        }
    }


    private void logout(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        boolean unbindToken = param.getBoolean("unbindToken");
        EMClient.getInstance().logout(unbindToken, new EMCommonCallback(callback));
    }

    private void changeAppKey(String method, JSONObject param, EMWrapperCallback callback) throws JSONException{
        String appKey = param.getString("appKey");
        asyncRunnable(()-> {
            try {
                EMClient.getInstance().changeAppkey(appKey);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private String getCurrentUser(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        return EMClient.getInstance().getCurrentUser();
    }

    private void loginWithAgoraToken(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {

        String username = param.getString("username");
        String agoraToken = param.getString("agora_token");

        EMClient.getInstance().loginWithAgoraToken(username, agoraToken, new EMCommonCallback(callback));
    }
    private String getToken(String method, JSONObject param, EMWrapperCallback callback) throws JSONException
    {
        return EMClient.getInstance().getAccessToken();
    }

    private String isLoggedInBefore(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("value", EMClient.getInstance().isLoggedInBefore());
        return jsonObject.toString();
    }

    private String isConnected(String method, JSONObject param, EMWrapperCallback callback) throws JSONException{
        JSONObject jsonObject = new JSONObject();
        jsonObject.put("value", EMClient.getInstance().isConnected());
        return jsonObject.toString();
    }

    private void uploadLog(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().uploadLog(new EMCommonCallback(callback));
    }

    private void compressLogs(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(()->{
            try {
                String path = EMClient.getInstance().compressLogs();
                onSuccess(path, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }

    private void kickDevice(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {

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
    }

    private void kickAllDevices(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
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
    }

    private void renewToken(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        String agoraToken = param.getString("agora_token");
        asyncRunnable(()->{
            EMClient.getInstance().renewToken(agoraToken);
            onSuccess(true, callback);
        });
    }

    private void getLoggedInDevicesFromServer(String method, JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("username");
        String password = param.getString("password");
        asyncRunnable(()->{
            try {
                List<EMDeviceInfo> devices = EMClient.getInstance().getLoggedInDevicesFromServer(username, password);
                try {
                    JSONArray jsonList = new JSONArray();
                    for (EMDeviceInfo info: devices) {
                        jsonList.put(EMDeviceInfoHelper.toJson(info));
                    }
                    onSuccess(jsonList, callback);
                }catch (JSONException ignore) {}
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
    }


    private void registerEaseListener() {

        multiDeviceListener = new EMMultiDeviceListener() {
            @Override
            public void onContactEvent(int event, String target, String ext) {
                JSONObject data = new JSONObject();
                try {
                    data.put("event", Integer.valueOf(event));
                    data.put("target", target);
                    data.put("ext", ext);
                    post(()->JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onContactEvent", data.toString()));
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
                    post(()->JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onGroupEvent", data.toString()));
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
                    post(()->JavaWrapper.listener.onReceive("EMMultiDeviceListener", "onChatThreadEvent", data.toString()));
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        connectionListener = new EMConnectionListener() {
            @Override
            public void onConnected() {
                JSONObject data = new JSONObject();
                try {
                    data.put("connected", Boolean.TRUE);
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onContactEvent", data.toString()));
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
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidLoginFromOtherDevice", null));
                }else if (errorCode == 207) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidRemoveFromServer", null));
                }else if (errorCode == 305) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidForbidByServer", null));
                }else if (errorCode == 216) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidChangePassword", null));
                }else if (errorCode == 214) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserDidLoginTooManyDevice", null));
                } else if (errorCode == 217) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserKickedByOtherDevice", null));
                } else if (errorCode == 202) {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onUserAuthenticationFailed", null));
                } else {
                    post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onDisconnected", null));
                }
            }

            @Override
            public void onTokenExpired() {
                post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onTokenDidExpire", null));
            }

            @Override
            public void onTokenWillExpire() {
                post(()->JavaWrapper.listener.onReceive("EMConnectionListener", "onTokenWillExpire", null));
            }
        };

        EMClient.getInstance().addConnectionListener(connectionListener);
        EMClient.getInstance().addMultiDeviceListener(multiDeviceListener);

    }

    private void bindingManagers() {
        contactManagerWrapper = new EMContactManagerWrapper();
        roomManagerWrapper = new EMRoomManagerWrapper();
    }
}
