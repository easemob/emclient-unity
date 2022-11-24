#include "emclient.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

extern EMClient* gClient;

EMThreadManagerListener* gThreadListener = nullptr;

namespace sdk_wrapper {
	SDK_WRAPPER_API void SDK_WRAPPER_CALL ThreadManager_AddListener()
	{
		if (!CheckClientInitOrNot(nullptr)) return;

		if (nullptr == gThreadListener) { //only set once!
			gThreadListener = new ThreadManagerListener();
			CLIENT->getThreadManager().addListener(gThreadListener);
		}
	}

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_ChangeThreadSubject(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");
		string name = GetJsonValue_String(d, "newName", "");

		thread t([=]() {
			EMError error;
			CLIENT->getThreadManager().changeThreadSubject(thread_id, name, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_CreateThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string msg_id = GetJsonValue_String(d, "msgId", "");
		string group_id = GetJsonValue_String(d, "groupId", "");
		string name = GetJsonValue_String(d, "threadName", "");

		thread t([=]() {
			EMError error;
			EMThreadEventPtr result = CLIENT->getThreadManager().createThread(name, msg_id, group_id, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = ChatThread::ToJson(result);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_DestroyThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getThreadManager().destroyThread(thread_id, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchThreadListOfGroup(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string group_id = GetJsonValue_String(d, "groupId", "");
		bool joined = GetJsonValue_Bool(d, "joined", true);
		string cursor = GetJsonValue_String(d, "cursor", "");
		int page_size = GetJsonValue_Int(d, "pageSize", 20);

		thread t([=]() {
			EMError error;
			EMCursorResultRaw<EMThreadEventPtr> result = CLIENT->getThreadManager().fetchThreadListOfGroup(cursor, page_size, group_id, joined, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {
				

				string json = CursorResult::ToJson(cursor, result);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchThreadMembers(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");
		string cursor = GetJsonValue_String(d, "cursor", "");
		int page_size = GetJsonValue_Int(d, "pageSize", 20);

		thread t([=]() {
			EMError error;
			EMCursorResultRaw<std::string> result = CLIENT->getThreadManager().fetchThreadMembers(thread_id, cursor, page_size, error);
			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = CursorResult::ToJson(cursor, result.result());
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_GetLastMessageAccordingThreads(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		EMMucMemberList mem_list = MyJson::FromJsonObjectToVector(d["threadIds"]);

		thread t([=]() {
			EMError error;
			std::map<std::string, EMMessagePtr> result = CLIENT->getThreadManager().getLastMessageAccordingThreads(mem_list, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = ChatThread::ToJson(result);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_GetThreadDetail(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");

		thread t([=]() {
			EMError error;
			EMThreadEventPtr result = CLIENT->getThreadManager().getThreadDetail(thread_id, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = ChatThread::ToJson(result);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_JoinThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");

		thread t([=]() {
			EMError error;
			EMThreadEventPtr result = CLIENT->getThreadManager().joinThread(thread_id, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = ChatThread::ToJson(result);
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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_LeaveThread(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getThreadManager().leaveThread(thread_id, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_RemoveThreadMember(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string thread_id = GetJsonValue_String(d, "threadId", "");
		string user_name = GetJsonValue_String(d, "userId", "");

		thread t([=]() {
			EMError error;
			CLIENT->getThreadManager().removeThreadMember(thread_id, user_name, error);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL ThreadManager_FetchMineJoinedThreadList(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		string cursor = GetJsonValue_String(d, "cursor", "");
		int page_size = GetJsonValue_Int(d, "pageSize", 20);

		thread t([=]() {
			EMError error;
			EMCursorResultRaw<EMThreadEventPtr> result = CLIENT->getThreadManager().fetchMineJoinedThreadList(cursor, page_size, error);

			if (EMError::EM_NO_ERROR == error.mErrorCode) {

				string json = CursorResult::ToJson(cursor, result);
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
}
