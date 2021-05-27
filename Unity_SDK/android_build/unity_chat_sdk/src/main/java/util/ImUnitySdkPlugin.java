package util;

import android.os.Handler;
import android.os.Looper;

public class ImUnitySdkPlugin {
    public static final Handler handler = new Handler(Looper.getMainLooper());
    private ImUnitySdkPlugin() {
    }
}
