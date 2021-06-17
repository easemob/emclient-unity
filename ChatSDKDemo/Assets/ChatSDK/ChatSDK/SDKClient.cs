using System.Threading.Tasks;

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

        internal WeakDelegater<IConnectionDelegate> Delegate = new WeakDelegater<IConnectionDelegate>();

        public static SDKClient Instance
        {
            get
            {
                return _instance ?? (_instance = new SDKClient());
            }
        }

        public IChatManager ChatManager { get => _Sdk.ChatManager(); }

        public IContactManager ContactManager { get => _Sdk.ContactManager(); }

        public IGroupManager GroupManager { get => _Sdk.GroupManager(); }

        public IRoomManager RoomManager { get => _Sdk.RoomManager(); }
        
        public IPushManager PushManager { get => _Sdk.PushManager(); }

        public Options Options { get { return _Options; } }

        public string SdkVersion { get { return _SdkVersion; } }

        public string CurrentUsername { get { return _CurrentUsername; } }

        public bool IsLoggedIn { get { return _IsLoggedIn; } }

        public bool IsConnected { get { return _IsConnected; } }

        public string AccessToken { get { return _AccessToken; } }


        public void AddConnectionDelegate(IConnectionDelegate contactManagerDelegate)
        {
            Delegate.Add(contactManagerDelegate);
        }

        internal void ClearDelegates()
        {
            Delegate.Clear();
        }

        public void InitWithOptions(Options options)
        {
            _Options = options;
            _Sdk.InitWithOptions(options, Delegate);
        }

        public void CreateAccount(string username, string password, CallBack handle = null)
        {
            _Sdk.CreateAccount(username, password, handle);
        }

        public void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null)
        {
            //TODO: set current user name once login succeeds.
            _CurrentUsername = username;
            _Sdk.Login(username, pwdOrToken, isToken, handle);
        }

        public void Logout(bool unbindDeviceToken, CallBack handle = null)
        {
            _Sdk.Logout(unbindDeviceToken, handle);
            ClearDelegates();
            _Sdk.ContactManager().ClearDelegates();
            _Sdk.ChatManager().ClearDelegates();
            _Sdk.GroupManager().ClearDelegates();
            _Sdk.RoomManager().ClearDelegates();
            CallbackManager.Instance().CleanAllCallback();
        }

        private SDKClient()
        {
            _Sdk = IClient.Instance;
        }
    }
}





