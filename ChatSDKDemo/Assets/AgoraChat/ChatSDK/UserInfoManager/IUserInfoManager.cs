using System.Collections.Generic;

namespace ChatSDK
{
    /**
 * \~chinese
 * 用户信息管理类，负责更新及获取用户属性。
 *
 * \~english
 * The user information manager for updating and getting user attributes.
 */
    public abstract class IUserInfoManager
    {
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
        public abstract void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null);

        /**
         * \~chinese
         * 根据用户 ID 获取用户信息。
         *
         * @param idList    用户 ID 列表。
         * @param handle    操作结果回调，成功则返回用户信息字典，失败则返回错误信息，详见 {@link ValueCallBack}。
         *
         * \~english
         * Gets user information by user ID.
         *
         * @param userIds   The list of user IDs.
         * @param handle	The operation callback. If success, the user information dictionary is returned; otherwise, an error is returned. See {@link ValueCallBack}.
         */        
	    public abstract void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null);
    }
}
