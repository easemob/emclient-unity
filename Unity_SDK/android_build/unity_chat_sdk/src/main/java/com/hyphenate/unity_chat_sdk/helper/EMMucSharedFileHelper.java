package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMMucSharedFile;

import java.util.HashMap;
import java.util.Map;

public class EMMucSharedFileHelper {
    public static Map<String, Object> toJson(EMMucSharedFile file) {
        Map<String, Object> data = new HashMap<>();
        data.put("fileId", file.getFileId());
        data.put("name", file.getFileName());
        data.put("owner", file.getFileOwner());
        data.put("createTime", file.getFileUpdateTime());
        data.put("fileSize", file.getFileSize());

        return data;
    }
}
