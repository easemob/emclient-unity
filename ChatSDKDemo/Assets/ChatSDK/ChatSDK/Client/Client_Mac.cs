using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {
        private IntPtr _client = IntPtr.Zero;

        public Client_Mac() {
            // start log service
            StartLog("/tmp/unmanaged_dll.log");
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
                ChatAPINative.Client_Login(_client, callBack, username, pwdOrToken, isToken);
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

        public override void StartLog(string logFilePath)
        {
            ChatAPINative.Client_StartLog(logFilePath);
        }

        public override void StopLog()
        {
            ChatAPINative.Client_StopLog();
        }
    }

}
