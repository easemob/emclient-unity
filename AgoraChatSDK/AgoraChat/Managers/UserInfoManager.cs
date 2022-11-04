using System.Collections.Generic;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    /**
 * \~chinese
 * 用户信息管理类，负责更新及获取用户属性。
 *
 * \~english
 * The user information manager for updating and getting user attributes.
 */
    public class UserInfoManager : BaseManager
    {

        internal UserInfoManager(NativeListener listener) : base(listener, SDKMethod.userInfoManager)
        {

        }

        /**
         * \~chinese
         * 修改当前用户的信息。
         *
         * @param userInfo  要修改的用户信息。
         * @param handle    操作结果回调，详见 {@link CallBack}。
         *
         * \~english
         * Modifies the information of the current user.
         *
         * @param userInfo  The user information to be modified.
         * @param handle	The operation callback. See {@link CallBack}.
         */
        public void UpdateOwnInfo(UserInfo userInfo, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("userInfo", userInfo.ToJsonObject());
            NativeCall(SDKMethod.updateOwnUserInfo, jo_param, callback);
        }

        /**
         * \~chinese
         * 根据用户 ID 获取用户信息。
         *
         * @param userIds   用户 ID 列表。
         * @param handle    操作结果回调，成功则返回用户信息字典，失败则返回错误信息，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets user information by user ID.
         *
         * @param userIds   The list of user IDs.
         * @param handle	The operation callback. If success, the user information dictionary is returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */
        public void FetchUserInfoByUserId(List<string> userIds, ValueCallBack<Dictionary<string, UserInfo>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("userIds", JsonObject.JsonArrayFromStringList(userIds));

            Process process = (_, jsonNode) =>
            {
                return Dictionary.BaseModelDictionaryFromJsonObject<UserInfo>(jsonNode);
            };

            NativeCall<Dictionary<string, UserInfo>>(SDKMethod.updateOwnUserInfo, jo_param, callback, process);
        }
    }
}