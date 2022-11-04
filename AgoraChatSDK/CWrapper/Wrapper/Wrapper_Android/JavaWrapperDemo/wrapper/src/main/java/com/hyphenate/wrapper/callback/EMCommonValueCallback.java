package com.hyphenate.wrapper.callback;

import com.hyphenate.EMValueCallBack;

import com.hyphenate.wrapper.util.EMWrapperThreadUtil;
import com.hyphenate.wrapper.helper.EMErrorHelper;

import org.json.JSONException;
import org.json.JSONObject;

public class EMCommonValueCallback<T> implements EMValueCallBack<T> {

    EMWrapperCallback callback;

    public EMCommonValueCallback(EMWrapperCallback callback) {
        this.callback = callback;
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }

    @Override
    public void onSuccess(T object) {
        updateObject(object);
    }

    @Override
    public void onError(int error, String errorMsg) {
        post(() -> {
            JSONObject data = new JSONObject();
            try {
                data.put("error", EMErrorHelper.toJson(error, errorMsg));
            } catch (JSONException e) {
                e.printStackTrace();
            }
            callback.onError(data.toString());
        });
    }

    public void updateObject(Object object) {
        post(()-> {
            JSONObject data = new JSONObject();
            if (object != null) {
                try {
                    data.put("value", object);
                }catch (JSONException e) {}
            }
            callback.onSuccess(data.toString());
        });
    }
}
