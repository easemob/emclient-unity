package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMCmdMessageBody;
import com.hyphenate.chat.EMCustomMessageBody;
import com.hyphenate.chat.EMFileMessageBody;
import com.hyphenate.chat.EMImageMessageBody;
import com.hyphenate.chat.EMLocationMessageBody;
import com.hyphenate.chat.EMNormalFileMessageBody;
import com.hyphenate.chat.EMTextMessageBody;
import com.hyphenate.chat.EMVideoMessageBody;
import com.hyphenate.chat.EMVoiceMessageBody;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

public class EMMessageBodyHelper {
    public static EMTextMessageBody textBodyFromJson(JSONObject json) throws JSONException {
        String content = json.getString("content");
        EMTextMessageBody body = new EMTextMessageBody(content);
        return body;
    }

    public static JSONObject textBodyToJson(EMTextMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("content", body.getMessage());
        data.put("type", "txt");
        return data;
    }

    public static EMLocationMessageBody localBodyFromJson(JSONObject json) throws JSONException {
        double latitude = json.getDouble("latitude");
        double longitude = json.getDouble("longitude");
        String address = json.getString("address");

        EMLocationMessageBody body = new EMLocationMessageBody(address, latitude, longitude);
        return body;
    }

    public static JSONObject localBodyToJson(EMLocationMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("latitude", body.getLatitude());
        data.put("longitude", body.getLongitude());
        data.put("address", body.getAddress());
        data.put("type", "loc");
        return data;
    }

    public static EMCmdMessageBody cmdBodyFromJson(JSONObject json) throws JSONException {
        String action = json.getString("action");
        boolean deliverOnlineOnly = json.getBoolean("deliverOnlineOnly");

        EMCmdMessageBody body = new EMCmdMessageBody(action);
        body.deliverOnlineOnly(deliverOnlineOnly);

        return body;
    }

    public static JSONObject cmdBodyToJson(EMCmdMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("deliverOnlineOnly", body.isDeliverOnlineOnly());
        data.put("action", body.action());
        data.put("type", "cmd");
        return data;
    }

    public static EMCustomMessageBody customBodyFromJson(JSONObject json) throws JSONException{
        String event = json.getString("event");
        JSONObject jsonObject = json.getJSONObject("params");
        Map<String, String> params = new HashMap<>();
        Iterator iterator = jsonObject.keys();
        while (iterator.hasNext()) {
            String key = iterator.next().toString();
            params.put(key, jsonObject.getString(key));
        }

        EMCustomMessageBody body = new EMCustomMessageBody(event);
        body.setParams(params);

        return body;
    }

    public static JSONObject customBodyToJson(EMCustomMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("event", body.event());
        data.put("params", body.getParams());
        data.put("type", "custom");
        return data;
    }

    public static EMFileMessageBody fileBodyFromJson(JSONObject json) throws  JSONException{
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMNormalFileMessageBody body = new EMNormalFileMessageBody(file);
        body.setFileName(json.getString("displayName"));
        body.setRemoteUrl(json.getString("remotePath"));
        body.setSecret(json.getString("secret"));
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        body.setFileLength(json.getInt("fileSize"));
        return body;
    }

    public static JSONObject fileBodyToJson(EMNormalFileMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("localPath", body.getLocalUrl());
        data.put("fileSize", body.getFileSize());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("secret", body.getSecret());
        data.put("fileSize", body.getFileSize());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("type", "file");
        return data;
    }

    public static EMImageMessageBody imageBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMImageMessageBody body = new EMImageMessageBody(file);
        body.setFileName(json.getString("displayName"));
        body.setRemoteUrl(json.getString("remotePath"));
        body.setSecret(json.getString("secret"));
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        if (json.getString("thumbnailLocalPath") != null) {
            body.setThumbnailLocalPath(json.getString("thumbnailLocalPath"));
        }
        body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        body.setThumbnailSecret(json.getString("thumbnailSecret"));
        body.setFileLength(json.getInt("fileSize"));
        int width = json.getInt("height");
        int height = json.getInt("width");
        body.setThumbnailSize(width, height);
        body.setSendOriginalImage(json.getBoolean("sendOriginalImage"));

