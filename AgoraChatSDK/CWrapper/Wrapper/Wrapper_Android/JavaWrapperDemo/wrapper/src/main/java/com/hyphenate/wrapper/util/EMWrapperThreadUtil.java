package com.hyphenate.wrapper.util;

import android.os.Handler;
import android.os.Looper;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class EMWrapperThreadUtil {

    public static void asyncExecute(Runnable runnable) {
        asyncThreadPool.execute(runnable);
    }

    public static void mainThreadExecute(Runnable runnable) {
        mainThreadHandler.post(runnable);
    }

    private static ExecutorService asyncThreadPool = Executors.newCachedThreadPool();
    private static Handler mainThreadHandler = new Handler(Looper.getMainLooper());
}
