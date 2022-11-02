using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;
#if _WIN32
#else
using UnityEngine;
using UnityEditor;
#endif

namespace AgoraChat
{
#if _WIN32
    internal class IClient
#else
    internal class IClient : MonoBehaviour
#endif
    {
        public ChatManager chatManager;
        public ContactManager contactManager;
        public GroupManager groupManager;
        public RoomManager roomManager;
        public PresenceManager presenceManager;
        public ChatThreadManager chatThreadManager;
        public UserInfoManager userInfoManager;
        public ConversationManager conversationManager;
        public MessageManager messageManager;

        internal NativeListener nativeListener = new NativeListener();
        private CallbackManager callbackManager;

        internal string NAME_CLIENT = "Client";
        internal List<IConnectionDelegate> delegater_connection;
        internal List<IMultiDeviceDelegate> delegater_multidevice;

        internal IClient() 
        {
            // 将 listener 和 native 挂钩
            nativeListener.AddNaitveListener();
          
            chatManager = new ChatManager(nativeListener);
            contactManager = new ContactManager(nativeListener);
            groupManager = new GroupManager(nativeListener);
            roomManager = new RoomManager(nativeListener);
            presenceManager = new PresenceManager(nativeListener);
            chatThreadManager = new ChatThreadManager(nativeListener);
            userInfoManager = new UserInfoManager();
            conversationManager = new ConversationManager();
            messageManager = new MessageManager();

            callbackManager = nativeListener.callbackManager;
            delegater_connection = new List<IConnectionDelegate>();
            delegater_multidevice = new List<IMultiDeviceDelegate>();

            nativeListener.ConnectionEvent += NativeEventHandle_Connection;
            nativeListener.MultiDeviceEvent += NativeEventHandle_MultiDevice;
        }

        internal void InitWithOptions(Options options)
        {
            JSONObject jo = options.ToJsonObject();
            CWrapperNative.NativeCall(NAME_CLIENT, "initWithOptions", jo, null);
        }

        internal string CurrentUsername()
        {
            string json = CWrapperNative.NativeGet(NAME_CLIENT, "currentUsername", null, null);
            JSONObject jo = JSON.Parse(json).AsObject;
            return jo["getCurrentUsername"].Value;
        }

        internal bool IsLoggedIn()
        {
            string json = CWrapperNative.NativeGet(NAME_CLIENT, "isLoggedIn", null, null);
            JSONObject jo = JSON.Parse(json).AsObject;
            return jo["isLoggedIn"].AsBool;
        }

        internal bool IsConnected()
        {
            string json = CWrapperNative.NativeGet(NAME_CLIENT, "isConnected", null, null);
            JSONObject jo = JSON.Parse(json).AsObject;
            return jo["isConnected"].AsBool;
        }

        internal string AccessToken()
        {
            string json = CWrapperNative.NativeGet(NAME_CLIENT, "accessToken", null, null);
            JSONObject jo = JSON.Parse(json).AsObject;
            return jo["accessToken"].Value;
        }

        internal void CreateAccount(string username, string password, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username ?? "");
            jo_param.Add("password", password ?? "");

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CLIENT, "createAccount", jo_param, (null != callback) ? callback.callbackId : "");
        }

        internal void Login(string username, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username ?? "");
            jo_param.Add("pwdOrToken", pwdOrToken ?? "");
            jo_param.Add("isToken", isToken);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CLIENT, "login", jo_param, (null != callback) ? callback.callbackId : "");
        }

        internal void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("unbindDeviceToken", unbindDeviceToken);

            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CLIENT, "logout", jo_param, (null != callback) ? callback.callbackId : "");
        }

        internal void LoginWithAgoraToken(string username, string token, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username);
            jo_param.Add("token", token);
            callbackManager.AddCallbackAction<Object>(callback, null);
            CWrapperNative.NativeCall(NAME_CLIENT, "loginWithAgoraToken", jo_param, (null != callback) ? callback.callbackId : "");
        }

        internal void RenewAgoraToken(string token)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("token", token);
            CWrapperNative.NativeCall(NAME_CLIENT, "renewToken", jo_param, "");
        }

        internal void CleanUp() 
        {
            nativeListener.RemoveNativeListener();
        }

        internal void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!delegater_connection.Contains(connectionDelegate))
            {
                delegater_connection.Add(connectionDelegate);
            }
        }

        internal void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (delegater_connection.Contains(connectionDelegate))
            {
                delegater_connection.Remove(connectionDelegate);
            }
        }

        internal void AddMultiDeviceDelegate(IMultiDeviceDelegate mutideviceDelegate)
        {
            if (!delegater_multidevice.Contains(mutideviceDelegate))
            {
                delegater_multidevice.Add(mutideviceDelegate);
            }
        }

        internal void DeleteMultiDeviceDelegate(IMultiDeviceDelegate mutideviceDelegate)
        {
            if (delegater_multidevice.Contains(mutideviceDelegate))
            {
                delegater_multidevice.Remove(mutideviceDelegate);
            }
        }

        internal void NativeEventHandle_Connection(string method, JSONNode jsonNode)
        {
            if (delegater_connection.Count == 0 || null == method || method.Length == 0) return;

            if (method.CompareTo("OnConnected") == 0)
            {
                foreach (IConnectionDelegate it in delegater_connection)
                {
                    it.OnConnected();
                }
            }
            else if (method.CompareTo("OnDisconnected") == 0)
            {
                foreach (IConnectionDelegate it in delegater_connection)
                {
                    it.OnDisconnected(int.Parse(jsonNode["value"]));
                }
            }
            else if (method.CompareTo("OnTokenExpired") == 0)
            {
                foreach (IConnectionDelegate it in delegater_connection)
                {
                    it.OnTokenExpired();
                }
            }
            else if (method.CompareTo("OnTokenExpired") == 0)
            {
                foreach (IConnectionDelegate it in delegater_connection)
                {
                    it.OnTokenExpired();
                }
            }
        }

        internal void NativeEventHandle_MultiDevice(string method, JSONNode jsonNode)
        {
            if (delegater_multidevice.Count == 0 || null == method || method.Length == 0) return;

            if (method.CompareTo("OnContactMultiDevicesEvent") == 0)
            {
                foreach (IMultiDeviceDelegate it in delegater_multidevice)
                {
                    string operationEvent = jsonNode["event"];
                    MultiDevicesOperation operation = (MultiDevicesOperation)int.Parse(operationEvent);
                    string username = jsonNode["username"];
                    string ext = jsonNode["ext"];
                    it.OnContactMultiDevicesEvent(operation, username, ext);
                }
            }
            else if (method.CompareTo("OnGroupMultiDevicesEvent") == 0)
            {
                foreach (IMultiDeviceDelegate it in delegater_multidevice)
                {
                    string operationEvent = jsonNode["event"];
                    MultiDevicesOperation operation = (MultiDevicesOperation)int.Parse(operationEvent);
                    string groupId = jsonNode["groupId"];
                    List<string> usernames = List.StringListFromJsonArray(jsonNode["usernames"]);
                    it.OnGroupMultiDevicesEvent(operation, groupId, usernames);
                }
            }
            else if (method.CompareTo("UndisturbMultiDevicesEvent") == 0)
            {
                foreach (IMultiDeviceDelegate it in delegater_multidevice)
                {
                    it.UndisturbMultiDevicesEvent(jsonNode["value"]);
                }
            }
            //TODO: need to add OnThreadMultiDevicesEvent?
        }

