#include <thread>

#include "emclient.h"
#include "emconfigmanager.h"

#include "tool.h"
#include "models.h"
#include "callbacks.h"
#include "sdk_wrapper.h"


EMClient* gClient = nullptr;
EMConnectionListener* gConnectionListener = nullptr;
EMMultiDevicesListener* gMultiDevicesListener = nullptr;
EMChatConfigsPtr configs = nullptr;

NativeListenerEvent gCallback = nullptr;
static bool NeedAllocResource = false;

namespace sdk_wrapper
{
    TokenWrapper token_wrapper;

    SDK_WRAPPER_API void SDK_WRAPPER_CALL AddListener_SDKWrapper(void* callback_handle)
    {
        gCallback = nullptr;
        gCallback = (NativeListenerEvent)callback_handle;
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL CleanListener_SDKWrapper()
    {
        gCallback = nullptr;
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gConnectionListener) {
            gConnectionListener = new ConnectionListener();
            gClient->addConnectionListener(gConnectionListener);
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddMultiDeviceListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gMultiDevicesListener) {
            gMultiDevicesListener = new MultiDevicesListener();
            gClient->addMultiDevicesListener(gMultiDevicesListener);
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_InitWithOptions(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        // singleton client handle
        if (nullptr == gClient) {
            configs = Options::FromJson(jstr, "./sdkdata", "./sdkdata");
            gClient = EMClient::create(configs);
        }
        else {
            //TODO: check NeedAllocResource
            if (NeedAllocResource) {
                gClient->allocResource();
                NeedAllocResource = false;
            }
        }

        //TODO: add other listener here
        Client_AddListener();
        ChatManager_AddListener();
        Client_AddMultiDeviceListener();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_CurrentUsername(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        JSON_STARTOBJ
        writer.Key("getCurrentUsername");
        writer.String(CLIENT->getLoginInfo().loginUser().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        JSON_STARTOBJ
        writer.Key("isLoggedIn");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        JSON_STARTOBJ
        writer.Key("isConnected");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        JSON_STARTOBJ
        writer.Key("accessToken");
        writer.String(CLIENT->getLoginInfo().loginToken().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_CreateAccount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());

        string user_name = GetJsonValue_String(d, "username", "");
        string passwd = GetJsonValue_String(d, "password", "");

        thread t([=]() {
            EMErrorPtr result = CLIENT->createAccount(user_name, passwd);

            if (EMError::isNoError(result)) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), result->mErrorCode, result->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }

        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string user_name = GetJsonValue_String(d, "username", "");
        string pwd_or_token = GetJsonValue_String(d, "pwdOrToken", "");
        bool is_token = GetJsonValue_Bool(d, "isToken", false);

        thread t([=]() {
            EMErrorPtr result;
            result = is_token ? CLIENT->loginWithToken(user_name, pwd_or_token) : CLIENT->login(user_name, pwd_or_token);

            if (EMError::isNoError(result)) {

                /* TODO: need to add this part for 
                if (isToken)
                    SetTokenInAutoLogin(usernameStr, pwdOrTokenStr, "");
                else
                    SetPasswdInAutoLogin(usernameStr, pwdOrTokenStr);
                SaveAutoLoginConfigToFile();
                */

                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), result->mErrorCode, result->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Logout(const char* jstr, const char* cbid, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        thread t([=]() {
            CLIENT->logout();

            //TODO: StopTimer();

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        });
        t.join();
    }

    int TOKEN_CHECK_INTERVAL = 180; // 180s
    void TokenCheck(int signo)
    {
        // Note: No need mutex for locking global_autologin_config。
        // Since if timer is running, means still in login status.
        // global_autologin_config noly can be changed from logout status
        // to login status.

        time_t nowTS = time(NULL);
        int64_t remainTS = token_wrapper.autologin_config_.expireTsInt - nowTS;

        if (remainTS <= 0) { // expired
            EMErrorPtr error(new EMError(EMError::TOKEN_EXPIRED));
            StopTimer();
            gConnectionListener->onTokenNotification(error);
            Client_Logout(nullptr, nullptr, nullptr);
        }
        else { // Not expired

            if (remainTS < ceil(token_wrapper.autologin_config_.availablePeriod / 2)) { // will expire
                EMErrorPtr error(new EMError(EMError::TOKEN_WILL_EXPIRE));
                error->mDescription = "Token will expire after ";
                error->mDescription.append(to_string((int)remainTS));
                error->mDescription.append(" seconds.");
                gConnectionListener->onTokenNotification(error);

                // reset timer trigger point and only trigger once
                // so no need to stop timer when expired!!
                if (abs(TOKEN_CHECK_INTERVAL - remainTS) <= 3 ||
                    TOKEN_CHECK_INTERVAL > remainTS) {
                    TOKEN_CHECK_INTERVAL = (int)remainTS + 2;
                    StartTimer(TOKEN_CHECK_INTERVAL, TokenCheck);
                }

            }
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginWithAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_jstr = jstr;
        string local_cbid = cbid;

        EMError error;
        string response = "";

        // parse param json
        Document d; d.Parse(local_jstr.c_str());
        string user_name = GetJsonValue_String(d, "username", "");
        string agora_token = GetJsonValue_String(d, "token", "");

        // get easemob token with agora_token
        CLIENT->getChatTokenbyAgoraToken(agora_token, response, error);
        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        }

        string easemob_token = "";
        string expire_ts = "";

        // parse easemob token from json
        if (!token_wrapper.GetTokenCofigFromJson(response, easemob_token, expire_ts)) {
            error.mErrorCode = EMError::GENERAL_ERROR;
            error.mDescription = "Cannot get token config from response.";
            string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            return;
        }

        // async login with easemob token
        thread t([=]() {
            EMErrorPtr error = CLIENT->loginWithToken(user_name, agora_token);
            if (EMError::EM_NO_ERROR == error->mErrorCode) {

                token_wrapper.SetTokenInAutoLogin(user_name, agora_token, expire_ts);
                //token_wrapper.SaveAutoLoginConfigToFile(configs->deviceUuid()); // not support auto login for platform.

                TOKEN_CHECK_INTERVAL = 180;
                StartTimer(token_wrapper.GetTokenCheckInterval(TOKEN_CHECK_INTERVAL, (int)token_wrapper.autologin_config_.availablePeriod), TokenCheck);

                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_RenewAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        // parse param json
        Document d; d.Parse(jstr);
        string user_name = GetJsonValue_String(d, "username", "");
        string agora_token = GetJsonValue_String(d, "token", "");

        EMError error;
        string response;

        // get easemob token with agora_token
        CLIENT->getChatTokenbyAgoraToken(agora_token, response, error);
        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            string call_back_jstr = MyJson::ToJsonWithError(cbid, error.mErrorCode, error.mDescription.c_str());
            CallBack(cbid, call_back_jstr.c_str());
        }

        string easemob_token = "";
        string expire_ts = "";

        // parse easemob token from json
        if (!token_wrapper.GetTokenCofigFromJson(response, easemob_token, expire_ts)) {
            error.mErrorCode = EMError::GENERAL_ERROR;
            error.mDescription = "Cannot get token config from response.";
            string call_back_jstr = MyJson::ToJsonWithError(cbid, error.mErrorCode, error.mDescription.c_str());
            CallBack(cbid, call_back_jstr.c_str());
            return;
        }

        // Check expireTS first, then renewToken
        if (!token_wrapper.SetTokenInAutoLogin(token_wrapper.autologin_config_.userName, easemob_token, expire_ts)) {
            return;
        }

        CLIENT->renewToken(easemob_token);
        //token_wrapper.SaveAutoLoginConfigToFile(configs->deviceUuid());

        TOKEN_CHECK_INTERVAL = 180;
        StartTimer(token_wrapper.GetTokenCheckInterval(TOKEN_CHECK_INTERVAL, (int)token_wrapper.autologin_config_.availablePeriod), TokenCheck);
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AutoLogin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        if (CLIENT->isLoggedIn()) return;

        string local_jstr = jstr;
        string local_cbid = cbid;

        token_wrapper.GetAutoLoginConfigFromFile(configs->deviceUuid());

        // validate token config
        if (token_wrapper.autologin_config_.userName.size() == 0 ||
            (token_wrapper.autologin_config_.token.size() == 0 && token_wrapper.autologin_config_.passwd.size() == 0)) {

            EMError error;
            error.mErrorCode = EMError::GENERAL_ERROR;
            error.mDescription = "AutoLogin load token config failed";

            string call_back_jstr = MyJson::ToJsonWithError(cbid, error.mErrorCode, error.mDescription.c_str());
            CallBack(cbid, call_back_jstr.c_str());
            return;
        }

        // default it is passwd login
        bool isToken = false;
        string code = token_wrapper.autologin_config_.passwd;

        if (token_wrapper.autologin_config_.token.size() > 0) {
            isToken = true;
            code = token_wrapper.autologin_config_.token;
        }

        // async login with easemob token
        thread t([=]() {

            EMErrorPtr error = CLIENT->autoLogin(token_wrapper.autologin_config_.userName, code, isToken);
            if (EMError::EM_NO_ERROR == error->mErrorCode) {

                if (token_wrapper.autologin_config_.availablePeriod > 0) {
                    TOKEN_CHECK_INTERVAL = 180;
                    StartTimer(token_wrapper.GetTokenCheckInterval(TOKEN_CHECK_INTERVAL,(int)token_wrapper.autologin_config_.availablePeriod), TokenCheck);
                }

                JSON_STARTOBJ
                writer.Key("username");
                writer.String(token_wrapper.autologin_config_.userName.c_str());
                JSON_ENDOBJ

                string json = s.GetString();
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_ClearResource(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (CLIENT->isLoggedIn()) {
            return;
        }

        CLIENT->clearResource();
        NeedAllocResource = true;

        //TODO
        // clear all listeners when replay
        //ChatManager_RemoveListener();
        //GroupManager_RemoveListener();
        //RoomManager_RemoveListener();
        //ContactManager_RemoveListener();

        CLIENT->removeConnectionListener(gConnectionListener);
        delete gConnectionListener;
        gConnectionListener = nullptr;

        //TODO
        //CLIENT->removeConnectionCallbackListener(gConnectionCallbackListener);
        //delete gConnectionCallbackListener;
        //gConnectionCallbackListener = nullptr;

        CLIENT->removeMultiDevicesListener(gMultiDevicesListener);
        delete gMultiDevicesListener;
        gMultiDevicesListener = nullptr;
    }
}


