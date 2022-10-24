package com.hyphenate.javawrapper;

public interface JavaWrapperInterface {
    public void callListener(String listener, String method, String jsonString);
    public String nativeGet(String manager, String method, String jsonString, String cid);
    public void nativeCall(String manager, String method, String jsonString, String cid);
}
