package com.hyphenate.cwrapper;

import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.wrapper.EMWrapper;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;

import org.json.JSONException;

public class EMCWrapper {
    EMWrapper wrapper;
    public EMCWrapper(int iType, long listener) {
        wrapper = JavaWrapper.wrapper(iType, listener);
        JavaWrapper.listener = new EMCWrapperListener();
    }

    public void nativeCall(String manager, String method, String jsonString, String cid) throws JSONException{
        wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(String jStr) {
                JavaWrapper.listener.onReceive("callback", cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                JavaWrapper.listener.onReceive("callback", cid, jStr);
            }
        });
    }

    public String nativeGet(String manager, String method, String jsonString, String cid) {
        return wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(String jStr) {
                JavaWrapper.listener.onReceive("callback", cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                JavaWrapper.listener.onReceive("callback", cid, jStr);
            }
        });
    }
}
