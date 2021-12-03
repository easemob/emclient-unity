using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    public enum UserInfoType
    {
        /// <summary>
        /// 昵称 
        /// </summary>
        NICKNAME = 0,

        /// <summary>
        /// 头像 
        /// </summary>
        AVATAR_URL = 1,

        /// <summary>
        /// 邮箱
        /// </summary>
        EMAIL = 2,

        /// <summary>
        /// 电话
        /// </summary>
        PHONE = 3,

        /// <summary>
        /// 性别
        /// </summary>
        GENDER = 4,

        /// <summary>
        /// 签名
        /// </summary>
        SIGN = 5,

        /// <summary>
        /// 生日
        /// </summary>
        BIRTH = 6,

        /// <summary>
        /// 扩展字段
        /// </summary>
        EXT = 100
    };

    /// <summary>
    /// gender: 性别(默认为0，1表示男，2表示女，其他为非法)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class UserInfo
    {
        public string nickName = "";
        public string avatarUrl = "";
        public string email = "";
        public string phoneNumber = "";
        public string signature = "";
        public string birth = "";
        public string userId = "";
        public string ext = "";
        public int gender = 0;
    };
}
