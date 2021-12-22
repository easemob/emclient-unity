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
            ChatAPIIOS.ContactManager_HandleMethodCall("acceptInvitation", obj.ToString(), handle?.callbackId);
        }

        public override void AddContact(string username, string reason = null, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("reason", reason ?? "");
            ChatAPIIOS.ContactManager_HandleMethodCall("addContact", obj.ToString(), handle?.callbackId);
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ChatAPIIOS.ContactManager_HandleMethodCall("addUserToBlockList", obj.ToString(), handle?.callbackId);
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ChatAPIIOS.ContactManager_HandleMethodCall("declineInvitation", obj.ToString(), handle?.callbackId);
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            obj.Add("keepConversation", keepConversation);
            ChatAPIIOS.ContactManager_HandleMethodCall("deleteContact", obj.ToString(), handle?.callbackId);
        }

        public override List<string> GetAllContactsFromDB()
        {
            string jsonString =  ChatAPIIOS.ContactManager_GetMethodCall("getAllContactsFromDB");
            if (jsonString == null || jsonString.Length == 0)
            {
                return null;
            }
            return TransformTool.JsonStringToStringList(jsonString);
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)
        {
            ChatAPIIOS.ContactManager_HandleMethodCall("getAllContactsFromServer", null, handle?.callbackId);
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            ChatAPIIOS.ContactManager_HandleMethodCall("getBlockListFromServer", null, handle?.callbackId);
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null)
        {
            ChatAPIIOS.ContactManager_HandleMethodCall("getSelfIdsOnOtherPlatform", null, handle?.callbackId);
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            JSONObject obj = new JSONObject();
            obj.Add("username", username);
            ChatAPIIOS.ContactManager_HandleMethodCall("removeUserFromBlockList", obj.ToString(), handle?.callbackId);
        }
    }

}