package com.hyphenate.javawrapper;

public class CPPChannel extends EMChannel{
    // 用于存储nativeListener 的指针,并不在java层调用，由cWrapper进行管理，当调用callNativeListener时，去cWrapper里通过JavaWrapper获取，并转换。
//    long nativeListener = 0;
    Integer value = 100;

    static {
        System.loadLibrary("ChatCWrapper");
    }

    // 调用c++ listener
    public native void callListener(String listener, String method, String jsonString);


    public void nativeCall(String manager, String method, String jsonString, String cid) {

    }

    public String nativeGet(String manager, String method, String jsonString, String cid) {
        callListener("111","2222","3333");
        String str = "你好" + (value++).toString();
        System.out.println("manager: " + manager + " method: " + method + "js: " + jsonString + "cid: " + cid + "  " + str);
        return str;
    }
}
