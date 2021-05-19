
using UnityEngine;
using ChatSDK;
using System.Collections.Generic;

public class TestCode : MonoBehaviour
{
    
    void Start()
    {

        var options = new Options("easemob-demo#easeim");
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

        SDKClient.Instance.ContactManager.GetAllContactsFromServer(handle: new ValueCallBack<List<string>>(onError: (error, desc) => { }, onSuccess: (x) =>
        {
            foreach (string v in x) {
                print("获取的好友: " + v);
            }
        }));


        //SDKClient.Instance.ContactManager.AddUserToBlockList("du012", handle: new CallBack(onSuccess: () => {
        //    print("添加到黑名单");
        //}, onError:(code, desc)=> {
        //    print("添加失败 -- " + code);
        //}));

        SDKClient.Instance.ContactManager.RemoveUserFromBlockList("du012", handle: new CallBack(onSuccess: () => {
            print("移出黑名单");
        }, onError: (code, desc) => {
            print("移出失败 -- " + code);
        }));
     
        Debug.Log(options.ToString());
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
