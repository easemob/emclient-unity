package com.hyphenate.javawrapper.wrapper;

import android.content.Context;

import com.hyphenate.javawrapper.channel.EMChannelCallback;

import org.json.JSONException;
import org.json.JSONObject;

import java.sql.Wrapper;

public class EMWrapper {
    public EMClientWrapper clientWrapper;
    public EMWrapper() {
        clientWrapper = new EMClientWrapper();
    }
}
