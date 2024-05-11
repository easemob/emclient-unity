using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

public class ConversationManagerTest : MonoBehaviour
{
    private Text conversationText;
    private Toggle chatToggle;
    private Toggle groupToggle;
    private Toggle roomToggle;
    private Button backButton;
    private Button LastMessageBtn;
    private Button LastReceiveMessageBtn;
    private Button GetExtBtn;
    private Button SetExtBtn;
    private Button UnReadCountBtn;
    private Button MarkMessageAsReadBtn;
    private Button MarkAllMessageAsReadBtn;
    private Button InsertMessageBtn;
    private Button AppendMessageBtn;
    private Button UpdateMessageBtn;
    private Button DeleteMessageBtn;
    private Button DeleteAllMessageBtn;
    private Button LoadMessageBtn;
    private Button LoadMessagesBtn;
    private Button LoadMessagesWithKeywordBtn;
    private Button LoadMessagesWithScopeBtn;
    private Button LoadMessagesWithTimeBtn;
    private Button LoadMessagesWithMsgTypeBtn;
    private Button MessagesCountBtn;
    private Button DeleteMessagesBtn;
    private Button PinnedMessagesBtn;

    private string conversationId
    {
        get => conversationText.text;
    }

    private ConversationType convType = ConversationType.Chat;

