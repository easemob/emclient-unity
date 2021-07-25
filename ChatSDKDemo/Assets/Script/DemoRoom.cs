using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ChatSDK;

public class DemoRoom : MonoBehaviour, IRoomManagerDelegate
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


    private string currentId;

    private string roomId  {
        get => currentId ?? InputText.text;
    }

    // Start is called before the first frame update

    void Start()
    {
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

        BackBtn.onClick.AddListener(BackAction);

        SDKClient.Instance.RoomManager.AddRoomManagerDelegate(this);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Custom1Action()
    {
        ValueCallBack<Room> callback = new ValueCallBack<Room>(
                onSuccess: (room) => {
                    Debug.Log("管理员列表添加 ===== ");
                    foreach (string s in room.AdminList)
                    {
                        Debug.Log("管理员列表 ===== " + s);
                    }
                }

            );

        SDKClient.Instance.RoomManager.AddRoomAdmin(roomId, "du003", callback);

    }

    void Custom2Action()
    {
        ValueCallBack<Room> callback = new ValueCallBack<Room>(
              onSuccess: (room) => {

                  foreach (string s in room.BlockList)
                  {
                      Debug.Log("block user --- " + s);
                  }
              },

              onError: (code, desc) => {
                  Debug.Log("block user error --- " + code + " desc " + desc);
              }
         );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.BlockRoomMembers(roomId, members, callback);
    }

    void Custom3Action()
    {

        ValueCallBack<Room> callback = new ValueCallBack<Room>(
            onSuccess: (room) => {
                Debug.Log("room --- " + room.Owner);
            },
            onError: (code, desc) => {
                Debug.Log("操作失败 -- " + code + " " + desc);
            }
        );


        SDKClient.Instance.RoomManager.ChangeOwner(roomId, "du003", callback);
    }

    void Custom4Action()
    {
        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
                 onSuccess: (room) => {
                     Debug.Log("room decs --- " + room.Description);
                 },
                 onError: (code, desc) => {
                     Debug.Log("操作失败 -- " + code + " " + desc);
                 }
         );

        SDKClient.Instance.RoomManager.ChangeRoomDescription(roomId, "我是聊天室描述", callBack);
    }
    void Custom5Action()
    {
        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
               onSuccess: (room) => {
                   Debug.Log("room name --- " + room.Name);
               }
       );

        SDKClient.Instance.RoomManager.ChangeRoomName(roomId, "我是聊天室名称", callBack);
    }
    void Custom6Action()
    {

        ValueCallBack<Room> callback = new ValueCallBack<Room>();
        callback.OnSuccessValue = (Room room) => {
            Debug.Log("操作成功 -- " + room.RoomId);
            currentId = room.RoomId;
        };
        callback.Error = (int code, string desc) => {
            Debug.Log("操作失败 -- " + code + " " + desc);
        };


        SDKClient.Instance.RoomManager.CreateRoom("uniaa", null, null, 200, null, handle:callback);
    }

    void Custom7Action()
    {
        CallBack callback = new CallBack(
            onSuccess: () => {
                Debug.Log("解散成功");
            },
            onError: (code, desc) => {
                Debug.Log("解散失败 --- " + code + " " + desc);
            }
        );

        SDKClient.Instance.RoomManager.DestroyRoom(roomId, callback);
    }

    void Custom8Action()
    {
        ValueCallBack<PageResult<Room>> callback = new ValueCallBack<PageResult<Room>>(

             onSuccess: (result) => {
                 foreach (Room room in result.Data)
                 {
                     Debug.Log("room --- " + room.RoomId);
                     //currRoom = room;
                 }
             }
             );

        SDKClient.Instance.RoomManager.FetchPublicRoomsFromServer(handle: callback);
    }

    void Custom9Action()
    {
        ValueCallBack<string> callBack = new ValueCallBack<string>(
             onSuccess: (Announcement) => {
                 Debug.Log("room Announcement --- " + Announcement);
             }
         );

        SDKClient.Instance.RoomManager.FetchRoomAnnouncement(roomId, callBack);
    }

    void Custom10Action()
    {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>(
            onSuccess: (list) => {

                foreach (string s in list)
                {
                    Debug.Log("block user --- " + s);
                }
            }
        );

        SDKClient.Instance.RoomManager.FetchRoomBlockList(roomId, handle: callback);
    }

    void Custom11Action()
    {
        ValueCallBack<Room> callBack = new ValueCallBack<Room>(
       onSuccess: (room) => {
           Debug.Log("room ----------- 详情成功 " + room.RoomId);
           Debug.Log("room ----------- 详情成功 " + room.Name);
           Debug.Log("room ----------- 详情成功 " + room.Description);
           Debug.Log("room ----------- 详情成功 " + room.Announcement);
           foreach (string s in room.MemberList)
           {
               Debug.Log("room ----------- 成员 " + s);
           }

           foreach (string s in room.AdminList)
           {
               Debug.Log("room ----------- 管理员 " + s);
           }

           foreach (string s in room.MuteList)
           {
               Debug.Log("room ----------- 禁言 " + s);
           }
       }
       );

        SDKClient.Instance.RoomManager.FetchRoomInfoFromServer(roomId, callBack);

    }
    void Custom12Action()
    {
        ValueCallBack<CursorResult<string>> callBack = new ValueCallBack<CursorResult<string>>(
           onSuccess: (result) => {
               foreach (string s in result.Data)
               {
                   Debug.Log("username --- " + s);
               }
           }
       );

        SDKClient.Instance.RoomManager.FetchRoomMembers(roomId, handle: callBack);

    }
    void Custom13Action()
    {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>(
            onSuccess: (list) => {

                foreach (string s in list)
                {
                    Debug.Log("禁言 user --- " + s);
                }
            }
        );

        SDKClient.Instance.RoomManager.FetchRoomMuteList(roomId, handle: callback);
    }
    void Custom14Action() { }

    
    void Custom15Action()
    {
     
        ValueCallBack<Room> callback = new ValueCallBack<Room>();
        callback.OnSuccessValue = (Room room) => {
            Debug.Log("加入成功");
        };

        callback.Error = (int code, string desc) =>
        {
            Debug.Log("加入聊天室失败 " + code + " desc " + desc);
        };

        SDKClient.Instance.RoomManager.JoinRoom("154985791815681", callback);

    }
    void Custom16Action()
    {
        CallBack callBack = new CallBack();
        callBack.Success = () => {
            Debug.Log("成功");
        };
        callBack.Error = (int code, string desc) =>
        {
            Debug.Log("失败 " + code + " desc " + desc);
        };
        SDKClient.Instance.RoomManager.LeaveRoom("154985791815681", callBack);
    }
    void Custom17Action()
    {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 成员禁言");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.MuteRoomMembers(roomId, members, callBack);
    }
    void Custom18Action()
    {
        ValueCallBack<Room> callback = new ValueCallBack<Room>(
               onSuccess: (room) =>
               {
                   Debug.Log("管理员列表删除 ===== ");
               }
       );


        SDKClient.Instance.RoomManager.RemoveRoomAdmin(roomId, "du003", callback);
    }
    void Custom19Action()
    {
        CallBack callBack = new CallBack(
           onSuccess: () => {
               Debug.Log("room ----------- 移除成功");
           },

           onError: (code, desc) => {
               Debug.Log("room ----------- 移除失败 code " + code + "  desc " + desc);
           }
       );

        List<string> members = new List<string>();
        members.Add("du003");
        SDKClient.Instance.RoomManager.RemoveRoomMembers(roomId, members, callBack);
    }
    void Custom20Action()
    {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 移除黑名单");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.UnBlockRoomMembers(roomId, members, callBack);
    }
    void Custom21Action()
    {
        CallBack callBack = new CallBack(
            onSuccess: () => {
                Debug.Log("room ----------- 成员禁言");
            }
        );

        List<string> members = new List<string>();
        members.Add("du003");

        SDKClient.Instance.RoomManager.UnMuteRoomMembers(roomId, members, callBack);
    }
    void Custom22Action()
    {
        CallBack callBack = new CallBack(
           onSuccess: () => {
               Debug.Log("room ----------- Announcement成功");
           }
       );

        SDKClient.Instance.RoomManager.UpdateRoomAnnouncement(roomId, "我是Announcement4444", callBack);
    }
    void Custom23Action()
    {

    }
    void Custom24Action()
    {

    }

    void BackAction()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnDestroyedFromRoom(string roomId, string roomName)
    {
        Debug.Log("OnDestroyedFromRoom --- " + roomId + " " + roomName);
    }

    public void OnMemberJoinedFromRoom(string roomId, string participant)
    {
        Debug.Log("OnMemberJoinedFromRoom --- " + roomId + " " + participant);
    }

    public void OnMemberExitedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("OnMemberExitedFromRoom --- " + roomId + " " + participant);
    }

    public void OnRemovedFromRoom(string roomId, string roomName, string participant)
    {
        Debug.Log("OnRemovedFromRoom --- " + roomId + " " + participant);
    }

    public void OnMuteListAddedFromRoom(string roomId, List<string> mutes, long expireTime)
    {
        Debug.Log("OnMuteListAddedFromRoom --- " + roomId);

        foreach (var name in mutes) {
            Debug.Log("name --- " + name);
        }
    }

    public void OnMuteListRemovedFromRoom(string roomId, List<string> mutes)
    {
        Debug.Log("OnMuteListRemovedFromRoom --- " + roomId);

        foreach (var name in mutes)
        {
            Debug.Log("name --- " + name);
        }
    }

    public void OnAdminAddedFromRoom(string roomId, string admin)
    {
        Debug.Log("OnAdminAddedFromRoom --- " + roomId + " " + admin);
    }

    public void OnAdminRemovedFromRoom(string roomId, string admin)
    {
        Debug.Log("OnAdminRemovedFromRoom --- " + roomId + " " + admin);
    }

    public void OnOwnerChangedFromRoom(string roomId, string newOwner, string oldOwner)
    {
        Debug.Log("OnOwnerChangedFromRoom --- " + roomId + " " + newOwner + " " + oldOwner);
    }

    public void OnAnnouncementChangedFromRoom(string roomId, string announcement)
    {
        Debug.Log("OnAnnouncementChangedFromRoom --- " + roomId + " " + announcement);
    }
}
