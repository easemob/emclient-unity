using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK {

    internal class ConnectionListener : MonoBehaviour
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
    }
}