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

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_AcceptInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_AddContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");
		string reason = GetJsonValue_String(d, "reason", "");

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_AddToBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_DeclineInvitation(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_DeleteContact(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");
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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_GetContactsFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		EMError error;
		vector<std::string> vec = CLIENT->getContactManager().getContactsFromDB(error);

		string json = "";
		if (EMError::EM_NO_ERROR == error.mErrorCode) {
			json = MyJson::ToJson(vec);
		}
		memcpy(buf, json.c_str(), json.size());
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_GetContactsFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_GetBlackListFromServer(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_GetSelfIdsOnOtherPlatform(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_RemoveFromBlackList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string user_name = GetJsonValue_String(d, "username", "");

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
	}

	SDK_WRAPPER_API void SDK_WRAPPER_CALL ContactManager_GetBlockListFromDB(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return;

		EMError error;
		vector<string> list = CLIENT->getContactManager().getBlackListFromDB(error);

		string json = "";

		if (EMError::EM_NO_ERROR == error.mErrorCode) {

			string json = MyJson::ToJson(list);
		}
		memcpy(buf, json.c_str(), json.size());
	}
}

