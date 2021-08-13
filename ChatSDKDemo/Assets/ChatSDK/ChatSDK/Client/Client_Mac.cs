using UnityEngine;
using System;
//to-do: just for testing
using System.Runtime.InteropServices;

namespace ChatSDK
{
    class Client_Mac : IClient
    {
        private static ConnectionHub connectionHub;

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


        //to-do: just for testing
        public string getMemory(object o)
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);
            IntPtr addr = GCHandle.ToIntPtr(h);
            return "0x" + addr.ToString("X");

            //GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
            //IntPtr addr = h.AddrOfPinnedObject();
            //return "0x" + addr.ToString("X");
            //h.AddrOfPinnedObject().ToString();
            //return "0x" + h.AddrOfPinnedObject().ToString();

        }

        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> listeners = null)
        {
            ChatCallbackObject.GetInstance();
            if(connectionHub == null)
            {
                connectionHub = new ConnectionHub(this, listeners); //init only once
                connectionHub.ts = GetTimeStamp();
            }
            Debug.Log($"connectionHub  ts is {connectionHub.ts}");
            
            // keep only 1 client left
            if(client != IntPtr.Zero)
            {
                //stop log service
                StopLog();
            }
            StartLog("/tmp/unmanaged_dll.log");
            client = ChatAPINative.Client_InitWithOptions(options, connectionHub.OnConnected, connectionHub.OnDisconnected, connectionHub.OnPong);
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
                OnLoginError = (int code, string desc) => callback?.Error(code, desc);

                ChatAPINative.Client_Login(client, OnLoginSuccess, OnLoginError, username, pwdOrToken, isToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            Debug.Log($"in logout, step1, connectionHub  ts is {connectionHub.ts}");
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
            Debug.Log($"in logout, step2, connectionHub  ts is {connectionHub.ts}");
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
