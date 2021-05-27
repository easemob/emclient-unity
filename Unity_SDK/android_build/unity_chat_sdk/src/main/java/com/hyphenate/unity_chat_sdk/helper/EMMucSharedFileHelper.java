package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMucSharedFile;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class EMMucSharedFileHelper {
    public static JSONObject toJson(EMMucSharedFile file) throws JSONException  {
        JSONObject data = new JSONObject();
        data.put("fileId", file.getFileId());
        data.put("name", file.getFileName());
        data.put("owner", file.getFileOwner());
        data.put("createTime", file.getFileUpdateTime());
        data.put("fileSize", file.getFileSize());

        return data;
    }
}
