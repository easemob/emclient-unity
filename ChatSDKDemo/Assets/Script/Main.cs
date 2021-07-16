using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

using ChatSDK;

public class Main : MonoBehaviour , IConnectionDelegate
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

    Room currRoom;
    Conversation conversation;


    // Start is called before the first frame update
    void Start()
    {
        SendBtn.onClick.AddListener(SendMessageAction);

        JoinGroupBtn.onClick.AddListener(JoinGroupAction);
        GroupInfoBtn.onClick.AddListener(GetGroupInfoAction);
        LeaveGroupBtn.onClick.AddListener(LeaveGroupAction);

        JoinRoomBtn.onClick.AddListener(JoinRoomAction);
        RoomInfoBtn.onClick.AddListener(GetRoomInfoAction);
        LeaveRoomBtn.onClick.AddListener(LeaveRoomAction);

        ToggleGroup = GameObject.Find("ToggleGroup").GetComponent<ToggleGroup>().ActiveToggles();
        foreach (Toggle tog in ToggleGroup) {
        }
    }

    private void Awake()
    {
        SDKClient.Instance.AddConnectionDelegate(this);
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

    void SendMessageAction()
    {

    }

    void JoinGroupAction()
    {
        CreateRoom();
    }

    void GetGroupInfoAction()
    {
        DestroyRoom();
    }

    void LeaveGroupAction()
    {
        GetAllRoomsFromLocal();
    }

    void JoinRoomAction()
    {
        GetPublicRoomsFromServer();
    }

    void GetRoomInfoAction()
    {

    }

    void LeaveRoomAction()
    {

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

        CallBack callback = new CallBack(
                onSuccess: () => {
                    Debug.Log("插入成功");
                },
                onError: (code, desc) => {
                    Debug.LogError("插入失败 --- " + code + " " + desc);
                }
            );

        SDKClient.Instance.ChatManager.UpdateMessage(msg, callback);
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
                    currRoom = room;
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
                onSuccess: (room) => {
                    Debug.Log("管理员列表删除 ===== ");
                    foreach (string s in room.AdminList)
                    {
                        Debug.Log("管理员列表 ===== " + s);
                    }
                },
                onError: (code, desc) => {
                    Debug.LogError("error -- " + code + " desc " + desc);
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
}
