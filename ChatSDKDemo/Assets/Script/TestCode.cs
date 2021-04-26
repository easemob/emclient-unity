using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChatSDK;
using System;

public class TestCode : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        var options = new Options("easemob-demo#chatdemouo");
        SDKClient.Instance.InitWithOptions(options);
        SDKClient.Instance.ContactManager.AddContact("du001", callBack: new SDKCallBack(
                onSuccess: () => { },
                onProgress: (s) => { },
                onError: (e, s) => { }
            )
        );
    }
    // Update is called once per frame
    void Update()
    {

    }
}
