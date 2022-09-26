using System;
using System.Collections.Generic;

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
#endif

namespace ChatSDK
{
    internal sealed class ChatCallbackObject
    {
        private static string _GameObjectName = "chatCallbackObject";

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE || UNITY_EDITOR
        private GameObject _CallbackGameObject
        {
            get;
            set;
        }
#endif

        private static ChatCallbackObject _ChatCallbackObject
        {
            get;
            set;
        }

        public ChatCallbackQueue _CallbackQueue
        {
            set;
            get;
        }

        public static ChatCallbackObject GetInstance()
        {
            if (_ChatCallbackObject == null)
            {
                _ChatCallbackObject = new ChatCallbackObject(_GameObjectName);
            }
            return _ChatCallbackObject;
        }

        public ChatCallbackObject(string gameObjectName)
        {
            InitGameObject(gameObjectName);
        }

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX || UNITY_EDITOR_WIN || UNITY_STANDALONE
        public void Release()
        {
            if (!ReferenceEquals(_CallbackGameObject, null))
            {
                if (!ReferenceEquals(_CallbackQueue, null))
                {
                    _CallbackQueue.ClearQueue();
                }
                GameObject.Destroy(_CallbackGameObject);
                _CallbackGameObject = null;
                _CallbackQueue = null;
            }
        }

        private void InitGameObject(string gameObjectName)
        {
            DeInitGameObject(gameObjectName);
            _CallbackGameObject = new GameObject(gameObjectName);
            _CallbackQueue = _CallbackGameObject.AddComponent<ChatCallbackQueue>();
            GameObject.DontDestroyOnLoad(_CallbackGameObject);
            _CallbackGameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        private void DeInitGameObject(string gameObjectName)
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            if (!ReferenceEquals(gameObject, null))
            {
                ChatCallbackQueue callbackQueue = gameObject.GetComponent<ChatCallbackQueue>();
                if (!ReferenceEquals(callbackQueue, null))
                {
                    callbackQueue.ClearQueue();
                }
                GameObject.Destroy(gameObject);
                gameObject = null;
                callbackQueue = null;
            }
        }
#else
        public void Release()
        {
            if (!ReferenceEquals(_CallbackQueue, null))
            {
                _CallbackQueue.ClearQueue();
            }
            _CallbackQueue = null;
        }

        private void InitGameObject(string gameObjectName)
        {
            DeInitGameObject(gameObjectName);
            _CallbackQueue = new ChatCallbackQueue();
        }

        private void DeInitGameObject(string gameObjectName)
        {
            if (!ReferenceEquals(_CallbackQueue, null))
            {
                _CallbackQueue.ClearQueue();
            }
            _CallbackQueue = null;
        }
#endif

        static public void ValueCallBackOnSuccess<T>(int cbId, object obj)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                ValueCallBack<T> myhandle = (ValueCallBack<T>)CallbackManager.Instance().GetCallBackHandle(cbId);
                if(null != myhandle && null != myhandle.OnSuccessValue)
                    myhandle.OnSuccessValue((T)obj);
            });
        }

        static public void ValueCallBackOnError<T>(int cbId, int code, string desc)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                ValueCallBack<T> myhandle = (ValueCallBack<T>)CallbackManager.Instance().GetCallBackHandle(cbId);
                if(null != myhandle && null != myhandle.Error)
                    myhandle.Error(code, desc);
            });
        }

        static public void CallBackOnSuccess(int cbId)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                if(null != myhandle && null != myhandle.Success)
                    myhandle.Success();
            });
        }

        static public void CallBackResultOnSuccess(int cbId, Dictionary<string, string> failInfo)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBackResult)CallbackManager.Instance().GetCallBackHandle(cbId);
                if (null != myhandle && null != myhandle.SuccessResult)
                    myhandle.SuccessResult(failInfo);
            });
        }

        static public void CallBackOnError(int cbId, int code, string desc)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId); 
                if(null != myhandle && null != myhandle.Error)
                    myhandle.Error(code, desc);
            });
        }

        static public void CallBackResultOnError(int cbId, int code, string desc)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBackResult)CallbackManager.Instance().GetCallBackHandle(cbId);
                if (null != myhandle && null != myhandle.Error)
                    myhandle.Error(code, desc);
            });
        }

        static public void CallBackOnProgress(int cbId, int progress)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                if(null != myhandle && null != myhandle.Progress)
                    myhandle.Progress(progress);
            });
        }
    }
}
