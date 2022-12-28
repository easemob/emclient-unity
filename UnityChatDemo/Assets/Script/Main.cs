
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;
using System.Collections.Generic;

public class Main : MonoBehaviour, IConnectionDelegate, IChatManagerDelegate, IRoomManagerDelegate, IGroupManagerDelegate, IPresenceManagerDelegate, IMultiDeviceDelegate, IContactManagerDelegate, IChatThreadManagerDelegate
{
    // Start is called before the first frame update

    private Button ChatBtn;
    private Button ContactBtn;
    private Button ConversationBtn;
    private Button GroupBtn;
    private Button RoomBtn;
    private Button PushBtn;
    private Button PresenceBtn;
    private Button ThreadBtn;
    private Button isConnectedBtn;
    private Button isLoggedBtn;
    private Button CurrentUsernameBtn;
    private Button AccessTokenBtn;
    private Button LogoutBtn;
    private Button m_NewTokenBtn;


    private void Awake()
    {

        Debug.Log("main script has load");

        ChatBtn = transform.Find("Panel/ChatBtn").GetComponent<Button>();
        ContactBtn = transform.Find("Panel/ContactBtn").GetComponent<Button>();
        ConversationBtn = transform.Find("Panel/ConversationBtn").GetComponent<Button>();
        GroupBtn = transform.Find("Panel/GroupBtn").GetComponent<Button>();
        RoomBtn = transform.Find("Panel/RoomBtn").GetComponent<Button>();
        PushBtn = transform.Find("Panel/PushBtn").GetComponent<Button>();
        PresenceBtn = transform.Find("Panel/PresenceBtn").GetComponent<Button>();
        ThreadBtn = transform.Find("Panel/ThreadBtn").GetComponent<Button>();
        isConnectedBtn = transform.Find("Panel/Panel/IsConnectBtn").GetComponent<Button>();
        isLoggedBtn = transform.Find("Panel/Panel/IsLoggedBtn").GetComponent<Button>();
        CurrentUsernameBtn = transform.Find("Panel/Panel/CurrentUsernameBtn").GetComponent<Button>();
        AccessTokenBtn = transform.Find("Panel/Panel/AccessTokenBtn").GetComponent<Button>();
        LogoutBtn = transform.Find("Panel/LogoutBtn").GetComponent<Button>();
        m_NewTokenBtn = transform.Find("Panel/NewTokenBtn").GetComponent<Button>();

        ChatBtn.onClick.AddListener(ChatBtnAction);
        ContactBtn.onClick.AddListener(ContactBtnAction);
        ConversationBtn.onClick.AddListener(ConversationBtnAction);
        GroupBtn.onClick.AddListener(GroupBtnAction);
        RoomBtn.onClick.AddListener(RoomBtnAction);
        PushBtn.onClick.AddListener(PushBtnAction);
        PresenceBtn.onClick.AddListener(PresenceBtnAction);
        ThreadBtn.onClick.AddListener(ThreadBtnAction);
        isConnectedBtn.onClick.AddListener(isConnectedBtnAction);
        isLoggedBtn.onClick.AddListener(isLoggedBtnAction);
        CurrentUsernameBtn.onClick.AddListener(CurrentUsernameBtnAction);
        AccessTokenBtn.onClick.AddListener(AccessTokenBtnAction);
        LogoutBtn.onClick.AddListener(LogoutBtnAction);
        m_NewTokenBtn.onClick.AddListener(NewTokenAction);

        SDKClient.Instance.AddConnectionDelegate(this);
        SDKClient.Instance.AddMultiDeviceDelegate(this);

        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
        SDKClient.Instance.PresenceManager.AddPresenceManagerDelegate(this);
        SDKClient.Instance.ThreadManager.AddThreadManagerDelegate(this);
    }

