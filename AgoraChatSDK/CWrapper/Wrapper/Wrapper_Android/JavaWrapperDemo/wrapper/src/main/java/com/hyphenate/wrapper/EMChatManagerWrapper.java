package com.hyphenate.wrapper;

import com.hyphenate.EMCallBack;
import com.hyphenate.EMConversationListener;
import com.hyphenate.EMError;
import com.hyphenate.EMMessageListener;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMGroupReadAck;
import com.hyphenate.chat.EMLanguage;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.chat.EMMessageReaction;
import com.hyphenate.chat.EMMessageReactionChange;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.callback.EMCommonCallback;

import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMConversationHelper;
import com.hyphenate.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.wrapper.helper.EMErrorHelper;
import com.hyphenate.wrapper.helper.EMGroupAckHelper;
import com.hyphenate.wrapper.helper.EMLanguageHelper;
import com.hyphenate.wrapper.helper.EMMessageHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionChangeHelper;
import com.hyphenate.wrapper.helper.EMMessageReactionHelper;
import com.hyphenate.wrapper.helper.HyphenateExceptionHelper;
import com.hyphenate.wrapper.listeners.EMWrapperMessageListener;
import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;

public class EMChatManagerWrapper extends EMBaseWrapper {

    public EMWrapperMessageListener emWrapperMessageListener;

    EMChatManagerWrapper(){
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.sendMessage.equals(method)) {
            ret = sendMessage(jsonObject, callback);
        } else if (EMSDKMethod.resendMessage.equals(method)) {
            ret = resendMessage(jsonObject, callback);
        } else if (EMSDKMethod.ackMessageRead.equals(method)) {
            ret = ackMessageRead(jsonObject, callback);
        } else if (EMSDKMethod.ackGroupMessageRead.equals(method)) {
            ret = ackGroupMessageRead(jsonObject, callback);
        } else if (EMSDKMethod.ackConversationRead.equals(method)) {
            ret = ackConversationRead(jsonObject, callback);
        } else if (EMSDKMethod.recallMessage.equals(method)) {
            ret = recallMessage(jsonObject, callback);
        } else if (EMSDKMethod.getConversation.equals(method)) {
            ret = getConversation(jsonObject, callback);
        } else if (EMSDKMethod.getThreadConversation.equals(method)) {
            ret = getThreadConversation(jsonObject, callback);
        } else if (EMSDKMethod.markAllChatMsgAsRead.equals(method)) {
            ret = markAllChatMsgAsRead(jsonObject, callback);
        } else if (EMSDKMethod.getUnreadMessageCount.equals(method)) {
            ret = getUnreadMessageCount(jsonObject, callback);
        } else if (EMSDKMethod.updateChatMessage.equals(method)) {
            ret = updateChatMessage(jsonObject, callback);
        } else if (EMSDKMethod.downloadAttachment.equals(method)) {
            ret = downloadAttachment(jsonObject, callback);
        } else if (EMSDKMethod.downloadThumbnail.equals(method)) {
            ret = downloadThumbnail(jsonObject, callback);
        } else if (EMSDKMethod.importMessages.equals(method)) {
            ret = importMessages(jsonObject, callback);
        } else if (EMSDKMethod.loadAllConversations.equals(method)) {
            ret = loadAllConversations(jsonObject, callback);
        } else if (EMSDKMethod.getConversationsFromServer.equals(method)) {
            ret = getConversationsFromServer(jsonObject, callback);
        } else if (EMSDKMethod.deleteConversation.equals(method)) {
            ret = deleteConversation(jsonObject, callback);
        } else if (EMSDKMethod.fetchHistoryMessages.equals(method)) {
            ret = fetchHistoryMessages(jsonObject, callback);
        } else if (EMSDKMethod.searchChatMsgFromDB.equals(method)) {
            ret = searchChatMsgFromDB(jsonObject, callback);
        } else if (EMSDKMethod.getMessage.equals(method)) {
            ret = getMessage(jsonObject, callback);
        } else if (EMSDKMethod.asyncFetchGroupAcks.equals(method)){
            ret = asyncFetchGroupMessageAckFromServer(jsonObject, callback);
        } else if (EMSDKMethod.deleteRemoteConversation.equals(method)){
            ret = deleteRemoteConversation(jsonObject, callback);
        } else if (EMSDKMethod.deleteMessagesBeforeTimestamp.equals(method)) {
            ret = deleteMessagesBefore(jsonObject, callback);
        } else if (EMSDKMethod.translateMessage.equals(method)) {
            ret = translateMessage(jsonObject, callback);
        } else if (EMSDKMethod.fetchSupportedLanguages.equals(method)) {
            ret = fetchSupportedLanguages(jsonObject, callback);
        } else if (EMSDKMethod.addReaction.equals(method)) {
            ret = addReaction(jsonObject, callback);
        } else if (EMSDKMethod.removeReaction.equals(method)) {
            ret = removeReaction(jsonObject, callback);
        } else if (EMSDKMethod.fetchReactionList.equals(method)) {
            ret = fetchReactionList(jsonObject, callback);
        } else if (EMSDKMethod.fetchReactionDetail.equals(method)) {
            ret = fetchReactionDetail(jsonObject, callback);
        } else if (EMSDKMethod.reportMessage.equals(method)) {
            ret = reportMessage(jsonObject, callback);
        }
        else {
            super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }


