package com.hyphenate.wrapper;

import com.hyphenate.helper.EMUnityHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.callback.EMWrapperCallback;

import org.json.JSONException;
import org.json.JSONObject;

public class EMWrapper {
    public EMClientWrapper clientWrapper;
    public EMWrapper() {
        clientWrapper = new EMClientWrapper();
        new EMUnityHelper();
    }

    public String callSDKApi(String manager, String method, String jsonString, EMWrapperCallback callback) {
        String str = null;
        try {
            JSONObject jsonObject = null;
            if (jsonString.length() > 0) {
                jsonObject = new JSONObject(jsonString);
            }
            switch (manager) {
                case EMSDKMethod.client:
                    str = clientWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.chatManager:
                    str = clientWrapper.chatManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.contactManager:
                    str = clientWrapper.contactManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.roomManager:
                    str = clientWrapper.roomManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.groupManager:
                    str = clientWrapper.groupManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.userInfoManager:
                    str = clientWrapper.userInfoManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.presenceManager:
                    str = clientWrapper.presenceManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.threadManager:
                    str = clientWrapper.chatThreadManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
                case EMSDKMethod.pushManager:
                    str = clientWrapper.pushManagerWrapper.onMethodCall(method, jsonObject, callback);
                    break;
            }
        } catch (JSONException e) {
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("error", e.getLocalizedMessage());
                callback.onError(jsonObject);
            }catch (JSONException ignore){}
        }
        return str;
    }
}
