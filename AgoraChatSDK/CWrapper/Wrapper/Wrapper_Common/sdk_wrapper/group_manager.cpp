
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

    SDK_WRAPPER_API void SDK_WRAPPER_CALL GroupManager_RemoveListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        CLIENT->getGroupManager().clearListeners();
        if (nullptr != gGroupManagerListener) {
            delete gGroupManagerListener;
            gGroupManagerListener = nullptr;
        }
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ApplyJoinPublicGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string reason = GetJsonValue_String(d, "msg", "");
        string nick_name = "";

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().applyJoinPublicGroup(group_id, nick_name, reason, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AcceptInvitationFromGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string inviter = "";

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().acceptInvitationFromGroup(group_id, inviter, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(result);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AcceptJoinGroupApplication(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string username = GetJsonValue_String(d, "userId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().acceptJoinGroupApplication(group_id, username, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string member_id = GetJsonValue_String(d, "userId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().addGroupAdmin(group_id, member_id, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList memberList = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {

            EMError error;
            CLIENT->getGroupManager().addGroupMembers(group_id, memberList, "", error); //lack of welcome message param. in signature

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_AddWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList memberList = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().addWhiteListMembers(group_id, memberList, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_BlockGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().blockGroupMessage(group_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_BlockGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList memberList = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().blockGroupMembers(group_id, memberList, error);

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
    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupDescription(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string desc = GetJsonValue_String(d, "desc", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().changeGroupDescription(group_id, desc, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupName(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string name = GetJsonValue_String(d, "name", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().changeGroupSubject(group_id, name, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_TransferGroupOwner(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string owner = GetJsonValue_String(d, "userId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().transferGroupOwner(group_id, owner, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchIsMemberInWhiteList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            bool result = CLIENT->getGroupManager().fetchIsMemberInWhiteList(group_id, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                JSON_STARTOBJ
                writer.Key("ret");
                writer.Bool(result);
                JSON_ENDOBJ

                string json = s.GetString();
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_CreateGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_name = GetJsonValue_String(d, "name", "");
        string desc = GetJsonValue_String(d, "desc", "");
        string invite_reason = GetJsonValue_String(d, "msg", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);
        EMMucSetting setting = *(Group::JsonObjectToMucSetting(d["options"]).get());

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().createGroup(group_name, desc, invite_reason, setting, mem_list, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(result);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeclineInvitationFromGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string reason = GetJsonValue_String(d, "msg", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().declineInvitationFromGroup(group_id, "", reason, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeclineJoinGroupApplication(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string user_name = GetJsonValue_String(d, "userId", "");
        string reason = GetJsonValue_String(d, "msg", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().declineJoinGroupApplication(group_id, user_name, reason, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DestoryGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().destroyGroup(group_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DownloadGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string file_id = GetJsonValue_String(d, "fileId", "");
        string save_path = GetJsonValue_String(d, "savePath", "");

        AddGroupProgressItem(local_cbid);

        thread t([=]() {
            EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                [=]()->bool {
                    return true;
                },
                [=](const easemob::EMErrorPtr error)->bool {
                    return true;
                },
                [=](int progress) {
                    int last_progress = GetGroupLastProgress(local_cbid);
                    if (progress - last_progress >= 5) {
                        string call_back_jstr = MyJson::ToJsonWithProcess(local_cbid.c_str(), progress);
                        CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                        UpdateGroupProgressMap(local_cbid, progress);
                    }
                    return;
                }));

            EMError error;
            CLIENT->getGroupManager().downloadGroupSharedFile(group_id, save_path, file_id, callbackPtr, error);

            DeleteGroupProgressItem(local_cbid);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            string ret = CLIENT->getGroupManager().fetchGroupAnnouncement(group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                JSON_STARTOBJ
                writer.Key("ret");
                writer.String(ret.c_str());
                JSON_ENDOBJ
                string json = s.GetString();

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupBans(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMMucMemberList banList = CLIENT->getGroupManager().fetchGroupBans(group_id, page_num, page_size, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = MyJson::ToJson(banList);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupSharedFiles(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMMucSharedFileList fileList = CLIENT->getGroupManager().fetchGroupSharedFiles(group_id, page_num, page_size, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = GroupSharedFile::ToJson(fileList);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<string> msgCursorResult = CLIENT->getGroupManager().fetchGroupMembers(group_id, cursor, page_size, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string next_cursor = msgCursorResult.nextPageCursor();
                string json = CursorResult::ToJson(next_cursor, msgCursorResult.result());
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupMutes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMMucMuteList muteList = CLIENT->getGroupManager().fetchGroupMutes(group_id, page_num, page_size, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(muteList);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupSpecification(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(result);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchGroupWhiteList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMMucMemberList memberList = CLIENT->getGroupManager().fetchGroupWhiteList(group_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = MyJson::ToJson(memberList);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_GetGroupWithId(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        EMError error;
        EMGroupPtr result = CLIENT->getGroupManager().fetchGroupSpecification(group_id, error);

        string json = "";

        if (EMError::EM_NO_ERROR == error.mErrorCode) {

            if (nullptr != result) {
                JSON_STARTOBJ
                writer.Key("ret");
                Group::ToJsonObject(writer, result);
                JSON_ENDOBJ
                json = s.GetString();
            }
        }

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_LoadAllMyGroupsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        EMGroupList groupList = CLIENT->getGroupManager().loadAllMyGroupsFromDB();

        JSON_STARTOBJ
        writer.Key("ret");
        Group::ToJsonObject(writer, groupList);
        JSON_ENDOBJ
        string json = s.GetString();

        return CopyToPointer(json);
    }

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchAllMyGroupsWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);
        bool need_affiliations = GetJsonValue_Bool(d, "needAffiliations", false);
        bool need_role = GetJsonValue_Bool(d, "needRole", false);

        thread t([=]() {
            EMError error;
            EMGroupList groupList = CLIENT->getGroupManager().fetchAllMyGroupsWithPage(page_num, page_size, need_affiliations, need_role, error);            
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(groupList);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchAllMyGroupsWithPageSimple(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMGroupList groupList = CLIENT->getGroupManager().fetchAllMyGroupsWithPage(page_num, page_size, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(groupList);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchPublicGroupsWithCursor(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;

            EMCursorResult cursorResult = CLIENT->getGroupManager().fetchPublicGroupsWithCursor(cursor, page_size, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string next_cursor = cursorResult.nextPageCursor();
                string json = CursorResult::ToJsonWithGroupInfo(next_cursor, cursorResult);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_JoinPublicGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().joinPublicGroup(group_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_LeaveGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().leaveGroup(group_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_MuteAllGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr group = CLIENT->getGroupManager().muteAllGroupMembers(group_id, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_MuteGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        int64_t expireTime = GetJsonValue_Int64(d, "expireTime", -1);
        EMMucMemberList mem_list;
        if (d.HasMember("userIds")) {
            mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);
        }

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().muteGroupMembers(group_id, mem_list, expireTime, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveGroupAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string admin = GetJsonValue_String(d, "userId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().removeGroupAdmin(group_id, admin, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_DeleteGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string file_id = GetJsonValue_String(d, "fileId", "");

        std::thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().deleteGroupSharedFile(group_id, file_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().removeGroupMembers(group_id, mem_list, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RemoveWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().removeWhiteListMembers(group_id, mem_list, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnblockGroupMessage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().unblockGroupMessage(group_id, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnblockGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().unblockGroupMembers(group_id, mem_list, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnMuteAllMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr group = CLIENT->getGroupManager().unmuteAllGroupMembers(group_id, error);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UnmuteGroupMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().unmuteGroupMembers(group_id, mem_list, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UpdateGroupAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string announcement = GetJsonValue_String(d, "announcement", "");

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().updateGroupAnnouncement(group_id, announcement, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_ChangeGroupExtension(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string ext = GetJsonValue_String(d, "ext", "");

        thread t([=]() {
            EMError error;
            EMGroupPtr result = CLIENT->getGroupManager().changeGroupExtension(group_id, ext, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_UploadGroupSharedFile(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string file_path = GetJsonValue_String(d, "filePath", "");

        AddGroupProgressItem(local_cbid);

        thread t([=]() {
            EMCallbackPtr callbackPtr(new EMCallback(gCallbackObserverHandle,
                [=]()->bool {
                    return true;
                },
                [=](const easemob::EMErrorPtr error)->bool {
                    return false;
                },
                [=](int progress) {

                    int last_progress = GetGroupLastProgress(local_cbid);
                    if (progress - last_progress >= 5) {
                        string call_back_jstr = MyJson::ToJsonWithProcess(local_cbid.c_str(), progress);
                        CallBackProgress(local_cbid.c_str(), call_back_jstr.c_str());
                        UpdateGroupProgressMap(local_cbid, progress);
                    }
                    return;
                }));

            EMError error;
            CLIENT->getGroupManager().uploadGroupSharedFile(group_id, file_path, callbackPtr, error);

            DeleteGroupProgressItem(local_cbid);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_FetchMemberAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        vector<string> user_ids = MyJson::FromJsonObjectToVector(d["userIds"]);
        vector<string> attrs = MyJson::FromJsonObjectToVector(d["attrs"]);

        thread t([=]() {
            EMError error;
            unordered_map<string, unordered_map<string, string>> result = CLIENT->getGroupManager().fetchMemberAttributes(group_id, user_ids, attrs, error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Group::ToJson(result);
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
    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_SetMemberAttributes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string group_id = GetJsonValue_String(d, "groupId", "");
        string user_id = GetJsonValue_String(d, "userId", "");
        unordered_map<string, string> attrs = MyJson::FromJsonObjectToUnorderedMap(d["attrs"]);

        thread t([=]() {
            EMError error;
            CLIENT->getGroupManager().setMemberAttributes(group_id, user_id, attrs, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL GroupManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != gGroupManagerListener) {
            gGroupManagerListener->onReceiveInviteFromGroup("groupId", "groupName", "inviter", "inviter message");

            EMError error;
            EMCursorResult cursorResult = CLIENT->getGroupManager().fetchPublicGroupsWithCursor("", 10, error);

            if (cursorResult.result().size() == 0)
                return nullptr;

            EMGroupPtr groupPtr = dynamic_pointer_cast<EMGroup>(cursorResult.result().at(0));
            gGroupManagerListener->onReceiveInviteAcceptionFromGroup(groupPtr, "invitee");
            gGroupManagerListener->onReceiveInviteDeclineFromGroup(groupPtr, "invitee", "reason");
            gGroupManagerListener->onAutoAcceptInvitationFromGroup(groupPtr, "inviter", "inviteMessage");
            gGroupManagerListener->onLeaveGroup(groupPtr, EMMuc::EMMucLeaveReason::BE_KICKED);
            gGroupManagerListener->onLeaveGroup(groupPtr, EMMuc::EMMucLeaveReason::DESTROYED);
            gGroupManagerListener->onReceiveJoinGroupApplication(groupPtr, "from", "message");
            gGroupManagerListener->onReceiveAcceptionFromGroup(groupPtr);
            gGroupManagerListener->onReceiveRejectionFromGroup("123456", "reason");
            //gGroupManagerListener->onUpdateMyGroupList()

            vector<string> members;
            members.push_back("user1");
            members.push_back("user2");
            gGroupManagerListener->onAddMutesFromGroup(groupPtr, members, 12345678);

            gGroupManagerListener->onRemoveMutesFromGroup(groupPtr, members);
            gGroupManagerListener->onAddWhiteListMembersFromGroup(groupPtr, members);
            gGroupManagerListener->onRemoveWhiteListMembersFromGroup(groupPtr, members);
            gGroupManagerListener->onAllMemberMuteChangedFromGroup(groupPtr, true);
            gGroupManagerListener->onAddAdminFromGroup(groupPtr, "admin");
            gGroupManagerListener->onRemoveAdminFromGroup(groupPtr, "admin");
            gGroupManagerListener->onAssignOwnerFromGroup(groupPtr, "newOwner", "oldOwner");
            gGroupManagerListener->onMemberJoinedGroup(groupPtr, "join_member");
            gGroupManagerListener->onMemberLeftGroup(groupPtr, "left_member");
            gGroupManagerListener->onUpdateAnnouncementFromGroup(groupPtr, "updated announcement");

            EMMucSharedFilePtr shared_file(new EMMucSharedFile("fileId", "fileName", "fileOwner", 12345, 67890));
            gGroupManagerListener->onUploadSharedFileFromGroup(groupPtr, shared_file);
            gGroupManagerListener->onDeleteSharedFileFromGroup(groupPtr, "fileId");
            gGroupManagerListener->onDisabledStateChangedFromGroup(groupPtr, true);
            gGroupManagerListener->onUpdateSpecificationFromGroup(groupPtr);
        }
        return nullptr;
    }
}
