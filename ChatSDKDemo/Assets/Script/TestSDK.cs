using System;
using System.Collections.Generic;
using ChatSDK;
using UnityEngine;

public class TestSDK : MonoBehaviour, IRoomManagerDelegate
{

    private void Start()
    {
        // 添加监听，需在sdk 初始化后调用
        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
    }


    public void Test() {

        PushConfig config = SDKClient.Instance.PushManager.GetPushConfig();

SDKClient.Instance.PushManager.SetGroupToDisturb("groupId", true, handle: new CallBack(
    onSuccess: () =>
    {
        Debug.Log("执行成功");
    },

    onError: (code, desc) =>
    {
        Debug.Log($"错误码 -- {code}");
        Debug.Log($"错误描述 -- {desc}");
    }
));
    }

    private void OnDestroy()
    {
        // 移除监听
        SDKClient.Instance.RoomManager.RemoveRoomManagerDelegate(this);
    }


    // 聊天室被销毁回调
    public void OnDestroyedFromRoom(string roomId, string roomName)
    {

    }

    // 有用户加入聊天室回调
    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {

    }

    // 有用户离开聊天室回调
    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {

    }

    // 当前账号被移出聊天室
    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {

    }

    // 被禁言用户增加
    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {

    }

    // 被禁言用户减少
    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {

    }

    // 管理员增加
    public void OnAdminAddedFromRoom(string roomId, string admin)
    {

    }

    // 管理员减少
    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {

    }

    // 聊天室拥有者变更
    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {

    }

    // 聊天室公告变更
    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {

    }
}
