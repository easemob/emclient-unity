using System;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    /**
     * \~chinese
     * 用户属性枚举类。
     * 
     * \~english
     * The enumeration of user attributes.  
     */
    public enum UserInfoType
    {
        /**
         * \~chinese
         * 用户昵称。
         * 
         * \~english
         * The nickname of the user.
         */
        NICKNAME = 0,

        /**
         * \~chinese
         * 用户头像的 URL。
         * 
         * \~english
         * The avatar URL of the user.
         */
        AVATAR_URL = 1,

        /**
         * \~chinese
         * 用户的电子邮件地址。
         * 
         * \~english
         * The email address of the user.
         */
        EMAIL = 2,

        /**
         * \~chinese
         * 用户的电话号码。
         * 
         * \~english
         * The phone number of the user.
         */
        PHONE = 3,

        /**
         * \~chinese
         * 用户的性别。
         * - （默认）`0`：未知；
         * - `1`：男；
         * - `2`：女。
         * 
         * \~english
         * The gender of the user.
         * - (Default) `0`: Unknown.
         * - `1`: Male.
         * - `2`: Female.
         */
        GENDER = 4,

        /**
         * \~chinese
         * 用户的签名。
         * 
         * \~english
         * The signature of the user.
         */
        SIGN = 5,

        /**
         * \~chinese
         * 用户的生日。
         * 
         * \~english
         * The birthday of the user.
         */
        BIRTH = 6,

        /**
         * \~chinese
         * 用户的扩展信息。
         * 
         * 用户可自行扩展，建议封装成 JSON 字符串，也可设置为空字符串。
         * 
         * \~english
         * The extension information of the user. 
         * 
         * You can specify either an empty string or the custom information encapsulated as the JSON string.
         */
        EXT = 100
    };

    /**
    * \~chinese
    * 用户信息类。
    * 
    * \~english
    * The user information class.
    */
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class UserInfo
    {
        /**
         * \~chinese
         * 用户昵称。
         * 
         * \~english
         * The nickname of the user.
         */
		 [MarshalAs(UnmanagedType.LPTStr)]
        public string nickName = "";
        /**
         * \~chinese
         * 用户头像的 URL。
         * 
         * \~english
         * The avatar URL of the user.
         */
        public string avatarUrl = "";
        /**
         * \~chinese
         * 用户的电子邮件地址。
         * 
         * \~english
         * The email address of the user.
         */
        public string email = "";
         /**
         * \~chinese
         * 用户的电话号码。
         * 
         * \~english
         * The phone number of the user.
         */
        public string phoneNumber = "";
        /**
         * \~chinese
         * 用户的签名。
         * 
         * \~english
         * The signature of the user.
         */
        public string signature = "";
         /**
         * \~chinese
         * 用户的生日。
         * 
         * \~english
         * The birthday of the user.
         */
        public string birth = "";
        /**
         * \~chinese
         * 用户 ID。
         * 
         * \~english
         * The user ID.
         */
        public string userId = "";
         /**
         * \~chinese
         * 用户的扩展信息。
         * 
         * 用户可自行扩展，建议封装成 JSON 字符串，也可设置为空字符串。
         * 
         * \~english
         * The extension information of the user. 
         * 
         * You can specify either an empty string or the custom information encapsulated as the JSON string.
         */
        public string ext = "";
        /**
         * \~chinese
         * 用户的性别。
         * - （默认）`0`：未知；
         * - `1`：男；
         * - `2`：女。
         * 
         * \~english
         * The gender of the user.
         * - (Default) `0`: Unknown.
         * - `1`: Male.
         * - `2`: Female.
         */
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
