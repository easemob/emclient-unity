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


    //roommanager
    async void AddRoomAdminAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        string memberId = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(() => roomManager.AddRoomAdmin(roomId, memberId,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Add admin:{memberId} to Room {room.RoomId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Add admin:{memberId} to room failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
    }


    void BlockRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.BlockRoomMembers(roomId, members,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Block member in {room.RoomId} named {room.Name} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Block memmber in room failed with code={code}, desc={desc}");
            }));
    }

    async void ChangeOwnerAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        string newOwner = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(() => roomManager.AddRoomAdmin(roomId, newOwner,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Change newOwner:{newOwner} to Room {room.RoomId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Change newOwner:{newOwner} to room failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
    }

    async void ChangeRoomDescriptionAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        string newDescription = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(() => roomManager.AddRoomAdmin(roomId, newDescription,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Change newDescription:{newDescription} to Room {room.RoomId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Change newDescription:{newDescription} to room failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
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
        await Task.Run(() => roomManager.ChangeRoomSubject(roomId, newSubject,
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

    void DestroyRoomAction()
    {
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.DestroyRoom(roomId,
            new CallBack(
                onSuccess: () => Debug.Log($"Destory room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to destory room {roomId} with error: {desc}.")));
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

    void FetchPublicRoomsFromServerAction()
    {
        int pageNum = 1;
        int pageSize = 20; ;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.FetchPublicRoomsFromServer(pageNum, pageSize,
            new ValueCallBack<PageResult<Room>>(onSuccess: (PageResult<Room> pageResult) =>
            {
                Debug.Log($"Fetch public rooms {pageResult.Data.Count} from server successfully.");
                int i = 0;
                foreach (var item in pageResult.Data)
                {
                    Debug.Log($"Room{i}: roomId={item.RoomId}, roomName={item.Name}");
                    i++;
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch public rooms from server failed with error, code={code}, desc={desc}.");
            }));
    }

    void FetchRoomAnnouncementAction()
    {
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.FetchRoomAnnouncement(roomId,
            new ValueCallBack<string>(onSuccess: (string result) =>
            {
                Debug.Log($"Fetch public rooms {roomId} announcement {result} successfully.");
      
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch public rooms from server failed with error, code={code}, desc={desc}.");
            }));
    }

    void FetchRoomBlockListAction()
    {
        string roomId = RecvIdField.text;
        int pageNum = 1;
        int pageSize = 200;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.FetchRoomBlockList(roomId, pageNum, pageSize,
            new ValueCallBack<List<string>>(onSuccess: (List<string> banList) =>
            {
                int i = 0;
                foreach (string item in banList)
                {
                    Debug.Log($"Fetch blockList of room {roomId}, item{i}:{item}.");
                    i++;
                }

            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch blockList of room {roomId} failed with code={code}, desc={desc}");
            }));
    }

    async void FetchRoomInfoFromServerAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(() => roomManager.FetchRoomInfoFromServer(roomId,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Fetch room info roomId={room.RoomId} roomName={room.Name} from server successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch room info from server failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
    }

    void FetchRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        string cursor = TextField.text;
        int pageSize = 200;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.FetchRoomMembers(roomId, cursor, pageSize,
            new ValueCallBack<CursorResult<string>>(onSuccess: (CursorResult<string> cursorResult) =>
            {
                Debug.Log($"Fetch room member list with next cursor: {cursorResult.Cursor}");
                foreach (var item in cursorResult.Data)
                {
                    Debug.Log($"member: {item}");
                }
            }, onError: (int code, string desc) =>
            {
                Debug.LogError($"Group member list from server failed with error, code={code}, desc={desc}.");
            }));
    }

    void FetchRoomMuteListAction()
    {
        string roomId = RecvIdField.text;
        int pageNum = 1;
        int pageSize = 200;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.FetchRoomMuteList(roomId, pageNum, pageSize,
            new ValueCallBack<List<string>>(onSuccess: (List<string> muteList) =>
            {
                int i = 0;
                foreach (string item in muteList)
                {
                    Debug.Log($"Fetch muteList of room {roomId}, item{i}:{item}.");
                    i++;
                }

            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch muteList of room {roomId} failed with code={code}, desc={desc}");
            }));
    }

    async void GetChatRoomWithIdAction()
    {
        SendBtn.enabled = false;
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        await Task.Run(() => roomManager.GetChatRoomWithId(roomId,
            new ValueCallBack<Room>(onSuccess: (Room room) =>
            {
                Debug.Log($"Get room with roomId={room.RoomId} roomName={room.Name} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get room with roomId={roomId} failed with code={code}, desc={desc}");
            })));
        SendBtn.enabled = true;
    }

    void JoinRoomAction()
    {
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.JoinRoom(roomId,
            new CallBack(
                onSuccess: () => Debug.Log($"Join room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to join room {roomId} with error: {desc}.")));
    }

    void GetRoomInfoAction()
    {

    }

    void LeaveRoomAction()
    {
        string roomId = RecvIdField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.LeaveRoom(roomId,
            new CallBack(
                onSuccess: () => Debug.Log($"Leave room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to leave room {roomId} with error: {desc}.")));
    }

    void MuteRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.MuteRoomMembers(roomId, members,
            new CallBack(
                onSuccess: () => Debug.Log($"Mute members in room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed mute members in {roomId} with error: {desc}.")));
    }

    void RemoveRoomAdminAction()
    {
        string roomId = RecvIdField.text;
        string adminId = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.RemoveRoomAdmin(roomId, adminId,
            new CallBack(
                onSuccess: () => Debug.Log($"Remove admin {adminId} from room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to remove admin {adminId} room {roomId} with error: {desc}.")));
    }

    void UnBlockRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.UnBlockRoomMembers(roomId, members,
            new CallBack(
                onSuccess: () => Debug.Log($"Unblock members in room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to unblock members in {roomId} with error: {desc}.")));
    }

    void UnMuteRoomMembersAction()
    {
        string roomId = RecvIdField.text;
        List<string> members = new List<string> { "f1", "ys1" };
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.UnMuteRoomMembers(roomId, members,
            new CallBack(
                onSuccess: () => Debug.Log($"Unmute members in room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to unmute members in {roomId} with error: {desc}.")));
    }

    void UpdateRoomAnnouncementAction()
    {
        string roomId = RecvIdField.text;
        string newAnnouncement = TextField.text;
        IRoomManager roomManager = SDKClient.Instance.RoomManager;
        roomManager.UpdateRoomAnnouncement(roomId, newAnnouncement,
            new CallBack(
                onSuccess: () => Debug.Log($"Update room annoucement {newAnnouncement} to room {roomId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to update announcement to room {roomId} with error: {desc}.")));
    }

    //groupmanager
    void CreateGroupAction()
    {
        string groupName = RecvIdField.text;
        string description = TextField.text;
        List<string> inviteMembers = new List<string> { "f1", "ys1" };
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
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.LeaveGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Leave group {groupId} success."),
                onError: (int code, string desc) => Debug.LogError($"Failed to leave group {groupId} with error: {desc}.")));

        //used for logout and scene change!
        //SDKClient.Instance.Logout(false);
        //SceneManager.LoadScene("Login");
    }

    void MuteAllMembersAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.MuteAllMembers(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Mute all group {groupId} members success."),
                onError: (int code, string desc) => Debug.LogError($"Failed to mute group {groupId} with error: {desc}.")));
    }

    void AcceptInvitationFromGroupAction()
    {
        string groupId = RecvIdField.text;
        string inviter = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.AcceptInvitationFromGroup(groupId, inviter,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Accept invitation from inviter {inviter} of group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Accept invitation from inviter {inviter} of group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void AcceptJoinApplicationAction()
    {
        string groupId = RecvIdField.text;
        string username = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.AcceptJoinApplication(groupId, username,
            new CallBack(
                onSuccess: () => Debug.Log($"Accept join application from username {username} of group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to accept application from username {username} of group {groupId} with error: {desc}.")));
    }

    void AddWhiteListAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.AddMembers(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Add {member} into whitelist {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to add member {member} to whitelist {groupId} with error: {desc}.")));
    }

    void BlockGroupAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.BlockGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Block group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to block group {groupId} with error: {desc}.")));
    }

    void BlockMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.BlockMembers(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Block {member} in {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to block member {member} to whitelist {groupId} with error: {desc}.")));
    }

    void ChangeGroupDescriptionAction()
    {
        string groupId = RecvIdField.text;
        string desc = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.ChangeGroupDescription(groupId, desc,
            new CallBack(
                onSuccess: () => Debug.Log($"change group {groupId} description {desc} successfully."),
                onError: (int code, string errDesc) => Debug.LogError($"Failed to change group {groupId} description with error: {errDesc}.")));
    }


    void ChangeGroupOwnerAction()
    {
        string groupId = RecvIdField.text;
        string newOwner = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.ChangeGroupOwner(groupId, newOwner,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Change owner {newOwner} of group {group.GroupId} successfully.");
                if (group.AdminList.Count > 0)
                {
                    Debug.Log($"Admin[0]={group.AdminList[0]}");
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Change owner {newOwner} of group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void CheckIfInGroupWhiteListAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.CheckIfInGroupWhiteList(groupId, 
            new ValueCallBack<bool>(onSuccess: (bool ret) =>
            {
                if(ret)
                    Debug.Log($"CheckIfInGroupWhiteListf for group {groupId} is true.");
                else
                    Debug.Log($"CheckIfInGroupWhiteListf for group {groupId} is false.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"CheckIfInGroupWhiteListf for group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void DeclineInvitationFromGroupAction()
    {
        string groupId = RecvIdField.text;
        string username = TextField.text;
        string reason = "testing";
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.DeclineInvitationFromGroup(groupId, username, reason,
            new CallBack(
                onSuccess: () => Debug.Log($"Decline invitation from user {username} in group {groupId} with reason:{reason} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to decline invitation from user {username} in group {groupId}.")));
    }

    void DeclineJoinApplicationAction()
    {
        string groupId = RecvIdField.text;
        string username = TextField.text;
        string reason = "testing";
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.DeclineJoinApplication(groupId, username, reason,
            new CallBack(
                onSuccess: () => Debug.Log($"Decline invitation of application from user {username} in group {groupId} with reason:{reason} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to decline invitation of application from user {username} in group {groupId}.")));
    }

    void GetGroupAnnouncementFromServerAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupAnnouncementFromServer(groupId,
            new ValueCallBack<string>(onSuccess: (string ret) =>
            {
                Debug.Log($"Get from group {groupId}, announcement:{ret}.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get annnouncement from group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupBlockListFromServerAction()
    {
        string groupId = RecvIdField.text;
        int pageNum = 1;
        int pageSize = 200;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupBlockListFromServer(groupId, pageNum, pageSize,
            new ValueCallBack<List<string>>(onSuccess: (List<string> banList) =>
            {
                int i = 0;
                foreach(string banItem in banList)
                {
                    Debug.Log($"Get banList from group {groupId}, item{i}:{banItem}.");
                    i++;
                }
                
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get annnouncement from group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupFileListFromServerAction()
    {
        string groupId = RecvIdField.text;
        int pageNum = 1;
        int pageSize = 200;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupFileListFromServer(groupId, pageNum, pageSize,
            new ValueCallBack<List<GroupSharedFile>>(onSuccess: (List<GroupSharedFile> fileList) =>
            {
                int i = 0;
                foreach (GroupSharedFile file in fileList)
                {
                    Debug.Log($"Get shared file from group {groupId}, item{i}, filename:{file.FileName}, fileid:{file.FileId}.");
                    i++;
                }

            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get shared files from group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupMemberListFromServerAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupMemberListFromServer(groupId, 200, "", 
            new ValueCallBack<CursorResult<string>>(onSuccess: (CursorResult<string> cursorResult) =>
            {
                Debug.Log($"Get group member list with next cursor: {cursorResult.Cursor}");
                foreach (var item in cursorResult.Data)
                {
                    Debug.Log($"member: {item}");
                }
            }, onError: (int code, string desc) =>
            {
                Debug.LogError($"Group member list from server failed with error, code={code}, desc={desc}.");
            }));
    }

    void GetGroupMuteListFromServerAction()
    {
        string groupId = RecvIdField.text;
        int pageNum = 1;
        int pageSize = 200;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupMuteListFromServer(groupId, pageNum, pageSize,
            new ValueCallBack<List<string>>(onSuccess: (List<string> muteList) =>
            {
                int i = 0;
                foreach (string muteItem in muteList)
                {
                    Debug.Log($"Get muteList of group {groupId}, item{i}:{muteItem}.");
                    i++;
                }

            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get muteList from group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupSpecificationFromServerAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupSpecificationFromServer(groupId,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Get group specification of group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get group specification of group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupsWithoutNoticeAction()
    {
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupsWithoutNotice(
            new ValueCallBack<List<string>>(onSuccess: (List<string> groupList) =>
            {
                int i = 0;
                foreach (string group in groupList)
                {
                    Debug.Log($"Get a group without notice : {group}.");
                    i++;
                }

            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get group without notice failed with code={code}, desc={desc}");
            }));
    }

    void GetGroupWhiteListFromServerAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetGroupWhiteListFromServer(groupId,
            new ValueCallBack<List<string>>(onSuccess: (List<string> whiteList) =>
            {
                int i = 0;
                foreach (string member in whiteList)
                {
                    Debug.Log($"{member} is in whiteList.");
                    i++;
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get group without notice failed with code={code}, desc={desc}");
            }));
    }

    void GetJoinedGroupsAction()
    {
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetJoinedGroups(
            new ValueCallBack<List<Group>>(onSuccess: (List<Group> groupList) =>
            {
                int i = 0;
                foreach (Group group in groupList)
                {
                    Debug.Log($"Get joined group{i} with groupid:{group.GroupId}");
                    i++;
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get joined groups failed with code={code}, desc={desc}");
            }));
    }

    void GetJoinedGroupsFromServerAction()
    {
        int pageNum = 1;
        int pageSize = 200;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetJoinedGroupsFromServer(pageNum, pageSize,
            new ValueCallBack<List<Group>>(onSuccess: (List<Group> groupList) =>
            {
                int i = 0;
                foreach (Group group in groupList)
                {
                    Debug.Log($"Get joined group{i} from server with groupid:{group.GroupId}");
                    i++;
                }
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Get joined groups from server failed with code={code}, desc={desc}");
            }));
    }

    void GetPublicGroupsFromServerAction()
    {
        string cursor = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.GetPublicGroupsFromServer(200, cursor,
            new ValueCallBack<CursorResult<GroupInfo>>(onSuccess: (CursorResult<GroupInfo> cursorResult) =>
            {
                Debug.Log($"Get public groups from server with next cursor: {cursorResult.Cursor}");
                foreach (var item in cursorResult.Data)
                {
                    Debug.Log($"public groupid: {item.GroupId}, name:{item.GroupName}");
                }
            }, onError: (int code, string desc) =>
            {
                Debug.LogError($"Group public groups from server failed with error, code={code}, desc={desc}.");
            }));
    }

    void JoinPublicGroupAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.DestroyGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Join public group {groupId} success."),
                onError: (int code, string desc) => Debug.LogError($"Failed to join public group {groupId} with error: {desc}.")));
    }

    void MuteMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.MuteMembers(groupId, new List<string> { member },
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Mute member {member} in group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Mute member {member} in group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void RemoveAdminAction()
    {
        string groupId = RecvIdField.text;
        string admin = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.RemoveAdmin(groupId, admin,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Remove admin:{admin} from group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Failed to remove admin:{admin} from group failed with error code={code}, desc={desc}");
            }));
    }

    void RemoveGroupSharedFileAction()
    {
        string groupId = RecvIdField.text;
        string fileId = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.RemoveGroupSharedFile(groupId, fileId,
            new CallBack(
                onSuccess: () => Debug.Log($"Remove group shard file {fileId} from group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to remove group shared file {fileId} from group {groupId} with error: {desc}.")));
    }

    void RemoveWhiteListAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.RemoveWhiteList(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Remove whileList {member} from group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to remove whileList {member} from group {groupId} with error: {desc}.")));
    }

    void RequestToJoinPublicGroupAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.RequestToJoinPublicGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Request to join public group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to join public group {groupId} with error: {desc}.")));
    }

    void UnblockGroupAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UnblockGroup(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Unblock group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to unblock group {groupId} with error: {desc}.")));
    }

    void UnblockMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UnblockMembers(groupId, new List<string> { member },
            new CallBack(
                onSuccess: () => Debug.Log($"Unblock member {member} from group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to unblock {member} from group {groupId} with error: {desc}.")));
    }

    void UnMuteAllMembersAction()
    {
        string groupId = RecvIdField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UnMuteAllMembers(groupId,
            new CallBack(
                onSuccess: () => Debug.Log($"Unmute group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to unmute group {groupId} with error: {desc}.")));
    }

    void UnMuteMembersAction()
    {
        string groupId = RecvIdField.text;
        string member = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UnMuteMembers(groupId, new List<string> { member },
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Unmute member {member} in group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Unmute member {member} in group {groupId} failed with code={code}, desc={desc}");
            }));
    }

    void UpdateGroupAnnouncementAction()
    {
        string groupId = RecvIdField.text;
        string announcement = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UpdateGroupAnnouncement(groupId, announcement,
            new CallBack(
                onSuccess: () => Debug.Log($"Update group {groupId} with annoucement {announcement} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to update announcement for group {groupId} with error: {desc}.")));
    }

    void UpdateGroupExtAction()
    {
        string groupId = RecvIdField.text;
        string newExtension = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UpdateGroupExt(groupId, newExtension,
            new ValueCallBack<Group>(onSuccess: (Group group) =>
            {
                Debug.Log($"Update ext:{newExtension} for group {group.GroupId} successfully.");
            },
            onError: (int code, string desc) =>
            {
                Debug.LogError($"Failed to update ext:{newExtension} for group failed with error code={code}, desc={desc}");
            }));
    }

    void UploadGroupSharedFileAction()
    {
        string groupId = RecvIdField.text;
        string filePath = TextField.text;
        IGroupManager groupManager = SDKClient.Instance.GroupManager;
        groupManager.UploadGroupSharedFile(groupId, filePath,
            new CallBack(
                onSuccess: () => Debug.Log($"Upload group shared file {filePath} to group {groupId} successfully."),
                onError: (int code, string desc) => Debug.LogError($"Failed to upload group shared file {filePath} to group {groupId} with error: {desc}.")));
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

    //ConversationManager related
    void AppendMessageAction()
    {
        string conversationId = RecvIdField.text;
        Message message = Message.CreateTextSendMessage("yqtest", "hello world");
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        if(conversationManager.AppendMessage(conversationId, ConversationType.Chat, message))
        {
            Debug.Log($"AppendMessage for conversation {conversationId} with message {message.MsgId} successfully.");
        }
        else
        {
            Debug.LogError($"AppendMessage for conversation {conversationId} with message {message.MsgId} failed.");
        }
    }

    void DeleteAllMessagesAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        if (conversationManager.DeleteAllMessages(conversationId, ConversationType.Chat))
        {
            Debug.Log($"DeleteAllMessage for conversation {conversationId} successfully.");
        }
        else
        {
            Debug.LogError($"DeleteAllMessage for conversation {conversationId} failed.");
        }
    }

    void DeleteMessageAction()
    {
        string conversationId = RecvIdField.text;
        string messageId = TextField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        if (conversationManager.DeleteMessage(conversationId, ConversationType.Chat, messageId))
        {
            Debug.Log($"DeleteMessage {messageId} for conversation {conversationId} successfully.");
        }
        else
        {
            Debug.LogError($"DeleteMessage {messageId} for conversation {conversationId} failed.");
        }
    }

    void GetExtAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        Dictionary<string, string> dict = conversationManager.GetExt(conversationId, ConversationType.Chat);
        if (dict.Count > 0)
        {
            foreach(var k in dict)
            {
                Debug.Log($"GetExt conversation {conversationId} successfully: key={k.Key}, value={k.Value}");
            }
            
        }
        else
        {
            Debug.Log($"GetExt for conversation {conversationId} complete, but the ext is empty.");
        }
    }

    void InsertMessageAction()
    {
        string conversationId = RecvIdField.text;
        Message message = Message.CreateTextSendMessage("yqtest", "hello world");
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        if (conversationManager.InsertMessage(conversationId, ConversationType.Chat, message))
        {
            Debug.Log($"InsertMessage for conversation {conversationId} with message {message.MsgId} successfully.");
        }
        else
        {
            Debug.LogError($"InsertMessage for conversation {conversationId} with message {message.MsgId} failed.");
        }
    }

    void LastMessageAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        Message lastMessage = conversationManager.LastMessage(conversationId, ConversationType.Chat);
        Debug.Log($"LastMessage get message id={lastMessage.MsgId} successfully.");
    }

    void LastReceivedMessageAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        Message lastMessage = conversationManager.LastReceivedMessage(conversationId, ConversationType.Chat);
        Debug.Log($"LastReceivedMessage get message id={lastMessage.MsgId} successfully.");
    }

    void ConversationManagerLoadMessageAction()
    {
        string conversationId = RecvIdField.text;
        string messageId = TextField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        Message message = conversationManager.LoadMessage(conversationId, ConversationType.Chat, messageId);
        Debug.Log($"LoadMessage return message id={message.MsgId} successfully.");
    }

    void LoadMessagesAction()
    {
        string conversationId = RecvIdField.text;
        string startMessageId = TextField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        List<Message> msgList = conversationManager.LoadMessages(conversationId, ConversationType.Chat, startMessageId, 20, MessageSearchDirection.UP);
        foreach(Message msg in msgList)
        {
            Debug.Log($"LoadMessages return message id={msg.MsgId} successfully.");
        }
    }

    void LoadMessagesWithKeywordAction()
    {
        string conversationId = RecvIdField.text;
        string keywords = TextField.text;
        string sender = keywords; // need more input field on UI

        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;

        //(string conversationId, ConversationType converationType, string keywords, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        List<Message> msgList = conversationManager.LoadMessagesWithKeyword(conversationId, ConversationType.Chat, keywords, sender);
        foreach (Message msg in msgList)
        {
            Debug.Log($"LoadMessagesWithKeywordAction return message id={msg.MsgId} successfully.");
        }
    }

    void LoadMessagesWithMsgTypeAction()
    {
        string conversationId = RecvIdField.text;
        string sender = TextField.text;

        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;

        //(string conversationId, ConversationType conversationType, MessageBodyType bodyType, string sender, long timestamp = -1, int count = 20, MessageSearchDirection direction = MessageSearchDirection.UP)
        List<Message> msgList = conversationManager.LoadMessagesWithMsgType(conversationId, ConversationType.Chat, MessageBodyType.TXT, sender);
        foreach (Message msg in msgList)
        {
            Debug.Log($"LoadMessagesWithMsgType return message id={msg.MsgId} successfully.");
        }
    }

    void LoadMessagesWithTimeAction()
    {
        string conversationId = RecvIdField.text;
        string startTimeStr = TextField.text;
        long startTime = long.Parse(startTimeStr);
        long endTime = startTime + 100;

        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;

        //(string conversationId, ConversationType converationType, long startTime, long endTime, int count = 20)
        List<Message> msgList = conversationManager.LoadMessagesWithTime(conversationId, ConversationType.Chat, startTime, endTime);
        foreach (Message msg in msgList)
        {
            Debug.Log($"LoadMessagesWithTime return message id={msg.MsgId} successfully.");
        }
    }

    void MarkAllMessageAsReadAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        conversationManager.MarkAllMessageAsRead(conversationId, ConversationType.Chat);
        Debug.Log($"MarkAllMessageAsRead for conversation {conversationId} successfully.");
    }

    void MarkMessageAsReadAction()
    {
        string conversationId = RecvIdField.text;
        string messageId = TextField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        conversationManager.MarkMessageAsRead(conversationId, ConversationType.Chat, messageId);
        Debug.Log($"MarkMessageAsRead messageId {messageId} for conversation {conversationId} successfully.");
    }

    void SetExtAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        Dictionary<string, string> dict = new Dictionary<string, string>{{ "city", "Beijing" },{ "date","20210803" } };
        conversationManager.SetExt(conversationId, ConversationType.Chat, dict);
        Debug.Log($"SetExt for conversation {conversationId} successfully.");
    }

    void UnReadCountAction()
    {
        string conversationId = RecvIdField.text;
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        int count = conversationManager.UnReadCount(conversationId, ConversationType.Chat);
        Debug.Log($"UnReadCount return {count} unread messages for conversation {conversationId}.");
    }

    void ConversationManagerUpdateMessageAction()
    {
        string conversationId = RecvIdField.text;
        Message message = Message.CreateTextSendMessage("yqtest", "hello world");
        IConversationManager conversationManager = SDKClient.Instance.ConversationManager;
        if (conversationManager.UpdateMessage(conversationId, ConversationType.Chat, message))
        {
            Debug.Log($"UpdateMessage for conversation {conversationId} with message {message.MsgId} successfully.");
        }
        else
        {
            Debug.LogError($"UpdateMessage for conversation {conversationId} with message {message.MsgId} failed.");
        }
    }
}

