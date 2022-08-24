using AgoraChat.SimpleJSON;
namespace AgoraChat
{
    /**
    * \~chinese
    * 用户信息类。
    * 
    * \~english
    * The user information class.
    */
    public class UserInfo: BaseModel
    {

        /**
         * \~chinese
         * 用户昵称。
         * 
         * \~english
         * The nickname of the user.
         */
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

        public UserInfo(string jsonString): base(jsonString) { }

        public UserInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            if (!jsonObject["nickName"].IsNull)
            {
                nickName = jsonObject["nickName"].Value;
            }

            if (!jsonObject["avatarUrl"].IsNull)
            {
                avatarUrl = jsonObject["avatarUrl"].Value;
            }

            if (!jsonObject["mail"].IsNull)
            {
                email = jsonObject["mail"].Value;
            }

            if (!jsonObject["phone"].IsNull)
            {
                phoneNumber = jsonObject["phone"].Value;
            }

            if (!jsonObject["sign"].IsNull)
            {
                signature = jsonObject["sign"].Value;
            }

            if (!jsonObject["birth"].IsNull)
            {
                birth = jsonObject["birth"].Value;
            }

            if (!jsonObject["userId"].IsNull)
            {
                userId = jsonObject["userId"].Value;
            }

            if (!jsonObject["gender"].IsNull)
            {
                gender = jsonObject["gender"].AsInt;
            }

            if (!jsonObject["ext"].IsNull)
            {
                ext = jsonObject["ext"].Value;
            }
        }
    }
}