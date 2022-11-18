using AgoraChat.SimpleJSON;

namespace AgoraChat
{

    /**
     * \~chinese
     * 群组的基本信息, 用于定义公开群的信息。
     * 
     * 若要从服务器获取群组基本信息，可调用 {@link GetGroupSpecificationFromServer(string  ValueCallBack<Group>)} 。
     *
     * \~english
     * The class that defines basic information of public groups.
     * 
     * To get basic information of a group from the server, you can call {@link IGroupManager#FetchPublicGroupsFromServer(int, String, ValueCallBack)}.
     */
    public class GroupInfo : BaseModel
    {
        /**
         * \~chinese
         * 群组 ID。
         *
         * \~english
         * The group ID.
         */
        public string GroupId { get; internal set; }

        /**
	     * \~chinese
	     * 群组名称。
	     *
	     * \~english
	     * The group name.
	     */
        public string GroupName { get; internal set; }

        internal GroupInfo() { }

        internal GroupInfo(string jsonString) : base(jsonString) { }

        internal GroupInfo(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            GroupId = jsonObject["groupId"];
            GroupName = jsonObject["name"];
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("groupId", GroupId);
            jo.AddWithoutNull("name", GroupName);
            return jo;
        }
    }
}