package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;

import org.json.JSONException;
import org.json.JSONObject;

public class EMWrapper {
    public EMClientWrapper clientWrapper;
    public EMWrapper() {
        clientWrapper = new EMClientWrapper();
    }

    public String callSDKApi(String manager, String method, String jsonString, EMWrapperCallback callback) {
        try {
            JSONObject jsonObject = new JSONObject(jsonString);
            if (manager.equals(EMSDKMethod.client)) {
                clientWrapper.onMethodCall(method, jsonObject, callback);
            }else if (manager.equals(EMSDKMethod.contactManager)) {
                clientWrapper.contactManagerWrapper.onMethodCall(method,jsonObject, callback);
            }else if (manager.equals(EMSDKMethod.roomManager)) {
                clientWrapper.contactManagerWrapper.onMethodCall(method,jsonObject, callback);
            }
        } catch (JSONException e) {
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("error", e.getLocalizedMessage());
                callback.onError(jsonObject.toString());
            }catch (JSONException ignore){}
        }
        return "";
    }
}
