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
        }
    }


    public class ValueCallBack<T>
    {
        public Action<T> OnSuccessValue;
        public OnError Error;

        /// <summary>
        /// 结果回调
        /// </summary>
        /// <param name="onSuccess">成功</param>
        /// <param name="onError">失败</param>
        public ValueCallBack(Action<T> onSuccess = null, OnError onError = null)
        {
            OnSuccessValue = onSuccess;
            Error = onError;
        }
    }
}
