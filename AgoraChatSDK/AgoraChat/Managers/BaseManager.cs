using System;
using AgoraChat.SimpleJSON;

namespace AgoraChat
{
    public abstract class BaseManager
    {
        internal string managerName;
        internal CallbackManager callbackManager;
        internal BaseManager(NativeListener listener, string manager)
        {
            managerName = manager;
            callbackManager = listener.callbackManager;
        }

        // 不需要解析
        internal void NativeCall(string methodName, JSONNode jn = null, CallBack callback = null)
        {
            callbackManager.AddCallbackAction(callback);
            CWrapperNative.NativeCall(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // 需要解析，T为解析类型
        internal void NativeCall<T>(string methodName, JSONNode jn = null, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction<T>(callback, process);
            CWrapperNative.NativeCall(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // 不需要解析
        internal string NativeGet(string methodName, JSONNode jn = null, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction(callback, process);
            return CWrapperNative.NativeGet(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // 需要解析，T为解析类型
        internal string NativeGet<T>(string methodName, JSONNode jn, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction<T>(callback, process);
            return CWrapperNative.NativeGet(managerName, methodName, jn, callback?.callbackId ?? "");
        }
    }
}
