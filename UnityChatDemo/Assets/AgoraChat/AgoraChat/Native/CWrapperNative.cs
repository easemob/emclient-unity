using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AgoraChat
{


    public sealed class CWrapperNative
    {
        internal static void NativeCall(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            LogPrinter.Log($"---NativeCall: {manager}: {method}: {json}: {callbackId}");
            _NativeCall(manager, method, json?.ToString(), callbackId ?? "");
        }


        internal static string NativeGet(string manager, string method, SimpleJSON.JSONNode json, string callbackId = null)
        {
            LogPrinter.Log($"---NativeGet: {manager}: {method}: {json}: {callbackId}");

            IntPtr ptr = _NativeGet(manager, method, json?.ToString(), callbackId ?? "");
            string native_string = Tools.PtrToString(ptr);
            string ret = Tools.GetUnicodeStringFromUTF8(native_string);
            Marshal.FreeHGlobal(ptr);

            LogPrinter.Log($"---NativeGet get: {ret}");
            return ret;
        }

        #region DllImport
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        public const string MyLibName = "ChatCWrapper";
#else

#if UNITY_IPHONE
		public const string MyLibName = "__Internal";
#else
        public const string MyLibName = "ChatCWrapper";
#endif
#endif

        [DllImport(MyLibName)]
        internal static extern void Init(int type, NativeListenerEvent listener);

        [DllImport(MyLibName)]
        internal static extern void UnInit();

        [DllImport(MyLibName)]
        private extern static void _NativeCall(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, string callbackId = null);


        [DllImport(MyLibName)]
#if _WIN32
        private extern static IntPtr _NativeGet(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, string callbackId = null);
#else
        private extern static IntPtr _NativeGet(string manager, string method, [In, MarshalAs(UnmanagedType.LPTStr)] string jsonString = null, string callbackId = null);
#endif
        #endregion engine callbacks
    }
}