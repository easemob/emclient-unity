using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AgoraChat;

public class GroupManagerTest : MonoBehaviour
{
    private Text groupText;
    private Button backButton;

    private Button AcceptInvitationFromGroupBtn;
    private Button AcceptJoinApplicationBtn;
    private Button AddMembersBtn;
    private Button AddAdminBtn;
    private Button AddWhiteListBtn;
    private Button BlockGroupBtn;
    private Button BlockMembersBtn;
    private Button ChangeGroupDescriptionBtn;
    private Button ChangeGroupNameBtn;
    private Button ChangeGroupOwnerBtn;
    private Button CheckIfInGroupWhiteListBtn;
    private Button CreateGroupBtn;
    private Button DeclineInvitationFromGroupBtn;
    private Button DeclineJoinApplicationBtn;
    private Button DestoryGroupBtn;
    private Button DownloadGroupSharedFileBtn;
    private Button GetGroupAnnouncementFromServerBtn;
    private Button GetGroupBlockListFromServerBtn;
    private Button GetGroupFileListFromServerBtn;
    private Button GetGroupMemberListFromServerBtn;
    private Button GetGroupMuteListFromServerBtn;
    private Button GetGroupSpecificationFromServerBtn;
    private Button GetGroupWhiteListFromServerBtn;
    private Button GetGroupWithIdBtn;
    private Button GetJoinedGroupsBtn;
    private Button GetJoinedGroupsFromServerBtn;
    private Button GetPublicGroupsFromServerBtn;
    private Button JoinPublicGroupBtn;
    private Button LeaveGroupBtn;
    private Button MuteAllMembersBtn;
    private Button MuteMembersBtn;
    private Button RemoveAdminBtn;
    private Button RemoveGroupSharedFileBtn;
    private Button RemoveMembersBtn;
    private Button RemoveWhiteListBtn;
    private Button RequestToJoinPublicGroupBtn;
    private Button UnblockGroupBtn;
    private Button UnblockMembersBtn;
    private Button UnMuteAllMembersBtn;
    private Button UnMuteMembersBtn;
    private Button UpdateGroupAnnouncementBtn;
    private Button UpdateGroupExtBtn;
    private Button UploadGroupSharedFileBtn;

    private string currentGroupId
    {
        get => groupText.text;
    }


