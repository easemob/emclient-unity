using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update

    private Text m_UsernameText;
    //private Text m_PasswordText;
    private InputField m_PasswordText;
    private Button m_LoginBtn;
    private Button m_RegisterBtn;

    private void Awake()
    {

        Debug.Log("login script has load");

        m_UsernameText = transform.Find("Panel/Username/Text").GetComponent<Text>();
        //m_PasswordText = transform.Find("Panel/Password/Text").GetComponent<Text>();
        m_PasswordText = transform.Find("Panel/Password").GetComponent<InputField>();
        m_LoginBtn = transform.Find("Panel/LoginBtn").GetComponent<Button>();
        m_RegisterBtn = transform.Find("Panel/RegisterBtn").GetComponent<Button>();


        m_LoginBtn.onClick.AddListener(LoginAction);
        m_RegisterBtn.onClick.AddListener(RegisterAction);

        InitEaseMobSDK();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoginAction()
    {
        LoginWithPassword();
        //LoginWithAgoraTokenAction();
        Debug.Log("执行了登录");
        return;
    }

    void RegisterAction()
    {
        SDKClient.Instance.CreateAccount(m_UsernameText.text, m_PasswordText.text,
            callback: new CallBack(

                onSuccess: () =>
                {
                    Debug.Log("login succeed");
                    UIManager.SuccessAlert(transform);
                },

                onError: (code, desc) =>
                {
                    Debug.Log($"failed desc {desc}");
                    UIManager.DefaultAlert(transform, "failed " + desc + " " + code);
                }
            )
        );
    }

    void LoginWithAgoraTokenAction()
    {

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE

        //Read agora token from file
        /*
        FileOperator foper = FileOperator.GetInstance();
        string tokenFromFile = foper.ReadData(foper.GetTokenConfFile()); // should be only one element

        if (tokenFromFile.Length == 0)
        {
            UIManager.DefaultAlert(transform, "Empty agora token!");
            return;
        }
        else
        {
            token = tokenFromFile;
        }
        */
#endif

        Debug.Log($"token is : {m_PasswordText.text}");

        SDKClient.Instance.LoginWithAgoraToken(m_UsernameText.text, m_PasswordText.text,
            callback: new CallBack(

                onSuccess: () =>
                {
                    Debug.Log("login with agora token succeed");
                    SceneManager.LoadSceneAsync("Main");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        SceneManager.LoadSceneAsync("Main");
                    }
                    else
                    {
                        UIManager.DefaultAlert(transform, "failed " + code + " " + desc);
                    }
                }
            )
        );
    }

    void LoginWithPassword()
    {
        SDKClient.Instance.Login(m_UsernameText.text, m_PasswordText.text,
            callback: new CallBack(

                onSuccess: () =>
                {
                    Debug.Log("login with agora token succeed");
                    SceneManager.LoadSceneAsync("Main");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        SceneManager.LoadSceneAsync("Main");
                    }
                    else
                    {
                        UIManager.DefaultAlert(transform, "failed " + code + " " + desc);
                    }
                }
            )
        );
    }


    void AutoLoginBtnAction()
    {
        /*
        SDKClient.Instance.AutoLogin(
            handle: new CallBack(

                onSuccess: () =>
                {
                    Debug.Log("Auto loginin");
                    SceneManager.LoadSceneAsync("Main");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        SceneManager.LoadSceneAsync("Main");
                    }
                    else
                    {
                        UIManager.DefaultAlert(transform, "AutoLogin failed, code: " + code);
                    }
                }
            )
        );
        */
    }

    void InitEaseMobSDK()
    {
        //default appkey
        // string appkey = "easemob-demo#flutter";
        // string appkey = "easemob-demo#wang";
        string appkey = "easemob-demo#unitytest";
        //string appkey = "41117440#383391";
        //string appkey = "easemob#easeim";

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE

        //Read appkey from file
        /*
        FileOperator foper = FileOperator.GetInstance();
        string appkeyFromFile = foper.ReadData(foper.GetAppkeyConfFile()); // should be only one element

        if (appkeyFromFile.Length != 0)
        {
            Debug.Log($"appkey from file: {appkeyFromFile}");
            appkey = appkeyFromFile;
        }
        */
#endif

        //Options options = new Options("81446724#514456");
        Options options = new Options(appkey);
        options.AutoLogin = false;
        options.UsingHttpsOnly = true;
        options.DebugMode = true;
        options.EnableEmptyConversation = true;
        options.RegardImportMsgAsRead = true;
        //options.UseReplacedMessageContents = true;
        SDKClient.Instance.InitWithOptions(options);

        //if (SDKClient.Instance.IsLoggedIn && SDKClient.Instance.Options.AutoLogin)
        //{
        //    SceneManager.LoadSceneAsync("Main");
        //}
    }

}
