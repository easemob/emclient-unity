using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    public class ContactManager_Mac : IContactManager
    {
        private IntPtr client;
        private ContactManagerHub contactManagerHub;

        internal ContactManager_Mac(IClient _client)
        {
            if(_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
            //register listeners
            contactManagerHub = new ContactManagerHub();
            ChatAPINative.ContactManager_AddListener(client, contactManagerHub.OnContactAdd,contactManagerHub.OnContactDeleted,
                contactManagerHub.OnContactInvited, contactManagerHub.OnFriendRequestAccepted,contactManagerHub.OnFriendRequestDeclined);
        }

        public override void AcceptInvitation(string username, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_AcceptInvitation(client, username,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AddContact(string username, string reason = null, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_AddContact(client, username, reason ?? "",
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_AddToBlackList(client, username, true,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_DeclineInvitation(client, username,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_DeleteContact(client, username, false,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override List<string> GetAllContactsFromDB()
        {
            List<string> list = new List<string>();
            ChatAPINative.ContactManager_GetContactsFromDB(client,
                (IntPtr[] data, DataType dType, int size) =>
                {
                    Debug.Log($"GetAllContactsFromServer callback with dType={dType}, size={size}");
                    if (dType == DataType.ListOfString)
                    {
                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                                list.Add(Marshal.PtrToStringAnsi(data[i]));
                        }
                        else
                            Debug.Log("Empty contact list is returned.");
                    }
                    else
                        Debug.LogError("Incorrect delegate parameters returned.");
                },
                (int code, string desc) => { Debug.Log($"GetAllContactsFromDB failed with code={code}, desc={desc}"); });
            return list;
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.ContactManager_GetContactsFromServer(client,
                (IntPtr[] data, DataType dType, int size) =>
                {
                    Debug.Log($"GetAllContactsFromServer callback with dType={dType}, size={size}");
                    if (dType == DataType.ListOfString)
                    {
                        var list = new List<string>();
                        if (size > 0)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                list.Add(Marshal.PtrToStringAnsi(data[i]));
                            }
                        }
                        else
                        {
                            Debug.Log("Empty contact list is returned.");
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(list); });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.ContactManager_GetBlackListFromServer(client,
                (IntPtr[] array, DataType dType, int size) =>
                {
                    Debug.Log($"GetBlockListFromServer callback with dType={dType}, size={size}");
                    if (DataType.ListOfString == dType)
                    {
                        var list = new List<string>();
                        for (int i = 0; i < size; i++)
                        {
                            list.Add(Marshal.PtrToStringAnsi(array[i]));
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(list); });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },

                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null)
        {
            ChatAPINative.ContactManager_GetSelfIdsOnOtherPlatform(client,
                (IntPtr[] array, DataType dType, int size) =>
                {
                    Debug.Log($"GetSelfIdsOnOtherPlatform callback with dType={dType}, size={size}");
                    //Verify parameter
                    if (dType == DataType.ListOfString)
                    {
                        var list = new List<string>();
                        for (int i = 0; i < size; i++)
                        {
                            list.Add(Marshal.PtrToStringAnsi(array[i]));
                        }
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(list); });
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },

                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            if (null == username)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.ContactManager_RemoveFromBlackList(client, username,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }
    }
}