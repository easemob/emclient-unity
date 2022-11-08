package com.hyphenate.helper;

import com.hyphenate.wrapper.EMWrapperHelper;
import com.unity3d.player.UnityPlayer;

public class EMUnityHelper {
    public EMUnityHelper() {
        EMWrapperHelper.context = UnityPlayer.currentActivity.getApplicationContext();
    }
}