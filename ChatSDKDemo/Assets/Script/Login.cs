using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;
using System.Runtime.InteropServices;

public class Login : MonoBehaviour
{

    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;
    public Button registerButton;

    // Start is called before the first frame update
    void Start()
    {

        loginButton.onClick.AddListener(LoginAction);
        registerButton.onClick.AddListener(RegisterAction);
        Options options = new Options("easemob-demo#chatdemoui");
        options.DebugModel = true;
        SDKClient.Instance.InitWithOptions(options);
    }


    void LoginAction()
    {
        print("登录被点击: " + usernameField.text + ", " + passwordField.text);
        SDKClient.Instance.Login(usernameField.text, passwordField.text, false, new CallBack(
            onSuccess: () => {
                print("登录成功");
                SceneManager.LoadScene("Main");
            },
            onError:(code, desc) => {
                print("登录失败: " + desc);
            }
        ));
    }

    void RegisterAction()
    {
        print("注册被点击: " + usernameField.text + ", " + passwordField.text);
        SDKClient.Instance.CreateAccount(usernameField.text, passwordField.text, new CallBack(
            onSuccess:()=> {
                print("注册成功");
            },
            onError:(code, desc) => {
                print("注册失败-- desc: " + desc);
            }
        ));
    }

    // Update is called once per frame
    void Update()
    {

    }
}