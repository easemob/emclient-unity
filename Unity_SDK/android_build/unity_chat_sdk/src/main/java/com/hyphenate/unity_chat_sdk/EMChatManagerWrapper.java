package com.hyphenate.unity_chat_sdk;

import android.os.Debug;
import android.os.Message;
import android.util.Log;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.unity_chat_sdk.helper.EMConversationHelper;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityChatManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class EMChatManagerWrapper extends EMWrapper  {
    static public EMChatManagerWrapper wrapper() {
        return new EMChatManagerWrapper();
    }

    public EMChatManagerWrapper() {
        EMUnityChatManagerListener listener = new EMUnityChatManagerListener();
        EMClient.getInstance().chatManager().addConversationListener(listener);
        EMClient.getInstance().chatManager().addMessageListener(listener);
    }

    private boolean deleteConversation(String conversationId, boolean deleteMessages) {

        if (conversationId == null || conversationId.length() == 0) { return false; }
        return EMClient.getInstance().chatManager().deleteConversation(conversationId, deleteMessages);
    }

    private void downloadAttachment(String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback((callbackId)));
            EMClient.getInstance().chatManager().downloadAttachment(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "Message not found.");
            onError(callbackId, e);
        }
    }

    private void downloadThumbnail(String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback((callbackId)));
            EMClient.getInstance().chatManager().downloadThumbnail(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "Message not found.");
            onError(callbackId, e);
        }
    }

    private void fetchHistoryMessages(String conversationId, final int type, String startMessageId, int count,String callbackId) {
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

            try {
                EMCursorResult<EMMessage> cursorResult = EMClient.getInstance().chatManager().fetchHistoryMessages(conversationId,
                        conversationType, count, startMessageId.length() > 0 ? startMessageId : null);
                onSuccess("EMCursorResult<EMMessage>", callbackId, EMCursorResultHelper.toJson(cursorResult).toString());
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
                onSuccess("List<EMConversation>", callbackId, jsonArray.toString());
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

    private String searchChatMsgFromDB(String keywords, long timeStamp, int count, String from, String directionString) throws JSONException {
        EMConversation.EMSearchDirection direction = directionString == "up" ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        List<EMMessage> msgList = EMClient.getInstance().chatManager().searchMsgFromDB(keywords, timeStamp, count, from, direction);
        if (msgList == null) return null;
        JSONArray jsonArray = new JSONArray();
        for (EMMessage msg : msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg).toString());
        }

        return jsonArray.toString();
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

    private String sendMessage(String jsonString, String callbackId) throws JSONException {
        Log.d("unity_sdk","will send: " + jsonString);
        if (jsonString == null || jsonString.length() == 0) {
            onError(callbackId, new HyphenateException(501, "Message contains invalid content"));
            return null;
        }
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
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

    private void updateChatMessage(String jsonString, String callbackId) throws JSONException{
        if (jsonString == null || jsonString.length() == 0) {
            onError(callbackId, new HyphenateException(501, "Message contains invalid content"));
            return;
        }
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        EMMessage message = EMClient.getInstance().chatManager().getMessage(msg.getMsgId());
        if (message == null) {
            onError(callbackId, new HyphenateException(500, "Message not found"));
        }else {
            asyncRunnable(() -> {
                EMClient.getInstance().chatManager().updateMessage(msg);
                onSuccess(null, callbackId, Boolean.TRUE);
            });
        }
    }
}