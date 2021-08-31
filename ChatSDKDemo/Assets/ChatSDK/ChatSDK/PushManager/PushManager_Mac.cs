using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ChatSDK
{
    public class PushManager_Mac : IPushManager
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

        public List<string> GetNoDisturbGroups() {
            var list = new List<string>();
            ChatAPINative.PushManager_GetIgnoredGroupIds(client,
                (IntPtr[] array, DataType dType, int size) =>
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

        public PushConfig GetPushConfig()
        {
            PushConfig pushConfig = null;
            ChatAPINative.PushManager_GetPushConfig(client,
                (IntPtr[] array, DataType dType, int size) =>
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

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            ChatAPINative.PushManager_GetUserConfigsFromServer(client,
                onSuccessResult: (IntPtr[] array, DataType dType, int size) => {
                    Debug.Log($"GetPushConfigFromServer callback with dType={dType}, size={size}");
                    if (1 == size)
                    {
                        PushConfig pushConfig = Marshal.PtrToStructure<PushConfig>(array[0]);
                        ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.OnSuccessValue(pushConfig); });
                    }
                    else
                    {
                        Debug.LogError($"size is not correct {size}.");
                    }
                },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            if (null == groupId)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.PushManager_IgnoreGroupPush(client, groupId, noDisturb,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdatePushNoDisturbing(client, noDisturb, startTime, endTime,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdatePushDisplayStyle(client, pushStyle,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            if (null == token)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.PushManager_UpdateFCMPushToken(client, token,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            if (null == token)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.PushManager_UpdateHMSPushToken(client, token,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }

        public void UpdateAPNSPuthToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            if (null == nickname)
            {
                Debug.LogError("Mandatory parameter is null!");
                return;
            }
            ChatAPINative.PushManager_UpdatePushNickName(client, nickname,
                onSuccess: () => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Success(); }); },
                onError: (int code, string desc) => { ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => { handle?.Error(code, desc); }); });
        }
    }
}