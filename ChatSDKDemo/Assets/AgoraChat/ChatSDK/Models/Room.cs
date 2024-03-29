﻿using System.Collections.Generic;
using SimpleJSON;

namespace AgoraChat
{
    /**
     *  \~chinese
     * 聊天室类，用于定义聊天室信息。
     *
     *  \~english
     *  The chat room class, which defines chat room information.
     */
    public class Room
    {

        /**
         * \~chinese
         * 聊天室 ID。
         *
         * \~english
         * The chat room ID.
         *
         */
        public string RoomId { get; internal set; }

        /**
         * \~chinese
         * 聊天室名称。
         *
         * \~english
         * The chat room name.
         */
        public string Name { get; internal set; }
        /**
         * \~chinese
         * 聊天室描述。
         *
         * \~english
         * The chat room description.
         */
        public string Description { get; internal set; }

        /**
         * \~chinese
         * 聊天室公告。
         *
         * \~english
         * The chat room announcement.
         */
        public string Announcement { get; internal set; }

         /**
         * \~chinese
         * 在线成员数。
         *
         * \~english
         * The number of online members.
         */
        public int MemberCount { get; internal set; }

        /**
         * \~chinese
         * 聊天室的管理员列表。
         *
         * \~english
         * The admin list of the chat room.
         */
        public List<string> AdminList { get; internal set; }

        /**
         * \~chinese
         * 聊天室的成员列表。
         * 
         * 若从服务器端获取成员列表，可调用 `{@link IRoomManager#FetchRoomMembers(String, String, int, ValueCallBack)}` 方法。
         *
         * \~english
         * The member list of the chat room.
         * 
         * To get the member list of the chat room from the server, you can call `{@link IRoomManager#FetchRoomMembers(String, String, int, ValueCallBack)}`.
         */
        public List<string> MemberList { get; internal set; }

        /**
         * \~chinese
         * 聊天室的黑名单。
         * 
         * 若从服务器端获取黑名单，可调用 `{@link IRoomManager#FetchRoomBlockList(String, int, int, ValueCallBack)}` 方法。
         *
         * \~english
         * The block list of the chat room.
         * 
         * To get the block list of the chat room from the server, you can call `{@link IRoomManager#FetchRoomBlockList(String, int, int, ValueCallBack)}`.         
         */
        public List<string> BlockList { get; internal set; }

        /**
         * \~chinese
         * 聊天室的禁言列表。
         * 
         * 若从服务器端获取禁言列表，可调用 `{@link IRoomManager#FetchRoomMuteList(String, int, int, ValueCallBack)}` 方法。
         *
         *
         * \~english
         * The mute list of the chat room.
         * 
         * To get the mute list of the chat room from the server, you can call `{@link IRoomManager#FetchRoomMuteList(String, int, int, ValueCallBack)}`.
         *
         */
        public List<string> MuteList { get; internal set; }

        /**
         * \~chinese
         * 聊天室最大成员数，在创建聊天室时确定。
         * 
         * 如需获取最新数据，可调用 `{@link IRoomManager#FetchRoomInfoFromServer(String,ValueCallBack)}` 获取聊天室详情。
         *
         * \~english
         * The maximum number of members allowed in the chat room, which is determined during chat room creation.
         * 
         * To get the latest data, you can call `{@link IRoomManager#FetchRoomInfoFromServer(String,ValueCallBack)}` to get details of a chat room from the server. 
         */
        public int MaxUsers { get; internal set; }

        /**
         * \~chinese
         * 聊天室所有者。
         * 
         * 如需获取最新数据，可调用 `{@link IRoomManager#FetchRoomInfoFromServer(String,ValueCallBack)}` 获取聊天室详情。
         *
         * \~english
         * The chat room owner.
         * 
         * To get the latest data, you can call `{@link IRoomManager#FetchRoomInfoFromServer(String,ValueCallBack)}` to get details of a chat room from the server.
         */
        public string Owner { get; internal set; }

        /**
         * \~chinese
         * 是否开启全员禁言。
         * - `true`：开启。
         * - `false`：关闭。
         *  
         * **注意**
         * - 加入聊天室后，收到一键禁言/取消禁言的回调时，该状态会更新，此时为可靠状态。
         * - 从聊天室退出后再进入聊天室，该状态不可信。
         
         *
         * \~english
         * Whether all members are muted.
         * - `true`: Yes.  
         * - `false`: No.
         * 
         * **Note**
         * - Once all members are muted or unmuted, the callback is triggered to notify and update the mute or unmute status. You can call the method to get the current status.
         * - If you leave the chat room and rejoin it, the status is not reliable.
         * 
         */
        public bool IsAllMemberMuted { get; internal set; }

        /**
         * \~chinese
         * 当前用户在群组中的角色。
         *
         * \~english
         * The role of the current user in the chat room.
         */
        public RoomPermissionType PermissionType { get; internal set; }



        internal Room(string jsonString)
        {
            if (jsonString != null)
            {
                JSONNode jn = JSON.Parse(jsonString);

                if (!jn.IsNull && jn.IsObject)
                {
                    JSONObject jo = jn.AsObject;
                    RoomId = jo["roomId"].Value;
                    Name = jo["name"].Value;
                    Description = jo["desc"].Value;
                    Announcement = jo["announcement"].Value;
                    MemberCount = jo["memberCount"].AsInt;
                    AdminList = TransformTool.JsonStringToStringList(jo["adminList"].Value);
                    MemberList = TransformTool.JsonStringToStringList(jo["memberList"].Value);
                    BlockList = TransformTool.JsonStringToStringList(jo["blockList"].Value);
                    MuteList = TransformTool.JsonStringToStringList(jo["muteList"].Value);
                    MaxUsers = jo["maxUsers"].AsInt;
                    Owner = jo["owner"].Value;
                    IsAllMemberMuted = jo["isAllMemberMuted"].AsBool;
                    if (jo["permissionType"].AsInt == -1)
                    {
                        PermissionType = RoomPermissionType.None;
                    }
                    else if (jo["permissionType"].AsInt == 0)
                    {
                        PermissionType = RoomPermissionType.Member;
                    }
                    else if (jo["permissionType"].AsInt == 1)
                    {
                        PermissionType = RoomPermissionType.Admin;
                    }
                    else if (jo["permissionType"].AsInt == 2)
                    {
                        PermissionType = RoomPermissionType.Owner;
                    }
                }
            }
        }


        internal Room()
        {
            //default constructor
        }

    }
}