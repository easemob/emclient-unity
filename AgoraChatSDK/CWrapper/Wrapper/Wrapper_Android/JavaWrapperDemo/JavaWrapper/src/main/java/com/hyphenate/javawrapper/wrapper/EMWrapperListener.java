package com.hyphenate.javawrapper.wrapper;

public interface EMWrapperListener {
    void onReceive(String listener, String method, String jsonString);
}
