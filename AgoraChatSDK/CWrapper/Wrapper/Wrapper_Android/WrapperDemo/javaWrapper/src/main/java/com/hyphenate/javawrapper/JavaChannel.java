package com.hyphenate.javawrapper;

public class JavaChannel extends EMChannel{
    @Override
    void callListener(String listener, String method, String jsonString) {

    }

    @Override
    void nativeCall(String manager, String method, String jsonString, String cid) {

    }

    @Override
    String nativeGet(String manager, String method, String jsonString, String cid) {
        return null;
    }
}