    private String sendMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        final EMMessage msg = EMMessageHelper.fromJson(params);
        msg.setMessageStatusCallback(new EMCallBack() {
            @Override
            public void onSuccess() {
                JSONObject jo = new JSONObject();
                try {
                    jo.put("ret", EMMessageHelper.toJson(msg));
                    callback.onSuccess(jo.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(int code, String error) {
                HyphenateException e = new HyphenateException(code, error);
                callback.onError(HyphenateExceptionHelper.toJson(e));
            }

            @Override
            public void onProgress(int progress, String status) {
                callback.onProgress(progress);
            }
        });
        asyncRunnable(() -> EMClient.getInstance().chatManager().sendMessage(msg));
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String resendMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMMessage tempMsg = EMMessageHelper.fromJson(params);
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(tempMsg.getMsgId());
        msg.setMessageStatusCallback(new EMCallBack() {
            @Override
            public void onSuccess() {
                JSONObject jo = new JSONObject();
                try {
                    jo.put("ret", EMMessageHelper.toJson(msg));
                    callback.onSuccess(jo.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(int code, String error) {
                HyphenateException e = new HyphenateException(code, error);
                callback.onError(HyphenateExceptionHelper.toJson(e));
            }

            @Override
            public void onProgress(int progress, String status) {
                callback.onProgress(progress);
            }
        });
        asyncRunnable(() -> EMClient.getInstance().chatManager().sendMessage(msg));
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String ackMessageRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        String to = params.getString("to");

        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatManager().ackMessageRead(to, msgId);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String ackGroupMessageRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        String to = params.getString("groupId");
        String content = null;
        if(params.has("content")) {
            content = params.getString("content");
        }
        String finalContent = content;
        asyncRunnable(()->{
            try {
                EMClient.getInstance().chatManager().ackGroupMessageRead(to, msgId, finalContent);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String ackConversationRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conversationId = params.getString("convId");
        asyncRunnable(() -> {
            try {
                EMClient.getInstance().chatManager().ackConversationRead(conversationId);
                onSuccess(null, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String recallMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");

        asyncRunnable(() -> {
            try {
                EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
                if (msg != null) {
                    EMClient.getInstance().chatManager().recallMessage(msg);
                    onSuccess(null, callback);
                }else {
                    onError(new HyphenateException(500, "The message was not found"), callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });

        return null;
    }

    private String getMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String getConversation(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conId = params.getString("convId");
        boolean createIfNeed = true;
        if (params.has("createIfNeed")) {
            createIfNeed = params.getBoolean("createIfNeed");
        }

        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("convType"));

        boolean finalCreateIfNeed = createIfNeed;
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conId, type, finalCreateIfNeed);
        return EMHelper.getReturnJsonObject(EMConversationHelper.toJson(conversation)).toString();
    }

    private String getThreadConversation(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conId = params.getString("convId");
        EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conId, EMConversation.EMConversationType.GroupChat, true, true);
        return EMHelper.getReturnJsonObject(EMConversationHelper.toJson(conversation)).toString();
    }

    private String  markAllChatMsgAsRead(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().chatManager().markAllConversationsAsRead();
        return EMHelper.getReturnJsonObject(true).toString();
    }

    private String getUnreadMessageCount(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int count = EMClient.getInstance().chatManager().getUnreadMessageCount();
        return EMHelper.getReturnJsonObject(count).toString();
    }

    private String updateChatMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMMessage msg = EMMessageHelper.fromJson(params.getJSONObject("message"));
        boolean ret = EMClient.getInstance().chatManager().updateMessage(msg);
        return EMHelper.getReturnJsonObject(ret).toString();
    }

    private String importMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        JSONArray ary = params.getJSONArray("list");
        List<EMMessage> messages = new ArrayList<>();
        for (int i = 0; i < ary.length(); i++) {
            JSONObject obj = ary.getJSONObject(i);
            messages.add(EMMessageHelper.fromJson(obj));
        }
        asyncRunnable(()->{
            EMClient.getInstance().chatManager().importMessages(messages);
            onSuccess(true, callback);
        });

        return null;
    }

    private String downloadAttachment(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        final EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
        msg.setMessageStatusCallback(new EMCallBack() {
            @Override
            public void onSuccess() {
                JSONObject jo = new JSONObject();
                try {
                    jo.put("ret", EMMessageHelper.toJson(msg));
                    callback.onSuccess(jo.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(int code, String error) {
                HyphenateException e = new HyphenateException(code, error);
                callback.onError(HyphenateExceptionHelper.toJson(e));
            }

            @Override
            public void onProgress(int progress, String status) {
                callback.onProgress(progress);
            }
        });
        EMClient.getInstance().chatManager().downloadAttachment(msg);
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String downloadThumbnail(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        final EMMessage msg = EMClient.getInstance().chatManager().getMessage(msgId);
        msg.setMessageStatusCallback(new EMCallBack() {
            @Override
            public void onSuccess() {
                JSONObject jo = new JSONObject();
                try {
                    jo.put("ret", EMMessageHelper.toJson(msg));
                    callback.onSuccess(jo.toString());
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onError(int code, String error) {
                HyphenateException e = new HyphenateException(code, error);
                callback.onError(HyphenateExceptionHelper.toJson(e));
            }

            @Override
            public void onProgress(int progress, String status) {
                callback.onProgress(progress);
            }
        });
        EMClient.getInstance().chatManager().downloadThumbnail(msg);
        return EMHelper.getReturnJsonObject(EMMessageHelper.toJson(msg)).toString();
    }

    private String loadAllConversations(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<EMConversation> list = new ArrayList<>(EMClient.getInstance().chatManager().getAllConversations().values());
        boolean retry = false;
        JSONArray conversations = new JSONArray();
        do{
            try{
                retry = false;
                Collections.sort(list, (o1, o2) -> {
                    if (o1 == null && o2 == null) {
                        return 0;
                    }
                    if (o1.getLastMessage() == null) {
                        return 1;
                    }

                    if (o2.getLastMessage() == null) {
                        return -1;
                    }

                    if (o1.getLastMessage().getMsgTime() == o2.getLastMessage().getMsgTime()) {
                        return 0;
                    }

                    return o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime() > 0 ? 1 : -1;
                });
                for (EMConversation conversation : list) {
                    conversations.put(EMConversationHelper.toJson(conversation));
                }
            }catch(IllegalArgumentException e) {
                retry = true;
            } catch (JSONException e) {
                e.printStackTrace();
            }
        }while (retry);

        return EMHelper.getReturnJsonObject(conversations).toString();
    }

    private String getConversationsFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(() -> {
            try {
                List<EMConversation> list = new ArrayList<>(
                        EMClient.getInstance().chatManager().fetchConversationsFromServer().values());
                Collections.sort(list, (o1, o2) -> {
                    if (o1.getLastMessage() == null) {
                        return 1;
                    }

                    if (o2.getLastMessage() == null) {
                        return -1;
                    }

                    if (o1.getLastMessage().getMsgTime() == o2.getLastMessage().getMsgTime()) {
                        return 0;
                    }

                    return o2.getLastMessage().getMsgTime() - o1.getLastMessage().getMsgTime() > 0 ? 1 : -1;
                });
                JSONArray conversations = new JSONArray();
                try {
                    for (EMConversation conversation : list) {
                        conversations.put(EMConversationHelper.toJson(conversation));
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(conversations, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String deleteConversation(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conId = params.getString("convId");
        boolean isDelete = params.getBoolean("deleteMessages");
        EMClient.getInstance().chatManager().deleteConversation(conId, isDelete);
        return null;
    }

    private String fetchHistoryMessages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conId = params.getString("convId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("type"));
        int pageSize = params.getInt("pageSize");
        String startMsgId = params.getString("startMsgId");
        asyncRunnable(() -> {
            try {
                EMCursorResult<EMMessage> cursorResult = EMClient.getInstance().chatManager().fetchHistoryMessages(conId,
                        type, pageSize, startMsgId);
                JSONObject jsonObject = null;
                try{
                    jsonObject = EMCursorResultHelper.toJson(cursorResult);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jsonObject, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String searchChatMsgFromDB(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String keywords = params.getString("keywords");
        long timeStamp = params.getLong("timeStamp");
        int count = params.getInt("maxCount");
        String from = params.getString("from");
        EMConversation.EMSearchDirection direction = searchDirectionFromString(params.getString("direction"));
        List<EMMessage> msgList = EMClient.getInstance().chatManager().searchMsgFromDB(keywords, timeStamp, count,
                from, direction);
        JSONArray messages = new JSONArray();
        try {
            for (EMMessage msg : msgList) {
                messages.put(EMMessageHelper.toJson(msg));
            }
        }catch (JSONException e) {
            e.printStackTrace();
        }finally {
            return EMHelper.getReturnJsonObject(messages).toString();
        }
    }


    private String asyncFetchGroupMessageAckFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        String ackId = null;
        if (params.has("ackId")){
            ackId = params.getString("ackId");
        }
        int pageSize = params.getInt("pageSize");

        EMCommonValueCallback<EMCursorResult<EMGroupReadAck>> callBack = new EMCommonValueCallback<EMCursorResult<EMGroupReadAck>>(callback) {
            @Override
            public void onSuccess(EMCursorResult<EMGroupReadAck> result) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMCursorResultHelper.toJson(result);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        };

        EMClient.getInstance().chatManager().asyncFetchGroupReadAcks(msgId, pageSize, ackId, callBack);
        return null;
    }


    private String deleteRemoteConversation(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conversationId = params.getString("conversationId");
        EMConversation.EMConversationType type = typeFromInt(params.getInt("conversationType"));
        boolean isDeleteRemoteMessage = params.getBoolean("isDeleteRemoteMessage");
        EMClient.getInstance().chatManager().deleteConversationFromServer(conversationId, type, isDeleteRemoteMessage, new EMCommonCallback(callback));
        return null;
    }

    private String deleteMessagesBefore(JSONObject params, EMWrapperCallback callback) throws JSONException {
        long timestamp = params.getLong("timestamp");
        EMClient.getInstance().chatManager().deleteMessagesBeforeTimestamp(timestamp, new EMCommonCallback(callback));
        return null;
    }

    private String translateMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMMessage msg = EMMessageHelper.fromJson(params.getJSONObject("message"));
        List<String> list = new ArrayList<String>();
        if (params.has("languages")){
            JSONArray array = params.getJSONArray("languages");
            for (int i = 0; i < array.length(); i++) {
                list.add(array.getString(i));
            }
        }
        EMClient.getInstance().chatManager().translateMessage(msg, list, new EMCommonValueCallback<EMMessage>(callback){
            @Override
            public void onSuccess(EMMessage object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMMessageHelper.toJson(object);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String fetchSupportedLanguages(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().chatManager().fetchSupportLanguages(new EMCommonValueCallback<List<EMLanguage>>(callback){
            @Override
            public void onSuccess(List<EMLanguage> object) {
                JSONArray list = new JSONArray();
                try {
                    for (EMLanguage language : object) {
                        list.put(EMLanguageHelper.toJson(language));
                    }
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(list);
                }
            }
        });
        return null;
    }

    private String addReaction(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String reaction = params.getString("reaction");
        String msgId = params.getString("msgId");
        EMClient.getInstance().chatManager().asyncAddReaction(msgId, reaction, new EMCommonCallback(callback));
        return null;
    }

    private String removeReaction(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String reaction = params.getString("reaction");
        String msgId = params.getString("msgId");
        EMClient.getInstance().chatManager().asyncRemoveReaction(msgId, reaction, new EMCommonCallback(callback));
        return null;
    }

    private String fetchReactionList(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<String> msgIds = new ArrayList<>();
        JSONArray ja = params.getJSONArray("msgIds");
        for (int i = 0; i < ja.length(); i++) {
            msgIds.add(ja.getString(i));
        }
        String groupId = null;
        if (params.has("groupId")) {
            groupId = params.getString("groupId");
        }
        EMMessage.ChatType type = EMMessage.ChatType.Chat;
        String sType = params.getString("type");
        if (sType.equals("chat")) {
            type = EMMessage.ChatType.Chat;
        } else {
            type = EMMessage.ChatType.GroupChat;
        }
        EMClient.getInstance().chatManager().asyncGetReactionList(msgIds, type, groupId, new EMCommonValueCallback<Map<String, List<EMMessageReaction>>>(callback){
            @Override
            public void onSuccess(Map<String, List<EMMessageReaction>> object) {
                JSONObject jo = new JSONObject();
                try {
                    if (object != null) {
                        for (Map.Entry<String, List<EMMessageReaction>> entry: object.entrySet()) {
                            List<EMMessageReaction> list = entry.getValue();
                            JSONArray jsonArray = new JSONArray();
                            for (int i = 0; i < list.size(); i++) {
                                jsonArray.put(EMMessageReactionHelper.toJson(list.get(i)));
                            }
                            jo.put(entry.getKey(), jsonArray);
                        }
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchReactionDetail(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        String reaction = params.getString("reaction");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        int pageSize = params.getInt("pageSize");
        EMClient.getInstance().chatManager().asyncGetReactionDetail(msgId, reaction, cursor, pageSize, new EMCommonValueCallback<EMCursorResult<EMMessageReaction>>(callback) {
            @Override
            public void onSuccess(EMCursorResult<EMMessageReaction> object) {
                JSONObject jsonObject = null;
                try {
                    jsonObject = EMCursorResultHelper.toJson(object);
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(jsonObject);
                }
            }
        });
        return null;
    }

    private String reportMessage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String msgId = params.getString("msgId");
        String tag = params.getString("tag");
        String reason = params.getString("reason");
        EMClient.getInstance().chatManager().asyncReportMessage(msgId, tag, reason, new EMCommonCallback(callback));
        return null;
    }
    
    private void registerEaseListener(){
        emWrapperMessageListener = new EMWrapperMessageListener();
        EMClient.getInstance().chatManager().addConversationListener(emWrapperMessageListener);
        EMClient.getInstance().chatManager().addMessageListener(emWrapperMessageListener);
    }

    private EMConversation.EMSearchDirection searchDirectionFromString(String direction) {
        return direction.equals("up") ? EMConversation.EMSearchDirection.UP : EMConversation.EMSearchDirection.DOWN;
    }

    private EMConversation.EMConversationType typeFromInt(int intType) {
        if (intType == 0){
            return EMConversation.EMConversationType.Chat;
        }else if(intType == 1){
            return EMConversation.EMConversationType.GroupChat;
        }else {
            return EMConversation.EMConversationType.ChatRoom;
        }
    }
}
