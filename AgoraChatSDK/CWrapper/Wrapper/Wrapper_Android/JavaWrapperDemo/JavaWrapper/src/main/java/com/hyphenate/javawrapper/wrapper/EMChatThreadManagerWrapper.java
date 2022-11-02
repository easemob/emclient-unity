package com.hyphenate.javawrapper.wrapper;

import com.hyphenate.EMChatThreadChangeListener;
import com.hyphenate.chat.EMChatThread;
import com.hyphenate.chat.EMChatThreadEvent;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMCursorResult;
import com.hyphenate.chat.EMMessage;
import com.hyphenate.javawrapper.JavaWrapper;
import com.hyphenate.javawrapper.util.EMSDKMethod;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.javawrapper.wrapper.callback.EMWrapperCallback;
import com.hyphenate.javawrapper.wrapper.helper.EMChatThreadEventHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMChatThreadHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMCursorResultHelper;
import com.hyphenate.javawrapper.wrapper.helper.EMMessageHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class EMChatThreadManagerWrapper extends EMBaseWrapper{
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
        } else if (EMSDKMethod.fetchJoinedChatThreadsWithParentId.equals(method)) {
            ret = fetchJoinedChatThreadsWithParentId(jsonObject, callback);
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
        int pageSize = params.getInt("pageSize");
        String cursor = null;
        if (params.has("cursor")) {
            cursor = params.getString("cursor");
        }
        String parentId = params.getString("parentId");
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
        String parentId = params.getString("parentId");

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
        String memberId = params.getString("memberId");
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
        String messageId = params.getString("messageId");
        String name = params.getString("name");
        String parentId = params.getString("parentId");
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
        EMChatThreadChangeListener chatThreadChangeListener = new EMChatThreadChangeListener() {
            @Override
            public void onChatThreadCreated(EMChatThreadEvent event) {
                try {
                    JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
                    post(() -> JavaWrapper.listener.onReceive("EMChatThreadChangeListener", EMSDKMethod.onChatThreadCreate, jsonObject.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onChatThreadUpdated(EMChatThreadEvent event) {
                try {
                    JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
                    post(() -> JavaWrapper.listener.onReceive("EMChatThreadChangeListener", EMSDKMethod.onChatThreadUpdate, jsonObject.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onChatThreadDestroyed(EMChatThreadEvent event) {
                try {
                    JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
                    post(() -> JavaWrapper.listener.onReceive("EMChatThreadChangeListener", EMSDKMethod.onChatThreadDestroy, jsonObject.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }

            @Override
            public void onChatThreadUserRemoved(EMChatThreadEvent event) {
                try {
                    JSONObject jsonObject = EMChatThreadEventHelper.toJson(event);
                    post(() -> JavaWrapper.listener.onReceive("EMChatThreadChangeListener", EMSDKMethod.onUserKickOutOfChatThread, jsonObject.toString()));
                }catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        };

        EMClient.getInstance().chatThreadManager().addChatThreadChangeListener(chatThreadChangeListener);
    }
}
