using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class ContactManagerTest : MonoBehaviour, IContactManagerDelegate
{
    private Button addContactBtn;
    private Button deleteContactBtn;
    private Button getAllContactsFromServerBtn;
    private Button getAllContactsFromDBBtn;
    private Button addUserToBlockListBtn;
    private Button removeUserFromBlockListBtn;
    private Button getBlockListFromServerBtn;
    private Button acceptInvitationBtn;
    private Button declineInvitationBtn;
    private Button getSelfIdsOnOtherPlatformBtn;

    private Button backButton;


    private void Awake()
    {
        Debug.Log("contact manager test script has load");

        addContactBtn = transform.Find("Scroll View/Viewport/Content/AddContactBtn").GetComponent<Button>();
        deleteContactBtn = transform.Find("Scroll View/Viewport/Content/DeleteContactBtn").GetComponent<Button>();
        getAllContactsFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetAllContactsFromServerBtn").GetComponent<Button>();
        getAllContactsFromDBBtn = transform.Find("Scroll View/Viewport/Content/GetAllContactsFromDBBtn").GetComponent<Button>();
        addUserToBlockListBtn = transform.Find("Scroll View/Viewport/Content/AddUserToBlockListBtn").GetComponent<Button>();
        removeUserFromBlockListBtn = transform.Find("Scroll View/Viewport/Content/RemoveUserFromBlockListBtn").GetComponent<Button>();
        getBlockListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetBlockListFromServerBtn").GetComponent<Button>();
        acceptInvitationBtn = transform.Find("Scroll View/Viewport/Content/AcceptInvitationBtn").GetComponent<Button>();
        declineInvitationBtn = transform.Find("Scroll View/Viewport/Content/DeclineInvitationBtn").GetComponent<Button>();
        getSelfIdsOnOtherPlatformBtn = transform.Find("Scroll View/Viewport/Content/GetSelfIdsOnOtherPlatformBtn").GetComponent<Button>();
        addContactBtn.onClick.AddListener(AddContactBtnAction);
        deleteContactBtn.onClick.AddListener(DeleteContactBtnAction);
        getAllContactsFromServerBtn.onClick.AddListener(GetAllContactsFromServerBtnAction);
        getAllContactsFromDBBtn.onClick.AddListener(GetAllContactsFromDBBtnAction);
        addUserToBlockListBtn.onClick.AddListener(AddUserToBlockListBtnAction);
        removeUserFromBlockListBtn.onClick.AddListener(RemoveUserFromBlockListBtnAction);
        getBlockListFromServerBtn.onClick.AddListener(GetBlockListFromServerBtnAction);
        acceptInvitationBtn.onClick.AddListener(AcceptInvitationBtnAction);
        declineInvitationBtn.onClick.AddListener(DeclineInvitationBtnAction);
        getSelfIdsOnOtherPlatformBtn.onClick.AddListener(GetSelfIdsOnOtherPlatformBtnAction);

        backButton = transform.Find("BackBtn").GetComponent<Button>();
        backButton.onClick.AddListener(backButtonAction);

        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void AddContactBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("添加好友", (dict) =>
        {
            SDKClient.Instance.ContactManager.AddContact(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);


        Debug.Log("AddContactBtnAction");
    }
    void DeleteContactBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("删除好友", (dict) =>
        {
            SDKClient.Instance.ContactManager.DeleteContact(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("DeleteContactBtnAction");
    }
    void GetAllContactsFromServerBtnAction()
    {

        SDKClient.Instance.ContactManager.GetAllContactsFromServer(new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"失败code:{code}");
            }
        ));

        Debug.Log("GetAllContactsFromServerBtnAction");
    }

    void GetAllContactsFromDBBtnAction()
    {
        List<string>list = SDKClient.Instance.ContactManager.GetAllContactsFromDB();
        string str = string.Join(",", list.ToArray());
        UIManager.DefaultAlert(transform, str);
        Debug.Log("GetAllContactsFromDBBtnAction");
    }

    void AddUserToBlockListBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("添加黑名单", (dict) =>
        {
            SDKClient.Instance.ContactManager.AddUserToBlockList(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("AddUserToBlockListBtnAction");
    }
    void RemoveUserFromBlockListBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig("移除黑名单", (dict) =>
        {
            SDKClient.Instance.ContactManager.RemoveUserFromBlockList(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveUserFromBlockListBtnAction");
    }
    void GetBlockListFromServerBtnAction()
    {
        SDKClient.Instance.ContactManager.GetBlockListFromServer(new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"失败code:{code}");
            }
        ));

        Debug.Log("GetBlockListFromServerBtnAction");
    }
    void AcceptInvitationBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("同意好友申请", (dict) =>
        {
            SDKClient.Instance.ContactManager.AcceptInvitation(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("AcceptInvitationBtnAction");
    }
    void DeclineInvitationBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig("拒绝好友申请", (dict) =>
        {
            SDKClient.Instance.ContactManager.DeclineInvitation(dict["id"], handle: new CallBack(
                onSuccess: () =>
                {
                    UIManager.DefaultAlert(transform, "成功");
                },
                onError: (code, desc) =>
                {
                    UIManager.DefaultAlert(transform, $"失败code:{code}");
                }
            ));
        });

        config.AddField("id");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("DeclineInvitationBtnAction");
    }
    void GetSelfIdsOnOtherPlatformBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnContactAdded(string username)
    {
        UIManager.DefaultAlert(transform, $"OnContactAdded: {username}");
    }

    public void OnContactDeleted(string username)
    {
        UIManager.DefaultAlert(transform, $"OnContactDeleted: {username}");
    }

    public void OnContactInvited(string username, string reason)
    {
        CallBack callBack = new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        );

        UIManager.TitleAlert(transform, $"收到好友申请", $"{username}添加您为好友",
            () => { SDKClient.Instance.ContactManager.AcceptInvitation(username, callBack); },
            () => { SDKClient.Instance.ContactManager.DeclineInvitation(username, callBack); },
            "同意",
            "拒绝"
        );
    }

    public void OnFriendRequestAccepted(string username)
    {
        UIManager.DefaultAlert(transform, $"OnFriendRequestAccepted: {username}");
    }

    public void OnFriendRequestDeclined(string username)
    {
        UIManager.DefaultAlert(transform, $"OnFriendRequestDeclined: {username}");
    }
}
