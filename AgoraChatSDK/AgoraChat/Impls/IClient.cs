using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal class IClient : BaseManager
    {
        public static bool IsInit { get; internal set; }

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
            // make relation between listener and native.
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

        internal int InitWithOptions(Options options)
        {
            Tools.SetDebugMode(options.DebugMode);
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("options", options.ToJsonObject());
            JSONNode jsonNode = NativeGet(SDKMethod.init, jo_param).GetReturnJsonNode();
            int ret = jsonNode.IsNumber ? jsonNode.AsInt : 0;
            SetupManagers();
            if (0 == ret) IsInit = true;
            return ret;
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
            jo_param.AddWithoutNull("userId", userId ?? "");
            jo_param.AddWithoutNull("password", password ?? "");

            NativeCall(SDKMethod.createAccount, jo_param, callback);
        }

        internal void Login(string userId, string pwdOrToken, bool isToken = false, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId ?? "");
            jo_param.AddWithoutNull("pwdOrToken", pwdOrToken ?? "");
            jo_param.AddWithoutNull("isToken", isToken);

            NativeCall(SDKMethod.login, jo_param, callback);
        }

        internal void Logout(bool unbindDeviceToken, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("unbindDeviceToken", unbindDeviceToken);
            NativeCall(SDKMethod.logout, jo_param, callback);
        }

        internal void LoginWithAgoraToken(string userId, string token, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("token", token);
            NativeCall(SDKMethod.loginWithAgoraToken, jo_param, callback);
        }

        internal void RenewAgoraToken(string token)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("token", token);
            NativeCall(SDKMethod.renewToken, jo_param);
        }

        internal void GetLoggedInDevicesFromServer(string userId, string password, ValueCallBack<List<DeviceInfo>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("password", password);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<DeviceInfo>(jsonNode);
            };

            NativeCall<List<DeviceInfo>>(SDKMethod.getLoggedInDevicesFromServer, jo_param, callback, process);
        }

        internal void GetLoggedInDevicesFromServerWithToken(string userId, string token, ValueCallBack<List<DeviceInfo>> callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("token", token);

            Process process = (_, jsonNode) =>
            {
                return List.BaseModelListFromJsonArray<DeviceInfo>(jsonNode);
            };

            NativeCall<List<DeviceInfo>>(SDKMethod.getLoggedInDevicesFromServerWithToken, jo_param, callback, process);
        }

        internal void KickDevice(string userId, string password, string resource, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("password", password);
            jo_param.AddWithoutNull("resource", resource);
            NativeCall(SDKMethod.kickDevice, jo_param, callback);
        }

        internal void KickDeviceWithToken(string userId, string token, string resource, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("token", token);
            jo_param.AddWithoutNull("resource", resource);
            NativeCall(SDKMethod.kickDeviceWithToken, jo_param, callback);
        }

        internal void KickAllDevices(string userId, string password, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("password", password);
            NativeCall(SDKMethod.kickAllDevices, jo_param, callback);
        }

        internal void KickAllDevicesWithToken(string userId, string token, CallBack callback = null)
        {
            JSONObject jo_param = new JSONObject();
            jo_param.AddWithoutNull("userId", userId);
            jo_param.AddWithoutNull("token", token);
            NativeCall(SDKMethod.kickAllDevicesWithToken, jo_param, callback);
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


        internal void DelegateTesterRun()
        {
            NativeCall(SDKMethod.runDelegateTester);
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
                        it.OnDisconnected();
                        break;
                    case SDKMethod.onLoggedOtherDevice:
                        {
                            string deviceName = jsonNode["deviceName"];
                            it.OnLoggedOtherDevice(deviceName);
                        }
                        break;
                    case SDKMethod.onRemovedFromServer:
                        it.OnRemovedFromServer();
                        break;
                    case SDKMethod.onForbidByServer:
                        it.OnForbidByServer();
                        break;
                    case SDKMethod.onChangedImPwd:
                        it.OnChangedIMPwd();
                        break;
                    case SDKMethod.onLoginTooManyDevice:
                        it.OnLoginTooManyDevice();
                        break;
                    case SDKMethod.onKickedByOtherDevice:
                        it.OnKickedByOtherDevice();
                        break;
                    case SDKMethod.onAuthFailed:
                        it.OnAuthFailed();
                        break;
                    case SDKMethod.onTokenExpired:
                        it.OnTokenExpired();
                        break;
                    case SDKMethod.onTokenWillExpire:
                        it.OnTokenWillExpire();
                        break;
                    case SDKMethod.onAppActiveNumberReachLimitation:
                        it.OnAppActiveNumberReachLimitation();
                        break;
                    default:
                        break;
                }
            }

        }

        internal void NativeEventHandle_MultiDevice(string method, JSONNode jsonNode)
        {
            if (delegater_multidevice.Count == 0 || null == jsonNode) return;

            MultiDevicesOperation operation = jsonNode["operation"].AsInt.ToMultiDevicesOperation();
            string target = jsonNode["target"];
            string ext = jsonNode["ext"];
            List<string> userIds = List.StringListFromJsonArray(jsonNode["userIds"]);
            string convId = "";

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
                        // OnUndisturbMultiDevicesEvent is related to Push function
                        // currently Unity don't support Push function, so ignore OnUndisturbMultiDevicesEvent
                        //it.OnUndisturbMultiDevicesEvent();
                        break;
                    case SDKMethod.onThreadMultiDevicesEvent:
                        it.OnThreadMultiDevicesEvent(operation, target, userIds);
                        break;
                    case SDKMethod.onRoamDeleteMultiDevicesEvent:
                        convId = jsonNode["convId"];
                        string deviceId = jsonNode["deviceId"];
                        it.OnRoamDeleteMultiDevicesEvent(convId, deviceId);
                        break;
                    case SDKMethod.onConversationMultiDevicesEvent:
                        convId = jsonNode["convId"];
                        ConversationType type = jsonNode["type"].AsInt.ToConversationType();
                        it.OnConversationMultiDevicesEvent(operation, convId, type);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void CleanUp()
        {
            nativeListener.RemoveNativeListener();
        }

        internal void ClearResource()
        {
            delegater_connection.Clear();
            delegater_multidevice.Clear();
            SDKClient.Instance.ChatManager.ClearDelegates();
            SDKClient.Instance.ThreadManager.ClearDelegates();
            SDKClient.Instance.ContactManager.ClearDelegates();
            SDKClient.Instance.GroupManager.ClearDelegates();
            SDKClient.Instance.RoomManager.ClearDelegates();
            SDKClient.Instance.PresenceManager.ClearDelegates();
            CWrapperNative.NativeCall(SDKMethod.client, "clearResource", null, "");
        }
    }
}