    private void OnDestroy()
    {
        SDKClient.Instance.DeleteConnectionDelegate(this);
        SDKClient.Instance.DeleteMultiDeviceDelegate(this);

        SDKClient.Instance.ChatManager.RemoveChatManagerDelegate(this);
        SDKClient.Instance.ContactManager.RemoveContactManagerDelegate(this);
        SDKClient.Instance.GroupManager.RemoveGroupManagerDelegate(this);
        SDKClient.Instance.RoomManager.RemoveRoomManagerDelegate(this);
        SDKClient.Instance.PresenceManager.RemovePresenceManagerDelegate(this);
        SDKClient.Instance.ThreadManager.RemoveThreadManagerDelegate(this);
    }

    void ChatBtnAction()
    {
        SceneManager.LoadSceneAsync("ChatManager");
    }

    void ContactBtnAction()
    {
        SceneManager.LoadSceneAsync("ContactManager");
    }

    void ConversationBtnAction()
    {
        SceneManager.LoadSceneAsync("ConversationManager");
    }

    void GroupBtnAction()
    {
        SceneManager.LoadSceneAsync("GroupManager");
    }

    void RoomBtnAction()
    {
        SceneManager.LoadSceneAsync("RoomManager");
    }

    void PushBtnAction()
    {
        SceneManager.LoadSceneAsync("PushManager");
    }

