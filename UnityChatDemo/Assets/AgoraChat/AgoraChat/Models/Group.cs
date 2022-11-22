using System.Collections.Generic;
using AgoraChat.SimpleJSON;
namespace AgoraChat
{
    public class Group : BaseModel
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
        public string Name { get; internal set; }

        /**
         * \~chinese
         * 群组描述。
         *
         * \~english
         * The group description.
         */
        public string Description { get; internal set; }

        /**
         * \~chinese
         * 群主信息。
         *
         * \~english
         * The group owner.
         * 
         */
        public string Owner { get; internal set; }

        /**
         * \~chinese
         * 群组公告。
         * 
         * 从服务端获取群组公告，可调用 {@link IGroupManager#GetGroupAnnouncementFromServer(String, ValueCallBack)}。
         * 
         *
         * \~english
         * The group announcement.
         * 
         * To get the group announcement from the server, you can call {@link IGroupManager#GetGroupAnnouncementFromServer(String, ValueCallBack)}.
         */
        public string Announcement { get; internal set; }

        /**
         * \~chinese
         * 群组成员数量。
         *
         * \~english
         * The number of members in the group.
         * 
         */
        public int MemberCount { get; internal set; }

        /**
         * \~chinese
         * 群组成员列表。
         *
         * \~english
         * The member list of the group.
         * 
         */
        public List<string> MemberList { get; internal set; }

        /**
         * \~chinese
         * 群组管理员列表。
         * 
         * 若要获取服务器端的群组管理员列表，可调用 {@link IGroupManager#GetGroupWithId(String)} 获取群组详情。
         *
         * \~english
         * The admin list of the group.
         * 
         * To get the admin list of the group from the server, you can call {@link IGroupManager#GetGroupWithId(String)} to get group details.
         * 
         */
        public List<string> AdminList { get; internal set; }

        /**
         * \~chinese
         * 群组黑名单。
         * 
         * 若要获取服务器端的群组黑名单列表，可调用 {@link IGroupManager#GetGroupBlockListFromServer(String, int, int, ValueCallBack)}。
         *
         * \~english
         * Gets the block list of the group.
         * 
         * To get the block list of the group from the server, you can call {@link IGroupManager#GetGroupBlockListFromServer(String, int, int, ValueCallBack)}.
         * 
         */
        public List<string> BlockList { get; internal set; }

        /**
         * \~chinese
         * 群组禁言列表。
         * 
         * 若要获取服务器端的群组禁言列表，可调用 {@link IGroupManager#GetGroupMuteListFromServer(String, int, int, ValueCallBack)}。
         *
         * \~english
         * Gets the mute list of the group.
         * 
         * To get the mute list of the group from the server, you can call {@link IGroupManager#GetGroupMuteListFromServer(String, int, int, ValueCallBack)}.
         *
         */
        public List<string> MuteList { get; internal set; }

        /**
         * \~chinese
         * 是否开启了推送通知。
         * - `true`：是；
         * - `false`：否。
         * 
         * \~english
         * Whether push notifications are enabled.
         * - `true`: Yes.
         * - `false`: No.
         * 
         */
        public bool NoticeEnabled { get; internal set; }

        /**
         * \~chinese
         * 是否已屏蔽群消息。
         * - `true`：是；
         * - `false`：否。
         * 
         * 开启屏蔽群消息，请参见 {@link IGroupManager#BlockGroup(String，CallBack}。
         * 
         * 取消屏蔽群消息，请参见 {@link IGroupManager#UnBlockGroup(String, CallBack)}。
         *
         * \~english
         * Whether group messages are blocked.
         * - `true`: Yes.
         * - `false`: No.
         
         * To block group messages, you can call {@link IGroupManager#BlockGroup(String，CallBack}.
         * 
         * To unblock group messages, you can call {@link IGroupManager#UnBlockGroup(String, CallBack)}.
         * 
         */
        public bool MessageBlocked { get; internal set; }

        /**
         * \~chinese
         * 是否全员禁言。
         * - `true`：是；
         * - `false`：否。
         * 
         * 在收到禁言回调 {@link OnMuteListAddedFromGroup} 或解禁回调 {@link OnMuteListRemovedFromGroup} 时，内存中对象的禁言状态会更新。
         * 
         * 内存中对象被回收后以及再次从数据库或者服务端拉取后，该状态不可信。
         *
         * \~english
         * Whether all members are muted.
         * - `true`: Yes.
         * - `false`: No.
         * 
         * The mute state of the in-memory object is updated when the mute callback {@link OnMuteListAddedFromGroup} or unmuting callback {@link OnMuteListRemovedFromGroup} is received.
         *
         *  After the in-memory object is collected and pulled again from the database or server, the state becomes untrustworthy.
         */
        public bool IsAllMemberMuted { get; internal set; }

        /**
         * \~chinese
         * 群组选项。
         * 
         * \~english
         * The group options.
         * 
         */
        public GroupOptions Options { get; internal set; }

        /**
         * \~chinese
         * 当前用户在群组中的角色。
         * 
         * \~english
         * The role of the current user in the group.
         * 
         */
        public GroupPermissionType PermissionType { get; internal set; }

        internal Group() { }

        internal Group(string jsonString) : base(jsonString) { }

        internal Group(JSONObject jsonObject) : base(jsonObject) { }

        internal override void FromJsonObject(JSONObject jsonObject)
        {
            GroupId = jsonObject["groupId"];
            Name = jsonObject["name"];
            Description = jsonObject["desc"];
            Owner = jsonObject["owner"];
            Announcement = jsonObject["announcement"];
            MemberCount = jsonObject["memberCount"];
            MemberList = List.StringListFromJsonArray(jsonObject["memberList"]);
            AdminList = List.StringListFromJsonArray(jsonObject["adminList"]);
            BlockList = List.StringListFromJsonArray(jsonObject["blockList"]);
            MuteList = List.StringListFromJsonArray(jsonObject["muteList"]);
            MessageBlocked = jsonObject["block"];
            IsAllMemberMuted = jsonObject["isMuteAll"];
            Options = ModelHelper.CreateWithJsonObject<GroupOptions>(jsonObject["options"]);
            PermissionType = (GroupPermissionType)jsonObject["permissionType"].AsInt;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();
            jo.AddWithoutNull("groupId", GroupId);
            jo.AddWithoutNull("name", Name);
            jo.AddWithoutNull("desc", Description);
            jo.AddWithoutNull("owner", Owner);
            jo.AddWithoutNull("announcement", Announcement);
            jo.AddWithoutNull("memberCount", MemberCount);
            jo.AddWithoutNull("memberList", JsonObject.JsonArrayFromStringList(MemberList));
            jo.AddWithoutNull("adminList", JsonObject.JsonArrayFromStringList(AdminList));
            jo.AddWithoutNull("blockList", JsonObject.JsonArrayFromStringList(BlockList));
            jo.AddWithoutNull("muteList", JsonObject.JsonArrayFromStringList(MuteList));
            jo.AddWithoutNull("block", MessageBlocked);
            jo.AddWithoutNull("isMuteAll", IsAllMemberMuted);
            jo.AddWithoutNull("options", Options.ToJsonObject());
            jo.AddWithoutNull("permissionType", PermissionType.ToInt());
            return jo;
        }
    }
}