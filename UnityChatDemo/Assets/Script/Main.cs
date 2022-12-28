
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
        Debug.Log($"OnRequestToJoinAcceptedFromGroup groupId: {groupId}, groupName: {groupName}, accepter: {accepter}");
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string reason)
    {
        Debug.Log($"OnRequestToJoinDeclinedFromGroup groupId: {groupId}, reason: {reason}");
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {
        Debug.Log($"OnInvitationAcceptedFromGroup groupId: {groupId}, invitee: {invitee}");
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log($"OnInvitationDeclinedFromGroup groupId: {groupId}, invitee: {invitee}, reason: {reason}");
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        Debug.Log($"OnUserRemovedFromGroup groupId: {groupId}, groupName: {groupName}");
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        Debug.Log($"OnDestroyedFromGroup groupId: {groupId}, groupName: {groupName}");
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log($"OnAutoAcceptInvitationFromGroup groupId: {groupId}, inviter: {inviter}, inviteMessage: {inviteMessage}");
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        Debug.Log($"OnMuteListRemovedFromGroup groupId: {groupId}, mutes: {string.Join(", ", mutes.ToArray())}");
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"OnAdminAddedFromGroup groupId: {groupId}, administrator: {administrator}");
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"OnAdminRemovedFromGroup groupId: {groupId}, administrator: {administrator}");
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log($"OnMemberJoinedFromGroup groupId: {groupId}, newOwner: {newOwner}, oldOwner: {oldOwner}");
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        Debug.Log($"OnMemberJoinedFromGroup groupId: {groupId}, member: {member}");
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        Debug.Log($"OnMemberExitedFromGroup groupId: {groupId}, member: {member}");
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        Debug.Log($"OnAnnouncementChangedFromGroup groupId: {groupId}, announcement: {announcement}");
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log($"OnSharedFileAddedFromGroup groupId: {groupId}, sharedFile: {sharedFile.ToJsonObject()}");
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        Debug.Log($"OnSharedFileDeletedFromGroup groupId: {groupId}, fileId: {fileId}");
    }

    public void OnAddAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        Debug.Log($"OnAddAllowListMembersFromGroup groupId: {groupId}, whiteList: {string.Join(", ", whiteList.ToArray())}");
    }

    public void OnRemoveAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        Debug.Log($"OnRemoveAllowListMembersFromGroup groupId: {groupId}, whiteList: {string.Join(", ", whiteList.ToArray())}");
    }

    public void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted)
    {
        Debug.Log($"OnStateChangedFromGroup groupId: {groupId}, isAllMuted:{isAllMuted}");
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire)
    {
        Debug.Log($"OnMuteListAddedFromGroup groupId: {groupId}, mutes: {string.Join(",", mutes.ToArray())}, expireTime: {muteExpire}");
    }

    public void OnStateChangedFromGroup(string groupId, bool isDisable)
    {
        Debug.Log($"OnStateChangedFromGroup groupId: {groupId}, isDisable:{isDisable}");
    }

    public void OnSpecificationChangedFromGroup(Group group)
    {
        Debug.Log($"OnSpecificationChangedFromGroup group: {group.ToJsonObject()}");
    }


    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        Debug.Log($"OnDestroyedFromRoom roomId: {roomId}, roomName:{roomName}");
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        Debug.Log($"OnMemberJoinedFromRoom roomId: {roomId}, participant:{participant}");
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log($"OnMemberExitedFromRoom roomId: {roomId}, name:{roomName}, participant:{participant}");
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

        Debug.Log($"OnRemovedFromRoom roomId: {roomId}, name: {roomName}, participant: {participant}");
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        Debug.Log($"OnMuteListAddedFromRoom roomId: {roomId}, mutes: {string.Join(", ", mutes.ToArray())}, expireTime: {expireTime}");
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        Debug.Log($"OnMuteListRemovedFromRoom roomId: {roomId}, mutes: {string.Join(", ", mutes.ToArray())}");
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        Debug.Log($"OnAdminAddedFromRoom roomId: {roomId}, admin: {admin}");
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        Debug.Log($"OnAdminRemovedFromRoom roomId: {roomId}, admin: {admin}");
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log($"OnOwnerChangedFromRoom roomId: {roomId}, newOwner: {newOwner}, oldOwner: {oldOwner}");
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        Debug.Log($"OnAnnouncementChangedFromRoom roomId: {roomId}, announcement: {announcement}");
    }

    public void OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from)
    {
        List<string> list = new List<string>();

        foreach (var key in kv.Keys)
        {
            list.Add($"{key}:kv[key]");
        }

        Debug.Log($"OnChatroomAttributesChanged roomId: {roomId}, keys: {string.Join(", ", list.ToArray())}, from: {from}");
    }

    public void OnChatroomAttributesRemoved(string roomId, List<string> keys, string from)
    {
        Debug.Log($"OnSpecificationChangedFromRoom roomId: {roomId}, keys: {string.Join(", ", keys.ToArray())}, from: {from}");
    }

    public void OnSpecificationChangedFromRoom(Room room)
    {
        Debug.Log($"OnSpecificationChangedFromRoom room: {room.ToJsonObject()}");
    }

    public void OnAddAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        Debug.Log($"OnAddAllowListMembersFromChatroom roomId: {roomId}, members: {string.Join(", ", members.ToArray())}");
    }

    public void OnRemoveAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        Debug.Log($"OnRemoveAllowListMembersFromChatroom roomId: {roomId}, members: {string.Join(", ", members.ToArray())}");
    }

    public void OnAllMemberMuteChangedFromChatroom(string roomId, bool isAllMuted)
    {
        Debug.Log($"OnAllMemberMuteChangedFromChatroom roomId: {roomId}, isAllMuted: {isAllMuted}");
    }

    public void OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
        Debug.Log($"OnRemoveFromRoomByOffline roomId: {roomId}, roomName: {roomName}");
    }

    public void OnPresenceUpdated(List<Presence> presences)
    {
        List<string> list = new List<string>();
        foreach (var presence in presences)
        {
            list.Add(presence.ToJsonObject().ToString());
        }
        Debug.Log($"OnPresenceUpdated {string.Join(", ", list.ToArray())}");
    }

    public void OnMessagesReceived(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"OnMessagesReceived {string.Join(", ", msgs.ToArray())}");
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"OnCmdMessagesReceived {string.Join(", ", msgs.ToArray())}");
    }

    public void OnMessagesRead(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"OnMessagesRead {string.Join(", ", msgs.ToArray())}");
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"OnMessagesDelivered {string.Join(", ", msgs.ToArray())}");
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"OnMessagesRecalled {string.Join(", ", msgs.ToArray())}");
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        Debug.Log($"OnReadAckForGroupMessageUpdated");
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        List<string> acks = new List<string>();
        foreach (var ack in list)
        {
            acks.Add(ack.ToJsonObject().ToString());
        }
        Debug.Log($"OnGroupMessageRead {string.Join(", ", acks.ToArray())}");
    }

    public void OnConversationsUpdate()
    {
        Debug.Log($"OnConversationsUpdate");
    }

    public void OnConversationRead(string from, string to)
    {
        Debug.Log($"OnConversationRead from:{from}, to:{to}");
    }

    public void MessageReactionDidChange(List<MessageReactionChange> list)
    {
        List<string> changes = new List<string>();
        foreach (var change in list)
        {
            changes.Add(change.ToJsonObject().ToString());
        }
        Debug.Log($"OnContactAdded {string.Join(", ", changes.ToArray())}");
    }

    public void OnContactAdded(string username)
    {
        Debug.Log($"OnContactAdded {username}");
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log($"OnContactDeleted {username}");
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
        Debug.Log($"OnFriendRequestAccepted {username}");
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log($"OnFriendRequestDeclined {username}");
    }
    public void OnChatThreadCreate(ChatThreadEvent threadEvent)
    {
        Debug.Log($"OnChatThreadCreate {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadUpdate(ChatThreadEvent threadEvent)
    {
        Debug.Log($"OnChatThreadUpdate {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadDestroy(ChatThreadEvent threadEvent)
    {
        Debug.Log($"OnChatThreadDestroy {threadEvent.ToJsonObject()}");
    }

    public void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent)
    {
        Debug.Log($"OnUserKickOutOfChatThread {threadEvent.ToJsonObject()}");
    }

    public void OnContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext)
    {
        Debug.Log($"OnContactMultiDevicesEvent {operation}: {target}: {ext}");
    }

    public void OnGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        Debug.Log($"OnGroupMultiDevicesEvent {operation}: {target}: {string.Join(", ", usernames.ToArray())}");
    }

    public void OnUndisturbMultiDevicesEvent(string data)
    {
        Debug.Log($"OnUndisturbMultiDevicesEvent {data}");
    }

    public void OnThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        Debug.Log($"OnThreadMultiDevicesEvent {operation}: {target}: {string.Join(", ", usernames.ToArray())}");
    }

    public void OnConnected()
    {
        Debug.Log("OnConnected run");
    }

    public void OnDisconnected()
    {
        Debug.Log("OnDisconnected run");
    }

    public void OnLoggedOtherDevice()
    {
        Debug.Log("OnLoggedOtherDevice run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnRemovedFromServer()
    {
        Debug.Log("OnRemovedFromServer run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnForbidByServer()
    {
        Debug.Log("OnForbidByServer run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnChangedIMPwd()
    {
        Debug.Log("OnChangedIMPwd run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnLoginTooManyDevice()
    {
        Debug.Log("OnLoginTooManyDevice run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnKickedByOtherDevice()
    {
        Debug.Log("OnKickedByOtherDevice run");
        SceneManager.LoadSceneAsync("Login");
    }

    public void OnAuthFailed()
    {
        Debug.Log("OnAuthFailed run");
    }

    public void OnTokenExpired()
    {
        Debug.Log("OnTokenExpired run");
    }

    public void OnTokenWillExpire()
    {
        Debug.Log("OnTokenWillExpire run");
    }
}
