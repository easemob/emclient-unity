using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

public class ThreadManagerTest : MonoBehaviour, IChatThreadManagerDelegate
{

    private Text threadText;
    private Button backButton;

    private Button CreateThreadBtn;
    private Button JoinThreadBtn;
    private Button LeaveThreadBtn;
    private Button DestroyThreadBtn;
    private Button RemoveThreadMemberBtn;
    private Button ChangeThreadNameBtn;
    private Button FetchThreadMembersBtn;
    private Button FetchThreadListOfGroupBtn;
    private Button FetchMineJoinedThreadListBtn;
    private Button GetThreadDetailBtn;
    private Button GetLastMessageAccordingThreadsBtn;


    private string groupId
    {
        get => threadText.text;
    }

    private void Awake()
    {
        Debug.Log("ThreadManagerTest script has load");

        threadText = transform.Find("ThreadText/Text").GetComponent<Text>();

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        CreateThreadBtn = transform.Find("Scroll View/Viewport/Content/CreateThreadBtn").GetComponent<Button>();
        JoinThreadBtn = transform.Find("Scroll View/Viewport/Content/JoinThreadBtn").GetComponent<Button>();
        LeaveThreadBtn = transform.Find("Scroll View/Viewport/Content/LeaveThreadBtn").GetComponent<Button>();
        DestroyThreadBtn = transform.Find("Scroll View/Viewport/Content/DestroyThreadBtn").GetComponent<Button>();
        RemoveThreadMemberBtn = transform.Find("Scroll View/Viewport/Content/RemoveThreadMemberBtn").GetComponent<Button>();
        ChangeThreadNameBtn = transform.Find("Scroll View/Viewport/Content/ChangeThreadNameBtn").GetComponent<Button>();
        FetchThreadMembersBtn = transform.Find("Scroll View/Viewport/Content/FetchThreadMembersBtn").GetComponent<Button>();
        FetchThreadListOfGroupBtn = transform.Find("Scroll View/Viewport/Content/FetchThreadListOfGroupBtn").GetComponent<Button>();
        FetchMineJoinedThreadListBtn = transform.Find("Scroll View/Viewport/Content/FetchMineJoinedThreadListBtn").GetComponent<Button>();
        GetThreadDetailBtn = transform.Find("Scroll View/Viewport/Content/GetThreadDetailBtn").GetComponent<Button>();
        GetLastMessageAccordingThreadsBtn = transform.Find("Scroll View/Viewport/Content/GetLastMessageAccordingThreadsBtn").GetComponent<Button>();



        CreateThreadBtn.onClick.AddListener(CreateThreadBtnAction);
        JoinThreadBtn.onClick.AddListener(JoinThreadBtnAction);
        LeaveThreadBtn.onClick.AddListener(LeaveThreadBtnAction);
        DestroyThreadBtn.onClick.AddListener(DestroyThreadBtnAction);
        RemoveThreadMemberBtn.onClick.AddListener(RemoveThreadMemberBtnAction);
        ChangeThreadNameBtn.onClick.AddListener(ChangeThreadNameBtnAction);
        FetchThreadMembersBtn.onClick.AddListener(FetchThreadMembersBtnAction);
        FetchThreadListOfGroupBtn.onClick.AddListener(FetchThreadListOfGroupBtnAction);
        FetchMineJoinedThreadListBtn.onClick.AddListener(FetchMineJoinedThreadListBtnAction);
        GetThreadDetailBtn.onClick.AddListener(GetThreadDetailBtnAction);
        GetLastMessageAccordingThreadsBtn.onClick.AddListener(GetLastMessageAccordingThreadsBtnAction);


        SDKClient.Instance.ThreadManager.AddThreadManagerDelegate(this);
    }


