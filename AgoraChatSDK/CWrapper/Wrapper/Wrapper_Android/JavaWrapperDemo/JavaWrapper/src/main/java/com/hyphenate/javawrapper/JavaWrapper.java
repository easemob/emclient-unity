package com.hyphenate.javawrapper;

import com.hyphenate.javawrapper.channel.EMChannel;
import com.hyphenate.javawrapper.channel.EMChannelType;
import com.hyphenate.javawrapper.channel.unity.UnityChannel;

public class JavaWrapper {
    public static EMChannel channel = null;
    public static EMChannel wrapper(int iType, long listener) {
        EMChannelType type = EMChannelType.fromInt(iType);
        if (type == EMChannelType.Unity) {
            channel = new UnityChannel(listener);
        }else if (type == EMChannelType.Flutter) {

        }else if (type == EMChannelType.RN) {

        }
        return channel;
    }
}
