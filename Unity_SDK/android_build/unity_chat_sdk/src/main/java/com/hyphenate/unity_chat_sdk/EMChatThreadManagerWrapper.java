package com.hyphenate.unity_chat_sdk;

import com.hyphenate.chat.EMChatThread;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.unity_chat_sdk.helper.EMChatThreadHelper;
import com.hyphenate.unity_chat_sdk.helper.EMCursorResultHelper;
import com.hyphenate.unity_chat_sdk.helper.EMMessageHelper;
import com.hyphenate.unity_chat_sdk.listeners.EMChatThreadManagerListener;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityCallback;
import com.hyphenate.unity_chat_sdk.listeners.EMUnityValueCallback;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

public class EMChatThreadManagerWrapper {
    static public EMChatThreadManagerWrapper wrapper() {
        return new EMChatThreadManagerWrapper();
    }

    EMChatThreadManagerListener listener;

    public EMChatThreadManagerWrapper() {
        listener = new EMChatThreadManagerListener();
        EMClient.getInstance().chatThreadManager().addChatThreadChangeListener(listener);
    }

    private void createThread(String threadName, String msgId, String groupId, String callbackId) {
        EMClient.getInstance().chatThreadManager().createChatThread(groupId, msgId, threadName, new EMUnityValueCallback<EMChatThread>("ChatThread", callbackId) {
            @Override
            public void onSuccess(EMChatThread thread) {
                try {
                    sendJsonObjectToUnity(EMChatThreadHelper.toJson(thread).toString());
                }catch (JSONException ignored) {}
            }
        });
    }
    private void joinThread(String threadId, String callbackId) {
        EMClient.getInstance().chatThreadManager().joinChatThread(threadId, new EMUnityValueCallback<EMChatThread>("ChatThread", callbackId) {
            @Override
            public void onSuccess(EMChatThread thread) {
                try {
                    sendJsonObjectToUnity(EMChatThreadHelper.toJson(thread).toString());
                }catch (JSONException ignored) {}
            }
        });
    }
    private void leaveThread(String threadId, String callbackId) {
        EMClient.getInstance().chatThreadManager().leaveChatThread(threadId, new EMUnityCallback(callbackId));
    }
    private void destroyThread(String threadId, String callbackId) {
        EMClient.getInstance().chatThreadManager().destroyChatThread(threadId, new EMUnityCallback(callbackId));
    }
    private void removeThreadMember(String threadId, String username, String callbackId) {
        EMClient.getInstance().chatThreadManager().removeMemberFromChatThread(threadId, username, new EMUnityCallback(callbackId));
    }
    private void changeThreadName(String threadId, String newName, String callbackId) {
        EMClient.getInstance().chatThreadManager().updateChatThreadName(threadId, newName, new EMUnityCallback(callbackId));
    }
    private void fetchThreadMembers(String threadId, String cursor, int pageSize, String callbackId) {
        EMClient.getInstance().chatThreadManager().getChatThreadMembers(threadId, pageSize, cursor, new EMUnityValueCallback<EMCursorResult<String>>("CursorResult<String>", callbackId) {
            @Override
            public void onSuccess(EMCursorResult<String> stringEMCursorResult) {
                try {
                    sendJsonObjectToUnity(EMCursorResultHelper.toJson(stringEMCursorResult).toString());
                }catch (JSONException ignored) {}
            }
        });
    }
    private void fetchThreadListOfGroup(String groupId, boolean joined, String cursor, int pageSize, String callbackId) {
        if (joined) {
            EMClient.getInstance().chatThreadManager().getJoinedChatThreadsFromServer(groupId, pageSize, cursor, new EMUnityValueCallback<EMCursorResult<EMChatThread>>("CursorResult<ChatThread>", callbackId) {
                @Override
                public void onSuccess(EMCursorResult<EMChatThread> emChatThreadEMCursorResult) {
                    try {
                        sendJsonObjectToUnity(EMCursorResultHelper.toJson(emChatThreadEMCursorResult).toString());
                    }catch (JSONException ignored) {}
                }
            });
        }else {
            EMClient.getInstance().chatThreadManager().getChatThreadsFromServer(groupId, pageSize, cursor, new EMUnityValueCallback<EMCursorResult<EMChatThread>>("CursorResult<ChatThread>", callbackId) {
                @Override
                public void onSuccess(EMCursorResult<EMChatThread> emChatThreadEMCursorResult) {
                    try {
                        sendJsonObjectToUnity(EMCursorResultHelper.toJson(emChatThreadEMCursorResult).toString());
                    }catch (JSONException ignored) {}
                }
            });
        }
    }
    private void fetchMineJoinedThreadList(String cursor, int pageSize, String callbackId) {
        EMClient.getInstance().chatThreadManager().getJoinedChatThreadsFromServer(pageSize, cursor, new EMUnityValueCallback<EMCursorResult<EMChatThread>>("CursorResult<ChatThread>", callbackId) {
            @Override
            public void onSuccess(EMCursorResult<EMChatThread> emChatThreadEMCursorResult) {
                try {
                    sendJsonObjectToUnity(EMCursorResultHelper.toJson(emChatThreadEMCursorResult).toString());
                }catch (JSONException ignored) {}
            }
        });
    }
    private void getThreadDetail(String threadId, String callbackId) {
        EMClient.getInstance().chatThreadManager().getChatThreadFromServer(threadId, new EMUnityValueCallback<EMChatThread>("ChatThread", callbackId) {
            @Override
            public void onSuccess(EMChatThread thread) {
                try {
                    sendJsonObjectToUnity(EMChatThreadHelper.toJson(thread).toString());
                }catch (JSONException ignored) {}
            }
        });
    }
    private void getLastMessageAccordingThreads(String jsonStringThreadIds, String callbackId) throws JSONException{
        JSONArray jAry = new JSONArray(jsonStringThreadIds);
        ArrayList<String> list = new ArrayList<>();
        for (int i = 0; i < jAry.length(); i++) {
            String s = jAry.getString(i);
            list.add(s);
        }
        EMClient.getInstance().chatThreadManager().getChatThreadLatestMessage(list, new EMUnityValueCallback<Map<String, EMMessage>>("Dictionary<string, Message>", callbackId) {
            @Override
            public void onSuccess(Map<String, EMMessage> stringEMMessageMap) {
                Iterator it = stringEMMessageMap.keySet().iterator();
                JSONObject jsonObject = new JSONObject();
                while (it.hasNext()) {
                    String key = (String) it.next();
                    try {
                        jsonObject.put(key, EMMessageHelper.toJson(stringEMMessageMap.get(key)));
                    } catch (JSONException ignored) { }
                }
                sendJsonObjectToUnity(jsonObject.toString());
            }
        });
    }
}
