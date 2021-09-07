using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class PushManagerTest : MonoBehaviour
{

    private Button backButton;
    private Button GetPushConfigBtn;
    private Button GetPushConfigFromServerBtn;
    private Button GetNoDisturbGroupsBtn;
    private Button UpdatePushNickNameBtn;
    private Button UpdateHMSPushTokenBtn;
    private Button UpdateFCMPushTokenBtn;
    private Button UpdateAPNSPuthTokenBtn;
    private Button SetNoDisturbBtn;
    private Button SetPushStyleBtn;
    private Button SetGroupToDisturbBtn;



    private void Awake()
    {
        Debug.Log("push manager test script has load");

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        GetPushConfigBtn = transform.Find("Scroll View/Viewport/Content/GetPushConfigBtn").GetComponent<Button>();
        GetPushConfigFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetPushConfigFromServerBtn").GetComponent<Button>();
        GetNoDisturbGroupsBtn = transform.Find("Scroll View/Viewport/Content/GetNoDisturbGroupsBtn").GetComponent<Button>();
        UpdatePushNickNameBtn = transform.Find("Scroll View/Viewport/Content/UpdatePushNickNameBtn").GetComponent<Button>();
        UpdateHMSPushTokenBtn = transform.Find("Scroll View/Viewport/Content/UpdateHMSPushTokenBtn").GetComponent<Button>();
        UpdateFCMPushTokenBtn = transform.Find("Scroll View/Viewport/Content/UpdateFCMPushTokenBtn").GetComponent<Button>();
        UpdateAPNSPuthTokenBtn = transform.Find("Scroll View/Viewport/Content/UpdateAPNSPuthTokenBtn").GetComponent<Button>();
        SetNoDisturbBtn = transform.Find("Scroll View/Viewport/Content/SetNoDisturbBtn").GetComponent<Button>();
        SetPushStyleBtn = transform.Find("Scroll View/Viewport/Content/SetPushStyleBtn").GetComponent<Button>();
        SetGroupToDisturbBtn = transform.Find("Scroll View/Viewport/Content/SetGroupToDisturbBtn").GetComponent<Button>();

        GetPushConfigBtn.onClick.AddListener(GetPushConfigBtnAction);
        GetPushConfigFromServerBtn.onClick.AddListener(GetPushConfigFromServerBtnAction);
        GetNoDisturbGroupsBtn.onClick.AddListener(GetNoDisturbGroupsBtnAction);
        UpdatePushNickNameBtn.onClick.AddListener(UpdatePushNickNameBtnAction);
        UpdateHMSPushTokenBtn.onClick.AddListener(UpdateHMSPushTokenBtnAction);
        UpdateFCMPushTokenBtn.onClick.AddListener(UpdateFCMPushTokenBtnAction);
        UpdateAPNSPuthTokenBtn.onClick.AddListener(UpdateAPNSPuthTokenBtnAction);
        SetNoDisturbBtn.onClick.AddListener(SetNoDisturbBtnAction);
        SetPushStyleBtn.onClick.AddListener(SetPushStyleBtnAction);
        SetGroupToDisturbBtn.onClick.AddListener(SetGroupToDisturbBtnAction);

    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void GetPushConfigBtnAction() {
        PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();
        if(null != config)
            UIManager.DefaultAlert(transform, config.ToString());
        else
            UIManager.DefaultAlert(transform, "未配置");
    }

    void GetPushConfigFromServerBtnAction() {
        SDKClient.Instance.PushManager.GetPushConfigFromServer(new ValueCallBack<PushConfig>(
            onSuccess: (config) => {
                UIManager.DefaultAlert(transform, config.ToString());
            },
             onError:(code, desc) => {
                 UIManager.ErrorAlert(transform, code, desc);
             }
        ));
    }
    void GetNoDisturbGroupsBtnAction() {
        List<string> list = SDKClient.Instance.PushManager.GetNoDisturbGroups();
        if(list.Count > 0)
        {
            string str = string.Join(",", list.ToArray());
            UIManager.DefaultAlert(transform, str);
        }
        else
        {
            UIManager.DefaultAlert(transform, "未配置");
        }
        
    }
    void UpdatePushNickNameBtnAction() {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string nickname = dict["nickname"];
            if (null == nickname || 0 == nickname.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            SDKClient.Instance.PushManager.UpdatePushNickName(dict["nickname"], new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError:(code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("nickname");

        UIManager.DefaultInputAlert(transform, config);
    }
    void UpdateHMSPushTokenBtnAction() {
        UIManager.UnfinishedAlert(transform);
    }
    void UpdateFCMPushTokenBtnAction() {
        UIManager.UnfinishedAlert(transform);
    }
    void UpdateAPNSPuthTokenBtnAction() {
        UIManager.UnfinishedAlert(transform);
    }
    void SetNoDisturbBtnAction() {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string noDisturbStr = dict["NoDisturb(0/1)"];
            string startTimeStr = dict["StartTime(0~24)"];
            string endTimeStr = dict["EndTime(0~24)"];
            if (null == noDisturbStr || 0 == noDisturbStr.Length ||
                null == startTimeStr || 0 == startTimeStr.Length ||
                null == endTimeStr || 0 == endTimeStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            int noDisturb = int.Parse(dict["NoDisturb(0/1)"]);
            int startTime = int.Parse(dict["StartTime(0~24)"]);
            int endTime = int.Parse(dict["EndTime(0~24)"]);
            SDKClient.Instance.PushManager.SetNoDisturb(noDisturb == 0 ? false : true, startTime, endTime, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("NoDisturb(0/1)");
        config.AddField("StartTime(0~24)");
        config.AddField("EndTime(0~24)");

        UIManager.DefaultInputAlert(transform, config);
    }
    void SetPushStyleBtnAction() {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string pushStyleStr = dict["PushStyle(0/1)"];
            if (null == pushStyleStr || 0 == pushStyleStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            int pushStyle = int.Parse(dict["PushStyle(0/1)"]);
            SDKClient.Instance.PushManager.SetPushStyle(pushStyle == 0 ? PushStyle.Simple: PushStyle.Summary, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("PushStyle(0/1)");
        UIManager.DefaultInputAlert(transform, config);
    }
    void SetGroupToDisturbBtnAction() {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string groupId = dict["groupId"];

            string noDisturbStr = dict["NoDisturb(0/1)"];
            if (null == noDisturbStr || 0 == noDisturbStr.Length || null == groupId || 0 == groupId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            int noDisturb = int.Parse(dict["NoDisturb(0/1)"]);
            
            SDKClient.Instance.PushManager.SetGroupToDisturb(groupId, noDisturb == 1 ? true : false, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("NoDisturb(0/1)");
        config.AddField("groupId");
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
}
