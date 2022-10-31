package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.util.EMWrapperThreadUtil;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.HyphenateExceptionHelper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMBaseWrapper {

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException{
        return "";
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
    public void asyncRunnable(Runnable runnable) {
        EMWrapperThreadUtil.asyncExecute(runnable);
    }

    public void onSuccess(Object object, EMWrapperCallback callback)  {
        post(()->{
            JSONObject jsonObject = new JSONObject();
            try {
                jsonObject.put("value", object);
                callback.onSuccess(jsonObject.toString());
            }catch (JSONException ignore){}
        });
    }

    public void onError(HyphenateException e, EMWrapperCallback callback)  {
        post(()->{
            JSONObject jsonObject = new JSONObject();
            try {
                jsonObject.put("error", HyphenateExceptionHelper.toJson(e));
                callback.onError(jsonObject.toString());
            }catch (JSONException ignore) {}
        });
    }

    public void onErrorCode(int code, String desc, EMWrapperCallback callback) {
        HyphenateException e = new HyphenateException(code, desc);
        onError(e, callback);
    }
}
