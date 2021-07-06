using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ChatSDK;
using ChatSDK.MessageBody;
using UnityEngine.SceneManagement;

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
        SendBtn.onClick.AddListener(SendImageMessageAction);
        
        JoinGroupBtn.onClick.AddListener(JoinGroupAction);
        GroupInfoBtn.onClick.AddListener(GetGroupInfoAction);
        LeaveGroupBtn.onClick.AddListener(LeaveGroupAction);

        JoinRoomBtn.onClick.AddListener(JoinRoomAction);
        RoomInfoBtn.onClick.AddListener(GetRoomInfoAction);
        LeaveRoomBtn.onClick.AddListener(LeaveRoomAction);

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

    void SendMessageAction()
    {
        string receiverId = RecvIdField.text;
        string content = TextField.text;
        IChatManager chatManager = SDKClient.Instance.ChatManager;
        Message message = Message.CreateTextSendMessage(receiverId,content);
        CallBack callback = new CallBack(onSuccess: () => { Debug.Log("Message sent successfully!"); },
                                            onProgress: (int progress) => { Debug.Log(progress); },
                                            onError: (int code, string desc) => { Debug.Log(code + desc); });
        chatManager.SendMessage(message, callback);
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

    void SendImageMessageAction()
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
                    }
                    
                }
            }, onError: (int code, string desc) =>
            {
                Debug.LogError($"Fetch history messages with error, code={code}, desc={desc}.");
            }));
    }

    void JoinGroupAction()
    {

    }

    void GetGroupInfoAction()
    {

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
}
