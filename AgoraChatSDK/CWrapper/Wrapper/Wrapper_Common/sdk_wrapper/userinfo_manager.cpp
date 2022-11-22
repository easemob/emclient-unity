#include "emclient.h"

#include "sdk_wrapper_internal.h"
#include "tool.h"
#include "callbacks.h"
#include "sdk_wrapper.h"

enum UserInfoType {
	NICKNAME = 0,
	AVATAR_URL = 1,
	EMAIL = 2,
	PHONE = 3,
	GENDER = 4,
	SIGN = 5,
	BIRTH = 6,
	EXT = 100
};

std::map<UserInfoType, std::string> UserInfoTypeMap =
{
	{NICKNAME,      "nickname"},
	{AVATAR_URL,    "avatarurl"},
	{EMAIL,         "mail"},
	{PHONE,         "phone"},
	{GENDER,        "gender"},
	{SIGN,          "sign"},
	{BIRTH,         "birth"},
	{EXT,           "ext"}
};

extern EMClient* gClient;

namespace sdk_wrapper {

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL UserInfoManager_FetchUserInfoByUserId(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		Document d; d.Parse(jstr);
		EMMucMemberList id_list = MyJson::FromJsonObjectToVector(d["userIds"]);

		std::vector<std::string> attrv;
		for (auto it : UserInfoTypeMap) {
			attrv.push_back(it.second);
		}

        thread t([=]() {
            std::string response = "";
            EMError error;
            CLIENT->getUserInfoManager().fetchUsersPropertyByType(id_list, attrv, response, error);

            if (EMError::EM_NO_ERROR == error.mErrorCode) {

				map<string, UserInfo> user_map = UserInfo::FromJsonFromServer(response);
				string json = UserInfo::ToJson(user_map);

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

	SDK_WRAPPER_API const char* SDK_WRAPPER_CALL UserInfoManager_UpdateOwnInfo(const char* jstr, const char* cbid = nullptr, char* buf = nullptr)
	{
		if (!CheckClientInitOrNot(cbid)) return nullptr;

		string local_cbid = cbid;

		UserInfo ui = UserInfo::FromJson(jstr);
		string userinfo_json = UserInfo::ToJsonForServer(ui);

		thread t([=]() {
			std::string response = "";
			EMError error;
			CLIENT->getUserInfoManager().updateOwnUserInfo(userinfo_json, response, error);

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
}