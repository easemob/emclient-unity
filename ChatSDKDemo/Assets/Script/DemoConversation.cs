using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChatSDK;

public class DemoConversation : MonoBehaviour
{


    public Text ConversationIdText;
    public Text MessageIdText;
    public Button GetConversationBtn;
    public Button AllConversationsBtn;
    public Button UnReadCountBtn;
    public Button AllUnreadCountBtn;
    public Button InsertMsgBtn;
    public Button DeleteBtn;
    public Button DeleteAllMsgBtn;
    public Button DeleteConversationBtn;
    public Button LoadMsgBtn;
    public Button LoadMsgs1Btn;
    public Button LoadMsgs2Btn;
    public Button LoadMsgs3Btn;
    public Button SetExtBtn;
    public Button GetExtBtn;
    public Button FetchConversationListFromServerBtn;
    public Button ReadAllMsgBtn;


    // Start is called before the first frame update
    void Start()
    {
        GetConversationBtn.onClick.AddListener(GetConversationBtnAction);
        AllConversationsBtn.onClick.AddListener(AllConversationsBtnAction);
        UnReadCountBtn.onClick.AddListener(UnReadCountBtnAction);
        AllUnreadCountBtn.onClick.AddListener(AllUnreadCountBtnAction);
        InsertMsgBtn.onClick.AddListener(InsertMsgBtnAction);
        DeleteBtn.onClick.AddListener(DeleteBtnAction);
        DeleteAllMsgBtn.onClick.AddListener(DeleteAllMsgBtnAction);
        DeleteConversationBtn.onClick.AddListener(DeleteConversationBtnAction);
        LoadMsgBtn.onClick.AddListener(LoadMsgBtnAction);
        LoadMsgs1Btn.onClick.AddListener(LoadMsgs1BtnAction);
        LoadMsgs2Btn.onClick.AddListener(LoadMsgs2BtnAction);
        LoadMsgs3Btn.onClick.AddListener(LoadMsgs3BtnAction);
        SetExtBtn.onClick.AddListener(SetExtBtnAction);
        GetExtBtn.onClick.AddListener(GetExtBtnAction);
        FetchConversationListFromServerBtn.onClick.AddListener(FetchConversationListFromServerBtnAction);
        ReadAllMsgBtn.onClick.AddListener(ReadAllMsgBtnAction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetConversationBtnAction() {
        string str = ConversationIdText.text;
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);
        Debug.Log("GetConversation id --- " + conv.Id);
    }

    void AllConversationsBtnAction() {
        List<Conversation>list = SDKClient.Instance.ChatManager.LoadAllConversations();
        foreach (var conversation in list) {
            Debug.Log("conversation id --- " + conversation.Id);
        }
    }

    void UnReadCountBtnAction() {
        string str = ConversationIdText.text;
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);
        Debug.Log("GetConversation id --- " + conv.Id + " unread count " + conv.UnReadCount);
    }

    void AllUnreadCountBtnAction() {
        int count = SDKClient.Instance.ChatManager.GetUnreadMessageCount();
        Debug.Log("all unread count ---- " + count);
    }

    void InsertMsgBtnAction() {
        string str = ConversationIdText.text;
        Message msg = Message.CreateTextSendMessage(str,  "我是插入的消息");
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);
        bool ret = conv.InsertMessage(msg);
        Debug.Log("insert " + (ret ? "success" : "failed"));
    }

    void DeleteBtnAction() {
        string str = ConversationIdText.text;
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);
        bool ret = conv.DeleteMessage(conv.LastMessage.MsgId);
        Debug.Log("delete " + (ret ? "success" : "failed"));
    }

    void DeleteAllMsgBtnAction() {
        string str = ConversationIdText.text;
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);
        bool ret = conv.DeleteAllMessages();
        Debug.Log("delete all" + (ret ? "success" : "failed"));
    }

    void DeleteConversationBtnAction() {
        string str = ConversationIdText.text;
        bool ret = SDKClient.Instance.ChatManager.DeleteConversation(str);
        Debug.Log("delete conversation" + (ret ? "success" : "failed"));
    }

    void LoadMsgBtnAction() {

        string str = ConversationIdText.text;

        string msgId = MessageIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        Message msg = conv.LoadMessage(msgId);

        ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;

        Debug.Log("msg context --- " + textBody.Text);

        Message latestMsg = conv.LastMessage;

        Debug.Log("latestMsg context --- " + textBody.Text);

        Message latestReceiveMsg = conv.LastReceivedMessage;

        Debug.Log("latestReceiveMsg context --- " + textBody.Text);
    }

    void LoadMsgs1BtnAction()
    {
        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        List<Message>list = conv.LoadMessages(null);

        foreach (var msg in list) {

            ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;

            Debug.Log("msg context --- " + textBody.Text);
        }
    }

    void LoadMsgs2BtnAction()
    {
        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        string keyword = MessageIdText.text;

        List<Message> list = conv.LoadMessagesWithKeyword(keyword);

        foreach (var msg in list)
        {

            ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;

            Debug.Log("msg context --- " + textBody.Text);
        }

    }

    void LoadMsgs3BtnAction() {

        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        List<Message> list = conv.LoadMessagesWithMsgType(MessageBodyType.TXT);

        foreach (var msg in list)
        {

            ChatSDK.MessageBody.TextBody textBody = (ChatSDK.MessageBody.TextBody)msg.Body;

            Debug.Log("msg context --- " + textBody.Text);
        }
    }

    void SetExtBtnAction() {

        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        Dictionary<string, string> ext = new Dictionary<string, string>();

        ext.Add("extkey", "extvalue");

        conv.Ext = ext;

    }

    void GetExtBtnAction() {

        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        Dictionary<string, string> ext = conv.Ext;

        foreach (var key in ext.Keys) {
            Debug.Log("key -- " + key + " value: " + ext[key]);
        }
    }

    void FetchConversationListFromServerBtnAction() {

        ValueCallBack<List<Conversation>> callback = new ValueCallBack<List<Conversation>>();

        callback.OnSuccessValue = (List<Conversation> list) => {
            foreach (var conversation in list) {
                Debug.Log("conversation --- " + conversation.Id);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.LogError("error -- " + code + " " + desc);
        };

        SDKClient.Instance.ChatManager.GetConversationsFromServer(callback);

    }

    void ReadAllMsgBtnAction() {

        string str = ConversationIdText.text;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(str);

        Debug.Log("before read " + conv.UnReadCount);

        conv.MarkAllMessageAsRead();

        Debug.Log("after read " + conv.UnReadCount);

    }
}
