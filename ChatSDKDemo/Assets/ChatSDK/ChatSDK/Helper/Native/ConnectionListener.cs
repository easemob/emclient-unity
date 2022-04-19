using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK {

    internal sealed class ConnectionListener : MonoBehaviour
    {

        internal List<IConnectionDelegate> delegater;


        internal void OnConnected(string i)
        {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate connectionDelegate in delegater)
                    {
                        connectionDelegate.OnConnected();
                    }
                });
            }
        }


        internal void OnDisconnected(string i)
        {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate connectionDelegate in delegater)
                    {
                        connectionDelegate.OnDisconnected(int.Parse(i));
                    }
                });
            }
        }

        internal void OnTokenNoficationed(string i, string desc)
        {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate connectionDelegate in delegater)
                    {
                        connectionDelegate.OnTokenNotificationed(int.Parse(i), desc);
                    }
                });
            }
        }


        internal void OnTokenNoficationedFromMobile(string jsonString) {
            JSONNode jo = JSON.Parse(jsonString);
            string code = jo["code"].Value;
            string desc = jo["desc"].Value;
            OnTokenNoficationed(code, desc);

        }
    }
}