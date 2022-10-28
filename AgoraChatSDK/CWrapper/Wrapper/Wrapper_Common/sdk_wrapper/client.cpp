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

NativeListenerEvent gCallback = nullptr;
static bool NeedAllocResource = false;

namespace sdk_wrapper
{
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
            EMChatConfigsPtr configs = Options::FromJson(jstr, "./sdkdata", "./sdkdata");
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
        JSON_STARTOBJ
        writer.Key("getCurrentUsername");
        writer.String(CLIENT->getLoginInfo().loginUser().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {

        JSON_STARTOBJ
        writer.Key("isLoggedIn");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {

        JSON_STARTOBJ
        writer.Key("isConnected");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        JSON_STARTOBJ
        writer.Key("accessToken");
        writer.String(CLIENT->getLoginInfo().loginToken().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        memcpy(buf, json.c_str(), json.size());
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;        

        string local_jstr = jstr;
        string local_cbid = cbid;

        thread t([=]() {

            Document d; d.Parse(local_jstr.c_str());
            string user_name    = GetJsonValue_String(d, "username", "");
            string pwd_or_token = GetJsonValue_String(d, "pwdOrToken", "");
            bool is_token       = GetJsonValue_Bool(d, "isToken", false);

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

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), result->mErrorCode, result->mDescription.c_str());
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

            string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        });
        t.join();
    }
}


