package com.hyphenate.cwrapper;

import android.content.Context;

import com.hyphenate.wrapper.EMWrapper;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONException;
import org.json.JSONObject;

public class EMCWrapper {

    long nativeListener = 0;
    static EMCWrapper cWrapper;

    EMWrapper wrapper;
    public static EMCWrapper wrapper(int iType, long listener) {
        cWrapper = new EMCWrapper(iType, listener);
        return cWrapper;
    }

    int iType = 0;

    public EMCWrapper(int iType, long listener){
        this.iType = iType;
        this.nativeListener = listener;
        wrapper = new EMWrapper();
        EMWrapperHelper.listener = new EMCWrapperListener();
    }

    public void nativeCall(String manager, String method, String jsonString, String cid) throws JSONException{
        System.out.println("java: nativeCall: manager:" + manager + " method:" + method + " jsonString:" + jsonString + " cid:" + cid);
        wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(Object obj){
                try {
                    JSONObject jsonObject = new JSONObject();
                    if (obj != null) {
                        jsonObject.put("value", obj);
                    }
                    jsonObject.put("callbackId", cid);
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(Object obj){
                try {
                    JSONObject jsonObject = new JSONObject();
                    jsonObject.put("callbackId", cid);
                    if (obj != null) {
                        jsonObject.put("error", obj);
                    }
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onProgress(int progress) {
                try {
                    JSONObject jsonObject = new JSONObject();
                    jsonObject.put("callbackId", cid);
                    jsonObject.put("progress", progress);
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callbackProgress, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }

    public String nativeGet(String manager, String method, String jsonString, String cid) {
        System.out.println("java: nativeGet: manager:" + manager + " method:" + method + " jsonString:" + jsonString + " cid:" + cid);
        String str = wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(Object obj){
                try {
                    JSONObject jsonObject = new JSONObject();
                    if (obj != null) {
                        jsonObject.put("value", obj);
                    }
                    jsonObject.put("callbackId", cid);
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(Object obj){
                try {
                    JSONObject jsonObject = new JSONObject();
                    jsonObject.put("callbackId", cid);
                    if (obj != null) {
                        jsonObject.put("error", obj);
                    }
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callback, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onProgress(int progress) {
                try {
                    JSONObject jsonObject = new JSONObject();
                    jsonObject.put("callbackId", cid);
                    jsonObject.put("progress", progress);
                    EMWrapperHelper.listener.onReceive(EMSDKMethod.callbackProgress, cid, jsonObject.toString());
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });

        System.out.println("java: nativeGetReturn" + str + " cid:" + cid);

        return str;
    }
}
