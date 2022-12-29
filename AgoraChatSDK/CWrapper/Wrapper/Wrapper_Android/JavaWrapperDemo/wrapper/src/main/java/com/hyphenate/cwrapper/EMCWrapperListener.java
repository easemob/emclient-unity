package com.hyphenate.cwrapper;

import com.hyphenate.wrapper.EMWrapperListener;

public class EMCWrapperListener implements EMWrapperListener {
    @Override
    public void onReceive(String listener, String method, String jsonString) {
        callListener(listener, method, jsonString);
    }

    public native void callListener(String listener, String method, String jsonString);
}
