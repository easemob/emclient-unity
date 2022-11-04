package com.hyphenate.wrapper;

public interface EMWrapperListener {
    void onReceive(String listener, String method, String jsonString);
}