    void PresenceBtnAction()
    {
        SceneManager.LoadSceneAsync("PresenceManager");
    }
    void ThreadBtnAction()
    {
        SceneManager.LoadSceneAsync("ThreadManager");
    }
    void isConnectedBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.IsConnected ? "已连接" : "未连接");
    }

    void isLoggedBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.IsLoggedIn ? "已登录" : "未登录");
    }

    void CurrentUsernameBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.CurrentUsername);
    }

    void AccessTokenBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.AccessToken);
    }

    void LogoutBtnAction()
    {
        SDKClient.Instance.Logout(false);
        SceneManager.LoadSceneAsync("Login");
    }

    void NewTokenAction()
    {
        string token = "12345";

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE

        /*
        //Read token from file
        FileOperator foper = FileOperator.GetInstance();
        string tokenFromFile = foper.ReadData(foper.GetTokenConfFile()); // should be only one element

        if (tokenFromFile.Length == 0)
        {
            UIManager.DefaultAlert(transform, "Empty agora token!");
            return;
        }
        else
        {
            token = tokenFromFile;
        }
        */
#endif

        SDKClient.Instance.RenewAgoraToken(token);
        UIManager.DefaultAlert(transform, "Renew agora token complete.");
    }

    void Start()
    {
        SDKClient.Instance.AddConnectionDelegate(this);
        SDKClient.Instance.AddMultiDeviceDelegate(this);

        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);


        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 收到群组邀请
    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {
        UIManager.TitleAlert(transform, "回调 收到群组邀请", $"groupId: {groupId}",
            () =>
            {
                SDKClient.Instance.GroupManager.AcceptGroupInvitation(groupId, new ValueCallBack<Group>(
                    onSuccess: (group) =>
                    {
                        UIManager.SuccessAlert(transform);
                    },
                    onError: (code, desc) =>
                    {
                        UIManager.ErrorAlert(transform, code, desc);
                    }
                ));
            },
            () =>
            {
                SDKClient.Instance.GroupManager.DeclineGroupInvitation(groupId, callback: new CallBack(
                    onSuccess: () =>
                    {
                        UIManager.SuccessAlert(transform);
                    },
                    onError: (code, desc) =>
                    {
                        UIManager.ErrorAlert(transform, code, desc);
                    }
                ));
            },
            "同意",
            "拒绝"
        );
    }

    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {
        UIManager.TitleAlert(transform, "回调 收到加群申请", $"groupId: {groupId}, user: {applicant}",
            () =>
            {
                SDKClient.Instance.GroupManager.AcceptGroupJoinApplication(groupId, applicant, new CallBack(
                    onSuccess: () =>
                    {
                        UIManager.SuccessAlert(transform);
                    },
                    onError: (code, desc) =>
                    {
                        UIManager.ErrorAlert(transform, code, desc);
                    }
                ));
            },
            () =>
            {
                SDKClient.Instance.GroupManager.DeclineGroupJoinApplication(groupId, applicant, callback: new CallBack(
                    onSuccess: () =>
                    {
                        UIManager.SuccessAlert(transform);
                    },
                    onError: (code, desc) =>
                    {
                        UIManager.ErrorAlert(transform, code, desc);
                    }
                ));
            },
            "同意",
            "拒绝"
        );
    }

    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {
        UIManager.DefaultAlert(transform, $"回调 加群申请已同意,groupId: {groupId}");
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string reason)
    {
        UIManager.DefaultAlert(transform, $"回调 加群申请被拒绝:{groupId} :{reason}");
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}邀请被{invitee}同意");
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}邀请被{invitee}拒绝");
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        UIManager.DefaultAlert(transform, $"回调 被{groupId}移除");
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        UIManager.DefaultAlert(transform, $"回调 群组{groupId}已销毁");
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        UIManager.DefaultAlert(transform, $"回调 自动同意群组{groupId}邀请，邀请人{inviter}");
    }


    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}禁言列表移除");
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"{groupId}: {administrator}");
        UIManager.DefaultAlert(transform, $"回调 {groupId}管理员列表添加{administrator}");
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"{groupId}: {administrator}");
        UIManager.DefaultAlert(transform, $"回调 {groupId}管理员列表移除{administrator}");
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}群主由{oldOwner}变为{newOwner}");
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        UIManager.DefaultAlert(transform, $"回调 {member}加入群组{groupId}");
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        UIManager.DefaultAlert(transform, $"回调 {member}离开群组{groupId}");
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}群组公告变更{announcement}");
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}群组共享文件增加");
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}群组共享文件被移除");
    }

    public void OnAddAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        string str = "";
        if (whiteList.Count > 0)
            str = string.Join(",", whiteList.ToArray());
        UIManager.DefaultAlert(transform, $"回调 {groupId} 添加成员至白名单{str}");
    }

    public void OnRemoveAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        string str = "";
        if (whiteList.Count > 0)
            str = string.Join(",", whiteList.ToArray());
        UIManager.DefaultAlert(transform, $"回调 {groupId} 将成员移出白名单{str}");
    }

    public void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId} 所有群成员禁言变化{isAllMuted}");
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire)
    {
        UIManager.DefaultAlert(transform, $"回调 {groupId}禁言列表添加");
    }

    public void OnStateChangedFromGroup(string groupId, bool isDisable)
    {
        UIManager.DefaultAlert(transform, $"回调OnStateChangedFromGroup {groupId},{isDisable}");
    }

    public void OnSpecificationChangedFromGroup(Group group)
    {
        UIManager.DefaultAlert(transform, $"回调 OnSpecificationChangedFromGroup {group.GroupId}");
    }


    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnDestroyedFromRoom: {roomId} , {roomName}");
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnMemberJoinedFromRoom: {roomId} , {participant}");
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnMemberExitedFromRoom: {roomId} , {roomName}, {participant}");
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

        Debug.Log($"roomId: {roomId}, name:{roomName}, participant:{participant}, transfrom: {this.transform}");

        UIManager.DefaultAlert(this.transform, $"回调 OnRemovedFromRoom: {roomId} , {roomName ?? ""}, {participant}");
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {

        string str = string.Join(",", mutes.ToArray());

        UIManager.DefaultAlert(this.transform, $"回调 OnMuteListAddedFromRoom: {roomId} , {str}");
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        string str = string.Join(",", mutes.ToArray());

        UIManager.DefaultAlert(this.transform, $"回调 OnMuteListRemovedFromRoom: {roomId} , {str}");
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAdminAddedFromRoom: {roomId} , {admin}");
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAdminRemovedFromRoom: {roomId} , {admin}");
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnOwnerChangedFromRoom: {roomId} , {newOwner}, {oldOwner}");
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAnnouncementChangedFromRoom: {roomId} , {announcement}");
    }

    public void OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnChatroomAttributesChanged: {roomId} , {from}");
    }

    public void OnChatroomAttributesRemoved(string roomId, List<string> keys, string from)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnChatroomAttributesRemoved: {roomId} , {from}");
    }

    public void OnSpecificationChangedFromRoom(Room room)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnSpecificationChangedFromRoom: {room.RoomId}");
    }

    public void OnAddAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAddAllowListMembersFromChatroom: {roomId}");
    }

    public void OnRemoveAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnRemoveAllowListMembersFromChatroom: {roomId}");
    }

    public void OnAllMemberMuteChangedFromChatroom(string roomId, bool isAllMuted)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAllMemberMuteChangedFromChatroom: {roomId} , {isAllMuted}");
    }

    public void OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnRemoveFromRoomByOffline: {roomId} , {roomName}");
    }

    public void OnPresenceUpdated(List<Presence> presences)
    {
        throw new System.NotImplementedException();
    }

    public void OnMessagesReceived(List<Message> messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnMessagesRead(List<Message> messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        throw new System.NotImplementedException();
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        throw new System.NotImplementedException();
    }

    public void OnConversationsUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnConversationRead(string from, string to)
    {
        throw new System.NotImplementedException();
    }

    public void MessageReactionDidChange(List<MessageReactionChange> list)
    {
        throw new System.NotImplementedException();
    }

    public void OnContactAdded(string username)
    {
        UIManager.DefaultAlert(transform, $"OnContactAdded: {username}");
    }

    public void OnContactDeleted(string username)
    {
        UIManager.DefaultAlert(transform, $"OnContactDeleted: {username}");
    }

    public void OnContactInvited(string username, string reason)
    {
        CallBack callBack = new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        );

        UIManager.TitleAlert(transform, $"收到好友申请", $"{username}添加您为好友",
            () => { SDKClient.Instance.ContactManager.AcceptInvitation(username, callBack); },
            () => { SDKClient.Instance.ContactManager.DeclineInvitation(username, callBack); },
            "同意",
            "拒绝"
        );
    }

    public void OnFriendRequestAccepted(string username)
    {
        UIManager.DefaultAlert(transform, $"OnFriendRequestAccepted: {username}");
    }

    public void OnFriendRequestDeclined(string username)
    {
        UIManager.DefaultAlert(transform, $"OnFriendRequestDeclined: {username}");
    }
    public void OnChatThreadCreate(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"OnChatThreadCreate: {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadUpdate(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"OnChatThreadUpdate: {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadDestroy(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"OnChatThreadDestroy: {threadEvent.ToJsonObject()}");
    }

    public void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"OnUserKickOutOfChatThread: {threadEvent.ToJsonObject()}");
    }

    public void OnContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext)
    {
        UIManager.DefaultAlert(transform, $"OnContactMultiDevicesEvent: {operation.ToInt()} : {target} : {ext}");
    }

    public void OnGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        UIManager.DefaultAlert(transform, $"OnGroupMultiDevicesEvent: {operation.ToInt()} : {target} : {string.Join(",", usernames)}");
    }

    public void OnUndisturbMultiDevicesEvent(string data)
    {
        UIManager.DefaultAlert(transform, $"OnUndisturbMultiDevicesEvent: {data}");
    }

    public void OnThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnLoggedOtherDevice()
    {
        throw new System.NotImplementedException();
    }

    public void OnRemovedFromServer()
    {
        throw new System.NotImplementedException();
    }

    public void OnForbidByServer()
    {
        throw new System.NotImplementedException();
    }

    public void OnChangedIMPwd()
    {
        throw new System.NotImplementedException();
    }

    public void OnLoginTooManyDevice()
    {
        throw new System.NotImplementedException();
    }

    public void OnKickedByOtherDevice()
    {
        throw new System.NotImplementedException();
    }

    public void OnAuthFailed()
    {
        throw new System.NotImplementedException();
    }

    public void OnTokenExpired()
    {
        throw new System.NotImplementedException();
    }

    public void OnTokenWillExpire()
    {
        throw new System.NotImplementedException();
    }
}
