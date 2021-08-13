using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContactManagerTest : MonoBehaviour
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
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void AddContactBtnAction()
    {
        Debug.Log("AddContactBtnAction");
    }
    void DeleteContactBtnAction()
    {
        Debug.Log("DeleteContactBtnAction");
    }
    void GetAllContactsFromServerBtnAction()
    {
        Debug.Log("GetAllContactsFromServerBtnAction");
    }
    void GetAllContactsFromDBBtnAction()
    {
        Debug.Log("GetAllContactsFromDBBtnAction");
    }
    void AddUserToBlockListBtnAction()
    {
        Debug.Log("AddUserToBlockListBtnAction");
    }
    void RemoveUserFromBlockListBtnAction()
    {
        Debug.Log("RemoveUserFromBlockListBtnAction");
    }
    void GetBlockListFromServerBtnAction()
    {
        Debug.Log("GetBlockListFromServerBtnAction");
    }
    void AcceptInvitationBtnAction()
    {
        Debug.Log("AcceptInvitationBtnAction");
    }
    void DeclineInvitationBtnAction()
    {
        Debug.Log("DeclineInvitationBtnAction");
    }
    void GetSelfIdsOnOtherPlatformBtnAction()
    {
        Debug.Log("GetSelfIdsOnOtherPlatformBtnAction");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
