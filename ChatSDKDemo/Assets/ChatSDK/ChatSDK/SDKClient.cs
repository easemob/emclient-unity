using System.Threading.Tasks;

namespace ChatSDK
{
    public class SDKClient
    {
        private Options _Options;
        private string _SdkVersion = null;
        private static SDKClient _instance;
        private IClient _Sdk;

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

        public string CurrentUsername { get => _Sdk.CurrentUsername(); }

        public bool IsLoggedIn { get => _Sdk.IsLoggedIn(); }

        public bool IsConnected { get => _Sdk.IsConnected(); }

        public string AccessToken { get => _Sdk.AccessToken(); }


        public void AddConnectionDelegate(IConnectionDelegate ConnectionDelegate)
        {
            CallbackManager.Instance().connnectDelegates.Add(ConnectionDelegate);
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().connnectDelegates.Clear();
        }

        public void InitWithOptions(Options options)
        {
            _Options = options;
            _Sdk.InitWithOptions(options, null);
            registerManagers();
        }

        public void CreateAccount(string username, string password, CallBack handle = null)
        {
            _Sdk.CreateAccount(username, password, handle);
        }

        public void Login(string username, string pwdOrToken, bool isToken = false, CallBack handle = null)
        {
            _Sdk.Login(username, pwdOrToken, isToken, handle);
        }

        public void Logout(bool unbindDeviceToken, CallBack handle = null)
        {
            _Sdk.Logout(unbindDeviceToken, new CallBack(
                onSuccess: () => {
                    ClearDelegates();
                    _Sdk.ContactManager().ClearDelegates();
                    _Sdk.ChatManager().ClearDelegates();
                    _Sdk.GroupManager().ClearDelegates();
                    _Sdk.RoomManager().ClearDelegates();
                    CallbackManager.Instance().CleanAllCallback();
                    handle?.Success();
                },
                onError:(code, desc) => {
                    handle?.Error(code, desc);
                }
                ));
        }

        private SDKClient()
        {
            _Sdk = IClient.Instance;
        }

        private void registerManagers() {
            _Sdk.ContactManager();
            _Sdk.ChatManager();
            _Sdk.GroupManager();
            _Sdk.RoomManager();
        }
    }
}





