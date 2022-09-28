using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{
	internal sealed class UserInfoManager_Common : IUserInfoManager
	{
		private IntPtr client;

		internal UserInfoManager_Common(IClient _client)
		{
			if (_client is Client_Common clientCommon)
			{
				client = clientCommon.client;
			}
		}

		public override void UpdateOwnInfo(UserInfo userInfo, CallBack handle = null)
		{
			if (null == userInfo)
			{
				Debug.LogError("Mandatory parameter is null!");
				return;
			}

			int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

			IntPtr up = Marshal.AllocCoTaskMem(Marshal.SizeOf(userInfo));
			Marshal.StructureToPtr(userInfo, up, false);

			ChatAPINative.UserInfoManager_UpdateOwnInfo(client, callbackId, up,
				(int cbId) =>
				{
					try
					{
						ChatCallbackObject.CallBackOnSuccess(cbId);
					}
					catch (NullReferenceException nre)
					{
						Debug.LogWarning($"NullReferenceException: {nre.StackTrace}");
					}

				},
				(int code, string desc, int cbId) =>
				{
					ChatCallbackObject.CallBackOnError(cbId, code, desc);
				}
				);

			Marshal.FreeCoTaskMem(up);
		}

		// 暂不提供该方法
		internal void UpdateOwnByAttribute(UserInfoType userInfoType, string value, ValueCallBack<string> handle = null)
		{
			if (null == value)
			{
				Debug.LogError("Mandatory parameter is null!");
				return;
			}

			int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

			ChatAPINative.UserInfoManager_UpdateOwnInfoByAttribute(client, callbackId, (int)userInfoType, value,

				(IntPtr[] array, DataType dType, int size, int cbId) => {

					Debug.Log($"UpdateOwnByAttribute callback with size={size}.");
					if (DataType.String == dType && 1 == size)
					{
						string response = Marshal.PtrToStringAnsi(array[0]);
						ChatCallbackObject.ValueCallBackOnSuccess<string>(cbId, response);
					}
					else
					{
						Debug.LogError("Incorrect delegate parameters returned.");
					}
				},

				(int code, string desc, int cbId) => {
					Debug.LogError($"UpdateOwnByAttribute failed with code={code},desc={desc}");
					ChatCallbackObject.ValueCallBackOnError<string>(cbId, code, desc);
				});

		}

		public override void FetchUserInfoByUserId(List<string> idList, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
		{
			if (null == idList || 0 == idList.Count)
			{
				Debug.LogError("Mandatory parameter is null!");
				return;
			}

			int idSize = idList.Count;
			var idArray = idList.ToArray();
			int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

			ChatAPINative.UserInfoManager_FetchUserInfoByUserId(client, callbackId, idArray, idSize,

				(IntPtr[] array, DataType dType, int size, int cbId) => {

					Debug.Log($"FetchUserInfoByUserId callback with size={size}.");
					if (DataType.ListOfGroup == dType && size >= 0)
					{
						Dictionary<string, UserInfo> userinfoMap = new Dictionary<string, UserInfo>(); 
						for (int j=0; j<size; j++)
                        {
							UserInfo ui = Marshal.PtrToStructure<UserInfo>(array[j]);
							ui.Unmarshall();
							userinfoMap.Add(ui.userId, ui);
                        }
						if(0 == userinfoMap.Count)
                        {
							Debug.Log("Cannot find any userinfo by user ids.");
						}
						ChatCallbackObject.ValueCallBackOnSuccess<Dictionary<string, UserInfo>>(cbId, userinfoMap);
					}
					else
					{
						Debug.LogError("Incorrect delegate parameters returned.");
					}
				},

				(int code, string desc, int cbId) => {
					Debug.LogError($"FetchUserInfoByUserId failed with code={code},desc={desc}");
					ChatCallbackObject.ValueCallBackOnError<Dictionary<string, UserInfo>>(cbId, code, desc);
				});
		}

		// 暂不提供该方法
		internal void FetchUserInfoByAttribute(List<string> idList, List<UserInfoType> attrs, ValueCallBack<Dictionary<string, UserInfo>> handle = null)
		{
			if (null == idList || 0 == idList.Count || null == attrs || 0 == attrs.Count)
			{
				Debug.LogError("Mandatory parameter is null!");
				return;
			}

			int idSize = idList.Count;
			var idArray = idList.ToArray();

			int attrSize = attrs.Count;
			var attrArray = new int[attrSize];
			int i = 0;
			foreach (UserInfoType attr in attrs)
			{
				attrArray[i] = (int)attr;
				i++;
			}

			int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

			ChatAPINative.UserInfoManager_FetchUserInfoByAttribute(client, callbackId, idArray, idSize, attrArray, attrSize,

				(IntPtr[] array, DataType dType, int size, int cbId) => {

					Debug.Log($"FetchUserInfoByAttribute callback with size={size}.");
					if (DataType.ListOfGroup == dType && size >= 0)
					{
						Dictionary<string, UserInfo> userinfoMap = new Dictionary<string, UserInfo>();
						for (int j = 0; j < size; j++)
						{
							UserInfo ui = Marshal.PtrToStructure<UserInfo>(array[j]);
							ui.Unmarshall();
							userinfoMap.Add(ui.userId, ui);
						}
						if (0 == userinfoMap.Count)
						{
							Debug.Log("Cannot find any userinfo by user ids.");
						}
						ChatCallbackObject.ValueCallBackOnSuccess<Dictionary<string, UserInfo>>(cbId, userinfoMap);
					}
					else
					{
						Debug.LogError("Incorrect delegate parameters returned.");
					}
				},

				(int code, string desc, int cbId) => {
					Debug.LogError($"FetchUserInfoByAttribute failed with code={code},desc={desc}");
					ChatCallbackObject.ValueCallBackOnError<Dictionary<string, UserInfo>>(cbId, code, desc);
				});
		}
	}
}
