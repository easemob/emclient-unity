using UnityEngine;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            ChatAPINative.Client_CreateAccount(username, password);
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null)
        {
            ChatAPINative.Client_InitWithOptions(options);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            ChatAPINative.Client_Login(username, pwdOrToken, isToken);
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            ChatAPINative.Client_Logout(unbindDeviceToken);
        }


    }

}
