package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.javawrapper.util.EMWrapperThreadUtil;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.HyphenateExceptionHelper;

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
                JSONObject jo = null;
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("value", object);
                jo = jsonObject;
            }catch (JSONException ignore){
                ignore.printStackTrace();
            }finally {
                callback.onSuccess(jo.toString());
            }
        });
    }

    public void onError(HyphenateException e, EMWrapperCallback callback)  {
        post(()->{
            JSONObject jo = null;
            try {
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("error", HyphenateExceptionHelper.toJson(e));
            }catch (JSONException ignore) {
                e.printStackTrace();
            }finally {
                callback.onError(jo.toString());
            }
        });
    }

    public void onErrorCode(int code, String desc, EMWrapperCallback callback) {
        HyphenateException e = new HyphenateException(code, desc);
        onError(e, callback);
    }
}
