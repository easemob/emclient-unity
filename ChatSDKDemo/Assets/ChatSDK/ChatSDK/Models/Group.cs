using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;

namespace ChatSDK
{
    /// <summary>
    /// 群组对象
    /// </summary>
    public class Group
    {

        internal Group(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);
                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    this.GroupId = jo["groupId"].Value;
                    this.Name = jo["name"].Value;
                    this.Description = jo["desc"].Value;
                    this.Owner = jo["owner"].Value;
                    this.Annoumcement = jo["announcement"].Value;
                    this.MemberCount = jo["memberCount"].AsInt;
                    this.MemberList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                    this.AdminList = TransformTool.JsonStringToStringList(jo["adminList"].Value);
                    this.BlockList = TransformTool.JsonStringToStringList(jo["blockList"].Value);
                    this.MuteList = TransformTool.JsonStringToStringList(jo["muteList"].Value);
                    this.NoticeEnabled = jo["noticeEnable"].AsBool;
                    this.MessageBlocked = jo["messageBlocked"].AsBool;
                    this.IsAllMemberMuted = jo["isAllMemberMuted"].AsBool;
                    string optionsString = jo["options"].Value;
                    if (optionsString != null)
                    {
                        this.Options = new GroupOptions(optionsString);
                    }

                    if (jo["permissionType"].AsInt == -1)
                    {
                        this.PermissionType = GroupPermissionType.None;
                    }
                    else if (jo["permissionType"].AsInt == 0)
                    {
                        this.PermissionType = GroupPermissionType.Member;
                    }
                    else if (jo["permissionType"].AsInt == 1)
                    {
                        this.PermissionType = GroupPermissionType.Admin;
                    }
                    else if (jo["permissionType"].AsInt == 2)
                    {
                        this.PermissionType = GroupPermissionType.Owner;
                    }
                }
            }
        }

        /// <summary>
        /// 群组id
        /// </summary>
        public string GroupId { get; internal set; }

        /// <summary>
        /// 群组名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 群组描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 群主
        /// </summary>
        public string Owner { get; internal set; }

        /// <summary>
        /// 群公告
        /// </summary>
        public string Annoumcement { get; internal set; }

        /// <summary>
        /// 群人数
        /// </summary>
        public int MemberCount { get; internal set; }

        /// <summary>
        /// 群成员列表
        /// </summary>
        public List<string> MemberList { get; internal set; }

        /// <summary>
        /// 管理员列表
        /// </summary>
        public List<string> AdminList { get; internal set; }

        /// <summary>
        /// 黑名单列表
        /// </summary>
        public List<string> BlockList { get; internal set; }

        /// <summary>
        /// 禁言列表
        /// </summary>
        public List<string> MuteList { get; internal set; }

        /// <summary>
        /// 是否推送
        /// </summary>
        public bool NoticeEnabled { get; internal set; }

        /// <summary>
        /// 不接收群消息
        /// </summary>
        public bool MessageBlocked { get; internal set; }

        /// <summary>
        /// 是否全员禁言
        /// </summary>
        public bool IsAllMemberMuted { get; internal set; }

        /// <summary>
        /// 去设置
        /// </summary>
        public GroupOptions Options { get; internal set; }

        /// <summary>
        /// 当前用户在群中的角色
        /// </summary>
        public GroupPermissionType PermissionType { get; internal set; }

        internal Group()
        {
            //default constructor
        }

    }
}