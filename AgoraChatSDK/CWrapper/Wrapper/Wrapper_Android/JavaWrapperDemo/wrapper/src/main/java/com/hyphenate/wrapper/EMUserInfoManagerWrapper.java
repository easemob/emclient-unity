package com.hyphenate.wrapper;

import com.hyphenate.EMError;
import com.hyphenate.chat.EMClient;
import com.hyphenate.chat.EMUserInfo;
import com.hyphenate.exceptions.HyphenateException;
import com.hyphenate.wrapper.util.EMSDKMethod;
import com.hyphenate.wrapper.callback.EMCommonValueCallback;
import com.hyphenate.wrapper.callback.EMWrapperCallback;
import com.hyphenate.wrapper.helper.EMUserInfoHelper;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.Map;

public class EMUserInfoManagerWrapper extends EMBaseWrapper{
    EMUserInfoManagerWrapper() {
        registerEaseListener();
    }

    public String onMethodCall(String method, JSONObject jsonObject, EMWrapperCallback callback) throws JSONException {
        String ret = null;
        if (EMSDKMethod.updateOwnUserInfo.equals(method)) {
            ret = updateOwnUserInfo(jsonObject, callback);
        } else if (EMSDKMethod.updateOwnUserInfoWithType.equals(method)) {
            ret = updateOwnUserInfoWithType(jsonObject, callback);
        }else if (EMSDKMethod.fetchUserInfoById.equals(method)) {
            ret = fetchUserInfoByUserId(jsonObject, callback);
        }else if (EMSDKMethod.fetchUserInfoByIdWithType.equals(method)) {
            ret = fetchUserInfoByIdWithType(jsonObject, callback);
        } else {
            ret = super.onMethodCall(method, jsonObject, callback);
        }
        return ret;
    }

    private String updateOwnUserInfo(JSONObject params, EMWrapperCallback callback) throws JSONException {
        String username = EMClient.getInstance().getCurrentUser();
        if (username == null) {
            HyphenateException e = new HyphenateException(EMError.USER_NOT_LOGIN,"User not login");
            onError(e, callback);
            return null;
        }

        EMUserInfo userInfo = EMUserInfoHelper.fromJson(params.getJSONObject("userInfo"));
        userInfo.setUserId(username);
        asyncRunnable(() -> EMClient.getInstance().userInfoManager().updateOwnInfo(userInfo, new EMCommonValueCallback<>(callback)));
        return null;
    }


    private String updateOwnUserInfoWithType(JSONObject params, EMWrapperCallback callback) throws JSONException {
        int userInfoTypeInt = params.getInt("userInfoType");
        String userInfoTypeValue = params.getString("userInfoValue");
        EMUserInfo.EMUserInfoType userInfoType = getUserInfoTypeFromInt(userInfoTypeInt);

        asyncRunnable(()-> EMClient.getInstance().userInfoManager().updateOwnInfoByAttribute(userInfoType,userInfoTypeValue,new EMCommonValueCallback<>(callback)));
        return null;
    }


    private String fetchUserInfoByUserId(JSONObject params, EMWrapperCallback callback) throws JSONException {
        JSONArray userIdArray = params.getJSONArray("userIds");
        String[] userIds = new String[userIdArray.length()];
        for (int i = 0; i < userIdArray.length(); i++) {
            userIds[i] = (String) userIdArray.get(i);
        }

        asyncRunnable(()->{
            EMCommonValueCallback<Map<String,EMUserInfo>> callBack = new EMCommonValueCallback<Map<String,EMUserInfo>>(callback) {
                @Override
                public void onSuccess(Map<String,EMUserInfo> object) {
                    JSONObject jo = null;
                    try {
                        jo = generateMapFromMap(object);
                    }catch (JSONException e) {
                        e.printStackTrace();
                    }finally {
                        updateObject(jo);
                    }
                }
            };

            EMClient.getInstance().userInfoManager().fetchUserInfoByUserId(userIds, callBack);
        });
        return null;
    }


    private String fetchUserInfoByIdWithType(JSONObject params, EMWrapperCallback callback) throws JSONException {
        JSONArray userIdArray = params.getJSONArray("userIds");
        JSONArray userInfoTypeArray = params.getJSONArray("userInfoTypes");

        String[] userIds = new String[userIdArray.length()];
        for (int i = 0; i < userIdArray.length(); i++) {
            userIds[i] = (String) userIdArray.get(i);
        }

        EMUserInfo.EMUserInfoType[] infoTypes = new EMUserInfo.EMUserInfoType[userInfoTypeArray.length()];
        for (int i = 0; i < userInfoTypeArray.length(); i++) {
            EMUserInfo.EMUserInfoType infoType = getUserInfoTypeFromInt((int) userInfoTypeArray.get(i));
            infoTypes[i] = infoType;
        }

        asyncRunnable(()->{
            EMCommonValueCallback<Map<String,EMUserInfo>> callBack = new EMCommonValueCallback<Map<String,EMUserInfo>>(callback) {
                @Override
                public void onSuccess(Map<String,EMUserInfo> object) {
                    JSONObject jo = null;
                    try {
                        jo = generateMapFromMap(object);
                    }catch (JSONException e) {
                        e.printStackTrace();
                    }finally {
                        updateObject(jo);
                    }
                }
            };

            EMClient.getInstance().userInfoManager().fetchUserInfoByAttribute(userIds, infoTypes, callBack);
        });

        return null;
    }


    //组装response map
    JSONObject generateMapFromMap(Map<String, EMUserInfo> aMap) throws JSONException{
        JSONObject jsonObject = new JSONObject();
        for(Map.Entry<String, EMUserInfo> entry : aMap.entrySet()){
            String mapKey = entry.getKey();
            EMUserInfo mapValue = entry.getValue();
            jsonObject.put(mapKey, EMUserInfoHelper.toJson(mapValue));
        }
        return jsonObject;
    }


    //获取用户属性类型
    private EMUserInfo.EMUserInfoType getUserInfoTypeFromInt(int value){
        EMUserInfo.EMUserInfoType infoType;

        switch (value){
            case 0:
            {
                infoType = EMUserInfo.EMUserInfoType.NICKNAME;
            }
            break;

            case 1:
            {
                infoType = EMUserInfo.EMUserInfoType.AVATAR_URL;
            }
            break;

            case 2:
            {
                infoType = EMUserInfo.EMUserInfoType.EMAIL;
            }
            break;

            case 3:
            {
                infoType = EMUserInfo.EMUserInfoType.PHONE;
            }
            break;

            case 4:
            {
                infoType = EMUserInfo.EMUserInfoType.GENDER;
            }
            break;

            case 5:
            {
                infoType = EMUserInfo.EMUserInfoType.SIGN;
            }
            break;

            case 6:
            {
                infoType = EMUserInfo.EMUserInfoType.BIRTH;
            }
            break;

            case 100:
            {
                infoType = EMUserInfo.EMUserInfoType.EXT;
            }
            break;

            default:
                throw new IllegalStateException("Unexpected value: " + value);
        }

        return infoType;
    }
    
    private void registerEaseListener (){}
}
