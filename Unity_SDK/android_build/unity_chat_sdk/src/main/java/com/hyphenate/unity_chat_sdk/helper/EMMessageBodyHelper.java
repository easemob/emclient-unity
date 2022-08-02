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

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

public class EMMessageBodyHelper {
    public static EMTextMessageBody textBodyFromJson(JSONObject json) throws JSONException {
        String content = json.getString("content");
        EMTextMessageBody body = new EMTextMessageBody(content);
        if (json.has("targetLanguages")) {
            ArrayList<String> list = new ArrayList<>();
            JSONArray jsonArray = json.getJSONArray("targetLanguages");
            for (int i = 0; i < jsonArray.length(); i++) {
                String str = jsonArray.getString(i);
                list.add(str);
            }
            body.setTargetLanguages(list);
        }
        return body;
    }

    public static JSONObject textBodyToJson(EMTextMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("content", body.getMessage());
        if (body.getTargetLanguages() != null) {
            JSONArray jsonArray = new JSONArray();
            for (String item: body.getTargetLanguages()){
                jsonArray.put(item);
            }
            data.put("targetLanguages", jsonArray);
        }

        if (body.getTranslations() != null) {
            JSONObject jsonObject = new JSONObject();
            for (EMTextMessageBody.EMTranslationInfo info : body.getTranslations()) {
                jsonObject.put(info.languageCode, info.translationText);
            }
            data.put("translations", jsonObject);
        }

        return data;
    }

    public static EMLocationMessageBody localBodyFromJson(JSONObject json) throws JSONException {
        double latitude = json.getDouble("latitude");
        double longitude = json.getDouble("longitude");
        String address = "";
        if (json.has("address")) {
            address = json.getString("address");
        }

        String buildingName = null;
        if (json.has("buildingName")) {
            buildingName = json.getString("buildingName");
        }
        EMLocationMessageBody body = new EMLocationMessageBody(address, latitude, longitude, buildingName);
        return body;
    }

    public static JSONObject localBodyToJson(EMLocationMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("latitude", body.getLatitude());
        data.put("longitude", body.getLongitude());
        if (body.getAddress() != null) {
            data.put("address", body.getAddress());
        }
        if (body.getBuildingName() != null) {
            data.put("buildingName", body.getBuildingName());
        }

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
        if (body.action() != null) {
            data.put("action", body.action());
        }

        return data;
    }

    public static EMCustomMessageBody customBodyFromJson(JSONObject json) throws JSONException{
        String event = json.getString("event");
        EMCustomMessageBody body = new EMCustomMessageBody(event);
        if (json.has("params")) {
            String paramString = json.getString("params");
            JSONObject jsonObject = new JSONObject(paramString);
            Map<String, String> params = new HashMap<>();
            Iterator iterator = jsonObject.keys();
            while (iterator.hasNext()) {
                String key = iterator.next().toString();
                params.put(key, jsonObject.getString(key));
            }
            body.setParams(params);
        }
        return body;
    }

    public static JSONObject customBodyToJson(EMCustomMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        JSONObject params = new JSONObject();
        for (Map.Entry<String, String> m: body.getParams().entrySet()) {
            params.put(m.getKey(), m.getValue());
        }

        if (body.event() != null) {
            data.put("event", body.event());
        }
        if (params.length() > 0) {
            data.put("params", params);
        }

        return data;
    }

    public static EMFileMessageBody fileBodyFromJson(JSONObject json) throws  JSONException{
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMNormalFileMessageBody body = new EMNormalFileMessageBody(file);

        if(json.has("displayName")){
            body.setFileName(json.getString("displayName"));
        }

        if(json.has("remotePath")){
            body.setRemoteUrl(json.getString("remotePath"));
        }

        if(json.has("secret")){
            body.setSecret(json.getString("secret"));
        }

        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        body.setFileLength(json.getInt("fileSize"));
        return body;
    }

    public static JSONObject fileBodyToJson(EMNormalFileMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        if (body.getLocalUrl() != null) {
            data.put("localPath", body.getLocalUrl());
        }

        if (body.getFileName() != null) {
            data.put("displayName", body.getFileName());
        }

        if (body.getRemoteUrl() != null) {
            data.put("remotePath", body.getRemoteUrl());
        }

        if (body.getSecret() != null) {
            data.put("secret", body.getSecret());
        }

        data.put("fileSize", body.getFileSize());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        return data;
    }

    public static EMImageMessageBody imageBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMImageMessageBody body = new EMImageMessageBody(file);
        if (json.has("displayName")) {
            body.setFileName(json.getString("displayName"));
        }

        if (json.has("remotePath")) {
            body.setRemoteUrl(json.getString("remotePath"));
        }

        if (json.has("secret")) {
            body.setSecret(json.getString("secret"));
        }

        if (json.has("thumbnailLocalPath")) {
            body.setThumbnailLocalPath(json.getString("thumbnailLocalPath"));
        }

