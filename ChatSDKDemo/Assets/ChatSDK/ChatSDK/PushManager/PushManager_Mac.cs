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

            //make a array of IntPtr(point to TOArray)
            TOArray toArray = new TOArray();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArray));
            Marshal.StructureToPtr(toArray, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.PushManager_GetIgnoredGroupIds(client, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArray>(array[0]);

            var result = new List<string>();

            //cannot get any message
            if (returnTOArray.Size == 0)
            {
                Debug.Log($"Cannot find any group ids with NoDisturb.");
                Marshal.FreeCoTaskMem(intPtr);
                return result;
            }

            for(int i=0; i<returnTOArray.Size; i++)
            {
                result.Add(Marshal.PtrToStringAnsi(returnTOArray.Data[i]));
            }

            ChatAPINative.PushManager_ReleaseStringList(array, 1);
            Marshal.FreeCoTaskMem(intPtr);
            return result;

        }

        public PushConfig GetPushConfig()
        {
            //make a array of IntPtr(point to TOArray)
            TOArray toArray = new TOArray();
            IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(toArray));
            Marshal.StructureToPtr(toArray, intPtr, false);
            var array = new IntPtr[1];
            array[0] = intPtr;

            ChatAPINative.PushManager_GetPushConfig(client, array, 1);

            //get data from IntPtr
            var returnTOArray = Marshal.PtrToStructure<TOArray>(array[0]);

            //cannot get any message
            if (returnTOArray.Size == 0)
            {
                Debug.Log($"No group config is set.");
                Marshal.FreeCoTaskMem(intPtr);
                return null;
            }

            PushConfig pushConfig = Marshal.PtrToStructure<PushConfig>(returnTOArray.Data[0]);
            ChatAPINative.ConversationManager_ReleasePushConfigList(array, 1);
            Marshal.FreeCoTaskMem(intPtr);
            return pushConfig;
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