using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ChatSDK;

public class DemoPush : MonoBehaviour
{

    public Button BackBtn;
    public Button Custom1Btn;
    public Button Custom2Btn;
    public Button Custom3Btn;
    public Button Custom4Btn;
    public Button Custom5Btn;
    public Button Custom6Btn;
    public Button Custom7Btn;
    public Button Custom8Btn;
    public Button Custom9Btn;


    // Start is called before the first frame update

    void Start()
    {
        Custom1Btn.onClick.AddListener(Custom1Action);
        Custom2Btn.onClick.AddListener(Custom2Action);
        Custom3Btn.onClick.AddListener(Custom3Action);
        Custom4Btn.onClick.AddListener(Custom4Action);
        Custom5Btn.onClick.AddListener(Custom5Action);
        Custom6Btn.onClick.AddListener(Custom6Action);
        Custom7Btn.onClick.AddListener(Custom7Action);
        Custom8Btn.onClick.AddListener(Custom8Action);
        Custom9Btn.onClick.AddListener(Custom9Action);
        BackBtn.onClick.AddListener(BackAction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Custom1Action() {
        PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();
        Debug.Log("local config --- " + (config.NoDisturb ? "推送免打扰" : "正常推送"));
        Debug.Log("local config --- " + (config.Style == PushStyle.Simple ? "显示完整内容" : "您有一条新消息"));
        Debug.Log("local config --- 免打扰开始时间 " + config.NoDisturbStartHour);
        Debug.Log("local config --- 免打扰结束时间 " + config.NoDisturbEndHour);
    }

    void Custom2Action() {
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

    void Custom3Action() {
        List<string> list = SDKClient.Instance.PushManager.GetNoDisturbGroups();
        foreach (string s in list)
        {
            Debug.Log("disturb group ------- " + s);
        }
    }

    void Custom4Action() {
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
    void Custom5Action() { }
    void Custom6Action() { }

    void Custom7Action() {
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

    void Custom8Action() {
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

    void Custom9Action() {
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

    void BackAction() {
        SceneManager.LoadScene("Main");
    }
}
