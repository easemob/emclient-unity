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
        [MarshalAs(UnmanagedType.LPTStr)]
        public string nickName = "";

        public string avatarUrl = "";
        public string email = "";
        public string phoneNumber = "";
        public string signature = "";
        public string birth = "";
        public string userId = "";
        public string ext = "";
        public int gender = 0;

        public UserInfo()
        {
            //default constructor
        }

        internal void Unmarshall()
        {
            nickName = TransformTool.GetUnicodeStringFromUTF8(nickName);
        }

        //public UserInfo(string jsonString)
        internal UserInfo(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    if(!jo["nickName"].IsNull) {
                        this.nickName = jo["nickName"].Value;
                    }

                    if (!jo["avatarUrl"].IsNull) {
                        this.avatarUrl = jo["avatarUrl"].Value;
                    }

                    if (!jo["mail"].IsNull) {
                        this.email = jo["mail"].Value;
                    }

                    if (!jo["phone"].IsNull) {
                        this.phoneNumber = jo["phone"].Value;
                    }

                    if (!jo["sign"].IsNull)
                    {
                        this.signature = jo["sign"].Value;
                    }

                    if (!jo["birth"].IsNull)
                    {
                        this.birth = jo["birth"].Value;
                    }

                    if (!jo["userId"].IsNull)
                    {
                        this.userId = jo["userId"].Value;
                    }

                    if (!jo["gender"].IsNull)
                    {
                        this.gender = jo["gender"].AsInt;
                    }
                    
                    if (!jo["ext"].IsNull) {
                        this.ext = jo["ext"].Value;
                    }
                    
                }
            }
        }

        //public JSONObject ToJson()
        internal JSONObject ToJson()
        {
            JSONObject jo = new JSONObject();
            jo.Add("userId", userId);
            if (nickName != null) {
                jo.Add("nickName", nickName);
            }

            if (avatarUrl != null) {
                jo.Add("avatarUrl", avatarUrl);
            }

            if (email != null) {
                jo.Add("mail", email);
            }

            if (phoneNumber != null) {
                jo.Add("phone", phoneNumber);
            }

            if (signature != null) {
                jo.Add("sign", signature);
            }

            if (birth != null) {
                jo.Add("birth", birth);
            }

            if(ext != null)
            {
                jo.Add("ext", ext);
            }
            
           
            jo.Add("gender", gender);
            return jo;
        }
    };
}
