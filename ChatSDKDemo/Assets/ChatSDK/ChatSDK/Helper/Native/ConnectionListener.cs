using System;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK {

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
    internal sealed class ConnectionListener : MonoBehaviour
#else
    internal sealed class ConnectionListener
#endif
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

        internal void OnTokenExpired(string i) {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate connectionDelegate in delegater)
                    {
                        connectionDelegate.OnTokenExpired();
                    }
                });
            }
        }

        internal void OnTokenWillExpire(string i) {
            if (delegater != null)
            {
                ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                    foreach (IConnectionDelegate connectionDelegate in delegater)
                    {
                        connectionDelegate.OnTokenWillExpire();
                    }
                });
            }
        }

    }
}