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

    GroupInfo currGroup;
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


        //Debug.Log("token ---- " + SDKClient.Instance.AccessToken);
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

        ValueCallBack<bool> callBack = new ValueCallBack<bool>(
            onSuccess: (value) => {
                if (value) {
                    Debug.Log("白名单 -- " + (value ? "在白名单" : "不在白名单"));
                }
            }
            );
        SDKClient.Instance.GroupManager.CheckIfInGroupWhiteList("153875044696065", callBack);

    }

    void GetGroupInfoAction()
    {

        ValueCallBack<List<string>> callBack = new ValueCallBack<List<string>>(
                onSuccess:(list)=> {
                    foreach (string s in list) {
                        Debug.Log("白--- " + s);
                    }
                }
            );
        SDKClient.Instance.GroupManager.GetGroupWhiteListFromServer("153875044696065", callBack);
    }

    void LeaveGroupAction()
    {

        CallBack callBack = new CallBack();
        SDKClient.Instance.GroupManager.BlockGroup("153875044696065", callBack);
    }

    void JoinRoomAction()
    {
        CallBack callBack = new CallBack();
        SDKClient.Instance.GroupManager.UnBlockGroup("153875044696065", callBack);
    }

    void GetRoomInfoAction()
    {
        ValueCallBack<Group> callBack = new ValueCallBack<Group>(
                onSuccess:(d)=> {
                    Debug.Log("aaaaa -" + d.Options.Ext);
                }
            );
        SDKClient.Instance.GroupManager.UpdateGroupExt("153875044696065", ",,,,", callBack);
    }

    void LeaveRoomAction()
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

        SDKClient.Instance.GroupManager.GetGroupSpecificationFromServer("153875044696065", callback);

    }
}
