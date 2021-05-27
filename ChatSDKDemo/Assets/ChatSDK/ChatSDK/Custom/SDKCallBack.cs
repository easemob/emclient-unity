using System;

namespace ChatSDK
{

    public delegate void OnError(int error, string desc);
    public delegate void OnProgress(int progress);


    public class CallBack
    {
        public Action Success;
        public OnError Error;
        public OnProgress Progress;

        internal string callbackId ;
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
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddCallback(CallbackManager.Instance().currentId, this);   
        }
        internal void ClearCallback()
        {
            Error(0, null);
            CallbackManager.Instance().RemoveCallback(int.Parse(callbackId));
        }
    }


    public class ValueCallBack<T> : CallBack
    {
        public Action<T> OnSuccessValue;
        public OnError OnError;

        /// <summary>
        /// 结果回调
        /// </summary>
        /// <param name="onSuccess">成功</param>
        /// <param name="onError">失败</param>
        public ValueCallBack(Action<T> onSuccess = null, OnError onError = null)
        {
            OnSuccessValue = onSuccess;
            OnError = onError;
            callbackId = CallbackManager.Instance().currentId.ToString();
            CallbackManager.Instance().AddValueCallback<T>(CallbackManager.Instance().currentId, this);
        }
    }
}
