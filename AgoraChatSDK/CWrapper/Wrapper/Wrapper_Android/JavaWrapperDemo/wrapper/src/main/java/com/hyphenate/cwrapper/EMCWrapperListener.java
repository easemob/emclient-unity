package com.hyphenate.cwrapper;

import com.hyphenate.wrapper.EMWrapperListener;

public class EMCWrapperListener implements EMWrapperListener {
    @Override
    public void onReceive(String listener, String method, String jsonString) {
        System.out.println("java listener: " + listener + " method: " + method + " jStr: " + jsonString);
        callListener(listener, method, jsonString);
    }

    public native void callListener(String listener, String method, String jsonString);
}
