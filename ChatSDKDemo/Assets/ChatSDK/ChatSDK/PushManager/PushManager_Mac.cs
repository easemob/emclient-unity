using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
using UnityEngine;
#endif

namespace ChatSDK
{
    internal sealed class PushManager_Mac : IPushManager
    {
        private IntPtr client;

        internal PushManager_Mac(IClient _client)
        {
            if (_client is Client_Mac clientMac)
            {
                client = clientMac.client;
            }
        }

        // Mac 不需要推送，直接返回；

        public override List<string> GetNoDisturbGroups() {
            var list = new List<string>();
            ChatAPINative.PushManager_GetIgnoredGroupIds(client,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"PushManager_GetIgnoredGroupIds callback with dType={dType}, size={size}");
                    if (dType == DataType.ListOfString)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            list.Add(Marshal.PtrToStringAnsi(array[i]));
                        }
                    }
                    else
                    {
                        Debug.LogError("Incorrect delegate parameters returned.");
                    }
                });

            return list;
        }

        public override PushConfig GetPushConfig()
        {
            PushConfig pushConfig = null;
            ChatAPINative.PushManager_GetPushConfig(client,
                (IntPtr[] array, DataType dType, int size, int cbId) =>
                {
                    Debug.Log($"GetPushConfig callback with dType={dType}, size={size}");
                    if (1 == size)
                    {
                        pushConfig = Marshal.PtrToStructure<PushConfig>(array[0]);
                    }
                    else
                    {
                        Debug.Log($"No push config.");
                    }
                });
            return pushConfig;
        }

        public override void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_GetUserConfigsFromServer(client, callbackId, 
                onSuccessResult: (IntPtr[] array, DataType dType, int size, int cbId) => {
                    Debug.Log($"GetPushConfigFromServer callback with dType={dType}, size={size}");
                    if (1 == size)
                    {
                        var pc = Marshal.PtrToStructure<PushConfig>(array[0]);
                        PushConfig pushConfig = new PushConfig(pc);
                        ChatCallbackObject.ValueCallBackOnSuccess<PushConfig>(cbId, pushConfig);
                    }
                    else
                    {
                        Debug.LogError($"size is not correct {size}.");
                    }
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.ValueCallBackOnError<PushConfig>(cbId, code, desc);
                });
        }

        public override void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            if (null == groupId || 0 == groupId.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_IgnoreGroupPush(client, callbackId, groupId, noDisturb,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_UpdatePushNoDisturbing(client, callbackId, noDisturb, startTime, endTime,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_UpdatePushDisplayStyle(client, callbackId, pushStyle,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        public override void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            //不支持
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;
            ChatCallbackObject.CallBackOnError(callbackId, -1, "Not Supported.");
        }

        public override void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            //不支持
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;
            ChatCallbackObject.CallBackOnError(callbackId, -1, "Not Supported.");
        }

        public override void UpdateAPNSPushToken(string token, CallBack handle = null)
        {
            //不支持
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;
            ChatCallbackObject.CallBackOnError(callbackId, -1, "Not Supported.");
        }

        public override void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            if (null == nickname || 0 == nickname.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_UpdatePushNickName(client, callbackId, nickname,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }

        internal override void ReportPushAction(string parameters, CallBack handle = null)
        {
            if (null == parameters || 0 == parameters.Length)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            int callbackId = (null != handle) ? int.Parse(handle.callbackId) : -1;

            ChatAPINative.PushManager_ReportPushAction(client, callbackId, parameters,
                onSuccess: (int cbId) => {
                    ChatCallbackObject.CallBackOnSuccess(cbId);
                },
                onError: (int code, string desc, int cbId) => {
                    ChatCallbackObject.CallBackOnError(cbId, code, desc);
                });
        }
    }
}