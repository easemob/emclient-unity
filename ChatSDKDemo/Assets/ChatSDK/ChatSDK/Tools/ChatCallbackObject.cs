using System;
using UnityEngine;

namespace ChatSDK
{
    internal sealed class ChatCallbackObject
    {
        private static string _GameObjectName = "chatCallbackObject";
        private GameObject _CallbackGameObject
        {
            get;
            set;
        }

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

        static public void ValueCallBackOnSuccess<T>(int cbId, object obj)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                ValueCallBack<T> myhandle = (ValueCallBack<T>)CallbackManager.Instance().GetCallBackHandle(cbId);
                myhandle?.OnSuccessValue((T)obj);
            });
        }

        static public void ValueCallBackOnError<T>(int cbId, int code, string desc)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                ValueCallBack<T> myhandle = (ValueCallBack<T>)CallbackManager.Instance().GetCallBackHandle(cbId);
                myhandle?.Error(code, desc);
            });
        }

        static public void CallBackOnSuccess(int cbId)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                myhandle?.Success();
            });
        }

        static public void CallBackOnError(int cbId, int code, string desc)
        {
            ChatCallbackObject.GetInstance()._CallbackQueue.EnQueue(() => {
                var myhandle = (CallBack)CallbackManager.Instance().GetCallBackHandle(cbId);
                myhandle?.Error(code, desc);
            });
        }
    }
}