using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class ChatManagerTest : MonoBehaviour, IChatManagerDelegate
{

    private Button backButton;

    private Button sendTextBtn;
    private Button sendImageBtn;
    private Button sendFileBtn;
    private Button sendVideoBtn;
    private Button sendVoiceBtn;
    private Button sendCmdBtn;
    private Button sendCustomBtn;
    private Button sendLocBtn;
    private Button resendBtn;
    private Button recallBtn;
    private Button getConversationBtn;
    private Button loadAllConverstaionsBtn;
    private Button downLoadAttachmentBtn;
    private Button fetchHistoryMessagesBtn;
    private Button getConversationsFromServerBtn;
    private Button getUnreadMessageCountBtn;
    private Button importMessagesBtn;
    private Button loadMessageBtn;
    private Button markAllConversationAsReadBtn;
    private Button searchMessageFromDBBtn;
    private Button sendConversationAckBtn;
    private Button sendMessageReadAckBtn;
    private Button updateMessageBtn;

    private void Awake()
    {
        Debug.Log("chat manager test script has load");

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);


        sendTextBtn = transform.Find("Scroll View/Viewport/Content/SendTextBtn").GetComponent<Button>();
        sendImageBtn = transform.Find("Scroll View/Viewport/Content/SendImageBtn").GetComponent<Button>();
        sendFileBtn = transform.Find("Scroll View/Viewport/Content/SendFileBtn").GetComponent<Button>();
        sendVideoBtn = transform.Find("Scroll View/Viewport/Content/SendVideoBtn").GetComponent<Button>();
        sendVoiceBtn = transform.Find("Scroll View/Viewport/Content/SendVoiceBtn").GetComponent<Button>();
        sendCmdBtn = transform.Find("Scroll View/Viewport/Content/SendCmdBtn").GetComponent<Button>();
        sendCustomBtn = transform.Find("Scroll View/Viewport/Content/SendCustomBtn").GetComponent<Button>();
        sendLocBtn = transform.Find("Scroll View/Viewport/Content/SendLocBtn").GetComponent<Button>();
        resendBtn = transform.Find("Scroll View/Viewport/Content/ResendBtn").GetComponent<Button>();
        recallBtn = transform.Find("Scroll View/Viewport/Content/RecallBtn").GetComponent<Button>();
        getConversationBtn = transform.Find("Scroll View/Viewport/Content/GetConversationBtn").GetComponent<Button>();
        loadAllConverstaionsBtn = transform.Find("Scroll View/Viewport/Content/LoadAllConverstaionsBtn").GetComponent<Button>();
        downLoadAttachmentBtn = transform.Find("Scroll View/Viewport/Content/DownLoadAttachmentBtn").GetComponent<Button>();
        fetchHistoryMessagesBtn = transform.Find("Scroll View/Viewport/Content/FetchHistoryMessagesBtn").GetComponent<Button>();
        getConversationsFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetConversationsFromServerBtn").GetComponent<Button>();
        getUnreadMessageCountBtn = transform.Find("Scroll View/Viewport/Content/GetUnreadMessageCountBtn").GetComponent<Button>();
        importMessagesBtn = transform.Find("Scroll View/Viewport/Content/ImportMessagesBtn").GetComponent<Button>();
        loadMessageBtn = transform.Find("Scroll View/Viewport/Content/LoadMessageBtn").GetComponent<Button>();
        markAllConversationAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkAllConversationAsReadBtn").GetComponent<Button>();
        searchMessageFromDBBtn = transform.Find("Scroll View/Viewport/Content/SearchMessageFromDBBtn").GetComponent<Button>();
        sendConversationAckBtn = transform.Find("Scroll View/Viewport/Content/SendConversationAckBtn").GetComponent<Button>();
        sendMessageReadAckBtn = transform.Find("Scroll View/Viewport/Content/SendMessageReadAckBtn").GetComponent<Button>();
        updateMessageBtn = transform.Find("Scroll View/Viewport/Content/UpdateMessageBtn").GetComponent<Button>();


        sendTextBtn.onClick.AddListener(SendTextBtnAction);
        sendImageBtn.onClick.AddListener(SendImageBtnAction);
        sendFileBtn.onClick.AddListener(SendFileBtnAction);
        sendVideoBtn.onClick.AddListener(SendVideoBtnAction);
        sendVoiceBtn.onClick.AddListener(SendVoiceBtnAction);
        sendCmdBtn.onClick.AddListener(SendCmdBtnAction);
        sendCustomBtn.onClick.AddListener(SendCustomBtnAction);
        sendLocBtn.onClick.AddListener(SendLocBtnAction);
        resendBtn.onClick.AddListener(ResendBtnAction);
        recallBtn.onClick.AddListener(RecallBtnAction);
        getConversationBtn.onClick.AddListener(GetConversationBtnAction);
        loadAllConverstaionsBtn.onClick.AddListener(LoadAllConverstaionsBtnAction);
        downLoadAttachmentBtn.onClick.AddListener(DownLoadAttachmentBtnAction);
        fetchHistoryMessagesBtn.onClick.AddListener(FetchHistoryMessagesBtnAction);
        getConversationsFromServerBtn.onClick.AddListener(GetConversationsFromServerBtnAction);
        getUnreadMessageCountBtn.onClick.AddListener(GetUnreadMessageCountBtnAction);
        importMessagesBtn.onClick.AddListener(ImportMessagesBtnAction);
        loadMessageBtn.onClick.AddListener(LoadMessageBtnAction);
        markAllConversationAsReadBtn.onClick.AddListener(MarkAllConversationAsReadBtnAction);
        searchMessageFromDBBtn.onClick.AddListener(SearchMessageFromDBBtnAction);
        sendConversationAckBtn.onClick.AddListener(SendConversationAckBtnAction);
        sendMessageReadAckBtn.onClick.AddListener(SendMessageReadAckBtnAction);
        updateMessageBtn.onClick.AddListener(UpdateMessageBtnAction);

        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
    }


    void backButtonAction()
    {
        Debug.Log("back btn clicked");
        SceneManager.LoadSceneAsync("Main");
    }

    void SendTextBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict)=> {
            Message msg = Message.CreateTextSendMessage(dict["to"], dict["content"]);
            SDKClient.Instance.ChatManager.SendMessage(msg, new CallBack(
                onSuccess: () => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { UIManager.SuccessAlert(transform); });
                    //UIManager.SuccessAlert(transform);
                },
                onError:(code, desc) => {
                    ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { UIManager.ErrorAlert(transform, code, desc); });
                    //UIManager.ErrorAlert(transform, code, desc);
                }               
            ));
        });

        config.AddField("to");
        config.AddField("content");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SendImageBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendImageBtnAction");
    }
    void SendFileBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendFileBtnAction");
    }
    void SendVideoBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendVideoBtnAction");
    }
    void SendVoiceBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendVoiceBtnAction");
    }

    void SendCmdBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateCmdSendMessage(dict["to"], dict["action"]);
            SDKClient.Instance.ChatManager.SendMessage(msg, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("action");
        UIManager.DefaultInputAlert(transform, config);

    }
    void SendCustomBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateCustomSendMessage(dict["to"], dict["custom"]);
            SDKClient.Instance.ChatManager.SendMessage(msg, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("custom");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SendLocBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateLocationSendMessage(dict["to"], 139.33, 130.55, dict["address"]);
            SDKClient.Instance.ChatManager.SendMessage(msg, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("address");
        UIManager.DefaultInputAlert(transform, config);
    }
    void ResendBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            SDKClient.Instance.ChatManager.ResendMessage(dict["msgId"], new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("msgId");
        UIManager.DefaultInputAlert(transform, config);
    }
    void RecallBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            SDKClient.Instance.ChatManager.RecallMessage(dict["msgId"], new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("msgId");
        UIManager.DefaultInputAlert(transform, config);
    }
    void GetConversationBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            ConversationType type = ConversationType.Chat;
            int iType = int.Parse(dict["type(0/1/2)"]);
            switch (iType) {
                case 0:
                    type = ConversationType.Chat;
                    break;
                case 1:
                    type = ConversationType.Group;
                    break;
                case 2:
                    type = ConversationType.Room;
                    break;
            }
            Conversation conversation = SDKClient.Instance.ChatManager.GetConversation(dict["conversationId"], type);
            if (conversation != null)
            {
                UIManager.SuccessAlert(transform);
            }
            else {
                UIManager.DefaultAlert(transform, "未找到会话");
            }
            
        });

        config.AddField("conversationId");
        config.AddField("type(0/1/2)");
        UIManager.DefaultInputAlert(transform, config);
    }
    void LoadAllConverstaionsBtnAction()
    {
        List<Conversation>list = SDKClient.Instance.ChatManager.LoadAllConversations();

        List<string> strList = new List<string>();
        foreach (var conv in list) {
            strList.Add(conv.Id);
        }
        string str = string.Join(",", strList.ToArray());
        UIManager.DefaultAlert(transform, str);

        Debug.Log("LoadAllConverstaionsBtnAction");
    }
    void DownLoadAttachmentBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("DownLoadAttachmentBtnAction");
    }
    void FetchHistoryMessagesBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) => {
            string conversationId = dict["ConversationId"];
            ConversationType type = ConversationType.Chat;
            int iType = int.Parse(dict["ConversationType(0/1/2)"]);
            switch (iType)
            {
                case 0:
                    type = ConversationType.Chat;
                    break;
                case 1:
                    type = ConversationType.Group;
                    break;
                case 2:
                    type = ConversationType.Room;
                    break;
            }
            string startId = dict["StartMsgId"];
            int loadCount = int.Parse(dict["LoadCount"]);


            SDKClient.Instance.ChatManager.FetchHistoryMessagesFromServer(conversationId, type, startId, loadCount, new ValueCallBack<CursorResult<Message>>(
                onSuccess: (result) => {
                    List<string> strList = new List<string>();
                    foreach (var msg in result.Data)
                    {
                        strList.Add(msg.MsgId);
                    }
                    string str = string.Join(",", strList.ToArray());
                    UIManager.DefaultAlert(transform, str);
                },
                onError:(code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("ConversationType(0/1/2)");
        config.AddField("StartMsgId");
        config.AddField("LoadCount");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("FetchHistoryMessagesBtnAction");
    }
    void GetConversationsFromServerBtnAction()
    {
        SDKClient.Instance.ChatManager.GetConversationsFromServer(new ValueCallBack<List<Conversation>>(
             onSuccess: (list) =>
             {
                 List<string> strList = new List<string>();
                 foreach (var conv in list)
                 {
                     strList.Add(conv.Id);
                 }
                 string str = string.Join(",", strList.ToArray());
                 UIManager.DefaultAlert(transform, str);
             },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
        ));

        Debug.Log("GetConversationsFromServerBtnAction");
    }

    void GetUnreadMessageCountBtnAction()
    {
        int count = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
        UIManager.DefaultAlert(transform, $"未读总数: {count}");

        Debug.Log("GetUnreadMessageCountBtnAction");
    }

    void ImportMessagesBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("ImportMessagesBtnAction");
    }
    void LoadMessageBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("根据id获取消息", (dict) =>
        {
            Message msg = SDKClient.Instance.ChatManager.LoadMessage(dict["id"]);
            if (msg != null)
            {
                UIManager.SuccessAlert(transform);
            }
            else {
                UIManager.DefaultAlert(transform, "未找到消息");
            }
            
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("LoadMessageBtnAction");
    }
    void MarkAllConversationAsReadBtnAction()
    {
        bool ret = SDKClient.Instance.ChatManager.MarkAllConversationsAsRead();
        if (ret) {
            int unreadCount = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
            UIManager.DefaultAlert(transform, $"设置成功，当前未读数为: {unreadCount}");
        }
        else
        {
            UIManager.DefaultAlert(transform, "设置失败");
        }

        Debug.Log("MarkAllConversationAsReadBtnAction");
    }
    void SearchMessageFromDBBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("根据关键字获取消息", (dict) =>
        {
            int count = int.Parse(dict["LoadCount"]);
            List<Message> list = SDKClient.Instance.ChatManager.SearchMsgFromDB(dict["Keyword"], maxCount: count);
            if (list != null)
            {
                UIManager.SuccessAlert(transform);
            }
            else
            {
                UIManager.DefaultAlert(transform, "未找到消息");
            }

        });

        config.AddField("Keyword");
        config.AddField("LoadCount");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("SearchMessageFromDBBtnAction");
    }
    void SendConversationAckBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("发送会话Ack", (dict) =>
        {
            //int count = int.Parse(dict["LoadCount"]);
            SDKClient.Instance.ChatManager.SendConversationReadAck(dict["id"], new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError:(code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("SendConversationAckBtnAction");
    }
    void SendMessageReadAckBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("发送消息Ack", (dict) =>
        {
            //int count = int.Parse(dict["LoadCount"]);
            SDKClient.Instance.ChatManager.SendMessageReadAck(dict["id"], new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("SendMessageReadAckBtnAction");
    }
    void UpdateMessageBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("UpdateMessageBtnAction");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMessagesReceived(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnMessagesReceived: {messages.Count}");
    }

    public void OnCmdMessagesReceived(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnCmdMessagesReceived: {messages.Count}");
    }

    public void OnMessagesRead(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnMessagesRead: {messages.Count}");
    }

    public void OnMessagesDelivered(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnMessagesDelivered: {messages.Count}");
    }

    public void OnMessagesRecalled(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnMessagesRecalled: {messages.Count}");
    }

    public void OnReadAckForGroupMessageUpdated()
    {
        UIManager.DefaultAlert(transform, "OnReadAckForGroupMessageUpdated");
    }

    public void OnGroupMessageRead(List<GroupReadAck> list)
    {
        UIManager.DefaultAlert(transform, $"OnGroupMessageRead: {list.Count}");
    }

    public void OnConversationsUpdate()
    {
        UIManager.DefaultAlert(transform, "OnConversationsUpdate");
    }

    public void OnConversationRead(string from, string to)
    {
        UIManager.DefaultAlert(transform, $"OnConversationRead, from: {from}, to: {to}");
    }
}
