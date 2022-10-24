package com.hyphenate.javawrapper;

public  class JavaWrapper {

    // 用于存储nativeListener 的指针,并不在java层调用，由cWrapper进行管理，当调用callNativeListener时，去cWrapper里通过JavaWrapper获取，并转换。
    long nativeListener = 0;

    static {
        System.loadLibrary("CWrapper");
    }

    // 调用c++ listener
    public native void callNativeListener(String listener, String method, String jsonString);


    public void nativeCall(String manager, String method, String jsonString, String cid) {
        System.out.println("manager: " + manager + " method: " + method + "js: " + jsonString + "cid: " + cid);
        callNativeListener("test", "method","js");
    }

    public String nativeGet(String manager, String method, String jsonString, String cid) {
        System.out.println("manager: " + manager + " method: " + method + "js: " + jsonString + "cid: " + cid);
        return "100";
    }

    static public JavaWrapper share() {
        return new JavaWrapper();
    }
}