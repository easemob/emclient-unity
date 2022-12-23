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

        // No need to parse
        internal void NativeCall(string methodName, JSONNode jn = null, CallBack callback = null)
        {
            callbackManager.AddCallbackAction(callback);
            CWrapperNative.NativeCall(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // Need parse, T is target type
        internal void NativeCall<T>(string methodName, JSONNode jn = null, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction<T>(callback, process);
            CWrapperNative.NativeCall(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // No need to parse
        internal string NativeGet(string methodName, JSONNode jn = null, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction(callback, process);
            return CWrapperNative.NativeGet(managerName, methodName, jn, callback?.callbackId ?? "");
        }

        // Need parse, T is target type
        internal string NativeGet<T>(string methodName, JSONNode jn, CallBack callback = null, Process process = null)
        {
            callbackManager.AddCallbackAction<T>(callback, process);
            return CWrapperNative.NativeGet(managerName, methodName, jn, callback?.callbackId ?? "");
        }
    }
}
