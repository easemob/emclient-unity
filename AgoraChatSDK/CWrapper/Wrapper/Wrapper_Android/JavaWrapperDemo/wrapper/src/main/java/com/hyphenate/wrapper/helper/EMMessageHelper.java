package com.hyphenate.wrapper.helper;

import com.hyphenate.chat.EMCmdMessageBody;
import com.hyphenate.chat.EMCombineMessageBody;
import com.hyphenate.chat.EMCustomMessageBody;
import com.hyphenate.chat.EMImageMessageBody;
import com.hyphenate.chat.EMLocationMessageBody;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMNormalFileMessageBody;
import com.hyphenate.chat.EMTextMessageBody;
import com.hyphenate.chat.EMVideoMessageBody;
import com.hyphenate.chat.EMVoiceMessageBody;
import com.hyphenate.wrapper.util.EMHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMMessageHelper {
    public static EMMessage fromJson(JSONObject json) throws JSONException {
        EMMessage message = null;
        JSONObject bodyJson = json.getJSONObject("body");
        int type = bodyJson.getInt("type");
        if (json.getInt("direction") == 0) {
            switch (type) {
                case 0: {
                    message = EMMessage.createSendMessage(EMMessage.Type.TXT);
                    message.addBody(EMMessageBodyHelper.textBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 1: {
                    message = EMMessage.createSendMessage(EMMessage.Type.IMAGE);
                    message.addBody(EMMessageBodyHelper.imageBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 3: {
                    message = EMMessage.createSendMessage(EMMessage.Type.LOCATION);
                    message.addBody(EMMessageBodyHelper.localBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 2: {
                    message = EMMessage.createSendMessage(EMMessage.Type.VIDEO);
                    message.addBody(EMMessageBodyHelper.videoBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 4: {
                    message = EMMessage.createSendMessage(EMMessage.Type.VOICE);
                    message.addBody(EMMessageBodyHelper.voiceBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 5: {
                    message = EMMessage.createSendMessage(EMMessage.Type.FILE);
                    message.addBody(EMMessageBodyHelper.fileBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 6: {
                    message = EMMessage.createSendMessage(EMMessage.Type.CMD);
                    message.addBody(EMMessageBodyHelper.cmdBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 7: {
                    message = EMMessage.createSendMessage(EMMessage.Type.CUSTOM);
                    message.addBody(EMMessageBodyHelper.customBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                case 8: {
                    message = EMMessage.createSendMessage(EMMessage.Type.COMBINE);
                    message.addBody(EMMessageBodyHelper.combineBodyFromJson(bodyJson.getJSONObject("body")));
                }
                break;
                default:
                    break;
            }
            if (message != null) {
                message.setDirection(EMMessage.Direct.SEND);
            }
        } else {
            switch (type) {
                case 0: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.TXT);
                    message.addBody(EMMessageBodyHelper.textBodyFromJson(bodyJson));
                }
                break;
                case 1: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.IMAGE);
                    message.addBody(EMMessageBodyHelper.imageBodyFromJson(bodyJson));
                }
                break;
                case 3: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.LOCATION);
                    message.addBody(EMMessageBodyHelper.localBodyFromJson(bodyJson));
                }
                break;
                case 2: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.VIDEO);
                    message.addBody(EMMessageBodyHelper.videoBodyFromJson(bodyJson));
                }
                break;
                case 4: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.VOICE);
                    message.addBody(EMMessageBodyHelper.voiceBodyFromJson(bodyJson));
                }
                break;
                case 5: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.FILE);
                    message.addBody(EMMessageBodyHelper.fileBodyFromJson(bodyJson));
                }
                break;
                case 6: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.CMD);
                    message.addBody(EMMessageBodyHelper.cmdBodyFromJson(bodyJson));
                }
                break;
                case 7: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.CUSTOM);
                    message.addBody(EMMessageBodyHelper.customBodyFromJson(bodyJson));
                }
                break;
                case 8: {
                    message = EMMessage.createReceiveMessage(EMMessage.Type.COMBINE);
                    message.addBody(EMMessageBodyHelper.combineBodyFromJson(bodyJson));
                }
                break;
            }
            if (message != null) {
                message.setDirection(EMMessage.Direct.RECEIVE);
            }
        }

        if (json.has("to")) {
            message.setTo(json.getString("to"));
        }

        if (json.has("isRead")) {
            message.setUnread(!json.getBoolean("isRead"));
        }

        if (json.has("from")) {
            message.setFrom(json.getString("from"));
        }

        message.setAcked(json.getBoolean("hasReadAck"));
        if (statusFromInt(json.getInt("status")) == EMMessage.Status.SUCCESS) {
            message.setUnread(!json.getBoolean("isRead"));
        }
        message.setDeliverAcked(json.getBoolean("hasDeliverAck"));
        message.setIsNeedGroupAck(json.getBoolean("isNeedGroupAck"));
        if (json.has("groupAckCount")) {
            message.setGroupAckCount(json.getInt("groupAckCount"));
        }

        if (json.has("receiverList")){
            message.setReceiverList(EMHelper.stringListFromJsonArray(json.getJSONArray("receiverList")));
        }

        message.setIsChatThreadMessage(json.getBoolean("isThread"));

        message.setLocalTime(json.getLong("localTime"));
        if (json.has("serverTime")){
            message.setMsgTime(json.getLong("serverTime"));
        }
        message.setStatus(statusFromInt(json.getInt("status")));
        message.setChatType(chatTypeFromInt(json.getInt("chatType")));
        if (json.has("msgId")){
            message.setMsgId(json.getString("msgId"));
        }

        if (json.has("priority")) {
            int intPriority = json.getInt("priority");
            if (intPriority == 0) {
                message.setPriority(EMMessage.EMChatRoomMessagePriority.PriorityHigh);
            }else if (intPriority == 1) {
                message.setPriority(EMMessage.EMChatRoomMessagePriority.PriorityNormal);
            }else if (intPriority == 2) {
                message.setPriority(EMMessage.EMChatRoomMessagePriority.PriorityLow);
            }
        }

        if (json.has("deliverOnlineOnly")) {
            message.deliverOnlineOnly(json.getBoolean("deliverOnlineOnly"));
        }

        if(json.has("attr")){
            JSONObject data = json.getJSONObject("attr");
            Iterator iterator = data.keys();
            while (iterator.hasNext()) {
                String key = iterator.next().toString();
                JSONObject result = data.getJSONObject(key);
                String valueType = result.getString("type");
                String value = result.getString("value");
                if (valueType.equals("b")) {
                    if (value.equalsIgnoreCase("false")) {
                        message.setAttribute(key, false);
                    } else {
                        message.setAttribute(key, true);
                    }
                } else if (valueType.equals("l")) {
                    message.setAttribute(key, Long.valueOf(value));
                } else if (valueType.equals("f")) {
                    message.setAttribute(key, Float.valueOf(value));
                } else if (valueType.equals("d")) {
                    message.setAttribute(key, Double.valueOf(value));
                } else if (valueType.equals("i")) {
                    message.setAttribute(key,Integer.valueOf(value));
                } else if (valueType.equals("str")) {
                    message.setAttribute(key, value);
                } else if (valueType.equals("jstr")) {
                    boolean hasAdd = false;
                    do {
                        try {
                            JSONObject jo = new JSONObject(value);
                            message.setAttribute(key, jo);
                            hasAdd = true;
                            break;
                        }catch (JSONException ignored){}
                        try {
                            JSONArray ja = new JSONArray(value);
                            message.setAttribute(key, ja);
                            hasAdd = true;
                            break;
                        }catch (JSONException ignored){}
                    }while (hasAdd);
                }
            }
        }
        return message;
    }

    public static JSONObject toJson(EMMessage message) throws JSONException{
        if (message == null) return null;
        JSONObject data = new JSONObject();
        JSONObject bodyData = new JSONObject();
        switch (message.getType()) {
            case TXT: {
                bodyData.put("body", EMMessageBodyHelper.textBodyToJson((EMTextMessageBody) message.getBody()));
                bodyData.put("type", 0);
            }
            break;
            case IMAGE: {
                bodyData.put("body", EMMessageBodyHelper.imageBodyToJson((EMImageMessageBody) message.getBody()));
                bodyData.put("type", 1);
            }
            break;
            case LOCATION: {
                bodyData.put("body", EMMessageBodyHelper.localBodyToJson((EMLocationMessageBody) message.getBody()));
                bodyData.put("type", 3);
            }
            break;
            case CMD: {
                bodyData.put("body", EMMessageBodyHelper.cmdBodyToJson((EMCmdMessageBody) message.getBody()));
                bodyData.put("type", 6);
            }
            break;
            case CUSTOM: {
                bodyData.put("body", EMMessageBodyHelper.customBodyToJson((EMCustomMessageBody) message.getBody()));
                bodyData.put("type", 7);
            }
            break;
            case FILE: {
                bodyData.put("body", EMMessageBodyHelper.fileBodyToJson((EMNormalFileMessageBody) message.getBody()));
                bodyData.put("type", 5);
            }
            break;
            case VIDEO: {
                bodyData.put("body", EMMessageBodyHelper.videoBodyToJson((EMVideoMessageBody) message.getBody()));
                bodyData.put("type", 2);
            }
            break;
            case VOICE: {
                bodyData.put("body", EMMessageBodyHelper.voiceBodyToJson((EMVoiceMessageBody) message.getBody()));
                bodyData.put("type", 4);
            }
            case COMBINE: {
                bodyData.put("body", EMMessageBodyHelper.combineBodyToJson((EMCombineMessageBody) message.getBody()));
                bodyData.put("type", 8);
            }
            break;
        }
        data.put("body", bodyData);
        if (message.ext().size() > 0 && null != message.ext()) {
            Map<String, Object> ext = message.ext();
            JSONObject jo = new JSONObject();
            for (Map.Entry<String, Object> enter: ext.entrySet()) {
                String key = enter.getKey();
                Object value = enter.getValue();
                JSONObject js = new JSONObject();
                if (value instanceof String) {
                    js.put("type", "str");
                    js.put("value", value);
                }else if (value instanceof Integer) {
                    js.put("type", "i");
                    js.put("value", value);
                }else if (value instanceof Float) {
                    js.put("type", "f");
                    js.put("value", value);
                }else if (value instanceof Double) {
                    js.put("type", "d");
                    js.put("value", value);
                }else if (value instanceof Long) {
                    js.put("type", "l");
                    js.put("value", value);
                }else if (value instanceof Boolean) {
                    js.put("type", "b");
                    Boolean b = (Boolean)value;
                    if (b) {
                        js.put("value", "True");
                    }else {
                        js.put("value", "False");
                    }
                }else if (value instanceof JSONArray || value instanceof JSONObject) {
                    js.put("type", "jstr");
                    js.put("value", value);
                }
                jo.put(key, js);
            }
            data.put("attr", jo);
        }
        data.put("from", message.getFrom());
        data.put("to", message.getTo());
        data.put("hasReadAck", message.isAcked());
        data.put("hasDeliverAck", message.isDelivered());
        data.put("localTime", message.localTime());
        data.put("serverTime", message.getMsgTime());
        data.put("status", statusToInt(message.status()));
        data.put("chatType", chatTypeToInt(message.getChatType()));
        data.put("direction", message.direct() == EMMessage.Direct.SEND ? 0 : 1);
        data.put("convId", message.conversationId());
        data.put("msgId", message.getMsgId());
        data.put("isRead", !message.isUnread());
        data.put("isNeedGroupAck", message.isNeedGroupAck());
        data.put("messageOnlineState", message.isOnlineState());
        // 通过EMMessageWrapper获取
        // data.put("groupAckCount", message.groupAckCount());
        data.put("isThread", message.isChatThreadMessage());
        data.put("deliverOnlineOnly", message.isDeliverOnlineOnly());
        data.put("broadcast", message.isBroadcast());
        data.put("isContentReplaced", message.isContentReplaced());
        return data;
    }

    public static EMMessage.ChatType chatTypeFromInt(int type) {
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

    public static int chatTypeToInt(EMMessage.ChatType type) {
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

    public static EMMessage.Status statusFromInt(int status) {
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

    public static int statusToInt(EMMessage.Status status) {
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
