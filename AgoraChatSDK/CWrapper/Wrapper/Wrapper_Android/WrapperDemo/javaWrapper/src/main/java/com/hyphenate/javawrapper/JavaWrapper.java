package com.hyphenate.javawrapper;

public class JavaWrapper {
    static EMChannel channel = null;
    public static EMChannel wrapper(int iType) {
        ChannelType type = ChannelType.fromInt(iType);
        if (type == ChannelType.C) {
            channel = new CPPChannel();
            return channel;
        }else if (type == ChannelType.Java) {
            channel = new JavaChannel();
            return channel;
        }else {
            return null;
        }
    }
    public static Object test(int i){
        return new Object();
    }
}

enum ChannelType {
    C(0),
    Java(1);

    private int channelType;

    static ChannelType fromInt(int iType) {
        switch (iType) {
            case 0: return C;
            case 1: return Java;
            default: return null;
        }
    }
     ChannelType(int i) {
        this.channelType = i;
    }
 }
