using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    public delegate void OnDisconnected(int info);

    public class ConnectionDelegate : CallBack, IConnectionDelegate
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct FPtrs
        {
            public Action Connected;
            public OnDisconnected Disconnected;
        };

        internal FPtrs _FPtrs;
        internal WeakDelegater<IConnectionDelegate> _delegater;

        public ConnectionDelegate(WeakDelegater<IConnectionDelegate> delegater)
        {
            _delegater = delegater;
            _FPtrs.Connected = OnConnected;
            _FPtrs.Disconnected = OnDisconnected;
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().currentId, this);
        }

        public void OnConnected()
        {
            //invoke each delegaters in list
            Debug.Log("ConnectionDelegateHub.OnConnected() invoked!");
            foreach (IConnectionDelegate dlg in _delegater.List)
            {
                dlg.OnConnected();
            }
        }

        public void OnDisconnected(int info)
        {
            //invoke each delegaters in list
            Debug.Log("ConnectionDelegateHub.OnDisconnected() invoked with info=" + info + "!");
            foreach (IConnectionDelegate dlg in _delegater.List)
            {
                dlg.OnDisconnected(info);
            }
        }

        public FPtrs FuncPointers()
        {
            return _FPtrs;
        }
    }
}