    private void OnDestroy()
    {
        SDKClient.Instance.ThreadManager.RemoveThreadManagerDelegate(this);
    }

    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void CreateThreadBtnAction()
    {


        InputAlertConfig config = new InputAlertConfig("消息id", (dict) =>
        {
            string id = dict["msgId"];
            string name = dict["name"];
            if (null == groupId || 0 == groupId.Length || null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.CreateThread(name, id, groupId, new ValueCallBack<ChatThread>(
                onSuccess: (result) =>
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
        config.AddField("name");

        UIManager.DefaultInputAlert(transform, config);

    }
    void JoinThreadBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("threadId", (dict) =>
        {
            string id = dict["threadId"];
            if (null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["threadId"]);
            SDKClient.Instance.ThreadManager.JoinThread(id, new ValueCallBack<ChatThread>(
                onSuccess: (result) =>
                {
                    UIManager.DefaultAlert(transform, $"成功：{result.Name}");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("threadId");

        UIManager.DefaultInputAlert(transform, config);

    }
    void LeaveThreadBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("threadId", (dict) =>
        {
            string id = dict["threadId"];
            if (null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.LeaveThread(id, new CallBack(
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

        config.AddField("threadId");

        UIManager.DefaultInputAlert(transform, config);

    }
    void DestroyThreadBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("threadId", (dict) =>
        {
            string id = dict["threadId"];
            if (null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.DestroyThread(id, new CallBack(
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
        config.AddField("threadId");

        UIManager.DefaultInputAlert(transform, config);

    }

    void RemoveThreadMemberBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("移除member", (dict) =>
        {
            string userId = dict["userId"];
            string threadId = dict["threadId"];
            if (null == userId || 0 == userId.Length || null == threadId || 0 == threadId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.RemoveThreadMember(threadId, userId, new CallBack(
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
        config.AddField("threadId");
        config.AddField("userId");

        UIManager.DefaultInputAlert(transform, config);

    }
    void ChangeThreadNameBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string threadId = dict["threadId"];
            string newName = dict["newName"];
            if (null == newName || 0 == newName.Length || null == threadId || 0 == threadId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.ChangeThreadName(threadId, newName, new CallBack(
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
        config.AddField("threadId");
        config.AddField("newName");
        UIManager.DefaultInputAlert(transform, config);
    }

    void FetchThreadMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string threadId = dict["threadId"];
            if (null == threadId || 0 == threadId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.ThreadManager.FetchThreadMembers(threadId, callback: new ValueCallBack<CursorResult<string>>(
                onSuccess: (result) =>
                {
                    List<string> list = result.Data;
                    UIManager.DefaultAlert(transform, $"{string.Join(",", list.ToArray())}");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("threadId");
        UIManager.DefaultInputAlert(transform, config);
    }

    void FetchThreadListOfGroupBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (groupId == null || groupId.Length == 0)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            bool joined = dict["joined(0/1)"] == "1" ? true : false;
            SDKClient.Instance.ThreadManager.FetchThreadListOfGroup(groupId, joined, callback: new ValueCallBack<CursorResult<ChatThread>>(
                onSuccess: (result) =>
                {
                    List<string> list = new List<string>();
                    foreach (var thread in result.Data)
                    {
                        list.Add(thread.Tid);
                    }
                    UIManager.DefaultAlert(transform, $"{string.Join(",", list.ToArray())}");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("joined(0/1)");
        UIManager.DefaultInputAlert(transform, config);
    }

    void FetchMineJoinedThreadListBtnAction()
    {
        if (null == groupId || 0 == groupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.ThreadManager.FetchMineJoinedThreadList(callback: new ValueCallBack<CursorResult<ChatThread>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();
                foreach (var thread in result.Data)
                {
                    list.Add(thread.Tid);
                }
                UIManager.DefaultAlert(transform, $"{string.Join(",", list.ToArray())}");
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
    }

    void GetThreadDetailBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string tid = dict["threadId"];
            if (null == tid || 0 == tid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            SDKClient.Instance.ThreadManager.GetThreadDetail(tid, new ValueCallBack<ChatThread>(
                onSuccess: (thread) =>
                {
                    UIManager.DefaultAlert(transform, $"{thread.Tid}: {thread.Name}");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("threadId");
        UIManager.DefaultInputAlert(transform, config);
    }

    void GetLastMessageAccordingThreadsBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string tid = dict["tid"];
            if (null == tid || 0 == tid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(tid);
            SDKClient.Instance.ThreadManager.GetLastMessageAccordingThreads(list, new ValueCallBack<Dictionary<string, Message>>(
                onSuccess: (result) =>
                {
                    List<string> ret = new List<string>();
                    foreach (var _tid in result.Keys)
                    {
                        ret.Add(_tid);
                        ret.Add(":");
                        Message msg = result[_tid];
                        ret.Add(msg.MsgId);
                    }
                    UIManager.DefaultAlert(transform, $"{string.Join(",", ret.ToArray())}");
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("tid");
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

    public void OnChatThreadCreate(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"回调 OnChatThreadCreate: {threadEvent.ChatThread.Name}");
    }

    public void OnChatThreadUpdate(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"回调 OnChatThreadUpdate: {threadEvent.ChatThread.Name}");
    }

    public void OnChatThreadDestroy(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"回调 OnChatThreadDestroy: {threadEvent.ChatThread.Name}");
    }

    public void OnUserKickOutOfChatThread(ChatThreadEvent threadEvent)
    {
        UIManager.DefaultAlert(transform, $"回调 OnUserKickOutOfChatThread: {threadEvent.ChatThread.Name}");
    }
}
