using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSDK;
using System;

public class TestCode : MonoBehaviour, IContactManagerDelegate
{
    public void OnContactAdded(string username)
    {
        throw new NotImplementedException();
    }

    public void OnContactDeleted(string username)
    {
        throw new NotImplementedException();
    }

    public void OnContactInvited(string username, string reason)
    {
        throw new NotImplementedException();
    }

    public void OnFriendRequestAccepted(string username)
    {
        throw new NotImplementedException();
    }

    public void OnFriendRequestDeclined(string username)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {

        var options = new Options("easemob-demo#chatdemou");
        SDKClient.Instance.InitWithOptions(options);
        SDKClient.Instance.Login("du001", "1");
        SDKClient.Instance.ContactManager.GetAllContactsFromServer(callBack: new ValueCallBack<List<string>>(onError: (error, desc) => { }, onSuccess: (x) => { Debug.Log(x); }));
        SDKClient.Instance.ContactManager.AddContact("du001", callBack: new CallBack(onSuccess: () => { }, onError: (error, desc) => { }));
        SDKClient.Instance.ContactManager.AddContactManagerDelegate(this);
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
