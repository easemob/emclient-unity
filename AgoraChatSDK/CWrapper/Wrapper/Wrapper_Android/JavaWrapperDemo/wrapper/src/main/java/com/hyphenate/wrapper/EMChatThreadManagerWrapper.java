package com.hyphenate.wrapper;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.chat.EMChatThread;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMMessage;

import com.hyphenate.wrapper.callback.EMCommonCallback;
import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMChatThreadEventHelper;
import com.hyphenate.wrapper.helper.EMChatThreadHelper;
import com.hyphenate.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.wrapper.helper.EMMessageHelper;
import com.hyphenate.wrapper.listeners.EMWrapperThreadListener;
import com.hyphenate.wrapper.util.EMSDKMethod;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMChatThreadManagerWrapper extends EMBaseWrapper{

    public EMWrapperThreadListener emWrapperThreadListener;

    EMChatThreadManagerWrapper(){
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.fetchChatThreadDetail.equals(method)) {
            ret = fetchChatThreadDetail(jsonObject, callback);
        } else if (EMSDKMethod.fetchJoinedChatThreads.equals(method)) {
            ret = fetchJoinedChatThreads(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatThreadsWithParentId.equals(method)) {
            ret = fetchChatThreadsWithParentId(jsonObject, callback);
        } else if (EMSDKMethod.fetchChatThreadMember.equals(method)) {
            ret = fetchChatThreadMember(jsonObject, callback);
        } else if (EMSDKMethod.fetchLastMessageWithChatThreads.equals(method)) {
            ret = fetchLastMessageWithChatThreads(jsonObject, callback);
        } else if (EMSDKMethod.removeMemberFromChatThread.equals(method)) {
            ret = removeMemberFromChatThread(jsonObject, callback);
        } else if (EMSDKMethod.updateChatThreadSubject.equals(method)) {
            ret = updateChatThreadSubject(jsonObject, callback);
        } else if (EMSDKMethod.createChatThread.equals(method)) {
            ret = createChatThread(jsonObject, callback);
        } else if (EMSDKMethod.joinChatThread.equals(method)) {
            ret = joinChatThread(jsonObject, callback);
        } else if (EMSDKMethod.leaveChatThread.equals(method)) {
            ret = leaveChatThread(jsonObject, callback);
        } else if (EMSDKMethod.destroyChatThread.equals(method)) {
            ret = destroyChatThread(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String fetchChatThreadDetail(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        EMClient.getInstance().chatThreadManager().getChatThreadFromServer(threadId, new EMCommonValueCallback<EMChatThread>(callback){
            @Override
            public void onSuccess(EMChatThread object) {
                JSONObject jo = null;
                try {
                    jo = EMChatThreadHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchJoinedChatThreads(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = params.getInt("pageSize");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }

        EMClient.getInstance().chatThreadManager().getJoinedChatThreadsFromServer(pageSize, cursor, new EMCommonValueCallback<EMCursorResult<EMChatThread>>(callback){
            @Override
            public void onSuccess(EMCursorResult<EMChatThread> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchChatThreadsWithParentId(JSONObject params, EMWrapperCallback callback) throws JSONException {

        boolean hasJoined = false;
        if (params.has("joined")) {
            hasJoined = params.getBoolean("joined");
        }

        if (hasJoined) {
            return fetchJoinedChatThreadsWithParentId(params, callback);
        }

        int pageSize = params.getInt("pageSize");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        String parentId = params.getString("groupId");
        EMClient.getInstance().chatThreadManager().getChatThreadsFromServer(parentId, pageSize, cursor, new EMCommonValueCallback<EMCursorResult<EMChatThread>>(callback){
            @Override
            public void onSuccess(EMCursorResult<EMChatThread> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchJoinedChatThreadsWithParentId(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = params.getInt("pageSize");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        String parentId = params.getString("groupId");

        EMClient.getInstance().chatThreadManager().getJoinedChatThreadsFromServer(parentId, pageSize, cursor, new EMCommonValueCallback<EMCursorResult<EMChatThread>>(callback) {
            @Override
            public void onSuccess(EMCursorResult<EMChatThread> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchChatThreadMember(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int pageSize = params.getInt("pageSize");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        String threadId = params.getString("threadId");
        EMClient.getInstance().chatThreadManager().getChatThreadMembers(threadId, pageSize, cursor, new EMCommonValueCallback<EMCursorResult<String>>(callback) {
            @Override
            public void onSuccess(EMCursorResult<String> object) {
                JSONObject jo = null;
                try {
                    jo = EMCursorResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchLastMessageWithChatThreads(JSONObject params, EMWrapperCallback callback) throws JSONException {
        List<String> threadIds = new ArrayList<>();
        JSONArray ja = params.getJSONArray("threadIds");
        for (int i = 0; i < ja.length(); i++) {
            String threadId = ja.getString(i);
            threadIds.add(threadId);
        }
        EMClient.getInstance().chatThreadManager().getChatThreadLatestMessage(threadIds, new EMCommonValueCallback<Map<String, EMMessage>>(callback) {
            @Override
            public void onSuccess(Map<String, EMMessage> object) {
                JSONObject data = new JSONObject();
                try {
                    for (Map.Entry<String, EMMessage> entry: object.entrySet()) {
                        data.put(entry.getKey(), EMMessageHelper.toJson(entry.getValue()));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(data);
                }
            }
        });
        return null;
    }

    private String removeMemberFromChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        String memberId = params.getString("userId");
        EMClient.getInstance().chatThreadManager().removeMemberFromChatThread(threadId, memberId, new EMCommonCallback(callback));
        return null;
    }

    private String updateChatThreadSubject(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        String name = params.getString("name");
        EMClient.getInstance().chatThreadManager().updateChatThreadName(threadId, name, new EMCommonCallback(callback));
        return null;
    }

    private String createChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String messageId = params.getString("msgId");
        String name = params.getString("threadName");
        String parentId = params.getString("groupId");
        EMClient.getInstance().chatThreadManager().createChatThread(parentId, messageId, name, new EMCommonValueCallback<EMChatThread>(callback){
            @Override
            public void onSuccess(EMChatThread object) {
                JSONObject jo = null;
                try {
                    jo = EMChatThreadHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String joinChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        EMClient.getInstance().chatThreadManager().joinChatThread(threadId, new EMCommonValueCallback<EMChatThread>(callback){
            @Override
            public void onSuccess(EMChatThread object) {
                JSONObject jo = null;
                try {
                    jo = EMChatThreadHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String leaveChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        EMClient.getInstance().chatThreadManager().leaveChatThread(threadId, new EMCommonCallback(callback));
        return null;
    }

    private String destroyChatThread(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String threadId = params.getString("threadId");
        EMClient.getInstance().chatThreadManager().destroyChatThread(threadId, new EMCommonCallback(callback));
        return null;
    }
    
    private void registerEaseListener() {
        emWrapperThreadListener = new EMWrapperThreadListener();
        EMClient.getInstance().chatThreadManager().addChatThreadChangeListener(emWrapperThreadListener);
    }
}
