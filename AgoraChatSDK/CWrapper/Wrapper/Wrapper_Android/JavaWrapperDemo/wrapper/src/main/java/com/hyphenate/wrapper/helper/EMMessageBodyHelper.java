package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMCmdMessageBody;
import com.hyphenate.chat.EMCombineMessageBody;
import com.hyphenate.chat.EMCustomMessageBody;
import com.hyphenate.chat.EMFileMessageBody;
import com.hyphenate.chat.EMImageMessageBody;
import com.hyphenate.chat.EMLocationMessageBody;
import com.hyphenate.chat.EMMessageBody;
import com.hyphenate.chat.EMNormalFileMessageBody;
import com.hyphenate.chat.EMTextMessageBody;
import com.hyphenate.chat.EMVideoMessageBody;
import com.hyphenate.chat.EMVoiceMessageBody;
import com.hyphenate.wrapper.util.EMHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMMessageBodyHelper {


    public static JSONObject bodyToJson(EMMessageBody body) throws JSONException {
        JSONObject data = new JSONObject();
        data.put("operationCount", body.operationCount());
        data.put("operationTime", body.operationTime());
        data.put("operatorId", body.operatorId());
        return data;
    }

    public static EMTextMessageBody textBodyFromJson(JSONObject json) throws JSONException {
        String content = json.getString("content");
        List<String> list = new ArrayList<>();
        if (json.has("targetLanguages")) {
            JSONArray ja = json.getJSONArray("targetLanguages");
            for (int i = 0; i < ja.length(); i++) {
                list.add(ja.getString(i));
            }
        }
        EMTextMessageBody body = new EMTextMessageBody(content);
        body.setTargetLanguages(list);
        return body;
    }

    public static JSONObject textBodyToJson(EMTextMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("content", body.getMessage());
        if (body.getTargetLanguages() != null) {
            data.put("targetLanguages", body.getTargetLanguages());
        }
        if (body.getTranslations() != null) {
            JSONObject map = new JSONObject();
            List<EMTextMessageBody.EMTranslationInfo> list = body.getTranslations();
            for (int i = 0; i < list.size(); ++i) {
                String key = list.get(i).languageCode;
                String value = list.get(i).translationText;
                map.put(key, value);
            }
            data.put("translations", map);
        }
        return data;
    }

    public static EMLocationMessageBody localBodyFromJson(JSONObject json) throws JSONException {
        double latitude = json.getDouble("latitude");
        double longitude = json.getDouble("longitude");
        String address = null;
        String buildingName = null;
        if (json.has("address")){
            address = json.getString("address");
        }

        if (json.has("buildingName")){
            buildingName = json.getString("buildingName");
        }

        EMLocationMessageBody body = new EMLocationMessageBody(address, latitude, longitude, buildingName);

        return body;
    }

    public static JSONObject localBodyToJson(EMLocationMessageBody body) throws JSONException {
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("latitude", body.getLatitude());
        data.put("longitude", body.getLongitude());
        data.put("buildingName", body.getBuildingName());
        data.put("address", body.getAddress());
        return data;
    }

    public static EMCmdMessageBody cmdBodyFromJson(JSONObject json) throws JSONException {
        String action = json.getString("action");
        boolean deliverOnlineOnly = json.getBoolean("deliverOnlineOnly");

        EMCmdMessageBody body = new EMCmdMessageBody(action);
        body.deliverOnlineOnly(deliverOnlineOnly);

        return body;
    }

    public static JSONObject cmdBodyToJson(EMCmdMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("deliverOnlineOnly", body.isDeliverOnlineOnly());
        data.put("action", body.action());
        return data;
    }

    public static EMCustomMessageBody customBodyFromJson(JSONObject json) throws JSONException {
        String event = json.getString("event");
        EMCustomMessageBody body = new EMCustomMessageBody(event);

        if (json.has("params") && json.get("params") != JSONObject.NULL) {
            JSONObject jsonObject = json.getJSONObject("params");
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
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("event", body.event());
        if (body.getParams() != null) {
            JSONObject jsonObject = new JSONObject();
            for (Map.Entry<String, String> entry : body.getParams().entrySet()) {
                jsonObject.put(entry.getKey(), entry.getValue());
            }
            data.put("params", jsonObject);
        }
        return data;
    }

    public static EMFileMessageBody fileBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMNormalFileMessageBody body = new EMNormalFileMessageBody(file);
        if (json.has("displayName")){
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("remotePath")){
            body.setRemoteUrl(json.getString("remotePath"));
        }
        if (json.has("secret")){
            body.setSecret(json.getString("secret"));
        }
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        if (json.has("fileSize")){
            body.setFileLength(json.getInt("fileSize"));
        }

        return body;
    }

    public static JSONObject fileBodyToJson(EMNormalFileMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("localPath", body.getLocalUrl());
        data.put("fileSize", body.getFileSize());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("secret", body.getSecret());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        return data;
    }

    public static EMImageMessageBody imageBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);

        EMImageMessageBody body = new EMImageMessageBody(file);
        if (json.has("displayName")){
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("remotePath")){
            body.setRemoteUrl(json.getString("remotePath"));
        }
        if (json.has("secret")){
            body.setSecret(json.getString("secret"));
        }
        if (json.has("thumbnailLocalPath")) {
            body.setThumbnailLocalPath(json.getString("thumbnailLocalPath"));
        }
        if (json.has("thumbnailRemotePath")){
            body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        }
        if (json.has("thumbnailSecret")){
            body.setThumbnailSecret(json.getString("thumbnailSecret"));
        }
        if (json.has("fileSize")){
            body.setFileLength(json.getInt("fileSize"));
        }
        if (json.has("width") && json.has("height")){
            int width = json.getInt("width");
            int height = json.getInt("height");
            body.setThumbnailSize(width, height);
        }
        if (json.has("sendOriginalImage")){
            body.setSendOriginalImage(json.getBoolean("sendOriginalImage"));
        }

        if (json.has("fileStatus")){
            body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        }
        // 安卓没有暴露出来这个属性
