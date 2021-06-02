using UnityEngine;
using SimpleJSON;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    public class Client_Mac : IClient
    {

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            _CreateAccount(username, password, callBack?.callbackId);
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater = null)
        {
            //throw new System.NotImplementedException();
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            //throw new System.NotImplementedException();
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            //throw new System.NotImplementedException();
        }




        [DllImport("hyphenateCWrapper")]
        private static extern void _CreateAccount(string username, string password, string callbackId);

    }
}