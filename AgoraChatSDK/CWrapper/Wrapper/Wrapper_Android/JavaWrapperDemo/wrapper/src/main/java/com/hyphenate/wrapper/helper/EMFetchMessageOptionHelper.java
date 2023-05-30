package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMFetchMessageOption;
import com.hyphenate.chat.EMMessage;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class EMFetchMessageOptionHelper {
    public static EMFetchMessageOption fromJson(JSONObject json) throws JSONException {
        EMFetchMessageOption options = new EMFetchMessageOption();
        if (json.optInt("direction") == 0) {
            options.setDirection(EMConversation.EMSearchDirection.UP);
        }else {
            options.setDirection(EMConversation.EMSearchDirection.DOWN);
        }
        options.setIsSave(json.getBoolean("isSave"));
        options.setStartTime(json.getLong("startTime"));
        options.setEndTime(json.getLong("endTime"));
        if (json.has("from")){
            options.setFrom(json.getString("from"));
        }
        if (json.has("types")){
            List<EMMessage.Type> list = new ArrayList<>();
            JSONArray array = json.getJSONArray("types");
            for (int i = 0; i < array.length(); i++) {
                int type = array.optInt(i);
                switch (type) {
                    case 0: {
                        list.add(EMMessage.Type.TXT);
                    }
                    break;
                    case 1: {
                        list.add(EMMessage.Type.IMAGE);
                    }
                    break;
                    case 3: {
                        list.add(EMMessage.Type.LOCATION);
                    }
                    break;
                    case 2: {
                        list.add(EMMessage.Type.VIDEO);
                    }
                    break;
                    case 4: {
                        list.add(EMMessage.Type.VOICE);
                    }
                    break;
                    case 5: {
                        list.add(EMMessage.Type.FILE);
                    }
                    break;
                    case 6: {
                        list.add(EMMessage.Type.CMD);
                    }
                    break;
                    case 7: {
                        list.add(EMMessage.Type.CUSTOM);
                    }
                    break;
                }
            }
            if (list.size() > 0) {
                options.setMsgTypes(list);
            }
        }

        return options;
    }
}
