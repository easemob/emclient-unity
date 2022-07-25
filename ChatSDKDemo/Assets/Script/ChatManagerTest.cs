using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChatSDK;
using SimpleJSON;
using UnityEngine.UI;

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
    private Button removeMessagesBeforeTimestampBtn;
    private Button deleteConversationFromServerBtn;
    private Button deleteConversationBtn;

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
        removeMessagesBeforeTimestampBtn = transform.Find("Scroll View/Viewport/Content/RemoveMessagesBeforeTimestampBtn").GetComponent<Button>();
        deleteConversationFromServerBtn = transform.Find("Scroll View/Viewport/Content/DeleteConversationFromServerBtn").GetComponent<Button>();
        deleteConversationBtn = transform.Find("Scroll View/Viewport/Content/DeleteLocalConversationAndMessage").GetComponent<Button>();

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
        removeMessagesBeforeTimestampBtn.onClick.AddListener(RemoveMessagesBeforeTimestampBtnAction);
        deleteConversationFromServerBtn.onClick.AddListener(DeleteConversationFromServerBtnAction);
        deleteConversationBtn.onClick.AddListener(DeleteLocalConversationAndMessage);
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
        InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateTextSendMessage(dict["to"], dict["content"]);
            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    UIManager.TitleAlert(transform, "成功", msg.MsgId);

                    //Message msg1 = SDKClient.Instance.ChatManager.LoadMessage(msg.MsgId);
                    //Debug.Log($"body content:{msg1.MsgId}");
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "发送进度", progress.ToString());
                },
                onError:(code, desc) => {
                    UIManager.ErrorAlert(transform, code, msg.MsgId);
                }            
            ));
        });

        config.AddField("to");
        config.AddField("content");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SendImageBtnAction()
    {
        SDKClient.Instance.ChatManager.FetchSupportLanguages(new ValueCallBack<List<SupportLanguages>>(
             onSuccess: (list) =>
             {
                 Debug.Log($"FetchSupportLanguages found total: {list.Count}");
                 foreach (var lang in list)
                 {
                     Debug.Log($"code: {lang.LanguageCode}, name:{lang.LanguageName}, nativename:{lang.LanguageNativeName}");
                 }
             },
             onError: (code, desc) =>
             {
                 Debug.Log($"FetchSupportLanguages failed, code:{code}, desc:{desc}");
             }
            ));

        return;


        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendImageBtnAction");
    }
    void SendFileBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) => {
            SDKClient.Instance.ThreadManager.DestroyThread(dict["threadId"], new CallBack(
                onSuccess: () =>
                {
                Debug.Log($"DestroyThread sucess");
                },
                onError: (code, desc) =>
                {
                Debug.Log($"DestroyThread failed, code:{code}, desc:{desc}");
                }
                ));
        });

        config.AddField("threadId");
        UIManager.DefaultInputAlert(transform, config);

        return;

        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendFileBtnAction");
    }
    void SendVideoBtnAction()
    {

        SDKClient.Instance.ThreadManager.FetchMineJoinedThreadList("", 10, new ValueCallBack<CursorResult<ThreadEvent>>(
            onSuccess: (cursor_result) =>
            {
                Debug.Log($"FetchMineJoinedThreadList sucess");
                if (null != cursor_result)
                {
                    Debug.Log($"cursor:{cursor_result.Cursor}");
                    foreach (var it in cursor_result.Data)
                    {
                        ThreadEvent thread = it;
                        Debug.Log($"--------------------------------");
                        Debug.Log($"Tid:{thread.Tid}; msgId:{thread.MessageId}; parentId:{thread.ParentId}; owner:{thread.Owner}");
                        Debug.Log($"Name:{thread.Name}; From:{thread.From}; To:{thread.To}; Operation:{thread.Operation}; MessageCount:{thread.MessageCount}");
                        Debug.Log($"MembersCount:{thread.MembersCount}; CreateTimestamp:{thread.CreateTimestamp}; UpdateTimestamp:{thread.UpdateTimestamp}");
                        Debug.Log($"Timestamp:{thread.Timestamp};");
                    }
                }
            },
            onError: (code, desc) =>
            {
                Debug.Log($"FetchMineJoinedThreadList failed, code:{code}, desc:{desc}");
            }
        ));

        return;

        UIManager.UnfinishedAlert(transform);
        Debug.Log("SendVideoBtnAction");

        /*InputAlertConfig config = new InputAlertConfig((dict) => {
            Message msg = Message.CreateVideoSendMessage(dict["to"], "/Users/yuqiang/Test/resource/video.mp4", "", "", 425507);

            SDKClient.Instance.ChatManager.SendMessage(ref msg, new CallBack(
                onSuccess: () => {
                    ChatSDK.MessageBody.VideoBody vb = (ChatSDK.MessageBody.VideoBody)msg.Body;
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
        */
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
            Message msg = Message.CreateCustomSendMessage(dict["to"], dict["custom"]);
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
            SDKClient.Instance.ChatManager.ResendMessage(dict["msgId"], new CallBack(
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

            SDKClient.Instance.ChatManager.RecallMessage(dict["msgId"], new CallBack(
                onSuccess: () => {
                    UIManager.TitleAlert(transform, "成功", "回撤成功");
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "回撤进度", progress.ToString());
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("msgId");
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
                UIManager.SuccessAlert(transform);
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
        // Download Attachment
        /*InputAlertConfig config = new InputAlertConfig((dict) => {

            SDKClient.Instance.ChatManager.DownloadAttachment(dict["msgId"], new CallBack(
                onSuccess: () => {
                    UIManager.TitleAlert(transform, "下载附件成功", "完成");

                    //Message msg = SDKClient.Instance.ChatManager.LoadMessage("Message ID");
                    //if (msg != null)
                    //{
                    //    if (msg.Body.Type == ChatSDK.MessageBodyType.VIDEO) {
                    //        ChatSDK.MessageBody.VideoBody vb = (ChatSDK.MessageBody.VideoBody)msg.Body;

                           //从本地获取短视频文件路径
                    //        string imgLocalUri = vb.LocalPath;
                    //    }

                    //}
                    // else
                    //{
                    //     Debug.Log($"未找到消息");
                    //}
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "下载附件进度", progress.ToString());
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        });
        
        
        // Download Attatchment
        InputAlertConfig config = new InputAlertConfig((dict) => {

            SDKClient.Instance.ChatManager.DownloadThumbnail(dict["msgId"], new CallBack(
                onSuccess: () => {
                    UIManager.TitleAlert(transform, "下载缩略图成功", "完成"); ;
                },
                onProgress: (progress) => {
                    UIManager.TitleAlert(transform, "下载缩略图进度", progress.ToString());
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        });
        */

        //config.AddField("msgId");
        //UIManager.DefaultInputAlert(transform, config);

        UIManager.UnfinishedAlert(transform);
        Debug.Log("DownLoadAttachmentBtnAction");
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
            List<Message> list = SDKClient.Instance.ChatManager.SearchMsgFromDB(dict["Keyword"], maxCount: count);
            if (list != null)
            {
                UIManager.DefaultAlert(transform, $"找到的消息条数: {list.Count}");
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
        UIManager.UnfinishedAlert(transform);
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


    void DeleteLocalConversationAndMessage() {


        InputAlertConfig config = new InputAlertConfig("根据会话id删除会话以及消息", (dict) =>
        {
            string idStr = dict["id"];
            if (null == idStr || 0 == idStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            bool ret = SDKClient.Instance.ChatManager.DeleteConversation(idStr);
            if (ret == true) {
                UIManager.SuccessAlert(transform);
            } else {
                UIManager.ErrorAlert(transform, 0, "删除失败");
            }

        });

        config.AddField("id");

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
            if(msg.Body.Type == MessageBodyType.TXT)
            {
                ChatSDK.MessageBody.TextBody tb = (ChatSDK.MessageBody.TextBody)msg.Body;
                Debug.Log($"text message body: {tb.Text}");
            }
            list.Add(msg.MsgId);
            foreach (string key in msg.Attributes.Keys) {
                AttributeValue a = msg.Attributes[key];
                switch (a.GetAttributeValueType()) {
                    case AttributeValueType.BOOL:
                        {
                            Debug.Log($"{key}|BOOL: {a.GetAttributeValue(AttributeValueType.BOOL)}");
                        }
                        break;
                    case AttributeValueType.INT64:
                        {
                            Debug.Log($"{key}|INT64: {a.GetAttributeValue(AttributeValueType.INT64)}");
                        }
                        break;
                    case AttributeValueType.FLOAT:
                        {
                            Debug.Log($"{key}|FLOAT: {a.GetAttributeValue(AttributeValueType.FLOAT)}");
                        }
                        break;
                    case AttributeValueType.DOUBLE:
                        {
                            Debug.Log($"{key}|DOUBLE: {a.GetAttributeValue(AttributeValueType.DOUBLE)}");
                        }
                        break;
                    case AttributeValueType.STRING:
                        {
                            Debug.Log($"{key}|STRING: {a.GetAttributeValue(AttributeValueType.STRING)}");
                        }
                        break;
                    /*case AttributeValueType.STRVECTOR:
                        {
                            Debug.Log($"{key}|STRVECTOR: {a.GetAttributeValue(AttributeValueType.STRVECTOR)}");
                        }
                        break;*/
                    case AttributeValueType.JSONSTRING:
                        {
                            Debug.Log($"{key}|JSONSTRING: {a.GetAttributeValue(AttributeValueType.JSONSTRING)}");
                        }
                        break;
                }
            }
        
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
