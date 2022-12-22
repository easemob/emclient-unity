
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

public class Main : MonoBehaviour, IConnectionDelegate
{
    // Start is called before the first frame update

    private Button ChatBtn;
    private Button ContactBtn;
    private Button ConversationBtn;
    private Button GroupBtn;
    private Button RoomBtn;
    private Button PushBtn;
    private Button PresenceBtn;
    private Button ThreadBtn;
    private Button isConnectedBtn;
    private Button isLoggedBtn;
    private Button CurrentUsernameBtn;
    private Button AccessTokenBtn;
    private Button LogoutBtn;
    private Button m_NewTokenBtn;


    private void Awake()
    {

        Debug.Log("main script has load");

        ChatBtn = transform.Find("Panel/ChatBtn").GetComponent<Button>();
        ContactBtn = transform.Find("Panel/ContactBtn").GetComponent<Button>();
        ConversationBtn = transform.Find("Panel/ConversationBtn").GetComponent<Button>();
        GroupBtn = transform.Find("Panel/GroupBtn").GetComponent<Button>();
        RoomBtn = transform.Find("Panel/RoomBtn").GetComponent<Button>();
        PushBtn = transform.Find("Panel/PushBtn").GetComponent<Button>();
        PresenceBtn = transform.Find("Panel/PresenceBtn").GetComponent<Button>();
        ThreadBtn = transform.Find("Panel/ThreadBtn").GetComponent<Button>();
        isConnectedBtn = transform.Find("Panel/Panel/IsConnectBtn").GetComponent<Button>();
        isLoggedBtn = transform.Find("Panel/Panel/IsLoggedBtn").GetComponent<Button>();
        CurrentUsernameBtn = transform.Find("Panel/Panel/CurrentUsernameBtn").GetComponent<Button>();
        AccessTokenBtn = transform.Find("Panel/Panel/AccessTokenBtn").GetComponent<Button>();
        LogoutBtn = transform.Find("Panel/LogoutBtn").GetComponent<Button>();
        m_NewTokenBtn = transform.Find("Panel/NewTokenBtn").GetComponent<Button>();

        ChatBtn.onClick.AddListener(ChatBtnAction);
        ContactBtn.onClick.AddListener(ContactBtnAction);
        ConversationBtn.onClick.AddListener(ConversationBtnAction);
        GroupBtn.onClick.AddListener(GroupBtnAction);
        RoomBtn.onClick.AddListener(RoomBtnAction);
        PushBtn.onClick.AddListener(PushBtnAction);
        PresenceBtn.onClick.AddListener(PresenceBtnAction);
        ThreadBtn.onClick.AddListener(ThreadBtnAction);
        isConnectedBtn.onClick.AddListener(isConnectedBtnAction);
        isLoggedBtn.onClick.AddListener(isLoggedBtnAction);
        CurrentUsernameBtn.onClick.AddListener(CurrentUsernameBtnAction);
        AccessTokenBtn.onClick.AddListener(AccessTokenBtnAction);
        LogoutBtn.onClick.AddListener(LogoutBtnAction);
        m_NewTokenBtn.onClick.AddListener(NewTokenAction);

        SDKClient.Instance.AddConnectionDelegate(this);
    }

    private void OnDestroy()
    {
        SDKClient.Instance.DeleteConnectionDelegate(this);
    }

    void ChatBtnAction()
    {
        SceneManager.LoadSceneAsync("ChatManager");
    }

    void ContactBtnAction()
    {
        SceneManager.LoadSceneAsync("ContactManager");
    }

    void ConversationBtnAction()
    {
        SceneManager.LoadSceneAsync("ConversationManager");
    }

    void GroupBtnAction()
    {
        SceneManager.LoadSceneAsync("GroupManager");
    }

    void RoomBtnAction()
    {
        SceneManager.LoadSceneAsync("RoomManager");
    }

    void PushBtnAction()
    {
        SceneManager.LoadSceneAsync("PushManager");
    }

    void PresenceBtnAction()
    {
        SceneManager.LoadSceneAsync("PresenceManager");
    }
    void ThreadBtnAction()
    {
        SceneManager.LoadSceneAsync("ThreadManager");
    }
    void isConnectedBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.IsConnected ? "已连接" : "未连接");
    }

    void isLoggedBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.IsLoggedIn ? "已登录" : "未登录");
    }

    void CurrentUsernameBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.CurrentUsername);
    }

    void AccessTokenBtnAction()
    {
        UIManager.DefaultAlert(transform, SDKClient.Instance.AccessToken);
    }

    void LogoutBtnAction()
    {
        SDKClient.Instance.Logout(false);
        SceneManager.LoadSceneAsync("Login");
    }

    void NewTokenAction()
    {
        string token = "12345";

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE

        /*
        //Read token from file
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

        SDKClient.Instance.RenewAgoraToken(token);
        UIManager.DefaultAlert(transform, "Renew agora token complete.");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnConnected()
    {
        UIManager.DefaultAlert(transform, "OnConnected");
    }

    public void OnDisconnected(DisconnectReason reason)
    {
        UIManager.DefaultAlert(transform, $"OnDisconnected : {reason}");
        if (reason == DisconnectReason.Reason_LoginFromOtherDevice)
        {
            SceneManager.LoadSceneAsync("Login");
        }
    }


    public void OnTokenExpired()
    {
        UIManager.DefaultAlert(transform, $"OnTokenExpired");
    }

    public void OnTokenWillExpire()
    {
        UIManager.DefaultAlert(transform, $"OnTokenWillExpire");
    }
}
