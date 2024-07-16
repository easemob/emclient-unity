
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;
using System.Collections.Generic;
using AgoraChat.InternalSpace;

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
    private Button RunDelegateTester;
    private Button GetLoggedInDevicesFromServerWithTokenBtn;
    private Button KickDeviceWithTokenBtn;
    private Button KickAllDevicesWithTokenBtn;


    private void Awake()
    {

        Debug.Log("main script has load");

        ChatBtn = transform.Find("Scroll View/Viewport/Content/ChatBtn").GetComponent<Button>();
        ContactBtn = transform.Find("Scroll View/Viewport/Content/ContactBtn").GetComponent<Button>();
        ConversationBtn = transform.Find("Scroll View/Viewport/Content/ConversationBtn").GetComponent<Button>();
        GroupBtn = transform.Find("Scroll View/Viewport/Content/GroupBtn").GetComponent<Button>();
        RoomBtn = transform.Find("Scroll View/Viewport/Content/RoomBtn").GetComponent<Button>();
        PushBtn = transform.Find("Scroll View/Viewport/Content/PushBtn").GetComponent<Button>();
        PresenceBtn = transform.Find("Scroll View/Viewport/Content/PresenceBtn").GetComponent<Button>();
        ThreadBtn = transform.Find("Scroll View/Viewport/Content/ThreadBtn").GetComponent<Button>();
        isConnectedBtn = transform.Find("Scroll View/Viewport/Content/IsConnectBtn").GetComponent<Button>();
        isLoggedBtn = transform.Find("Scroll View/Viewport/Content/IsLoggedBtn").GetComponent<Button>();
        CurrentUsernameBtn = transform.Find("Scroll View/Viewport/Content/CurrentUsernameBtn").GetComponent<Button>();
        AccessTokenBtn = transform.Find("Scroll View/Viewport/Content/AccessTokenBtn").GetComponent<Button>();
        LogoutBtn = transform.Find("Scroll View/Viewport/Content/LogoutBtn").GetComponent<Button>();
        m_NewTokenBtn = transform.Find("Scroll View/Viewport/Content/NewTokenBtn").GetComponent<Button>();
        RunDelegateTester = transform.Find("Scroll View/Viewport/Content/RunDelegateTester").GetComponent<Button>();
        GetLoggedInDevicesFromServerWithTokenBtn = transform.Find("Scroll View/Viewport/Content/GetLoggedInDevicesFromServerWithTokenBtn").GetComponent<Button>();
        KickDeviceWithTokenBtn = transform.Find("Scroll View/Viewport/Content/KickDeviceWithTokenBtn").GetComponent<Button>();
        KickAllDevicesWithTokenBtn = transform.Find("Scroll View/Viewport/Content/KickAllDevicesWithTokenBtn").GetComponent<Button>();

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
        RunDelegateTester.onClick.AddListener(RunDelegateTesterAction);
        GetLoggedInDevicesFromServerWithTokenBtn.onClick.AddListener(GetLoggedInDevicesFromServerWithTokenAction);
        KickDeviceWithTokenBtn.onClick.AddListener(KickDeviceWithTokenAction);
        KickAllDevicesWithTokenBtn.onClick.AddListener(KickAllDevicesWithTokenAction);

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

    void GetLoggedInDevicesFromServerWithTokenAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.GetLoggedInDevicesFromServerWithToken(dict["username"], dict["token"],
            callback: new ValueCallBack<List<DeviceInfo>>(

                onSuccess: (list) =>
                {
                    string json = "";
                    foreach (var it in list)
                    {
                        if (json.Length > 0)
                        {
                            json += ";";
                        }
                        json += it.ToJsonObject().ToString();
                    }
                    UIManager.TitleAlert(transform, "成功", json);
                },

                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
                )
            );
        });

        config.AddField("username");
        config.AddField("token");
        UIManager.DefaultInputAlert(transform, config);
    }

    void KickDeviceWithTokenAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.KickDeviceWithToken(dict["username"], dict["token"], dict["resource"],
            callback: new CallBack(

                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", "Success");
                },

                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
                )
            );
        });

        config.AddField("username");
        config.AddField("token");
        config.AddField("resource");
        UIManager.DefaultInputAlert(transform, config);
    }

    void KickAllDevicesWithTokenAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.KickAllDevicesWithToken(dict["username"], dict["token"],
            callback: new CallBack(

                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", "Success");
                },

                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
                )
            );
        });

        config.AddField("username");
        config.AddField("token");
        UIManager.DefaultInputAlert(transform, config);
    }

    void NewTokenAction()
    {
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
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.RenewAgoraToken(dict["token"]);
            UIManager.DefaultAlert(transform, "Renew agora token complete.");
        });

        config.AddField("token");
        UIManager.DefaultInputAlert(transform, config);


    }

    void RunDelegateTesterAction()
    {
        MyTest.DelegateTester();
    }

    void Start()
    {
        SDKClient.Instance.AddConnectionDelegate(this);
        SDKClient.Instance.AddMultiDeviceDelegate(this);

        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
        SDKClient.Instance.PresenceManager.AddPresenceManagerDelegate(this);
        SDKClient.Instance.ThreadManager.AddThreadManagerDelegate(this);
    }


    // Update is called once per frame
    void Update()
    {

    }


    // 收到群组邀请
    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {

        Debug.Log($"GroupManager1 OnRequestToJoinReceivedFromGroup groupId: {groupId}, groupName: {groupName}, inviter: {inviter}, reason: {reason}");
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
        Debug.Log($"GroupManager2 OnRequestToJoinReceivedFromGroup groupId: {groupId}, groupName: {groupName}, applicant: {applicant}, reason: {reason}");
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
        Debug.Log($"GroupManager3 OnRequestToJoinAcceptedFromGroup groupId: {groupId}, groupName: {groupName}, accepter: {accepter}");
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string reason, string decliner, string applicant)
    {
        Debug.Log($"GroupManager4 OnRequestToJoinDeclinedFromGroup groupId: {groupId}, reason: {reason}, decliner:{decliner}, applicant:{applicant}");
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee)
    {
        Debug.Log($"GroupManager5 OnInvitationAcceptedFromGroup groupId: {groupId}, invitee: {invitee}");
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log($"GroupManager6 OnInvitationDeclinedFromGroup groupId: {groupId}, invitee: {invitee}, reason: {reason}");
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        Debug.Log($"GroupManager7 OnUserRemovedFromGroup groupId: {groupId}, groupName: {groupName}");
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        Debug.Log($"GroupManager8 OnDestroyedFromGroup groupId: {groupId}, groupName: {groupName}");
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log($"GroupManager9 OnAutoAcceptInvitationFromGroup groupId: {groupId}, inviter: {inviter}, inviteMessage: {inviteMessage}");
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        Debug.Log($"GroupManager10 OnMuteListRemovedFromGroup groupId: {groupId}, mutes: {string.Join(", ", mutes.ToArray())}");
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"GroupManager11 OnAdminAddedFromGroup groupId: {groupId}, administrator: {administrator}");
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log($"GroupManager12 OnAdminRemovedFromGroup groupId: {groupId}, administrator: {administrator}");
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log($"GroupManager13 OnMemberJoinedFromGroup groupId: {groupId}, newOwner: {newOwner}, oldOwner: {oldOwner}");
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        Debug.Log($"GroupManager14 OnMemberJoinedFromGroup groupId: {groupId}, member: {member}");
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        Debug.Log($"GroupManager15 OnMemberExitedFromGroup groupId: {groupId}, member: {member}");
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        Debug.Log($"GroupManager16 OnAnnouncementChangedFromGroup groupId: {groupId}, announcement: {announcement}");
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log($"GroupManager17 OnSharedFileAddedFromGroup groupId: {groupId}, sharedFile: {sharedFile.ToJsonObject()}");
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        Debug.Log($"GroupManager18 OnSharedFileDeletedFromGroup groupId: {groupId}, fileId: {fileId}");
    }

    public void OnAddAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        Debug.Log($"GroupManager19 OnAddAllowListMembersFromGroup groupId: {groupId}, whiteList: {string.Join(", ", whiteList.ToArray())}");
    }

    public void OnRemoveAllowListMembersFromGroup(string groupId, List<string> whiteList)
    {
        Debug.Log($"GroupManager20 OnRemoveAllowListMembersFromGroup groupId: {groupId}, whiteList: {string.Join(", ", whiteList.ToArray())}");
    }

    public void OnAllMemberMuteChangedFromGroup(string groupId, bool isAllMuted)
    {
        Debug.Log($"GroupManager21 OnStateChangedFromGroup groupId: {groupId}, isAllMuted:{isAllMuted}");
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, long muteExpire)
    {
        Debug.Log($"GroupManager22 OnMuteListAddedFromGroup groupId: {groupId}, mutes: {string.Join(",", mutes.ToArray())}, expireTime: {muteExpire}");
    }

    public void OnStateChangedFromGroup(string groupId, bool isDisable)
    {
        Debug.Log($"GroupManager23 OnStateChangedFromGroup groupId: {groupId}, isDisable:{isDisable}");
    }

    public void OnSpecificationChangedFromGroup(Group group)
    {
        Debug.Log($"GroupManager24 OnSpecificationChangedFromGroup group: {group.ToJsonObject()}");
    }

    public void OnUpdateMemberAttributesFromGroup(string groupId, string userId, Dictionary<string, string> attributes, string from)
    {
        Debug.Log($"IGroupManagerDelegate25 OnUpdateMemberAttributesFromGroup: gid: {groupId}; userId:{userId}, from:{from}");
        Debug.Log($"attrs count:{attributes.Count}");
        foreach (var it in attributes)
        {
            Debug.Log($"key:{it.Key}, value:{it.Value}");
        }
    }

    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        Debug.Log($"RoomManager1 OnDestroyedFromRoom roomId: {roomId}, roomName:{roomName}");
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        Debug.Log($"RoomManager2 OnMemberJoinedFromRoom roomId: {roomId}, participant:{participant}");
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log($"RoomManager3 OnMemberExitedFromRoom roomId: {roomId}, name:{roomName}, participant:{participant}");
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

        Debug.Log($"RoomManager4 OnRemovedFromRoom roomId: {roomId}, name: {roomName}, participant: {participant}");
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        Debug.Log($"RoomManager5 OnMuteListAddedFromRoom roomId: {roomId}, mutes: {string.Join(", ", mutes.ToArray())}, expireTime: {expireTime}");
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        Debug.Log($"RoomManager6 OnMuteListRemovedFromRoom roomId: {roomId}, mutes: {string.Join(", ", mutes.ToArray())}");
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        Debug.Log($"RoomManager7 OnAdminAddedFromRoom roomId: {roomId}, admin: {admin}");
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        Debug.Log($"RoomManager8 OnAdminRemovedFromRoom roomId: {roomId}, admin: {admin}");
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log($"RoomManager9 OnOwnerChangedFromRoom roomId: {roomId}, newOwner: {newOwner}, oldOwner: {oldOwner}");
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        Debug.Log($"RoomManager10 OnAnnouncementChangedFromRoom roomId: {roomId}, announcement: {announcement}");
    }

    public void OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from)
    {
        List<string> list = new List<string>();

        foreach (var key in kv.Keys)
        {
            list.Add($"{key}:kv[key]");
        }

        Debug.Log($"RoomManager11 OnChatroomAttributesChanged roomId: {roomId}, keys: {string.Join(", ", list.ToArray())}, from: {from}");
    }

    public void OnChatroomAttributesRemoved(string roomId, List<string> keys, string from)
    {
        Debug.Log($"RoomManager12 OnSpecificationChangedFromRoom roomId: {roomId}, keys: {string.Join(", ", keys.ToArray())}, from: {from}");
    }

    public void OnSpecificationChangedFromRoom(Room room)
    {
        Debug.Log($"RoomManager13 OnSpecificationChangedFromRoom room: {room.ToJsonObject()}");
    }

    public void OnAddAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        Debug.Log($"RoomManager14 OnAddAllowListMembersFromChatroom roomId: {roomId}, members: {string.Join(", ", members.ToArray())}");
    }

    public void OnRemoveAllowListMembersFromChatroom(string roomId, List<string> members)
    {
        Debug.Log($"RoomManager15 OnRemoveAllowListMembersFromChatroom roomId: {roomId}, members: {string.Join(", ", members.ToArray())}");
    }

    public void OnAllMemberMuteChangedFromChatroom(string roomId, bool isAllMuted)
    {
        Debug.Log($"RoomManager16 OnAllMemberMuteChangedFromChatroom roomId: {roomId}, isAllMuted: {isAllMuted}");
    }

    public void OnRemoveFromRoomByOffline(string roomId, string roomName)
    {
        Debug.Log($"RoomManager17 OnRemoveFromRoomByOffline roomId: {roomId}, roomName: {roomName}");
    }

    public void OnPresenceUpdated(List<Presence> presences)
    {
        List<string> list = new List<string>();
        foreach (var presence in presences)
        {
            list.Add(presence.ToJsonObject().ToString());
        }
        Debug.Log($"Presence1 OnPresenceUpdated {string.Join(", ", list.ToArray())}");
    }

    public void OnMessagesReceived(List<Message> messages)
    {
        List<string> contents = new List<string>();
        foreach (var msg in messages)
        {
            contents.Add(msg.ToJsonObject().ToString());
            string pinStr = "PinnedBy:" + msg.PinnedInfo.PinnedBy + " PinnedAt:" + msg.PinnedInfo.PinnedAt.ToString();
            contents.Add(pinStr);
        }
        Debug.Log($"ChatManager1 OnMessagesReceived {string.Join(", ", contents.ToArray())}");
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager2 OnCmdMessagesReceived {string.Join(", ", msgs.ToArray())}");
    }

    public void OnMessagesRead(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager3 OnMessagesRead {string.Join(", ", msgs.ToArray())}");
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager4 OnMessagesDelivered {string.Join(", ", msgs.ToArray())}");
    }

    /*public void OnMessagesRecalled(List<Message> messages)
    {
        List<string> msgs = new List<string>();
        foreach (var msg in messages)
        {
            msgs.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager5 OnMessagesRecalled {string.Join(", ", msgs.ToArray())}");
    }*/

    public void OnMessagesRecalled(List<RecallMessageInfo> recallMessagesInfo)
    {
        List<string> list = new List<string>();
        foreach (var msg in recallMessagesInfo)
        {
            list.Add(msg.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager5 OnMessagesRecalled {string.Join(", ", list.ToArray())}");
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        Debug.Log($"ChatManager6 OnReadAckForGroupMessageUpdated");
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        List<string> acks = new List<string>();
        foreach (var ack in list)
        {
            acks.Add(ack.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager7 OnGroupMessageRead {string.Join(", ", acks.ToArray())}");
    }

    public void OnConversationsUpdate()
    {
        Debug.Log($"ChatManager8 OnConversationsUpdate");
    }

    public void OnConversationRead(string from, string to)
    {
        Debug.Log($"ChatManager9 OnConversationRead from:{from}, to:{to}");
    }

    public void MessageReactionDidChange(List<MessageReactionChange> list)
    {
        List<string> changes = new List<string>();
        foreach (var change in list)
        {
            changes.Add(change.ToJsonObject().ToString());
        }
        Debug.Log($"ChatManager10 OnContactAdded {string.Join(", ", changes.ToArray())}");
    }

    public void OnMessageContentChanged(Message msg, string operatorId, long operationTime)
    {
        Debug.Log($"ChatManager11 OnMessageContentChanged change msgId:{msg.MsgId}, operatorId:{operatorId}, operationTime:{operationTime}");
    }

    public void OnMessagePinChanged(string messageId, string conversationId, bool isPinned, string operatorId, long operationTime)
    {
        Debug.Log($"ChatManager12 OnMessagePinChanged msgId:{messageId}, convId:{conversationId}, isPinned:{isPinned}, operatorId:{operatorId}, operationTime:{operationTime}");
    }

    public void OnContactAdded(string username)
    {
        Debug.Log($"ContactManager1 OnContactAdded {username}");
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log($"ContactManager2 OnContactDeleted {username}");
    }

    public void OnContactInvited(string username, string reason)
    {

        Debug.Log($"ContactManager3 OnContactInvited {username} {reason}");
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
        Debug.Log($"ContactManager4 OnFriendRequestAccepted {username}");
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log($"ContactManager5 OnFriendRequestDeclined {username}");
    }
    public void OnChatThreadCreate(ChatThreadEvent threadEvent)
    {
        Debug.Log($"ChatThread1 OnChatThreadCreate {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadUpdate(ChatThreadEvent threadEvent)
    {
        Debug.Log($"ChatThread2 OnChatThreadUpdate {threadEvent.ToJsonObject()}");
    }

    public void OnChatThreadDestroy(ChatThreadEvent threadEvent)
    {
        Debug.Log($"ChatThread3 OnChatThreadDestroy {threadEvent.ToJsonObject()}");
    }

    public void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent)
    {
        Debug.Log($"ChatThread4 OnUserKickOutOfChatThread {threadEvent.ToJsonObject()}");
    }

    public void OnContactMultiDevicesEvent(MultiDevicesOperation operation, string target, string ext)
    {
        Debug.Log($"MultiDevice1 OnContactMultiDevicesEvent {operation}: {target}: {ext}");
    }

    public void OnGroupMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        Debug.Log($"MultiDevice2 OnGroupMultiDevicesEvent {operation}: {target}: {string.Join(", ", usernames.ToArray())}");
    }

    public void OnUndisturbMultiDevicesEvent(string data)
    {
        Debug.Log($"MultiDevice3 OnUndisturbMultiDevicesEvent {data}");
    }

    public void OnThreadMultiDevicesEvent(MultiDevicesOperation operation, string target, List<string> usernames)
    {
        Debug.Log($"MultiDevice4 OnThreadMultiDevicesEvent {operation}: {target}: {string.Join(", ", usernames.ToArray())}");
    }

    public void OnRoamDeleteMultiDevicesEvent(string conversationId, string deviceId)
    {
        Debug.Log($"MultiDevice5 OnRoamDeleteMultiDevicesEvent conversationId:{conversationId}, deviceId:{deviceId}");
    }

    public void OnConversationMultiDevicesEvent(MultiDevicesOperation operation, string conversationId, ConversationType type)
    {
        Debug.Log($"MultiDevice6 OnConversationMultiDevicesEvent: operation: {operation}, conversationId:{conversationId}, type:{type}");
    }

    public void OnConnected()
    {
        Debug.Log("Connection1 OnConnected run");
    }

    public void OnDisconnected()
    {
        Debug.Log("Connection2 OnDisconnected run");
    }

    public void OnLoggedOtherDevice(string deviceName)
    {
        Debug.Log($"Connection3 OnLoggedOtherDevice run, deviceName:{deviceName}");
    }

    public void OnRemovedFromServer()
    {
        Debug.Log("Connection4 OnRemovedFromServer run");
    }

    public void OnForbidByServer()
    {
        Debug.Log("Connection5 OnForbidByServer run");
    }

    public void OnChangedIMPwd()
    {
        Debug.Log("Connection6 OnChangedIMPwd run");
    }

    public void OnLoginTooManyDevice()
    {
        Debug.Log("Connection7 OnLoginTooManyDevice run");
    }

    public void OnKickedByOtherDevice()
    {
        Debug.Log("Connection8 OnKickedByOtherDevice run");
    }

    public void OnAuthFailed()
    {
        Debug.Log("Connection9 OnAuthFailed run");
    }

    public void OnTokenExpired()
    {
        Debug.Log("Connection10 OnTokenExpired run");
    }

    public void OnTokenWillExpire()
    {
        Debug.Log("Connection11 OnTokenWillExpire run");
    }

    public void OnAppActiveNumberReachLimitation()
    {
        Debug.Log("Connection12 OnAppActiveNumberReachLimitation run");
    }
}
