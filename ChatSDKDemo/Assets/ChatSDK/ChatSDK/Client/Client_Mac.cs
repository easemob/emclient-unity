using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {
        private IntPtr _client = IntPtr.Zero;

        public Client_Mac() {
            // do nothing
        }


        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            if(_client != IntPtr.Zero) {
                ChatAPINative.Client_CreateAccount(_client, username, password);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null)
        {
            ChatCallbackObject.GetInstance();
            _client = ChatAPINative.Client_InitWithOptions(options);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            if(_client != IntPtr.Zero) {
                ChatAPINative.Client_Login(_client, username, pwdOrToken, isToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            if (_client != IntPtr.Zero) {
                ChatAPINative.Client_Logout(_client, unbindDeviceToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }


    }

}
