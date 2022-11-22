#include "emclient.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

EMPresenceManagerListener* gPresenceManagerListener = nullptr;

namespace sdk_wrapper {
	SDK_WRAPPER_API void SDK_WRAPPER_CALL PresenceManager_AddListener()
	{
		if (!CheckClientInitOrNot(nullptr)) return;

		if (nullptr == gPresenceManagerListener) { //only set once!
			gPresenceManagerListener = new PresenceManagerListener();
			CLIENT->getPresenceManager().addListener(gPresenceManagerListener);
		}
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_PublishPresence(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string desc = GetJsonValue_String(d, "desc", "");

		thread t([=]() {
			EMErrorPtr error = CLIENT->getPresenceManager().publishPresence(1, desc);

			if (EMError::EM_NO_ERROR == error->mErrorCode) {

				string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
		});
		t.detach();

        return nullptr;
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_SubscribePresences(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);		
		int64_t expiry = GetJsonValue_Int64(d, "expiry", 0);
		EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

		thread t([=]() {
			std::vector<EMPresencePtr> outputPresencesVec;
			EMErrorPtr error = CLIENT->getPresenceManager().subscribePresences(mem_list, outputPresencesVec, expiry);

			if (EMError::EM_NO_ERROR == error->mErrorCode) {

				string json = Presence::ToJson(outputPresencesVec);
				string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
		});
		t.detach();

        return nullptr;
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_UnsubscribePresences(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

		thread t([=]() {
			EMErrorPtr error = CLIENT->getPresenceManager().unsubscribePresences(mem_list);

			if (EMError::EM_NO_ERROR == error->mErrorCode) {

				string call_back_jstr = MyJson::ToJsonWithSuccess(local_cbid.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
		});
		t.detach();

        return nullptr;
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_FetchSubscribedMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		int page_num = GetJsonValue_Int(d, "pageNum", 1);
		int page_size = GetJsonValue_Int(d, "pageSize", 20);

		thread t([=]() {

			std::vector<std::string> members;

			EMErrorPtr error = CLIENT->getPresenceManager().fetchSubscribedMembers(members, page_num, page_size);

			if (EMError::EM_NO_ERROR == error->mErrorCode) {

				string json = MyJson::ToJson(members);
				string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());

			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
		});
		t.detach();

        return nullptr;
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL PresenceManager_FetchPresenceStatus(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["userIds"]);

		thread t([=]() {
			std::vector<EMPresencePtr> presences;
			EMErrorPtr error = CLIENT->getPresenceManager().fetchPresenceStatus(mem_list, presences);

			if (EMError::EM_NO_ERROR == error->mErrorCode) {

				string json = Presence::ToJson(presences);
				string call_back_jstr = MyJson::ToJsonWithSuccessResult(local_cbid.c_str(), json.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
			else {
				string call_back_jstr = MyJson::ToJsonWithError(local_cbid.c_str(), error->mErrorCode, error->mDescription.c_str());
				CallBack(local_cbid.c_str(), call_back_jstr.c_str());
			}
		});
		t.detach();

        return nullptr;
	}
}
