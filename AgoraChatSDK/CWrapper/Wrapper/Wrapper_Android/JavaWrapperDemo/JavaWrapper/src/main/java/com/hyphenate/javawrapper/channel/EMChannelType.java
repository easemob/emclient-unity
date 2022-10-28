package com.hyphenate.javawrapper.channel;

public enum EMChannelType {
    Unity(0),
    Flutter(1),
    RN(2);

    private int channelType;

    public static com.hyphenate.javawrapper.channel.EMChannelType fromInt(int iType) {
        switch (iType) {
            case 0: return Unity;
            case 1: return Flutter;
            case 2: return RN;
            default: return null;
        }
    }
    EMChannelType(int i) {
        this.channelType = i;
    }
}
