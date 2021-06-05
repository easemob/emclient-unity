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

        //Options options = new Options("easemob-demo#chatdemoui");
        //SDKClient.Instance.InitWithOptions(options);

    }


    void LoginAction() {
        string username = usernameField.text;
        string password = passwordField.text;
        print("登录被点击: " + username + ", " + password);
        IClient client = IClient.Instance;
        Options options = new Options("easemobDemo");
        options.DNSURL = "easemob.com";
        options.IMServer = "easemob.com";
        options.IMPort = 6717;
        options.RestServer = "easemob.com";
        client.InitWithOptions(options);
        client.Login(username, password);
        SceneManager.LoadScene("Main");
    }

    void RegisterAction() {
        print("注册被点击: " + usernameField.text + ", " + passwordField.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
