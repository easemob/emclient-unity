package com.hyphenate.javawrapper.wrapper;

import android.os.Handler;
import android.os.Looper;

import com.hyphenate.javawrapper.channel.EMChannelCallback;

import org.json.JSONException;
import org.json.JSONObject;

public class EMBaseWrapper {

    static final Handler handler = new Handler(Looper.getMainLooper());

    public String onMethodCall(String method, JSONObject jsonObject, EMChannelCallback callback) throws JSONException{
        return "";
    }

    public void post(Runnable runnable) {
        handler.post(runnable);
    }
}
