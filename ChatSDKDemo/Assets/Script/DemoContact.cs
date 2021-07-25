using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChatSDK;
using UnityEngine.SceneManagement;

public class DemoContact : MonoBehaviour, IContactManagerDelegate
{

    public Text InputText;
    public Button BackBtn;
    public Button AddContactBtn;
    public Button RemoveContactBtn;
    public Button AddBlockBtn;
    public Button RemoveBlockBtn;
    public Button AcceptInvitationBtn;
    public Button DeclineInvitationBtn;
    public Button AllServerContactsBtn;
    public Button AllServerBlocksBtn;
    public Button AllLocalContactsBtn;
    public Button AllLocalBlocksBtn;


    void Start()
    {
        BackBtn.onClick.AddListener(BackAction);
        AddContactBtn.onClick.AddListener(AddContactAction);
        RemoveContactBtn.onClick.AddListener(RemoveContactAction);
        AddBlockBtn.onClick.AddListener(AddBlockAction);
        RemoveBlockBtn.onClick.AddListener(RemoveBlockAction);
        AcceptInvitationBtn.onClick.AddListener(AcceptInvitationAction);
        DeclineInvitationBtn.onClick.AddListener(DeclineInvitationAction);
        AllServerContactsBtn.onClick.AddListener(AllServerContactsAction);
        AllServerBlocksBtn.onClick.AddListener(AllServerBlocksAction);
        AllLocalContactsBtn.onClick.AddListener(AllLocalContactsAction);
        AllLocalBlocksBtn.onClick.AddListener(AllLocalBlocksAction);

        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackAction() {
        SceneManager.LoadScene("Main");
    }

    void AddContactAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code +  " " + desc);
        };

        SDKClient.Instance.ContactManager.AddContact(text, "", callBack);
    }

    void RemoveContactAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.DeleteContact(text, handle:callBack);
    }

    void AddBlockAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.AddUserToBlockList(text, handle: callBack);
    }

    void RemoveBlockAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.RemoveUserFromBlockList(text, handle: callBack);
    }

    void AcceptInvitationAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.AcceptInvitation(text, handle: callBack);
    }

    void DeclineInvitationAction()
    {
        string text = InputText.text;
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("操作成功");
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.DeclineInvitation(text, handle: callBack);
    }

    void AllServerContactsAction()
    {
        ValueCallBack<List<string>> callBack = new ValueCallBack<List<string>>();
        callBack.OnSuccessValue = (List<string> list) => {
            foreach (var s in list) {
                Debug.Log("操作成功 -- " + s);
            }
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.GetAllContactsFromServer(callBack);
    }

    void AllServerBlocksAction()
    {
        ValueCallBack<List<string>> callBack = new ValueCallBack<List<string>>();
        callBack.OnSuccessValue = (List<string> list) => {
            foreach (var s in list)
            {
                Debug.Log("操作成功 -- " + s);
            }
        };
        callBack.Error = (int code, string desc) => {
            Debug.Log("操作失败 " + code + " " + desc);
        };

        SDKClient.Instance.ContactManager.GetBlockListFromServer(callBack);
    }

    void AllLocalContactsAction()
    {
        List<string> list = SDKClient.Instance.ContactManager.GetAllContactsFromDB();
        foreach (var s in list)
        {
            Debug.Log("操作成功 -- " + s);

        }
    }
        void AllLocalBlocksAction()
    {
        
    }

    public void OnContactAdded(string username)
    {
        Debug.Log("OnContactAdded --- " + username);
    }

    public void OnContactDeleted(string username)
    {
        Debug.Log("OnContactDeleted --- " + username);
    }

    public void OnContactInvited(string username, string reason)
    {
        Debug.Log("OnContactInvited --- " + username);
    }

    public void OnFriendRequestAccepted(string username)
    {
        Debug.Log("OnFriendRequestAccepted --- " + username);
    }

    public void OnFriendRequestDeclined(string username)
    {
        Debug.Log("OnFriendRequestDeclined --- " + username);
    }
}
