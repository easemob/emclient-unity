using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace ChatSDK
{
    internal sealed class ContactManager_Mac : IContactManager
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
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_AcceptInvitation(client, callbackId, username,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AddContact(string username, string reason = null, CallBack handle = null)
        {
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_AddContact(client, callbackId, username, reason ?? "",
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void AddUserToBlockList(string username, CallBack handle = null)
        {
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_AddToBlackList(client, callbackId, username, true,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);

                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DeclineInvitation(string username, CallBack handle = null)
        {
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_DeclineInvitation(client, callbackId, username,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void DeleteContact(string username, bool keepConversation = false, CallBack handle = null)
        {
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_DeleteContact(client, callbackId, username, false,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override List<string> GetAllContactsFromDB()
        {
            List<string> list = new List<string>();
            ChatAPINative.ContactManager_GetContactsFromDB(client,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
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
                (int code, string desc, int cbId) => { Debug.Log($"GetAllContactsFromDB failed with code={code}, desc={desc}"); });
            return list;
        }

        public override void GetAllContactsFromServer(ValueCallBack<List<string>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_GetContactsFromServer(client, callbackId,
                (IntPtr[] data, DataType dType, int size, int cbId) =>
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
                        ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, list);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                });
        }

        public override void GetBlockListFromServer(ValueCallBack<List<string>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_GetBlackListFromServer(client, callbackId,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetBlockListFromServer callback with dType={dType}, size={size}");
                    if (DataType.ListOfString == dType)
                    {
                        var list = new List<string>();
                        for (int i = 0; i < size; i++)
                        {
                            list.Add(Marshal.PtrToStringAnsi(array[i]));
                        }
                        ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, list);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },

                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                });
        }

        public override void GetSelfIdsOnOtherPlatform(ValueCallBack<List<string>> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_GetSelfIdsOnOtherPlatform(client, callbackId, 
                (IntPtr[] array, DataType dType, int size, int cbId) =>
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
                        ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, list);
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                },

                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                });
        }

        public override void RemoveUserFromBlockList(string username, CallBack handle = null)
        {
            if (null == username || 0 == username.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.ContactManager_RemoveFromBlackList(client, callbackId, username,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }
    }
}