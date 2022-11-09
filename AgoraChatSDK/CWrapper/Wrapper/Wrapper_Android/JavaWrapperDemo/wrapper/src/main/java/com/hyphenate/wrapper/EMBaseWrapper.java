package com.hyphenate.wrapper;

import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.HyphenateExceptionHelper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMBaseWrapper {

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException{
        return null;
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
    public void asyncRunnable(Runnable runnable) {
        EMWrapperThreadUtil.asyncExecute(runnable);
    }

    public void onSuccess(Object object, EMWrapperCallback callback)  {
        post(()->{
            callback.onSuccess(object);
        });
    }

    public void onError(HyphenateException e, EMWrapperCallback callback)  {
        post(()->{
            callback.onError(HyphenateExceptionHelper.toJson(e));
        });
    }


    public void onErrorCode(int code, String desc, EMWrapperCallback callback) {
        HyphenateException e = new HyphenateException(code, desc);
        onError(e, callback);
    }
}
