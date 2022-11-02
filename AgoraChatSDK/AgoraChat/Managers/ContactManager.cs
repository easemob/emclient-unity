using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public class ContactManager
    {
        readonly CallbackManager callbackManager;

       

        internal ContactManager(NativeListener nativeListener)
        {
            callbackManager = nativeListener.callbackManager;
            nativeListener.ContactManagerEvent += NativeEventHandle;
        }

        internal void NativeEventHandle(string method, JSONNode jsonNode)
        {

        }

        public void AddContact(string userId, string reason = null, CallBack callBack = null)
        {
        }

    }
}