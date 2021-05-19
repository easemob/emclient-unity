using UnityEngine;

namespace ChatSDK
{
    public class Client_Android : IClient  
    {

        static string Connection_Obj = "unity_chat_emclient_connection_obj";

        private AndroidJavaObject wrapper;

        GameObject listenerGameObj;

        public Client_Android() {
           
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMClientWrapper")) {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }

        public override void InitWithOptions(Options options, WeakDelegater<IConnectionDelegate> connectionDelegater)
        {
            CallbackManager.Instance();
            listenerGameObj = new GameObject(Connection_Obj);
            ConnectionListener connectionListener = listenerGameObj.AddComponent<ConnectionListener>();
            connectionListener.connectionDelegater = connectionDelegater;
            wrapper.Call("init", options.ToString());
        }

        public override void CreateAccount(string username, string password, CallBack callBack = null)
        {
            wrapper.Call("createAccount", username, password, null);
        }

        public override void Login(string username, string pwdOrToken, bool isToken = false, CallBack callBack = null)
        {
            wrapper.Call("login", username, pwdOrToken, isToken, callBack?.callbackId.ToString());
        }

        public override void Logout(bool unbindDeviceToken, CallBack callBack = null)
        {
            wrapper.Call("logout", unbindDeviceToken, null);
        }

       
    }
}