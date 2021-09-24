using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ChatSDK
{
    // ValueCallback<T>
    internal delegate void OnSuccessResultV2(IntPtr header, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] IntPtr[] data, DataType dType, int size, int callbackId);
    internal delegate void OnSuccessResult([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]IntPtr[] data, DataType dType, int size, int callbackId);
    internal delegate void OnErrorV2(int code, string desc, int callbackId);

    public delegate void OnSuccess(int callbackId);
    public delegate void OnError(int code, string desc);
    public delegate void OnProgress(int progress);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class CallBack
    {
        public Action Success;
        public OnError Error;
        public OnProgress Progress;

        internal string callbackId;
        /// <summary>
        /// 结果回调
        /// </summary>
        /// <param name="onSuccess">成功</param>
        /// <param name="onProgress">进度变化</param>
        /// <param name="onError">失败</param>
        public CallBack(Action onSuccess = null, OnProgress onProgress = null, OnError onError = null)
        {
            Success = onSuccess;
            Error = onError;
            Progress = onProgress;
            callbackId = CallbackManager.Instance().CurrentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().CurrentId, this);
        }
        internal void ClearCallback()
        {
            Error(0, null);
            CallbackManager.Instance().RemoveCallback(int.Parse(callbackId));
        }

        ~CallBack()
        {
            Debug.Log($"CallBack ${callbackId} finalized!");
        }
    }

    public class ValueCallBack<T> : CallBack
    {
        public Action<T> OnSuccessValue;

        /// <summary>
        /// 结果回调
        /// </summary>
        /// <param name="onSuccess">成功</param>
        /// <param name="onError">失败</param>
        public ValueCallBack(Action<T> onSuccess = null, OnError onError = null)
        {
            OnSuccessValue = onSuccess;
            Error = onError;
            callbackId = CallbackManager.Instance().CurrentId.ToString();
            CallbackManager.Instance().AddValueCallback<T>(CallbackManager.Instance().CurrentId, this);
        }
    }
}
