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
    private Text m_PasswordText;
    private Button m_LoginBtn;
    private Button m_RegisterBtn;

    private void Awake()
    {

        Debug.Log("login script has load");

        m_UsernameText = transform.Find("Panel/Username/Text").GetComponent<Text>();
        m_PasswordText = transform.Find("Panel/Password/Text").GetComponent<Text>();
        m_LoginBtn = transform.Find("Panel/LoginBtn").GetComponent<Button>();
        m_RegisterBtn = transform.Find("Panel/RegisterBtn").GetComponent<Button>();


        m_LoginBtn.onClick.AddListener(LoginAction);
        m_RegisterBtn.onClick.AddListener(RegisterAction);



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
        InitEaseMobSDK();
        //LoginWithAgoraTokenAction();
        return;
    }

    void RegisterAction()
    {
        LoginWithAgoraTokenAction();
        //SDKClient.Instance.CreateAccount(m_UsernameText.text, m_PasswordText.text,
        //    handle: new CallBack(

        //        onSuccess: () =>
        //        {
        //            Debug.Log("login succeed");
        //            UIManager.SuccessAlert(transform);
        //        },

        //        onError: (code, desc) =>
        //        {
        //            UIManager.ErrorAlert(transform, code, desc);
        //        }
        //    )
        //);
    }

    void LoginWithAgoraTokenAction()
    {
        string token = "12345";

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

        SDKClient.Instance.LoginWithAgoraToken(m_UsernameText.text, token,
            handle: new CallBack(

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
                        UIManager.DefaultAlert(transform, "login failed, code: " + code);
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
        string appkey = "easemob-demo#unity";
        //string appkey = "easemob-demo#wang";
        //string appkey = "easemob-demo#unitytest";
        //string appkey = "41117440#383391";

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
        SDKClient.Instance.InitWithOptions(options);

        //if (SDKClient.Instance.IsLoggedIn && SDKClient.Instance.Options.AutoLogin)
        //{
        //    SceneManager.LoadSceneAsync("Main");
        //}
    }

}
