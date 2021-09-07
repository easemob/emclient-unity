using System.Collections.Generic;

namespace ChatSDK
{
    internal sealed class ContactManager_Win : IContactManager
    {
        public override void AcceptInvitation(string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddContact(string username, string reason = null, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override List<string> GetAllContactsFromDB()
        {
            throw new System.NotImplementedException();
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            throw new System.NotImplementedException();
        }
    }
}