using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ChatSDK;

public class Main : MonoBehaviour
{
    // 接收消息id
    public InputField RecvIdField;
    // 输入内容
    public InputField TextField;
    // 发送按钮
    public Button SendBtn;
    // 群组id
    public InputField GroupField;
    // 加入群组按钮
    public Button JoinGroupBtn;
    // 获取群详情按钮
    public Button GroupInfoBtn;
    // 退出群组按钮
    public Button LeaveGroupBtn;
    // 聊天室id
    public InputField RoomField;
    // 加入聊天室按钮
    public Button JoinRoomBtn;
    // 获取聊天室按钮
    public Button RoomInfoBtn;
    // 退出聊天室按钮
    public Button LeaveRoomBtn;


    //public ScrollView scrollView;
    public ScrollRect scrollRect;

    IEnumerable<Toggle> ToggleGroup;

    Group firstGroup;
    Conversation conversation;


    // Start is called before the first frame update
    void Start()
    {
        SendBtn.onClick.AddListener(SendMessageAction);

        JoinGroupBtn.onClick.AddListener(JoinGroupAction);
        GroupInfoBtn.onClick.AddListener(GetGroupInfoAction);
        LeaveGroupBtn.onClick.AddListener(LeaveGroupAction);

        JoinRoomBtn.onClick.AddListener(JoinRoomAction);
        RoomInfoBtn.onClick.AddListener(GetRoomInfoAction);
        LeaveRoomBtn.onClick.AddListener(LeaveRoomAction);

        ToggleGroup = GameObject.Find("ToggleGroup").GetComponent<ToggleGroup>().ActiveToggles();
        foreach (Toggle tog in ToggleGroup) {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        Debug.Log("Quit and release resources...");
        SDKClient.Instance.Logout(false);
    }

    void SendMessageAction()
    {

        ValueCallBack<Group> callback = new ValueCallBack<Group>(
                onSuccess:(group) => {
                    Debug.Log("group 0 " + group.GroupId);
                },

                onError:(code, desc) =>
                {
                    Debug.Log("group 0 " + code + "aa " + desc);
                }
            );


        List<string> memberList = new List<string>();
        memberList.Add("du003");
        memberList.Add("du004");

        GroupOptions groupOptions = new GroupOptions(GroupStyle.PublicJoinNeedApproval);
        SDKClient.Instance.GroupManager.CreateGroup("un群组", groupOptions, "来自", memberList, null, callback);
    }

    void JoinGroupAction()
    {
        
        ValueCallBack<Group> callback = new ValueCallBack<Group>(
         onSuccess: (group) => {
             Debug.Log("group 1 " + group.GroupId);
         },

         onError: (code, desc) =>{
             Debug.Log("group 1 " + code + "aa " + desc);
         }
       );

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.MuteMembers("153813531033602", list, callback);

    }

    void GetGroupInfoAction()
    {

        ValueCallBack<Group> callback = new ValueCallBack<Group>(
         onSuccess: (group) => {
             Debug.Log("group 1 " + group.GroupId);
         },

         onError: (code, desc) => {
             Debug.Log("group 1 " + code + "aa " + desc);
         }
       );

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.UnMuteMembers("153813531033602", list, callback);

    }

    void LeaveGroupAction()
    {

        ValueCallBack<Group> callback = new ValueCallBack<Group>(
            onSuccess: (group) => {
                Debug.Log("group 2 " + group.Name);
                foreach (string s in group.MemberList)
                {
                    Debug.Log("ssss - " + s);
                }

            },
            onError: (code, desc) => {
                Debug.Log("group 2 " + code + "aa " + desc);
            });

        SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer("153813531033602", callback);
    }

    void JoinRoomAction()
    {
        CallBack callback = new CallBack(


            onSuccess: () => {
                Debug.Log("成功");
            },
            onError: (code, desc) => {
                Debug.Log("group 2 " + code + "aa " + desc);
            });

        List<string> list = new List<string>();
        list.Add("du003");

        SDKClient.Instance.GroupManager.AddMembers("153813531033602", list, callback);

    }

    void GetRoomInfoAction()
    {
        ValueCallBack<CursorResult<string>> callback = new ValueCallBack<CursorResult<string>>(
           onSuccess: (result) => {
               foreach (string s in result.Data) {
                   Debug.Log("ssss - " + s);
               }
           },
           onError: (code, desc) => {
               Debug.Log("group 2 " + code + "aa " + desc);
           });

        SDKClient.Instance.GroupManager.GetGroupMemberListFromServer("153813531033602", handle:callback);
    }

    void LeaveRoomAction()
    {
        ValueCallBack<List<string>> callback = new ValueCallBack<List<string>>(
           onSuccess: (result) => {
               foreach (string s in result)
               {
                   Debug.Log("ssss - " + s);
               }
           },
           onError: (code, desc) => {
               Debug.Log("group 2 " + code + "aa " + desc);
           });

        SDKClient.Instance.GroupManager.GetGroupMuteListFromServer("153813531033602", handle: callback);
        
    }
}
