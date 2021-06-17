using System;
using UnityEngine;

namespace ChatSDK {

    internal class ConnectionListener : MonoBehaviour
    {

        internal WeakDelegater<IConnectionDelegate> delegater;


        internal void OnConnected(string i)
        {
            if (delegater != null)
            {
                foreach (IConnectionDelegate connectionDelegate in delegater.List)
                {
                    connectionDelegate.OnConnected();
                }
            }
        }


        internal void OnDisconnected(string i)
        {
            if (delegater != null)
            {
                foreach (IConnectionDelegate connectionDelegate in delegater.List)
                {
                    connectionDelegate.OnDisconnected(int.Parse(i));
                }
            }
        }
    }
}