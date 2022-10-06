using System.Collections.Generic;
using UnityEngine;

namespace AgoraChat
{
    internal sealed class ContactManager_Android : IContactManager
    {

        private AndroidJavaObject wrapper;

        public ContactManager_Android() {
            using (AndroidJavaClass aj = new AndroidJavaClass("com.hyphenate.unity_chat_sdk.EMContactManagerWrapper"))
            {
                wrapper = aj.CallStatic<AndroidJavaObject>("wrapper");
            }
        }


        public override void AddContact(string username, string reason = null, CallBack handle = null)
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

        public override List<string> GetAllContactsFromDB()
        {
            string jsonString = wrapper.Call<string>("getAllContactsFromDB");
            if (jsonString == null) {
                return null;
            }

            if (jsonString.Length == 0) {
                return new List<string>();
            }
            return TransformTool.JsonStringToStringList(jsonString);
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