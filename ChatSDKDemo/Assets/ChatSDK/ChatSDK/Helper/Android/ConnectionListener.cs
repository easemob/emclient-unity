using System;
using UnityEngine;

namespace ChatSDK {

    internal class ConnectionListener : MonoBehaviour
    {

        internal WeakDelegater<IConnectionDelegate> connectionDelegater;


        public void OnConnected(string i)
        {
            if (connectionDelegater != null)
            {
                foreach (IConnectionDelegate connectionDelegate in connectionDelegater.List)
                {
                    connectionDelegate.OnConnected();
                }
            }
        }


        public void OnDisconnected(string i)
        {
            if (connectionDelegater != null)
            {
                foreach (IConnectionDelegate connectionDelegate in connectionDelegater.List)
                {
                    connectionDelegate.OnDisconnected(int.Parse(i));
                }
            }
        }
    }
}