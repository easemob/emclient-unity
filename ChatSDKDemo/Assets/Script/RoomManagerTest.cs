using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

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


    private void OnDestroy()
    {
        SDKClient.Instance.RoomManager.RemoveRoomManagerDelegate(this);
    }

    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void AddRoomAdminBtnAction()
    {
        string roomId = "186601527377921";

        Dictionary<string, string> kv = new Dictionary<string, string>();
        kv["key1"] = "val1";
        kv["哈哈"] = "一个值2";

        bool auto_delete = true;
        bool forced = true;

        SDKClient.Instance.RoomManager.AddAttributes(roomId, kv, auto_delete, forced, new CallBackResult(
            onSuccessResult: (Dictionary<string, int> dict) => {
                if (dict.Count == 0)
                    Debug.Log($"AddAttributes success.");
                else
                {
                    Debug.Log($"AddAttributes partial sucess.");
                    string str = TransformTool.JsonStringFromDictionaryStringAndInt(dict);
                    Debug.Log($"failed keys are:{str}.");
                }
            },
            onError: (code, desc) => {
                Debug.Log($"AddAttributes failed, code:{code}, desc:{desc}");
            }
        ));
        return;

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string id = dict["adminId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.RoomManager.AddRoomAdmin(currentRoomId, dict["adminId"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });
        config.AddField("adminId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("AddRoomAdminBtnAction");
    }
    void BlockRoomMembersBtnAction()
    {
        string roomId = "186601527377921";

        List<string> keys = new List<string>();
        keys.Add("key1");
        keys.Add("key2");

        SDKClient.Instance.RoomManager.FetchAttributes(roomId, keys, new ValueCallBack<Dictionary<string, string>>(
            onSuccess: (Dictionary<string, string> dict) => {
                Debug.Log($"FetchAttributes sucess.");
                string str = TransformTool.JsonStringFromDictionary(dict);
                Debug.Log($"fetch contents are:{str}.");
            },
            onError: (code, desc) => {
                Debug.Log($"FetchAttributes failed, code:{code}, desc:{desc}");
            }
        ));

        return;

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string id = dict["memberId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.RoomManager.BlockRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });
        config.AddField("memberId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("BlockRoomMembersBtnAction");
    }
    void ChangeOwnerBtnAction()
    {
        string roomId = "186601527377921";

        List<string> keys = new List<string>();
        keys.Add("key1");
        keys.Add("key2");
        bool forced = true;

        SDKClient.Instance.RoomManager.RemoveAttributes(roomId, keys, forced, new CallBackResult(
            onSuccessResult: (Dictionary<string, int> dict) => {
                if (dict.Count == 0)
                    Debug.Log($"RemoveAttributes success.");
                else
                {
                    Debug.Log($"RemoveAttributes partial sucess.");
                    string str = TransformTool.JsonStringFromDictionaryStringAndInt(dict);
                    Debug.Log($"failed keys are:{str}.");
                }
            },
            onError: (code, desc) => {
                Debug.Log($"RemoveAttributes failed, code:{code}, desc:{desc}");
            }
        ));
        return;

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string nw = dict["newOwner"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == nw || 0 == nw.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.RoomManager.ChangeRoomOwner(currentRoomId, dict["newOwner"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("newOwner");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("ChangeOwnerBtnAction");
    }
    void ChangeRoomDescriptionBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (null == currentRoomId || 0 == currentRoomId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.RoomManager.ChangeRoomDescription(currentRoomId, dict["Description"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });
        config.AddField("Description");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("ChangeRoomDescriptionBtnAction");
    }

    void ChangeRoomSubjectBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string subName = dict["Name"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == subName || 0 == subName.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.RoomManager.ChangeRoomName(currentRoomId, dict["Name"], new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });
        config.AddField("Name");

        UIManager.DefaultInputAlert(this.transform, config);

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
                UIManager.DefaultAlert(this.transform, str);

            },
            onError:(code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("FetchPublicRoomsFromServerBtnAction");
    }
    void FetchRoomAnnouncementBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.FetchRoomAnnouncement(currentRoomId, new ValueCallBack<string>(
            onSuccess: (str) => {
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomAnnouncementBtnAction");
    }

    void FetchRoomBlockListBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.FetchRoomBlockList(currentRoomId, handle: new ValueCallBack<List<string>>(
            onSuccess: (list) => {
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomBlockListBtnAction");
    }
    void FetchRoomInfoFromServerBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.FetchRoomInfoFromServer(currentRoomId, new ValueCallBack<Room>(
            onSuccess: (room) => {
                List<string> list = new List<string>();
                list.Add(room.Name);
                list.Add(room.Description);
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomInfoFromServerBtnAction");
    }
    void FetchRoomMembersBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.FetchRoomMembers(currentRoomId, handle: new ValueCallBack<CursorResult<string>>(
            onSuccess: (result) => {
                string str = string.Join(",", result.Data.ToArray());
                UIManager.DefaultAlert(this.transform, str);
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("FetchRoomMembersBtnAction");
    }
    void FetchRoomMuteListBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.FetchRoomMuteList(currentRoomId, handle: new ValueCallBack<List<string>>(
            onSuccess: (result) => {
                string str = string.Join(",", result.ToArray());
                UIManager.DefaultAlert(this.transform, str);
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
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
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.JoinRoom(currentRoomId, new ValueCallBack<Room>(
            onSuccess: (room) => {
                UIManager.DefaultAlert(this.transform, "加入成功");
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));
        Debug.Log("JoinRoomBtnAction");
    }
    void LeaveRoomBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.RoomManager.LeaveRoom(currentRoomId, new CallBack(
            onSuccess: () => {
                UIManager.DefaultAlert(this.transform, "离开成功");
            },
            onError: (code, desc) => {
                UIManager.ErrorAlert(this.transform, code, desc);
            }
        ));

        Debug.Log("LeaveRoomBtnAction");
    }
    void MuteRoomMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.MuteRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("MuteRoomMembersBtnAction");
    }
    void RemoveRoomAdminBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string adminId = dict["AdminId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == adminId || 0 == adminId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.RoomManager.RemoveRoomAdmin(currentRoomId, adminId, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("AdminId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("RemoveRoomAdminBtnAction");
    }
    void RemoveRoomMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.DeleteRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("RemoveRoomMembersBtnAction");
    }
    void UnBlockRoomMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.UnBlockRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("UnBlockRoomMembersBtnAction");
    }
    void UnMuteRoomMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["MemberId"];
            if (null == currentRoomId || 0 == currentRoomId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.RoomManager.UnMuteRoomMembers(currentRoomId, list, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("UnMuteRoomMembersBtnAction");
    }
    void UpdateRoomAnnouncementBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (null == currentRoomId || 0 == currentRoomId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            string announcement = dict["announcement"];
            SDKClient.Instance.RoomManager.UpdateRoomAnnouncement(currentRoomId, announcement, new CallBack(
                onSuccess: () => {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) => {
                    UIManager.ErrorAlert(this.transform, code, desc);
                }
            ));
        });

        config.AddField("announcement");

        UIManager.DefaultInputAlert(this.transform, config);

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
        UIManager.DefaultAlert(this.transform, $"回调 OnDestroyedFromRoom: {roomId} , {roomName}");
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnMemberJoinedFromRoom: {roomId} , {participant}");
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnMemberExitedFromRoom: {roomId} , {roomName}, {participant}");
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

        Debug.Log($"roomId: {roomId}, name:{roomName}, participant:{participant}, transfrom: {this.transform}");

        UIManager.DefaultAlert(this.transform, $"回调 OnRemovedFromRoom: {roomId} , {roomName ?? ""}, {participant}");
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {

        string str = string.Join(",", mutes.ToArray());
        
        UIManager.DefaultAlert(this.transform, $"回调 OnMuteListAddedFromRoom: {roomId} , {str}");
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        string str = string.Join(",", mutes.ToArray());
        
        UIManager.DefaultAlert(this.transform, $"回调 OnMuteListRemovedFromRoom: {roomId} , {str}");
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAdminAddedFromRoom: {roomId} , {admin}");
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAdminRemovedFromRoom: {roomId} , {admin}");
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnOwnerChangedFromRoom: {roomId} , {newOwner}, {oldOwner}");
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        UIManager.DefaultAlert(this.transform, $"回调 OnAnnouncementChangedFromRoom: {roomId} , {announcement}");
    }

    void IRoomManagerDelegate.OnChatroomAttributesChanged(string roomId, Dictionary<string, string> kv, string from)
    {
        string k_str = TransformTool.JsonStringFromDictionary(kv);
        UIManager.DefaultAlert(this.transform, $"回调 OnChatroomAttributesChanged: {roomId} , {k_str}");
    }

    void IRoomManagerDelegate.OnChatroomAttributesRemoved(string roomId, List<string> keys, string from)
    {
        string kv_str = string.Join(",", keys.ToArray());
        UIManager.DefaultAlert(this.transform, $"回调 OnChatroomAttributesRemoved: {roomId} , {keys.Count}");
    }
}
