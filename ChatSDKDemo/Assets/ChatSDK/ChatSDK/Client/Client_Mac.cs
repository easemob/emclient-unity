using UnityEngine;
using System;

namespace ChatSDK
{
    class Client_Mac : IClient
    {
        internal IntPtr client = IntPtr.Zero;
        private ConnectionHub connectionHub;
        private string currentUserName;

        public Client_Mac() {
            // start log service
            StartLog("/tmp/unmanaged_dll.log");
        }


        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            if (client != IntPtr.Zero)
            {
                ChatAPINative.Client_CreateAccount(client, username, password);
            }
            else
            {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> listeners = null)
        {
            ChatCallbackObject.GetInstance();
            connectionHub = new ConnectionHub(listeners);
            client = ChatAPINative.Client_InitWithOptions(options, connectionHub.Delegates());
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            if (client != IntPtr.Zero) {
                ChatAPINative.Client_Login(client, callBack, username, pwdOrToken, isToken);
                //TODO: set current user name only if login succeeds.
                currentUserName = username;
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            if (client != IntPtr.Zero) {
                ChatAPINative.Client_Logout(client, unbindDeviceToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override string CurrentUsername()
        {
            return currentUserName;
        }

        public override bool IsConnected()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsLoggedIn()
        {
            throw new System.NotImplementedException();
        }

        public override string AccessToken()
        {
            throw new System.NotImplementedException();
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
