using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IRoomManager
    {
        internal WeakDelegater<IRoomManagerDelegate> Delegate = new WeakDelegater<IRoomManagerDelegate>();

        /// <summary>
        /// 加入聊天室
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void JoinRoom(string roomId, CallBack callBack = null);

        /// <summary>
        /// 离开聊天室
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void LeaveRoom(string roomId, CallBack callBack = null);

        /// <summary>
        /// 从服务器获取公共聊天室列表
        /// </summary>
        /// <param name="pageNum">页数</param>
        /// <param name="pageSize">每页返回个数</param>
        /// <param name="result">返回结果</param>
        public abstract void FetchPublicRoomsFromServer(int pageNum = 1, int pageSize = 200, ValueCallBack<PageResult<Room>> result = null);

        /// <summary>
        /// 从服务器获取聊天室详情
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="result">返回结果</param>
        public abstract void FetchRoomInfoFromServer(string roomId, ValueCallBack<Room> result = null);

        /// <summary>
        /// 更具聊天室Id构造聊天室对象
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <returns>聊天室</returns>
        public abstract Room GetChatRoomWithId(string roomId);

        /// <summary>
        /// 从本地获取已缓存的聊天室列表
        /// </summary>
        /// <returns>聊天室列表</returns>
        public abstract List<Room> GetAllRoomsFromLocal();

        /// <summary>
        /// 创建聊天室，需要服务器开通才能从客户端创建
        /// </summary>
        /// <param name="subject">聊天室名称</param>
        /// <param name="descriptionsc">聊天室描述</param>
        /// <param name="welcomeMsg">邀请信息</param>
        /// <param name="maxUserCount">聊天室人数上限</param>
        /// <param name="members">邀请列表</param>
        /// <param name="callBack">处理结果</param>
        public abstract void CreateRoom(string subject, string descriptionsc, string welcomeMsg, int maxUserCount = 300, List<string> members = null, ValueCallBack<Room> callBack = null);

        /// <summary>
        /// 销毁聊天室，拥有人可以操作，该APi需要服务器单独开通才能从客户端调用。
        /// </summary>
        /// <param name="roomId">要销毁的聊天室Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void DestroyRoom(string roomId, ValueCallBack<bool> callBack = null);

        /// <summary>
        /// 更改聊天室名称
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="newSubject">新名称</param>
        /// <param name="callBack">处理结果</param>
        public abstract void ChangeRoomSubject(string roomId, string newSubject, ValueCallBack<Room> callBack = null);

        /// <summary>
        /// 更改聊天室描述
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="newDescription">新描述</param>
        /// <param name="callBack">处理结果</param>
        public abstract void ChangeRoomDescription(string roomId, string newDescription, ValueCallBack<Room> callBack = null);

        /// <summary>
        /// 获取聊天室成员
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="cursor">游标</param>
        /// <param name="pageSize">请求数量</param>
        /// <param name="callBack">处理结果</param>
        public abstract void FetchRoomMembers(string roomId, string cursor = "", int pageSize = 200, ValueCallBack<CursorResult<Room>> callBack = null);

        /// <summary>
        /// 聊天室成员禁言，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="members">禁言人员Id</param>
        /// <param name="duration">禁言时长</param>
        /// <param name="callBack">处理结果</param>
        public abstract void MuteRoomMembers(string roomId, List<string> members, long duration = -1, CallBack callBack = null);

        /// <summary>
        /// 解除聊天室成员禁言，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="members">解除禁言成员Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void UnMuteRoomMembers(string roomId, List<string> members, CallBack callBack = null);

        /// <summary>
        /// 聊天室转移，只有拥有人可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="newOwner">聊天室接收人</param>
        /// <param name="callBack">处理结果</param>
        public abstract void ChangeOwner(string roomId, string newOwner, CallBack callBack = null);

        /// <summary>
        /// 添加聊天室管理员，只有聊天室拥有人可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="memberId">被操作成员Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void AddRoomAdmin(string roomId, string memberId, CallBack callBack = null);

        /// <summary>
        /// 移除聊天室管理员，只有聊天室拥有人可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="adminId">被操作管理员Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void RemoveRoomAdmin(string roomId, string adminId, CallBack callBack = null);

        /// <summary>
        /// 获取聊天室禁言列表
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="pageSize">每次请求返回数量</param>
        /// <param name="pageNum">请求页数</param>
        /// <param name="callBack">返回结果</param>
        public abstract void FetchRoomMuteList(string roomId, int pageSize = 1, int pageNum = 200, ValueCallBack<List<string>> callBack = null);

        /// <summary>
        /// 移除聊天室成员，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="members">被移除用户列表</param>
        /// <param name="callBack">处理结果</param>
        public abstract void RemoveRoomMembers(string roomId, List<string> members, CallBack callBack = null);

        /// <summary>
        /// 限制用户加入聊天室，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="members">被限制成员列表</param>
        /// <param name="callBack">处理结果</param>
        public abstract void BlockRoomMembers(string roomId, List<string> members, CallBack callBack = null);

        /// <summary>
        /// 解除被限制用户加入聊天室，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="members">被解除成员列表</param>
        /// <param name="callBack">处理结果</param>
        public abstract void UnBlockRoomMembers(string roomId, List<string> members, CallBack callBack = null);

        /// <summary>
        /// 获取被限制进入聊天室的用户列表
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="pageSize">每次请求返回数量</param>
        /// <param name="pageNum">请求页数</param>
        /// <param name="callBack">返回结果</param>
        public abstract void FetchRoomBlockList(string roomId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> callBack = null);

        /// <summary>
        /// 更新聊天室公告，只有聊天室拥有人和管理员可以操作
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="announcement">新公告内容</param>
        /// <param name="callBack">处理结果</param>
        public abstract void UpdateRoomAnnouncement(string roomId, string announcement, CallBack callBack = null);

        /// <summary>
        /// 获取聊天室公告
        /// </summary>
        /// <param name="roomId">聊天室Id</param>
        /// <param name="callBack">处理结果</param>
        public abstract void FetchRoomAnnouncement(string roomId, ValueCallBack<string> callBack);

        /// <summary>
        /// 添加聊天室事件监听
        /// </summary>
        /// <param name="roomManagerDelegate"></param>
        public void AddRoomManagerDelegate(IRoomManagerDelegate roomManagerDelegate)
        {
            Delegate.Add(roomManagerDelegate);
        }

        internal void ClearDelegates()
        {
            Delegate.Clear();
        }
    }
}