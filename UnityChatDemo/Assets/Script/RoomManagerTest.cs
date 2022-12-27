using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

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
    private Button AddAttributeBtn;
    private Button RemoveAttributeBtn;
    private Button FetchAttributeBtn;

    private string currentRoomId
    {
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
        AddAttributeBtn = transform.Find("Scroll View/Viewport/Content/AddAttributeBtn").GetComponent<Button>();
        RemoveAttributeBtn = transform.Find("Scroll View/Viewport/Content/RemoveAttributeBtn").GetComponent<Button>();
        FetchAttributeBtn = transform.Find("Scroll View/Viewport/Content/FetchAttributeBtn").GetComponent<Button>();



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
        AddAttributeBtn.onClick.AddListener(AddAttributeBtnAction);
        RemoveAttributeBtn.onClick.AddListener(RemoveAttributeBtnAction);
        FetchAttributeBtn.onClick.AddListener(FetchAttributeBtnAction);

    }


    private void OnDestroy()
    {

    }

    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    void AddRoomAdminBtnAction()
    {
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
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        SDKClient.Instance.RoomManager.DestroyRoom(currentRoomId, new CallBack(
            onSuccess: () =>
            {
                UIManager.DefaultAlert(transform, "解散成功");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"解散失败: {code}: {desc}");
            }
        ));
    }
    void FetchPublicRoomsFromServerBtnAction()
    {

        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(callback: new ValueCallBack<PageResult<Room>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();

                foreach (var room in result.Data)
                {
                    list.Add(room.Name);
                }

                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);

            },
            onError: (code, desc) =>
            {
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
            onSuccess: (str) =>
            {
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) =>
            {
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
        SDKClient.Instance.RoomManager.FetchRoomBlockList(currentRoomId, callback: new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) =>
            {
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
            onSuccess: (room) =>
            {
                List<string> list = new List<string>();
                list.Add(room.Name);
                list.Add(room.Description);
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) =>
            {
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
        SDKClient.Instance.RoomManager.FetchRoomMembers(currentRoomId, callback: new ValueCallBack<CursorResult<string>>(
            onSuccess: (result) =>
            {
                string str = string.Join(",", result.Data.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) =>
            {
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
        SDKClient.Instance.RoomManager.FetchRoomMuteList(currentRoomId, callback: new ValueCallBack<Dictionary<string, long>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();
                foreach (string key in result.Keys)
                {
                    list.Add(key);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(this.transform, str);
            },
            onError: (code, desc) =>
            {
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
            onSuccess: (room) =>
            {
                UIManager.DefaultAlert(this.transform, "加入成功");
            },
            onError: (code, desc) =>
            {
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
            onSuccess: () =>
            {
                UIManager.DefaultAlert(this.transform, "离开成功");
            },
            onError: (code, desc) =>
            {
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
            SDKClient.Instance.RoomManager.MuteRoomMembers(currentRoomId, list, -1, new CallBack(
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

        config.AddField("announcement");

        UIManager.DefaultInputAlert(this.transform, config);

        Debug.Log("UpdateRoomAnnouncementBtnAction");
    }


    void AddAttributeBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        Dictionary<string, string> kv = new Dictionary<string, string>();
        kv.Add("key", "value");
        SDKClient.Instance.RoomManager.AddAttributes(currentRoomId, kv, forced: true, callback: new ValueCallBack<Dictionary<string, int>>(
            onSuccess: (result) =>
            {
                UIManager.DefaultAlert(transform, "添加attr成功");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"添加attr失败 {code}:{desc}");
            }
        ));
    }

    void RemoveAttributeBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        List<string> keys = new List<string>();
        keys.Add("key1");
        keys.Add("key2");
        SDKClient.Instance.RoomManager.RemoveAttributes(currentRoomId, keys, forced: true, callback: new ValueCallBack<Dictionary<string, int>>(
            onSuccess: (result) =>
            {
                UIManager.DefaultAlert(transform, "移除attr成功");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"移除attr失败 {code}:{desc}");
            }
        ));
    }

    void FetchAttributeBtnAction()
    {
        if (null == currentRoomId || 0 == currentRoomId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        List<string> keys = new List<string>();
        keys.Add("key1");
        keys.Add("key2");

        SDKClient.Instance.RoomManager.FetchAttributes(currentRoomId, keys, new ValueCallBack<Dictionary<string, string>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();
                foreach (string key in result.Keys)
                {
                    list.Add(key);
                    list.Add(result[key]);
                }

                UIManager.DefaultAlert(transform, $"{string.Join(",", list.ToArray())}");
            },
            onError: (code, desc) =>
            {
                UIManager.DefaultAlert(transform, $"获取失败 {code}:{desc}");
            }
        ));
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
