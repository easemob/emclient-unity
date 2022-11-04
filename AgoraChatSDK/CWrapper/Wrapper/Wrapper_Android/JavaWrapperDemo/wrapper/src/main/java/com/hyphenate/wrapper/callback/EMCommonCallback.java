package com.hyphenate.wrapper.callback;

import com.hyphenate.EMCallBack;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;
import com.hyphenate.wrapper.helper.HyphenateExceptionHelper;

import org.json.JSONException;

public class EMCommonCallback implements EMCallBack {

    EMWrapperCallback callback;

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }

    public EMCommonCallback(EMWrapperCallback callback) {
        this.callback = callback;
    }

    @Override
    public void onSuccess()
    {
        post(()-> callback.onSuccess(null));
    }

    @Override
    public void onError(int code, String error) {
        HyphenateException e = new HyphenateException(code, error);
        post(()->{
            try {
                callback.onError(HyphenateExceptionHelper.toJson(e).toString());
            } catch (JSONException jsonException) {
                jsonException.printStackTrace();
            }
        });
    }
}
