package com.hyphenate.wrapper;

import com.hyphenate.EMConnectionListener;
import com.hyphenate.EMMultiDeviceListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMDeviceInfo;
import com.hyphenate.chat.EMOptions;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.listeners.EMWrapperConnectionListener;
import com.hyphenate.wrapper.listeners.EMWrapperMultiDeviceListener;
import com.hyphenate.wrapper.util.DelegateTester;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.callback.EMCommonCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMDeviceInfoHelper;
import com.hyphenate.wrapper.helper.EMOptionsHelper;

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
    public EMMessageManager messageManager;
    public EMConversationWrapper conversationWrapper;

    static EMClientWrapper clientWrapper;

    public EMWrapperConnectionListener wrapperConnectionListener;
    public EMWrapperMultiDeviceListener wrapperMultiDeviceListener;

    public static EMClientWrapper shared() {
        if (clientWrapper == null) {
            clientWrapper = new EMClientWrapper();
        }
        return clientWrapper;
    }


    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String str = null;
        switch (method) {
            case EMSDKMethod.runDelegateTester:
                str = runDelegateTester();
                break;
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

    private String runDelegateTester() {
        DelegateTester.shared().startTest();
        return null;
    }

    private String init(JSONObject param, EMWrapperCallback callback) throws JSONException {
        JSONObject jo = param.getJSONObject("options");
        EMOptions options = EMOptionsHelper.fromJson(jo, EMWrapperHelper.context);
        EMClient.getInstance().init(EMWrapperHelper.context, options);
        EMClient.getInstance().setDebugMode(jo.getBoolean("debugMode"));
        bindingManagers();
        registerEaseListener();
        onSuccess(null, callback);
        return null;
    }

    private String createAccount(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("userId");
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
        boolean isToken = param.getBoolean("isToken");
        String username = param.getString("userId");
        String pwdOrToken = param.getString("pwdOrToken");

        if (isToken){
            EMClient.getInstance().loginWithToken(username, pwdOrToken, new EMCommonCallback(callback));
        } else {
            EMClient.getInstance().login(username, pwdOrToken, new EMCommonCallback(callback));
        }
        return null;
    }


    private String logout(JSONObject param, EMWrapperCallback callback) throws JSONException {
        boolean unbindToken = param.getBoolean("unbindDeviceToken");
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
        String username = param.getString("userId");
        String agoraToken = param.getString("token");
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

        String username = param.getString("userId");
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
        String username = param.getString("userId");
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
        String agoraToken = param.getString("token");
        asyncRunnable(()->{
            EMClient.getInstance().renewToken(agoraToken);
            onSuccess(null, callback);
        });
        return null;
    }

    private String getLoggedInDevicesFromServer(JSONObject param, EMWrapperCallback callback) throws JSONException {
        String username = param.getString("userId");
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

        wrapperConnectionListener = new EMWrapperConnectionListener();
        wrapperMultiDeviceListener = new EMWrapperMultiDeviceListener();
        EMClient.getInstance().addConnectionListener(wrapperConnectionListener);
        EMClient.getInstance().addMultiDeviceListener(wrapperMultiDeviceListener);
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
        messageManager = new EMMessageManager();
        conversationWrapper = new EMConversationWrapper();
    }

}
