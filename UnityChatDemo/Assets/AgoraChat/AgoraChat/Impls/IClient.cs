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

            SetupManagers();
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

        internal void GetLoggedInDevicesFromServer(string username, string password, ValueCallBack<List<DeviceInfo>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username);
            jo_param.Add("password", password);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<DeviceInfo>(jsonNode);
            };

            NativeCall<List<DeviceInfo>>(SDKMethod.getLoggedInDevicesFromServer, jo_param, callback, process);
        }

        internal void KickDevice(string username, string password, string resource, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username);
            jo_param.Add("password", password);
            jo_param.Add("resource", resource);
            NativeCall(SDKMethod.kickDevice, jo_param, callback);
        }

        internal void kickAllDevices(string username, string password, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.Add("username", username);
            jo_param.Add("password", password);
            NativeCall(SDKMethod.kickDevice, jo_param, callback);
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
            userInfoManager = new UserInfoManager(nativeListener);
            conversationManager = new ConversationManager(nativeListener);
            messageManager = new MessageManager(nativeListener);
        }

        internal void NativeEventHandle_Connection(string method, JSONNode jsonNode)
        {
            if (delegater_connection.Count == 0) return;

            foreach (IConnectionDelegate it in delegater_connection)
            {
                switch (method)
                {
                    case SDKMethod.onConnected:
                        it.OnConnected();
                        break;
                    case SDKMethod.onDisconnected:
                        // TODO: dujiepeng 需要定义reason?
                        break;
                    case SDKMethod.onTokenExpired:
                        it.OnTokenExpired();
                        break;
                    case SDKMethod.onTokenWillExpire:
                        it.OnTokenWillExpire();
                        break;
                    default:
                        break;
                }
            }

        }

        internal void NativeEventHandle_MultiDevice(string method, JSONNode jsonNode)
        {
            if (delegater_multidevice.Count == 0) return;

            MultiDevicesOperation operation = jsonNode["operation"].AsInt.ToMultiDevicesOperation();
            string target = jsonNode["target"];
            string ext = jsonNode["ext"];
            List<string> userIds = List.StringListFromJsonArray(jsonNode["userIds"]);

            foreach (IMultiDeviceDelegate it in delegater_multidevice)
            {
                switch (method)
                {
                    case SDKMethod.onContactMultiDevicesEvent:
                        it.OnContactMultiDevicesEvent(operation, target, ext);
                        break;
                    case SDKMethod.onGroupMultiDevicesEvent:
                        it.OnGroupMultiDevicesEvent(operation, target, userIds);
                        break;
                    case SDKMethod.onUnDisturbMultiDevicesEvent:
                        // TODO: dujiepeng
                        //it.OnUndisturbMultiDevicesEvent();
                        break;
                    case SDKMethod.onThreadMultiDevicesEvent:
                        it.OnThreadMultiDevicesEvent(operation, target, userIds);
                        break;
                    default:
                        break;
                }
            }
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