#if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            EditorApplication.wantsToQuit -= Quit;
            EditorApplication.wantsToQuit += Quit;

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static bool Quit()
        {
            if (SDKClient.Instance.IsLoggedIn)
            {
                SDKClient.Instance.Logout(false);
            }
            if (CallbackManager._getInstance != null)
            {
                CallbackManager.Instance().ClearResource();
            }
            Debug.Log("Quit...");
            return true;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            switch (stateChange)
            {
                case (PlayModeStateChange.EnteredPlayMode):
                    {
                        EditorApplication.LockReloadAssemblies();
                        Debug.Log("Assembly Reload locked as entering play mode");
                        break;
                    }
                case (PlayModeStateChange.ExitingPlayMode):
                    {
                        Debug.Log("Assembly Reload unlocked as exiting play mode");
                        EditorApplication.UnlockReloadAssemblies();
                        break;
                    }
            }
        }
#endif

        private void OnApplicationQuit()
        {
            if (SDKClient.Instance.IsLoggedIn)
            {
                SDKClient.Instance.Logout(false);
            }
            ClearResource();
        }

        internal void ClearResource()
        {
            delegater_connection.Clear();
            delegater_multidevice.Clear();

            //TODO
            /*
            IClient.Instance.ContactManager().ClearDelegates();
            IClient.Instance.ChatManager().ClearDelegates();
            IClient.Instance.GroupManager().ClearDelegates();
            IClient.Instance.RoomManager().ClearDelegates();
            IClient.Instance.PresenceManager().ClearDelegates();
            IClient.Instance.ThreadManager().ClearDelegates();
            CallbackManager.Instance().CleanAllCallback();
            */

            CWrapperNative.NativeCall(NAME_CLIENT, "clearResource", null, "");
        }
    }
}
