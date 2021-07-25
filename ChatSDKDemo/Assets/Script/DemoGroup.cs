using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChatSDK;
using UnityEngine.SceneManagement;

public class DemoGroup : MonoBehaviour, IGroupManagerDelegate
{
    public Text InputText;
    public Button BackBtn;
    public Button Custom1Btn;
    public Button Custom2Btn;
    public Button Custom3Btn;
    public Button Custom4Btn;
    public Button Custom5Btn;
    public Button Custom6Btn;
    public Button Custom7Btn;
    public Button Custom8Btn;
    public Button Custom9Btn;
    public Button Custom10Btn;
    public Button Custom11Btn;
    public Button Custom12Btn;
    public Button Custom13Btn;
    public Button Custom14Btn;
    public Button Custom15Btn;
    public Button Custom16Btn;
    public Button Custom17Btn;
    public Button Custom18Btn;
    public Button Custom19Btn;
    public Button Custom20Btn;
    public Button Custom21Btn;
    public Button Custom22Btn;
    public Button Custom23Btn;
    public Button Custom24Btn;
    public Button Custom25Btn;
    public Button Custom26Btn;
    public Button Custom27Btn;
    public Button Custom28Btn;
    public Button Custom29Btn;
    public Button Custom30Btn;
    public Button Custom31Btn;
    public Button Custom32Btn;
    public Button Custom33Btn;
    public Button Custom34Btn;
    public Button Custom35Btn;
    public Button Custom36Btn;
    public Button Custom37Btn;
    public Button Custom38Btn;
    public Button Custom39Btn;
    public Button Custom40Btn;
    public Button Custom41Btn;
    public Button Custom42Btn;
    public Button Custom43Btn;
    public Button Custom44Btn;


    private string currentGroupId;

    private string firstInput
    {
        get => "154996968587266";
        //get => currentGroupId;
        //get => InputText.text ?? currentGroupId;
    }

