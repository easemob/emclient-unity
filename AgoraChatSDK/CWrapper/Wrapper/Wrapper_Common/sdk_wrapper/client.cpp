#include <thread>

#include "emclient.h"
#include "emconfigmanager.h"
#include "utils/emlog.h"

#include "tool.h"
#include "models.h"
#include "callbacks.h"
#include "sdk_wrapper.h"
#include "core_dump.h"

int SDK_TYPE = -1;
EMClient* gClient = nullptr;
EMConnectionListener* gConnectionListener = nullptr;
EMMultiDevicesListener* gMultiDevicesListener = nullptr;
EMConnectionCallbackListener* gConnectionCallbackListener = nullptr;
EMChatConfigsPtr configs = nullptr;

NativeListenerEvent gCallback = nullptr;
static bool NeedAllocResource = false;

string MY_APPKEY = "appkey";

const int TOKEN_CHECK_INTERVAL = 180; // 180s

namespace sdk_wrapper
{
    SDK_WRAPPER_API void SDK_WRAPPER_CALL Init_SDKWrapper(int sdkType, int compileType, void* callback_handle)
    {
        SDK_TYPE = sdkType;
        gCallback = nullptr;
        gCallback = (NativeListenerEvent)callback_handle;
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Uninit_SDKWrapper()
    {
        SDK_TYPE = -1;
        gCallback = nullptr;
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_AddListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gConnectionListener) {
            gConnectionListener = new ConnectionListener();
            gClient->addConnectionListener(gConnectionListener);
        }

        if (nullptr == gMultiDevicesListener) {
            gMultiDevicesListener = new MultiDevicesListener();
            gClient->addMultiDevicesListener(gMultiDevicesListener);
        }

        if (nullptr == gConnectionCallbackListener) { //only set once
            gConnectionCallbackListener = new ConnectionCallbackListener();
            gClient->addConnectionCallbackListener(gConnectionCallbackListener);
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL Client_RemoveListener()
    {
        CLIENT->removeConnectionListener(gConnectionListener);
        delete gConnectionListener;
        gConnectionListener = nullptr;

        CLIENT->removeConnectionCallbackListener(gConnectionCallbackListener);
        delete gConnectionCallbackListener;
        gConnectionCallbackListener = nullptr;

        CLIENT->removeMultiDevicesListener(gMultiDevicesListener);
        delete gMultiDevicesListener;
        gMultiDevicesListener = nullptr;
    }

    bool ResetAppKey(string& oldAppKey, string& newAppKey)
    {
        if (newAppKey.compare(oldAppKey) == 0) return true;

        if (nullptr == gClient || newAppKey.size() == 0) return false;

        CLIENT->logout(); // In login status, logout can clear dns
        gClient->changeAppkey(newAppKey); // In logout status, changeAppKey can clear dns

        gClient->getConfigManager()->setConfig(MY_APPKEY, newAppKey);
        gClient->getConfigManager()->saveConfigs();

        return true;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_InitWithOptions(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        // 0: default and correct
        // 100: App key is invalid
        int ret = 0;

        string appkeyPreInConfig;
        string appkeyInMem;
        string appkeyNew;

        configs = Options::FromJson(jstr, "./sdkdata", "./sdkdata");

        if (nullptr == configs) ret = 100;

        if (nullptr != configs) appkeyNew = configs->getAppKey();

        // singleton client handle, sdk initialize
        if (nullptr == gClient && nullptr != configs) {

            gClient = EMClient::create(configs);

            Client_AddListener();
            ChatManager_AddListener();
            GroupManager_AddListener();
            RoomManager_AddListener();
            ContactManager_AddListener();
            PresenceManager_AddListener();
            ThreadManager_AddListener();

            gClient->getConfigManager()->getConfig(MY_APPKEY, appkeyPreInConfig);
            ResetAppKey(appkeyPreInConfig, appkeyNew);
        }

        if (nullptr != gClient) {

            if (NeedAllocResource) {
                gClient->allocResource();
                NeedAllocResource = false;
            }

            appkeyInMem = gClient->getChatConfigs()->getAppKey();
            ResetAppKey(appkeyInMem, appkeyNew);
        }

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Int(ret);
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CurrentUsername(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        JSON_STARTOBJ
        writer.Key("ret");
        writer.String(CLIENT->getLoginInfo().loginUser().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_isLoggedIn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_isConnected(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        JSON_STARTOBJ
        writer.Key("ret");
        writer.Bool(CLIENT->isLoggedIn());
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LoginToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        JSON_STARTOBJ
        writer.Key("ret");
        writer.String(CLIENT->getLoginInfo().loginToken().c_str());
        JSON_ENDOBJ

        string json = s.GetString();
        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CreateAccount(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());

        string user_name = GetJsonValue_String(d, "userId", "");
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

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_Login(const char* jstr, const char* cbid, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string user_name = GetJsonValue_String(d, "userId", "");
        string pwd_or_token = GetJsonValue_String(d, "pwdOrToken", "");
        bool is_token = GetJsonValue_Bool(d, "isToken", false);

        thread t([=]() {
            EMErrorPtr result;
            result = is_token ? CLIENT->loginWithToken(user_name, pwd_or_token) : CLIENT->login(user_name, pwd_or_token);

            if (EMError::isNoError(result)) {

                // SetAndStartTokenCheckTimer(xxx, xxx) will be called at onReceiveToken in callbacks.h

                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), result->mErrorCode, result->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_Logout(const char* jstr, const char* cbid, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        thread t([=]() {
            CLIENT->logout();

            TokenWrapper::GetInstance()->StopTokenCheckTimer();

            string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
            CallBack(local_cbid.c_str(), call_back_jstr.c_str());
        });
        t.join();
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LoginWithAgoraToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        // parse param json
        Document d; d.Parse(local_jstr.c_str());
        string user_name = GetJsonValue_String(d, "userId", "");
        string token = GetJsonValue_String(d, "token", "");

        thread t([=]() {

            EMError error;
            string response = "";

            EMErrorPtr error_ptr = CLIENT->loginWithToken(user_name, token);
            if (EMError::EM_NO_ERROR == error_ptr->mErrorCode) {

                // SetAndStartTokenCheckTimer(xxx, xxx) will be called at onReceiveToken in callbacks.h

                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error_ptr->mErrorCode, error_ptr->mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_RenewToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        // parse param json
        Document d; d.Parse(jstr);
        string token = GetJsonValue_String(d, "token", "");

        EMError error;
        int64_t expiredTS = -1;
        CLIENT->getTokenExpiredTs(token, expiredTS, error);

        if (EMError::EM_NO_ERROR != error.mErrorCode) {
            EMLog::getInstance().getErrorLogStream() << "Client_RenewToken getTokenExpiredTs failed, code: " << error.mErrorCode << "; desc: " << error.mDescription;
            return nullptr;
        }

        CLIENT->renewToken(token);

        TokenWrapper::GetInstance()->SetAndStartTokenCheckTimer(expiredTS, TOKEN_CHECK_INTERVAL);

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ChangeAppKey(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        return nullptr; // No need to Implement
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_UploadLog(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        return nullptr; // No need to Implement
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_CompressLogs(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        return nullptr; // No need to Implement
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevice(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string password = GetJsonValue_String(d, "password", "");
        string resource = GetJsonValue_String(d, "resource", "");

        thread t([=]() {
            EMError error;
            CLIENT->kickDevice(userId, password, resource, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDeviceWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string token = GetJsonValue_String(d, "token", "");
        string resource = GetJsonValue_String(d, "resource", "");

        thread t([=]() {
            EMError error;
            CLIENT->kickDeviceWithToken(userId, token, resource, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevices(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string password = GetJsonValue_String(d, "password", "");

        thread t([=]() {
            EMError error;
            CLIENT->kickAllDevices(userId, password, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_KickDevicesWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string token = GetJsonValue_String(d, "token", "");

        thread t([=]() {
            EMError error;
            CLIENT->kickAllDevicesWithToken(userId, token, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_GetLoggedInDevicesFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string password = GetJsonValue_String(d, "password", "");

        thread t([=]() {
            EMError error;

            vector<EMDeviceInfoPtr> vec = CLIENT->getLoggedInDevicesFromServer(userId, password, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = DeviceInfo::ToJson(vec);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_GetLoggedInDevicesFromServerWithToken(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_jstr = jstr;
        string local_cbid = cbid;

        Document d; d.Parse(local_jstr.c_str());
        string userId = GetJsonValue_String(d, "userId", "");
        string token = GetJsonValue_String(d, "token", "");

        thread t([=]() {
            EMError error;

            vector<EMDeviceInfoPtr> vec = CLIENT->getLoggedInDevicesFromServerWithToken(userId, token, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = DeviceInfo::ToJson(vec);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ClearResource(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(nullptr)) return nullptr;

        /*if (CLIENT->isLoggedIn()) {
            return nullptr;
        }*/

        CLIENT->clearResource();
        NeedAllocResource = true;

        // clear all listeners when replay, need remove?
        //Client_RemoveListener();
        //ChatManager_RemoveListener();
        //GroupManager_RemoveListener();
        //RoomManager_RemoveListener();
        //ContactManager_RemoveListener();

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogDebug(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != jstr && strlen(jstr) > 0) {
            EMLog::getInstance().getDebugLogStream() << "SDK_Debug: " << jstr;
        }
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogWarn(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != jstr && strlen(jstr) > 0) {
            EMLog::getInstance().getWarningLogStream() << "SDK_Warn: " << jstr;
        }
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_LogError(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != jstr && strlen(jstr) > 0) {
            EMLog::getInstance().getErrorLogStream() << "SDK_Error: " << jstr;
        }
        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_ConnectionDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != gConnectionListener) {
            gConnectionListener->onConnect("connected");

            EMErrorPtr error(new EMError(202));

            gConnectionListener->onDisconnect(error);
            error->setErrorCode(206);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(207);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(214);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(216);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(217);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(220);
            gConnectionListener->onDisconnect(error, "anotherDevice");
            error->setErrorCode(305);
            gConnectionListener->onDisconnect(error);
            error->setErrorCode(1);
            gConnectionListener->onDisconnect(error);

            error->setErrorCode(EMError::TOKEN_EXPIRED);
            gConnectionListener->onTokenNotification(error);

            error->setErrorCode(EMError::TOKEN_WILL_EXPIRE);
            gConnectionListener->onTokenNotification(error);

            error->setErrorCode(EMError::APP_ACTIVE_NUMBER_REACH_LIMITATION);
            gConnectionListener->onDisconnect(error);
            
        }

        if (nullptr != gMultiDevicesListener) {
            gMultiDevicesListener->onContactMultiDevicesEvent(EMMultiDevicesListener::MultiDevicesOperation::CONTACT_REMOVE, "target", "ext");

            vector<string> usernames;
            usernames.push_back("user1");
            usernames.push_back("user2");
            gMultiDevicesListener->onGroupMultiDevicesEvent(EMMultiDevicesListener::MultiDevicesOperation::CONTACT_REMOVE, "target", usernames);

            gMultiDevicesListener->onThreadMultiDevicesEvent(EMMultiDevicesListener::MultiDevicesOperation::THREAD_CREATE, "thread_target", usernames);

            gMultiDevicesListener->undisturbMultiDevicesEvent("data");

            gMultiDevicesListener->onRoamDeleteMultiDevicesEvent("convId", "deviceId", usernames, 123456);

            gMultiDevicesListener->onConversationMultiDevicesEvent(EMMultiDevicesListener::MultiDevicesOperation::CONVERSATION_MARK, "convId", EMConversation::EMConversationType::CHAT);
        }

        return nullptr;
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL Client_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        Client_ConnectionDelegateTester(nullptr);
        ChatManager_RunDelegateTester(nullptr);
        GroupManager_RunDelegateTester(nullptr);
        RoomManager_RunDelegateTester(nullptr);
        ContactManager_RunDelegateTester(nullptr);
        PresenceManager_RunDelegateTester(nullptr);
        ThreadManager_RunDelegateTester(nullptr);
        return nullptr;
    }

    TokenWrapper* TokenWrapper::instance_ = nullptr;

    TokenWrapper::TokenWrapper()
    {
        expireTS_ = -1;
        availablePeriod_ = -1;
        check_interval_ = 180;
    }

    TokenWrapper::~TokenWrapper()
    {
        if (expireTS_ > 0) StopTokenCheckTimer();
    }

    TokenWrapper* TokenWrapper::GetInstance()
    {
        if (nullptr != instance_) return instance_;

        instance_ = new TokenWrapper();
        return instance_;
    }

    void TokenWrapper::TimeFunc(int signo)
    {
        GetInstance()->TokenCheck();
    }

    int TokenWrapper::GetTokenCheckInterval()
    {
        if (check_interval_ > floor(availablePeriod_ / 2)) {
            check_interval_ = floor(availablePeriod_ / 3);
        }

        EMLog::getInstance().getDebugLogStream() << "GetTokenCheckInterval check_interval_: " << check_interval_;

        return check_interval_;
    }

    void TokenWrapper::AdjustLastCheckInterval(int64_t remain_ts)
    {
        // Reset timer for the last time trigger point,
        // in order to trigger with EMError::TOKEN_EXPIRED for the last time.
        // Since no need to trigger EMError::TOKEN_WILL_EXPIRE and EMError::TOKEN_EXPIRED in transient.
        if (abs(check_interval_ - remain_ts) <= 3 ||
            check_interval_ > remain_ts) {
            check_interval_ = (int)remain_ts + 2; // try to trigger with EMError::TOKEN_EXPIRED

            EMLog::getInstance().getDebugLogStream() << "AdjustLastCheckInterval check_interval_: " << check_interval_;

            StartTimer(check_interval_, TimeFunc);
        }
    }

    void TokenWrapper::onTokenNotification(int errCode, int64_t remain_ts)
    {
        if (EMError::TOKEN_EXPIRED == errCode) {
            EMErrorPtr error(new EMError(EMError::TOKEN_EXPIRED));
            if (nullptr != gConnectionListener) gConnectionListener->onTokenNotification(error);
            EMLog::getInstance().getDebugLogStream() << "onTokenNotification token expired.";
            return;
        }

        if (EMError::TOKEN_WILL_EXPIRE == errCode) {
            EMErrorPtr error(new EMError(EMError::TOKEN_WILL_EXPIRE));
            error->mDescription = "Token will expire after ";
            error->mDescription.append(to_string((int)remain_ts));
            error->mDescription.append(" seconds.");
            if (nullptr != gConnectionListener) gConnectionListener->onTokenNotification(error);
            EMLog::getInstance().getDebugLogStream() << "onTokenNotification will expire after " << remain_ts << " seconds";
            return;
        }
    }

    void TokenWrapper::SetAndStartTokenCheckTimer(int64_t expireTS, int interval)
    {
        string passwd = CLIENT->getLoginInfo().loginPassword();

        // For password login, no need to start timer
        if (!passwd.empty()) return;

        expireTS_ = expireTS / 1000; // milli-second to second

        time_t nowTS = time(NULL);
        availablePeriod_ = expireTS_ - nowTS;

        check_interval_ = interval;

        if (availablePeriod_ > 0) {
            EMLog::getInstance().getDebugLogStream() << "SetAndStartTokenCheckTimer will start timer, availablePeriod_ : " << availablePeriod_ << ", expireTS_: " << expireTS_ << "; nowTS: " << nowTS;
            StartTimer(GetTokenCheckInterval(), TimeFunc);
        }
        else {
            EMLog::getInstance().getErrorLogStream() << "SetAndStartTokenCheckTimer failed, availablePeriod_ <= 0,  expireTS_: " << expireTS_ << "; nowTS: " << nowTS;
        }
    }

    void TokenWrapper::StopTokenCheckTimer()
    {
        if (expireTS_ > 0) {
            StopTimer();
        }

        expireTS_ = -1;
        availablePeriod_ = -1;
        check_interval_ = TOKEN_CHECK_INTERVAL;
    }

    void TokenWrapper::TokenCheck()
    {
        time_t nowTS = time(NULL);

        int64_t remainTS = expireTS_ - nowTS;

        if (remainTS <= 0) {
            StopTokenCheckTimer();
            onTokenNotification(EMError::TOKEN_EXPIRED, -1);
            Client_Logout(nullptr, "", nullptr);
        }
        else {
            if (remainTS < ceil(availablePeriod_ / 2)) { // will expire
                onTokenNotification(EMError::TOKEN_WILL_EXPIRE, remainTS);
                AdjustLastCheckInterval(remainTS);
            }
        }
    }
}


