package com.hyphenate.cwrapper;

import com.hyphenate.wrapper.EMWrapper;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class EMCWrapper {
    int iType = 0;
    // 用于保持callback指针，初始化时c++将自己listener的指针传过来放到java层保存，当需要是从java层取。
    long nativeListener = 0;
    static EMCWrapper cWrapper;

    EMWrapper wrapper;
    public static EMCWrapper cWrapper(int iType, long listener) {
        cWrapper = new EMCWrapper(iType, listener);
        return cWrapper;
    }

    public EMCWrapper(int iType, long listener){
        this.iType = iType;
        this.nativeListener = listener;
        wrapper = new EMWrapper();
        EMWrapperHelper.listener = new EMCWrapperListener();
    }

    public void nativeCall(String manager, String method, String jsonString, String cid) throws JSONException{
        wrapper.callSDKApi(manager, method, jsonString, new EMWrapperCallback(){
            @Override
            public void onSuccess(Object obj){
                try {
                    JSONObject jsonObject = new JSONObject();
                    if (obj != null) {
                        // value 里是 JSONObject to String
                        if (obj instanceof JSONObject || obj instanceof JSONArray) {
                            jsonObject.put("value", obj.toString());
                        }else if (obj instanceof String) {
                            JSONObject tmpJsonObject = new JSONObject();
                            tmpJsonObject.put("ret", obj);
                            jsonObject.put("value", tmpJsonObject.toString());
                        }
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
                        // error 里是 HyphenateException to JSONObject
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
                    // progress 里是 int progress
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

        return str;
    }
}
