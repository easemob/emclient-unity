using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ChatSDK;
using ChatSDK.MessageBody;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Main : MonoBehaviour
{
    // 接收消息id
    public InputField RecvIdField;
    // 输入内容
    public InputField TextField;
    // 发送按钮
    public Button SendBtn;
     // 群组id
    public InputField GroupField;
    // 加入群组按钮
    public Button JoinGroupBtn;
    // 获取群详情按钮
    public Button GroupInfoBtn;
    // 退出群组按钮
    public Button LeaveGroupBtn;
    // 聊天室id
    public InputField RoomField;
    // 加入聊天室按钮
    public Button JoinRoomBtn;
    // 获取聊天室按钮
    public Button RoomInfoBtn;
    // 退出聊天室按钮
    public Button LeaveRoomBtn;


    //public ScrollView scrollView;
    public ScrollRect scrollRect;

    IEnumerable<Toggle> ToggleGroup;

    private void Awake()
    {
        //setup chat manager delegate
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(ChatManagerDelegate.Global);
    }

    // Start is called before the first frame update
    void Start()
    {
        SendBtn.onClick.AddListener(GetConversationAction);
        LeaveGroupBtn.onClick.AddListener(DeclineInvitationAction);
        LeaveRoomBtn.onClick.AddListener(GetBlockListFromServerAction);

        JoinGroupBtn.onClick.AddListener(JoinGroupAction);
        GroupInfoBtn.onClick.AddListener(GetGroupInfoAction);
        //LeaveGroupBtn.onClick.AddListener(LeaveGroupAction);

        JoinRoomBtn.onClick.AddListener(JoinRoomAction);
        RoomInfoBtn.onClick.AddListener(GetRoomInfoAction);
        

        ToggleGroup = GameObject.Find("ToggleGroup").GetComponent<ToggleGroup>().ActiveToggles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quit and release resources...");
        SDKClient.Instance.Logout(false);
    }

    //chatmanager
    async void SendMessageAction()
    {
        SendBtn.enabled = false;
        
        string receiverId = RecvIdField.text;
        string content = TextField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = Message.CreateTextSendMessage(receiverId,content);
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });
        await Task.Run(()=>chatManager.SendMessage(message, callback));
        SendBtn.enabled = true;
    }

    void SendLocationMessageAction()
    {
        string receiverId = RecvIdField.text;
        double latitude = 39.33F;
        double longitude = 116.33F;
        string address = "Beijing";
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });
        
        //send a location message
        Message message = Message.CreateLocationSendMessage(receiverId, latitude, longitude, address);
        chatManager.SendMessage(message, callback);
    }

    void SendCmdAction()
    {
        string receiverId = RecvIdField.text;
        string content = TextField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });

        //send a location message
        Message message = Message.CreateCmdSendMessage(receiverId, content, false);
        chatManager.SendMessage(message, callback);
    }

    void SendFileMessageAction()
    {
        string receiverId = RecvIdField.text;
        string displayName = TextField.text;
        string localPath = "/Users/bingo/1.txt";
        long fileSize = 4;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = Message.CreateFileSendMessage(receiverId, localPath, displayName, fileSize);
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.SendMessage(message, callback);
    }

    async void SendImageMessageAction()
    {
        SendBtn.enabled = false;
        await Task.Run(() =>
        {
            string receiverId = RecvIdField.text;
            string displayName = TextField.text;
            string localPath = "/Users/bingo/1.png";
            long fileSize = 2385;
            IChatManager chatManager = SDKClient.Instance.ChatManager;
            Message message = Message.CreateImageSendMessage(receiverId, localPath, displayName, fileSize, true, 447, 147);
            CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                                onProgress: (int progress) => { Debug.Log(progress); },
                                                onError: (int code, string desc) => { Debug.Log(code + desc); });
            chatManager.SendMessage(message, callback);

        });
        SendBtn.enabled = true;
    }
	
	void SendVoiceMessageAction()
    {
        string receiverId = RecvIdField.text;
        string displayName = TextField.text;
        string localPath = "/Users/Shared/1.mp3";
        long fileSize = 5993;
        int duration = 10;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = Message.CreateVoiceSendMessage(receiverId, localPath, displayName, fileSize, duration);
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message send successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.SendMessage(message, callback);
    }

    void FetchHistoryMessagesAction()
    {
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        chatManager.FetchHistoryMessages("ys1", ConversationType.Chat, "", 32,
            new ValueCallBack<CursorResult<Message>>(onSuccess: (CursorResult<Message> cursorResult) =>
            {
                Debug.Log($"Fetch history messages with next cursor: {cursorResult.Cursor}");
                foreach(var message in cursorResult.Data)
                {
                    Debug.Log($"MsgId: {message.MsgId}: {message.From} -> {message.To}");
                    if(message.Body.Type == MessageBodyType.TXT)
                    {
                        var body = (TextBody)message.Body;
                        Debug.Log($"Content: {body.Text}");
                    }else if(message.Body.Type == MessageBodyType.LOCATION)
                    {
                        var body = (LocationBody)message.Body;
                        Debug.Log($"Address: {body.Address}");
                    }else if (message.Body.Type == MessageBodyType.CMD)
                    {
                        var body = (CmdBody)message.Body;
                        Debug.Log($"Action: {body.Action}");
                    }else if (message.Body.Type == MessageBodyType.FILE)
                    {
                        var body = (FileBody)message.Body;
                        Debug.Log($"LocalPath: {body.LocalPath}, DisplayName: {body.DisplayName}");
                    }else if (message.Body.Type == MessageBodyType.IMAGE)
                    {
                        var body = (ImageBody)message.Body;
                        Debug.Log($"LocalPath: {body.LocalPath}, DisplayName: {body.DisplayName}");
                        Debug.Log($"ThumnailLocalPath: {body.ThumbnailLocalPath}");
                    }
                }
            }, onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch history messages with error, code={code}, desc={desc}.");
            }));
    }

    void GetConversationsFromServerAction()
    {
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        chatManager.GetConversationsFromServer(
            new ValueCallBack<List<Conversation>>(
                onSuccess: (List<Conversation> conversationList) =>
                {
                    Debug.Log($"Get conversations from server succeed");
                    int count = 1;
                    foreach(var converation in conversationList)
                    {
                        //string extField = TransformTool.JsonStringFromDictionary(converation.Ext);
                        Debug.Log($"conversation{count} id={converation.Id}, type={converation.GetType()}");
                        count++;
                    }
                },
                onError: (int code, string desc) =>
                {
                    Debug.LogError($"Get conversations from server failed, code={code}, desc={desc}");
                }
                ));
    }

    void DownloadAttachmentAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Down load attachment successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.DownloadAttachment(messageId, callback);
    }

    void DownloadThumbnailAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Down load thumbnail successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.DownloadThumbnail(messageId, callback);
    }

    void GetUnreadMessageCountAction()
    {
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        int unreadMessageCount = chatManager.GetUnreadMessageCount();
        Debug.Log($"Get unread message count:{unreadMessageCount}");
    }

    void ImportMessagesAction()
    {
        List<Message> messageList = new List<Message>();

        string receiverId = RecvIdField.text;
        string content = "123";
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message1 = Message.CreateTextSendMessage(receiverId, content);
        messageList.Add(message1);

        receiverId = RecvIdField.text;
        content = "456";
        Message message2 = Message.CreateTextSendMessage(receiverId, content);
        messageList.Add(message2);

        chatManager.ImportMessages(messageList);
    }

    void LoadAllConversationsAction()
    {
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        List<Conversation> conversationList = chatManager.LoadAllConversations();
        int i = 0;
        foreach(Conversation conversation in conversationList)
        {
            Debug.Log($"Get conversaion:{i} with id:{conversation.Id} and type:{conversation.GetType()}");
            i++;
        }
    }

    void LoadMessageAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = chatManager.LoadMessage(messageId);
        Debug.Log($"Load message id:{message.MsgId} and type:{message.GetType()}");
    }

    void MarkAllConversationsAsReadAction()
    {
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        if(chatManager.MarkAllConversationsAsRead())
            Debug.Log("Mark all conversation as read successfully!");
        else
            Debug.Log("Mark all conversation as read failed!");
    }

    void RecallMessageAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log($"Recall message:{messageId} successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.RecallMessage(messageId, callback);
    }

    void ReSendMessageAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log($"Resend message:{messageId} successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.ResendMessage(messageId, callback);
    }

    void SearchMsgFromDBAction()
    {
        string keywords = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        List<Message>  messageList = chatManager.SearchMsgFromDB(keywords, 1627439541, 20, null, MessageSearchDirection.UP);
        int i = 0;
        foreach(Message message in messageList)
        {
            Debug.Log($"Find message:{i}, msgid:{message.MsgId}, conversationid:{message.ConversationId}, to:{message.To}, from:{message.From}, type:{message.MessageType}");
            i++;
        }
    }

    void SendConversationReadAckAction()
    {
        string conversationId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log($"Send converation:{conversationId} read ack successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.SendConversationReadAck(conversationId, callback);
    }

    void SendMessageReadAckAction()
    {
        string messageId = RecvIdField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log($"Send message:{messageId} read ack successfully!"); },
                                onProgress: (int progress) => { Debug.Log(progress); },
                                onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.SendMessageReadAck(messageId, callback);
    }

    void UpdateMessageAction()
    {
        string messageId = RecvIdField.text;
        string receiverId = "yqtest";
        string content = TextField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = Message.CreateTextSendMessage(receiverId, content);
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Update message successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });
        //to-do: Add more message fields in update action
        message.MsgId = messageId;
        message.Direction = MessageDirection.RECEIVE;
        chatManager.UpdateMessage(message, callback);
    }

    void GetConversationAction()
    {
        string conversationId = RecvIdField.text;
        ConversationType type = ConversationType.Chat;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        chatManager.GetConversation(conversationId, type, true);
    }

    //groupmanager
    void CreateGroupAction()
    {
        string groupName = RecvIdField.text;
        string description = TextField.text;
        List<string> inviteMembers = new List<string>{ "f1", "ys1" };
        GroupOptions options = new GroupOptions(GroupStyle.PublicOpenJoin, 20, false, "");
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.CreateGroup(groupName, options, description, inviteMembers, "join us!",
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Group {group.GroupId} named {group.Name} created successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Create group failed with code={code}, desc={desc}");
            }));
    }

    void CreateRoomAction()
    {
        string subject = RecvIdField.text;
        string description = TextField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.CreateRoom(subject, description, description, 300, members,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Room {room.RoomId} named {room.Name} created successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Create room failed with code={code}, desc={desc}");
            }));
    }

    async void ChangeRoomSubjectAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        string newSubject = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(()=>roomManager.ChangeRoomSubject(roomId, newSubject,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Room {room.RoomId} subject changed to {room.Name} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Change room subject failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
    }

    void RemoveRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        string description = TextField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.RemoveRoomMembers(roomId, members,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Remove members from room {roomId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Remove members from room failed with code={code}, desc={desc}");
            }));
    }

    void RenameGroupAction()
    {
        string groupId = RecvIdField.text;
        string newName = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.ChangeGroupName(groupId, newName,
            new CallBack(
                onSuccess: () => Debug.Log($"Group name changed to {newName}"),
                onError: (int code, string desc) => Debug.LogError($"Failed to change group {groupId} name to {newName}")));
    }

    void DestoryGroupAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.DestroyGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Destory group {groupId} success."),
                onError: (int code, string desc) => Debug.LogError($"Failed to destory group {groupId} with error: {desc}.")));
    }

    void AddMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.AddMembers(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Add {member} into group {groupId}"),
                onError: (int code, string desc) => Debug.LogError($"Failed to add member {member} to group {groupId} with error: {desc}.")));
    }

    void AddAdminAction()
    {
        string groupId = RecvIdField.text;
        string admin = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.AddAdmin(groupId, admin,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Add admin {admin} into group {group.GroupId} successfully.");
                if(group.AdminList.Count > 0)
                {
                    Debug.Log($"Admin[0]={group.AdminList[0]}");
                }               
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Add admin {admin} into group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void RemoveMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.RemoveMembers(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Remove {member} from group {groupId}"),
                onError: (int code, string desc) => Debug.LogError($"Failed to remove member {member} from group {groupId} with error: {desc}.")));
    }

    void JoinGroupAction()
    {

    }

    void GetGroupInfoAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupWithId(groupId,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Group {group.GroupId} information retrieved successfully.");
                if(group.MemberCount > 0)
                {
                    Debug.Log($"MemberLsit[0]={group.MemberList[0]}");
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Failed to get group information, error code={code}, desc={desc}");
            }));
    }

    void LeaveGroupAction()
    {
        //used for logout and scene change!
        SDKClient.Instance.Logout(false);
        SceneManager.LoadScene("Login");
    }

    void JoinRoomAction()
    {

    }

    void GetRoomInfoAction()
    {

    }

    void LeaveRoomAction()
    {

    }

    //ContactManager related
    void AddContactAction()
    {
        string username = RecvIdField.text;
        string reason = TextField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.AddContact(username, reason,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Add contact {username} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Add contact {username} failed with code={code}, desc={desc}");
            }));
    }

    void DeleteContactAction()
    {
        string username = RecvIdField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.DeleteContact(username, false,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Delete contact {username} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Delete contact {username} failed with code={code}, desc={desc}");
            }));
    }


    public int repeatcount = 0;
    void GetAllContactsFromServerAction()
    {
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        repeatcount++;
        contactManager.GetAllContactsFromServer(
            new ValueCallBack<List<string>>(
                onSuccess: (List<string> contactList) =>
                {
                    Debug.Log($"Get contacts from server succeed. repeat:{repeatcount}");
                    int count = 1;
                    foreach(var contact in contactList)
                    {
                        Debug.Log($"contact{count}:{contact}, repeat:{repeatcount}");
                    }
                },
                onError: (int code, string desc) =>
                {
                    Debug.LogError($"Get contacts from server failed, code={code}, desc={desc}");
                }
                ));
    }

    void GetAllContactsFromDBAction()
    {
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        repeatcount++;
        contactManager.GetAllContactsFromDB(
            new ValueCallBack<List<string>>(
                onSuccess: (List<string> contactList) =>
                {
                    Debug.Log($"Get contacts from DB succeed. repeat:{repeatcount}");
                    int count = 1;
                    foreach (var contact in contactList)
                    {
                        Debug.Log($"contact{count}:{contact}, repeat:{repeatcount}");
                    }
                },
                onError: (int code, string desc) =>
                {
                    Debug.LogError($"Get contacts from DB failed, code={code}, desc={desc}");
                }
                ));
    }

    void AddUserToBlockListAction()
    {
        string username = RecvIdField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.AddUserToBlockList(username,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Add {username} to block list successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Add {username} to block list failed with code={code}, desc={desc}");
            }));
    }

    void RemoveUserFromBlockListAction()
    {
        string username = RecvIdField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.RemoveUserFromBlockList(username,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Remove {username} from block list successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Remove {username} from block list failed with code={code}, desc={desc}");
            }));
    }

    void GetBlockListFromServerAction()
    {
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        repeatcount++;
        contactManager.GetBlockListFromServer(
            new ValueCallBack<List<string>>(
                onSuccess: (List<string> contactList) =>
                {
                    Debug.Log($"Get blacklist from server succeed. repeat:{repeatcount}");
                    int count = 1;
                    foreach (var contact in contactList)
                    {
                        Debug.Log($"contact{count}:{contact}, repeat:{repeatcount}");
                    }
                },
                onError: (int code, string desc) =>
                {
                    Debug.LogError($"Get black list from DB failed, code={code}, desc={desc}");
                }
                ));
    }

    void AcceptInvitationAction()
    {
        string username = RecvIdField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.AcceptInvitation(username,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Accept invitation from {username} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Accept invitation from {username} failed with code={code}, desc={desc}");
            }));
    }

    void DeclineInvitationAction()
    {
        string username = RecvIdField.text;
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        contactManager.DeclineInvitation(username,
            new CallBack(onSuccess: () =>
            {
                Debug.Log($"Decline invitation from {username} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Decline invitation from {username} failed with code={code}, desc={desc}");
            }));
    }

    void GetSelfIdsOnOtherPlatformAction()
    {
        IContactManager contactManager = SDKClient.Instance.ContactManager;
        repeatcount++;
        contactManager.GetSelfIdsOnOtherPlatform(
            new ValueCallBack<List<string>>(
                onSuccess: (List<string> selfIdList) =>
                {
                    Debug.Log($"Get self id on other platform succeed. repeat:{repeatcount}");
                    int count = 1;
                    foreach (var selfId in selfIdList)
                    {
                        Debug.Log($"selfid{count}:{selfId}, repeat:{repeatcount}");
                    }
                },
                onError: (int code, string desc) =>
                {
                    Debug.LogError($"Get self id failed, code={code}, desc={desc}");
                }
                ));
    }
}
