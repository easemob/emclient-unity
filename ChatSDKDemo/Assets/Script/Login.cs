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


    private void Awake()
    {

        Debug.Log("login script has load");

        m_UsernameText = transform.Find("Panel/Username/Text").GetComponent<Text>();
        m_PasswordText = transform.Find("Panel/Password/Text").GetComponent<Text>();
        m_LoginBtn = transform.Find("Panel/LoginBtn").GetComponent<Button>();
        m_RegisterBtn = transform.Find("Panel/RegisterBtn").GetComponent<Button>();

        m_LoginBtn.onClick.AddListener(LoginAction);
        m_RegisterBtn.onClick.AddListener(RegisterAction);

        InitEaseMobSDK();
       
    }

    private void OnApplicationQuit()
    {
        SDKClient.Instance.Logout(false);
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
                    
                }
            )
        );
    }

    void RegisterAction() {
        SDKClient.Instance.CreateAccount(m_UsernameText.text, m_PasswordText.text,
            handle: new CallBack(

                onSuccess: () => {
                    Debug.Log("login succeed");
                    SceneManager.LoadSceneAsync("Main");
                },

                onError: (code, desc) => {
                    Debug.LogFormat("login failed, code: {0}, desc: {1}", code, desc);
                }
            )
        );
    }

    void InitEaseMobSDK() {

        Options options = new Options("easemob-demo#chatdemoui");
        options.DebugMode = true;
        SDKClient.Instance.InitWithOptions(options);
    }

}
