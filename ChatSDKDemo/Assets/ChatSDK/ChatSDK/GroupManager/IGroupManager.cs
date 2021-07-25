using System.Collections.Generic;

namespace ChatSDK
{
    public abstract class IGroupManager
    {
        /// <summary>
        /// 同意加群邀请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AcceptInvitationFromGroup(string groupId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 同意加群申请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="username">用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AcceptJoinApplication(string groupId, string username, CallBack handle = null);

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="memberId">用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AddAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 添加群成员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AddMembers(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 添加白名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void AddWhiteList(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 不接收群消息
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void BlockGroup(string groupId, CallBack handle = null);

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void BlockMembers(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 修改群描述
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="desc">新群描述</param>
        /// <param name="handle">执行结果</param>
        public abstract void ChangeGroupDescription(string groupId, string desc, CallBack handle = null);

        /// <summary>
        /// 修改群名称
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="name">新群名称</param>
        /// <param name="handle">执行结果</param>
        public abstract void ChangeGroupName(string groupId, string name, CallBack handle = null);

        /// <summary>
        /// 转移群
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="newOwner">新群主id</param>
        /// <param name="handle">执行结果</param>
        public abstract void ChangeGroupOwner(string groupId, string newOwner, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 检查自己是否在白名单中
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void CheckIfInGroupWhiteList(string groupId, ValueCallBack<bool> handle = null);

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="groupName">群名称</param>
        /// <param name="options">群配置</param>
        /// <param name="desc">群描述</param>
        /// <param name="inviteMembers">邀请的成员</param>
        /// <param name="inviteReason">邀请原因</param>
        /// <param name="handle">执行结果</param>
        public abstract void CreateGroup(string groupName, GroupOptions options, string desc = null, List<string> inviteMembers = null, string inviteReason = null, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 拒绝群邀请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="reason">原因</param>
        /// <param name="handle">执行结果</param>
        public abstract void DeclineInvitationFromGroup(string groupId, string reason = null, CallBack handle = null);

        /// <summary>
        /// 拒绝群申请
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="username">用户id</param>
        /// <param name="reason">原因</param>
        /// <param name="handle">执行结果</param>
        public abstract void DeclineJoinApplication(string groupId, string username, string reason = null, CallBack handle = null);

        /// <summary>
        /// 解散群
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void DestroyGroup(string groupId, CallBack handle = null);

        /// <summary>
        /// 下载群文件
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="fileId">文件id</param>
        /// <param name="savePath">存储路径</param>
        /// <param name="handle">执行结果</param>
        public abstract void DownloadGroupSharedFile(string groupId, string fileId, string savePath, CallBack handle = null);

        /// <summary>
        /// 获取群公告
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupAnnouncementFromServer(string groupId, ValueCallBack<string> handle = null);

        /// <summary>
        /// 获取黑名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupBlockListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 获取群共享文件
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupFileListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<GroupSharedFile>> handle = null);

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="cursor">游标</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupMemberListFromServer(string groupId, int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<string>> handle = null);

        /// <summary>
        /// 获取群禁言列表
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupMuteListFromServer(string groupId, int pageNum = 1, int pageSize = 200, ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 获取群详情
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupSpecificationFromServer(string groupId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 获取群白名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetGroupWhiteListFromServer(string groupId, ValueCallBack<List<string>> handle = null);

        /// <summary>
        /// 根据群id获取本地群
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <returns></returns>
        public abstract Group GetGroupWithId(string groupId);

        /// <summary>
        /// 从根本的获取加入的群列表
        /// </summary>
        /// <returns></returns>
        public abstract List<Group> GetJoinedGroups();

        /// <summary>
        /// 从服务器获取加入的群列表
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetJoinedGroupsFromServer(int pageNum = 1, int pageSize = 200,  ValueCallBack<List<Group>> handle = null);

        /// <summary>
        /// 获取公开群列表
        /// </summary>
        /// <param name="pageSize">每页数量</param>
        /// <param name="cursor">游标</param>
        /// <param name="handle">执行结果</param>
        public abstract void GetPublicGroupsFromServer(int pageSize = 200, string cursor = "", ValueCallBack<CursorResult<GroupInfo>> handle = null);

        /// <summary>
        /// 加入公开群
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void JoinPublicGroup(string groupId, CallBack handle = null);

        /// <summary>
        /// 离开群
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void LeaveGroup(string groupId, CallBack handle = null);

        /// <summary>
        /// 禁言所有群成员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void MuteAllMembers(string groupId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 禁言群成员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">群成员id</param>
        /// <param name="handle">执行结果</param>
        public abstract void MuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 删除群管理员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="memberId">群管理员id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveAdmin(string groupId, string memberId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 删除群共享文件
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="fileId">共享文件id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveGroupSharedFile(string groupId, string fileId, CallBack handle = null);

        /// <summary>
        /// 删除群成员
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">群成员id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveMembers(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 删除白名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">白名单用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void RemoveWhiteList(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 恢复接受群消息
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void UnBlockGroup(string groupId, CallBack handle = null);

        /// <summary>
        /// 解除群黑名单
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">黑名单用户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void UnBlockMembers(string groupId, List<string> members, CallBack handle = null);

        /// <summary>
        /// 解除群禁言
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="handle">执行结果</param>
        public abstract void UnMuteAllMembers(string groupId, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 解除用户禁言
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="members">禁言公户id</param>
        /// <param name="handle">执行结果</param>
        public abstract void UnMuteMembers(string groupId, List<string> members, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 更新群公告
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="announcement">新公告</param>
        /// <param name="handle">执行结果</param>
        public abstract void UpdateGroupAnnouncement(string groupId, string announcement, CallBack handle = null);

        /// <summary>
        /// 更新群扩展
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="ext">扩展信息</param>
        /// <param name="handle">执行结果</param>
        public abstract void UpdateGroupExt(string groupId, string ext, ValueCallBack<Group> handle = null);

        /// <summary>
        /// 上传群共享文件
        /// </summary>
        /// <param name="groupId">群组id</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="handle">执行结果</param>
        public abstract void UploadGroupSharedFile(string groupId, string filePath, CallBack handle = null);

        /// <summary>
        /// 添加群监听
        /// </summary>
        /// <param name="groupManagerDelegate">实现IGroupManagerDelegate监听的对象</param>
        public void AddGroupManagerDelegate(IGroupManagerDelegate groupManagerDelegate)
        {
            CallbackManager.Instance().groupManagerDelegates.Add(groupManagerDelegate);
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().groupManagerDelegates.Clear();
        }

    }

}