//        if (json.has("thumbnailStatus")) {
//
//        }

        return body;
    }

    public static JSONObject imageBodyToJson(EMImageMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("localPath", body.getLocalUrl());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("secret", body.getSecret());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("thumbnailLocalPath", body.thumbnailLocalPath());
        data.put("thumbnailRemotePath", body.getThumbnailUrl());
        data.put("thumbnailSecret", body.getThumbnailSecret());
        data.put("thumbnailStatus", downloadStatusToInt(body.thumbnailDownloadStatus()));
        data.put("height", body.getHeight());
        data.put("width", body.getWidth());
        data.put("sendOriginalImage", body.isSendOriginalImage());
        data.put("fileSize", body.getFileSize());
        return data;
    }

    public static EMVideoMessageBody videoBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        int duration = json.getInt("duration");
        EMVideoMessageBody body = new EMVideoMessageBody(localPath, null, duration, 0);

        if (json.has("thumbnailRemotePath")){
            body.setThumbnailUrl(json.getString("thumbnailRemotePath"));
        }
        if (json.has("thumbnailLocalPath")) {
            body.setLocalThumb(json.getString("thumbnailLocalPath"));
        }
        if (json.has("thumbnailSecret")){
            body.setThumbnailSecret(json.getString("thumbnailSecret"));
        }
        if (json.has("displayName")){
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("remotePath")){
            body.setRemoteUrl(json.getString("remotePath"));
        }
        if (json.has("secret")){
            body.setSecret(json.getString("secret"));
        }
        if (json.has("fileSize")){
            body.setVideoFileLength(json.getInt("fileSize"));
        }

        if(json.has("fileStatus")){
            body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        }

        if (json.has("width") && json.has("height")){
            int width = json.getInt("width");
            int height = json.getInt("height");
            body.setThumbnailSize(width, height);
        }


        return body;
    }

    public static JSONObject videoBodyToJson(EMVideoMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("localPath", body.getLocalUrl());
        data.put("thumbnailLocalPath", body.getLocalThumbUri());
        data.put("duration", body.getDuration());
        data.put("thumbnailRemotePath", body.getThumbnailUrl());
        data.put("thumbnailSecret", body.getThumbnailSecret());
        data.put("thumbnailStatus", downloadStatusToInt(body.thumbnailDownloadStatus()));
        data.put("displayName", body.getFileName());
        data.put("height", body.getThumbnailHeight());
        data.put("width", body.getThumbnailWidth());
        data.put("remotePath", body.getRemoteUrl());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("secret", body.getSecret());
        data.put("fileSize", body.getVideoFileLength());

        return data;
    }

    public static EMVoiceMessageBody voiceBodyFromJson(JSONObject json) throws JSONException {
        String localPath = json.getString("localPath");
        File file = new File(localPath);
        int duration = json.getInt("duration");
        EMVoiceMessageBody body = new EMVoiceMessageBody(file, duration);
        body.setDownloadStatus(downloadStatusFromInt(json.getInt("fileStatus")));
        if (json.has("displayName")){
            body.setFileName(json.getString("displayName"));
        }
        if (json.has("secret")){
            body.setSecret(json.getString("secret"));
        }
        if (json.has("remotePath")){
            body.setRemoteUrl(json.getString("remotePath"));
        }
        if (json.has("fileSize")){
            body.setFileLength(json.getLong("fileSize"));
        }

        return body;
    }

    public static JSONObject voiceBodyToJson(EMVoiceMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject data = bodyToJson(body);
        data.put("localPath", body.getLocalUrl());
        data.put("duration", body.getLength());
        data.put("displayName", body.getFileName());
        data.put("remotePath", body.getRemoteUrl());
        data.put("fileStatus", downloadStatusToInt(body.downloadStatus()));
        data.put("secret", body.getSecret());
        data.put("fileSize", body.getFileSize());
        return data;
    }

    public static EMCombineMessageBody combineBodyFromJson(JSONObject json) throws JSONException {
        String title = null;
        if (json.has("title")) {
            title = json.optString("title");
        }
        String summary = null;
        if (json.has("summary")) {
            summary = json.optString("summary");
        }
        String compatibleText = null;
        if (json.has("compatibleText")) {
            compatibleText = json.optString("compatibleText");
        }
        List<String> msgList = EMHelper.stringListFromJsonArray(json.optJSONArray("messageList"));

        String remotePath = null;
        if (json.has("remotePath")) {
            remotePath = json.optString("remotePath");
        }
        String secret = null;
        if (json.has("secret")) {
            secret = json.optString("secret");
        }
        String localPath = null;
        if (json.has("localPath")) {
            localPath = json.optString("localPath");
        }

        EMCombineMessageBody body = new EMCombineMessageBody(title, summary, compatibleText, msgList);
        body.setRemoteUrl(remotePath);
        body.setSecret(secret);
        body.setLocalUrl(localPath);
        return body;
    }

    public static JSONObject combineBodyToJson(EMCombineMessageBody body) throws JSONException{
        if (body == null) return null;
        JSONObject jo = bodyToJson(body);
        jo.put("remotePath", body.getRemoteUrl());
        jo.put("secret", body.getSecret());
        jo.put("localPath", body.getLocalUrl());
        jo.put("title", body.getTitle());
        jo.put("summary", body.getSummary());
        jo.put("compatibleText", body.getCompatibleText());
        jo.put("remotePath", body.getRemoteUrl());
        return jo;
    }

    public  static EMFileMessageBody.EMDownloadStatus downloadStatusFromInt(int downloadStatus) {
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

    public  static int downloadStatusToInt(EMFileMessageBody.EMDownloadStatus downloadStatus) {
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
