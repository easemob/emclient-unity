
#include "emclient.h"
#include "emgroupmanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

static EMCallbackObserverHandle gCallbackObserverHandle;
EMGroupManagerListener* gGroupManagerListener = nullptr;

mutex progressGroupLocker;
map<string, int> progressGroupMap;

namespace sdk_wrapper {
    void AddGroupProgressItem(string id)
    {
        lock_guard<mutex> maplocker(progressGroupLocker);
        progressGroupMap[id] = 0;
    }

    void DeleteGroupProgressItem(string id)
    {
        lock_guard<mutex> maplocker(progressGroupLocker);
        auto it = progressGroupMap.find(id);
        if (progressGroupMap.end() != it) {
            progressGroupMap.erase(it);
        }
    }

    void UpdateGroupProgressMap(string id, int progress)
    {
        lock_guard<mutex> maplocker(progressGroupLocker);

        auto it = progressGroupMap.find(id);
        if (progressGroupMap.end() == it) {
            return;
        }
        it->second = progress;
    }

    int GetGroupLastProgress(string id)
    {
        lock_guard<mutex> maplocker(progressGroupLocker);
        auto it = progressGroupMap.find(id);
        if (progressGroupMap.end() == it) {
            return 0;
        }
        return it->second;
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AddListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gGroupManagerListener) {
            gGroupManagerListener = new GroupManagerListener();
            CLIENT->getGroupManager().addListener(gGroupManagerListener);
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_ApplyJoinPublicGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string reason = GetJsonValue_String(d, "reason", "");
        string nick_name = "";

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().applyJoinPublicGroup(group_id, nick_name, reason, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {                
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AcceptInvitationFromGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string inviter = "";

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().acceptInvitationFromGroup(group_id, inviter, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(result);
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AcceptJoinGroupApplication(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string username = GetJsonValue_String(d, "username", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().acceptJoinGroupApplication(group_id, username, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {                
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AddAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string member_id = GetJsonValue_String(d, "memberId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().addGroupAdmin(group_id, member_id, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AddMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string members_json = GetJsonValue_String(d, "members", "");

        EMMucMemberList memberList = JsonStringToVector(members_json);

        thread t([=]() {

            EMError error;
            CLIENT->getGroupManager().addGroupMembers(group_id, memberList, "", error); //TODO: lack of welcome message param. in signature

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_AddWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string members_json = GetJsonValue_String(d, "members", "");

        EMMucMemberList memberList = JsonStringToVector(members_json);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().addWhiteListMembers(group_id, memberList, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_BlockGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().blockGroupMessage(group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_BlockGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string members_json = GetJsonValue_String(d, "members", "");

        EMMucMemberList memberList = JsonStringToVector(members_json);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().blockGroupMembers(group_id, memberList, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }
    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_ChangeGroupDescription(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string desc = GetJsonValue_String(d, "desc", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().changeGroupDescription(group_id, desc, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_ChangeGroupName(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string name = GetJsonValue_String(d, "name", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().changeGroupSubject(group_id, name, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_TransferGroupOwner(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string owner = GetJsonValue_String(d, "owner", "");

        std::thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().transferGroupOwner(group_id, owner, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string call_back_jstr = JsonStringFromSuccess(local_cbid.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_FetchIsMemberInWhiteList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        std::thread t([=]() {
            EMError error;
            bool result = CLIENT->getGroupManager().fetchIsMemberInWhiteList(group_id, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                JSON_STARTOBJ
                writer.Key("ret");
                writer.Bool(result);
                JSON_ENDOBJ

                string json = s.GetString();
                string call_back_jstr = JsonStringFromSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = JsonStringFromError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }
}

