﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroupManagerTest : MonoBehaviour
{

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



    private void Awake()
    {
        Debug.Log("group manager test script has load");

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


    void AcceptInvitationFromGroupBtnAction() { Debug.Log("AcceptInvitationFromGroupBtnAction"); }
    void AcceptJoinApplicationBtnAction() { Debug.Log("AcceptJoinApplicationBtnAction"); }
    void AddMembersBtnAction() { Debug.Log("AddMembersBtnAction"); }
    void AddAdminBtnAction() { Debug.Log("AddAdminBtnAction"); }
    void AddWhiteListBtnAction() { Debug.Log("AddWhiteListBtnAction"); }
    void BlockGroupBtnAction() { Debug.Log("BlockGroupBtnAction"); }
    void BlockMembersBtnAction() { Debug.Log("BlockMembersBtnAction"); }
    void ChangeGroupDescriptionBtnAction() { Debug.Log("ChangeGroupDescriptionBtnAction"); }
    void ChangeGroupNameBtnAction() { Debug.Log("ChangeGroupNameBtnAction"); }
    void ChangeGroupOwnerBtnAction() { Debug.Log("ChangeGroupOwnerBtnAction"); }
    void CheckIfInGroupWhiteListBtnAction() { Debug.Log("CheckIfInGroupWhiteListBtnAction"); }
    void CreateGroupBtnAction() { Debug.Log("CreateGroupBtnAction"); }
    void DeclineInvitationFromGroupBtnAction() { Debug.Log("DeclineInvitationFromGroupBtnAction"); }
    void DeclineJoinApplicationBtnAction() { Debug.Log("DeclineJoinApplicationBtnAction"); }
    void DestoryGroupBtnAction() { Debug.Log("DestoryGroupBtnAction"); }
    void DownloadGroupSharedFileBtnAction() { Debug.Log("DownloadGroupSharedFileBtnAction"); }
    void GetGroupAnnouncementFromServerBtnAction() { Debug.Log("GetGroupAnnouncementFromServerBtnAction"); }
    void GetGroupBlockListFromServerBtnAction() { Debug.Log("GetGroupBlockListFromServerBtnAction"); }
    void GetGroupFileListFromServerBtnAction() { Debug.Log("GetGroupFileListFromServerBtnAction"); }
    void GetGroupMemberListFromServerBtnAction() { Debug.Log("GetGroupMemberListFromServerBtnAction"); }
    void GetGroupMuteListFromServerBtnAction() { Debug.Log("GetGroupMuteListFromServerBtnAction"); }
    void GetGroupSpecificationFromServerBtnAction() { Debug.Log("GetGroupSpecificationFromServerBtnAction"); }
    void GetGroupWhiteListFromServerBtnAction() { Debug.Log("GetGroupWhiteListFromServerBtnAction"); }
    void GetGroupWithIdBtnAction() { Debug.Log("GetGroupWithIdBtnAction"); }
    void GetJoinedGroupsBtnAction() { Debug.Log("GetJoinedGroupsBtnAction"); }
    void GetJoinedGroupsFromServerBtnAction() { Debug.Log("GetJoinedGroupsFromServerBtnAction"); }
    void GetPublicGroupsFromServerBtnAction() { Debug.Log("GetPublicGroupsFromServerBtnAction"); }
    void JoinPublicGroupBtnAction() { Debug.Log("JoinPublicGroupBtnAction"); }
    void LeaveGroupBtnAction() { Debug.Log("LeaveGroupBtnAction"); }
    void MuteAllMembersBtnAction() { Debug.Log("MuteAllMembersBtnAction"); }
    void MuteMembersBtnAction() { Debug.Log("MuteMembersBtnAction"); }
    void RemoveAdminBtnAction() { Debug.Log("RemoveAdminBtnAction"); }
    void RemoveGroupSharedFileBtnAction() { Debug.Log("RemoveGroupSharedFileBtnAction"); }
    void RemoveMembersBtnAction() { Debug.Log("RemoveMembersBtnAction"); }
    void RemoveWhiteListBtnAction() { Debug.Log("RemoveWhiteListBtnAction"); }
    void RequestToJoinPublicGroupBtnAction() { Debug.Log("RequestToJoinPublicGroupBtnAction"); }
    void UnblockGroupBtnAction() { Debug.Log("UnblockGroupBtnAction"); }
    void UnblockMembersBtnAction() { Debug.Log("UnblockMembersBtnAction"); }
    void UnMuteAllMembersBtnAction() { Debug.Log("UnMuteAllMembersBtnAction"); }
    void UnMuteMembersBtnAction() { Debug.Log("UnMuteMembersBtnAction"); }
    void UpdateGroupAnnouncementBtnAction() { Debug.Log("UpdateGroupAnnouncementBtnAction"); }
    void UpdateGroupExtBtnAction() { Debug.Log("UpdateGroupExtBtnAction"); }
    void UploadGroupSharedFileBtnAction() { Debug.Log("UploadGroupSharedFileBtnAction"); }


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