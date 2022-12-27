
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

    public void OnConnected()
    {
        UIManager.DefaultAlert(transform, "OnConnected");
    }

    public void OnDisconnected()
    {
        UIManager.DefaultAlert(transform, "OnDisconnected");
    }

    public void OnLoggedOtherDevice()
    {
        UIManager.DefaultAlert(transform, "OnLoggedOtherDevice");
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

    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        throw new System.NotImplementedException();
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        throw new System.NotImplementedException();
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
        throw new System.NotImplementedException();
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        throw new System.NotImplementedException();
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        throw new System.NotImplementedException();
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        throw new System.NotImplementedException();
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        throw new System.NotImplementedException();
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        throw new System.NotImplementedException();
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatroomAttributesRemoved(string roomId, List<string> keys, string from)
    {
        throw new System.NotImplementedException();
    }

    public void OnSpecificationChangedFromRoom(Room room)
    {
        throw new System.NotImplementedException();
    }

    public void OnAddAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemoveAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        throw new System.NotImplementedException();
    }

    public void OnAllMemberMuteChangedFromChatroom(string roomId, bool isAllMuted)
    {
        throw new System.NotImplementedException();
    }

    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {
        throw new System.NotImplementedException();
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {
        throw new System.NotImplementedException();
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        throw new System.NotImplementedException();
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        throw new System.NotImplementedException();
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        throw new System.NotImplementedException();
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire)
    {
        throw new System.NotImplementedException();
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        throw new System.NotImplementedException();
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        throw new System.NotImplementedException();
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string reason)
    {
    }
    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        throw new System.NotImplementedException();
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {
    }
    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        throw new System.NotImplementedException();
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        throw new System.NotImplementedException();
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        throw new System.NotImplementedException();
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        throw new System.NotImplementedException();
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        throw new System.NotImplementedException();
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        throw new System.NotImplementedException();
    }

    public void OnAddAllowListMembersFromGroup(string groupId, List<string> allowList)
    {
        throw new System.NotImplementedException();
    }

    public void OnRemoveAllowListMembersFromGroup(string groupId, List<string> allowList)
    {
        throw new System.NotImplementedException();
    }

    public void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted)
    {
        throw new System.NotImplementedException();
    }

    public void OnStateChangedFromGroup(string groupId, bool isDisable)
    {
        throw new System.NotImplementedException();
    }

    public void OnSpecificationChangedFromGroup(Group group)
    {
        throw new System.NotImplementedException();
    }

    public void OnPresenceUpdated(List<Presence> presences)
    {
        throw new System.NotImplementedException();
    }

    public void OnContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext)
    {
        throw new System.NotImplementedException();
    }

    public void OnGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        throw new System.NotImplementedException();
    }

    public void OnUndisturbMultiDevicesEvent(string data)
    {
        throw new System.NotImplementedException();
    }

    public void OnThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        throw new System.NotImplementedException();
    }

    public void OnContactAdded(string userId)
    {
        throw new System.NotImplementedException();
    }

    public void OnContactDeleted(string userId)
    {
        throw new System.NotImplementedException();
    }

    public void OnContactInvited(string userId, string reason)
    {
        throw new System.NotImplementedException();
    }

    public void OnFriendRequestAccepted(string userId)
    {
        throw new System.NotImplementedException();
    }

    public void OnFriendRequestDeclined(string userId)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatThreadCreate(ChatThreadEvent threadEvent)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatThreadUpdate(ChatThreadEvent threadEvent)
    {
        throw new System.NotImplementedException();
    }

    public void OnChatThreadDestroy(ChatThreadEvent threadEvent)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent)
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnLoggedOtherDevice()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnRemovedFromServer()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnForbidByServer()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnChangedIMPwd()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnLoginTooManyDevice()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnKickedByOtherDevice()
    {
        throw new System.NotImplementedException();
    }

    void IConnectionDelegate.OnAuthFailed()
    {
        throw new System.NotImplementedException();
    }

    void IRoomManagerDelegate.OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
        throw new System.NotImplementedException();
    }

    void IGroupManagerDelegate.OnStateChangedFromGroup(string groupId, bool isDisable)
    {
        throw new System.NotImplementedException();
    }

    void IGroupManagerDelegate.OnSpecificationChangedFromGroup(Group group)
    {
        throw new System.NotImplementedException();
    }
}
