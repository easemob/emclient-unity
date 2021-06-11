using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    public delegate void OnDisconnected(int info);

    public class ConnectionHub : CallBack, IConnectionDelegate
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Delegate
        {
            public Action Connected;
            public OnDisconnected Disconnected;
        };

        private Delegate @delegate;
        private WeakDelegater<IConnectionDelegate> listeners;

        public ConnectionHub(WeakDelegater<IConnectionDelegate> _listeners)
        {
            listeners = _listeners;
            (@delegate.Connected, @delegate.Disconnected) = (OnConnected, OnDisconnected);
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().currentId, this);
        }

        public void OnConnected()
        {
            //invoke each delegaters in list
            Debug.Log("ConnectionHub.OnConnected() invoked!");
            foreach (IConnectionDelegate listener in listeners.List)
            {
                listener.OnConnected();
            }
        }

        public void OnDisconnected(int info)
        {
            //invoke each delegaters in list
            Debug.Log($"ConnectionHub.OnDisconnected() invoked with info={info}!");
            foreach (IConnectionDelegate listener in listeners.List)
            {
                listener.OnDisconnected(info);
            }
        }

        public Delegate Delegates()
        {
            return @delegate;
        }
    }
}