using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {
        private IntPtr _client = IntPtr.Zero;

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            if(_client != IntPtr.Zero)
                ChatAPINative.Client_CreateAccount(username, password);
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null)
        {
            ChatCallbackObject.GetInstance();
            _client = ChatAPINative.Client_InitWithOptions(options);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            if(_client != IntPtr.Zero)
                ChatAPINative.Client_Login(username, pwdOrToken, isToken);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            if (_client != IntPtr.Zero)
                ChatAPINative.Client_Logout(unbindDeviceToken);
        }


    }

}
