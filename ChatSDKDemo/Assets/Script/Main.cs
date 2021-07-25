using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChatSDK;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour , IConnectionDelegate, IChatManagerDelegate, IContactManagerDelegate, IGroupManagerDelegate, IRoomManagerDelegate
{
    // 接收消息id
    public Text InputText;
    public Button Logout;

    public Button SendBtn;
    public Button RecallBtn;
    public Button ResendBtn;
    public Button SearchBtn;
    public Button UpdateBtn;
    public Button SendReadBtn;
    public Button DownAttBtn;
    public Button DownAttDBtn;
    public Button IsConnectBtn;
    public Button CurrentUsernameBtn;
    public Button AccessTokenBtn;
    public Button IsLoggedInBtn;
    public Button ConversationBtn;
    public Button GroupBtn;
    public Button RoomBtn;
    public Button PushBtn;

    public Button ContactBtn;
    public Button Custom1Btn;
    public Button Custom2Btn;
    public Button Custom3Btn;

    // Start is called before the first frame update
    void Start()
    {
        Logout.onClick.AddListener(LogoutAction);

        SendBtn.onClick.AddListener(SendMsg);
        RecallBtn.onClick.AddListener(RecallMsg);
        ResendBtn.onClick.AddListener(ResendMsg);
        SearchBtn.onClick.AddListener(SearchMsg);

        UpdateBtn.onClick.AddListener(UpdateMsg);
        SendReadBtn.onClick.AddListener(ReadAck);
        DownAttBtn.onClick.AddListener(DownLoadAttachment);
        DownAttDBtn.onClick.AddListener(DownloadThumbnail);

        IsConnectBtn.onClick.AddListener(IsConnectAction);
        CurrentUsernameBtn.onClick.AddListener(CurrentUsernameAction);
        AccessTokenBtn.onClick.AddListener(AccessTokenAction);
        IsLoggedInBtn.onClick.AddListener(IsLoggedinAction);

        ConversationBtn.onClick.AddListener(ToConversationSence);
        GroupBtn.onClick.AddListener(ToGroupSence);
        RoomBtn.onClick.AddListener(ToRoomSence);
        PushBtn.onClick.AddListener(ToPushSence);
        ContactBtn.onClick.AddListener(ToContactSence);
        Custom1Btn.onClick.AddListener(Custom1Action);
        Custom2Btn.onClick.AddListener(Custom2Action);
        Custom3Btn.onClick.AddListener(Custom3Action);

    }

    private void Awake()
    {
        SDKClient.Instance.AddConnectionDelegate(this);
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
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


    void LogoutAction() {

        CallBack callback = new CallBack();

        callback.Success = () =>
        {
            Debug.Log("发送成功");
        };

        callback.Error = (int code, string desc) =>
        {
            Debug.Log("发送失败 -- " + code + desc);
        };

        SDKClient.Instance.ChatManager.SendConversationReadAck("du003", callback);

        //ValueCallBack<List<Conversation>> callback = new ValueCallBack<List<Conversation>>();

        //callback.OnSuccessValue = (List<Conversation> list) => {

        //    foreach (var conversation in list) {
        //        Debug.Log("conv id ---- " + conversation.Id);
        //    }
        //};

        //callback.Error = (int code, string desc) => { Debug.LogError("error code " + code + " " +desc); };

        //List<Message>list = SDKClient.Instance.ChatManager.SearchMsgFromDB("文字", direction: MessageSearchDirection.DOWN);
        //foreach (var msg in list)
        //{
        //    ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;
        //    Debug.Log("message id --- " + msg.MsgId + " " + "content " + textBody.Text);
        //}
    }

    void SendMsg() {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("unity 成功");
            },

            onError: (code, desc) => {
                Debug.LogError("unity 失败 code " + code + " " + desc);
            }
        );

        string to = InputText.text;
        Message msg = Message.CreateTextSendMessage(to, "文字消息");
        SDKClient.Instance.ChatManager.SendMessage(msg, callBack);
    }

    void RecallMsg() {

        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("unity 成功");
            },

            onError: (code, desc) => {
                Debug.LogError("unity 失败 code " + code + " " + desc);
            }
        );

        string msgId = InputText.text;
        SDKClient.Instance.ChatManager.RecallMessage(msgId, callBack);
    }

    void ResendMsg() {

        CallBack callBack = new CallBack(
           onSuccess: () => {
               Debug.Log("unity 成功");
           },

           onError: (code, desc) => {
               Debug.LogError("unity 失败 code " + code + " " + desc);
           }
       );

        string msgId = InputText.text;
        SDKClient.Instance.ChatManager.ResendMessage(msgId, callBack);
    }

    void SearchMsg() {

        string keywords = InputText.text;
        List<Message>list = SDKClient.Instance.ChatManager.SearchMsgFromDB(keywords);

        foreach (var msg in list) {
            if (msg.Body.Type == MessageBodyType.TXT) {
                ChatSDK.MessageBody.TextBody body = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log("搜索消息 -- " + body.Text);
            }
        }

    }

    void UpdateMsg() {
        string msgId = InputText.text;
        Message msg = SDKClient.Instance.ChatManager.LoadMessage(msgId);
        msg.Body = new ChatSDK.MessageBody.TextBody("我是更新的消息");
        bool ret = SDKClient.Instance.ChatManager.UpdateMessage(msg);
        if (ret)
        {
            Debug.Log("更新成功");
        }
        else {
            Debug.LogError("更新失败");
        }
    }

    void ReadAck() {

        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("unity 成功");
            },

            onError: (code, desc) => {
                Debug.LogError("unity 失败 code " + code + " " + desc);
            }
        );

        string msgId = InputText.text;
        SDKClient.Instance.ChatManager.SendMessageReadAck(msgId, callBack);
    }

    void DownLoadAttachment() {

        CallBack callBack = new CallBack(
           onSuccess: () => {
               Debug.Log("unity 成功");
           },

           onError: (code, desc) => {
               Debug.LogError("unity 失败 code " + code + " " + desc);
           },

           onProgress: (progress) => {
               Debug.Log("unity 下载 --- " + progress);
           }
       );

        string msgId = InputText.text;
        SDKClient.Instance.ChatManager.DownloadAttachment(msgId, callBack);
    }

    void DownloadThumbnail() {
        CallBack callBack = new CallBack(
           onSuccess: () => {
               Debug.Log("unity 成功");
           },

           onError: (code, desc) => {
               Debug.LogError("unity 失败 code " + code + " " + desc);
           },

           onProgress: (progress) => {
               Debug.Log("unity 下载 --- " + progress);
           }
       );

        string msgId = InputText.text;
        SDKClient.Instance.ChatManager.DownloadThumbnail(msgId, callBack);
    }

    void IsConnectAction()
    {
        bool ret = SDKClient.Instance.IsConnected;
        if (ret)
        {
            Debug.Log("已连接");
        }
        else {
            Debug.Log("连接失败");
        }
    }

    void CurrentUsernameAction()
    {
        string ret = SDKClient.Instance.CurrentUsername;
        if (ret != null)
        {
            Debug.Log("当前登录账号 -- " + ret);
        }
        else
        {
            Debug.Log("当前登录账号获取失败");
        }
    }

    void AccessTokenAction() {
        string ret = SDKClient.Instance.AccessToken;
        if (ret != null)
        {
            Debug.Log("accessToken -- " + ret);
        }
        else
        {
            Debug.Log("get accessToken failed");
        }
    }

    void IsLoggedinAction() {
        bool ret = SDKClient.Instance.IsLoggedIn;
        if (ret)
        {
            Debug.Log("has loggedin");
        }
        else {
            Debug.Log("has no loggedin");
        }
    }


    void ToConversationSence() {
        SceneManager.LoadScene("Conversation");
    }

    void ToGroupSence() {
        SceneManager.LoadScene("GroupManager");
    }

    void ToRoomSence() {
        SceneManager.LoadScene("RoomManager");
    }

    void ToPushSence() {
        SceneManager.LoadScene("PushManager");
    }


    void ToContactSence() {
        SceneManager.LoadScene("Contact");
    }

    void Custom1Action() { }
    void Custom2Action() { }
    void Custom3Action() { }



    void SendMessageAction()
    {

        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("unity 消息发送成功");
            },

            onError: (code, desc)=> {
                Debug.Log("unity 消息发送失败");
            }
        );

        Message msg = Message.CreateTextSendMessage("du003", "文字消息");
        SDKClient.Instance.ChatManager.SendMessage(msg, callBack);

    }

    void JoinGroupAction()
    {
        SDKClient.Instance.RoomManager.CreateRoom("uniaa", null, null, 200, null,null );

    }

    void GetGroupInfoAction()
    {

        ValueCallBack<Group> callBack = new ValueCallBack<Group>(
            onSuccess: (group) => {
                Debug.Log("创建群组成功，群组id " + group.GroupId);
            },
            onError:(code, desc) => {
                Debug.Log("创建群组失败 " + code + " desc " + desc);
            }
        );

        GroupOptions groupOptions = new GroupOptions(GroupStyle.PrivateMemberCanInvite);
        SDKClient.Instance.GroupManager.CreateGroup("群组名称", groupOptions, desc: "群描述", handle: callBack);
    }

    void LeaveGroupAction()
    {
        ValueCallBack<PageResult<Room>> callback = new ValueCallBack<PageResult<Room>>();
        callback.OnSuccessValue = (PageResult<Room> result) => {
            foreach (var room in result.Data) {
                Debug.Log("聊天室id " + room.RoomId);
            }
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("获取列表失败 " + code + " desc " + desc);
        };

        // 从服务器获取聊天室列表
        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle:callback);

    }

    void JoinRoomAction()
    {
        string roomId = "";

        ValueCallBack<Room> callback = new ValueCallBack<Room>();
        callback.OnSuccessValue = (Room room) => {
            Debug.Log("加入成功");
        };

        callback.Error = (int code, string desc) =>
        {
            Debug.Log("加入聊天室失败 " + code + " desc " + desc);
        };

        SDKClient.Instance.RoomManager.JoinRoom(roomId, callback);
    }

    void GetRoomInfoAction()
    {
        ValueCallBack<PushConfig> callback = new ValueCallBack<PushConfig>();
        callback.OnSuccessValue = (PushConfig config) => {
            Debug.Log("获取推送配置成功");
        };

        callback.Error = (int code, string desc) =>
        {
            Debug.Log("获取推送配置失败 " + code + " desc " + desc);
        };
        SDKClient.Instance.PushManager.GetPushConfigFromServer(callback);
    }

    void LeaveRoomAction()
    {
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("设置免打扰成功");
        };
        callBack.Error = (int code, string desc) =>
        {
            Debug.Log("设置免打扰失败 " + code + " desc " + desc);
        };
        SDKClient.Instance.PushManager.SetNoDisturb(true, 22, 8, callBack);
    }


    // 发送文字消息
    void SendTextMessage() {

        Message msg = Message.CreateTextSendMessage("du003", "我是文字消息");

        CallBack callBack = new CallBack(

            onSuccess: () => {
                Debug.Log("发送成功");
            },

            onError: (code, desc) => {
                Debug.LogError("发送失败: " + code + "desc: " + desc);
            }

            );

        SDKClient.Instance.ChatManager.SendMessage(msg, callBack);
    }

    // 获取与xxx的会话
    void GetConversation() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Debug.Log("conversation id: " + conv.Id);
    }

    void GetUnreadCount() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Debug.Log("unread count --- " + conv.UnReadCount);
    }

    void GetLatestReceiveMessage() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Message msg = conv.LastReceivedMessage;
        if (msg != null)
        {
            if (msg.Body.Type == MessageBodyType.TXT)
            {
                ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log("lataestReceive message: " + textBody.Text);
            }
        }
    }

    void GetLatestMessage() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Message msg = conv.LastMessage;
        if (msg != null)
        {
            if (msg.Body.Type == MessageBodyType.TXT)
            {
                ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log("latest message: " + textBody.Text);
            }
        }
    }

    void MarkAllMessageAsRead() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        conv.MarkAllMessageAsRead();
    }

    void DeleteLastMessage() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Message msg = conv.LastMessage;
        conv.DeleteMessage(msg.MsgId);
    }

    void DeleteAllMessages() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        conv.DeleteAllMessages();
    }

    void LoadMessages() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        List<Message> list = conv.LoadMessages(null);
        Debug.Log("load messsage count --- " + list.Count);
    }

    void MakeAllConversationAsRead() {
        SDKClient.Instance.ChatManager.MarkAllConversationsAsRead();
    }

    void GetAllMessageUnReadCount() {
        int count = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
        Debug.Log("all unread count --- " + count);
    }

    void InsertConversationMessage() {
        Message msg = Message.CreateTextSendMessage("du003", "我是会话插入的消息");
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        conv.InsertMessage(msg);
    }

    void UpdateChatMangerMessage(Message msg) {
        ChatSDK.MessageBody.TextBody textBody = new ChatSDK.MessageBody.TextBody("我是更新的消息");
        msg.Body = textBody;

        bool ret = SDKClient.Instance.ChatManager.UpdateMessage(msg);
        if (ret)
        {
            Debug.Log("更新成功");
        }
        else {
            Debug.LogError("更新失败");
        }
    }

    void AppendConversationMessage()
    {
        Message msg = Message.CreateTextSendMessage("du003", "我是会话Append的消息");
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        bool ret = conv.AppendMessage(msg);
        Debug.Log("插入 " + (ret ? "成功" : "失败"));

    }

    void UpdateConversationMessage() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Message msg = conv.LastMessage;
        ChatSDK.MessageBody.TextBody body = new ChatSDK.MessageBody.TextBody("我是Conversation更新的消息");
        msg.Body = body;

        conv.UpdateMessage(msg);
    }

    void InsertChatManagerMessage() {
        Message msg = Message.CreateTextSendMessage("du003", "我是Chat插入的消息");
        List<Message> list = new List<Message>();
        list.Add(msg);
        SDKClient.Instance.ChatManager.ImportMessages(list);
    }

    void RecallMessage(Message msg) {
        SDKClient.Instance.ChatManager.RecallMessage(msg.MsgId);
    }

    void SetConversationExt() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("keyaaa", "valuebbb");
        conv.Ext = dict;
    }

    void GetConversationExt() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        foreach (string key in conv.Ext.Keys) {
            Debug.Log("ext --- " + conv.Ext[key]);
        }
    }

    void LoadConversationMessagesWithKeyword(string str) {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        List<Message> msgs = conv.LoadMessagesWithKeyword(str, null);
        foreach (Message msg in msgs) {
            if (msg.Body.Type == MessageBodyType.TXT)
            {
                ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log("search message: " + textBody.Text);
            }
        }
    }

    void LoadConversationMessagesWithType() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
        List<Message> msgs = conv.LoadMessagesWithMsgType(MessageBodyType.TXT, null);
        foreach (Message msg in msgs)
        {
            if (msg.Body.Type == MessageBodyType.TXT)
            {
                ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log("type message: " + textBody.Text);
            }
        }
    }

    void LoadConversationMessageWithType() {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation("du003");
    }


    ///// room

    void CreateRoom() {


        ValueCallBack<Room> callback = new ValueCallBack<Room>(

            onSuccess: (room) =>
            {
                Debug.Log("room --- " + room.RoomId);
            },

            onError: (code, desc) => {
                Debug.Log("room ----------- 失败 code " + code + "  desc " + desc);
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");
        members.Add("du004");

        SDKClient.Instance.RoomManager.CreateRoom("unun1", "descaaaa", members: members, handle: callback);
    }

    void DestroyRoom() {


        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("解散成功");
            },
            onError:(code, desc) => {
                Debug.Log("解散失败 --- " + code + " " + desc);
            }
        );

        SDKClient.Instance.RoomManager.DestroyRoom("154157521633281", callback);
    }

    void FetchPublicRooms() {

        ValueCallBack<PageResult<Room>> callback = new ValueCallBack<PageResult<Room>>(

            onSuccess: (result) => {
                foreach (Room room in result.Data) {
                    Debug.Log("room --- " + room.RoomId);
                    //currRoom = room;
                }
            }
            );

        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle: callback);
    }

    void JoinRoom(string roomId) {

        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
            onSuccess: (room) => {
                Debug.Log("room ----------- 加入成功 " + room.RoomId);
            }
            );

        SDKClient.Instance.RoomManager.JoinRoom(roomId, callBack);
    }

    void LeaveRoom(string roomId) {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 离开成功");
            }
        );

        SDKClient.Instance.RoomManager.LeaveRoom(roomId, callBack);
    }

    void FetchRoomInfo(string roomId) {

        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
            onSuccess: (room) => {
                Debug.Log("room ----------- 详情成功 " + room.RoomId);
                Debug.Log("room ----------- 详情成功 " + room.Name);
                Debug.Log("room ----------- 详情成功 " + room.Description);
                Debug.Log("room ----------- 详情成功 " + room.Announcement);
                foreach (string s in room.MemberList) {
                    Debug.Log("room ----------- 成员 " + s);
                }

                foreach (string s in room.AdminList)
                {
                    Debug.Log("room ----------- 管理员 " + s);
                }

                foreach (string s in room.MuteList)
                {
                    Debug.Log("room ----------- 禁言 " + s);
                }
            }
            );

        SDKClient.Instance.RoomManager.FetchRoomInfoFromServer(roomId, callBack);
    }

    void FetchRoomMemberList(string roomId) {

        ValueCallBack<CursorResult<string>> callBack = new ValueCallBack<CursorResult<string>>(
                onSuccess: (result) => {
                    foreach (string s in result.Data) {
                        Debug.Log("username --- " + s);
                    }
                }
            );

        SDKClient.Instance.RoomManager.FetchRoomMembers(roomId, handle: callBack);
    }


    void AddRoomAdmin() {

        //153983136104449

        ValueCallBack<Room> callback = new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Debug.Log("管理员列表添加 ===== ");
                    foreach (string s in room.AdminList) {
                        Debug.Log("管理员列表 ===== " + s);
                    }
                }

            );

        SDKClient.Instance.RoomManager.AddRoomAdmin("153983136104449", "du003", callback);
    }

    void RemoveRoomAdmin() {
        ValueCallBack<Room> callback = new ValueCallBack<Room>(
                onSuccess: (room) =>
                {
                    Debug.Log("管理员列表删除 ===== ");
                }
        );

                    
        SDKClient.Instance.RoomManager.RemoveRoomAdmin("153983136104449", "du003", callback);
    }

    void ChangeRoomDesc() {
        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Debug.Log("room decs --- " + room.Description);
                }
        );

        SDKClient.Instance.RoomManager.ChangeRoomDescription("153983136104449", "我是聊天室描述", callBack);
    }

    void ChangeRoomName() {

        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Debug.Log("room name --- " + room.Name);
                }
        );

        SDKClient.Instance.RoomManager.ChangeRoomName("153983136104449", "我是聊天室名称", callBack);

    }

    void UpdateRoomAnnouncement() {

        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- Announcement成功");
            }
        );

        SDKClient.Instance.RoomManager.UpdateRoomAnnouncement("153983136104449", "我是Announcement4444", callBack);
    }

    void GetRoomAnnouncement()
    {
        ValueCallBack<string> callBack = new ValueCallBack<string>(
            onSuccess: (Announcement) => {
                Debug.Log("room Announcement --- " + Announcement);
            }
        );

        SDKClient.Instance.RoomManager.FetchRoomAnnouncement("153983136104449", callBack);
    }

    void BlockRoomMembers() {

        ValueCallBack<Room> callback = new ValueCallBack<Room>(
             onSuccess: (room) => {

                 foreach (string s in room.BlockList)
                 {
                     Debug.Log("block user --- " + s);
                 }
             },

             onError: (code, desc) => {
                 Debug.Log("block user errir --- " + code + " desc " + desc);
             }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.BlockRoomMembers("153983136104449", members, callback);
    }

    void UnBlockRoomMembers()
    {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 移除黑名单");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.UnBlockRoomMembers("153983136104449", members, callBack);

    }

    void FetchRoomBlockList() {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>(
            onSuccess: (list) => {

                foreach (string s in list) {
                    Debug.Log("block user --- " + s);
                }
            }
        );

        SDKClient.Instance.RoomManager.FetchRoomBlockList("153983136104449", handle: callback);
    }

    void MuteRoomMembers() {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 成员禁言");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.MuteRoomMembers("153983136104449", members, callBack);
    }

    void UnMuteRoomMembers() {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 成员禁言");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.UnMuteRoomMembers("153983136104449", members, callBack);
    }

    void FetchRoomMuteList() {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>(
            onSuccess: (list) => {

                foreach (string s in list)
                {
                    Debug.Log("禁言 user --- " + s);
                }
            }
        );

        SDKClient.Instance.RoomManager.FetchRoomMuteList("153983136104449", handle: callback);
    }

    void RemoveRoomMembers() {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 移除成功");
            },

            onError: (code, desc) => {
                Debug.Log("room ----------- 移除失败 code " + code + "  desc " + desc);
            }
        );

        List<string> members = new List<string>();
        members.Add("du004");
        SDKClient.Instance.RoomManager.RemoveRoomMembers("153983136104449", members, callBack);

    }

    void GetAllRoomsFromLocal() {

        ValueCallBack<List<Room>> callback = new ValueCallBack<List<Room>>(
            onSuccess: (list) => {
                foreach (Room room in list) {
                    Debug.Log("local room -- " + room.RoomId);
                }
            },
            onError: (code, desc) => {
                Debug.Log("room ----------- 失败 code " + code + "  desc " + desc);
            }
        );



        SDKClient.Instance.RoomManager.GetAllRoomsFromLocal(callback);
    }

    void GetPublicRoomsFromServer() {

        ValueCallBack<PageResult<Room>> callback = new ValueCallBack<PageResult<Room>>(
            onSuccess:(result)=> {
                foreach (Room room in result.Data) {
                    Debug.Log("room id --- " + room.RoomId);
                }
            },
            onError:(code, desc) => {
                Debug.Log("错误 --- " + code + " " + desc);
            }
        );

        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle: callback);
    }

    void ChangeRoomOwner() {

        ValueCallBack<Room> callback = new ValueCallBack<Room>(
            onSuccess: (room) => {
            },
            onError:(code, desc) => {
            }
        );


        SDKClient.Instance.RoomManager.ChangeOwner("", "du004", callback);
    }

    ////// push
    ///
    void GetLocalPushConfig() {
        PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();
        Debug.Log("local config --- " + (config.NoDisturb ? "推送免打扰" : "正常推送"));
        Debug.Log("local config --- " + (config.Style == PushStyle.Simple ? "显示完整内容" : "您有一条新消息"));
        Debug.Log("local config --- 免打扰开始时间 " + config.NoDisturbStartHour);
        Debug.Log("local config --- 免打扰结束时间 " + config.NoDisturbEndHour);
    }

    void GetPushConfigFromServer() {
        ValueCallBack<PushConfig> callback = new ValueCallBack<PushConfig>(
            onSuccess: (config) => {
                Debug.Log("config --- " + (config.NoDisturb ? "推送免打扰" : "正常推送"));
                Debug.Log("config --- " + (config.Style == PushStyle.Simple ? "显示完整内容" : "您有一条新消息"));
                Debug.Log("config --- 免打扰开始时间 " + config.NoDisturbStartHour);
                Debug.Log("config --- 免打扰结束时间 " + config.NoDisturbEndHour);
            }
        );
        SDKClient.Instance.PushManager.GetPushConfigFromServer(callback);
    }

    void SetNoDisturb() {
        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("设置免打扰成功");
            },

            onError: (code, desc) => {
                Debug.Log("设置免打扰失败" + code + " " + desc);
            }
        );
        SDKClient.Instance.PushManager.SetNoDisturb(true, 3, 10, callback);
    }

    void SetPushType() {
        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("设置免打扰成功");
            },

            onError: (code, desc) => {
                Debug.Log("设置免打扰失败" + code + " " + desc);
            }
        );
        PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();

        SDKClient.Instance.PushManager.SetPushStyle(config.Style == PushStyle.Simple ? PushStyle.Summary : PushStyle.Simple, callback);
    }

    void UpdateNickname() {
        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("设置昵称成功");
            },

            onError: (code, desc) => {
                Debug.Log("设置昵称失败" + code + " " + desc);
            }
        );

        SDKClient.Instance.PushManager.UpdatePushNickName("我是新昵称", callback);
    }



    void SetNoDisturbGroups() {

        //137296031580162
        //137600494010369
        //138019701063681
        //138597050155009
        //129975025991681

        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("设置群组免打扰成功");
            },

            onError: (code, desc) => {
                Debug.Log("设置群组免打扰失败 --- " + code + " " + desc);
            }
        );

        SDKClient.Instance.PushManager.SetGroupToDisturb("138019701063681", true, callBack);

    }

    void SetDisturbGroups() {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("设置打扰成功");
            },

            onError: (code, desc) => {
                Debug.Log("设置打扰失败 --- " + code + " " + desc);
            }
        );

        SDKClient.Instance.PushManager.SetGroupToDisturb("138019701063681", false, callBack);
    }

    void GetNoDisturbGroups() {
        List<string>list = SDKClient.Instance.PushManager.GetNoDisturbGroups();
        foreach (string s in list) {
            Debug.Log("disturb group ------- " + s);
        }
    }

    public void OnConnected()
    {
        Debug.Log("连接恢复-------");
    }

    public void OnDisconnected(int i)
    {
        Debug.Log("连接断开------- " + i);
    }

    public void OnPong()
    {
        throw new System.NotImplementedException();
    }

    public void OnMessagesReceived(List<Message> messages)
    {
        Debug.Log("unity ---- 收到消息");
        foreach (var msg in messages) {

            switch (msg.Body.Type) {
                case MessageBodyType.TXT:
                    {
                        ChatSDK.MessageBody.TextBody body = (ChatSDK.MessageBody.TextBody)msg.Body;
                        Debug.Log("unity ---- 文字消息 " + body.Text);

                    }
                    break;
                case MessageBodyType.IMAGE:
                    {
                        ChatSDK.MessageBody.ImageBody body = (ChatSDK.MessageBody.ImageBody)msg.Body;
                        Debug.Log("unity ---- 图片消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 图片消息thumbnai remote path " + body.ThumbnaiRemotePath);
                    }
                    break;
                case MessageBodyType.VOICE:
                    {
                        ChatSDK.MessageBody.VoiceBody body = (ChatSDK.MessageBody.VoiceBody)msg.Body;
                        Debug.Log("unity ---- 短音频消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 短音频消息时长 " + body.Duration);
                    }
                    break;
                case MessageBodyType.VIDEO:
                    {
                        ChatSDK.MessageBody.VideoBody body = (ChatSDK.MessageBody.VideoBody)msg.Body;
                        Debug.Log("unity ---- 短视频消息remote path " + body.RemotePath);
                        Debug.Log("unity ---- 短视频消息时长 " + body.Duration);
                    }
                    break;
                case MessageBodyType.FILE:
                    {
                        Debug.Log("unity ---- 文件消息");
                    }
                    break;
                case MessageBodyType.CUSTOM:
                    {
                        Debug.Log("unity ---- 自定义消息");
                    }
                    break;
                case MessageBodyType.LOCATION:
                    {
                        ChatSDK.MessageBody.LocationBody body = (ChatSDK.MessageBody.LocationBody)msg.Body;
                        Debug.Log("unity ---- 位置消息 " + body.Address);
                        Debug.Log("unity ---- 位置消息 " + body.Latitude);
                        Debug.Log("unity ---- 位置消息 " + body.Longitude);
                    }
                    break;
            }
        }
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        Debug.Log("unity ---- 收到cmd消息");

        foreach (var msg in messages) {
            ChatSDK.MessageBody.CmdBody cmdBody = (ChatSDK.MessageBody.CmdBody)msg.Body;
            Debug.Log("cmd action -- " + cmdBody.Action);
        }

    }

    public void OnMessagesRead(List<Message> messages)
    {
        Debug.Log("unity ---- 收到消息已读");

        foreach (var msg in messages)
        {
            Debug.Log("已读消息id -- " + msg.MsgId);
        }
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        Debug.Log("unity ---- 消息被撤回");

        foreach (var msg in messages)
        {
            Debug.Log("撤回消息id -- " + msg.MsgId);
        }
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        throw new System.NotImplementedException();
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        foreach (var ack in list) {
            Debug.Log("群消息已读id -- " + ack.MsgId);
        }
    }

    public void OnConversationsUpdate()
    {
        Debug.Log("unity ---- 会话列表变化");
        List<Conversation> list = SDKClient.Instance.ChatManager.LoadAllConversations();
        foreach (var conv in list) {
            Debug.Log("unity ---- 会话id ---- " + conv.Id);
        }
    }

    public void OnConversationRead(string from, string to)
    {
        Debug.Log("会话已读");
    }

    //////
    public void OnContactAdded(string username)
    {
        Debug.Log("添加通讯录 ---- username " + username);
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log("删除通讯录 ---- username " + username);
    }

    public void OnContactInvited(string username, string reason)
    {
        Debug.Log("通讯录申请 ---- username " + username);
    }

    public void OnFriendRequestAccepted(string username)
    {
        Debug.Log("通讯录申请被同意 ---- username " + username);
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log("通讯录申请被拒绝 ---- username " + username);
    }

    ///////// group
    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {
        //this.groupId = groupId;
        Debug.Log("收到群组邀请 " + groupId +  " " + groupName);
    }

    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {
        Debug.Log("收到加群申请 " + groupId + " " + groupName);
    }

    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {
        Debug.Log("加群申请被同意 " + groupId + " " + groupName);
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string groupName, string decliner, string reason)
    {
        Debug.Log("加群申请被拒绝 " + groupId + " " + groupName);
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("收到群组邀请被同意 " + groupId + " " + invitee);
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("收到群组邀请被拒绝 " + groupId + " " + invitee);
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        Debug.Log("被群组移除 " + groupId + " " + groupName);
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        Debug.Log("群组解散 " + groupId + " " + groupName);
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log("自动同意群组邀请 " + groupId + " " + inviter);
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, int muteExpire)
    {
        foreach (var username in mutes) {
            Debug.Log("被加入到禁言列表 " + groupId + " " + username);
        }
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        foreach (var username in mutes)
        {
            Debug.Log("从禁言列表中移除 " + groupId + " " + username);
        }
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log("被添加为管理员 " + groupId + " " + administrator);
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log("被移除管理员 " + groupId + " " + administrator);
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log("群主变化 " + groupId + " 新群主 " + newOwner + "  旧群主 " + oldOwner);
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        Debug.Log("进入群 " + groupId + " " + member);
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        Debug.Log("离开群 " + groupId + " " + member);
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        Debug.Log("群描述变化 " + groupId + " " + announcement);
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log("群文件添加 " + groupId + " " + sharedFile.FileId);
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        Debug.Log("群文件移除 " + groupId + " " + fileId);
    }

    ////// room
    ///
    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        Debug.Log("聊天室解散 " + roomId + " " + roomName);
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        Debug.Log("用户加入聊天室 " + roomId + " " + participant);
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("用户离开聊天室 " + roomId + " " + participant);
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("用户被从聊天室删除 " + roomId + " " + roomName + " " + participant);
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        foreach (var s in mutes) {
            Debug.Log("用户被加入禁言 " + roomId + " " + s + " " + expireTime.ToString());
        }
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        foreach (var s in mutes)
        {
            Debug.Log("用户被移除禁言 " + roomId + " " + s);
        }
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        Debug.Log("聊天室添加管理员 " + roomId + " " + admin);
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        Debug.Log("聊天室移除管理员 " + roomId + " " + admin);
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log("聊天室更换房主 " + roomId + " 新房主 " + newOwner + " 旧房主 " + oldOwner);
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        Debug.Log("聊天室描述更新 " + roomId + " " + announcement);
    }

}
