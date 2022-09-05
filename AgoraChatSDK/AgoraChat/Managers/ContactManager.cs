using System.Collections.Generic;

namespace AgoraChat
{
    public class ContactManager
    {
        CallbackManager callbackManager;

       

        internal ContactManager(NativeListener nativeListener)
        {
            callbackManager = nativeListener.callbackManager;
            nativeListener.contactManagerEvent += NativeEventHandle;
        }

        void NativeEventHandle(string method, string jsonString)
        {

        }

        public void AddContact(string userId, string reason = null, CallBack callBack = null)
        {
            callbackManager.AddCallback(callBack, (jsonString, callback) => {

            });
            CWrapperNative.NativeCall("contactManager", "addContact", null, callBack?.callbackId);
        }

    }
}