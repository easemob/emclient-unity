package com.hyphenate.cwrapper;

import com.hyphenate.wrapper.EMWrapper;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONException;

public class EMCWrapper {
    static EMCWrapper cWrapper;

    EMWrapper wrapper;
    public static EMCWrapper wrapper(int iType, long listener) {
        cWrapper = new EMCWrapper(iType, listener);
        return cWrapper;
    }

    long listener = 0;
    int iType = 0;

    public EMCWrapper(int iType, long listener){
        this.iType = iType;
        this.listener = listener;
        wrapper = new EMWrapper();
        EMWrapperHelper.listener = new EMCWrapperListener();
    }

    public void nativeCall(String manager, String method, String jsonString, String cid) throws JSONException{
        wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(String jStr) {
                EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jStr);
            }

            @Override
            public void onProgress(String jStr) {
                EMWrapperHelper.listener.onReceive(EMSDKMethod.callbackProgress, cid, jStr);
            }
        });
    }

    public String nativeGet(String manager, String method, String jsonString, String cid) {
        return wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(String jStr) {
                EMWrapperHelper.listener.onReceive("callback", cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                EMWrapperHelper.listener.onReceive("callback", cid, jStr);
            }

            @Override
            public void onProgress(String jStr) {
                EMWrapperHelper.listener.onReceive(EMSDKMethod.callbackProgress, cid, jStr);
            }
        });
    }
}