    private void Awake()
    {
        Debug.Log("group manager test script has load");

        groupText = transform.Find("GroupText/Text").GetComponent<Text>();

        backButton = transform.Find("BackBtn").GetComponent<Button>();

        backButton.onClick.AddListener(backButtonAction);

        AcceptInvitationFromGroupBtn = transform.Find("Scroll View/Viewport/Content/AcceptInvitationFromGroupBtn").GetComponent<Button>();
        AcceptJoinApplicationBtn = transform.Find("Scroll View/Viewport/Content/AcceptJoinApplicationBtn").GetComponent<Button>();
        AddMembersBtn = transform.Find("Scroll View/Viewport/Content/AddMembersBtn").GetComponent<Button>();
        AddAdminBtn = transform.Find("Scroll View/Viewport/Content/AddAdminBtn").GetComponent<Button>();
        AddWhiteListBtn = transform.Find("Scroll View/Viewport/Content/AddWhiteListBtn").GetComponent<Button>();
        BlockGroupBtn = transform.Find("Scroll View/Viewport/Content/BlockGroupBtn").GetComponent<Button>();
        BlockMembersBtn = transform.Find("Scroll View/Viewport/Content/BlockMembersBtn").GetComponent<Button>();
        ChangeGroupDescriptionBtn = transform.Find("Scroll View/Viewport/Content/ChangeGroupDescriptionBtn").GetComponent<Button>();
        ChangeGroupNameBtn = transform.Find("Scroll View/Viewport/Content/ChangeGroupNameBtn").GetComponent<Button>();
        ChangeGroupOwnerBtn = transform.Find("Scroll View/Viewport/Content/ChangeGroupOwnerBtn").GetComponent<Button>();
        CheckIfInGroupWhiteListBtn = transform.Find("Scroll View/Viewport/Content/CheckIfInGroupWhiteListBtn").GetComponent<Button>();
        CreateGroupBtn = transform.Find("Scroll View/Viewport/Content/CreateGroupBtn").GetComponent<Button>();
        DeclineInvitationFromGroupBtn = transform.Find("Scroll View/Viewport/Content/DeclineInvitationFromGroupBtn").GetComponent<Button>();
        DeclineJoinApplicationBtn = transform.Find("Scroll View/Viewport/Content/DeclineJoinApplicationBtn").GetComponent<Button>();
        DestoryGroupBtn = transform.Find("Scroll View/Viewport/Content/DestoryGroupBtn").GetComponent<Button>();
        DownloadGroupSharedFileBtn = transform.Find("Scroll View/Viewport/Content/DownloadGroupSharedFileBtn").GetComponent<Button>();
        GetGroupAnnouncementFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupAnnouncementFromServerBtn").GetComponent<Button>();
        GetGroupBlockListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupBlockListFromServerBtn").GetComponent<Button>();
        GetGroupFileListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupFileListFromServerBtn").GetComponent<Button>();
        GetGroupMemberListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupMemberListFromServerBtn").GetComponent<Button>();
        GetGroupMuteListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupMuteListFromServerBtn").GetComponent<Button>();
        GetGroupSpecificationFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupSpecificationFromServerBtn").GetComponent<Button>();
        GetGroupWhiteListFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetGroupWhiteListFromServerBtn").GetComponent<Button>();
        GetGroupWithIdBtn = transform.Find("Scroll View/Viewport/Content/GetGroupWithIdBtn").GetComponent<Button>();
        GetJoinedGroupsBtn = transform.Find("Scroll View/Viewport/Content/GetJoinedGroupsBtn").GetComponent<Button>();
        GetJoinedGroupsFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetJoinedGroupsFromServerBtn").GetComponent<Button>();
        GetPublicGroupsFromServerBtn = transform.Find("Scroll View/Viewport/Content/GetPublicGroupsFromServerBtn").GetComponent<Button>();
        JoinPublicGroupBtn = transform.Find("Scroll View/Viewport/Content/JoinPublicGroupBtn").GetComponent<Button>();
        LeaveGroupBtn = transform.Find("Scroll View/Viewport/Content/LeaveGroupBtn").GetComponent<Button>();
        MuteAllMembersBtn = transform.Find("Scroll View/Viewport/Content/MuteAllMembersBtn").GetComponent<Button>();
        MuteMembersBtn = transform.Find("Scroll View/Viewport/Content/MuteMembersBtn").GetComponent<Button>();
        RemoveAdminBtn = transform.Find("Scroll View/Viewport/Content/RemoveAdminBtn").GetComponent<Button>();
        RemoveGroupSharedFileBtn = transform.Find("Scroll View/Viewport/Content/RemoveGroupSharedFileBtn").GetComponent<Button>();
        RemoveMembersBtn = transform.Find("Scroll View/Viewport/Content/RemoveMembersBtn").GetComponent<Button>();
        RemoveWhiteListBtn = transform.Find("Scroll View/Viewport/Content/RemoveWhiteListBtn").GetComponent<Button>();
        RequestToJoinPublicGroupBtn = transform.Find("Scroll View/Viewport/Content/RequestToJoinPublicGroupBtn").GetComponent<Button>();
        UnblockGroupBtn = transform.Find("Scroll View/Viewport/Content/UnblockGroupBtn").GetComponent<Button>();
        UnblockMembersBtn = transform.Find("Scroll View/Viewport/Content/UnblockMembersBtn").GetComponent<Button>();
        UnMuteAllMembersBtn = transform.Find("Scroll View/Viewport/Content/UnMuteAllMembersBtn").GetComponent<Button>();
        UnMuteMembersBtn = transform.Find("Scroll View/Viewport/Content/UnMuteMembersBtn").GetComponent<Button>();
        UpdateGroupAnnouncementBtn = transform.Find("Scroll View/Viewport/Content/UpdateGroupAnnouncementBtn").GetComponent<Button>();
        UpdateGroupExtBtn = transform.Find("Scroll View/Viewport/Content/UpdateGroupExtBtn").GetComponent<Button>();
        UploadGroupSharedFileBtn = transform.Find("Scroll View/Viewport/Content/UploadGroupSharedFileBtn").GetComponent<Button>();

        AcceptInvitationFromGroupBtn.onClick.AddListener(AcceptInvitationFromGroupBtnAction);
        AcceptJoinApplicationBtn.onClick.AddListener(AcceptJoinApplicationBtnAction);
        AddMembersBtn.onClick.AddListener(AddMembersBtnAction);
        AddAdminBtn.onClick.AddListener(AddAdminBtnAction);
        AddWhiteListBtn.onClick.AddListener(AddWhiteListBtnAction);
        BlockGroupBtn.onClick.AddListener(BlockGroupBtnAction);
        BlockMembersBtn.onClick.AddListener(BlockMembersBtnAction);
        ChangeGroupDescriptionBtn.onClick.AddListener(ChangeGroupDescriptionBtnAction);
        ChangeGroupNameBtn.onClick.AddListener(ChangeGroupNameBtnAction);
        ChangeGroupOwnerBtn.onClick.AddListener(ChangeGroupOwnerBtnAction);
        CheckIfInGroupWhiteListBtn.onClick.AddListener(CheckIfInGroupWhiteListBtnAction);
        CreateGroupBtn.onClick.AddListener(CreateGroupBtnAction);
        DeclineInvitationFromGroupBtn.onClick.AddListener(DeclineInvitationFromGroupBtnAction);
        DeclineJoinApplicationBtn.onClick.AddListener(DeclineJoinApplicationBtnAction);
        DestoryGroupBtn.onClick.AddListener(DestoryGroupBtnAction);
        DownloadGroupSharedFileBtn.onClick.AddListener(DownloadGroupSharedFileBtnAction);
        GetGroupAnnouncementFromServerBtn.onClick.AddListener(GetGroupAnnouncementFromServerBtnAction);
        GetGroupBlockListFromServerBtn.onClick.AddListener(GetGroupBlockListFromServerBtnAction);
        GetGroupFileListFromServerBtn.onClick.AddListener(GetGroupFileListFromServerBtnAction);
        GetGroupMemberListFromServerBtn.onClick.AddListener(GetGroupMemberListFromServerBtnAction);
        GetGroupMuteListFromServerBtn.onClick.AddListener(GetGroupMuteListFromServerBtnAction);
        GetGroupSpecificationFromServerBtn.onClick.AddListener(GetGroupSpecificationFromServerBtnAction);
        GetGroupWhiteListFromServerBtn.onClick.AddListener(GetGroupWhiteListFromServerBtnAction);
        GetGroupWithIdBtn.onClick.AddListener(GetGroupWithIdBtnAction);
        GetJoinedGroupsBtn.onClick.AddListener(GetJoinedGroupsBtnAction);
        GetJoinedGroupsFromServerBtn.onClick.AddListener(GetJoinedGroupsFromServerBtnAction);
        GetPublicGroupsFromServerBtn.onClick.AddListener(GetPublicGroupsFromServerBtnAction);
        JoinPublicGroupBtn.onClick.AddListener(JoinPublicGroupBtnAction);
        LeaveGroupBtn.onClick.AddListener(LeaveGroupBtnAction);
        MuteAllMembersBtn.onClick.AddListener(MuteAllMembersBtnAction);
        MuteMembersBtn.onClick.AddListener(MuteMembersBtnAction);
        RemoveAdminBtn.onClick.AddListener(RemoveAdminBtnAction);
        RemoveGroupSharedFileBtn.onClick.AddListener(RemoveGroupSharedFileBtnAction);
        RemoveMembersBtn.onClick.AddListener(RemoveMembersBtnAction);
        RemoveWhiteListBtn.onClick.AddListener(RemoveWhiteListBtnAction);
        RequestToJoinPublicGroupBtn.onClick.AddListener(RequestToJoinPublicGroupBtnAction);
        UnblockGroupBtn.onClick.AddListener(UnblockGroupBtnAction);
        UnblockMembersBtn.onClick.AddListener(UnblockMembersBtnAction);
        UnMuteAllMembersBtn.onClick.AddListener(UnMuteAllMembersBtnAction);
        UnMuteMembersBtn.onClick.AddListener(UnMuteMembersBtnAction);
        UpdateGroupAnnouncementBtn.onClick.AddListener(UpdateGroupAnnouncementBtnAction);
        UpdateGroupExtBtn.onClick.AddListener(UpdateGroupExtBtnAction);
        UploadGroupSharedFileBtn.onClick.AddListener(UploadGroupSharedFileBtnAction);



    }

