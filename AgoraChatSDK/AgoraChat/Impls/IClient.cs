using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;

namespace AgoraChat
{
    internal class IClient
    {
        public NativeListener nativeListener = new NativeListener();

        public ChatManager chatManager;
        public ContactManager contactManager;
        public GroupManager groupManager;
        public RoomManager roomManager;
        public PresenceManager presenceManager;
        public ChatThreadManager chatThreadManager;
        public UserInfoManager userInfoManager;
        public ConversationManager conversationManager;
        public MessageManager messageManager;

        private CallbackManager callbackManager;

        internal string NAME_CLIENT = "Client";
        internal List<IConnectionDelegate> delegater;

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
            delegater = new List<IConnectionDelegate>();

            nativeListener.connectionEvent += NativeEventHandle;
        }

        internal void InitWithOptions(Options options)
        {
            JSONObject jo = options.ToJsonObject();
            CWrapperNative.NativeCall(NAME_CLIENT, "initWithOptions", jo, null);
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

        internal void CleanUp() 
        {
            nativeListener.RemoveNativeListener();
        }


        internal void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!delegater.Contains(connectionDelegate))
            {
                delegater.Add(connectionDelegate);
            }
        }

        internal void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (delegater.Contains(connectionDelegate))
            {
                delegater.Remove(connectionDelegate);
            }
        }

        //TODO: add multi listener

        internal void NativeEventHandle(string method, string jsonString)
        {
            if (delegater.Count == 0 || null == method || method.Length == 0) return;

            if (method.CompareTo("OnConnected") == 0)
            {
                foreach (IConnectionDelegate it in delegater)
                {
                    it.OnConnected();
                }
            }
            else if (method.CompareTo("OnDisconnected") == 0)
            {
                foreach (IConnectionDelegate it in delegater)
                {
                    it.OnDisconnected(int.Parse(jsonString));
                }
            }
            else if (method.CompareTo("OnTokenExpired") == 0)
            {
                foreach (IConnectionDelegate it in delegater)
                {
                    it.OnTokenExpired();
                }
            }
            else if (method.CompareTo("OnTokenExpired") == 0)
            {
                foreach (IConnectionDelegate it in delegater)
                {
                    it.OnTokenExpired();
                }
            }
        }
    }
}