        return body;
    }

    public static JSONObject imageBodyToJson(EMImageMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("localPath", body.getLocalUrl());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("secret", body.getSecret());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("thumbnailLocalPath", body.thumbnailLocalPath());
        data.put("thumbnailRemotePath", body.getThumbnailUrl());
        data.put("thumbnailSecret", body.getThumbnailSecret());
        data.put("height", body.getHeight());
        data.put("width", body.getWidth());
        data.put("sendOriginalImage", body.isSendOriginalImage());
        data.put("fileSize", body.getFileSize());
        data.put("type", "img");
        return data;
    }

    public static EMVideoMessageBody videoBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        String thumbnailLocalPath = json.getString("thumbnailLocalPath");
        int duration = json.getInt("duration");
        int fileSize = json.getInt("fileSize");
        EMVideoMessageBody body = new EMVideoMessageBody(localPath, thumbnailLocalPath, duration, fileSize);
        body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        if (json.getString("thumbnailLocalPath") != null) {
            body.setLocalThumb(json.getString("thumbnailLocalPath"));
        }
        body.setThumbnailSecret(json.getString("thumbnailSecret"));
        body.setFileName(json.getString("displayName"));
        int width = json.getInt("height");
        int height = json.getInt("width");
        body.setThumbnailSize(width, height);
        body.setRemoteUrl(json.getString("remotePath"));
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        body.setSecret(json.getString("secret"));
        body.setFileLength(json.getInt("fileSize"));
        return body;
    }

    public static JSONObject videoBodyToJson(EMVideoMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        data.put("localPath", body.getLocalUrl());
        data.put("thumbnailLocalPath", body.getLocalThumbUri());
        data.put("duration", body.getDuration());
        data.put("fileSize", body.getVideoFileLength());
        data.put("thumbnailRemotePath", body.getThumbnailUrl());
        data.put("thumbnailSecret", body.getThumbnailSecret());
        data.put("displayName", body.getFileName());
        data.put("height", body.getThumbnailHeight());
        data.put("width", body.getThumbnailWidth());
        data.put("remotePath", body.getRemoteUrl());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("secret", body.getSecret());
        data.put("fileSize", body.getVideoFileLength());
        data.put("type", "video");

        return data;
    }

    public static EMVoiceMessageBody voiceBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);
        int duration = json.getInt("duration");
        EMVoiceMessageBody body = new EMVoiceMessageBody(file, duration);
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        body.setFileName(json.getString("displayName"));
        body.setFileLength(json.getLong("fileSize"));
        body.setSecret(json.getString("secret"));
        body.setFileLength(json.getInt("fileSize"));
        return body;
    }

    public static JSONObject voiceBodyToJson(EMVoiceMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("localPath", body.getLocalUrl());
        data.put("duration", body.getLength());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("secret", body.getSecret());
        data.put("type", "voice");
        data.put("fileSize", body.getFileSize());
        return data;
    }

    private static EMFileMessageBody.EMDownloadStatus downloadStatusFromInt(int downloadStatus) {
        switch (downloadStatus) {
            case 0:
                return EMFileMessageBody.EMDownloadStatus.DOWNLOADING;
            case 1:
                return EMFileMessageBody.EMDownloadStatus.SUCCESSED;
            case 2:
                return EMFileMessageBody.EMDownloadStatus.FAILED;
            case 3:
                return EMFileMessageBody.EMDownloadStatus.PENDING;
        }
        return EMFileMessageBody.EMDownloadStatus.DOWNLOADING;
    }

    private static int downloadStatusToInt(EMFileMessageBody.EMDownloadStatus downloadStatus) {
        switch (downloadStatus) {
            case DOWNLOADING:
                return 0;
            case SUCCESSED:
                return 1;
            case FAILED:
                return 2;
            case PENDING:
                return 3;
        }
        return 0;
    }
}
