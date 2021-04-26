using System;

namespace ChatSDK
{

    public delegate void OnSuccess();
    public delegate void OnError(int error, string desc);
    public delegate void OnProgress(int progress);

    public class SDKCallBack
    {
        public OnSuccess Success;
        public OnError Error;
        public OnProgress Progress;

        public SDKCallBack(OnSuccess onSuccess = null, OnProgress onProgress = null, OnError onError = null)
        {
            Success = onSuccess;
            Error = onError;
            Progress = onProgress;
        }
    }

    public class SDKValueCallBack<T>
    {
        public Func<T> OnSuccessValue;
        public OnError Error;

        public SDKValueCallBack(Func<T> onSuccess = null, OnError onError = null)
        {
            OnSuccessValue = onSuccess;
            Error = onError;
        }
    }
}
