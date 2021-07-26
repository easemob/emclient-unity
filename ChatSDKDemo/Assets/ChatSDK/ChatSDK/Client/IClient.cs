namespace ChatSDK
{
    public abstract class IClient
    {
        private static IClient instance;
        public static IClient Instance
        {
            get
            {
                if (instance == null)
                {
#if UNITY_ANDROID
                    instance = new Client_Android();
#elif UNITY_IOS
                    instance = new Client_iOS();
#elif UNITY_STANDALONE_OSX
                    instance = new Client_Mac();
#elif UNITY_STANDALONE_WIN
                    instance = new Client_Win();
#endif
                }

                return instance;
            }
        }

        private IChatManager chatImp;
        private IContactManager contactImp;
        private IGroupManager groupImp;
        private IRoomManager roomImp;
        private IPushManager pushImp;


        public IChatManager ChatManager()
        {
            if (chatImp != null) { return chatImp; }
#if UNITY_ANDROID
            chatImp = new ChatManager_Android();
#elif UNITY_IOS
                chatImp = new ChatManager_iOS();
#elif UNITY_STANDALONE_OSX
                chatImp = new ChatManager_Mac(instance);
#elif UNITY_STANDALONE_WIN
                chatImp = new ChatManager_Win();
#endif
            return chatImp;
        }

        public IContactManager ContactManager()
        {
            if (contactImp != null) { return contactImp; }
#if UNITY_ANDROID
            contactImp = new ContactManager_Android();
#elif UNITY_IOS
            contactImp = new ContactManager_iOS();
#elif UNITY_STANDALONE_OSX
            contactImp = new ContactManager_Mac(instance);
#elif UNITY_STANDALONE_WIN
            contactImp = new ContactManager_Win();
#endif
            return contactImp;
        }


        public IGroupManager GroupManager()
        {
            if (groupImp != null) { return groupImp; }
#if UNITY_ANDROID
            groupImp = new GroupManager_Android();
#elif UNITY_IOS
                groupImp = new GroupManager_iOS();
#elif UNITY_STANDALONE_OSX
                groupImp = new GroupManager_Mac(instance);
#elif UNITY_STANDALONE_WIN
                groupImp = new GroupManager_Win();
#endif
            return groupImp;
        }


        public IRoomManager RoomManager()
        {
            if (roomImp != null) { return roomImp; }
#if UNITY_ANDROID
            roomImp = new RoomManager_Android();
#elif UNITY_IOS
                roomImp = new RoomManager_iOS();
#elif UNITY_STANDALONE_OSX
                roomImp = new RoomManager_Mac(instance);
#elif UNITY_STANDALONE_WIN
                roomImp = new RoomManager_Win();
#endif
            return roomImp;
        }


        public IPushManager PushManager()
        {
            if (pushImp != null) { return pushImp; }
#if UNITY_ANDROID
            pushImp = new PushManager_Android();
#elif UNITY_IOS
                pushImp = new PushManager_iOS();
#elif UNITY_STANDALONE_OSX
                pushImp = new PushManager_Mac();
#elif UNITY_STANDALONE_WIN
                pushImp = new PushManager_Win();
#endif
            return pushImp;
        }

        public abstract void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null);

        public abstract void CreateAccount(string username, string password, CallBack handle = null);

        public abstract void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null);

        public abstract void Logout(bool unbindDeviceToken, CallBack handle = null);

        public abstract string CurrentUsername();

        public abstract bool IsConnected { get; internal set; }

        public abstract bool IsLoggedIn();

        public abstract string AccessToken();

        public abstract void StartLog(string logFilePath);

        public abstract void StopLog();

    }
}