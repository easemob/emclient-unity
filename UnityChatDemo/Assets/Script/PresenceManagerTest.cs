
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;
using System.Collections.Generic;

public class PresenceManagerTest : MonoBehaviour, IPresenceManagerDelegate
{

    private Button backButton;
    private Button PublishPresenceBtn;
    private Button SubscribePresencesBtn;
    private Button UnsubscribePresencesBtn;
    private Button FetchSubscribedMembersBtn;
    private Button FetchPresenceStatusBtn;


    private void Awake()
    {
        Debug.Log("PresenceManagerTest script has load");

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        PublishPresenceBtn = transform.Find("Scroll View/Viewport/Content/PublishPresenceBtn").GetComponent<Button>();
        SubscribePresencesBtn = transform.Find("Scroll View/Viewport/Content/SubscribePresencesBtn").GetComponent<Button>();
        UnsubscribePresencesBtn = transform.Find("Scroll View/Viewport/Content/UnsubscribePresencesBtn").GetComponent<Button>();
        FetchSubscribedMembersBtn = transform.Find("Scroll View/Viewport/Content/FetchSubscribedMembersBtn").GetComponent<Button>();
        FetchPresenceStatusBtn = transform.Find("Scroll View/Viewport/Content/FetchPresenceStatusBtn").GetComponent<Button>();


        PublishPresenceBtn.onClick.AddListener(PublishPresenceBtnAction);
        SubscribePresencesBtn.onClick.AddListener(SubscribePresencesBtnAction);
        UnsubscribePresencesBtn.onClick.AddListener(UnsubscribePresencesBtnAction);
        FetchSubscribedMembersBtn.onClick.AddListener(FetchSubscribedMembersBtnAction);
        FetchPresenceStatusBtn.onClick.AddListener(FetchPresenceStatusBtnAction);

        SDKClient.Instance.PresenceManager.AddPresenceManagerDelegate(this);
    }

    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void PublishPresenceBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("发布状态", (dict) =>
        {
            string info = dict["desc"];
            if (null == info || 0 == info.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.PresenceManager.PublishPresence(info, new CallBack(
            onSuccess: () =>
            {
                UIManager.DefaultAlert(transform, "发布成功");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"发布失败:{code}: {desc}");
            }
            ));
        });

        config.AddField("desc");

        UIManager.DefaultInputAlert(transform, config);
    }

    void SubscribePresencesBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("订阅用户", (dict) =>
        {
            string userId = dict["desc"];
            if (null == userId || 0 == userId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> userIds = new List<string>();
            userIds.Add(userId);
            SDKClient.Instance.PresenceManager.SubscribePresences(userIds, 10000, new ValueCallBack<List<Presence>>(
                onSuccess: (list) =>
                {
                    List<string> pubilsher = new List<string>();
                    foreach (var presence in list)
                    {
                        pubilsher.Add(presence.Publisher);
                    }

                    UIManager.DefaultAlert(transform, $"订阅成功: {string.Join(",", pubilsher.ToArray())}");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"订阅失败:{code}: {desc}");
                }
            ));
        });

        config.AddField("desc");

        UIManager.DefaultInputAlert(transform, config);
    }
    void UnsubscribePresencesBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("取消订阅", (dict) =>
        {
            string userId = dict["desc"];
            if (null == userId || 0 == userId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> userIds = new List<string>();
            userIds.Add(userId);
            SDKClient.Instance.PresenceManager.UnsubscribePresences(userIds, new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "取关成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"取关失败:{code}: {desc}");
                }
            ));
        });

        config.AddField("desc");

        UIManager.DefaultInputAlert(transform, config);
    }
    void FetchSubscribedMembersBtnAction()
    {

        SDKClient.Instance.PresenceManager.FetchSubscribedMembers(0, 200, new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                UIManager.DefaultAlert(transform, $"订阅用户列表: {string.Join(",", list.ToArray())}");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"获取失败:{code}: {desc}");
            }
        ));
    }
    void FetchPresenceStatusBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("查询订阅用户", (dict) =>
        {
            string userId = dict["desc"];
            if (null == userId || 0 == userId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> userIds = new List<string>();
            userIds.Add(userId);
            SDKClient.Instance.PresenceManager.FetchPresenceStatus(userIds, new ValueCallBack<List<Presence>>(
                onSuccess: (list) =>
                {
                    List<string> pubilsher = new List<string>();
                    foreach (var presence in list)
                    {
                        pubilsher.Add(presence.Publisher);
                    }

                    UIManager.DefaultAlert(transform, $"获取成功: {string.Join(",", pubilsher.ToArray())}");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"获取失败:{code}: {desc}");
                }
            ));
        });

        config.AddField("desc");

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

    public void OnPresenceUpdated(List<Presence> presences)
    {
        List<string> list = new List<string>();
        foreach (var presence in presences)
        {
            list.Add(presence.Publisher);
        }
        UIManager.DefaultAlert(transform, $"发布状态: {string.Join(",", list.ToArray())}");
    }
}
