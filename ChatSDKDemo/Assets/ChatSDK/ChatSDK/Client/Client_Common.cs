#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif
using System;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    internal sealed class Client_Common : IClient
    {
        private ConnectionHub connectionHub;
        private MultiDevicesHub multiDeviceHub;

        internal IntPtr client = IntPtr.Zero;
        private string currentUserName;

        //events
        public event OnSuccess OnLoginSuccess;
        public event OnErrorV2 OnLoginError;
        public event OnSuccess OnRegistrationSuccess;
        public event OnErrorV2 OnRegistrationError;
        public event OnSuccess OnLogoutSuccess;

        public Client_Common() {
            // start log service
            StartLog("unmanaged_dll.log");
        }

        public override void CreateAccount(string username, string password, CallBack callback = null)
        {
            if (null == username || username.Length == 0 || null == password || password.Length == 0)
            {
                Debug.LogError("Username or passwd cannot be empty!");
                return;
            }

            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;
            if (client != IntPtr.Zero)
            {
                OnRegistrationSuccess = (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                };
                OnRegistrationError = (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                };
                ChatAPINative.Client_CreateAccount(client, callbackId, OnRegistrationSuccess, OnRegistrationError, username, password);
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

            if(multiDeviceHub == null)
            {
                multiDeviceHub = new MultiDevicesHub(); //init only once
            }
            
            // keep only 1 client left
            if(client != IntPtr.Zero)
            {
                //stop log service
                StopLog();
                StartLog("unmanaged_dll.log");
            }
            
            client = ChatAPINative.Client_InitWithOptions(options, connectionHub.OnConnected, connectionHub.OnDisconnected, connectionHub.OnPong, connectionHub.OnTokenNotification);
            Debug.Log($"InitWithOptions completed.");

            ChatAPINative.Client_AddMultiDeviceListener(multiDeviceHub.onContactMultiDevicesEvent, multiDeviceHub.onGroupMultiDevicesEvent, multiDeviceHub.undisturbMultiDevicesEvent, multiDeviceHub.onThreadMultiDevicesEvent);
            Debug.Log("AddMultiDeviceListener completed.");
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            if(null == username || username.Length == 0 || null == pwdOrToken || pwdOrToken.Length == 0)
            {
                Debug.LogError("Username or passwd cannot be empty!");
                return;
            }
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;
            if (client != IntPtr.Zero) {

                currentUserName = username;

                OnLoginSuccess = (int cbId) =>
                {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                };
                OnLoginError = (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                };
                ChatAPINative.Client_Login(client, callbackId, OnLoginSuccess, OnLoginError, username, pwdOrToken, isToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;
            if (client != IntPtr.Zero)
            {
                OnLogoutSuccess = (int cbId) =>
                {
                    currentUserName = "";
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                };
                ChatAPINative.Client_Logout(client, callbackId, OnLogoutSuccess, unbindDeviceToken);
            } else {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
            Debug.Log($"logout complete.");
        }

        public override string CurrentUsername()
        {
            return currentUserName;
        }

        public override bool IsConnected()
        {
            return ChatAPINative.Client_isConnected(client);
        }

        public override bool IsLoggedIn()
        {
            return ChatAPINative.Client_isLoggedIn(client);
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

        public override void LoginWithAgoraToken(string username, string token, CallBack callback = null)
        {
            if (null == username || username.Length == 0 || null == token || token.Length == 0)
            {
                Debug.LogError("Username or token cannot be empty!");
                return;
            }

            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;
            if (client != IntPtr.Zero)
            {

                currentUserName = username;

                OnLoginSuccess = (int cbId) =>
                {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                };
                OnLoginError = (int code, string desc, int cbId) =>
                {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                };
                ChatAPINative.Client_LoginWithAgoraToken(client, callbackId, OnLoginSuccess, OnLoginError, username, token);
            }
            else
            {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        public override void RenewAgoraToken(string token)
        {
            if (client != IntPtr.Zero)
            {
                ChatAPINative.Client_RenewAgoraToken(client, token);
            }
            else
            {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }

        /*
        public override void AutoLogin(CallBack callback = null)
        {
            int callbackId = (null != callback) ? int.Parse(callback.callbackId) : -1;
            if (client != IntPtr.Zero)
            {
                ChatAPINative.Client_AutoLogin(client, callbackId,
                onSuccess: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                {
                    if (DataType.String == dType && 1 == dSize)
                    {
                        var username = Marshal.PtrToStringAnsi(data[0]);
                        isLoggedIn = true;
                        currentUserName = username;
                        ChatCallbackObject.CallBackOnSuccess(cbId);
                    }
                    else
                    {
                        Debug.LogError($"AutoLogin user information expected.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
            }
            else
            {
                Debug.LogError("::InitWithOptions() not called yet.");
            }
        }
        */

        internal void StartLog(string logFilePath)
        {
            ChatAPINative.Client_StartLog(logFilePath);
        }

        internal void StopLog()
        {
            ChatAPINative.Client_StopLog();
        }

        public override void ClearResource()
        {
            ChatAPINative.Client_ClearResource(client);
        }

    }

}
