package com.hyphenate.unity_chat_sdk;


import android.util.Log;


import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMLanguage;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;
import com.hyphenate.exceptions.HyphenateException;

import com.hyphenate.unity_chat_sdk.helper.EMConversationHelper;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;

import com.hyphenate.unity_chat_sdk.helper.EMLanguageHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageReactionHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityChatManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class EMChatManagerWrapper extends EMWrapper  {
    static public EMChatManagerWrapper wrapper() {
        return new EMChatManagerWrapper();
    }

    EMUnityChatManagerListener listener;

    public EMChatManagerWrapper() {
        listener = new EMUnityChatManagerListener();
        EMClient.getInstance().chatManager().addConversationListener(listener);
        EMClient.getInstance().chatManager().addMessageListener(listener);
    }

    private boolean deleteConversation(String conversationId, boolean deleteMessages) {

        if (conversationId == null || conversationId.length() == 0) { return false; }
        return EMClient.getInstance().chatManager().deleteConversation(conversationId, deleteMessages);
    }

    private void downloadAttachment(final String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
            EMClient.getInstance().chatManager().downloadAttachment(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "Message not found.");
            onError(callbackId, e);
        }    }

    private void downloadThumbnail(String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
            EMClient.getInstance().chatManager().downloadThumbnail(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "Message not found.");
            onError(callbackId, e);
        }
    }

    private void fetchHistoryMessages(String conversationId, final int type, String startMessageId, int count, int iDirection, String callbackId) {
        asyncRunnable(()->{
            EMConversation.EMConversationType conversationType = EMConversation.EMConversationType.Chat;
            if (type == 0) {
                conversationType = EMConversation.EMConversationType.Chat;
            }else if(type == 1) {
                conversationType = EMConversation.EMConversationType.GroupChat;
            }else if(type == 2) {
                conversationType = EMConversation.EMConversationType.ChatRoom;
            }

            if (conversationId == null) {
                HyphenateException e = new HyphenateException(500, "Conversation not found");
                onError(callbackId, e);
                return;
            }

            EMConversation.EMSearchDirection direction = iDirection == 0 ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
            try {
                EMCursorResult<EMMessage> cursorResult = EMClient.getInstance().chatManager().fetchHistoryMessages(conversationId, conversationType, count, startMessageId, direction);
                onSuccess("CursorResult<Message>", callbackId, EMCursorResultHelper.toJson(cursorResult).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException ignored) {

            }
        });
    }

    private String getConversation(String conversationId, int type, boolean createIfNeed) throws JSONException {
        EMConversation.EMConversationType conversationType = EMConversation.EMConversationType.Chat;
        if (type == 0) {
            conversationType = EMConversation.EMConversationType.Chat;
        }else if(type == 1) {
            conversationType = EMConversation.EMConversationType.GroupChat;
        }else if(type == 2) {
            conversationType = EMConversation.EMConversationType.ChatRoom;
        }
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conversationId, conversationType, createIfNeed);
        if (conversation == null) return null;
        return EMConversationHelper.toJson(conversation).toString();
    }

    private String getThreadConversation(String conversationId) throws JSONException {
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conversationId, EMConversation.EMConversationType.GroupChat, true, true);
        if (conversation == null) return null;
        return EMConversationHelper.toJson(conversation).toString();
    }

    private void getConversationsFromServer(String callbackId) {
        asyncRunnable(() -> {
            try {
                List<EMConversation> list = new ArrayList<>(EMClient.getInstance().chatManager().fetchConversationsFromServer().values());
                Collections.sort(list,
                        (o1, o2) -> (int) (o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime()));

                JSONArray jsonArray = new JSONArray();

                for (EMConversation conversation : list) {
                    jsonArray.put(EMConversationHelper.toJson(conversation).toString());
                }
                onSuccess("List<Conversation>", callbackId, jsonArray.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException ignored){

            }
        });
    }

    private int getUnreadMessageCount() {
        return EMClient.getInstance().chatManager().getUnreadMessageCount();
    }

    private boolean importMessages(String messagesString) throws JSONException {
        if (messagesString == null) {
            return false;
        }
        JSONArray jsonArray = new JSONArray(messagesString);
        List<EMMessage> list = new ArrayList<>();
        for (int i = 0; i < jsonArray.length(); i++) {
            JSONObject jsonObject = jsonArray.getJSONObject(i);
            list.add(EMMessageHelper.fromJson(jsonObject));
        }

        EMClient.getInstance().chatManager().importMessages(list);
        return true;
    }


    private String loadAllConversations() {
        List<EMConversation> list = new ArrayList<>(EMClient.getInstance().chatManager().getAllConversations().values());
        if (list == null) return null;
        Collections.sort(list, (o1, o2) -> {
            if (o1.getLastMessage() == null) {
                return 1;
            }

            if (o2.getLastMessage() == null) {
                return -1;
            }
            return o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime() > 0 ? 1 : -1;
        });
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMConversation conversation : list) {
                jsonArray.put(EMConversationHelper.toJson(conversation).toString());
            }
        }finally {
            return jsonArray.toString();
        }
    }

    private String loadMessage(String messageId) throws JSONException {
        if (messageId == null || messageId.length() == 0 ) return null;
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg == null) return  null;
        return EMMessageHelper.toJson(msg).toString();
    }

    private boolean markAllConversationsAsRead(){
        EMClient.getInstance().chatManager().markAllConversationsAsRead();
        return true;
    }

    private void recallMessage(String messageId, String callbackId) {
        asyncRunnable(() -> {
            try {
                if (messageId == null || messageId.length() == 0) {
                    onError(callbackId, new HyphenateException(500, "messageId is invalid"));
                    return;
                }
                EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
                if (msg != null) {
                    EMClient.getInstance().chatManager().recallMessage(msg);
                    onSuccess(null, callbackId, null);
                }else {
                    onError(callbackId, new HyphenateException(500, "Message not found"));
                    return;
                }
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private String resendMessage(String messageId, String callbackId) throws JSONException {
        if (messageId == null || messageId.length() == 0) {
            onError(callbackId, new HyphenateException(500, "messageId is invalid"));
            return null;
        }
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
            EMClient.getInstance().chatManager().sendMessage(msg);
        }else {
            onError(callbackId, new HyphenateException(500, "Message not found"));
            return null;
        }
        return EMMessageHelper.toJson(msg).toString();
    }

    private void searchChatMsgFromDB(String keywords, long timeStamp, int count, String from, String directionString, String callbackId) throws JSONException {
        EMConversation.EMSearchDirection direction = directionString.equals("up")  ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        List<EMMessage> msgList = EMClient.getInstance().chatManager().searchMsgFromDB(keywords, timeStamp, count, from, direction);
        if (msgList == null) return ;
        JSONArray jsonArray = new JSONArray();
        for (EMMessage msg : msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg).toString());
        }

        onSuccess("List<Message>", callbackId, jsonArray.toString());
    }

    private void ackConversationRead(String conversationId,  String callbackId){
        asyncRunnable(() -> {
            if (conversationId == null || conversationId.length() == 0) {
                onError(callbackId, new HyphenateException(500, "conversationId is invalid"));
                return ;
            }
            try {
                EMClient.getInstance().chatManager().ackConversationRead(conversationId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void sendReadAckForGroupMessage(String msgId, String content, String callbackId) {
        asyncRunnable(() -> {
            if (msgId == null || msgId.length() == 0) {
                onError(callbackId, new HyphenateException(500, "messageId is invalid"));
                return ;
            }
            EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
            if (msg == null) {
                onError(callbackId, new HyphenateException(500, "messageId is invalid"));
                return ;
            }
            try {
                EMClient.getInstance().chatManager().ackGroupMessageRead(msg.conversationId(), msgId, content);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private String sendMessage(String jsonString, String callbackId) throws JSONException {
        Log.d("unity_sdk","will send: " + jsonString);
        if (jsonString == null || jsonString.length() == 0) {
            onError(callbackId, new HyphenateException(501, "Message contains invalid content"));
            return null;
        }
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        msg.setMessageStatusCallback(new EMUnityCallback(callbackId){
            @Override
            public void onSuccess() {
                try {
                    EMChatManagerWrapper.this.onSuccess("OnMessageSuccess", callbackId, EMMessageHelper.toJson(msg).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(int i, String s) {
                try {
                    EMChatManagerWrapper.this.onSendMessageError(callbackId, EMMessageHelper.toJson(msg).toString(), i, s);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
        asyncRunnable(() -> {
            EMClient.getInstance().chatManager().sendMessage(msg);
        });
        return EMMessageHelper.toJson(msg).toString();
    }

    private void ackMessageRead(String messageId, String callbackId) throws JSONException {
        if (messageId == null || messageId.length() == 0) {
            onError(callbackId, new HyphenateException(500, "messageId is invalid"));
            return;
        }
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg == null) {
            onError(callbackId, new HyphenateException(500, "Message not found"));
        }else {
            try {
                EMClient.getInstance().chatManager().ackMessageRead(msg.getFrom(), messageId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        }
    }

    private boolean updateChatMessage(String jsonString) throws JSONException{
        if (jsonString == null || jsonString.length() == 0) {
            return false;
        }
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        EMClient.getInstance().chatManager().updateMessage(msg);
        return true;
    }

    private void removeMessageBeforeTimestamp(long timeStamp, String callbackId) throws JSONException {
        EMClient.getInstance().chatManager().deleteMessagesBeforeTimestamp(timeStamp, new EMUnityCallback(callbackId));
    }

    private void deleteConversationFromServer(String conversationId, int conversatinType, boolean deleteMessage, String callbackId) throws JSONException {
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(conversatinType);
        EMClient.getInstance().chatManager().deleteConversationFromServer(conversationId, type, deleteMessage, new EMUnityCallback(callbackId));
    }

    private void fetchGroupReadAcks(String messageId, int pageSize, String ackId, String callbackId) {
        asyncRunnable(()->{
            try {
                EMCursorResult<EMGroupReadAck> cursorResult = EMClient.getInstance().chatManager().fetchGroupReadAcks(messageId, pageSize, ackId);
                onSuccess("CursorResult<GroupReadAck>", callbackId, EMCursorResultHelper.toJson(cursorResult).toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException ignored) {

            }
        });
    }

    private void fetchSupportLanguages(String callbackId) {
        EMClient.getInstance().chatManager().fetchSupportLanguages(new EMUnityValueCallback<List<EMLanguage>>("List<SupportLanguage>", callbackId){
            @Override
            public void onSuccess(List<EMLanguage> emLanguages) {
                ArrayList<Map> list = new ArrayList<>();
                for (EMLanguage l: emLanguages) {
                    list.add(EMLanguageHelper.toJson(l));
                }
                sendJsonObjectToUnity(list.toString());
            }
        });
    }

    private void translateMessage(String jsonString, String targetLanguages , String callbackId) throws JSONException {
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        JSONArray jAry = new JSONArray(targetLanguages);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jAry.length(); i++) {
            String s = jAry.getString(i);
            list.add(s);
        }
        EMClient.getInstance().chatManager().translateMessage(msg, list, new EMUnityValueCallback<EMMessage>("Message", callbackId){
            @Override
            public void onSuccess(EMMessage msg) {
                try {
                    String ret = EMMessageHelper.toJson(msg).toString();
                    sendJsonObjectToUnity(ret);
                } catch (JSONException e) {

                }
            }
        });
    }

    private void reportMessage(String messageId, String tag, String reason, String callbackId) {
        EMClient.getInstance().chatManager().asyncReportMessage(messageId, tag, reason, new EMUnityCallback(callbackId));
    }

    public void addReaction(String msgId, String reaction, String callbackId) throws JSONException {
        EMClient.getInstance().chatManager().asyncAddReaction(msgId, reaction, new EMUnityCallback(callbackId));
    }

    private void removeReaction(String msgId, String reaction, String callbackId) throws JSONException {
        EMClient.getInstance().chatManager().asyncRemoveReaction(msgId, reaction, new EMUnityCallback(callbackId));
    }

    public void getReactionList(String msgListJsonString, int iMessageType, String groupId, String callbackId) throws JSONException{
        JSONArray jAry = new JSONArray(msgListJsonString);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jAry.length(); i++) {
            String s = jAry.getString(i);
            list.add(s);
        }

        EMMessage.ChatType type = EMMessageHelper.chatTypeFromInt(iMessageType);
        EMClient.getInstance().chatManager().asyncGetReactionList(list, type, groupId, new EMUnityValueCallback<Map<String, java.util.List<EMMessageReaction>>>("Dictionary<string, List<MessageReaction>>", callbackId){
            @Override
            public void onSuccess(Map<String, List<EMMessageReaction>> stringListMap) {
                Map<String, ArrayList< Map<String, Object>>> map = new HashMap<>();
                for (Map.Entry<String, List<EMMessageReaction>> entry: stringListMap.entrySet()) {
                    ArrayList< Map<String, Object>> list = new ArrayList<>();
                    for (EMMessageReaction reaction: entry.getValue()) {
                        list.add(EMMessageReactionHelper.toJson(reaction));
                    }
                    map.put(entry.getKey(), list);
                }
                sendJsonObjectToUnity(map.toString());
            }
        });
    }

    private void getReactionDetail(String messageId, String reaction, String cursor, int pageSize, String callbackId) {
        EMClient.getInstance().chatManager().asyncGetReactionDetail(messageId, reaction, cursor, pageSize, new EMUnityValueCallback<EMCursorResult<EMMessageReaction>>("CursorResult<MessageReaction>", callbackId) {
            @Override
            public void onSuccess(EMCursorResult<EMMessageReaction> emMessageReactionEMCursorResult) {
                try {
                    sendJsonObjectToUnity(EMCursorResultHelper.toJson(emMessageReactionEMCursorResult).toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        });
    }
}