        if (json.has("thumbnailRemotePath")) {
            body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        }

        if (json.has("thumbnailSecret")) {
            body.setThumbnailSecret(json.getString("thumbnailSecret"));
        }

        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        body.setFileLength(json.getInt("fileSize"));
        int width = json.getInt("height");
        int height = json.getInt("width");
        body.setThumbnailSize(width, height);
        body.setSendOriginalImage(json.getBoolean("sendOriginalImage"));

        return body;
    }

    public static JSONObject imageBodyToJson(EMImageMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        if (body.getLocalUrl() != null) {
            data.put("localPath", body.getLocalUrl());
        }
        if (body.getFileName() != null) {
            data.put("displayName", body.getFileName());
        }
        if (body.getRemoteUrl() != null) {
            data.put("remotePath", body.getRemoteUrl());
        }
        if (body.getSecret() != null) {
            data.put("secret", body.getSecret());
        }
        if (body.thumbnailLocalPath() != null) {
            data.put("thumbnailLocalPath", body.thumbnailLocalPath());
        }
        if (body.getThumbnailUrl() != null) {
            data.put("thumbnailRemotePath", body.getThumbnailUrl());
        }
        if (body.getThumbnailSecret() != null) {
            data.put("thumbnailSecret", body.getThumbnailSecret());
        }
        data.put("thumbnailStatus", downloadStatusToInt(body.thumbnailDownloadStatus()));
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("height", body.getHeight());
        data.put("width", body.getWidth());
        data.put("sendOriginalImage", body.isSendOriginalImage());
        data.put("fileSize", body.getFileSize());

        return data;
    }

    public static EMVideoMessageBody videoBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        int duration = json.getInt("duration");
        int fileSize = json.getInt("fileSize");
        EMVideoMessageBody body = new EMVideoMessageBody(localPath, "", duration, fileSize);
        if (json.has("displayName")) {
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("thumbnailLocalPath")) {
            body.setLocalThumb(json.getString("thumbnailLocalPath"));
        }
        if (json.has("thumbnailRemotePath")){
            body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        }
        if (json.has("thumbnailSecret")) {
            body.setThumbnailSecret(json.getString("thumbnailSecret"));
        }
        if (json.has("thumbnailStatus")) {
            // android 目前没有暴露set方法。
        }

        if (json.has("remotePath")) {
            body.setRemoteUrl(json.getString("remotePath"));
        }
        if (json.has("secret")) {
            body.setSecret(json.getString("secret"));
        }

        if(json.has("fileSize")) {
            body.setFileLength(json.getInt("fileSize"));
        }

        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        int width = json.getInt("height");
        int height = json.getInt("width");
        body.setThumbnailSize(width, height);
        return body;
    }

    public static JSONObject videoBodyToJson(EMVideoMessageBody body) throws JSONException{
        JSONObject data = new JSONObject();
        if (body.getLocalUrl() != null) {
            data.put("localPath", body.getLocalUrl());
        }
        if (body.getFileName() != null) {
            data.put("displayName", body.getFileName());
        }
        data.put("duration", body.getDuration());
        data.put("fileSize", body.getVideoFileLength());
        if (body.getLocalThumbUri() != null) {
            data.put("thumbnailLocalPath", body.getLocalThumbUri());
        }
        if (body.getThumbnailUrl() != null) {
            data.put("thumbnailRemotePath", body.getThumbnailUrl());
        }
        if (body.getThumbnailSecret() != null) {
            data.put("thumbnailSecret", body.getThumbnailSecret());
        }
        data.put("thumbnailStatus", downloadStatusToInt(body.thumbnailDownloadStatus()));
        data.put("height", body.getThumbnailHeight());
        data.put("width", body.getThumbnailWidth());
        if (body.getRemoteUrl() !=  null) {
            data.put("remotePath", body.getRemoteUrl());
        }
        if (body.getSecret() != null) {
            data.put("secret", body.getSecret());
        }
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));

        return data;
    }

    public static EMVoiceMessageBody voiceBodyFromJson(JSONObject json) throws JSONException {
        int duration = json.getInt("duration");
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMVoiceMessageBody body = new EMVoiceMessageBody(file, duration);
        if (json.has("displayName")) {
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("secret")) {
            body.setSecret(json.getString("secret"));
        }

        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));

        body.setFileLength(json.getInt("fileSize"));
        return body;
    }

    public static JSONObject voiceBodyToJson(EMVoiceMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        if (body.getLocalUrl() != null) {
            data.put("localPath", body.getLocalUrl());
        }
        if (body.getFileName() != null) {
            data.put("displayName", body.getFileName());
        }
        if (body.getRemoteUrl() != null) {
            data.put("remotePath", body.getRemoteUrl());
        }
        if (body.getSecret() != null) {
            data.put("secret", body.getSecret());
        }

        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("fileSize", body.getFileSize());
        data.put("duration", body.getLength());
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
