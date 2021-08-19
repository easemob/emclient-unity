using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ChatSDK;

public class RoomManagerTest : MonoBehaviour, IRoomManagerDelegate
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


        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
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

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            SDKClient.Instance.RoomManager.ChangeRoomName(currentRoomId, dict["Name"], new CallBack(
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
        config.AddField("Name");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("ChangeRoomSubjectBtnAction");
    }
    void CreateRoomBtnAction()
    {
        UIManager.UnfinishedAlert(transform);

        Debug.Log("CreateRoomBtnAction");
    }
    void DestroyRoomBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("DestroyRoomBtnAction");
    }
    void FetchPublicRoomsFromServerBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle: new ValueCallBack<PageResult<Room>>(
            onSuccess: (result) => {
                List<string> list = new List<string>();

                foreach (var room in result.Data) {
                    list.Add(room.Name);
                }

                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);

            },
            onError:(code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchPublicRoomsFromServerBtnAction");
    }
    void FetchRoomAnnouncementBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchRoomAnnouncement(currentRoomId, new ValueCallBack<string>(
            onSuccess: (str) => {
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomAnnouncementBtnAction");
    }

    void FetchRoomBlockListBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchRoomBlockList(currentRoomId, handle: new ValueCallBack<List<string>>(
            onSuccess: (list) => {
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomBlockListBtnAction");
    }
    void FetchRoomInfoFromServerBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchRoomInfoFromServer(currentRoomId, new ValueCallBack<Room>(
            onSuccess: (room) => {
                List<string> list = new List<string>();
                list.Add(room.Name);
                list.Add(room.Description);
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomInfoFromServerBtnAction");
    }
    void FetchRoomMembersBtnAction()
    {
        SDKClient.Instance.RoomManager.FetchRoomMembers(currentRoomId, handle: new ValueCallBack<CursorResult<string>>(
            onSuccess: (result) => {
                string str = string.Join(",", result.Data.ToArray());
                UIManager.DefaultAlert(transform, str);
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomMembersBtnAction");
    }
    void FetchRoomMuteListBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchRoomMuteList(currentRoomId, handle: new ValueCallBack<List<string>>(
            onSuccess: (result) => {
                string str = string.Join(",", result.ToArray());
                UIManager.DefaultAlert(transform, str);
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomMuteListBtnAction");
    }
    void GetAllRoomsFromLocalBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("GetAllRoomsFromLocalBtnAction");
    }
    void JoinRoomBtnAction()
    {
        SDKClient.Instance.RoomManager.JoinRoom(currentRoomId, new ValueCallBack<Room>(
            onSuccess: (room) => {
                UIManager.DefaultAlert(transform, "加入成功");
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
        Debug.Log("JoinRoomBtnAction");
    }
    void LeaveRoomBtnAction()
    {
        SDKClient.Instance.RoomManager.LeaveRoom(currentRoomId, new CallBack(
            onSuccess: () => {
                UIManager.DefaultAlert(transform, "离开成功");
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("LeaveRoomBtnAction");
    }
    void MuteRoomMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.MuteRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("MuteRoomMembersBtnAction");
    }
    void RemoveRoomAdminBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string adminId = dict["AdminId"];
            SDKClient.Instance.RoomManager.RemoveRoomAdmin(currentRoomId, adminId, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("AdminId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveRoomAdminBtnAction");
    }
    void RemoveRoomMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.RemoveRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveRoomMembersBtnAction");
    }
    void UnBlockRoomMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.UnBlockRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UnBlockRoomMembersBtnAction");
    }
    void UnMuteRoomMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.UnMuteRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UnMuteRoomMembersBtnAction");
    }
    void UpdateRoomAnnouncementBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string announcement = dict["announcement"];
            SDKClient.Instance.RoomManager.UpdateRoomAnnouncement(currentRoomId, announcement, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });

        config.AddField("announcement");

        UIManager.DefaultInputAlert(transform, config);

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

    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        UIManager.DefaultAlert(transform, $"OnDestroyedFromRoom: {roomId} , {roomName}");
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        UIManager.DefaultAlert(transform, $"OnMemberJoinedFromRoom: {roomId} , {participant}");
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        UIManager.DefaultAlert(transform, $"OnMemberExitedFromRoom: {roomId} , {roomName}, {participant}");
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {
        UIManager.DefaultAlert(transform, $"OnRemovedFromRoom: {roomId} , {roomName}, {participant}");
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {

        string str = string.Join(",", mutes.ToArray());
        
        UIManager.DefaultAlert(transform, $"OnMuteListAddedFromRoom: {roomId} , {str}");
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        string str = string.Join(",", mutes.ToArray());
        
        UIManager.DefaultAlert(transform, $"OnMuteListRemovedFromRoom: {roomId} , {str}");
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(transform, $"OnAdminAddedFromRoom: {roomId} , {admin}");
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(transform, $"OnAdminRemovedFromRoom: {roomId} , {admin}");
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        UIManager.DefaultAlert(transform, $"OnOwnerChangedFromRoom: {roomId} , {newOwner}, {oldOwner}");
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        UIManager.DefaultAlert(transform, $"OnAnnouncementChangedFromRoom: {roomId} , {announcement}");
    }
}
