package com.hyphenate.unity;
import com.hyphenate.cwrapper.EMCWrapperListener;
import com.hyphenate.javawrapper.JavaWrapper;
import com.unity3d.player.UnityPlayer;

public class EMUnityHelper {
    static  {
        JavaWrapper.context = UnityPlayer.currentActivity.getApplicationContext();
        JavaWrapper.listener = new EMCWrapperListener();
    }
}
