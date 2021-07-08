using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

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
        Options options = new Options("easemob-demo#easeim");
        options.UsingHttpsOnly = true;
        SDKClient client = SDKClient.Instance;
        client.InitWithOptions(options);

        if (SDKClient.Instance.IsLoggedIn) {
            SceneManager.LoadScene("Main");
        }
    }

    void LoginAction() {
        string username = usernameField.text;
        string password = passwordField.text;
        print("登录被点击: " + username + ", " + password);
        //set callback handler
        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("Login succeeds!");
                SceneManager.LoadScene("Main");
            },
            onError: (code, desc)=> {
                Debug.LogError($"Login error: code={code},description={desc}");
            });
        SDKClient.Instance.Login(username, password, false, callback);
    }

    void RegisterAction()
    {
        //print("注册被点击: " + usernameField.text + ", " + passwordField.text);
        //SDKClient.Instance.CreateAccount(usernameField.text, passwordField.text, new CallBack(
        //    onSuccess:()=> {
        //        print("注册成功");
        //    },
        //    onError:(code, desc) => {
        //        print("注册失败-- desc: " + desc);
        //    }
        //));
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quit and release resources...");
        SDKClient.Instance.Logout(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
