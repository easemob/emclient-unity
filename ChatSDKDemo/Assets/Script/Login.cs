using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        print("登录被点击: " + usernameField.text + ", " + passwordField.text);
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
