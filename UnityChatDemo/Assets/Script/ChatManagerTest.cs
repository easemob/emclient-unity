using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AgoraChat;
using UnityEngine.UI;
using AgoraChat.MessageBody;

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
    private Button sendCombineBtn;
    private Button resendBtn;
    private Button recallBtn;
    private Button getConversationBtn;
    private Button loadAllConverstaionsBtn;
    private Button downLoadAttachmentBtn;
    //private Button downLoadThumbAttachmentBtn;
    private Button fetchHistoryMessagesBtn;
    private Button getConversationsFromServerBtn;
    private Button getUnreadMessageCountBtn;
    private Button importMessagesBtn;
    private Button loadMessageBtn;
    private Button markAllConversationAsReadBtn;
    private Button searchMessageFromDBBtn;
    private Button searchMessageFromDBWithScopeBtn;
    private Button sendConversationAckBtn;
    private Button sendMessageReadAckBtn;
    private Button updateMessageBtn;
    private Button removeMessagesBeforeTimestampBtn;
    private Button deleteConversationFromServerBtn;
    private Button deleteConversationBtn;
    private Button getConversationsFromServerWithCursorBtn;
    private Button getConversationsFromServerWithCursorWithMarkBtn;
    private Button getConversationsFromServerWithPageBtn;
    private Button removeMessagesFromServerWithMsgIdsBtn;
    private Button removeMessagesFromServerWithTsBtn;
    private Button fetchHistoryMessagesByBtn;
    private Button pinConversationBtn;
    private Button modifyMessageBtn;
    private Button downloadCombineMessagesBtn;
    private Button markConversationsBtn;
    private Button deleteAllMessagesAndConversationsBtn;
    private Button pinMessageBtn;
    private Button getPinnedMessagesFromServerBtn;

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
        sendCombineBtn = transform.Find("Scroll View/Viewport/Content/SendCombineBtn").GetComponent<Button>();
        resendBtn = transform.Find("Scroll View/Viewport/Content/ResendBtn").GetComponent<Button>();
        recallBtn = transform.Find("Scroll View/Viewport/Content/RecallBtn").GetComponent<Button>();
        getConversationBtn = transform.Find("Scroll View/Viewport/Content/GetConversationBtn").GetComponent<Button>();
        loadAllConverstaionsBtn = transform.Find("Scroll View/Viewport/Content/LoadAllConverstaionsBtn").GetComponent<Button>();
        downLoadAttachmentBtn = transform.Find("Scroll View/Viewport/Content/DownLoadAttachmentBtn").GetComponent<Button>();
        //downLoadThumbAttachmentBtn = transform.Find("Scroll View/Viewport/Content/DownLoadThumbAttachmentBtn").GetComponent<Button>();
        fetchHistoryMessagesBtn = transform.Find("Scroll View/Viewport/Content/FetchHistoryMessagesBtn").GetComponent<Button>();
        getConversationsFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetConversationsFromServerBtn").GetComponent<Button>();
        getUnreadMessageCountBtn = transform.Find("Scroll View/Viewport/Content/GetUnreadMessageCountBtn").GetComponent<Button>();
        importMessagesBtn = transform.Find("Scroll View/Viewport/Content/ImportMessagesBtn").GetComponent<Button>();
        loadMessageBtn = transform.Find("Scroll View/Viewport/Content/LoadMessageBtn").GetComponent<Button>();
        markAllConversationAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkAllConversationAsReadBtn").GetComponent<Button>();
        searchMessageFromDBBtn = transform.Find("Scroll View/Viewport/Content/SearchMessageFromDBBtn").GetComponent<Button>();
        searchMessageFromDBWithScopeBtn = transform.Find("Scroll View/Viewport/Content/SearchMessageFromDBWithScopeBtn").GetComponent<Button>();
        sendConversationAckBtn = transform.Find("Scroll View/Viewport/Content/SendConversationAckBtn").GetComponent<Button>();
        sendMessageReadAckBtn = transform.Find("Scroll View/Viewport/Content/SendMessageReadAckBtn").GetComponent<Button>();
        updateMessageBtn = transform.Find("Scroll View/Viewport/Content/UpdateMessageBtn").GetComponent<Button>();
        removeMessagesBeforeTimestampBtn = transform.Find("Scroll View/Viewport/Content/RemoveMessagesBeforeTimestampBtn").GetComponent<Button>();
        deleteConversationFromServerBtn = transform.Find("Scroll View/Viewport/Content/DeleteConversationFromServerBtn").GetComponent<Button>();
        deleteConversationBtn = transform.Find("Scroll View/Viewport/Content/DeleteLocalConversationAndMessage").GetComponent<Button>();
        getConversationsFromServerWithCursorBtn = transform.Find("Scroll View/Viewport/Content/GetConversationsFromServerWithCursorBtn").GetComponent<Button>();
        getConversationsFromServerWithCursorWithMarkBtn = transform.Find("Scroll View/Viewport/Content/GetConversationsFromServerWithCursorWithMarkBtn").GetComponent<Button>();
        getConversationsFromServerWithPageBtn = transform.Find("Scroll View/Viewport/Content/GetConversationsFromServerWithPageBtn").GetComponent<Button>();
        removeMessagesFromServerWithMsgIdsBtn = transform.Find("Scroll View/Viewport/Content/RemoveMessagesFromServerWithMsgIdsBtn").GetComponent<Button>();
        removeMessagesFromServerWithTsBtn = transform.Find("Scroll View/Viewport/Content/RemoveMessagesFromServerWithTsBtn").GetComponent<Button>();
        fetchHistoryMessagesByBtn = transform.Find("Scroll View/Viewport/Content/FetchHistoryMessagesByBtn").GetComponent<Button>();
        pinConversationBtn = transform.Find("Scroll View/Viewport/Content/PinConversationBtn").GetComponent<Button>();
        modifyMessageBtn = transform.Find("Scroll View/Viewport/Content/ModifyMessageBtn").GetComponent<Button>();
        downloadCombineMessagesBtn = transform.Find("Scroll View/Viewport/Content/DownloadCombineMessagesBtn").GetComponent<Button>();
        markConversationsBtn = transform.Find("Scroll View/Viewport/Content/MarkConversationsBtn").GetComponent<Button>();
        deleteAllMessagesAndConversationsBtn = transform.Find("Scroll View/Viewport/Content/DeleteAllMessagesAndConversationsBtn").GetComponent<Button>();
        pinMessageBtn = transform.Find("Scroll View/Viewport/Content/PinMessageBtn").GetComponent<Button>();
        getPinnedMessagesFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetPinnedMessagesFromServerBtn").GetComponent<Button>();

        sendTextBtn.onClick.AddListener(SendTextBtnAction);
        sendImageBtn.onClick.AddListener(SendImageBtnAction);
        sendFileBtn.onClick.AddListener(SendFileBtnAction);
        sendVideoBtn.onClick.AddListener(SendVideoBtnAction);
        sendVoiceBtn.onClick.AddListener(SendVoiceBtnAction);
        sendCmdBtn.onClick.AddListener(SendCmdBtnAction);
        sendCustomBtn.onClick.AddListener(SendCustomBtnAction);
        sendLocBtn.onClick.AddListener(SendLocBtnAction);
        sendCombineBtn.onClick.AddListener(SendCombineBtnAction);
        resendBtn.onClick.AddListener(ResendBtnAction);
        recallBtn.onClick.AddListener(RecallBtnAction);
        getConversationBtn.onClick.AddListener(GetConversationBtnAction);
        loadAllConverstaionsBtn.onClick.AddListener(LoadAllConverstaionsBtnAction);
        downLoadAttachmentBtn.onClick.AddListener(DownLoadAttachmentBtnAction);
        //downLoadThumbAttachmentBtn.onClick.AddListener(DownLoadThumbAttachmentBtnAction);
        fetchHistoryMessagesBtn.onClick.AddListener(FetchHistoryMessagesBtnAction);
        getConversationsFromServerBtn.onClick.AddListener(GetConversationsFromServerBtnAction);
        getUnreadMessageCountBtn.onClick.AddListener(GetUnreadMessageCountBtnAction);
        importMessagesBtn.onClick.AddListener(ImportMessagesBtnAction);
        loadMessageBtn.onClick.AddListener(LoadMessageBtnAction);
        markAllConversationAsReadBtn.onClick.AddListener(MarkAllConversationAsReadBtnAction);
        searchMessageFromDBBtn.onClick.AddListener(SearchMessageFromDBBtnAction);
        searchMessageFromDBWithScopeBtn.onClick.AddListener(SearchMessageFromDBWithScopeBtnAction);
        sendConversationAckBtn.onClick.AddListener(SendConversationAckBtnAction);
        sendMessageReadAckBtn.onClick.AddListener(SendMessageReadAckBtnAction);
        updateMessageBtn.onClick.AddListener(UpdateMessageBtnAction);
        removeMessagesBeforeTimestampBtn.onClick.AddListener(RemoveMessagesBeforeTimestampBtnAction);
        deleteConversationFromServerBtn.onClick.AddListener(DeleteConversationFromServerBtnAction);
        deleteConversationBtn.onClick.AddListener(DeleteLocalConversationAndMessage);
        getConversationsFromServerWithCursorBtn.onClick.AddListener(GetConversationsFromServerWithCursorAction);
        getConversationsFromServerWithCursorWithMarkBtn.onClick.AddListener(GetConversationsFromServerWithCursorWithMarkAction);
        getConversationsFromServerWithPageBtn.onClick.AddListener(GetConversationsFromServerWithPageAction);
        removeMessagesFromServerWithMsgIdsBtn.onClick.AddListener(RemoveMessagesFromServerWithMsgIdsAction);
        removeMessagesFromServerWithTsBtn.onClick.AddListener(RemoveMessagesFromServerWithTsAction);
        fetchHistoryMessagesByBtn.onClick.AddListener(FetchHistoryMessagesByBtnAction);
        pinConversationBtn.onClick.AddListener(PinConversationBtnAction);
        modifyMessageBtn.onClick.AddListener(ModifyMessageAction);
        downloadCombineMessagesBtn.onClick.AddListener(DownloadCombineMessagesAction);
        markConversationsBtn.onClick.AddListener(MarkConversationsBtnAction);
        deleteAllMessagesAndConversationsBtn.onClick.AddListener(DeleteAllMessagesAndConversationsBtnAction);
        pinMessageBtn.onClick.AddListener(PinMessageBtnAction);
        getPinnedMessagesFromServerBtn.onClick.AddListener(GetPinnedMessagesFromServerBtnAction);
        SDKClient.Instance.ChatManager.AddChatManagerDelegate(this);
    }

    private void OnDestroy()
    {
        SDKClient.Instance.ChatManager.RemoveChatManagerDelegate(this);
    }

    void backButtonAction()
    {
        Debug.Log("back btn clicked");
        SceneManager.LoadSceneAsync("Main");
    }

    void SendTextBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            int type = int.Parse(dict["type"]);

            Message msg = Message.CreateTextSendMessage(dict["to"], dict["content"]);
            msg.MessageType = (MessageType)type;
            Dictionary<string, AttributeValue> attr = new Dictionary<string, AttributeValue>();
            attr["strKey"] = AttributeValue.Of("strValue");
            attr["intKey"] = AttributeValue.Of(10, AttributeValueType.INT32);
            attr["longKey"] = AttributeValue.Of(1999L, AttributeValueType.INT64);
            attr["boolKey"] = AttributeValue.Of(true, AttributeValueType.BOOL);
            attr["floatKey"] = AttributeValue.Of(12.3f, AttributeValueType.FLOAT);
            attr["doubleKey"] = AttributeValue.Of(22.22, AttributeValueType.DOUBLE);
            msg.Attributes = attr;

            msg.ReceiverList = new List<string>();
            if(dict["receiverList"].Length > 0) msg.ReceiverList.Add(dict["receiverList"]);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);

                    //Message msg1 = SDKClient.Instance.ChatManager.LoadMessage(msg.MsgId);
                    //Debug.Log($"body content:{msg1.MsgId}");
                },
                onProgress: (progress) =>
                {
                    UIManager.TitleAlert(transform, "发送进度", progress.ToString());
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, msg.MsgId);
                }
            ));
        });

        config.AddField("to");
        config.AddField("content");
        config.AddField("type");
        config.AddField("receiverList");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SendImageBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            Message msg = Message.CreateImageSendMessage(dict["to"], dict["filepath"]);
            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);

                    //Message msg1 = SDKClient.Instance.ChatManager.LoadMessage(msg.MsgId);
                    //Debug.Log($"body content:{msg1.MsgId}");
                },
                onProgress: (progress) =>
                {
                    UIManager.TitleAlert(transform, "发送进度", progress.ToString());
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, msg.MsgId);
                }
            ));
        });

        config.AddField("to");
        config.AddField("filepath");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SendFileBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            long filesize = long.Parse(dict["fileSize"]);
            Message msg = Message.CreateFileSendMessage(dict["to"], dict["localPath"], "", filesize);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "发送进度", progress.ToString());
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("localPath");
        config.AddField("fileSize");
        UIManager.DefaultInputAlert(transform, config);

    }
    void SendVideoBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateVideoSendMessage(dict["to"], "/Users/yuqiang/Test/resource/video.mp4", "", "", 425507);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    //VideoBody vb = (VideoBody)msg.Body;
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "发送进度", progress.ToString());
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, msg.MsgId);
                }
            ));
        });

        config.AddField("to");
        UIManager.DefaultInputAlert(transform, config);

    }
    void SendVoiceBtnAction()
    {
        SDKClient.Instance.ChatManager.FetchGroupReadAcks("1033288267207805704", "187147300700161", 20, "", new ValueCallBack<CursorResult<GroupReadAck>>(
                onSuccess: (result) =>
                {
                    if (0 == result.Data.Count)
                    {
                        UIManager.DefaultAlert(transform, "No group ack messages.");
                        return;
                    }
                    List<string> strList = new List<string>();
                    foreach (var msg in result.Data)
                    {
                        strList.Add(msg.AckId);
                    }
                    string str = string.Join(",", strList.ToArray());
                    UIManager.DefaultAlert(transform, str);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        return;

        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendVoiceBtnAction");
    }

    void SendCmdBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            Message msg = Message.CreateCmdSendMessage(dict["to"], dict["action"]);
            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
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
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            Dictionary<string, string> ext = new Dictionary<string, string>();
            ext.Add("key", "value");
            Message msg = Message.CreateCustomSendMessage(dict["to"], dict["custom"], ext);
            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
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
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            Message msg = Message.CreateLocationSendMessage(dict["to"], 139.33, 130.55, dict["address"], dict["buildingName"]);
            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("address");
        config.AddField("buildingName");
        UIManager.DefaultInputAlert(transform, config);
    }

    void SendCombineBtnAction ()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            List<string> ml = new List<string>();
            if (dict["sub-msg1"].Length > 0)
            {
                ml.Add(dict["sub-msg1"]);    
            }
            
            if (dict["sub-msg2"].Length > 0)
            {
                ml.Add(dict["sub-msg2"]);    
            }
            Message msg = Message.CreateCombineSendMessage(dict["to"], dict["title"], dict["summary"], dict["compatible"], ml);
            msg.MessageType = (MessageType)(int.Parse(dict["chattype"]));

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("to");
        config.AddField("chattype");
        config.AddField("title");
        config.AddField("summary");
        config.AddField("compatible");
        config.AddField("sub-msg1");
        config.AddField("sub-msg2");
        UIManager.DefaultInputAlert(transform, config);
    }

    void ResendBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgid = dict["msgId"];
            if (null == msgid || 0 == msgid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
        });

        config.AddField("msgId");
        UIManager.DefaultInputAlert(transform, config);
    }
    void RecallBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgid = dict["msgId"];
            if (null == msgid || 0 == msgid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            string ext = dict["ext"];

            SDKClient.Instance.ChatManager.RecallMessage(dict["msgId"], ext, new CallBack(
                onSuccess: () =>
                {
                    UIManager.TitleAlert(transform, "成功", "回撤成功");
                },
                onProgress: (progress) =>
                {
                    UIManager.TitleAlert(transform, "回撤进度", progress.ToString());
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("msgId");
        config.AddField("ext");
        UIManager.DefaultInputAlert(transform, config);
    }
    void GetConversationBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string cid = dict["conversationId"];
            string chatType = dict["type(0/1/2)"];
            if (null == cid || 0 == cid.Length || null == chatType || 0 == chatType.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            ConversationType type = ConversationType.Chat;
            int iType = int.Parse(dict["type(0/1/2)"]);
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
            Conversation conversation = SDKClient.Instance.ChatManager.GetConversation(dict["conversationId"], type);
            if (conversation != null)
            {
                Debug.Log($"GetConversation: {conversation.ToJsonObject()}");
                UIManager.DefaultAlert(transform, $"id: {conversation.Id}, type: {conversation.Type}, isThread: {conversation.IsThread}, IsPinned:{conversation.IsPinned}, PinnedTime:{conversation.PinnedTime}");
            }
            else
            {
                UIManager.DefaultAlert(transform, "未找到会话");
            }

        });

        config.AddField("conversationId");
        config.AddField("type(0/1/2)");
        UIManager.DefaultInputAlert(transform, config);
    }

    void LoadAllConverstaionsBtnAction()
    {
        List<Conversation> list = SDKClient.Instance.ChatManager.LoadAllConversations();

        List<string> strList = new List<string>();
        foreach (var conv in list)
        {
            strList.Add(conv.Id);
        }
        string str = string.Join(",", strList.ToArray());
        UIManager.DefaultAlert(transform, str);

        Debug.Log("LoadAllConverstaionsBtnAction");
    }
    void DownLoadAttachmentBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["msgId"];
            SDKClient.Instance.ChatManager.DownloadAttachment(msgId, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                },
                onProgress: (progress) =>
                {
                    Debug.Log($"download progress: {progress}");
                }
            ));
        });
        config.AddField("msgId");
        UIManager.DefaultInputAlert(transform, config);
    }

    void DownLoadThumbAttachmentBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["msgId"];
            SDKClient.Instance.ChatManager.DownloadThumbnail(msgId, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                },
                onProgress: (progress) =>
                {
                    Debug.Log($"thumb progress: {progress}");
                }
            ));
        });
        config.AddField("msgId");
        UIManager.DefaultInputAlert(transform, config);
    }

    void FetchHistoryMessagesBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            string chatType = dict["ConversationType(0/1/2)"];
            string count = dict["LoadCount"];
            if (null == conversationId || 0 == conversationId.Length || null == chatType || 0 == chatType.Length || null == count || 0 == count.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

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

            MessageSearchDirection direction = MessageSearchDirection.UP;

            SDKClient.Instance.ChatManager.FetchHistoryMessagesFromServer(conversationId, type, startId, loadCount, direction, new ValueCallBack<CursorResult<Message>>(
                onSuccess: (result) =>
                {
                    if (0 == result.Data.Count)
                    {
                        UIManager.DefaultAlert(transform, "No history messages.");
                        return;
                    }
                    List<string> strList = new List<string>();
                    foreach (var msg in result.Data)
                    {
                        strList.Add(msg.MsgId);
                    }
                    string str = string.Join(",", strList.ToArray());
                    UIManager.DefaultAlert(transform, str);
                },
                onError: (code, desc) =>
                {
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

    void FetchHistoryMessagesByBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            ConversationType type = (ConversationType)(int.Parse(dict["ConversationType(0/1/2)"]));
            string cursor = "";
            int pageSize = 10;

            FetchServerMessagesOption option = new FetchServerMessagesOption();
            option.IsSave = false;
            option.Direction = (MessageSearchDirection)(int.Parse(dict["Direction(0/1)"]));
            option.From = dict["From"];

            option.MsgTypes = new List<MessageBodyType>();
            MessageBodyType msgType = (MessageBodyType)(int.Parse(dict["MsgType(0-7)"]));
            option.MsgTypes.Add(msgType);

            option.StartTime = long.Parse(dict["StartTime"]);
            option.EndTime = long.Parse(dict["EndTime"]);

            SDKClient.Instance.ChatManager.FetchHistoryMessagesFromServerBy(conversationId, type, cursor, pageSize, option, new ValueCallBack<CursorResult<Message>>(
                onSuccess: (result) =>
                {
                    if (0 == result.Data.Count)
                    {
                        UIManager.DefaultAlert(transform, "No history messages.");
                        return;
                    }
                    List<string> strList = new List<string>();
                    foreach (var msg in result.Data)
                    {
                        strList.Add(msg.MsgId);
                    }
                    string str = string.Join(",", strList.ToArray());
                    UIManager.DefaultAlert(transform, str);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("ConversationType(0/1/2)");
        config.AddField("Direction(0/1)");
        config.AddField("From");
        config.AddField("MsgType(0-7)");
        config.AddField("StartTime");
        config.AddField("EndTime");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("FetchHistoryMessagesByBtnAction");
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
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string convId = dict["convId"];
            string txt = dict["txt"];
            Message msg = Message.CreateTextSendMessage(convId, txt);
            List<Message> msgs = new List<Message>();
            msgs.Add(msg);
            SDKClient.Instance.ChatManager.ImportMessages(msgs, new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "导入消息成功");
                    /*UIManager.DefaultAlert(transform, "获取消息?",
                        () =>
                        {
                            Message loadMsg = SDKClient.Instance.ChatManager.LoadMessage(msg.MsgId);
                            TextBody body = loadMsg.Body as TextBody;
                            UIManager.DefaultAlert(transform, $"convId: { loadMsg.ConversationId}, content: {body.Text}");
                        },
                        () => { });*/
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"插入失败:{code}, {desc}");
                }
            ));
        });
        config.AddField("convId");
        config.AddField("txt");
        UIManager.DefaultInputAlert(transform, config);

    }
    void LoadMessageBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("根据id获取消息", (dict) =>
        {
            Message msg = SDKClient.Instance.ChatManager.LoadMessage(dict["id"]);
            if (msg != null)
            {
                Debug.Log($"loadMessage: {msg.ToJsonObject()}");
                UIManager.SuccessAlert(transform);
            }
            else
            {
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
        if (ret)
        {
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
            string keywordStr = dict["Keyword"];
            string countStr = dict["LoadCount"];
            if (null == keywordStr || 0 == keywordStr.Length || null == countStr || 0 == countStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            int count = int.Parse(dict["LoadCount"]);
            SDKClient.Instance.ChatManager.SearchMsgFromDB(dict["Keyword"], -1, count, null, MessageSearchDirection.UP, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    UIManager.DefaultAlert(transform, $"找到的消息条数: {list.Count}");
                },
                onError: (code, desc) => {
                    UIManager.DefaultAlert(transform, "未找到消息");
                }));

        });

        config.AddField("Keyword");
        config.AddField("LoadCount");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("SearchMessageFromDBBtnAction");
    }

    void SearchMessageFromDBWithScopeBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("根据关键字获取消息", (dict) =>
        {
            string keywordStr = dict["Keyword"];
            string countStr = dict["LoadCount"];
            if (null == keywordStr || 0 == keywordStr.Length || null == countStr || 0 == countStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            int count = int.Parse(dict["LoadCount"]);
            MessageSearchScope scope = (MessageSearchScope)(int.Parse(dict["scope"]));
            SDKClient.Instance.ChatManager.SearchMsgFromDB(dict["Keyword"], -1, count, null, MessageSearchDirection.UP, scope, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    UIManager.DefaultAlert(transform, $"找到的消息条数: {list.Count}");
                },
                onError: (code, desc) => {
                    UIManager.DefaultAlert(transform, "未找到消息");
                }));

        });

        config.AddField("Keyword");
        config.AddField("scope");
        config.AddField("LoadCount");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("SearchMessageFromDBBtnAction");
    }

    void SendConversationAckBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("发送会话Ack", (dict) =>
        {
            string conversationId = dict["id"];
            if (null == conversationId || 0 == conversationId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ChatManager.SendConversationReadAck(dict["id"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
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
            string idStr = dict["id"];
            if (null == idStr || 0 == idStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            //int count = int.Parse(dict["LoadCount"]);
            SDKClient.Instance.ChatManager.SendMessageReadAck(dict["id"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
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
        InputAlertConfig config = new InputAlertConfig("更新消息id", (dict) =>
        {
            string messageId = dict["messageId"]; ;

            Message msg = SDKClient.Instance.ChatManager.LoadMessage(messageId);
            if (null == msg)
            {
                UIManager.ErrorAlert(transform, -1, " cannot find the message");
                return;
            }

            msg.Status = MessageStatus.PROGRESS;

            bool ret = SDKClient.Instance.ChatManager.UpdateMessage(msg);

            if (ret)
                UIManager.SuccessAlert(transform);
            else
                UIManager.ErrorAlert(transform, -1, "UpdateMessagen failed");

        });

        config.AddField("messageId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UpdateMessageBtnAction");
    }

    void RemoveMessagesBeforeTimestampBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("移除时间戳之前消息", (dict) =>
        {
            string tsStr = dict["ts"];
            if (null == tsStr || 0 == tsStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            long ts = long.Parse(tsStr);
            Debug.Log($"time: {ts}");
            SDKClient.Instance.ChatManager.RemoveMessagesBeforeTimestamp(ts, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        });

        config.AddField("ts");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveMessagesBeforeTimestampBtnAction");
    }

    void DeleteConversationFromServerBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            string chatType = dict["ConversationType(0/1/2)"];
            if (null == conversationId || 0 == conversationId.Length || null == chatType || 0 == chatType.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

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

            SDKClient.Instance.ChatManager.DeleteConversationFromServer(conversationId, type, true, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("ConversationType(0/1/2)");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("DeleteConversationFromServerBtnAction");
    }


    void DeleteLocalConversationAndMessage()
    {


        InputAlertConfig config = new InputAlertConfig("根据会话id删除会话以及消息", (dict) =>
        {
            string idStr = dict["id"];
            if (null == idStr || 0 == idStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            bool ret = SDKClient.Instance.ChatManager.DeleteConversation(idStr);
            if (ret == true)
            {
                UIManager.SuccessAlert(transform);
            }
            else
            {
                UIManager.ErrorAlert(transform, 0, "删除失败");
            }

        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);
    }

    void GetConversationsFromServerWithCursorAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            bool pinOnly = dict["pinOnly"].CompareTo("true") == 0;
            string cursor = dict["cursor"];
            int limit = int.Parse(dict["limit"]);

            SDKClient.Instance.ChatManager.GetConversationsFromServerWithCursor(pinOnly, cursor, limit, new ValueCallBack<CursorResult<Conversation>>(
            onSuccess: (ret) =>
            {
                string str = "";
                foreach (var it in ret.Data)
                {
                    str = str + it.Id + (it.IsPinned ? "true" : "false") + it.PinnedTime.ToString() + ";";
                }
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
            ));
        });

        config.AddField("pinOnly");
        config.AddField("cursor");
        config.AddField("limit");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("GetConversationsFromServerWithCursorAction");
    }

    void GetConversationsFromServerWithCursorWithMarkAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            int mark = int.Parse(dict["mark"]);
            string cursor = dict["cursor"];
            int limit = int.Parse(dict["limit"]);

            SDKClient.Instance.ChatManager.GetConversationsFromServerWithCursor((MarkType)mark, cursor, limit, new ValueCallBack<CursorResult<Conversation>>(
            onSuccess: (ret) =>
            {
                string str = "";
                foreach (var it in ret.Data)
                {
                    string marks_str = string.Join(", ", it.Marks());
                    str = str + it.Id + (it.IsPinned ? "true" : "false") + "," + it.PinnedTime.ToString() + "," + marks_str + ";";
                }
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
            ));
        });

        config.AddField("mark");
        config.AddField("cursor");
        config.AddField("limit");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("GetConversationsFromServerWithCursorActionWithMark");
    }

    void GetConversationsFromServerWithPageAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string pageNumStr = dict["PageNum"];
            string pageSizeStr = dict["PageSize"];

            int pageNum = int.Parse(pageNumStr);
            int pageSize = int.Parse(pageSizeStr);

            SDKClient.Instance.ChatManager.GetConversationsFromServerWithPage(pageNum, pageSize, new ValueCallBack<List<Conversation>>(
            onSuccess: (ret) =>
            {
                string str = "";
                foreach (var it in ret)
                {
                    str = str + it.Id + ";";
                }
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
            ));
        });

        config.AddField("PageNum");
        config.AddField("PageSize");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("GetConversationsFromServerWithPageAction");
    }

    void RemoveMessagesFromServerWithMsgIdsAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            string chatType = dict["ConversationType(0/1/2)"];
            string msgId = dict["MsgId"];
            if (null == conversationId || 0 == conversationId.Length || null == chatType || 0 == chatType.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

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

            List<string> list = new List<string>();
            list.Add(msgId);

            SDKClient.Instance.ChatManager.RemoveMessagesFromServer(conversationId, type, list, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("ConversationType(0/1/2)");
        config.AddField("MsgId");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("RemoveMessagesFromServerWithMsgIdsAction");
    }

    void RemoveMessagesFromServerWithTsAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            string chatType = dict["ConversationType(0/1/2)"];
            string tsStr = dict["TimeStamp"];
            if (null == conversationId || 0 == conversationId.Length || null == chatType || 0 == chatType.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

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

            long ts = long.Parse(tsStr);

            SDKClient.Instance.ChatManager.RemoveMessagesFromServer(conversationId, type, ts, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("ConversationType(0/1/2)");
        config.AddField("TimeStamp");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("RemoveMessagesFromServerWithTsAction");
    }

    void PinConversationBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string conversationId = dict["ConversationId"];
            bool isPinned = dict["isPinned"].CompareTo("true") == 0;

            SDKClient.Instance.ChatManager.PinConversation(conversationId, isPinned, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("ConversationId");
        config.AddField("isPinned");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("PinConversationBtnAction");
    }

    void ModifyMessageAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            TextBody tb = new TextBody(dict["text"]);

            SDKClient.Instance.ChatManager.ModifyMessage(dict["msgId"], tb, new ValueCallBack<Message>(
             onSuccess: (dmsg) =>
             {
                 UIManager.TitleAlert(transform, "成功", dmsg.ToJsonObject().ToString());
             },
             onError: (code, desc) =>
             {
                 UIManager.ErrorAlert(transform, code, desc);
             }
            ));
        });

        config.AddField("msgId");
        config.AddField("text");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("ModifyMessageAction");
    }
    void DownloadCombineMessages(Message _msg, int _level)
    {
        if (null == _msg || _msg.Body.Type != MessageBodyType.COMBINE)
        {
            Debug.Log($"Cannot find any combine message. --- level:{_level}");
            return;
        }

        SDKClient.Instance.ChatManager.FetchCombineMessageDetail(_msg, new ValueCallBack<List<Message>>(
            onSuccess: (list) => {
                Debug.Log($"DownloadCombineMessages found {list.Count} messages --- level:{_level}");

                foreach (var it in list)
                {
                    string json = it.ToJsonObject().ToString();
                    Debug.Log($"level:{_level}, msg is:{json}");

                    if (it.Body.Type == MessageBodyType.COMBINE)
                    {
                        DownloadCombineMessages(it, _level + 1);
                    }
                }
            },
            onError: (code, desc) => {
                Debug.LogError($"DownloadCombineMessages failed, code:{code}, desc:{desc} --- level: {_level}");
            }
        ));
    }

    void DownloadCombineMessagesAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            Message msg = SDKClient.Instance.ChatManager.LoadMessage(dict["msgId"]);
            DownloadCombineMessages(msg, 0);
        });

        config.AddField("msgId");

        UIManager.DefaultInputAlert(transform, config);
        Debug.Log("DownloadCombineMessagesAction");
    }

    void MarkConversationsBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string convId1 = dict["convId1"];
            string convId2 = dict["convId2"];
            bool isMarked = dict["isMarked"].CompareTo("true") == 0;
            MarkType mark = (MarkType)(int.Parse(dict["mark"]));

            List<string> list = new List<string>();
            list.Add(convId1);
            list.Add(convId2);

            SDKClient.Instance.ChatManager.MarkConversations(list, isMarked, (MarkType)mark, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("convId1");
        config.AddField("convId2");
        config.AddField("isMarked");
        config.AddField("mark");
        UIManager.DefaultInputAlert(transform, config);
    }

    void DeleteAllMessagesAndConversationsBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            bool clearServerData = dict["clearServerData"].CompareTo("true") == 0;

            SDKClient.Instance.ChatManager.DeleteAllMessagesAndConversations(clearServerData, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("clearServerData");
        UIManager.DefaultInputAlert(transform, config);
    }

    void PinMessageBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["msgId"];
            bool isPinned = dict["isPinned"].CompareTo("true") == 0;

            SDKClient.Instance.ChatManager.PinMessage(msgId, isPinned, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("msgId");
        config.AddField("isPinned");
        UIManager.DefaultInputAlert(transform, config);
    }

    void GetPinnedMessagesFromServerBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("根据关键字获取消息", (dict) =>
        {
            string convId = dict["convId"];

            SDKClient.Instance.ChatManager.GetPinnedMessagesFromServer(convId, new ValueCallBack<List<Message>>(
                onSuccess: (list) => {
                    string str = "";
                    foreach(var it in list)
                    {
                        str = str + ", msgid:" + it.MsgId + "," + ", pinnedBy:" + it.PinnedInfo.PinnedBy + ", pinnedAt:" + it.PinnedInfo.PinnedAt + ";";
                    }
                    UIManager.DefaultAlert(transform, $"找到的消息条数: {list.Count} and content: {str}");
                },
                onError: (code, desc) => {
                    UIManager.DefaultAlert(transform, "未找到消息");
                }
            ));
        });

        config.AddField("convId");
        UIManager.DefaultInputAlert(transform, config);
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
        List<string> list = new List<string>();

        foreach (var msg in messages)
        {
            list.Add(msg.MsgId);
            Debug.Log($"msg content: {msg.ToJsonObject().ToString()}");
            Debug.Log($"msg pininfo: {msg.PinnedInfo.PinnedBy}, {msg.PinnedInfo.PinnedAt}");
        }
        string str = string.Join(",", list.ToArray());

        UIManager.DefaultAlert(transform, $"OnMessagesReceived: {str}");
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

    /*public void OnMessagesRecalled(List<Message> messages)
    {
        UIManager.DefaultAlert(transform, $"OnMessagesRecalled: {messages.Count}");
    }*/

    public void OnMessagesRecalled(List<RecallMessageInfo> recallMessagesInfo)
    {
        List<string> list = new List<string>();
        foreach (var msg in recallMessagesInfo)
        {
            list.Add(msg.ToJsonObject().ToString());
        }
        string s = "OnMessagesRecalled:" + string.Join(", ", list.ToArray());
        UIManager.DefaultAlert(transform, s);
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

    public void MessageReactionDidChange(List<MessageReactionChange> list)
    {
        string detail = "";
        foreach(var it in list)
        {
            detail = detail + "ReactionChange:{" + it.ToJsonObject().ToString() + "};";
        }
        UIManager.DefaultAlert(transform, $"MessageReactionDidChange: {list.Count}, detail: {detail}");
    }

    public void OnMessageContentChanged(Message msg, string operatorId, long operationTime)
    {
        AgoraChat.MessageBody.TextBody tb = (AgoraChat.MessageBody.TextBody)msg.Body;
        UIManager.DefaultAlert(transform, $"OnMessageContentChanged change msgId:{msg.MsgId}, text:{tb.Text}; operatorId:{operatorId}, operationTime:{operationTime}");
    }

    public void OnMessagePinChanged(string messageId, string conversationId, bool isPinned, string operatorId, long operationTime)
    {
        UIManager.DefaultAlert(transform, $"OnMessagePinChanged msgId:{messageId}, convId:{conversationId}, isPinned:{isPinned}, operatorId:{operatorId}, operationTime:{operationTime}");       
    }
}
