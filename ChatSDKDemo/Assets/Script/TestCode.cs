
using UnityEngine;
using ChatSDK;
using System.Collections.Generic;

public class TestCode : MonoBehaviour, IConnectionDelegate
{
    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected(int i)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {

        var options = new Options("1110200629107815#tip");
        options.AcceptInvitationAlways = true;
        options.UsingHttpsOnly = true;
        options.DebugModel = true;
        SDKClient.Instance.InitWithOptions(options);

        SDKClient.Instance.Login("du001", "1", handle: new CallBack(
            onSuccess: () => {
                print("登录成功");
            },
            onError:(error, desc) => {
                print("登录失败");
            }));

        SDKClient.Instance.AddConnectionDelegate(this);

        //SDKClient.Instance.GroupManager.CheckIfInGroupWhiteList("asdasdasdasdsas", handle: new ValueCallBack<bool>(
        //        onSuccess:(bool b)=> {
        //            print("请求返回");
        //        }
        //    ));

        List<string> members = new List<string>();
        members.Add("du003");
        members.Add("du004");
        
        SDKClient.Instance.GroupManager.CreateGroup("测试", new GroupOptions(GroupStyle.PrivateMemberCanInvite), "test", members, handle: new ValueCallBack<Group>( onSuccess:(x)=> {
            print("groupName -- " + x.Name);
        }));

        //SDKClient.Instance.GroupManager.GetPublicGroupsFromServer();
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