    // Start is called before the first frame update
    void Start()
    {
        BackBtn.onClick.AddListener(BackAction);
        Custom1Btn.onClick.AddListener(Custom1Action);
        Custom2Btn.onClick.AddListener(Custom2Action);
        Custom3Btn.onClick.AddListener(Custom3Action);
        Custom4Btn.onClick.AddListener(Custom4Action);
        Custom5Btn.onClick.AddListener(Custom5Action);
        Custom6Btn.onClick.AddListener(Custom6Action);
        Custom7Btn.onClick.AddListener(Custom7Action);
        Custom8Btn.onClick.AddListener(Custom8Action);
        Custom9Btn.onClick.AddListener(Custom9Action);
        Custom10Btn.onClick.AddListener(Custom10Action);
        Custom11Btn.onClick.AddListener(Custom11Action);
        Custom12Btn.onClick.AddListener(Custom12Action);
        Custom13Btn.onClick.AddListener(Custom13Action);
        Custom14Btn.onClick.AddListener(Custom14Action);
        Custom15Btn.onClick.AddListener(Custom15Action);
        Custom16Btn.onClick.AddListener(Custom16Action);
        Custom17Btn.onClick.AddListener(Custom17Action);
        Custom18Btn.onClick.AddListener(Custom18Action);
        Custom19Btn.onClick.AddListener(Custom19Action);
        Custom20Btn.onClick.AddListener(Custom20Action);
        Custom21Btn.onClick.AddListener(Custom21Action);
        Custom22Btn.onClick.AddListener(Custom22Action);
        Custom23Btn.onClick.AddListener(Custom23Action);
        Custom24Btn.onClick.AddListener(Custom24Action);
        Custom25Btn.onClick.AddListener(Custom25Action);
        Custom26Btn.onClick.AddListener(Custom26Action);
        Custom27Btn.onClick.AddListener(Custom27Action);
        Custom28Btn.onClick.AddListener(Custom28Action);
        Custom29Btn.onClick.AddListener(Custom29Action);
        Custom30Btn.onClick.AddListener(Custom30Action);
        Custom31Btn.onClick.AddListener(Custom31Action);
        Custom32Btn.onClick.AddListener(Custom32Action);
        Custom33Btn.onClick.AddListener(Custom33Action);
        Custom34Btn.onClick.AddListener(Custom34Action);
        Custom35Btn.onClick.AddListener(Custom35Action);
        Custom36Btn.onClick.AddListener(Custom36Action);
        Custom37Btn.onClick.AddListener(Custom37Action);
        Custom38Btn.onClick.AddListener(Custom38Action);
        Custom39Btn.onClick.AddListener(Custom39Action);
        Custom40Btn.onClick.AddListener(Custom40Action);
        Custom41Btn.onClick.AddListener(Custom41Action);
        Custom42Btn.onClick.AddListener(Custom42Action);
        Custom43Btn.onClick.AddListener(Custom43Action);
        Custom44Btn.onClick.AddListener(Custom44Action);

        SDKClient.Instance.GroupManager.AddGroupManagerDelegate(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BackAction() {
        SceneManager.LoadScene("Main");
    }
    void Custom1Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 -- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        
        SDKClient.Instance.GroupManager.AcceptInvitationFromGroup(firstInput, callback);
        
    }
    void Custom2Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        string text = InputText.text;
        SDKClient.Instance.GroupManager.AcceptJoinApplication(firstInput, "du003" , callback);
    }
    void Custom3Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.AddMembers(firstInput, list, callback);
    }
    void Custom4Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 -- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.AddAdmin(firstInput, "du003", callback);
    }
    void Custom5Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.AddWhiteList(firstInput, list, callback);
    }

    void Custom6Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.BlockGroup(firstInput, callback);
    }

    void Custom7Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.BlockMembers(firstInput, list, callback);
    }

    void Custom8Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.ChangeGroupDescription(firstInput, "我是修改的描述", callback);
    }

    void Custom9Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.ChangeGroupName(firstInput, "我是修改的名称", callback);
    }

    void Custom10Action() {
        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 -- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.ChangeGroupOwner(firstInput, "du003", callback);
        
    }

    void Custom11Action() {

        ValueCallBack<bool> callback = new ValueCallBack<bool>();
        callback.OnSuccessValue = (bool inWhiteList) => {
            Debug.Log("操作成功 --inWhiteList  " + (inWhiteList ? "在白名单":"不在白名单"));
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.CheckIfInGroupWhiteList(firstInput, callback);
        
    }

    void Custom12Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 -- " + group.GroupId);
            currentGroupId = group.GroupId;
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        GroupOptions groupOptions = new GroupOptions(GroupStyle.PublicJoinNeedApproval, 300, false);

        SDKClient.Instance.GroupManager.CreateGroup("我是创建的群", groupOptions, handle: callback);
    }

    void Custom13Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        SDKClient.Instance.GroupManager.DeclineInvitationFromGroup(firstInput, handle: callback);
    }

    void Custom14Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.DeclineJoinApplication(firstInput, "du003", handle: callback);
    }

    void Custom15Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.DestroyGroup(firstInput, callback);

    }

    void Custom16Action() {
        //SDKClient.Instance.GroupManager.DownloadGroupSharedFile(firstInput, callback);
        
    }

    void Custom17Action() {

        ValueCallBack<string> callback = new ValueCallBack<string>();

        callback.OnSuccessValue = (string str) => {
            Debug.Log("操作成功 " + str);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupAnnouncementFromServer(firstInput, callback);
    }

    void Custom18Action() {

        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>();

        callback.OnSuccessValue = (List<string> list) => {
            foreach (var str in list) {
                Debug.Log("操作成功 " + str);
            }
            
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupBlockListFromServer(firstInput, handle: callback);

    }

    void Custom19Action() {

        ValueCallBack<List<GroupSharedFile>> callback = new ValueCallBack<List<GroupSharedFile>>();

        callback.OnSuccessValue = (List<GroupSharedFile> list) => {
            foreach (var shareFile in list)
            {
                Debug.Log("操作成功 " + shareFile.FileId);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupFileListFromServer(firstInput, handle: callback);

    }

    void Custom20Action() {

        ValueCallBack<CursorResult<string>> callback = new ValueCallBack<CursorResult<string>>();

        callback.OnSuccessValue = (CursorResult<string> result) => {
            foreach (var str in result.Data)
            {
                Debug.Log("操作成功 " + str);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupMemberListFromServer(firstInput, handle: callback);

    }

    void Custom21Action() {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>();

        callback.OnSuccessValue = (List<string> result) => {
            foreach (var str in result)
            {
                Debug.Log("操作成功 " + str);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupMuteListFromServer(firstInput, handle: callback);
        
    }

    void Custom22Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();

        callback.OnSuccessValue = (Group group) => {
            foreach (var s in group.AdminList) {
                Debug.Log("操作成功 admin " + s);
            }

            foreach (var s in group.MuteList)
            {
                Debug.Log("操作成功 mute " + s);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer(firstInput, callback);

    }

    void Custom23Action() {

        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>();

        callback.OnSuccessValue = (List<string> result) => {
            foreach (var str in result)
            {
                Debug.Log("操作成功 " + str);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetGroupWhiteListFromServer(firstInput, handle: callback);
   
    }

    void Custom24Action() {
        Group group = SDKClient.Instance.GroupManager.GetGroupWithId(firstInput);
        Debug.Log("操作成功 " + group.GroupId);
    }

    void Custom25Action() {
        List<Group>list = SDKClient.Instance.GroupManager.GetJoinedGroups();
        foreach (var group in list) {
            Debug.Log("操作成功 " + group.GroupId);
        }
    }

       
    void Custom26Action() {
        ValueCallBack<List<Group>> callback = new ValueCallBack<List<Group>>();

        callback.OnSuccessValue = (List<Group> list) =>
        {
            foreach (var group in list)
            {
                Debug.Log("操作成功 " + group.GroupId);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetJoinedGroupsFromServer(handle: callback);
    }

    void Custom27Action() {

        ValueCallBack<CursorResult<GroupInfo>> callback = new ValueCallBack<CursorResult<GroupInfo>>();

        callback.OnSuccessValue = (CursorResult<GroupInfo> result) =>
        {
            foreach (var groupInfo in result.Data)
            {
                Debug.Log("操作成功 " + groupInfo.GroupId);
            }
        };

        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.GetPublicGroupsFromServer(handle: callback);
    }

    void Custom28Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.JoinPublicGroup(firstInput, callback);
    }

    void Custom29Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.LeaveGroup("152172918538241", callback);
    }

    void Custom30Action() {


        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.MuteAllMembers(firstInput, callback);
    }

    void Custom31Action() {
        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.MuteMembers(firstInput, list, callback);
    }

    void Custom32Action() {
        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        SDKClient.Instance.GroupManager.RemoveAdmin(firstInput, "du003", callback);
    }

    void Custom33Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.RemoveGroupSharedFile(firstInput, "du003", callback);
    }

    void Custom34Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.RemoveMembers(firstInput, list, callback);
    }

    void Custom35Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.RemoveWhiteList(firstInput, list, callback);
    }

    void Custom36Action() {
        //SDKClient.Instance.GroupManager.JoinPublicGroup();
    }

    void Custom37Action() {
        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };
        SDKClient.Instance.GroupManager.UnBlockGroup(firstInput, callback);
    }

    void Custom38Action() {

        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };
        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.UnBlockMembers(firstInput, list, callback);
    }

    void Custom39Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        SDKClient.Instance.GroupManager.UnMuteAllMembers(firstInput, callback);
    }

    void Custom40Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.UnMuteMembers(firstInput, list, callback);
    }

    void Custom41Action() {


        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.UpdateGroupAnnouncement(firstInput, "我是修改的公告", callback);
    }

    void Custom42Action() {

        ValueCallBack<Group> callback = new ValueCallBack<Group>();
        callback.OnSuccessValue = (Group group) => {
            Debug.Log("操作成功 --- " + group.GroupId);
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.UpdateGroupExt(firstInput, "新的ext", callback);
    }

    void Custom43Action() {


        CallBack callback = new CallBack();
        callback.Success = () => {
            Debug.Log("操作成功");
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };

        SDKClient.Instance.GroupManager.applyJoinToGroup(firstInput, handle: callback);
    }

    void Custom44Action() { }

    public void OnInvitationReceivedFromGroup(string groupId, string groupName, string inviter, string reason)
    {
        currentGroupId = groupId;
        Debug.Log("OnInvitationReceivedFromGroup --- " + groupId + " " + groupName + " " + inviter + " " + reason);
    }

    public void OnRequestToJoinReceivedFromGroup(string groupId, string groupName, string applicant, string reason)
    {
        Debug.Log("OnRequestToJoinReceivedFromGroup --- " + groupId + " " + groupName + " " + applicant + " " + reason);
    }

    public void OnRequestToJoinAcceptedFromGroup(string groupId, string groupName, string accepter)
    {
        Debug.Log("OnRequestToJoinAcceptedFromGroup --- " + groupId + " " + groupName + " " + accepter);
    }

    public void OnRequestToJoinDeclinedFromGroup(string groupId, string groupName, string decliner, string reason)
    {
        Debug.Log("OnRequestToJoinDeclinedFromGroup --- " + groupId + " " + groupName + " " + decliner + " " + reason);
    }

    public void OnInvitationAcceptedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("OnInvitationAcceptedFromGroup --- " + groupId + " " + invitee + " " + reason);
    }

    public void OnInvitationDeclinedFromGroup(string groupId, string invitee, string reason)
    {
        Debug.Log("OnInvitationDeclinedFromGroup --- " + groupId + " " + invitee + " "  + reason);
    }

    public void OnUserRemovedFromGroup(string groupId, string groupName)
    {
        Debug.Log("OnUserRemovedFromGroup --- " + groupId + " " + groupName );
    }

    public void OnDestroyedFromGroup(string groupId, string groupName)
    {
        Debug.Log("OnDestroyedFromGroup --- " + groupId + " " + groupName);
    }

    public void OnAutoAcceptInvitationFromGroup(string groupId, string inviter, string inviteMessage)
    {
        Debug.Log("OnAutoAcceptInvitationFromGroup --- " + groupId + " " + inviter + " " + inviteMessage);
    }

    public void OnMuteListAddedFromGroup(string groupId, List<string> mutes, int muteExpire)
    {
        Debug.Log("OnMuteListAddedFromGroup --- " + groupId);
        foreach (var username in mutes) {
            Debug.Log("username --- " + username);
        }
    }

    public void OnMuteListRemovedFromGroup(string groupId, List<string> mutes)
    {
        Debug.Log("OnMuteListRemovedFromGroup --- " + groupId);
        foreach (var username in mutes)
        {
            Debug.Log("username --- " + username);
        }
    }

    public void OnAdminAddedFromGroup(string groupId, string administrator)
    {
        Debug.Log("OnAdminAddedFromGroup --- " + groupId + " " + administrator);
    }

    public void OnAdminRemovedFromGroup(string groupId, string administrator)
    {
        Debug.Log("OnAdminRemovedFromGroup --- " + groupId + " " + administrator);
    }

    public void OnOwnerChangedFromGroup(string groupId, string newOwner, string oldOwner)
    {
        Debug.Log("OnOwnerChangedFromGroup --- " + groupId + " " + newOwner + " " + oldOwner);
    }

    public void OnMemberJoinedFromGroup(string groupId, string member)
    {
        Debug.Log("OnMemberJoinedFromGroup --- " + groupId + " " + member);
    }

    public void OnMemberExitedFromGroup(string groupId, string member)
    {
        Debug.Log("OnMemberExitedFromGroup --- " + groupId + " " + member);
    }

    public void OnAnnouncementChangedFromGroup(string groupId, string announcement)
    {
        Debug.Log("OnAnnouncementChangedFromGroup --- " + groupId + " " + announcement);
    }

    public void OnSharedFileAddedFromGroup(string groupId, GroupSharedFile sharedFile)
    {
        Debug.Log("OnSharedFileAddedFromGroup --- " + groupId + " " + sharedFile.FileId + " " + sharedFile.FileName + " " + sharedFile.FileOwner);
    }

    public void OnSharedFileDeletedFromGroup(string groupId, string fileId)
    {
        Debug.Log("OnSharedFileDeletedFromGroup --- " + groupId + " " + fileId);
    }
}
