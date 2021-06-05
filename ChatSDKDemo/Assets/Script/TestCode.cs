
using UnityEngine;
using ChatSDK;
using System.Collections.Generic;

public class TestCode : MonoBehaviour, IConnectionDelegate
{
    
    public void OnConnected()
    {
        print("已连接服务器");
    }

    public void OnDisconnected(int i)
    {
        print("链接服务器断开: " + i);
    }

    void Start()
    {

        var options = new Options("1110200629107815#tip");
        options.AcceptInvitationAlways = true;
        options.UsingHttpsOnly = true;
        options.DebugMode = true;
        SDKClient.Instance.InitWithOptions(options);
        SDKClient.Instance.AddConnectionDelegate(this);

        SDKClient.Instance.Login("du001", "1", handle: new CallBack(
            onSuccess: () => {
                print("登录成功");
            },
            onError:(error, desc) => {
                print("登录失败");
            }));

        //SDKClient.Instance.GroupManager.CheckIfInGroupWhiteList("asdasdasdasdsas", handle: new ValueCallBack<bool>(
        //        onSuccess:(bool b)=> {
        //            print("请求返回");
        //        }
        //    ));

        List<string> members = new List<string>();
        members.Add("du003");
        members.Add("du004");
        
        SDKClient.Instance.GroupManager.CreateGroup(
            "测试",
            new GroupOptions(GroupStyle.PrivateMemberCanInvite),
            "test",
            members,
            "reason",
            new ValueCallBack<Group>(
                onSuccess:(x)=> { print("groupName -- " + x.Name); },
                onError:(code, desc) => { print("创建失败: " + code + " : " + desc); }
        ));

        //SDKClient.Instance.GroupManager.GetPublicGroupsFromServer();
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
