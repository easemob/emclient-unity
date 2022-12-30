package com.hyphenate.wrapper.listeners;

import com.hyphenate.EMPresenceListener;
import com.hyphenate.chat.EMPresence;
import com.hyphenate.wrapper.EMWrapperHelper;
import com.hyphenate.wrapper.helper.EMPresenceHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.util.EMWrapperThreadUtil;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Map;

public class EMWrapperPresenceListener implements EMPresenceListener {
    @Override
    public void onPresenceUpdated(List<EMPresence> presences) {
        JSONArray jsonArray = new JSONArray();
        try{
            for (EMPresence presence: presences) {
                jsonArray.put(EMPresenceHelper.toJson(presence));
            }
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.presenceListener, EMSDKMethod.onPresenceUpdated, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void onTestPresenceUpdated() {
        JSONArray jsonArray = new JSONArray();
        try{
            JSONObject data = new JSONObject();
            data.put("publisher", "presence.getPublisher()");
            data.put("desc", "presence.getExt()");
            data.put("lastTime", Integer.valueOf("1000000"));
            data.put("expiryTime", Integer.valueOf("2000000"));
            JSONArray ja = new JSONArray();
            JSONObject jo = new JSONObject();
            jo.put("device", "device");
            jo.put("status", Integer.valueOf(0));
            ja.put(jo);
            data.put("detail", ja);

            jsonArray.put(data);
            post(() -> EMWrapperHelper.listener.onReceive(EMSDKMethod.presenceListener, EMSDKMethod.onPresenceUpdated, jsonArray.toString()));
        }catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public void post(Runnable runnable) {
        EMWrapperThreadUtil.mainThreadExecute(runnable);
    }
}