    private void OnDestroy()
    {

    }

    void AcceptInvitationFromGroupBtnAction()
    {
        UIManager.DefaultAlert(transform, "请在收到请求后回复");
        Debug.Log("AcceptInvitationFromGroupBtnAction");
    }
    void AcceptJoinApplicationBtnAction()
    {
        UIManager.DefaultAlert(transform, "请在收到请求后回复");
        Debug.Log("AcceptJoinApplicationBtnAction");
    }
    void AddMembersBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.GroupManager.AddGroupMembers(currentGroupId, list, new CallBack(
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

        Debug.Log("AddMembersBtnAction");
    }
    void AddAdminBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string aid = dict["adminId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == aid || 0 == aid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.AddGroupAdmin(currentGroupId, dict["adminId"], new CallBack(
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

        Debug.Log("AddAdminBtnAction");
    }

    void AddWhiteListBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.GroupManager.AddGroupAllowList(currentGroupId, list, new CallBack(
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

        Debug.Log("AddAdminBtnAction");
    }
    void BlockGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.BlockGroup(currentGroupId, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
    }
    void BlockMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.GroupManager.BlockGroupMembers(currentGroupId, list, new CallBack(
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

        Debug.Log("BlockMembersBtnAction");
    }
    void ChangeGroupDescriptionBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string descStr = dict["Description"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == descStr || 0 == descStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.ChangeGroupDescription(currentGroupId, dict["Description"], new CallBack(
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

        Debug.Log("ChangeGroupDescriptionBtnAction");
    }

    void ChangeGroupNameBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string nameStr = dict["name"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == nameStr || 0 == nameStr.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.ChangeGroupName(currentGroupId, dict["name"], new CallBack(
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

        config.AddField("name");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("ChangeGroupNameBtnAction");
    }
    void ChangeGroupOwnerBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string nw = dict["newOwner"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == nw || 0 == nw.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.ChangeGroupOwner(currentGroupId, dict["newOwner"], new CallBack(
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

        Debug.Log("ChangeGroupOwnerBtnAction");

    }
    void CheckIfInGroupWhiteListBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.CheckIfInGroupAllowList(currentGroupId, new ValueCallBack<bool>(
           onSuccess: (ret) =>
           {
               UIManager.DefaultAlert(transform, ret ? "在白名单中" : "不在白名单中");
           },
           onError: (code, desc) =>
           {
               UIManager.ErrorAlert(transform, code, desc);
           }
        ));

        Debug.Log("CheckIfInGroupWhiteListBtnAction");

    }
    void CreateGroupBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {

            string name = dict["name"];
            string desc = dict["desc"];
            string memberId = dict["memberId"];
            if (null == name || 0 == name.Length || null == memberId || 0 == memberId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            GroupOptions optison = new GroupOptions(GroupStyle.PrivateMemberCanInvite);

            List<string> members = new List<string>();

            if (memberId.Length > 0)
            {
                members.Add(memberId);
            }
            SDKClient.Instance.GroupManager.CreateGroup(name, optison, desc, members, callback: new ValueCallBack<Group>(
                onSuccess: (group) =>
                {
                    groupText.text = group.GroupId;
                    UIManager.DefaultAlert(transform, group.GroupId);
                },
                onError: (code, error) =>
                {
                    UIManager.ErrorAlert(transform, code, error);
                }
            ));

        });
        config.AddField("name");
        config.AddField("desc");
        config.AddField("memberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("CreateGroupBtnAction");
    }

    void DeclineInvitationFromGroupBtnAction()
    {

        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        SDKClient.Instance.GroupManager.DeclineGroupInvitation(currentGroupId, callback: new CallBack(
            onSuccess: () =>
             {
                 UIManager.SuccessAlert(transform);
             },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
        Debug.Log("DeclineInvitationFromGroupBtnAction");
    }

    void DeclineJoinApplicationBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (null == currentGroupId || 0 == currentGroupId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            SDKClient.Instance.GroupManager.DeclineGroupJoinApplication(currentGroupId, dict["userId"], callback: new CallBack(
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

        config.AddField("userId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("DeclineJoinApplicationBtnAction");
    }

    void DestoryGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.DestroyGroup(currentGroupId, new CallBack(
            onSuccess: () =>
            {
                UIManager.SuccessAlert(transform);
            },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
        ));

        Debug.Log("DestoryGroupBtnAction");
    }

    void DownloadGroupSharedFileBtnAction()
    {

        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string fileId = dict["fileId"];
            string savePath = Application.temporaryCachePath + "/" + fileId;
            Debug.Log($"savePath: {savePath}");
            SDKClient.Instance.GroupManager.DownloadGroupSharedFile(currentGroupId, fileId, savePath, new CallBack(
                onSuccess: () =>
                {
                    UIManager.SuccessAlert(transform);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                },
                onProgress: (progress) =>
                {
                    Debug.Log($"download file progress: ${progress}");
                }

            ));
        });
        config.AddField("fileId");
        //config.AddField("savePath");
        UIManager.DefaultInputAlert(transform, config);
    }

    void GetGroupAnnouncementFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupAnnouncementFromServer(currentGroupId, new ValueCallBack<string>(
                onSuccess: (str) =>
                {
                    UIManager.DefaultAlert(transform, str);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));

        Debug.Log("GetGroupAnnouncementFromServerBtnAction");
    }

    void GetGroupBlockListFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {

            int pageNum = int.Parse(dict["pageNum"]);
            int pageSize = int.Parse(dict["pageSize"]);

            SDKClient.Instance.GroupManager.GetGroupBlockListFromServer(currentGroupId, pageNum: pageNum, pageSize: pageSize, callback: new ValueCallBack<List<string>>(
                onSuccess: (list) =>
                {
                    string str = string.Join(",", list.ToArray());
                    UIManager.DefaultAlert(transform, str);
                },
                onError: (code, desc) =>
                {
                    UIManager.ErrorAlert(transform, code, desc);
                }
            ));
        });
        config.AddField("pageSize");
        config.AddField("pageNum");

        UIManager.DefaultInputAlert(transform, config);


        Debug.Log("GetGroupBlockListFromServerBtnAction");
    }

    void GetGroupFileListFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupFileListFromServer(currentGroupId, callback: new ValueCallBack<List<GroupSharedFile>>(
            onSuccess: (fileList) =>
            {
                if (0 == fileList.Count)
                {
                    UIManager.DefaultAlert(transform, "filelist未配置");
                    return;
                }
                List<string> list = new List<string>();
                foreach (var file in fileList)
                {
                    list.Add(file.FileId);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
        Debug.Log("GetGroupFileListFromServerBtnAction");
    }

    void GetGroupMemberListFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupMemberListFromServer(currentGroupId, callback: new ValueCallBack<CursorResult<string>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();
                foreach (var username in result.Data)
                {
                    list.Add(username);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("GetGroupMemberListFromServerBtnAction");
    }

    void GetGroupMuteListFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupMuteListFromServer(currentGroupId, callback: new ValueCallBack<Dictionary<string, long>>(
            onSuccess: (dict) =>
            {
                List<string> list = new List<string>();
                foreach (string key in dict.Keys)
                {
                    list.Add(key);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("GetGroupMuteListFromServerBtnAction");
    }

    void GetGroupSpecificationFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer(currentGroupId, new ValueCallBack<Group>(
            onSuccess: (group) =>
            {
                List<string> list = new List<string>();
                list.Add(group.Name);
                list.Add(group.Description);
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("GetGroupSpecificationFromServerBtnAction");

    }

    void GetGroupWhiteListFromServerBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.GetGroupAllowListFromServer(currentGroupId, callback: new ValueCallBack<List<string>>(
            onSuccess: (list) =>
            {
                if (0 == list.Count)
                {
                    UIManager.DefaultAlert(transform, "Empty group white List");
                    return;
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));

        Debug.Log("GetGroupWhiteListFromServerBtnAction");
    }

    void GetGroupWithIdBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        Group group = SDKClient.Instance.GroupManager.GetGroupWithId(currentGroupId);
        if (group != null)
        {
            UIManager.SuccessAlert(transform);
        }
        else
        {
            UIManager.DefaultAlert(transform, "未获取到群组");
        }

        Debug.Log("GetGroupWithIdBtnAction");
    }

    void GetJoinedGroupsBtnAction()
    {
        List<Group> groupList = SDKClient.Instance.GroupManager.GetJoinedGroups();
        if (0 == groupList.Count)
        {
            UIManager.DefaultAlert(transform, "未加入任何群组");
            return;
        }
        List<string> list = new List<string>();
        foreach (var group in groupList)
        {
            list.Add(group.GroupId);
        }
        string str = string.Join(",", list.ToArray());
        UIManager.DefaultAlert(transform, str);

        Debug.Log("GetJoinedGroupsBtnAction");
    }

    void GetJoinedGroupsFromServerBtnAction()
    {
        SDKClient.Instance.GroupManager.FetchJoinedGroupsFromServer(0, 20, callback: new ValueCallBack<List<Group>>(
            onSuccess: (groupList) =>
            {
                List<string> list = new List<string>();
                foreach (var group in groupList)
                {
                    list.Add(group.GroupId);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
        Debug.Log("GetJoinedGroupsFromServerBtnAction");
    }

    void GetPublicGroupsFromServerBtnAction()
    {
        SDKClient.Instance.GroupManager.FetchPublicGroupsFromServer(callback: new ValueCallBack<CursorResult<GroupInfo>>(
            onSuccess: (result) =>
            {
                List<string> list = new List<string>();
                foreach (var group in result.Data)
                {
                    list.Add(group.GroupName);
                }
                string str = string.Join(",", list.ToArray());
                UIManager.DefaultAlert(transform, str);
            },
            onError: (code, desc) =>
            {
                UIManager.ErrorAlert(transform, code, desc);
            }
        ));
    }

    void JoinPublicGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.JoinPublicGroup(currentGroupId, new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));

        Debug.Log("JoinPublicGroupBtnAction");
    }

    void LeaveGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.LeaveGroup(currentGroupId, new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));
        Debug.Log("LeaveGroupBtnAction");
    }

    void MuteAllMembersBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.MuteGroupAllMembers(currentGroupId, new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));
        Debug.Log("MuteAllMembersBtnAction");
    }

    void MuteMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.GroupManager.MuteGroupMembers(currentGroupId, list, -1, new CallBack(
                onSuccess: () => { UIManager.SuccessAlert(transform); },
                onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
            ));
        });

        config.AddField("memberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("MuteMembersBtnAction");
    }

    void RemoveAdminBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string id = dict["adminId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.RemoveGroupAdmin(currentGroupId, dict["adminId"], new CallBack(
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

        Debug.Log("RemoveAdminBtnAction");
    }

    void RemoveGroupSharedFileBtnAction()
    {
        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string id = dict["fileId part1"];
            id += dict["fileId part2"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == id || 0 == id.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            SDKClient.Instance.GroupManager.DeleteGroupSharedFile(currentGroupId, id, new CallBack(
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

        config.AddField("fileId part1");
        config.AddField("fileId part2");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveGroupSharedFileBtnAction");
    }

    void RemoveMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["MemberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["MemberId"]);
            SDKClient.Instance.GroupManager.DeleteGroupMembers(currentGroupId, list, new CallBack(
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

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveMembersBtnAction");
    }
    void RemoveWhiteListBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["MemberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["MemberId"]);
            SDKClient.Instance.GroupManager.RemoveGroupAllowList(currentGroupId, list, new CallBack(
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

        config.AddField("MemberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("RemoveWhiteListBtnAction");
    }
    void RequestToJoinPublicGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.applyJoinToGroup(currentGroupId, callback: new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));

        Debug.Log("RequestToJoinPublicGroupBtnAction");
    }

    void UnblockGroupBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.UnBlockGroup(currentGroupId, new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));
        Debug.Log("UnblockGroupBtnAction");
    }

    void UnblockMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string mid = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == mid || 0 == mid.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(dict["memberId"]);
            SDKClient.Instance.GroupManager.UnBlockGroupMembers(currentGroupId, list, new CallBack(
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

        Debug.Log("UnblockMembersBtnAction");
    }

    void UnMuteAllMembersBtnAction()
    {
        if (null == currentGroupId || 0 == currentGroupId.Length)
        {
            UIManager.DefaultAlert(transform, "缺少必要参数");
            return;
        }
        SDKClient.Instance.GroupManager.UnMuteGroupAllMembers(currentGroupId, new CallBack(
            onSuccess: () => { UIManager.SuccessAlert(transform); },
            onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
        ));
        Debug.Log("UnMuteAllMembersBtnAction");
    }

    void UnMuteMembersBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            string member = dict["memberId"];
            if (null == currentGroupId || 0 == currentGroupId.Length || null == member || 0 == member.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }

            List<string> list = new List<string>();
            list.Add(member);
            SDKClient.Instance.GroupManager.UnMuteGroupMembers(currentGroupId, list, new CallBack(
                onSuccess: () => { UIManager.SuccessAlert(transform); },
                onError: (code, desc) => { UIManager.ErrorAlert(transform, code, desc); }
            ));
        });

        config.AddField("memberId");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UnMuteMembersBtnAction");

    }
    void UpdateGroupAnnouncementBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (null == currentGroupId || 0 == currentGroupId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            SDKClient.Instance.GroupManager.UpdateGroupAnnouncement(currentGroupId, dict["Announcement"], new CallBack(
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

        config.AddField("Announcement");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UpdateGroupAnnouncementBtnAction");
    }

    void UpdateGroupExtBtnAction()
    {

        InputAlertConfig config = new InputAlertConfig((dict) =>
        {
            if (null == currentGroupId || 0 == currentGroupId.Length)
            {
                UIManager.DefaultAlert(transform, "缺少必要参数");
                return;
            }
            SDKClient.Instance.GroupManager.UpdateGroupExt(currentGroupId, dict["ext"], new CallBack(
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

        config.AddField("ext");

        UIManager.DefaultInputAlert(transform, config);

        Debug.Log("UpdateGroupExtBtnAction");
    }

    void UploadGroupSharedFileBtnAction()
    {
        UIManager.UnfinishedAlert(transform);
        Debug.Log("UploadGroupSharedFileBtnAction");
    }


    void backButtonAction()
    {
        SceneManager.LoadSceneAsync("Main");
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
