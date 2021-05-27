using System.Collections.Generic;
using UnityEngine;

namespace ChatSDK
{
    public class ContactManager_Android : IContactManager
    {

        static string ContactListener_Obj = "unity_chat_emclient_contact_delegate_obj";

        private AndroidJavaObject wrapper;

        GameObject listenerGameObj;

        public ContactManager_Android() {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMContactManagerWrapper"))
            {
                listenerGameObj = new GameObject(ContactListener_Obj);
                ContactManagerListener contactListener = listenerGameObj.AddComponent<ContactManagerListener>();
                contactListener.managerDelegater = Delegate;
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }


        public override void AddContact(string username, string reason = "", CallBack handle = null)
        {
            wrapper.Call("addContact", username, reason, handle?.callbackId);
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            wrapper.Call("deleteContact", username, keepConversation, handle?.callbackId);
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)                        
        {
            wrapper.Call("getAllContactsFromServer", handle?.callbackId);
        }

        public override void GetAllContactsFromDB(ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getAllContactsFromDB", handle?.callbackId);
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            wrapper.Call("addUserToBlockList", username, handle?.callbackId);
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            wrapper.Call("removeUserFromBlockList", username, handle?.callbackId);
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            wrapper.Call("getBlockListFromServer", handle?.callbackId);
        }

        public override void AcceptInvitation(string username, CallBack handle = null)
        {
            wrapper.Call("acceptInvitation", username, handle?.callbackId);
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            wrapper.Call("declineInvitation", username, handle?.callbackId);
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null) {
            wrapper.Call("getSelfIdsOnOtherPlatform", handle?.callbackId);
        }
    }
}