using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    /**
    * \~chinese
    * 消息 Reaction 实体类，有如下属性：
    *
    *   Reaction：消息 Reaction。
    *   UserCount：添加了指定 Reaction 的用户数量。
    *   UserList：添加了指定 Reaction 的用户列表。
    *   State 当前用户是否添加了该 Reaction。
    *
    * \~english
    * The message Reaction instance class, which has the following attributes:
    *
    *   Reaction: The message Reaction.
    *   UserCount: The count of users that added the Reaction.
    *   UserList: The list of users that added the Reaction.
    *   State: Whether the current user added this Reaction.
    */
    [Preserve]
    public class MessageReaction : BaseModel
    {
        /**
         * \~chinese
         * 获取 Reaction 内容。
         *
         * \~english
         * Gets the Reaction.
         */
        public string Reaction;

        /**
         * \~chinese
         * 获取添加了指定 Reaction 的用户数量。
         *
         * \~english
         * Gets the count of users that added this Reaction.
         */
        public int Count;

        /**
         * \~chinese
         * 获取添加了指定 Reaction 的用户列表。
         *
         * **Note**
         *  只有通过 {@link IChatManager#GetReactionDetail} 接口获取的是全部用户的分页数据；其他相关接口如{@link IChatManager#GetReactionList}等都只包含前三个用户。
         *
         * @return 用户列表。
         *
         * \~english
         * Gets the list of users that added this Reaction.
         *
         * **Note**
         * {@link IChatManager#GetReactionDetail} can return the entire list of users that added this Reaction with pagination, whereas other methods such as {@link IChatManager#GetReactionList} can only return the first three users.
         * @return  The list of users that added this Reaction.
         */
        public List<string> UserList;

        /**
         * \~chinese
         * 获取当前用户是否添加过该 Reaction。
         *
         *  - `true`：是；
         *  - `false`：否。
         *
         * \~english
         * Gets whether the current user has added the Reaction.
         *
         *  - `true`: Yes.
         *  - `false`: No.
         */
        public bool State;

        [Preserve]
        internal MessageReaction() { }

        [Preserve]
        internal MessageReaction(string jsonString) : base(jsonString) { }

        [Preserve]
        internal MessageReaction(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            Reaction = jsonObject["reaction"];
            Count = jsonObject["count"].AsInt;
            UserList = List.StringListFromJsonArray(jsonObject["userList"]);
            State = jsonObject["isAddedBySelf"].AsBool;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("reaction", Reaction);
            jo.AddWithoutNull("count", Count);
            jo.AddWithoutNull("userList", JsonObject.JsonArrayFromStringList(UserList));
            jo.AddWithoutNull("isAddedBySelf", State);
            return jo;
        }
    }
}
