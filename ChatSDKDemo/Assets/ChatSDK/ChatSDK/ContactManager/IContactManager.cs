using System.Collections.Generic;

namespace ChatSDK
{

    public abstract class IContactManager
    {

        internal WeakDelegater<IContactManagerDelegate> Delegate = new WeakDelegater<IContactManagerDelegate>();

        public abstract void AddContact(string username, string reason = "", CallBack callBack = null);

        public abstract void DeleteContact(string username, CallBack callBack = null);

        public abstract void GetAllContactsFromServer(ValueCallBack<List<string>> callBack = null);

        public abstract void GetAllContactsFromDB(ValueCallBack<List<string>> callBack = null);

        public abstract void AddUserToBlockList(ValueCallBack<string> callBack = null);

        public abstract void RemoveUserFromBlockList(ValueCallBack<string> callBack = null);

        public abstract void GetBlockListFromServer(ValueCallBack<List<string>> callBack = null);

        public abstract void AcceptInvitation(ValueCallBack<string> callBack = null);

        public abstract void DeclineInvitation(ValueCallBack<string> callBack = null);

        public void AddContactManagerDelegate(IContactManagerDelegate contactManagerDelegate)
        {
            Delegate.Add(contactManagerDelegate);
        }

        internal void ClearDelegates()
        {
            Delegate.Clear();
        }
    }

}