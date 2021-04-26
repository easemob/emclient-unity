using System.Collections.Generic;

namespace ChatSDK
{

    public interface IContactManager
    {
        void AddContact(string username, string reason = "", SDKCallBack callBack = null);

        void DeleteContact(string username, SDKCallBack callBack = null);

        void GetAllContactsFromServer(SDKValueCallBack<List<string>> callBack = null);

        void GetAllContactsFromDB(SDKValueCallBack<List<string>> callBack = null);

        void AddUserToBlockList(SDKValueCallBack<string> callBack = null);

        void RemoveUserFromBlockList(SDKValueCallBack<string> callBack = null);

        void GetBlockListFromServer(SDKValueCallBack<List<string>> callBack = null);

        void AcceptInvitation(SDKValueCallBack<string> callBack = null);

        void DeclineInvitation(SDKValueCallBack<string> callBack = null);
    }

}