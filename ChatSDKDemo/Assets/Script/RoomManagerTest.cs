using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class RoomManagerTest : MonoBehaviour
{

    private Text roomText;
    private Button backButton;

    private Button AddRoomAdminBtn;
    private Button BlockRoomMembersBtn;
    private Button ChangeOwnerBtn;
    private Button ChangeRoomDescriptionBtn;
    private Button ChangeRoomSubjectBtn;
    private Button CreateRoomBtn;
    private Button DestroyRoomBtn;
    private Button FetchPublicRoomsFromServerBtn;
    private Button FetchRoomAnnouncementBtn;
    private Button FetchRoomBlockListBtn;
    private Button FetchRoomInfoFromServerBtn;
    private Button FetchRoomMembersBtn;
    private Button FetchRoomMuteListBtn;
    private Button GetAllRoomsFromLocalBtn;
    private Button JoinRoomBtn;
    private Button LeaveRoomBtn;
    private Button MuteRoomMembersBtn;
    private Button RemoveRoomAdminBtn;
    private Button RemoveRoomMembersBtn;
    private Button UnBlockRoomMembersBtn;
    private Button UnMuteRoomMembersBtn;
    private Button UpdateRoomAnnouncementBtn;

    private string currentRoomId {
        get => roomText.text;
    }

    private void Awake()
    {
        Debug.Log("room manager test script has load");

        roomText = transform.Find("RoomText/Text").GetComponent<Text>();

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        AddRoomAdminBtn = transform.Find("Scroll View/Viewport/Content/AddRoomAdminBtn").GetComponent<Button>();
        BlockRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/BlockRoomMembersBtn").GetComponent<Button>();
        ChangeOwnerBtn = transform.Find("Scroll View/Viewport/Content/ChangeOwnerBtn").GetComponent<Button>();
        ChangeRoomDescriptionBtn = transform.Find("Scroll View/Viewport/Content/ChangeRoomDescriptionBtn").GetComponent<Button>();
        ChangeRoomSubjectBtn = transform.Find("Scroll View/Viewport/Content/ChangeRoomSubjectBtn").GetComponent<Button>();
        CreateRoomBtn = transform.Find("Scroll View/Viewport/Content/CreateRoomBtn").GetComponent<Button>();
        DestroyRoomBtn = transform.Find("Scroll View/Viewport/Content/DestroyRoomBtn").GetComponent<Button>();
        FetchPublicRoomsFromServerBtn = transform.Find("Scroll View/Viewport/Content/FetchPublicRoomsFromServerBtn").GetComponent<Button>();
        FetchRoomAnnouncementBtn = transform.Find("Scroll View/Viewport/Content/FetchRoomAnnouncementBtn").GetComponent<Button>();
        FetchRoomBlockListBtn = transform.Find("Scroll View/Viewport/Content/FetchRoomBlockListBtn").GetComponent<Button>();
        FetchRoomInfoFromServerBtn = transform.Find("Scroll View/Viewport/Content/FetchRoomInfoFromServerBtn").GetComponent<Button>();
        FetchRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/FetchRoomMembersBtn").GetComponent<Button>();
        FetchRoomMuteListBtn = transform.Find("Scroll View/Viewport/Content/FetchRoomMuteListBtn").GetComponent<Button>();
        GetAllRoomsFromLocalBtn = transform.Find("Scroll View/Viewport/Content/GetAllRoomsFromLocalBtn").GetComponent<Button>();
        JoinRoomBtn = transform.Find("Scroll View/Viewport/Content/JoinRoomBtn").GetComponent<Button>();
        LeaveRoomBtn = transform.Find("Scroll View/Viewport/Content/LeaveRoomBtn").GetComponent<Button>();
        MuteRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/MuteRoomMembersBtn").GetComponent<Button>();
        RemoveRoomAdminBtn = transform.Find("Scroll View/Viewport/Content/RemoveRoomAdminBtn").GetComponent<Button>();
        RemoveRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/RemoveRoomMembersBtn").GetComponent<Button>();
        UnBlockRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/UnBlockRoomMembersBtn").GetComponent<Button>();
        UnMuteRoomMembersBtn = transform.Find("Scroll View/Viewport/Content/UnMuteRoomMembersBtn").GetComponent<Button>();
        UpdateRoomAnnouncementBtn = transform.Find("Scroll View/Viewport/Content/UpdateRoomAnnouncementBtn").GetComponent<Button>();


        AddRoomAdminBtn.onClick.AddListener(AddRoomAdminBtnAction);
        BlockRoomMembersBtn.onClick.AddListener(BlockRoomMembersBtnAction);
        ChangeOwnerBtn.onClick.AddListener(ChangeOwnerBtnAction);
        ChangeRoomDescriptionBtn.onClick.AddListener(ChangeRoomDescriptionBtnAction);
        ChangeRoomSubjectBtn.onClick.AddListener(ChangeRoomSubjectBtnAction);
        CreateRoomBtn.onClick.AddListener(CreateRoomBtnAction);
        DestroyRoomBtn.onClick.AddListener(DestroyRoomBtnAction);
        FetchPublicRoomsFromServerBtn.onClick.AddListener(FetchPublicRoomsFromServerBtnAction);
        FetchRoomAnnouncementBtn.onClick.AddListener(FetchRoomAnnouncementBtnAction);
        FetchRoomBlockListBtn.onClick.AddListener(FetchRoomBlockListBtnAction);
        FetchRoomInfoFromServerBtn.onClick.AddListener(FetchRoomInfoFromServerBtnAction);
        FetchRoomMembersBtn.onClick.AddListener(FetchRoomMembersBtnAction);
        FetchRoomMuteListBtn.onClick.AddListener(FetchRoomMuteListBtnAction);
        GetAllRoomsFromLocalBtn.onClick.AddListener(GetAllRoomsFromLocalBtnAction);
        JoinRoomBtn.onClick.AddListener(JoinRoomBtnAction);
        LeaveRoomBtn.onClick.AddListener(LeaveRoomBtnAction);
        MuteRoomMembersBtn.onClick.AddListener(MuteRoomMembersBtnAction);
        RemoveRoomAdminBtn.onClick.AddListener(RemoveRoomAdminBtnAction);
        RemoveRoomMembersBtn.onClick.AddListener(RemoveRoomMembersBtnAction);
        UnBlockRoomMembersBtn.onClick.AddListener(UnBlockRoomMembersBtnAction);
        UnMuteRoomMembersBtn.onClick.AddListener(UnMuteRoomMembersBtnAction);
        UpdateRoomAnnouncementBtn.onClick.AddListener(UpdateRoomAnnouncementBtnAction);
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void AddRoomAdminBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.RoomManager.AddRoomAdmin(currentRoomId, dict["adminId"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("adminId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("AddRoomAdminBtnAction");
    }
    void BlockRoomMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.RoomManager.BlockRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("memberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("BlockRoomMembersBtnAction");
    }
    void ChangeOwnerBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.RoomManager.ChangeRoomOwner(currentRoomId, dict["newOwner"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("newOwner");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("ChangeOwnerBtnAction");
    }
    void ChangeRoomDescriptionBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.RoomManager.ChangeRoomDescription(currentRoomId, dict["Description"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("Description");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("ChangeRoomDescriptionBtnAction");
    }

    void ChangeRoomSubjectBtnAction()
    {
        Debug.Log("ChangeRoomSubjectBtnAction");
    }
    void CreateRoomBtnAction()
    {
        Debug.Log("CreateRoomBtnAction");
    }
    void DestroyRoomBtnAction()
    {
        Debug.Log("DestroyRoomBtnAction");
    }
    void FetchPublicRoomsFromServerBtnAction()
    {
        Debug.Log("FetchPublicRoomsFromServerBtnAction");
    }
    void FetchRoomAnnouncementBtnAction()
    {
        Debug.Log("FetchRoomAnnouncementBtnAction");
    }

    void FetchRoomBlockListBtnAction()
    {
        Debug.Log("FetchRoomBlockListBtnAction");
    }
    void FetchRoomInfoFromServerBtnAction()
    {
        Debug.Log("FetchRoomInfoFromServerBtnAction");
    }
    void FetchRoomMembersBtnAction()
    {
        Debug.Log("FetchRoomMembersBtnAction");
    }
    void FetchRoomMuteListBtnAction()
    {
        Debug.Log("FetchRoomMuteListBtnAction");
    }
    void GetAllRoomsFromLocalBtnAction()
    {
        Debug.Log("GetAllRoomsFromLocalBtnAction");
    }
    void JoinRoomBtnAction()
    {
        Debug.Log("JoinRoomBtnAction");
    }
    void LeaveRoomBtnAction()
    {
        Debug.Log("LeaveRoomBtnAction");
    }
    void MuteRoomMembersBtnAction()
    {
        Debug.Log("MuteRoomMembersBtnAction");
    }
    void RemoveRoomAdminBtnAction()
    {
        Debug.Log("RemoveRoomAdminBtnAction");
    }
    void RemoveRoomMembersBtnAction()
    {
        Debug.Log("RemoveRoomMembersBtnAction");
    }
    void UnBlockRoomMembersBtnAction()
    {
        Debug.Log("UnBlockRoomMembersBtnAction");
    }
    void UnMuteRoomMembersBtnAction()
    {
        Debug.Log("UnMuteRoomMembersBtnAction");
    }
    void UpdateRoomAnnouncementBtnAction()
    {
        Debug.Log("UpdateRoomAnnouncementBtnAction");
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
