namespace ChatSDK
{
    public abstract class IClient
    {
        private static IClient _Instance;
        public static IClient Instance
        {
            get
            {
                if (_Instance == null)
                {
#if UNITY_ANDROID
                    _Instance = new Client_Android();
#elif UNITY_IOS
                    _Instance = new Client_iOS();
#elif UNITY_STANDALONE_OSX
                    _Instance = new Client_Mac();
#elif UNITY_STANDALONE_WIN
                    _Instance = new Client_Win();
#endif
                }

                return _Instance;
            }
        }

        private IChatManager _ChatImp;
        private IContactManager _ContactImp;
        private IGroupManager _GroupImp;
        private IRoomManager _RoomImp;
        private IPushManager _PushImp;

        public IChatManager chatManager
        {
            get
            {
                if (_ChatImp != null) { return _ChatImp; }
#if UNITY_ANDROID
                _ChatImp = new ChatManager_Android();
#elif UNITY_IOS
                _ChatImp = new ChatManager_iOS();
#elif UNITY_STANDALONE_OSX
                _ChatImp = new ChatManager_Mac();
#elif UNITY_STANDALONE_WIN
                _ChatImp = new ChatManager_Win();
#endif
                return _ChatImp;
            }
        }

        public IContactManager contactManager
        {
            get
            {
                if (_ContactImp != null) { return _ContactImp; }
#if UNITY_ANDROID
                _ContactImp = new ContactManager_Android();
#elif UNITY_IOS
                _ContactImp = new ContactManager_iOS();
#elif UNITY_STANDALONE_OSX
                _ContactImp = new ContactManager_Mac();
#elif UNITY_STANDALONE_WIN
                _ContactImp = new ContactManager_Win();
#endif
                return _ContactImp;
            }
        }


        public IGroupManager groupManager
        {
            get
            {
                if (_GroupImp != null) { return _GroupImp; }
#if UNITY_ANDROID
                _GroupImp = new GroupManager_Android();
#elif UNITY_IOS
                _GroupImp = new GroupManager_iOS();
#elif UNITY_STANDALONE_OSX
                _GroupImp = new GroupManager_Mac();
#elif UNITY_STANDALONE_WIN
                _GroupImp = new GroupManager_Win();
#endif
                return _GroupImp;
            }
        }


        public IRoomManager roomManager
        {
            get
            {
                if (_RoomImp != null) { return _RoomImp; }
#if UNITY_ANDROID
                _RoomImp = new RoomManager_Android();
#elif UNITY_IOS
                _RoomImp = new RoomManager_iOS();
#elif UNITY_STANDALONE_OSX
                _RoomImp = new RoomManager_Mac();
#elif UNITY_STANDALONE_WIN
                _RoomImp = new RoomManager_Win();
#endif
                return _RoomImp;
            }
        }


        public IPushManager pushManager
        {
            get
            {
                if (_PushImp != null) { return _PushImp; }
#if UNITY_ANDROID
                _PushImp = new PushManager_Android();
#elif UNITY_IOS
                _PushImp = new PushManager_iOS();
#elif UNITY_STANDALONE_OSX
                _PushImp = new PushManager_Mac();
#elif UNITY_STANDALONE_WIN
                _PushImp = new PushManager_Win();
#endif
                return _PushImp;
            }
        }


        public abstract int InitWithOptions(Options options);

        public abstract int CreateAccount(string username, string password);

        public abstract void Register(string username, string password);

        public abstract void Login(string username, string password);

        public abstract void Logout(bool unbindDeviceToken);

    }
}