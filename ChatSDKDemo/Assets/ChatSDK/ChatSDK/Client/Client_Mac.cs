using System.Runtime.InteropServices;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {
        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            ClientWrapper.CreateAccount(username, password);
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null)
        {
            ClientWrapper.InitWithOptions(options);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            ClientWrapper.Login(username, pwdOrToken, isToken);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            ClientWrapper.Logout(unbindDeviceToken);
        }
    }

    class ClientWrapper
    {
        [DllImport("Wrapper.dll", EntryPoint = "Client_CreateAccount", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CreateAccount(string username, string password);

        [DllImport("Wrapper.dll", EntryPoint = "Client_InitWithOptions", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitWithOptions(Options options);

        [DllImport("Wrapper.dll", EntryPoint = "Client_Login", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Login(string username, string pwdOrToken, bool isToken = false);


        [DllImport("Wrapper.dll", EntryPoint = "Client_Logout", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Logout(bool unbindDeviceToken);
    }
}