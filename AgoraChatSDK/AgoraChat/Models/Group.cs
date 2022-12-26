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
         * 若要获取服务器端的群组管理员列表，可调用 {@link IGroupManager#GetGroupSpecificationFromServer(String, ValueCallBack)} 获取群组详情。
         *
         * \~english
         * The admin list of the group.
         * 
         * To get the admin list of the group from the server, you can call {@link IGroupManager#GetGroupSpecificationFromServer(String, ValueCallBack)} to get group details.
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
        internal GroupOptions Options;

        /**
         * \~chinese
         * 当前用户在群组中的角色。
         * 
         * \~english
         * The role of the current user in the group.
         * 
         */
        public GroupPermissionType PermissionType { get; internal set; }

        /**
         * \~chinese
         * 获取群组属性：成员是否能自由加入，还是需要申请或者被邀请。
         * 
         * 群组有四个类型属性，`IsMemberOnly`是除了 {@link GroupStyle#PublicOpenJoin} 之外的三种属性，表示该群不是自由加入的群组。
         *
         * - `true`：进群需要群主邀请，群成员邀请，或者群主和管理员同意入群申请；
         * - `false`：意味着用户可以自由加入群，不需要申请和被邀请。
         *
         * \~english
         * Fetches the group property: whether users can auto join the group VS need requesting or invitation from a group member to join the group.
         * There are four types of group properties used to define the style of a group, and `IsMemberOnly` contains three types including: PrivateOnlyOwnerInvite,  PrivateMemberCanInvite, PublicJoinNeedApproval. And do not include {@link GroupStyle#PublicOpenJoin}.
         *
         * - `true`: Users can not join the group freely. Needs the invitation from the group owner or members, or the application been approved by the group owner or admins.
         * - `false`: Users can join freely without the group owner or member‘s invitation or the new joiner’s application been approved.
         */
        public bool IsMemberOnly { get; internal set; }

        /**
         * \~chinese 获取群组是否允许成员邀请。
         * 
         * \~english 
         * - `true`：群成员可以邀请其他用户加入； - `false`：不允许群成员邀请其他用户加入。 \~english Gets whether the group member is allowed to invite other users to join the group.
         */
        public bool IsMemberAllowToInvite { get; internal set; }

        /**
         * \~chinese 获取群允许加入的最大成员数，在创建群时确定。 需要获取群详情才能拿到正确的结果，如果没有获取则返回 0。
         * 
         * \~english 
         * The max number of group members allowed in a group. The param is set when the group is created. Be sure to fetch the detail specification of the group from the server first, see IGroupManager#GetGroupSpecificationFromServer(String, ValueCallBack). If not, the SDK returns 0.
         */
        public int MaxUserCount { get; internal set; }


        /**
        *  \~chinese
        *  群组是否禁用。（本地数据库不存储，从数据库读取或拉取漫游消息默认值是 NO）
        *
        *  \~english
        *  Whether the group is disabled. The default value for reading or pulling roaming messages from the database is NO
         */
        public bool IsDisabled { get; internal set; }

        /**
         * \~chinese
         * 获取群组订制扩展信息。
         * @return  群组定制扩展信息。
         *
         * \~english
         * Gets the customized extension of the group.
         * @return  The customized extension of the group.
         */
        public string Ext { get; internal set; }

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
            //Options = ModelHelper.CreateWithJsonObject<GroupOptions>(jsonObject["options"]);
            MaxUserCount = jsonObject["maxUserCount"].AsInt;
            IsMemberOnly = jsonObject["isMemberOnly"].AsBool;
            IsMemberAllowToInvite = jsonObject["isMemberAllowToInvite"].AsBool;
            Ext = jsonObject["ext"];
            PermissionType = (GroupPermissionType)jsonObject["permissionType"].AsInt;
            IsDisabled = jsonObject["isDisabled"].AsBool;
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
            jo.AddWithoutNull("isDisabled", IsDisabled);
            return jo;
        }
    }
}