using System.Collections.Generic;
using System.Runtime.InteropServices;
using SimpleJSON;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class ContactManager_iOS : IContactManager
    {
        
        public ContactManager_iOS() {

        }

        public override void AcceptInvitation(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ContactManagerNative.ContactManager_HandleMethodCall("acceptInvitation", obj.ToString(), handle?.callbackId);
        }

        public override void AddContact(string username, string reason = null, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("reason", reason ?? "");
            ContactManagerNative.ContactManager_HandleMethodCall("addContact", obj.ToString(), handle?.callbackId);
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ContactManagerNative.ContactManager_HandleMethodCall("addUserToBlockList", obj.ToString(), handle?.callbackId);
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ContactManagerNative.ContactManager_HandleMethodCall("declineInvitation", obj.ToString(), handle?.callbackId);
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("keepConversation", keepConversation);
            ContactManagerNative.ContactManager_HandleMethodCall("deleteContact", obj.ToString(), handle?.callbackId);
        }

        public override List<string> GetAllContactsFromDB()
        {
            string jsonString =  ContactManagerNative.ContactManager_GetMethodCall("getAllContactsFromDB");
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return TransformTool.JsonStringToStringList(jsonString);
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)
        {
            ContactManagerNative.ContactManager_HandleMethodCall("getAllContactsFromServer", null, handle?.callbackId);
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            ContactManagerNative.ContactManager_HandleMethodCall("getBlockListFromServer", null, handle?.callbackId);
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null)
        {
            ContactManagerNative.ContactManager_HandleMethodCall("getSelfIdsOnOtherPlatform", null, handle?.callbackId);
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ContactManagerNative.ContactManager_HandleMethodCall("removeUserFromBlockList", obj.ToString(), handle?.callbackId);
        }
    }

    class ContactManagerNative
    {
        [DllImport("__Internal")]
        internal extern static void ContactManager_HandleMethodCall(string methodName, string jsonString = null, string callbackId = null);

        [DllImport("__Internal")]
        internal extern static string ContactManager_GetMethodCall(string methodName, string jsonString = null, string callbackId = null);
    }
}