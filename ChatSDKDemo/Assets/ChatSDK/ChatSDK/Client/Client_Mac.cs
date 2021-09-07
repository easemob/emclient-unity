using UnityEngine;
using System;
//to-do: just for testing
using System.Runtime.InteropServices;

namespace ChatSDK
{
    class Client_Mac : IClient
    {
        private ConnectionHub connectionHub;

        internal IntPtr client = IntPtr.Zero;
        private string currentUserName;
        private bool isLoggedIn;
        private bool isConnected;

        //events
        public event Action OnLoginSuccess;
        public event OnError OnLoginError;
        public event Action OnRegistrationSuccess;
        public event OnError OnRegistrationError;
        public event Action OnLogoutSuccess;

        public Client_Mac() {
            // start log service
            StartLog("/tmp/unmanaged_dll.log");
        }

        public override void CreateAccount(string username, string password, CallBack callback = null)
        {
            if (client != IntPtr.Zero)
            {
                OnRegistrationSuccess = () => callback?.Success();
                OnRegistrationError = (int code, string desc) => callback?.Error(code, desc);
                ChatAPINative.Client_CreateAccount(client, OnRegistrationSuccess, OnRegistrationError, username, password);
            }
            else
            {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void InitWithOptions(Options options)
        {
            ChatCallbackObject.GetInstance();
            if(connectionHub == null)
            {
                connectionHub = new ConnectionHub(this); //init only once
            }
            
            
            // keep only 1 client left
            if(client != IntPtr.Zero)
            {
                //stop log service
                StopLog();
            }
            StartLog("/tmp/unmanaged_dll.log");
            client = ChatAPINative.Client_InitWithOptions(options, connectionHub.OnConnected, connectionHub.OnDisconnected, connectionHub.OnPong);
            Debug.Log($"InitWithOptions completed.");
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            if (client != IntPtr.Zero) {
                OnLoginSuccess = () =>
                {
                    currentUserName = username;
                    isLoggedIn = true;
                    callback?.Success();
                };
                OnLoginError = (int code, string desc) =>
                {
                    //200=code, user already login ,save the username
                    if (200 == code) currentUserName = username;
                    callback?.Error(code, desc);
                };
                ChatAPINative.Client_Login(client, OnLoginSuccess, OnLoginError, username, pwdOrToken, isToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            if (client != IntPtr.Zero)
            {
                OnLogoutSuccess = () =>
                {
                    currentUserName = "";
                    isLoggedIn = false;
                    callback?.Success();
                };
                ChatAPINative.Client_Logout(client, OnLogoutSuccess, unbindDeviceToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
            Debug.Log($"logout complete.");
        }

        public override string CurrentUsername()
        {
            return currentUserName;
        }

        public override bool IsConnected
        {
            get => isConnected;
            internal set => isConnected = value;
        }

        public override bool IsLoggedIn()
        {
            return isLoggedIn;
        }

        public override string AccessToken()
        {
            string result = null;
            ChatAPINative.Client_LoginToken(client,
                (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if(DataType.String == dType && 1 == dSize)
                    {
                        result = Marshal.PtrToStringAnsi(data[0]);
                    }
                    else
                    {
                        Debug.Log("Cannot get login token.");
                    }
                }
                );
            return result;
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
