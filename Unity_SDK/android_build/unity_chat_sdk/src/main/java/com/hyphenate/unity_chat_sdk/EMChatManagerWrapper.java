package com.hyphenate.unity_chat_sdk;

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

        private void sendMessage(String jsonString, String callbackId) throws JSONException {
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(jsonString));
        msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
        asyncRunnable(() -> {
            EMClient.getInstance().chatManager().sendMessage(msg);
            try {
                onSuccess("EMMessage", callbackId, EMMessageHelper.toJson(msg).toString());
            } catch (JSONException jsonException) {
                jsonException.printStackTrace();
            }
        });
    }

    private String resendMessage(String messageId, String callbackId) throws JSONException {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback(callbackId));
            EMClient.getInstance().chatManager().sendMessage(msg);
        }else {
            onError(callbackId, new HyphenateException(500, "message not found"));
            return null;
        }
        return EMMessageHelper.toJson(msg).toString();
    }

    private void ackMessageRead(String messageId, String callbackId) throws JSONException {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg == null) {
            onError(callbackId, new HyphenateException(500, "message not found"));
        }else {
            try {
                EMClient.getInstance().chatManager().ackMessageRead(msg.getFrom(), messageId);
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        }
    }
    private void ackConversationRead(String conversationId,  String callbackId){
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatManager().ackConversationRead(conversationId);
                onSuccess(null, callbackId, Boolean.TRUE.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private void recallMessage(String messageId, String callbackId) {
        asyncRunnable(() -> {
            try {
                EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
                if (msg != null) {
                    EMClient.getInstance().chatManager().recallMessage(msg);
                }else {
                    onError(callbackId, new HyphenateException(500, "message not found"));
                }
                onSuccess(null, callbackId, null);
            } catch (HyphenateException e) {
                onError(callbackId, e);
            }
        });
    }

    private String getMessage(String messageId) throws JSONException {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        return EMMessageHelper.toJson(msg).toString();
    }
    //
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
        return EMConversationHelper.toJson(conversation).toString();
    }

    private boolean markAllChatMsgAsRead(){
        EMClient.getInstance().chatManager().markAllConversationsAsRead();
        return true;
    }

    private int getUnreadMessageCount() {
        return EMClient.getInstance().chatManager().getUnreadMessageCount();
    }

    private void updateChatMessage(String messageString, String callbackId) throws JSONException{
        EMMessage msg = EMMessageHelper.fromJson(new JSONObject(messageString));

        asyncRunnable(() -> {
            EMClient.getInstance().chatManager().updateMessage(msg);
            onSuccess(null, callbackId, Boolean.TRUE.toString());
        });
    }

    private boolean importMessages(String messagesString) throws JSONException {
        JSONArray jsonArray = new JSONArray(messagesString);
        List<EMMessage> list = new ArrayList<>();
        for (int i = 0; i < jsonArray.length(); i++) {
            JSONObject jsonObject = jsonArray.getJSONObject(i);
            list.add(EMMessageHelper.fromJson(jsonObject));
        }

        EMClient.getInstance().chatManager().importMessages(list);
        return true;
    }

    private void downloadAttachment(String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback((callbackId)));
            EMClient.getInstance().chatManager().downloadAttachment(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "message not found.");
            onError(callbackId, e);
        }
    }

    private void downloadThumbnail(String messageId, String callbackId) {
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(messageId);
        if (msg != null) {
            msg.setMessageStatusCallback(new EMUnityCallback((callbackId)));
            EMClient.getInstance().chatManager().downloadThumbnail(msg);
        }else {
            HyphenateException e = new HyphenateException(500, "message not found.");
            onError(callbackId, e);
        }
    }

    private String loadAllConversations() {
        List<EMConversation> list = new ArrayList<>(EMClient.getInstance().chatManager().getAllConversations().values());
        Collections.sort(list, (o1, o2) -> {
            if (o1.getLastMessage() == null) {
                return 1;
            }

            if (o2.getLastMessage() == null) {
                return -1;
            }
            return (int) (o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime());
        });
        JSONArray jsonArray = new JSONArray();
        try {
            for (EMConversation conversation : list) {
                jsonArray.put(EMConversationHelper.toJson(conversation));
            }
        }catch (JSONException ignored) {

        }

        return jsonArray.toString();
    }

    private void getConversationsFromServer(String callbackId) {
        asyncRunnable(() -> {
            try {
                List<EMConversation> list = new ArrayList<>(
                        EMClient.getInstance().chatManager().fetchConversationsFromServer().values());
                Collections.sort(list,
                        (o1, o2) -> (int) (o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime()));
                JSONArray jsonArray = new JSONArray();

                for (EMConversation conversation : list) {
                    jsonArray.put(EMConversationHelper.toJson(conversation));
                }
                onSuccess("List<EMConversation>", callbackId, jsonArray.toString());
            } catch (HyphenateException e) {
                onError(callbackId, e);
            } catch (JSONException ignored){

            }
        });
    }

    private boolean deleteConversation(String conversationId, boolean deleteMessages) {
        return EMClient.getInstance().chatManager().deleteConversation(conversationId, deleteMessages);
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

    private String searchChatMsgFromDB(String keywords, long timeStamp, int count, String from, String directionString) throws JSONException {
        EMConversation.EMSearchDirection direction = directionString == "up" ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
        List<EMMessage> msgList = EMClient.getInstance().chatManager().searchMsgFromDB(keywords, timeStamp, count, from, direction);
        if (msgList == null) return null;
        JSONArray jsonArray = new JSONArray();
        for (EMMessage msg : msgList) {
            jsonArray.put(EMMessageHelper.toJson(msg));
        }

        return jsonArray.toString();
    }

}
