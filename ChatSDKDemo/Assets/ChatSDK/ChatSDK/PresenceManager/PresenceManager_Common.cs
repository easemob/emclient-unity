using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
	internal sealed class PresenceManager_Common : IPresenceManager
	{
		private IntPtr client;
        private PresenceManagerHub presenceManagerHub;

        internal PresenceManager_Common(IClient _client)
		{
			if (_client is Client_Common clientCommon)
			{
				client = clientCommon.client;
			}

            presenceManagerHub = new PresenceManagerHub();

            //registered listeners
            ChatAPINative.PresenceManager_AddListener(client, presenceManagerHub.onPresenceUpdated);
        }

		public override void PublishPresence(string description, CallBack handle = null)
		{
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;
            ChatAPINative.PresenceManager_PublishPresence(client, callbackId, 1, description,
                 (int cbId) =>
                 {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                 },
                 (int code, string desc, int cbId) =>
                 {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                 }
                 );
        }

		public override void SubscribePresences(List<string> members, long expiry, ValueCallBack<List<Presence>> handle = null)
        {
            if(members.Count == 0)
            {
                Debug.Log("Empty member list. Just quit.");
                return;
            }

            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.PresenceManager_SubscribePresences(client, callbackId, memberArray, size, expiry,
                   onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                   {
                       List<Presence> presenceList = new List<Presence>();
                       if (dSize > 0)
                       {
                           for (int j = 0; j < dSize; j++)
                           {
                               PresenceTO pto = Marshal.PtrToStructure<PresenceTO>(data[j]);
                               Presence presence = pto.Unmarshall();
                               presenceList.Add(presence);
                           }
                       }
                       else
                       {
                           Debug.Log($"Presence information expected.");
                       }
                       ChatCallbackObject.ValueCallBackOnSuccess<List<Presence>>(cbId, presenceList);
                   },
                  onError: (int code, string desc, int cbId) => {
                      ChatCallbackObject.ValueCallBackOnError<List<Presence>>(cbId, code, desc);
                  });
        }

		public override void UnsubscribePresences(List<string> members, CallBack handle = null)
        {
            if (members.Count == 0)
            {
                Debug.Log("Empty member list. Just quit");
                return;
            }

            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.PresenceManager_UnsubscribePresences(client, callbackId, memberArray, size,
                 (int cbId) =>
                 {
                     ChatCallbackObject.CallBackOnSuccess(cbId);
                 },
                 (int code, string desc, int cbId) =>
                 {
                     ChatCallbackObject.CallBackOnError(cbId, code, desc);
                 }
                 );
        }

		public override void FetchSubscribedMembers(int pageNum, int pageSize, ValueCallBack<List<string>> handle = null)
        {
            if (pageNum < 0 || pageSize < 0)
            {
                Debug.Log("Invalid param. Just quit.");
                return;
            }

            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;


            ChatAPINative.PresenceManager_FetchSubscribedMembers(client, callbackId, pageNum, pageSize,
                   onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                   {
                       if (1 == dSize)
                       {
                           string jstr = Marshal.PtrToStringAnsi(data[0]);
                           List<string> members = TransformTool.JsonStringToStringList(jstr);
                           ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, members);
                       }
                       else
                       {
                           Debug.Log($"Subscribed members information expected.");
                           List<string> members = new List<string>();
                           ChatCallbackObject.ValueCallBackOnSuccess<List<string>>(cbId, members);
                       }
                   },
                  onError: (int code, string desc, int cbId) => {
                      ChatCallbackObject.ValueCallBackOnError<List<string>>(cbId, code, desc);
                  });
        }

        public override void FetchPresenceStatus(List<string> members, ValueCallBack<List<Presence>> handle = null)
        {
            if (members.Count == 0)
            {
                Debug.Log("Empty member list. Just quit.");
                return;
            }

            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            int size = members.Count;
            string[] memberArray = new string[size];
            int i = 0;
            foreach (string member in members)
            {
                memberArray[i] = member;
                i++;
            }

            ChatAPINative.PresenceManager_FetchPresenceStatus(client, callbackId, memberArray, size,
                   onSuccessResult: (IntPtr[] data, DataType dType, int dSize, int cbId) =>
                   {
                       List<Presence> presenceList = new List<Presence>();
                       if (dSize > 0)
                       {
                           for (int j = 0; j < dSize; j++)
                           {
                               PresenceTO pto = Marshal.PtrToStructure<PresenceTO>(data[j]);
                               Presence presence = pto.Unmarshall();
                               presenceList.Add(presence);
                           }
                       }
                       else
                       {
                           Debug.Log($"Presence information expected.");
                       }
                       ChatCallbackObject.ValueCallBackOnSuccess<List<Presence>>(cbId, presenceList);
                   },
                  onError: (int code, string desc, int cbId) => {
                      ChatCallbackObject.ValueCallBackOnError<List<Presence>>(cbId, code, desc);
                  });
        }

	}
}
