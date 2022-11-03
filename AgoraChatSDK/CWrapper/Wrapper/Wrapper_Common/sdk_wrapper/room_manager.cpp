#include "emclient.h"
#include "emchatroommanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

EMChatroomManagerListener* gRoomManagerListener = nullptr;

namespace sdk_wrapper {

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_AddListener()
    {
        if (!CheckClientInitOrNot(nullptr)) return;

        if (nullptr == gRoomManagerListener) {
            gRoomManagerListener = new RoomManagerListener();
            CLIENT->getChatroomManager().addListener(gRoomManagerListener);
        }
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_AddRoomAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string admin = GetJsonValue_String(d, "admin", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().addChatroomAdmin(room_id, admin, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_BlockChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().blockChatroomMembers(room_id, mem_list, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_TransferChatroomOwner(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string owner = GetJsonValue_String(d, "newOwner", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().transferChatroomOwner(room_id, owner, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_ChangeChatroomDescription(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string desc = GetJsonValue_String(d, "desc", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().changeChatroomDescription(room_id, desc, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_ChangeRoomSubject(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string subject = GetJsonValue_String(d, "subject", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().changeChatroomSubject(room_id, subject, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_CreateRoom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string subject = GetJsonValue_String(d, "subject", "");
        string desc = GetJsonValue_String(d, "desc", "");
        int max_count = GetJsonValue_Int(d, "maxUserCount", 300);
        string welcome_msg = GetJsonValue_String(d, "welcomeMsg", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        EMMucSetting setting(EMMucSetting::EMMucStyle::DEFAUT, max_count, false);

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().createChatroom(subject, desc, welcome_msg, setting, mem_list, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Room::ToJson(result);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_DestroyChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatroomManager().destroyChatroom(room_id, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomsWithPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMPageResult pageResult = CLIENT->getChatroomManager().fetchChatroomsWithPage(page_num, page_size, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = PageResult::ToJsonWithRoom(pageResult.result().size(), pageResult);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            std::string announcement = CLIENT->getChatroomManager().fetchChatroomAnnouncement(room_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                JSON_STARTOBJ
                writer.Key("ret");
                writer.String(announcement.c_str());
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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomBans(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMMucMemberList banList = CLIENT->getChatroomManager().fetchChatroomBans(room_id, page_num, page_size, error);

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

    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomSpecification(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().fetchChatroomSpecification(room_id, error, false);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Room::ToJson(result);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
         });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string cursor = GetJsonValue_String(d, "cursor", "");
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMCursorResultRaw<std::string> msgCursorResult = CLIENT->getChatroomManager().fetchChatroomMembers(room_id, cursor, page_size, error);
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

    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_FetchChatroomMutes(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        int page_num = GetJsonValue_Int(d, "pageNum", 1);
        int page_size = GetJsonValue_Int(d, "pageSize", 20);

        thread t([=]() {
            EMError error;
            EMMucMuteList muteList = CLIENT->getChatroomManager().fetchChatroomMutes(room_id, page_num, page_size, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_JoinChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().joinChatroom(room_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                
                string json = Room::ToJson(result);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_LeaveChatroom(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            CLIENT->getChatroomManager().leaveChatroom(room_id, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_MuteChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().muteChatroomMembers(room_id, mem_list, -1, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_RemoveChatroomAdmin(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string admin = GetJsonValue_String(d, "admin", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().removeChatroomAdmin(room_id, admin, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_RemoveRoomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr result = CLIENT->getChatroomManager().removeChatroomMembers(room_id, mem_list, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_UnblockChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().unblockChatroomMembers(room_id, mem_list, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_UnmuteChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().unmuteChatroomMembers(room_id, mem_list, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_UpdateChatroomAnnouncement(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        string announcement = GetJsonValue_String(d, "announcement", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().updateChatroomAnnouncement(room_id, announcement, error);

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

    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_MuteAllChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr room = CLIENT->getChatroomManager().muteAllChatroomMembers(room_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Room::ToJson(room);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_UnMuteAllChatroomMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");

        thread t([=]() {
            EMError error;
            EMChatroomPtr room = CLIENT->getChatroomManager().unmuteAllChatroomMembers(room_id, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Room::ToJson(room);
                string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
            else {
                string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
                CallBack(local_cbid.c_str(), call_back_jstr.c_str());
            }
        });
        t.detach();
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_AddWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().addWhiteListMembers(room_id, mem_list, error);

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
    }

    SDK_WRAPPER_API void SDK_WRAPPER_CALL RoomManager_RemoveWhiteListMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string room_id = GetJsonValue_String(d, "roomId", "");
        EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["members"]);

        thread t([=]() {
            EMError error;
            EMChatroomPtr chatRoomPtr = CLIENT->getChatroomManager().removeWhiteListMembers(room_id, mem_list, error);

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
    }
}

