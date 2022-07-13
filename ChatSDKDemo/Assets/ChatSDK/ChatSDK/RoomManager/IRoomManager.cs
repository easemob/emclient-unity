using System.Collections.Generic;

namespace ChatSDK
{

    /// <summary>
    /// 聊天室管理类
    /// </summary>
    public abstract class IRoomManager
    {

        /// <summary>
        /// 添加管理员，群主可以操作
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="memberId">管理员id</param>
        /// <param name="handle">结果回调</param>
        public abstract void AddRoomAdmin(string roomId, string memberId, CallBack handle = null);

        /// <summary>
        /// 将用户添加到黑名单
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="members">黑名单id</param>
        /// <param name="handle">结果回调</param>
        public abstract void BlockRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 转移聊天室
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="newOwner">新创建者id</param>
        /// <param name="handle">结果回调</param>
        public abstract void ChangeRoomOwner(string roomId, string newOwner, CallBack handle = null);

        /// <summary>
        /// 修改聊天室描述
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="newDescription">新描述</param>
        /// <param name="handle">结果回调</param>
        public abstract void ChangeRoomDescription(string roomId, string newDescription, CallBack handle = null);

        /// <summary>
        /// 修改聊天室名称
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="newName">新名称</param>
        /// <param name="handle">结果回调</param>
        public abstract void ChangeRoomName(string roomId, string newName, CallBack handle = null);

        /// <summary>
        /// 创建聊天室
        /// </summary>
        /// <param name="subject">名称</param>
        /// <param name="descriptions">描述</param>
        /// <param name="welcomeMsg">欢迎信息</param>
        /// <param name="maxUserCount">用户上限</param>
        /// <param name="members">邀请用户</param>
        /// <param name="handle">结果回调</param>
        public abstract void CreateRoom(string name, string descriptions = null, string welcomeMsg = null, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> handle = null);

        /// <summary>
        /// 销毁聊天室
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="handle">结果回调</param>
        public abstract void DestroyRoom(string roomId, CallBack handle = null);

        /// <summary>
        /// 获取聊天室列表
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> handle = null);

        /// <summary>
        /// 获取聊天室公告
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchRoomAnnouncement(string roomId, ValueCallBack<string> handle = null);

        /// <summary>
        /// 获取聊天室黑名单
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 获取聊天室详情
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> handle = null);

        /// <summary>
        /// 获取聊天室成员
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="cursor">游标</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<string>> handle = null);

        /// <summary>
        /// 获取聊天室禁言列表
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="pageNum">页码</param>
        /// <param name="handle">结果回调</param>
        public abstract void FetchRoomMuteList(string roomId, int pageSize = 200, int pageNum = 1, ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="handle">结果回调</param>
        public abstract void JoinRoom(string roomId, ValueCallBack<Room> handle = null);

        /// <summary>
        /// 离开聊天室
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="handle">结果回调</param>
        public abstract void LeaveRoom(string roomId, CallBack handle = null);

        /// <summary>
        /// 禁言聊天室成员
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="members">禁言用户</param>
        /// <param name="handle">结果回调</param>
        public abstract void MuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 移除聊天室管理员
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="adminId">管理员id</param>
        /// <param name="handle">结果回调</param>
        public abstract void RemoveRoomAdmin(string roomId, string adminId, CallBack handle = null);

        /// <summary>
        /// 移除聊天室成员
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="members">成员id</param>
        /// <param name="handle">结果回调</param>
        public abstract void DeleteRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 移除黑名单
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="members">黑名单id</param>
        /// <param name="handle">结果回调</param>
        public abstract void UnBlockRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 移除禁言列表
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="members">用户id</param>
        /// <param name="handle">结果回调</param>
        public abstract void UnMuteRoomMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 更新聊天室公告
        /// </summary>
        /// <param name="roomId">聊天室id</param>
        /// <param name="announcement"></param>
        /// <param name="handle">结果回调</param>
        public abstract void UpdateRoomAnnouncement(string roomId, string announcement, CallBack handle = null);

        public abstract void MuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null);

        public abstract void UnMuteAllRoomMembers(string roomId, ValueCallBack<Room> handle = null);

        public abstract void AddWhiteListMembers(string roomId, List<string> members, CallBack handle = null);

        public abstract void RemoveWhiteListMembers(string roomId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 添加聊天室监听
        /// </summary>
        /// <param name="roomManagerDelegate"></param>
        public void AddRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)
        {
            if (!CallbackManager.Instance().roomManagerListener.delegater.Contains(roomManagerDelegate))
            {
                CallbackManager.Instance().roomManagerListener.delegater.Add(roomManagerDelegate);
            }
        }

        /// <summary>
        /// 移除聊天室监听
        /// </summary>
        /// <param name="roomManagerDelegate"></param>
        public void RemoveRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().roomManagerListener.delegater.Contains(roomManagerDelegate))
            {
                CallbackManager.Instance().roomManagerListener.delegater.Remove(roomManagerDelegate);
            }            
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().roomManagerListener.delegater.Clear();
        }
    }
}