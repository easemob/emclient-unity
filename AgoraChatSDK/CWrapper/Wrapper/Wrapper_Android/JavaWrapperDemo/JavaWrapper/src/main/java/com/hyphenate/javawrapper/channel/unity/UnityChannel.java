package com.hyphenate.javawrapper.channel.unity;

import com.hyphenate.javawrapper.channel.EMChannel;
import com.hyphenate.javawrapper.channel.EMChannelCallback;

public class UnityChannel extends EMChannel {

    public UnityChannel(long listener) {
        super(listener);
    }

    @Override
    public void nativeCall(String manager, String method, String jsonString, String cid) {
        this.callSDKApi(manager, method, jsonString, new EMChannelCallback() {
            @Override
            public void onSuccess(String jStr) {
                callListener("callback", cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                callListener("callback", cid, jStr);
            }
        });
    }

    @Override
    public String nativeGet(String manager, String method, String jsonString, String cid) {
        return this.callSDKApi(manager, method, jsonString, new EMChannelCallback() {
            @Override
            public void onSuccess(String jStr) {
                callListener("callback", cid, jStr);
            }

            @Override
            public void onError(String jStr) {
                callListener("callback", cid, jStr);
            }
        });
    }

    @Override
    public void callListener(String listener, String method, String jsonString) {
        super.callListener(listener, method, jsonString);
    }
}
