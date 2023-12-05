#include "emclient.h"
#include "emcontactmanager_interface.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

EMContactListener* gContactListener = nullptr;

namespace sdk_wrapper {
	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_AddListener()
	{
		if (!CheckClientInitOrNot(nullptr)) return;

		if (nullptr == gContactListener) {
			gContactListener = new ContactManagerListener();
			CLIENT->getContactManager().registerContactListener(gContactListener);
		}
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_RemoveListener()
	{
		if (!CheckClientInitOrNot(nullptr)) return;

		if (nullptr != gContactListener)
		{
			CLIENT->getContactManager().removeContactListener(gContactListener);
			delete gContactListener;
			gContactListener = nullptr;
		}
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AcceptInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().acceptInvitation(user_name, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AddContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");
		string reason = GetJsonValue_String(d, "msg", "");

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().inviteContact(user_name, reason, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_AddToBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().addToBlackList(user_name, true, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error.mErrorCode, error.mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());;
			}
			});
		t.detach();

        return nullptr;
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_DeclineInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().declineInvitation(user_name, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_DeleteContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");
		bool keep_conversation = GetJsonValue_Bool(d, "keepConversation", false);

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().deleteContact(user_name, error, keep_conversation);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetContactsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		EMError error;
		vector<std::string> vec = CLIENT->getContactManager().getContactsFromDB(error);

		string json = "";
		if (EMError::EM_NO_ERROR == error.mErrorCode) {

			JSON_STARTOBJ
			writer.Key("ret");
			MyJson::ToJsonObject(writer, vec);
			JSON_ENDOBJ
			json = s.GetString();

		}
        return CopyToPointer(json);
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetContactsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		thread t([=]() {
			EMError error;
			vector<std::string> vec = CLIENT->getContactManager().getContactsFromServer(error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = MyJson::ToJson(vec);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetBlackListFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		thread t([=]() {
			EMError error;
			vector<std::string> vec = CLIENT->getContactManager().getBlackListFromServer(error);
			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = MyJson::ToJson(vec);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetSelfIdsOnOtherPlatform(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		thread t([=]() {
			EMError error;
			vector<std::string> vec = CLIENT->getContactManager().getSelfIdsOnOtherPlatform(error);
			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = MyJson::ToJson(vec);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_RemoveFromBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "userId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getContactManager().removeFromBlackList(user_name, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_GetBlockListFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		EMError error;
		vector<string> list = CLIENT->getContactManager().getBlackListFromDB(error);

		string json = "";

		if (EMError::EM_NO_ERROR == error.mErrorCode) {
			JSON_STARTOBJ
			writer.Key("ret");
			MyJson::ToJsonObject(writer, list);
			JSON_ENDOBJ
			json = s.GetString();
		}
        return CopyToPointer(json);
	}

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_SetContactRemark(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string userId = GetJsonValue_String(d, "userId", "");
        string remark = GetJsonValue_String(d, "remark", "");

        thread t([=]() {
            string u = userId;
            string r = remark;

            EMError error;
            CLIENT->getContactManager().setContactRemark(u, r, error);

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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchContactFromLocal(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        string userId = GetJsonValue_String(d, "userId", "");

        thread t([=]() {
            string u = userId;

            EMError error;
            EMContactPtr contact = CLIENT->getContactManager().fetchContactFromLocal(u, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Contact::ToJson(contact);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromLocal(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        thread t([=]() {
            EMError error;
            vector<EMContactPtr> contacts = CLIENT->getContactManager().fetchAllContactsFromLocal(error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Contact::ToJson(contacts);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        thread t([=]() {
            EMError error;
            vector<EMContactPtr> contacts = CLIENT->getContactManager().fetchAllContactsFromServer(error);
            if (EMError::EM_NO_ERROR == error.mErrorCode) {

                string json = Contact::ToJson(contacts);
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_FetchAllContactsFromServerByPage(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (!CheckClientInitOrNot(cbid)) return nullptr;

        string local_cbid = cbid;

        Document d; d.Parse(jstr);
        int limit = GetJsonValue_Int(d, "limit", 20);
        string cursor = GetJsonValue_String(d, "cursor", "");

        thread t([=]() {
            string c = cursor;

            EMError error;
            EMCursorResultRaw<EMContactPtr> cursorResult = CLIENT->getContactManager().fetchAllContactsFromServerByPage(limit, c, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {
                string json = CursorResult::ToJson<EMContactPtr, Contact>(cursorResult.nextPageCursor(), cursorResult.result());
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

    SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ContactManager_RunDelegateTester(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
    {
        if (nullptr != gContactListener) {
            gContactListener->onContactAdded("username");
            gContactListener->onContactDeleted("username");

            string reason = "reason";
            gContactListener->onContactInvited("username", reason);

            gContactListener->onContactAgreed("username");
            gContactListener->onContactRefused("username");
        }
        return nullptr;
    }
}

