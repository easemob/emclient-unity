using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
    * \~chinese
    * 用户信息类。
    * 
    * \~english
    * The user information class.
    */
    [Preserve]
    public class UserInfo : BaseModel
    {

        /**
         * \~chinese
         * 用户昵称。
         * 
         * \~english
         * The nickname of the user.
         */
        public string NickName = "";
        /**
         * \~chinese
         * 用户头像的 URL。
         * 
         * \~english
         * The avatar URL of the user.
         */
        public string AvatarUrl = "";
        /**
         * \~chinese
         * 用户的电子邮件地址。
         * 
         * \~english
         * The email address of the user.
         */
        public string Email = "";
        /**
        * \~chinese
        * 用户的电话号码。
        * 
        * \~english
        * The phone number of the user.
        */
        public string PhoneNumber = "";
        /**
         * \~chinese
         * 用户的签名。
         * 
         * \~english
         * The signature of the user.
         */
        public string Signature = "";
        /**
        * \~chinese
        * 用户的生日。
        * 
        * \~english
        * The birthday of the user.
        */
        public string Birth = "";
        /**
         * \~chinese
         * 用户 ID。
         * 
         * \~english
         * The user ID.
         */
        public string UserId = "";
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
        public string Ext = "";
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
        public int Gender = 0;

        [Preserve]
        public UserInfo() { }

        [Preserve]
        internal UserInfo(string jsonString) : base(jsonString) { }

        [Preserve]
        internal UserInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            if (!jsonObject["nickName"].IsNull)
            {
                NickName = jsonObject["nickName"].Value;
            }

            if (!jsonObject["avatarUrl"].IsNull)
            {
                AvatarUrl = jsonObject["avatarUrl"].Value;
            }

            if (!jsonObject["mail"].IsNull)
            {
                Email = jsonObject["mail"].Value;
            }

            if (!jsonObject["phone"].IsNull)
            {
                PhoneNumber = jsonObject["phone"].Value;
            }

            if (!jsonObject["sign"].IsNull)
            {
                Signature = jsonObject["sign"].Value;
            }

            if (!jsonObject["birth"].IsNull)
            {
                Birth = jsonObject["birth"].Value;
            }

            if (!jsonObject["userId"].IsNull)
            {
                UserId = jsonObject["userId"].Value;
            }

            if (!jsonObject["gender"].IsNull)
            {
                Gender = jsonObject["gender"].AsInt;
            }

            if (!jsonObject["ext"].IsNull)
            {
                Ext = jsonObject["ext"].Value;
            }
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("nickName", NickName);
            jo.AddWithoutNull("avatarUrl", AvatarUrl);
            jo.AddWithoutNull("mail", Email);
            jo.AddWithoutNull("phone", PhoneNumber);
            jo.AddWithoutNull("sign", Signature);
            jo.AddWithoutNull("birth", Birth);
            jo.AddWithoutNull("gender", Gender);
            jo.AddWithoutNull("userId", UserId);
            jo.AddWithoutNull("ext", Ext);

            return jo;
        }
    }
}