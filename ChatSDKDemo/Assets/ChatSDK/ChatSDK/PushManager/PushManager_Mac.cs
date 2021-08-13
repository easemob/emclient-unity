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
        // to-do: what means about above comments? no need to implement??
        // TODO: 需要实现
        public List<string> GetNoDisturbGroups() {
            return null;
        }

        //public void GetNoDisturbGroupsFromServer(ValueCallBack<List<string>> handle = null)
        //{
        //    ChatAPINative.PushManager_GetIgnoredGroupIds(client,
        //        onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {
        //            List<string> ignoreList = new List<string>();
        //            if (dType == DataType.ListOfString && dSize > 0)
        //            {
        //                for (int i = 0; i < dSize; i++)
        //                {
        //                    var banItem = Marshal.PtrToStringAnsi(data[i]);
        //                    ignoreList.Add(banItem);
        //                }

        //                handle?.OnSuccessValue(ignoreList);
        //            }
        //            else
        //            {
        //                Debug.LogError($"DataType information expected.");
        //            }
        //        },
        //        handle?.Error);
        //}

        // TODO
        public PushConfig GetPushConfig()
        {
            return null;
            //ChatAPINative.PushManager_GetPushConfig(client,
            //                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {

            //                    if (1 == dSize)
            //                    {
            //                        PushConfig pushConfig = new PushConfig();
            //                        Marshal.PtrToStructure(data[0], pushConfig);
            //                        handle?.OnSuccessValue(pushConfig);
            //                    }
            //                    else
            //                    {
            //                        Debug.LogError($"size is not correct {dSize}.");
            //                    }
            //                },
            //                handle?.OnError);
        }

        public void GetPushConfigFromServer(ValueCallBack<PushConfig> handle = null)
        {
            ChatAPINative.PushManager_GetUserConfigsFromServer(client,
                onSuccessResult: (IntPtr[] data, DataType dType, int dSize) => {

                    if (1 == dSize)
                    {
                        PushConfig pushConfig = new PushConfig();
                        Marshal.PtrToStructure(data[0], pushConfig);
                        handle?.OnSuccessValue(pushConfig);
                    }
                    else
                    {
                        Debug.LogError($"size is not correct {dSize}.");
                    }
                },
                handle?.Error);
        }

        public void SetGroupToDisturb(string groupId, bool noDisturb, CallBack handle = null)
        {
            ChatAPINative.PushManager_IgnoreGroupPush(client, groupId, noDisturb,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public void SetNoDisturb(bool noDisturb, int startTime = 0, int endTime = 24, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdatePushNoDisturbing(client, noDisturb, startTime, endTime,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public void SetPushStyle(PushStyle pushStyle, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdatePushDisplayStyle(client, pushStyle,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public void UpdateFCMPushToken(string token, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdateFCMPushToken(client, token,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public void UpdateHMSPushToken(string token, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdateHMSPushToken(client, token,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }

        public void UpdateAPNSPuthToken(string token, CallBack handle = null)
        {
            handle?.ClearCallback();
        }

        public void UpdatePushNickName(string nickname, CallBack handle = null)
        {
            ChatAPINative.PushManager_UpdatePushNickName(client, nickname,
                onSuccess: () => handle?.Success(),
                onError: (int code, string desc) => handle?.Error(code, desc));
        }
    }
}