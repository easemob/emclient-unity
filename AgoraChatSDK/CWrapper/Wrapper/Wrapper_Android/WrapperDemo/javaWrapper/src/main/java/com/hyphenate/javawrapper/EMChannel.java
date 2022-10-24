package com.hyphenate.javawrapper;

public abstract class EMChannel {
    long nativeListener = 0;
    abstract void callListener(String listener, String method, String jsonString);
    abstract void nativeCall(String manager, String method, String jsonString, String cid);
    abstract String nativeGet(String manager, String method, String jsonString, String cid);
}
