


namespace ChatSDK
{
    public class SDKClient
    {
        private Options _Options;
        private string _SdkVersion = null;
        private string _CurrentUsername = null;
        private bool _IsLoggedIn = false;
        private bool _IsConnected = false;
        private string _AccessToken = null;
        private static SDKClient _instance;
        private IClient _Sdk;

        public static SDKClient Instance
        {
            get
            {
                return _instance ?? (_instance = new SDKClient());
            }
        }

        public IChatManager ChatManager
        {
            get
            {
                return _Sdk.chatManager;
            }
        }

        public IContactManager ContactManager
        {
            get
            {
                return _Sdk.contactManager;
            }
        }

        public IGroupManager GroupManager
        {
            get
            {
                return _Sdk.groupManager;
            }
        }

        public IRoomManager RoomManager
        {
            get
            {
                return _Sdk.roomManager;
            }
        }

        public IPushManager PushManager
        {
            get
            {
                return _Sdk.pushManager;
            }
        }

        public Options Options { get { return _Options; } }

        public string SdkVersion { get { return _SdkVersion; } }

        public string CurrentUsername { get { return _CurrentUsername; } }

        public bool IsLoggedIn { get { return _IsLoggedIn; } }

        public bool IsConnected { get { return _IsConnected; } }

        public string AccessToken { get { return _AccessToken; } }


        public void InitWithOptions(Options options)
        {
            _Options = options;
            _Sdk.InitWithOptions(options);
            // TODO: 设置callback；
        }

        public void Register(string username, string password)
        {
            _Sdk.Register(username, password);
        }

        public void Login(string username, string password)
        {
            _Sdk.Login(username, password);
        }

        public void Logout(bool unbindDeviceToken)
        {
            _Sdk.Logout(unbindDeviceToken);
        }

        private SDKClient()
        {
            _Sdk = IClient.Instance;
        }
    }
}





