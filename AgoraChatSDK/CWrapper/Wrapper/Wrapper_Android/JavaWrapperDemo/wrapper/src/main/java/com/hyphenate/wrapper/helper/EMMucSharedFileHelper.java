package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMMucSharedFile;

import org.json.JSONException;
import org.json.JSONObject;

public class EMMucSharedFileHelper {
    public static JSONObject toJson(EMMucSharedFile file) throws JSONException {
        if (file == null) return null;
        JSONObject data = new JSONObject();
        data.put("fileId", file.getFileId());
        data.put("name", file.getFileName());
        data.put("owner", file.getFileOwner());
        data.put("createTime", file.getFileUpdateTime());
        data.put("fileSize", file.getFileSize());

        return data;
    }
}

