#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
    internal abstract class IClient
    {
        static private bool _initialized = false;

        private static IClient instance;

        public static IClient Instance
        {
            get
            {
                if (instance == null)
                {
                    CallbackManager.Instance();
#if UNITY_ANDROID
                    instance = new Client_Android();
#elif UNITY_IOS
                    instance = new Client_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
                    instance = new Client_Mac();
#else
                    instance = new Client_Mac();
#endif
                    _initialized = true;
                }

                return instance;
            }
        }

        private IChatManager chatImp = null;
        private IContactManager contactImp = null;
        private IGroupManager groupImp = null;
        private IRoomManager roomImp = null;
        private IPushManager pushImp = null;
        private IConversationManager conversationImp = null;
        private IUserInfoManager userInfoImp = null;
        private IPresenceManager presenceImp = null;

        /// <summary>
        /// 获取聊天管理对象
        /// </summary>
        /// <returns>聊天管理对象</returns>
        public IChatManager ChatManager()
        {
            if (_initialized == false) return null;
            if (chatImp != null) { return chatImp; }
#if UNITY_ANDROID
            chatImp = new ChatManager_Android();
#elif UNITY_IOS
            chatImp = new ChatManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            chatImp = new ChatManager_Mac(instance);
#else
            chatImp = new ChatManager_Mac(instance);
#endif
            return chatImp;
        }

        /// <summary>
        /// 获取通讯录管理对象
        /// </summary>
        /// <returns>通讯录管理对象</returns>
        public IContactManager ContactManager()
        {
            if (_initialized == false) return null;
            if (contactImp != null) { return contactImp; }
#if UNITY_ANDROID
            contactImp = new ContactManager_Android();
#elif UNITY_IOS
            contactImp = new ContactManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            contactImp = new ContactManager_Mac(instance);
#else
            contactImp = new ContactManager_Mac(instance);
#endif
            return contactImp;
        }

        /// <summary>
        /// 获取群组管理对象
        /// </summary>
        /// <returns>群组管理对象</returns>
        public IGroupManager GroupManager()
        {
            if (_initialized == false) return null;
            if (groupImp != null) { return groupImp; }
#if UNITY_ANDROID
            groupImp = new GroupManager_Android();
#elif UNITY_IOS
            groupImp = new GroupManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            groupImp = new GroupManager_Mac(instance);
#else
            groupImp = new GroupManager_Mac(instance);
#endif
            return groupImp;
        }

        /// <summary>
        /// 获取聊天室管理对象
        /// </summary>
        /// <returns>聊天室管理对象</returns>
        public IRoomManager RoomManager()
        {
            if (_initialized == false) return null;
            if (roomImp != null) { return roomImp; }
#if UNITY_ANDROID
            roomImp = new RoomManager_Android();
#elif UNITY_IOS
            roomImp = new RoomManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            roomImp = new RoomManager_Mac(instance);
#else
            roomImp = new RoomManager_Mac(instance);
#endif
            return roomImp;
        }

        /// <summary>
        /// 获取推送管理对象
        /// </summary>
        /// <returns>推送管理对象</returns>
        public IPushManager PushManager()
        {
            if (_initialized == false) return null;
            if (pushImp != null) { return pushImp; }
#if UNITY_ANDROID
            pushImp = new PushManager_Android();
#elif UNITY_IOS
            pushImp = new PushManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            pushImp = new PushManager_Mac(instance);
#else
            pushImp = new PushManager_Mac(instance);
#endif
            return pushImp;
        }

        internal IConversationManager ConversationManager()
        {
            if (conversationImp != null) { return conversationImp; }
#if UNITY_ANDROID
            conversationImp = new ConversationManager_Android();
#elif UNITY_IOS
            conversationImp = new ConversationManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            conversationImp = new ConversationManager_Mac(instance);
#else
            conversationImp = new ConversationManager_Mac(instance);
#endif
            return conversationImp;
        }

        internal IUserInfoManager UserInfoManager()
        {
            if (userInfoImp != null) { return userInfoImp; }
#if UNITY_ANDROID
            userInfoImp = new UserInfoManager_Android();
#elif UNITY_IOS
            userInfoImp = new UserInfoManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            userInfoImp = new UserInfoManager_Mac(instance);
#else
            userInfoImp = new UserInfoManager_Mac(instance);
#endif
            return userInfoImp;
        }

        internal IPresenceManager PresenceManager()
        {
            if (presenceImp != null) { return presenceImp; }
#if UNITY_ANDROID
            presenceImp = new PresenceManager_Android();
#elif UNITY_IOS
            presenceImp = new PresenceManager_iOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
            presenceImp = new PresenceManager_Mac(instance);
#endif
            return presenceImp;
        }

        /// <summary>
        /// 初始化sdk
        /// </summary>
        /// <param name="options">sdk配置对象</param>
        public abstract void InitWithOptions(Options options);

        /// <summary>
        /// 创建环信id
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="password">环信id对应的密码</param>
        /// <param name="handle">执行结果</param>
        public abstract void CreateAccount(string username, string password, CallBack handle = null);

        /// <summary>
        /// 登录环信
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="pwdOrToken">环信id对应的密码/token</param>
        /// <param name="isToken">是否是token登录</param>
        /// <param name="handle">执行结果</param>
        public abstract void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null);

        /// <summary>
        /// 退出环信
        /// </summary>
        /// <param name="unbindDeviceToken">是否解除推送绑定</param>
        /// <param name="handle">执行结果</param>
        public abstract void Logout(bool unbindDeviceToken = true, CallBack handle = null);

        /// <summary>
        /// 获取当前登录的环信id
        /// </summary>
        /// <returns>当前登录的环信id</returns>
        public abstract string CurrentUsername();

        /// <summary>
        /// 获取当前是否链接到环信服务器
        /// </summary>
        public abstract bool IsConnected();

        /// <summary>
        /// 获取是否已经登录
        /// </summary>
        /// <returns>是否已登录</returns>
        public abstract bool IsLoggedIn();

        /// <summary>
        /// 获取当前环信id对应的token
        /// </summary>
        /// <returns>token</returns>
        public abstract string AccessToken();

        /// <summary>
        /// 使用声网token登录环信
        /// </summary>
        /// <param name="username">环信id</param>
        /// <param name="token">声网token</param>
        /// <param name="handle">执行结果</param>
        public abstract void LoginWithAgoraToken(string username, string token, CallBack handle = null);

        /// <summary>
        /// 更新声网token
        /// </summary>
        /// <param name="token">声网token</param>
        public abstract void RenewAgoraToken(string token);

        internal abstract void StartLog(string logFilePath);

        internal abstract void StopLog();

        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void ClearResource();

        /// <summary>
        /// 添加连接回调监听
        /// </summary>
        /// <param name="connectionDelegate">实现监听的对象</param>
        public void AddConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (!CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate))
            {
                CallbackManager.Instance().connectionListener.delegater.Add(connectionDelegate);
            }
        }

        /// <summary>
        /// 移除连接回调监听
        /// </summary>
        /// <param name="connectionDelegate">实现监听的对象</param>
        public void DeleteConnectionDelegate(IConnectionDelegate connectionDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().connectionListener.delegater.Contains(connectionDelegate))
            {
                CallbackManager.Instance().connectionListener.delegater.Remove(connectionDelegate);
            }
        }

        /// <summary>
        /// 添加多设备回调监听
        /// </summary>
        /// <param name="multiDeviceDelegate">实现监听的对象</param>
        public void AddMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (!CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Add(multiDeviceDelegate);
            }
        }

        /// <summary>
        /// 移除多设备回调监听
        /// </summary>
        /// <param name="multiDeviceDelegate">实现监听的对象</param>
        public void DeleteMultiDeviceDelegate(IMultiDeviceDelegate multiDeviceDelegate)
        {
            if (CallbackManager.IsQuit()) return;
            if (CallbackManager.Instance().multiDeviceListener.delegater.Contains(multiDeviceDelegate))
            {
                CallbackManager.Instance().multiDeviceListener.delegater.Remove(multiDeviceDelegate);
            }
        }

    }
}
