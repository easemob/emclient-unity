using System.Collections.Generic;
using UnityEngine;
namespace ChatSDK
{
    public abstract class IContactManager
    {
        

        public abstract void AddContact(string username, string reason = null, CallBack handle = null);

        public abstract void DeleteContact(string username, bool keepConversation = false, CallBack handle = null);

        public abstract void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null);

        public abstract List<string> GetAllContactsFromDB();

        public abstract void AddUserToBlockList(string username, CallBack handle = null);

        public abstract void RemoveUserFromBlockList(string username, CallBack handle = null);

        public abstract void GetBlockListFromServer(ValueCallBack<List<string>> handle = null);

        public abstract void AcceptInvitation(string username, CallBack handle = null);

        public abstract void DeclineInvitation(string username, CallBack handle = null);

        public abstract void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null);

        public void AddContactManagerDelegate(IContactManagerDelegate contactManagerDelegate)
        {
            CallbackManager.Instance().contactManagerDelegates.Add(contactManagerDelegate);
        }

        internal void ClearDelegates()
        {
            CallbackManager.Instance().contactManagerDelegates.Clear();
        }
    }

}