    private void Awake()
    {
        Debug.Log("conversation manager test script has load");

        conversationText = transform.Find("ConversationText/Text").GetComponent<Text>();

        ToggleGroup toggleGroup = transform.Find("ChatToggleGroup").GetComponent<ToggleGroup>();

        chatToggle = toggleGroup.transform.Find("Single").GetComponent<Toggle>();
        groupToggle = toggleGroup.transform.Find("Group").GetComponent<Toggle>();
        roomToggle = toggleGroup.transform.Find("Room").GetComponent<Toggle>();

        chatToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                convType = ConversationType.Chat;
                Debug.Log("单聊");
            }
        });

        groupToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                convType = ConversationType.Group;
                Debug.Log("群聊");
            }
        });

        roomToggle.onValueChanged.AddListener(isOn =>
        {
            if (isOn)
            {
                convType = ConversationType.Room;
                Debug.Log("聊天室");
            }
        });

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        LastMessageBtn = transform.Find("Scroll View/Viewport/Content/LastMessageBtn").GetComponent<Button>();
        LastReceiveMessageBtn = transform.Find("Scroll View/Viewport/Content/LastReceiveMessageBtn").GetComponent<Button>();
        GetExtBtn = transform.Find("Scroll View/Viewport/Content/GetExtBtn").GetComponent<Button>();
        SetExtBtn = transform.Find("Scroll View/Viewport/Content/SetExtBtn").GetComponent<Button>();
        UnReadCountBtn = transform.Find("Scroll View/Viewport/Content/UnReadCountBtn").GetComponent<Button>();
        MarkMessageAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkMessageAsReadBtn").GetComponent<Button>();
        MarkAllMessageAsReadBtn = transform.Find("Scroll View/Viewport/Content/MarkAllMessageAsReadBtn").GetComponent<Button>();
        InsertMessageBtn = transform.Find("Scroll View/Viewport/Content/InsertMessageBtn").GetComponent<Button>();
        AppendMessageBtn = transform.Find("Scroll View/Viewport/Content/AppendMessageBtn").GetComponent<Button>();
        UpdateMessageBtn = transform.Find("Scroll View/Viewport/Content/UpdateMessageBtn").GetComponent<Button>();
        DeleteMessageBtn = transform.Find("Scroll View/Viewport/Content/DeleteMessageBtn").GetComponent<Button>();
        DeleteAllMessageBtn = transform.Find("Scroll View/Viewport/Content/DeleteAllMessageBtn").GetComponent<Button>();
        LoadMessageBtn = transform.Find("Scroll View/Viewport/Content/LoadMessageBtn").GetComponent<Button>();
        LoadMessagesBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesBtn").GetComponent<Button>();
        LoadMessagesWithKeywordBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithKeywordBtn").GetComponent<Button>();
        LoadMessagesWithScopeBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithScopeBtn").GetComponent<Button>();
        LoadMessagesWithTimeBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithTimeBtn").GetComponent<Button>();
        LoadMessagesWithMsgTypeBtn = transform.Find("Scroll View/Viewport/Content/LoadMessagesWithMsgTypeBtn").GetComponent<Button>();
        MessagesCountBtn = transform.Find("Scroll View/Viewport/Content/MessagesCountBtn").GetComponent<Button>();
        DeleteMessagesBtn = transform.Find("Scroll View/Viewport/Content/DeleteMessagesBtn").GetComponent<Button>();
        PinnedMessagesBtn = transform.Find("Scroll View/Viewport/Content/PinnedMessagesBtn").GetComponent<Button>();


        LastMessageBtn.onClick.AddListener(LastMessageBtnAction);
        LastReceiveMessageBtn.onClick.AddListener(LastReceiveMessageBtnAction);
        GetExtBtn.onClick.AddListener(GetExtBtnAction);
        SetExtBtn.onClick.AddListener(SetExtBtnAction);
        UnReadCountBtn.onClick.AddListener(UnReadCountBtnAction);
        MarkMessageAsReadBtn.onClick.AddListener(MarkMessageAsReadBtnAction);
        MarkAllMessageAsReadBtn.onClick.AddListener(MarkAllMessageAsReadBtnAction);
        InsertMessageBtn.onClick.AddListener(InsertMessageBtnAction);
        AppendMessageBtn.onClick.AddListener(AppendMessageBtnAction);
        UpdateMessageBtn.onClick.AddListener(UpdateMessageBtnAction);
        DeleteMessageBtn.onClick.AddListener(DeleteMessageBtnAction);
        DeleteAllMessageBtn.onClick.AddListener(DeleteAllMessageBtnAction);
        LoadMessageBtn.onClick.AddListener(LoadMessageBtnAction);
        LoadMessagesBtn.onClick.AddListener(LoadMessagesBtnAction);
        LoadMessagesWithKeywordBtn.onClick.AddListener(LoadMessagesWithKeywordBtnAction);
        LoadMessagesWithScopeBtn.onClick.AddListener(LoadMessagesWithScopeBtnAction);
        LoadMessagesWithTimeBtn.onClick.AddListener(LoadMessagesWithTimeBtnAction);
        LoadMessagesWithMsgTypeBtn.onClick.AddListener(LoadMessagesWithMsgTypeBtnAction);
        MessagesCountBtn.onClick.AddListener(MessagesCountBtnAction);
        DeleteMessagesBtn.onClick.AddListener(DeleteMessagesBtnAction);
        PinnedMessagesBtn.onClick.AddListener(PinnedMessagesBtnAction);
    }




    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void LastMessageBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

        if (conv.LastMessage != null)
        {
            UIManager.SuccessAlert(transform);
        }
        else
        {
            UIManager.DefaultAlert(transform, "未获取到最后一条消");
        }
    }
    void LastReceiveMessageBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

        if (conv.LastReceivedMessage != null)
        {
            UIManager.SuccessAlert(transform);
        }
        else
        {
            UIManager.DefaultAlert(transform, "未获取到最后一条消");
        }
    }
    void GetExtBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

        if (conv.Ext != null)
        {
            List<string> list = new List<string>();
            if (0 == conv.Ext.Count)
            {
                UIManager.DefaultAlert(transform, "未获取到Ext");
            }
            else
            {
                foreach (var kv in conv.Ext)
                {
                    list.Add($"{kv.Key}:{kv.Value}");
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            }
        }
        else
        {
            UIManager.DefaultAlert(transform, "未获取到Ext");
        }
    }
    void SetExtBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string key = dict["key"];
            string value = dict["value"];

            if (null == conversationId || 0 == conversationId.Length ||
                null == key || 0 == key.Length ||
                null == value || 0 == value.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

            Dictionary<string, string> KV = new Dictionary<string, string>();
            KV.Add(key, value);
            conv.Ext = KV;
            UIManager.DefaultAlert(transform, "已设置");

        });

        config.AddField("key");
        config.AddField("value");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("SetExtBtnAction");
    }
    void UnReadCountBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        UIManager.DefaultAlert(transform, $"未读数: {conv.UnReadCount.ToString()}");
        Debug.Log("UnReadCountBtnAction");
    }
    void MarkMessageAsReadBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["MsgId"];
            if (null == conversationId || 0 == conversationId.Length || null == msgId || 0 == msgId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            conv.MarkMessageAsRead(msgId);
            UIManager.DefaultAlert(transform, "已设置");

        });

        config.AddField("MsgId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("MarkMessageAsReadBtnAction");
    }
    void MarkAllMessageAsReadBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        conv.MarkAllMessageAsRead();
        UIManager.DefaultAlert(transform, "已设置");

        Debug.Log("MarkAllMessageAsReadBtnAction");
    }
    void InsertMessageBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

        Message msg = Message.CreateTextSendMessage(conversationId, "test");
        bool ret = conv.InsertMessage(msg);
        UIManager.DefaultAlert(transform, ret ? "插入成功" : "插入失败");
    }
    void AppendMessageBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);

        Message msg = Message.CreateTextSendMessage(conversationId, "test");
        bool ret = conv.AppendMessage(msg);
        UIManager.DefaultAlert(transform, ret ? "添加成功" : "添加失败");
    }
    void UpdateMessageBtnAction()
    {
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        Message msg = conv.LastMessage;
        msg.LocalTime = 10000;
        bool ret = conv.UpdateMessage(msg);
        UIManager.DefaultAlert(transform, ret ? "更新成功" : "更新失败");
    }
    void DeleteMessageBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["MsgId"];
            if (null == conversationId || 0 == conversationId.Length || null == msgId || 0 == msgId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            conv.DeleteMessage(msgId);
            UIManager.DefaultAlert(transform, "已删除");

        });

        config.AddField("MsgId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("DeleteMessageBtnAction");
    }

    void DeleteMessagesBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            long startTime = long.Parse(dict["StartTime"]);
            long endTime = long.Parse(dict["EndTime"]);
            if (null == conversationId || 0 == conversationId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            if (conv.DeleteMessages(startTime, endTime))
            {
                UIManager.DefaultAlert(transform, "删除成功");
            }
            else
            {
                UIManager.DefaultAlert(transform, "删除失败");
            }
        });

        config.AddField("StartTime");
        config.AddField("EndTime");
        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("DeleteMessagesBtnAction");
    }

    void DeleteAllMessageBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        conv.DeleteAllMessages();
        UIManager.DefaultAlert(transform, "已删除");

        Debug.Log("DeleteAllMessageBtnAction");
    }
    void LoadMessageBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string msgId = dict["MsgId"];
            if (null == conversationId || 0 == conversationId.Length || null == msgId || 0 == msgId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            Message msg = conv.LoadMessage(msgId);
            UIManager.DefaultAlert(transform, msg == null ? "获取失败" : "获取成功");
        });

        config.AddField("MsgId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("LoadMessageBtnAction");
    }

    void LoadMessagesBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        conv.LoadMessages(null, count: 200, callback: new ValueCallBack<List<Message>>(
            onSuccess: (list) =>
            {
                UIManager.DefaultAlert(transform, $"获取到{list.Count}条消息");
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("LoadMessagesBtnAction");
    }
    void LoadMessagesWithKeywordBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string keyword = dict["keyword"];
            if (null == conversationId || 0 == conversationId.Length || null == keyword || 0 == keyword.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            conv.LoadMessagesWithKeyword(keyword, count: 200, callback: new ValueCallBack<List<Message>>(
                onSuccess: (list) =>
                {
                    UIManager.DefaultAlert(transform, $"获取到{list.Count}条消息");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("keyword");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("LoadMessagesWithKeywordBtnAction");
    }

    void LoadMessagesWithScopeBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string keyword = dict["keyword"];
            MessageSearchScope scope = (MessageSearchScope)(int.Parse(dict["scope"]));
            if (null == conversationId || 0 == conversationId.Length || null == keyword || 0 == keyword.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
            conv.LoadMessagesWithScope(keyword, scope, -1, 10, "", MessageSearchDirection.UP, new ValueCallBack<List<Message>>(
                onSuccess: (list) =>
                {
                    UIManager.DefaultAlert(transform, $"获取到{list.Count}条消息");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("keyword");
        config.AddField("scope");

        UIManager.DefaultInputAlert(transform, config);
    }

    void LoadMessagesWithTimeBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        long startTime = 0;
        long endTime = (long)(DateTime.UtcNow.Ticks);
        //long endTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds);

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        conv.LoadMessagesWithTime(startTime, endTime, count: 200, new ValueCallBack<List<Message>>(
            onSuccess: (list) =>
            {
                UIManager.DefaultAlert(transform, $"获取到{list.Count}条消息");
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("LoadMessagesWithTimeBtnAction");
    }
    void LoadMessagesWithMsgTypeBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        MessageBodyType type = MessageBodyType.TXT;

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        conv.LoadMessagesWithMsgType(type, null, -1, count: 200, MessageSearchDirection.UP, new ValueCallBack<List<Message>>(
            onSuccess: (list) =>
            {
                UIManager.DefaultAlert(transform, $"获取到{list.Count}条消息");
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
        Debug.Log("LoadMessagesWithMsgTypeBtnAction");
    }

    void MessagesCountBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        int count = conv.MessagesCount();
        UIManager.DefaultAlert(transform, $"messagecount:{count}");

        Debug.Log("MessagesCountBtnAction");
    }

    void PinnedMessagesBtnAction()
    {
        if (null == conversationId || 0 == conversationId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        Conversation conv = SDKClient.Instance.ChatManager.GetConversation(conversationId, convType);
        List<Message> list = conv.PinnedMessages();
        string str = "";
        foreach (var it in list)
        {
            str = str + ", msgid:" + it.MsgId + "," + ", pinnedBy:" + it.PinnedInfo.PinnedBy + ", pinnedAt:" + it.PinnedInfo.PinnedAt + ";";
        }
        UIManager.DefaultAlert(transform, $"找到的消息条数: {list.Count}, pinnedinfo: {str}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
