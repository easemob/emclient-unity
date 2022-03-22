using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update

    private Text m_UsernameText;
    private Text m_PasswordText;
    private Button m_LoginBtn;
    private Button m_RegisterBtn;
    private Button m_LoginWithAgoraTokenBtn;
    private Button m_AutoLoginBtn;

    private void Awake()
    {

        Debug.Log("login script has load");

        m_UsernameText = transform.Find("Panel/Username/Text").GetComponent<Text>();
        m_PasswordText = transform.Find("Panel/Password/Text").GetComponent<Text>();
        m_LoginBtn = transform.Find("Panel/LoginBtn").GetComponent<Button>();
        m_RegisterBtn = transform.Find("Panel/RegisterBtn").GetComponent<Button>();
        m_LoginWithAgoraTokenBtn = transform.Find("Panel/LoginWithAgoraTokenBtn").GetComponent<Button>();
        m_AutoLoginBtn = transform.Find("Panel/m_AutoLoginBtn").GetComponent<Button>();

        m_LoginBtn.onClick.AddListener(LoginAction);
        m_RegisterBtn.onClick.AddListener(RegisterAction);
        m_LoginWithAgoraTokenBtn.onClick.AddListener(LoginWithAgoraTokenAction);
        m_AutoLoginBtn.onClick.AddListener(AutoLoginBtnAction);

        InitEaseMobSDK();
       
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoginAction() {
        SDKClient.Instance.Login(m_UsernameText.text, m_PasswordText.text,
            handle: new CallBack(

                onSuccess: () =>
                {
                    Debug.Log("login succeed");
                    SceneManager.LoadSceneAsync("Main");
                },

                onError: (code, desc) =>
                {
                    if (code == 200)
                    {
                        SceneManager.LoadSceneAsync("Main");
                    }
                    else {
                        UIManager.DefaultAlert(transform, "login failed, code: " + code);
                    }
                    //UIManager.DefaultAlert(transform, "login failed, code: " + code);
                }
            )
        );
    }

    void RegisterAction() {
        SDKClient.Instance.CreateAccount(m_UsernameText.text, m_PasswordText.text,
            handle: new CallBack(

                onSuccess: () => {
                    Debug.Log("login succeed");
                    UIManager.SuccessAlert(transform);
                },

                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            )
        );
    }

    void LoginWithAgoraTokenAction()
    {
        //Read from file
        FileOperator foper = FileOperator.GetInstance();
        List<string> tokens = foper.ReadData(); // should be only one element
        if(tokens.Count == 0)
        {
            UIManager.DefaultAlert(transform, "Empty agora token!");
            return;
        }

        SDKClient.Instance.LoginWithAgoraToken(m_UsernameText.text, tokens[0],
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
    }

    void InitEaseMobSDK() {

        //Options options = new Options("81446724#514456");
        Options options = new Options("easemob-demo#easeim");
        options.AutoLogin = false;
        options.UsingHttpsOnly = true;
        options.DebugMode = true;
        SDKClient.Instance.InitWithOptions(options);

        if (SDKClient.Instance.IsLoggedIn && SDKClient.Instance.Options.AutoLogin) {
            SceneManager.LoadSceneAsync("Main");
        } 
    }

}
