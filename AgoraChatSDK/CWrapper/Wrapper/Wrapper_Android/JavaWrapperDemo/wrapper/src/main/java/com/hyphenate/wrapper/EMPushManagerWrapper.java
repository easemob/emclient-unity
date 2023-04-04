package com.hyphenate.wrapper;

import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMConversation;
import com.hyphenate.chat.EMPushConfigs;
import com.hyphenate.chat.EMPushManager;
import com.hyphenate.chat.EMSilentModeParam;
import com.hyphenate.chat.EMSilentModeResult;
import com.hyphenate.exceptions.HyphenateException;

import com.hyphenate.wrapper.util.EMHelper;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.callback.EMCommonCallback;
import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMConversationHelper;
import com.hyphenate.wrapper.helper.EMPushConfigsHelper;
import com.hyphenate.wrapper.helper.EMSilentModeParamHelper;
import com.hyphenate.wrapper.helper.EMSilentModeResultHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class EMPushManagerWrapper extends EMBaseWrapper{
    EMPushManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.getImPushConfig.equals(method)) {
            ret = getImPushConfig(jsonObject, callback);
        }
        else if(EMSDKMethod.getImPushConfigFromServer.equals(method)){
            ret = getImPushConfigFromServer(jsonObject, callback);
        }
        else if(EMSDKMethod.updatePushNickname.equals(method)){
            ret = updatePushNickname(jsonObject, callback);
        }
        else if(EMSDKMethod.updateImPushStyle.equals(method)){
            ret = updateImPushStyle(jsonObject, callback);
        }
        else if(EMSDKMethod.updateHMSPushToken.equals(method)){
            ret = updateHMSPushToken(jsonObject, callback);
        }
        else if(EMSDKMethod.updateFCMPushToken.equals(method)){
            ret = updateFCMPushToken(jsonObject, callback);
        }
        else if (EMSDKMethod.reportPushAction.equals(method)) {
            ret = reportPushAction(jsonObject, callback);
        }
        else if (EMSDKMethod.setConversationSilentMode.equals(method)) {
            ret = setConversationSilentMode(jsonObject, callback);
        }
        else if (EMSDKMethod.removeConversationSilentMode.equals(method)) {
            ret = removeConversationSilentMode(jsonObject, callback);
        }
        else if (EMSDKMethod.fetchConversationSilentMode.equals(method)) {
            ret = fetchConversationSilentMode(jsonObject, callback);
        }
        else if (EMSDKMethod.setSilentModeForAll.equals(method)) {
            ret = setSilentModeForAll(jsonObject, callback);
        }
        else if (EMSDKMethod.fetchSilentModeForAll.equals(method)) {
            ret = fetchSilentModeForAll(jsonObject, callback);
        }
        else if (EMSDKMethod.fetchSilentModeForConversations.equals(method)) {
            ret = fetchSilentModeForConversations(jsonObject, callback);
        }
        else if (EMSDKMethod.setPreferredNotificationLanguage.equals(method)) {
            ret = setPreferredNotificationLanguage(jsonObject, callback);
        }
        else if (EMSDKMethod.fetchPreferredNotificationLanguage.equals(method)) {
            ret = fetchPreferredNotificationLanguage(jsonObject, callback);
        }
        else if (EMSDKMethod.getPushTemplate.equals(method)) {
            ret = getPushTemplate(jsonObject, callback);
        }
        else if (EMSDKMethod.setPushTemplate.equals(method)) {
            ret = setPushTemplate(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String getImPushConfig(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMPushConfigs configs = EMClient.getInstance().pushManager().getPushConfigs();
        return EMHelper.getReturnJsonObject(EMPushConfigsHelper.toJson(configs)).toString();
    }

    private String getImPushConfigFromServer(JSONObject params, EMWrapperCallback callback) throws JSONException {
        asyncRunnable(()->{
            try {
                EMPushConfigs configs = EMClient.getInstance().pushManager().getPushConfigsFromServer();
                JSONObject jo = null;
                try {
                    JSONObject jsonObject = EMPushConfigsHelper.toJson(configs);
                    jo = jsonObject;
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    onSuccess(jo, callback);
                }
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String updatePushNickname(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String nickname = params.getString("nickname");

        asyncRunnable(()->{
            try {
                EMClient.getInstance().pushManager().updatePushNickname(nickname);
                onSuccess(nickname, callback);
            } catch (HyphenateException e) {
                onError(e, callback);
            }
        });
        return null;
    }

    private String updateImPushStyle(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMPushManager.DisplayStyle style = params.getInt("pushStyle") == 0 ? EMPushManager.DisplayStyle.SimpleBanner : EMPushManager.DisplayStyle.MessageSummary;
        EMClient.getInstance().pushManager().asyncUpdatePushDisplayStyle(style, new EMCommonCallback(callback));
        return null;
    }

    private String updateHMSPushToken(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String token = params.getString("token");
        asyncRunnable(()->{
            EMClient.getInstance().sendHMSPushTokenToServer(token);
            onSuccess(token, callback);
        });
        return null;
    }

    private String updateFCMPushToken(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String token = params.getString("token");
        String fcmKey = EMClient.getInstance().getOptions().getPushConfig().getFcmSenderId();
        EMClient.getInstance().pushManager().bindDeviceToken(fcmKey, token, new EMCommonCallback(callback));
        return null;
    }

    private String reportPushAction(JSONObject params, EMWrapperCallback callback) throws JSONException {
        return null;
    }

    private String setConversationSilentMode(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conversationId = params.getString("convId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("conversationType"));
        EMSilentModeParam param = EMSilentModeParamHelper.fromJson(params.getJSONObject("param"));
        EMClient.getInstance().pushManager().setSilentModeForConversation(conversationId, type, param, new EMCommonValueCallback<EMSilentModeResult>(callback){
            @Override
            public void onSuccess(EMSilentModeResult object) {
                JSONObject jo = null;
                try {
                    jo = EMSilentModeResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }
    private String removeConversationSilentMode(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conversationId = params.getString("conversationId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("convType"));
        EMClient.getInstance().pushManager().clearRemindTypeForConversation(conversationId, type, new EMCommonCallback(callback));
        return null;
    }

    private String fetchConversationSilentMode(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String conversationId = params.getString("conversationId");
        EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt("convType"));
        EMClient.getInstance().pushManager().getSilentModeForConversation(conversationId, type, new EMCommonValueCallback<EMSilentModeResult>(callback){
            @Override
            public void onSuccess(EMSilentModeResult object) {
                JSONObject jo = null;
                try {
                    jo = EMSilentModeResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String setSilentModeForAll(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMSilentModeParam param =  EMSilentModeParamHelper.fromJson(params.getJSONObject("param"));
        EMClient.getInstance().pushManager().setSilentModeForAll(param ,new EMCommonValueCallback<EMSilentModeResult>(callback){
            @Override
            public void onSuccess(EMSilentModeResult object) {
                JSONObject jo = null;
                try {
                    jo = EMSilentModeResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }

    private String fetchSilentModeForAll(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().pushManager().getSilentModeForAll(new EMCommonValueCallback<EMSilentModeResult>(callback){
            @Override
            public void onSuccess(EMSilentModeResult object) {
                JSONObject jo = null;
                try {
                    jo = EMSilentModeResultHelper.toJson(object);
                } catch (JSONException e) {
                    e.printStackTrace();
                } finally {
                    updateObject(jo);
                }
            }
        });
        return null;
    }
    private String fetchSilentModeForConversations(JSONObject params, EMWrapperCallback callback) throws JSONException {
        Iterator iterator = params.keys();
        ArrayList<EMConversation> list = new ArrayList<>();
        while (iterator.hasNext()) {
            String conversationId = (String)iterator.next();
            EMConversation.EMConversationType type = EMConversationHelper.typeFromInt(params.getInt(conversationId));
            EMConversation conversation = EMClient.getInstance().chatManager().getConversation(conversationId, type, true);
            list.add(conversation);
        }

        EMClient.getInstance().pushManager().getSilentModeForConversations(list, new EMCommonValueCallback<Map<String, EMSilentModeResult>>(callback) {
            @Override
            public void onSuccess(Map<String, EMSilentModeResult> object) {
                JSONObject result = new JSONObject();
                try {
                    for (Map.Entry<String, EMSilentModeResult>entry: object.entrySet()) {
                        result.put(entry.getKey(), EMSilentModeResultHelper.toJson(entry.getValue()));
                    }
                }catch (JSONException e) {
                    e.printStackTrace();
                }finally {
                    updateObject(result);
                }
            }
        });
        return null;
    }

    private String setPreferredNotificationLanguage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String code = params.getString("code");
        EMClient.getInstance().pushManager().setPreferredNotificationLanguage(code, new EMCommonCallback(callback));
        return null;
    }

    private String fetchPreferredNotificationLanguage(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().pushManager().getPreferredNotificationLanguage(new EMCommonValueCallback<>(callback));
        return null;
    }

    private String setPushTemplate(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String pushTemplateName = params.getString("pushTemplateName");
        EMClient.getInstance().pushManager().setPushTemplate(pushTemplateName, new EMCommonCallback(callback));
        return null;
    }

    private String getPushTemplate(JSONObject params, EMWrapperCallback callback) throws JSONException {
        EMClient.getInstance().pushManager().getPushTemplate(new EMCommonValueCallback<>(callback));
        return null;
    }
    
    private void registerEaseListener(){}
}
