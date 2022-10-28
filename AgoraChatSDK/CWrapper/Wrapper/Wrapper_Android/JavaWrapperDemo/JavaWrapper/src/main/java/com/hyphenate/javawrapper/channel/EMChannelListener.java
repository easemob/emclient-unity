package com.hyphenate.javawrapper.channel;

public interface EMChannelListener {
    void callListener(String listener, String method, String jsonString);
}
