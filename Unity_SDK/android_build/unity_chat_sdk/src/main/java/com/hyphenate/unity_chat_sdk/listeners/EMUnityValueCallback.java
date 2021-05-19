package com.hyphenate.unity_chat_sdk.listeners;

import com.hyphenate.EMValueCallBack;

public class EMUnityValueCallback<T> implements EMValueCallBack<T> {

    private String callbackId;

    public EMUnityValueCallback(String callbackId) {
        this.callbackId = callbackId;
    }

    @Override
    public void onSuccess(T t) {

    }

    @Override
    public void onError(int i, String s) {

    }
}
