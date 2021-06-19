using UnityEngine;
using System;

namespace ChatSDK
{
    class Client_Mac : IClient
    {
        internal IntPtr client = IntPtr.Zero;
        private ConnectionHub connectionHub;
        private string currentUserName;
        private bool isLoggedIn;
        private bool isConnected;

        public Client_Mac() {
            // start log service
            StartLog("/tmp/unmanaged_dll.log");
        }

        public override void CreateAccount(string username, string password, CallBack callback = null)
        {
            if (client != IntPtr.Zero)
            {
                ChatAPINative.Client_CreateAccount(client, callback, username, password);
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
            // keep only 1 client left
            if(client != IntPtr.Zero)
            {
                //stop log service
                StopLog();
                ChatAPINative.Client_Release(client);
            }
            StartLog("/tmp/unmanaged_dll.log");
            client = ChatAPINative.Client_InitWithOptions(options, connectionHub.Delegates());
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            if (client != IntPtr.Zero) {
                CallBack callbackDispatcher = new CallBack(onSuccess: () =>
                                                            {
                                                                currentUserName = username;
                                                                isLoggedIn = true;
                                                                callback?.Success();
                                                            },
                                                            onProgress: (int progress) => callback?.Progress(progress),
                                                            onError: (int error, string description) => callback?.Error(error, description));
                Debug.Log($"Login callback dispatcher ${callbackDispatcher.callbackId}");
                ChatAPINative.Client_Login(client, callbackDispatcher, username, pwdOrToken, isToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            if (client != IntPtr.Zero)
            {
                CallBack callbackDispatcher = new CallBack(onSuccess: () =>
                                                            {
                                                                currentUserName = "";
                                                                isLoggedIn = false;
                                                                callback?.Success();
                                                            });
                Debug.Log($"Logout callback dispatcher ${callbackDispatcher.callbackId}");
                ChatAPINative.Client_Logout(client, callbackDispatcher, unbindDeviceToken);
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
            return isConnected;
        }

        public override bool IsLoggedIn()
        {
            return isLoggedIn;
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
