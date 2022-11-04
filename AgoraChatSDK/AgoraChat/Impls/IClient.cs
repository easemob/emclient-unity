using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal class IClient : BaseManager
    {
        internal ChatManager chatManager;
        internal ContactManager contactManager;
        internal GroupManager groupManager;
        internal RoomManager roomManager;
        internal PresenceManager presenceManager;
        internal ChatThreadManager chatThreadManager;
        internal UserInfoManager userInfoManager;
        internal ConversationManager conversationManager;
        internal MessageManager messageManager;

        internal NativeListener nativeListener;


        internal List<IConnectionDelegate> delegater_connection;
        internal List<IMultiDeviceDelegate> delegater_multidevice;


        internal IClient(NativeListener listener) : base(listener, SDKMethod.client)
        {
            // 将 listener 和 native 挂钩
            nativeListener = listener;
            nativeListener.AddNaitveListener();
            callbackManager = nativeListener.callbackManager;

            delegater_connection = new List<IConnectionDelegate>();
            delegater_multidevice = new List<IMultiDeviceDelegate>();

            nativeListener.ConnectionEvent += NativeEventHandle_Connection;
            nativeListener.MultiDeviceEvent += NativeEventHandle_MultiDevice;
        }

        ~IClient()
        {
            nativeListener.RemoveNativeListener();
            nativeListener = null;
        }

        internal void InitWithOptions(Options options)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("options", options.ToJsonObject());
            NativeCall(SDKMethod.init, jo_param,
                new CallBack(
                    onSuccess: () =>
                    {
                        SetupManagers();
                    }
                )
            );
        }

        internal string CurrentUsername()
        {

            JSONNode jsonNode = NativeGet(SDKMethod.getCurrentUser).GetReturnJsonNode();
            return jsonNode.IsString ? jsonNode.Value : null;
        }

        internal bool IsLoggedIn()
        {
            JSONNode jsonNode = NativeGet(SDKMethod.isLoggedInBefore).GetReturnJsonNode();
            return jsonNode.IsBoolean ? jsonNode.AsBool : false;
        }

        internal bool IsConnected()
        {
            JSONNode jsonNode = NativeGet(SDKMethod.isConnected).GetReturnJsonNode();
            return jsonNode.IsBoolean ? jsonNode.AsBool : false;
        }

        internal string AccessToken()
        {
            JSONNode jsonNode = NativeGet(SDKMethod.getToken).GetReturnJsonNode();
            return jsonNode.IsString ? jsonNode.Value : null;
        }

        internal void CreateAccount(string userId, string password, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("userId", userId ?? "");
            jo_param.Add("password", password ?? "");

            NativeCall(SDKMethod.createAccount, jo_param, callback);
        }

        internal void Login(string userId, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("userId", userId ?? "");
            jo_param.Add("pwdOrToken", pwdOrToken ?? "");
            jo_param.Add("isToken", isToken);

            NativeCall(SDKMethod.login, jo_param, callback);
        }

        internal void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("unbindDeviceToken", unbindDeviceToken);
            NativeCall(SDKMethod.logout, jo_param, callback);
        }

        internal void LoginWithAgoraToken(string userId, string token, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("userId", userId);
            jo_param.Add("token", token);
            NativeCall(SDKMethod.loginWithAgoraToken, jo_param, callback);
        }

        internal void RenewAgoraToken(string token)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("token", token);
            NativeCall(SDKMethod.renewToken, jo_param);
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


        private void SetupManagers()
        {
            chatManager = new ChatManager(nativeListener);
            contactManager = new ContactManager(nativeListener);
            groupManager = new GroupManager(nativeListener);
            roomManager = new RoomManager(nativeListener);
            presenceManager = new PresenceManager(nativeListener);
            chatThreadManager = new ChatThreadManager(nativeListener);
            userInfoManager = new UserInfoManager();
            conversationManager = new ConversationManager();
            messageManager = new MessageManager();
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
                    //it.OnContactMultiDevicesEvent(operation, username, ext);
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
                    //it.OnGroupMultiDevicesEvent(operation, groupId, usernames);
                }
            }
            else if (method.CompareTo("UndisturbMultiDevicesEvent") == 0)
            {
                foreach (IMultiDeviceDelegate it in delegater_multidevice)
                {
                    //it.UndisturbMultiDevicesEvent(jsonNode["value"]);
                }
            }
            //TODO: need to add OnThreadMultiDevicesEvent?
        }

        private void OnApplicationQuit()
        {
            if (SDKClient.Instance.IsLoggedIn)
            {
                SDKClient.Instance.Logout(false);
            }
            ClearResource();
        }

        internal void CleanUp()
        {
            nativeListener.RemoveNativeListener();
        }

        internal void ClearResource()
        {
            delegater_connection.Clear();
            delegater_multidevice.Clear();
            CWrapperNative.NativeCall(SDKMethod.client, "clearResource", null, "");
        }
    }
}
