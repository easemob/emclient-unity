package com.hyphenate.wrapper.util;

public class DelegateTester {
    static DelegateTester tester;
    public static DelegateTester shared() {
        if (tester == null){
            tester = new DelegateTester();
        }
        return tester;
    }
    public void startTest (){

    }
}
