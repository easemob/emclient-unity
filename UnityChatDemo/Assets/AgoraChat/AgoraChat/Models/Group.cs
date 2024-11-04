using System.Collections.Generic;
using AgoraChat.SimpleJSON;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
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
         * 用户是否只能通过群成员邀请或申请才能加入群组：
         * - `true`：是。
         * - `false`：否。用户可自由加入群组，无需群成员邀请或提交入群申请。
         * 
         * 群组类型（`GroupStyle`）设置为 `PrivateOnlyOwnerInvite`, `PrivateMemberCanInvite` 或 `PublicJoinNeedApproval` 时，该属性的值为 `true`。
         *
         * \~english
         * Whether users can join a group only via a join request or a group invitation:
         * - `true`: Yes.
         * - `false`: No. Users can join a group freely, without a join request or a group invitation.
         * 
         * When `GroupStyle` is set to `PrivateOnlyOwnerInvite`, `PrivateMemberCanInvite`, or `PublicJoinNeedApproval`, the attribute value is `true`. 
         * 
         *
         */
        public bool IsMemberOnly { get; internal set; }

        /**
         * \~chinese 
         * 群组是否允许除群主和管理员之外的成员邀请用户入群：
         * - `true`：允许。所有群成员均可邀请其他用户加入群组。
         * - `false`：不允许。只有群主和群管理员可邀请其他用户加入群组。
         * 
         * \~english 
         * Whether other group members than the group owner and admins can invite users to join the group:
         * - `true`: Yes. All group members can invite other users to join the group.
         * - `false`: No. Only the group owner and admins can invite other users to join the group.
         */
        public bool IsMemberAllowToInvite { get; internal set; }

        /**
         * \~chinese 
         * 群组允许加入的最大成员数。
         * 
         * 该属性在创建群时确定。
         * 
         * 需要获取群详情才能得到正确的结果，如果没有获取则返回 `0`。
         * 
         * \~english 
         * The maximum number of members allowed in a group. 
         * 
         * The attribute is set during group creation. 
         
         * To get the correct attribute value, you need to first get the details of the group from the server by calling {@link IGroupManager#GetGroupSpecificationFromServer(String, ValueCallBack)}. Otherwise, the SDK returns `0`.
         */
        public int MaxUserCount { get; internal set; }


        /**
        *  \~chinese
        *  群组是否禁用：
        *  - `true`：该群组被禁用。群组成员无法收发消息，也不能进行群组和群成员的任何管理操作。而且，该群组的子区中也无法收发消息，也不能进行子区和成员的任何管理操作。
        *  - `false`：该群组为正常状态。群组成员可以正常收发消息，进行群组和群成员的管理操作。而且，该群组的子区也能发送和接收消息，进行子区和成员的管理操作。
        
        *  本地数据库不存储该状态。对于从数据库加载的群组，该属性的默认值为 `NO`。
        *
        *  \~english
        *  Whether the group is disabled:
        *  - `true`: Yes. The group is disabled. Group members cannot send or receive messages, nor perform group and member management operations. This is also the case for threads in this group.
        *  - `false`: No. The group is in the normal state. Group members can send and receive messages, as well as perform group and member management operations. This is also the case for threads in this group.
        * 
        *  This attribute is not stored in the local database.
        *  
        * For groups loaded from the local database, the default value of this attribute is `NO`.
         */
        public bool IsDisabled { get; internal set; }

        /**
         * \~chinese
         * 群组的自定义扩展信息。
         *
         * @return  群组自定义扩展信息。
         *
         * \~english
         * Gets the custom extension information of the group.
         * 
         * @return  The custom extension information of the group.
         */
        public string Ext { get; internal set; }

        [Preserve]
        internal Group() { }

        [Preserve]
        internal Group(string jsonString) : base(jsonString) { }

        [Preserve]
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
            jo.AddWithoutNull("permissionType", PermissionType.ToInt());
            // jo.AddWithoutNull("options", Options.ToJsonObject());
            // jo.AddWithoutNull("isDisabled", IsDisabled);
            return jo;
        }
    }
}
