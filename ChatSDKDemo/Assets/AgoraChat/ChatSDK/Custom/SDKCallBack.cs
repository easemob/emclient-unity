using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace AgoraChat
{

	/**
	* \~chinese
	* 执行带结果的成功回调。
	*
	* @param failInfo  错误信息。
	*
	* \~english
	* The callback for a method execution success with result.
	*
	* @param desc      The error information.
	*/
	public delegate void OnSuccessWithResult(Dictionary<string, int> failInfo);

    /**
	* \~chinese
	* 执行错误回调。 
	*
	* @param code	   错误码。
	* @param desc      错误描述。
	*
	* \~english
	* The callback for a method execution failure.
	*
	* @param code	   The error code.
	* @param desc      The error description.
	*/   
    public delegate void OnError(int code, string desc);

    /**
	* \~chinese
	* 执行进度回调。
	*
	* @param progress   执行进度值，取值范围为 [0,100]。
	*
	* \~english
	* The callback for the method execution progress.
	*
	* @param progress   The execution progress value that ranges from 0 to 100 in percentage.
	* 
	*/
    public delegate void OnProgress(int progress);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	/**
	    * \~chinese
	    * 不带返回值的回调类。
	    *
	    * \~english
	    * The class of callbacks without a return value.
		* 
	    */
    public class CallBack   
    {
		/**
	    * \~chinese
	    * 成功回调。
	    *
	    * \~english
	    * The success callback.
		* 
	    */
        public Action Success;
		/**
	    * \~chinese
	    * 错误回调。
	    *
	    * \~english
	    * The error callback.
		* 
	    */    
        public OnError Error;  
		/**
	    * \~chinese
	    * 进度回调。
	    *
	    * \~english
	    * The progress callback.
		* 
	    */
        public OnProgress Progress; 

        internal string callbackId;

        /**
	    * \~chinese
	    * 结果回调构造方法。
	    *
	    * @param onSuccess      成功回调。
	    * @param onProgress     进度回调。
	    * @param onError        错误回调。
	    *
	    * \~english
	    * The result callback constructor.
	    *
	    * @param onSuccess      The success callback.
	    * @param onProgress     The progress callback.
	    * @param onError        The error callback.
	    * 
	    */
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

	public class CallBackResult
	{
		/**
		* \~chinese
		* 带结果的成功回调。
		*
		* \~english
		* The success callback with result.
		*
		*/
		public OnSuccessWithResult SuccessResult;

		/**
		* \~chinese
		* 错误回调。
		*
		* \~english
		* The error callback.
		*
		*/
		public OnError Error;

		internal string callbackId;

		public CallBackResult(OnSuccessWithResult onSuccessResult = null, OnError onError = null)
		{
			SuccessResult = onSuccessResult;
			Error = onError;
			callbackId = CallbackManager.Instance().CurrentId.ToString();
			CallbackManager.Instance().AddCallbackResult(CallbackManager.Instance().CurrentId, this);
		}

		~CallBackResult()
		{
			Debug.Log($"CallBack ${callbackId} finalized!");
		}
	}

	/**
	* \~chinese
	* 带有返回值的回调类。
	* 
	* \~english
	* The class of callbacks with a return value.
	*/
	public class ValueCallBack<T> : CallBack
    {
	  /**
	   * \~chinese
	   * 带有返回值的成功回调方法。
	   * 
	   * \~english
	   * The success callback with a return value.
	   */
        public Action<T> OnSuccessValue;

        /**
	    * \~chinese
	    * 包含返回值的回调类的构造方法。
	    *
	    * @param onSuccess      成功回调。
	    * @param onError        错误回调。
	    *
	    * \~english
	    * The constructor for the class of callbacks with a return value.
	    *
	    * @param onSuccess      The success callback.
	    * @param onError        The error callback.
	    * 
	    */
        public ValueCallBack(Action<T> onSuccess = null, OnError onError = null)
        {
            OnSuccessValue = onSuccess;
            Error = onError;
            callbackId = CallbackManager.Instance().CurrentId.ToString();
            CallbackManager.Instance().AddValueCallback<T>(CallbackManager.Instance().CurrentId, this);
        }
    }
}
