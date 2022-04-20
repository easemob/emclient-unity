package com.hyphenate.unity_chat_sdk.helper;

import com.hyphenate.chat.EMCmdMessageBody;
import com.hyphenate.chat.EMCustomMessageBody;
import com.hyphenate.chat.EMImageMessageBody;
import com.hyphenate.chat.EMLocationMessageBody;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMNormalFileMessageBody;
import com.hyphenate.chat.EMTextMessageBody;
import com.hyphenate.chat.EMVideoMessageBody;
import com.hyphenate.chat.EMVoiceMessageBody;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

public class EMMessageHelper {
    public static EMMessage fromJson(JSONObject json) throws JSONException {
        EMMessage message = null;
        String bodyString = json.getString("body");
        JSONObject bodyJson = new JSONObject(bodyString);
        String type = json.getString("bodyType");
        if (json.getString("direction").equals("send")) {
            switch (type) {
                case "txt": {
                    message = EMMessage.createSendMessage(EMMessage.Type.TXT);
                    message.addBody(EMMessageBodyHelper.textBodyFromJson(bodyJson));
                }
                break;
                case "img": {
                    message = EMMessage.createSendMessage(EMMessage.Type.IMAGE);
                    message.addBody(EMMessageBodyHelper.imageBodyFromJson(bodyJson));
                }
                break;
                case "loc": {
                    message = EMMessage.createSendMessage(EMMessage.Type.LOCATION);
                    message.addBody(EMMessageBodyHelper.localBodyFromJson(bodyJson));
                }
                break;
                case "video": {
                    message = EMMessage.createSendMessage(EMMessage.Type.VIDEO);
                    message.addBody(EMMessageBodyHelper.videoBodyFromJson(bodyJson));
                }
                break;
                case "voice": {
                    message = EMMessage.createSendMessage(EMMessage.Type.VOICE);
                    message.addBody(EMMessageBodyHelper.voiceBodyFromJson(bodyJson));
                }
                break;
                case "file": {
                    message = EMMessage.createSendMessage(EMMessage.Type.FILE);
                    message.addBody(EMMessageBodyHelper.fileBodyFromJson(bodyJson));
                }
                break;
                case "cmd": {
                    message = EMMessage.createSendMessage(EMMessage.Type.CMD);
                    message.addBody(EMMessageBodyHelper.cmdBodyFromJson(bodyJson));
                }
                break;
                case "custom": {
                    message = EMMessage.createSendMessage(EMMessage.Type.CUSTOM);
                    message.addBody(EMMessageBodyHelper.customBodyFromJson(bodyJson));
                }
                break;
            }
        } else {
            switch (type) {
                case "txt": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.TXT);
                    message.addBody(EMMessageBodyHelper.textBodyFromJson(bodyJson));
                }
                break;
                case "img": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.IMAGE);
                    message.addBody(EMMessageBodyHelper.imageBodyFromJson(bodyJson));
                }
                break;
                case "loc": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.LOCATION);
                    message.addBody(EMMessageBodyHelper.localBodyFromJson(bodyJson));
                }
                break;
                case "video": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.VIDEO);
                    message.addBody(EMMessageBodyHelper.videoBodyFromJson(bodyJson));
                }
                break;
                case "voice": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.VOICE);
                    message.addBody(EMMessageBodyHelper.voiceBodyFromJson(bodyJson));
                }
                break;
                case "file": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.FILE);
                    message.addBody(EMMessageBodyHelper.fileBodyFromJson(bodyJson));
                }
                break;
                case "cmd": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.CMD);
                    message.addBody(EMMessageBodyHelper.cmdBodyFromJson(bodyJson));
                }
                break;
                case "custom": {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.CUSTOM);
                    message.addBody(EMMessageBodyHelper.customBodyFromJson(bodyJson));
                }
                break;
            }
        }
        message.setTo(json.getString("to"));
        message.setFrom(json.getString("from"));
        message.setAcked(json.getBoolean("hasReadAck"));
        if (statusFromInt(json.getInt("status")) == EMMessage.Status.SUCCESS) {
            message.setUnread(!json.getBoolean("hasRead"));
        }
        message.setDeliverAcked(json.getBoolean("hasDeliverAck"));
        message.setLocalTime(Long.parseLong(json.getString("localTime")));
        message.setMsgTime(Long.parseLong(json.getString("serverTime")));

        message.setStatus(statusFromInt(json.getInt("status")));
        message.setChatType(chatTypeFromInt(json.getInt("chatType")));
        message.setMsgId(json.getString("msgId"));
        if (!json.isNull("attributes") && null != json.getJSONObject("attributes")) {
            JSONObject data = json.getJSONObject("attributes");
            Iterator iterator = data.keys();
            while (iterator.hasNext()) {
                String key = iterator.next().toString();
                JSONObject result = data.getJSONObject(key);
                String valueType = result.getString("type");
                String value = result.getString("value");
                if (valueType.equals("b")) {
                    if (value.toLowerCase().equals("false")) {
                        message.setAttribute(key, false);
                    }else {
                        message.setAttribute(key, true);
                    }
                }else if (valueType.equals("i")) {
                    message.setAttribute(key,Integer.valueOf(value));
                }else if (valueType.equals("ui")) {
                    message.setAttribute(key, Integer.valueOf(value));
                }else if (valueType.equals("l")) {
                    message.setAttribute(key, Long.valueOf(value));
                }else if (valueType.equals("str")) {
                    message.setAttribute(key, value);
                }else if (valueType.equals("strv")) {
                    
                }else if (valueType.equals("jstr")) {

                }




//                if (result.getClass().getSimpleName().equals("Integer")) {
//                    message.setAttribute(key, (Integer) result);
//                } else if (result.getClass().getSimpleName().equals("Boolean")) {
//                    message.setAttribute(key, (Boolean) result);
//                } else if (result.getClass().getSimpleName().equals("Long")) {
//                    message.setAttribute(key, (Long) result);
//                } else if (result.getClass().getSimpleName().equals("JSONObject")) {
//                    message.setAttribute(key, (JSONObject) result);
//                } else if (result.getClass().getSimpleName().equals("JSONArray")) {
//                    message.setAttribute(key, (JSONArray) result);
//                } else {
//                    message.setAttribute(key, data.getString(key));
//                }
            }
        }
        return message;
    }

    public static JSONObject toJson(EMMessage message) throws JSONException {
        if (message == null)
            return null;
        JSONObject data = new JSONObject();
        String type = "";
        switch (message.getType()) {
            case TXT: {
                type = "txt";
                data.put("body", EMMessageBodyHelper.textBodyToJson((EMTextMessageBody) message.getBody()).toString());
            }
            break;
            case IMAGE: {
                type = "img";
                data.put("body", EMMessageBodyHelper.imageBodyToJson((EMImageMessageBody) message.getBody()).toString());
            }
            break;
            case LOCATION: {
                type = "loc";
                data.put("body", EMMessageBodyHelper.localBodyToJson((EMLocationMessageBody) message.getBody()).toString());
            }
            break;
            case CMD: {
                type = "cmd";
                data.put("body", EMMessageBodyHelper.cmdBodyToJson((EMCmdMessageBody) message.getBody()).toString());
            }
            break;
            case CUSTOM: {
                type = "custom";
                data.put("body", EMMessageBodyHelper.customBodyToJson((EMCustomMessageBody) message.getBody()).toString());
            }
            break;
            case FILE: {
                type = "file";
                data.put("body", EMMessageBodyHelper.fileBodyToJson((EMNormalFileMessageBody) message.getBody()).toString());
            }
            break;
            case VIDEO: {
                type = "video";
                data.put("body", EMMessageBodyHelper.videoBodyToJson((EMVideoMessageBody) message.getBody()).toString());
            }
            break;
            case VOICE: {
                type = "voice";
                data.put("body", EMMessageBodyHelper.voiceBodyToJson((EMVoiceMessageBody) message.getBody()).toString());
            }
            break;
        }
        data.put("bodyType",type);

        if (message.ext().size() > 0 && null != message.ext()) {
            data.put("attributes", message.ext());
        }
        data.put("from", message.getFrom());
        data.put("to", message.getTo());
        data.put("hasReadAck", message.isAcked());
        data.put("hasDeliverAck", message.isDelivered());
        data.put("localTime", String.valueOf(message.localTime()));
        data.put("serverTime", String.valueOf(message.getMsgTime()));
        data.put("status", statusToInt(message.status()));
        data.put("chatType", chatTypeToInt(message.getChatType()));
        data.put("direction", message.direct() == EMMessage.Direct.SEND ? "send" : "rec");
        data.put("conversationId", message.conversationId());
        data.put("msgId", message.getMsgId());
        data.put("hasRead", !message.isUnread());

        return data;
    }

    private static EMMessage.ChatType chatTypeFromInt(int type) {
        switch (type) {
            case 0:
                return EMMessage.ChatType.Chat;
            case 1:
                return EMMessage.ChatType.GroupChat;
            case 2:
                return EMMessage.ChatType.ChatRoom;
        }
        return EMMessage.ChatType.Chat;
    }

    private static int chatTypeToInt(EMMessage.ChatType type) {
        switch (type) {
            case Chat:
                return 0;
            case GroupChat:
                return 1;
            case ChatRoom:
                return 2;
        }
        return 0;
    }

    private static EMMessage.Status statusFromInt(int status) {
        switch (status) {
            case 0:
                return EMMessage.Status.CREATE;
            case 1:
                return EMMessage.Status.INPROGRESS;
            case 2:
                return EMMessage.Status.SUCCESS;
            case 3:
                return EMMessage.Status.FAIL;
        }
        return EMMessage.Status.CREATE;
    }

    private static int statusToInt(EMMessage.Status status) {
        switch (status) {
            case CREATE:
                return 0;
            case INPROGRESS:
                return 1;
            case SUCCESS:
                return 2;
            case FAIL:
                return 3;
        }
        return 0;
    }
}
